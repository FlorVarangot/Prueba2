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
    public partial class DetallesVenta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["ID"] != null)
                {

                    long ventaID = Convert.ToInt64(Request.QueryString["ID"]);
                    string nombreCliente = TraerNombreCliente(ventaID);
                    decimal totalVenta = TraerTotalVenta(ventaID);
                    lblCliente.Text = "Cliente: " + nombreCliente;
                    lblVentaID.Text = "Venta n° 00" + ventaID;
                    lblTotal.Text = "Total: $" + totalVenta;
                    CargarDetallesVenta(ventaID);
                }
                else
                {
                    Response.Redirect("Ventas.aspx");
                }

            }
        }

        private void CargarDetallesVenta(long id)
        {
            DetalleVentaNegocio negocio = new DetalleVentaNegocio();
            List<DetalleVenta> detalles = negocio.Listar(id);

            gvDetalle.DataSource = detalles;
            gvDetalle.DataBind();
        }

        protected void gvDetalle_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDetalle.PageIndex = e.NewPageIndex;
        }

        protected void gvDetalle_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = gvDetalle.SelectedDataKey.Value.ToString();
            Response.Redirect("AltaVenta.aspx?ID=" + id);

        }

        protected void gvDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                long idArt = Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "IdArticulo"));
                ArticuloNegocio negocio = new ArticuloNegocio();
                Articulo articulo = negocio.ObtenerArticuloPorID(idArt);
                DatoArticuloNegocio datoArticuloNegocio = new DatoArticuloNegocio();
                string nombreArticulo = articulo.Descripcion;

                int cantidad = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Cantidad"));
                decimal precioUnitario = datoArticuloNegocio.ObtenerPrecioArticulo(Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "IdArticulo")));
                decimal totalParcial = cantidad * precioUnitario;

                e.Row.Cells[0].Text = nombreArticulo;
                e.Row.Cells[2].Text = totalParcial.ToString("C");
            }
        }


        //F: Revisar optimización de este metodo: se repite tal cual en Venta y en DetallesVenta.
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
                return "Error al obtener el nombre del cliente";
            }
        }

        private decimal TraerTotalVenta(long id)
        {
            decimal total = 0;
            return total;
        }

    }
}