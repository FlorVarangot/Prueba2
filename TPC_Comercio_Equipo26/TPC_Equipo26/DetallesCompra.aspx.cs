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
    }
}