using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPC_Equipo26.Dominio;
using TPC_Equipo26.Negocio;


namespace TPC_Equipo26
{
    public partial class Default : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    CargarArticulos();
                    CargarMarcasYCategorias();
                }
            }
            catch
            { Response.Redirect("Error.aspx", false); }
        }

        protected void CargarArticulos()
        {
            try
            {
                chkIncluirInactivos.Checked = false;
                ArticuloNegocio articuloNegocio = new ArticuloNegocio();
                List<Articulo> listaArticulos = articuloNegocio.Listar();
                Session["ListaArticulos"] = listaArticulos;
                FiltrarArticulos();
                pnlFiltroAvanzado.Visible = chkAvanzado.Checked;
                if (chkAvanzado.Checked)
                {
                    CargarMarcasYCategorias();
                }
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void CargarMarcasYCategorias()
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            List<Marca> marcas = marcaNegocio.Listar();
            ddlMarca.DataSource = marcas;
            ddlMarca.DataTextField = "Descripcion";
            ddlMarca.DataValueField = "ID";
            ddlMarca.DataBind();
            ddlMarca.Items.Insert(0, new ListItem("Seleccione Marca", "0"));

            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            List<Categoria> categorias = categoriaNegocio.Listar();
            ddlCategoria.DataSource = categorias;
            ddlCategoria.DataTextField = "Descripcion";
            ddlCategoria.DataValueField = "ID";
            ddlCategoria.DataBind();
            ddlCategoria.Items.Insert(0, new ListItem("Seleccione Categoría", "0"));
        }

        private void FiltrarArticulos()
        {
            List<Articulo> listaArticulos = (List<Articulo>)Session["ListaArticulos"];
            if (!chkIncluirInactivos.Checked)
            {
                listaArticulos = listaArticulos.Where(x => x.Activo).ToList();
            }
            if (!string.IsNullOrEmpty(txtFiltro.Text.Trim()))
            {
                string filtro = txtFiltro.Text.Trim().ToUpper();
                listaArticulos = listaArticulos
                    .Where(x => x.Nombre.ToUpper().Contains(filtro) ||
                                x.Codigo.ToUpper().Contains(filtro) ||
                                x.Descripcion.ToUpper().Contains(filtro) ||
                                x.StockMin.ToString().Contains(filtro))
                    .ToList();
            }
            if (ddlMarca.SelectedIndex > 0)
            {
                string marcaSeleccionada = ddlMarca.SelectedItem.Text;
                listaArticulos = listaArticulos.Where(x => x.Marca.Descripcion.Equals(marcaSeleccionada)).ToList();
            }
            if (ddlCategoria.SelectedIndex > 0)
            {
                string CategoriaSeleccionada = ddlCategoria.SelectedItem.Text;
                listaArticulos = listaArticulos.Where(x => x.Categoria.Descripcion.Equals(CategoriaSeleccionada)).ToList();
            }
            if (chkOrdenarAZ.Checked)
            {
                listaArticulos = listaArticulos.OrderBy(x => x.Nombre).ToList();
            }
            else
            {

                listaArticulos = listaArticulos.OrderByDescending(x => x.Nombre).ToList();
            }
            if (chkOrdenarPorStock.Checked)
            {
                listaArticulos = listaArticulos.OrderByDescending(x => x.StockMin).ToList();
            }
            else
            {
                listaArticulos = listaArticulos.OrderBy(x => x.StockMin).ToList();
            }
            gvArticulos.DataSource = listaArticulos;
            gvArticulos.DataBind();
        }


        protected void Filtro_TextChanged(object sender, EventArgs e)
        {
            FiltrarArticulos();
        }
        protected void chkIncluirInactivos_CheckedChanged(object sender, EventArgs e)
        {
            FiltrarArticulos();
        }

        protected void ddlMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarArticulos();
        }

        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarArticulos();
        }

        protected void chkOrdenarAZ_CheckedChanged(object sender, EventArgs e)
        {
            FiltrarArticulos();
        }

        protected void chkOrdenarPorStock_CheckedChanged(object sender, EventArgs e)
        {
            FiltrarArticulos();
        }

        protected void BtnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            try
            {
                txtFiltro.Text = string.Empty;
                chkAvanzado.Checked = false;
                pnlFiltroAvanzado.Visible = false;
                chkIncluirInactivos.Checked = false;
                ddlMarca.SelectedIndex = 0;
                ddlCategoria.SelectedIndex = 0;
                // CargarMarcasYCategorias();
                CargarArticulos();
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void gvArticulos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvArticulos.PageIndex = e.NewPageIndex;

                gvArticulos.DataSource = Session["ListaArticulos"];
                gvArticulos.DataBind();
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx");
            }
        }

        protected void gvArticulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = gvArticulos.SelectedDataKey.Value.ToString();
            Response.Redirect("AltaArticulos.aspx?ID=" + id);
        }

        protected void chkAvanzado_CheckedChanged(object sender, EventArgs e)
        {
            pnlFiltroAvanzado.Visible = chkAvanzado.Checked;
            txtFiltro.Enabled = !chkAvanzado.Checked;
            if (!chkAvanzado.Checked)
            {
                ddlMarca.SelectedIndex = 0;
                ddlCategoria.SelectedIndex = 0;

            }
            CargarArticulos();
        }




        protected void btnBuscar_Click(object sender, EventArgs e)
        {/*
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                gvArticulos.DataSource = negocio.Filtrar(
                    ddlCampo.SelectedItem.Text,
                ddlCriterio.SelectedItem.Text,
                    txtFiltroAvanzado.Text,
                    chkIncluirInactivos.Checked);
                gvArticulos.DataBind();
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx");
            
            }*/
        }


    }

}
