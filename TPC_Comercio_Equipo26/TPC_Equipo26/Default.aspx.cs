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
                    btnLimpiarFiltros.Visible = false;
                }
            }
            catch
            { 
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
            catch (Exception)
            {
                Response.Redirect("Error.aspx", false);
            }
        }

        private void CargarMarcasYCategorias()
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
                    listaArticulos = listaArticulos.OrderByDescending(x => datoNegocio.ObtenerStockArticulo(x.ID)).ToList();
                    break;
                case "StockDisponibleDesc":
                    listaArticulos = listaArticulos.OrderBy(x => datoNegocio.ObtenerStockArticulo(x.ID)).ToList();
                    break;
                case "PrecioUnitarioAsc":
                    listaArticulos = listaArticulos.OrderByDescending(x => datoNegocio.ObtenerPrecioArticulo(x.ID)).ToList();
                    break;
                case "PrecioUnitarioDesc":
                    listaArticulos = listaArticulos.OrderBy(x => datoNegocio.ObtenerPrecioArticulo(x.ID)).ToList();
                    break;
                default:
                    listaArticulos = listaArticulos.OrderBy(x => x.ID).ToList();
                    break;
            }

            Session["ListaArticulosFiltrada"] = listaArticulos;

            gvArticulos.DataSource = listaArticulos;
            gvArticulos.DataBind();
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
            FiltrarArticulos();
            MostrarBotonRestablecer();
        }

        protected void chkIncluirInactivos_CheckedChanged(object sender, EventArgs e)
        {
            FiltrarArticulos();
            MostrarBotonRestablecer();
        }

        protected void ddlMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarArticulos();
            MostrarBotonRestablecer();
        }

        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarArticulos();
            MostrarBotonRestablecer();
        }
        
        protected void ddlOrdenarPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarArticulos();
            MostrarBotonRestablecer();
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
                ddlOrdenarPor.SelectedIndex = 0;
                pnlFiltroAvanzado.Visible = false;
                CargarArticulos();
                MostrarBotonRestablecer();
                btnLimpiarFiltros.Visible = false;
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void gvArticulos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int id = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ID"));
                decimal precio = TraerPrecioConGanancia(id);
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
                ddlOrdenarPor.SelectedIndex = 0;
            }
            CargarArticulos();
            MostrarBotonRestablecer();
        }

        //Revisar si hace falta este metodo:
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
                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        private decimal TraerPrecioConGanancia(long idArt)
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                Articulo articulo = negocio.ObtenerArticuloPorID(idArt);

                if (articulo != null)
                {
                    long idArticulo = articulo.ID;
                    decimal ganancia = articulo.Ganancia;
                    DatoArticuloNegocio datoNegocio = new DatoArticuloNegocio();
                    decimal precio = datoNegocio.ObtenerPrecioArticulo(idArticulo);
                    decimal precioConGanancia = precio + (precio * ganancia / 100);

                    return precioConGanancia;
                }
                return 0;
            }
            catch (Exception)
            {
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
                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }

       
    }

}
