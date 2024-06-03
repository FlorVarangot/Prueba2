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

            if (!IsPostBack)
            {

                List<Articulo> articulos = articuloNegocio.ListarArticulosConImagenes();
                gvArticulos.DataSource = articulos;
                gvArticulos.DataBind();
            }

        }
        protected void BtnAgregarArticulo_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("AltaArticulo.aspx");
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx");
            }
        }


    }

}
