﻿using System;
using System.Collections.Generic;
using System.Linq;
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
                    btnLimpiarFiltros.Visible = false;
                    
                }               
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        private void CargarArticulos()
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
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        private void CargarMarcasYCategorias()
        {
            try
            {
                MarcaNegocio marcaNegocio = new MarcaNegocio();
                List<Marca> marcas = marcaNegocio.Listar();
                marcas = marcas.OrderBy(p => p.Descripcion).ToList();
                ddlMarca.DataSource = marcas;
                ddlMarca.DataTextField = "Descripcion";
                ddlMarca.DataValueField = "ID";
                ddlMarca.DataBind();
                ddlMarca.Items.Insert(0, new ListItem("Seleccione Marca", "0"));

                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                List<Categoria> categorias = categoriaNegocio.Listar();
                categorias = categorias.OrderBy(p => p.Descripcion).ToList();
                ddlCategoria.DataSource = categorias;
                ddlCategoria.DataTextField = "Descripcion";
                ddlCategoria.DataValueField = "ID";
                ddlCategoria.DataBind();
                ddlCategoria.Items.Insert(0, new ListItem("Seleccione Categoría", "0"));
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        private void FiltrarArticulos()
        {
            List<Articulo> listaArticulos = (List<Articulo>)Session["ListaArticulos"];
            DatoArticuloNegocio datoNegocio = new DatoArticuloNegocio();

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
            string ordenSeleccionado = ddlOrdenarPor.SelectedValue;
            switch (ordenSeleccionado)
            {
                case "DescripcionAZ":
                    listaArticulos = listaArticulos.OrderBy(x => x.Descripcion).ToList();
                    break;
                case "DescripcionZA":
                    listaArticulos = listaArticulos.OrderByDescending(x => x.Descripcion).ToList();
                    break;
                case "StockDisponibleAsc":
                    listaArticulos = listaArticulos.OrderBy(x => datoNegocio.ObtenerStockArticulo(x.ID)).ToList();
                    break;
                case "StockDisponibleDesc":
                    listaArticulos = listaArticulos.OrderByDescending(x => datoNegocio.ObtenerStockArticulo(x.ID)).ToList();
                    break;
                case "PrecioUnitarioAsc":
                    listaArticulos = listaArticulos.OrderBy(x => datoNegocio.ObtenerPrecioArticulo(x.ID)).ToList();
                    break;
                case "PrecioUnitarioDesc":
                    listaArticulos = listaArticulos.OrderByDescending(x => datoNegocio.ObtenerPrecioArticulo(x.ID)).ToList();
                    break;
                case "IdAsc":
                    listaArticulos = listaArticulos.OrderBy(x => x.ID).ToList();
                    break;
                case "IdDesc":
                    listaArticulos = listaArticulos.OrderByDescending(x => x.ID).ToList();
                    break;
                default:
                    listaArticulos = listaArticulos.OrderBy(x => x.Descripcion).ToList();
                    break;
            }

            Session["ListaArticulosFiltrada"] = listaArticulos;

            gvArticulos.DataSource = listaArticulos;
            gvArticulos.DataBind();
            MostrarBotonRestablecer();
        }

        private void MostrarBotonRestablecer()
        {
            bool filtroActivo = !string.IsNullOrEmpty(txtFiltro.Text.Trim()) ||
                                ddlMarca.SelectedIndex > 0 ||
                                ddlCategoria.SelectedIndex > 0 ||
                                !string.IsNullOrEmpty(ddlOrdenarPor.SelectedValue) ||
                            chkIncluirInactivos.Checked;

            btnLimpiarFiltros.Visible = filtroActivo;
        }

        protected void Filtro_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFiltro.Text.Trim()))
            {
                txtFiltro.Text = string.Empty;
            }
            FiltrarArticulos();
            string script = $"BuscarVacio('{txtFiltro.ClientID}');";
            ClientScript.RegisterStartupScript(this.GetType(), "BuscarVacio", script, true);
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

        protected void ddlOrdenarPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarArticulos();
        }

        protected void BtnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            txtFiltro.Text = string.Empty;
            chkAvanzado.Checked = false;
            pnlFiltroAvanzado.Visible = false;
            chkIncluirInactivos.Checked = false;
            ddlMarca.SelectedIndex = 0;
            ddlCategoria.SelectedIndex = 0;
            ddlOrdenarPor.SelectedIndex = 0;
            pnlFiltroAvanzado.Visible = false;
            gvArticulos.PageIndex = 0;
            CargarArticulos();
            MostrarBotonRestablecer();          
            btnLimpiarFiltros.Visible = false;          
        }

        protected void gvArticulos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int id = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ID"));
                decimal precio = TraerPrecio(id);
                int stock = TraerStock(id);
                e.Row.Cells[7].Text = precio.ToString("C");
                e.Row.Cells[8].Text = stock.ToString();
            }
        }

        protected void gvArticulos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvArticulos.PageIndex = e.NewPageIndex;
                List<Articulo> listaArticulos = (List<Articulo>)Session["ListaArticulosFiltrada"];

                gvArticulos.DataSource = listaArticulos;
                gvArticulos.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
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
            txtFiltro.Enabled = true ;
            if (!chkAvanzado.Checked)
            {
                ddlMarca.SelectedIndex = 0;
                ddlCategoria.SelectedIndex = 0;
                ddlOrdenarPor.SelectedIndex = 0;
            }
            CargarArticulos();
            MostrarBotonRestablecer();
        }

        private decimal TraerPrecio(long idArt)
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                Articulo articulo = negocio.ObtenerArticuloPorID(idArt);

                if (articulo != null)
                {
                    long idArticulo = articulo.ID;
                    DatoArticuloNegocio datoNegocio = new DatoArticuloNegocio();
                    decimal precio = datoNegocio.ObtenerPrecioArticulo(idArticulo);

                    return precio;
                }
                else
                {
                    Session.Add("Error", "No se encontró el artículo.");
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
                return -1;
            }
        }

        private int TraerStock(long id)
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                Articulo articulo = negocio.ObtenerArticuloPorID(id);

                if (articulo != null)
                {
                    long idArticulo = articulo.ID;
                    DatoArticuloNegocio datoNegocio = new DatoArticuloNegocio();
                    int stock = datoNegocio.ObtenerStockArticulo(idArticulo);

                    return stock;
                }
                else
                {
                    Session.Add("Error", "No se encontró el artículo.");
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
                return -1;
            }

        }

        protected bool ValidarSesion()
        {
            if (Session["Usuario"] != null && ((Usuario)Session["Usuario"]).TipoUsuario == true)
            {
                return true;
            }
            return false;
        }

        
        
    }

}
