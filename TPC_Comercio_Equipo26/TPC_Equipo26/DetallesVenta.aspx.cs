using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPC_Equipo26.Dominio;
using TPC_Equipo26.Negocio;

namespace TPC_Equipo26
{
    public partial class DetallesVenta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ValidarSesionActiva())
            {
                try
                {
                    if (!IsPostBack)
                    {
                        if (Request.QueryString["ID"] != null)
                        {
                            long ventaID = Convert.ToInt64(Request.QueryString["ID"]);
                            lblVentaID.Text = "Venta n° 00" + ventaID;
                            lblCliente.Text = "Cliente: " + TraerNombreCliente(ventaID);
                            lblFecha.Text = "Fecha: " + TraerFechaVenta(ventaID).ToString("dd/MM/yyyy");
                            lblTotal.Text = "Total: $" + TraerTotalVenta(ventaID).ToString("N2");
                            CargarDetallesVenta(ventaID);

                            lnkVentaAnterior.NavigateUrl = ObtenerVentaAnterior();
                            lnkVentaSiguiente.NavigateUrl = ObtenerVentaSiguiente();
                            lnkVentaAnterior.Enabled = (ventaID > 1);
                            long maxID = TraerUltimoId();
                            lnkVentaSiguiente.Enabled = (ventaID < maxID);
                        }
                        else
                        {
                            Response.Redirect("Ventas.aspx", false);
                        }
                    }
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

        private void CargarDetallesVenta(long id)
        {
            try
            {
                DetalleVentaNegocio negocio = new DetalleVentaNegocio();
                List<DetalleVenta> detalles = negocio.Listar(id);

                gvDetalle.DataSource = detalles;
                gvDetalle.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void gvDetalle_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDetalle.PageIndex = e.NewPageIndex;
        }

        protected void gvDetalle_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = gvDetalle.SelectedDataKey.Value.ToString();
            Response.Redirect("DetallesVenta.aspx?ID=" + id);
        }

        protected void gvDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DatoArticuloNegocio datoArticuloNegocio = new DatoArticuloNegocio();
                ArticuloNegocio negocio = new ArticuloNegocio();
                long idArt = Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "IdArticulo"));
                Articulo articulo = negocio.ObtenerArticuloPorID(idArt);
                string nombreArticulo = articulo.Descripcion;
                long idVenta = long.Parse(Request.QueryString["ID"]);
                DateTime fechaVenta = TraerFechaVenta(idVenta);
                int cantidad = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Cantidad"));

                decimal precioUnitario = datoArticuloNegocio.ObtenerPrecioHistorico(idArt, fechaVenta);
                decimal importeParcial = cantidad * precioUnitario;

                e.Row.Cells[1].Text = nombreArticulo;
                e.Row.Cells[2].Text = precioUnitario.ToString("C2");
                e.Row.Cells[3].Text = importeParcial.ToString("C2");
            }
        }

        private string TraerNombreCliente(long id)
        {
            try
            {
                VentaNegocio ventaNegocio = new VentaNegocio();
                Venta venta = ventaNegocio.ObtenerVentaPorId(id);
                string apellidoNombre = "cliente";

                if (venta != null)
                {
                    long idCliente = venta.IdCliente;
                    ClienteNegocio clienteNegocio = new ClienteNegocio();
                    Cliente cliente = clienteNegocio.ObtenerClientePorId(idCliente);

                    if (cliente != null)
                    {
                        apellidoNombre = $"{cliente.Apellido}, {cliente.Nombre}";
                        return apellidoNombre;
                    }
                }
                return apellidoNombre;
            }
            catch (Exception)
            {
                Session.Add("Error", "Error al obtener el nombre del cliente");
                return null;
            }
        }

        private DateTime TraerFechaVenta(long id)
        {
            try
            {
                VentaNegocio ventaNegocio = new VentaNegocio();
                Venta venta = ventaNegocio.ObtenerVentaPorId(id);
                DateTime fechaVenta;

                if (venta != null)
                    fechaVenta = venta.FechaVenta;
                else
                    fechaVenta = new DateTime(1900, 1, 1);

                return fechaVenta;
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
                return DateTime.MinValue;
            }
        }

        private decimal TraerTotalVenta(long id)
        {
            try
            {
                VentaNegocio ventaNegocio = new VentaNegocio();
                Venta venta = ventaNegocio.ObtenerVentaPorId(id);
                decimal totalVenta;

                if (venta != null)
                    totalVenta = venta.Total;
                else
                    totalVenta = 0;

                return totalVenta;
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
                return 0;
            }
        }

        private long TraerUltimoId()
        {
            VentaNegocio ventaNegocio = new VentaNegocio();
            return ventaNegocio.TraerUltimoId();
        }

        private string ObtenerVentaSiguiente()
        {
            long ventaID = Convert.ToInt64(Request.QueryString["ID"]);
            long nuevoID = ventaID + 1;

            if (nuevoID <= TraerUltimoId())
            {
                return "DetallesVenta.aspx?ID=" + nuevoID;
            }
            else
            {
                return "Ventas.aspx";
            }
        }

        private string ObtenerVentaAnterior()
        {
            long ventaID = Convert.ToInt64(Request.QueryString["ID"]);
            long nuevoID = ventaID - 1;

            if (nuevoID > 0)
            {
                return "DetallesVenta.aspx?ID=" + nuevoID;
            }
            else
            {
                return "Ventas.aspx";
            }
        }

        protected bool ValidarSesionActiva()
        {
            if (Seguridad.sesionActiva(Session["Usuario"]))
                return true;
            return false;
        }
    }
}