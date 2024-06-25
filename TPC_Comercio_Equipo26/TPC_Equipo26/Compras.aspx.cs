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
    public partial class Compras : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCompras();
            }
        }

        private void CargarCompras()
        {
            try
            {
                CompraNegocio negocio = new CompraNegocio();
                List<Compra> compras = negocio.Listar();

                gvCompras.DataSource = compras;
                gvCompras.DataBind();
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx");
            }
        }
       
    }
}