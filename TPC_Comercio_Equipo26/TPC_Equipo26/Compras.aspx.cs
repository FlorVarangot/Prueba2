using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPC_Equipo26.Dominio;
using TPC_Equipo26.Negocio;

namespace TPC_Equipo26
{
    public partial class Compras : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ValidarSesionActiva())
            {
                try
                {
                    if (!IsPostBack)
                    {
                        CargarCompras();
                        CargarProveedores();
                    }
                    VerificarMostrarFiltros();
                }
                catch (Exception ex)
                {
                    Session.Add("Error", ex.ToString());
                    Response.Redirect("Error.aspx", false);
                }
            }
            else
            {
                Session.Add("Error", "No tenes permisos para ingresar a esta pantalla.");
                Response.Redirect("Error.aspx", false);
            }
        }

        private void CargarProveedores()
        {
            try
            {
                ProveedorNegocio negocio = new ProveedorNegocio();
                List<Proveedor> proveedores = negocio.Listar();

                ddlProveedor.DataSource = proveedores;
                ddlProveedor.DataTextField = "Nombre";
                ddlProveedor.DataValueField = "ID";
                ddlProveedor.DataBind();
                ddlProveedor.Items.Insert(0, new ListItem("Seleccione Proveedor", ""));
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        private void CargarCompras()
        {
            try
            {
                CompraNegocio compraNegocio = new CompraNegocio();
                List<Compra> listaCompras = compraNegocio.Listar();
                Session["ListaCompras"] = listaCompras;
                FiltrarCompras();
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        private void FiltrarCompras()
        {
            List<Compra> listaCompras = (List<Compra>)Session["ListaCompras"];
            string filtro = txtFiltro.Text.Trim();
            if (!string.IsNullOrEmpty(filtro))
            {
                listaCompras = listaCompras.Where(x => x.ID.ToString().Contains(filtro) || x.FechaCompra.ToString("dd/MM/yyyy").Contains(filtro)).ToList();
            }
            if (ddlProveedor.SelectedIndex > 0)
            {
                int proveedorSeleccionado = int.Parse(ddlProveedor.SelectedValue);
                listaCompras = listaCompras.Where(x => x.IdProveedor == proveedorSeleccionado).ToList();
            }
            string ordenSeleccionado = ddlOrdenarPor.SelectedValue;
            switch (ordenSeleccionado)
            {
                case "MayorPrecio":
                    listaCompras = listaCompras.OrderByDescending(x => x.TotalCompra).ToList();
                    break;
                case "MenorPrecio":
                    listaCompras = listaCompras.OrderBy(x => x.TotalCompra).ToList();
                    break;
                case "FechaReciente":
                    listaCompras = listaCompras.OrderByDescending(x => x.FechaCompra).ToList();
                    break;
                case "FechaAntigua":
                    listaCompras = listaCompras.OrderBy(x => x.FechaCompra).ToList();
                    break;
                case "CompraAsc":
                    listaCompras = listaCompras.OrderBy(x => x.ID).ToList();
                    break;
                case "CompraDesc":
                    listaCompras = listaCompras.OrderByDescending(x => x.ID).ToList();
                    break;
                default:
                    listaCompras = listaCompras.OrderByDescending(x => x.ID).ToList();
                    break;
            }

            Session["ListaFiltrada"] = listaCompras;

            gvCompras.DataSource = listaCompras;
            gvCompras.DataBind();
        }

        protected void gvCompras_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvCompras.PageIndex = e.NewPageIndex;
                List<Compra> compras = (List<Compra>)Session["ListaFiltrada"];

                gvCompras.DataSource = compras;
                gvCompras.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void gvCompras_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Compra compra = (Compra)e.Row.DataItem;
                int idProveedor = compra.IdProveedor;
                string proveedor = TraerNombreProveedor(idProveedor);
                e.Row.Cells[2].Text = proveedor;
            }
        }

        private string TraerNombreProveedor(int idProveedor)
        {
            try
            {
                ProveedorNegocio proveedorNegocio = new ProveedorNegocio();
                Proveedor proveedor = proveedorNegocio.ObtenerProveedorPorId(idProveedor);

                if (proveedor != null)
                {
                    return proveedor.Nombre;
                }
                return "Proveedor no encontrado";
            }
            catch (Exception)
            {
                Session.Add("Error", "Error al obtener el nombre del proveedor");
                Response.Redirect("Error.aspx", false);
                return null;
            }
        }

        protected void btnRestablecer_Click(object sender, EventArgs e)
        {
            try
            {
                ddlProveedor.SelectedIndex = 0;
                txtFiltro.Text = string.Empty;
                ddlOrdenarPor.SelectedIndex = 0;
               gvCompras.PageIndex = 0;
                CargarCompras();
                btnRestablecer.Visible = false;

                FiltrarCompras();
                MostrarBotonRestablecer();
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        private void MostrarBotonRestablecer()
        {
            bool filtroActivo = !string.IsNullOrEmpty(txtFiltro.Text.Trim()) ||
                                ddlProveedor.SelectedIndex > 0 ||
                                !string.IsNullOrEmpty(ddlOrdenarPor.SelectedValue);

            btnRestablecer.Visible = filtroActivo;
        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            FiltrarCompras();
            MostrarBotonRestablecer();
        }

        protected void ddlProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarCompras();
            MostrarBotonRestablecer();
        }

        protected void ddlOrdenarPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarCompras();
            MostrarBotonRestablecer();
        }

        private void VerificarMostrarFiltros()
        {
            bool mostrarFiltros = gvCompras.Rows.Count > 0;
            contenedorFiltros.Visible = mostrarFiltros;
        }

        protected bool ValidarSesionActiva()
        {
            if (Seguridad.sesionActiva(Session["Usuario"]))
                return true;
            return false;
        }

    }
}