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
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProveedores();
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
            catch (Exception)
            {
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void gvCompras_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvCompras_PageIndexChanged(object sender, EventArgs e)
        {        
        }
    }
}