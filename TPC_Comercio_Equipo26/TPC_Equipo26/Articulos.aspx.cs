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
            if (!IsPostBack)
            {
                ArticuloNegocio articuloNegocio = new ArticuloNegocio();
                Session.Add("ListaArticulos", articuloNegocio.ListarConImagenes());
                gvArticulos.DataSource = Session["ListaArticulos"];
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

        protected void filtro_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> lista = (List<Articulo>)Session["ListaArticulos"];
            List<Articulo> listaFiltrada = lista.FindAll(x => x.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()));
            gvArticulos.DataSource = listaFiltrada;
            gvArticulos.DataBind();
        }
    }

}
