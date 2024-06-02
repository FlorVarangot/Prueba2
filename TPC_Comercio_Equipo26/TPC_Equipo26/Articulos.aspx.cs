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
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            List<Articulo> articulos = new List<Articulo>();

            if (!IsPostBack)
            {
                articulos = articuloNegocio.ListarArticulosConImagenes();
                gvArticulos.DataSource = articulos;
                gvArticulos.DataBind();
             }

        }
        protected void btnAgregarArticulo_Click(object sender, EventArgs e)
        {   
            Response.Redirect("AgregarArticulo.aspx");
        }

    }
}