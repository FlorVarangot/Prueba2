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
    public partial class DetallesCompra : System.Web.UI.Page
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
                            long idCompra = Convert.ToInt64(Request.QueryString["ID"]);
                            lblCompraID.Text = "Compra n° 00" + idCompra;
                            lblProveedor.Text = "Proveedor: " + TraerProveedor(idCompra);
                            lblFecha.Text = "Fecha: " + TraerFechaCompra(idCompra).ToString("dd/MM/yyyy");

                            CargarDetallesCompra(idCompra);

                            lnkVentaAnterior.NavigateUrl = ObtenerCompraAnterior();
                            lnkVentaSiguiente.NavigateUrl = ObtenerCompraSiguiente();
                            lnkVentaAnterior.Enabled = (idCompra > 1);
                            long maxID = TraerUltimoId();
                            lnkVentaSiguiente.Enabled = (idCompra < maxID);
                        }
                        else
                        {
                            Response.Redirect("Compras.aspx");
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

        private DateTime TraerFechaCompra(long idCompra)
        {
            try
            {
                CompraNegocio compraNegocio = new CompraNegocio();
                Compra compra = compraNegocio.ObtenerCompraPorId(idCompra);
                DateTime fechaCompra;

                if (compra != null)
                    fechaCompra = compra.FechaCompra;
                else
                    fechaCompra = new DateTime(1900, 1, 1);

                return fechaCompra;
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
                return DateTime.MinValue;
            }
        }

        private string TraerProveedor(long idCompra)
        {
            try
            {
                CompraNegocio negocio = new CompraNegocio();
                Compra compra = negocio.ObtenerCompraPorId(idCompra);
                string nombreProveedor = "proveedor";

                if (compra != null)
                {
                    int idProv = compra.IdProveedor;
                    ProveedorNegocio provNegocio = new ProveedorNegocio();
                    Proveedor proveedor = provNegocio.ObtenerProveedorPorId(idProv);

                    if (proveedor != null)
                    {
                        nombreProveedor = $"{proveedor.Nombre}";
                        return nombreProveedor;
                    }
                }
                return nombreProveedor;
            }
            catch (Exception)
            {
                Session.Add("Error", "Error al obtener el nombre del proveedor");
                return null;
            }
        }

        private void CargarDetallesCompra(long idCompra)
        {
            try
            {
                DetalleCompraNegocio negocio = new DetalleCompraNegocio();
                List<DetalleCompra> detalles = negocio.ListarDetalleCompra(idCompra);

                ArticuloNegocio negocioArticulo = new ArticuloNegocio();
                foreach (var detalle in detalles)
                {
                    Articulo articulo = negocioArticulo.ObtenerArticuloPorID(detalle.IdArticulo);
                    if (articulo != null)
                    {
                        detalle.NombreArticulo = articulo.Descripcion;
                    }
                    else
                    {
                        detalle.NombreArticulo = "Artículo no encontrado";
                    }
                }
                gvDetalle.DataSource = detalles;
                gvDetalle.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        private long TraerUltimoId()
        {
            CompraNegocio negocio = new CompraNegocio();
            return negocio.TraerUltimoId();

        }

        private string ObtenerCompraSiguiente()
        {
            long compraID = Convert.ToInt64(Request.QueryString["ID"]);
            long nuevoID = compraID + 1;

            if (nuevoID <= TraerUltimoId())
            {
                return "DetallesCompra.aspx?ID=" + nuevoID;
            }
            else
            {
                return "Compras.aspx";
            }
        }

        private string ObtenerCompraAnterior()
        {
            long compraID = Convert.ToInt64(Request.QueryString["ID"]);
            long nuevoID = compraID - 1;

            if (nuevoID > 0)
            {
                return "DetallesCompra.aspx?ID=" + nuevoID;
            }
            else
            {
                return "Compras.aspx";

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