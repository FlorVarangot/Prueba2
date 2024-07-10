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
                Response.Redirect("Compras.aspx");
                return null;
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
                Response.Redirect("Compras.aspx");
                return null;
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