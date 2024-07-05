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
            if (!IsPostBack)
            {
                if (Request.QueryString["ID"] != null)
                {
                    long idCompra = Convert.ToInt64(Request.QueryString["ID"]);
                    CargarDetallesCompra(idCompra);

                    //navegacion entre detalles:
                    lnkVentaAnterior.NavigateUrl = ObtenerCompraAnterior();
                    lnkVentaSiguiente.NavigateUrl = ObtenerCompraSiguiente();
                    //si es la primera no puedo ir a anterior
                    lnkVentaAnterior.Enabled = (idCompra > 1);
                    long maxID = TraerUltimoId();
                    //si es la ultima no puedo ir a siguiente
                    lnkVentaSiguiente.Enabled = (idCompra < maxID);
                }
                else
                {
                    Response.Redirect("Compras.aspx");
                }
            }
        }

        private void CargarDetallesCompra(long idCompra)
        {
            try
            {
                DetalleCompraNegocio negocio = new DetalleCompraNegocio();
                List<DetalleCompra> detalles = negocio.ListarDetalleCompra(idCompra);

               
                gvDetalle.DataSource = detalles;
                gvDetalle.DataBind();
            }
            catch (Exception)
            {             
                Response.Redirect("Error.aspx"); 
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

            //return "Compras.aspx";
            return "Error.aspx";
        }

        private string ObtenerCompraAnterior()
        {
            long compraID = Convert.ToInt64(Request.QueryString["ID"]);
            long nuevoID = compraID - 1;

            if (nuevoID > 0)
            {
                return "DetallesCompra.aspx?ID=" + nuevoID;
            }

            //return "Compras.aspx";
            return "Error.aspx";
        }

    }
}