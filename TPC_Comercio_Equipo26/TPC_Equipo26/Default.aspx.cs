using System;
using System.Collections.Generic;
using System.Linq;
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
                    chkIncluirInactivos.Checked = true;
                }
            }
            catch
            {Response.Redirect("Error.aspx",false); }
        }

        protected void CargarArticulos()
        {
            
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            List<Articulo> listaArticulos = articuloNegocio.Listar();
            if (!chkIncluirInactivos.Checked)
            {
                listaArticulos = listaArticulos.Where(a => a.Activo).ToList();
            }
            Session["ListaArticulos"] = articuloNegocio.Listar();
            gvArticulos.DataSource = Session["ListaArticulos"];
            gvArticulos.DataBind();
            pnlFiltroAvanzado.Visible = chkAvanzado.Checked;
        }
        protected void Filtro_TextChanged(object sender, EventArgs e)
        {
            //aca filtro rapido
            List<Articulo> lista = (List<Articulo>)Session["listaArticulos"];
            List<Articulo> listaFiltrada = lista.FindAll(x => x.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()));
            gvArticulos.DataSource = listaFiltrada;
            gvArticulos.DataBind();
        }

        protected void FiltroMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarArticulos();
        }

        protected void FiltroCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarArticulos();
        }


        private void FiltrarArticulos()
        {
            List<Articulo> lista = (List<Articulo>)Session["ListaArticulos"];

            if (lista != null)
            {
                lista = lista.Where(x => x.Activo || chkIncluirInactivos.Checked).ToList();

                if (ddlCampo.SelectedItem.Text == "Marca" && ddlCriterio.SelectedValue != "Seleccionar Marca")
                {
                    int idMarca = int.Parse(ddlCriterio.SelectedValue);
                    lista = lista.Where(x => x.Marca.ID == idMarca).ToList();
                }
                else if (ddlCampo.SelectedItem.Text == "Categoría" && ddlCriterio.SelectedValue != "Seleccionar Categoría")
                {
                    int idCategoria = int.Parse(ddlCriterio.SelectedValue);
                    lista = lista.Where(x => x.Categoria.ID == idCategoria).ToList();
                }

                gvArticulos.DataSource = lista;
            }
            else
            {
                gvArticulos.DataSource = null;
            }

            gvArticulos.DataBind();
        }


        protected void BtnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            txtFiltro.Text = string.Empty;
            chkIncluirInactivos.Checked = false;
            chkAvanzado.Checked = false;
            pnlFiltroAvanzado.Visible = false;
            ddlCampo.SelectedIndex = 0;
            ddlCriterio.Items.Clear();
            ddlCriterio.Items.Add("Comienza con");
            txtFiltroAvanzado.Text = string.Empty;

            CargarArticulos();
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
                ddlCampo.SelectedIndex = 0;
                ddlCriterio.Items.Clear();
                txtFiltroAvanzado.Text = string.Empty;
            }
        }

        protected void ddlCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCriterio.Items.Clear();

            string selectedItemText = ddlCampo.SelectedItem.Text;

            if (selectedItemText == "Precio Unitario ($)")
            {
                ddlCriterio.Items.Add("Igual a");
                ddlCriterio.Items.Add("Mayor a");
                ddlCriterio.Items.Add("Menor a");
            }
            else if (selectedItemText == "Descripción")
            {
                ddlCriterio.Items.Add("Comienza con");
                ddlCriterio.Items.Add("Contiene");
                ddlCriterio.Items.Add("Termina con");
            }
            else if (selectedItemText == "Marca")
            {
                MarcaNegocio marcaNegocio = new MarcaNegocio();
                List<Marca> marcas = marcaNegocio.Listar();

                ddlCriterio.Items.Add("Seleccionar Marca");
                foreach (Marca marca in marcas)
                {
                    ddlCriterio.Items.Add(new ListItem(marca.Descripcion, marca.ID.ToString()));
                }
            }
            else if (selectedItemText == "Categoría")
            {
                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                List<Categoria> categorias = categoriaNegocio.Listar();

                ddlCriterio.Items.Add("Seleccionar Categoría");
                foreach (Categoria categoria in categorias)
                {
                    ddlCriterio.Items.Add(new ListItem(categoria.Descripcion, categoria.ID.ToString()));
                }
            }

            if (selectedItemText == "Marca" || selectedItemText == "Categoría")
            {
                txtFiltroAvanzado.Visible = false;
                btnBuscar.Visible = false;
            }
            else
            {
                txtFiltroAvanzado.Visible = true;
                btnBuscar.Visible = true;
            }
         
        }


        protected void btnBuscar_Click(object sender, EventArgs e)
        {
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
            }
        }

        protected void ddlCriterio_SelectedIndexChanged(object sender, EventArgs e)
        {
           btnBuscar_Click(sender, e);
        }

        protected void chkIncluirInactivos_CheckedChanged(object sender, EventArgs e)
        {
            FiltrarArticulos();
        }
    }

}
