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

                MarcaNegocio marcaNegocio = new MarcaNegocio();
                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                List<Marca> marcas = marcaNegocio.Listar();
                List<Categoria> categorias = categoriaNegocio.Listar();

                ddlMarca.DataSource = marcas;
                ddlMarca.DataTextField = "Descripcion";
                ddlMarca.DataValueField = "ID";
                ddlMarca.DataBind();
                ddlMarca.Items.Insert(0, new ListItem("Seleccionar Marca", "0"));

                ddlCategoria.DataSource = categorias;
                ddlCategoria.DataTextField = "Descripcion";
                ddlCategoria.DataValueField = "ID";
                ddlCategoria.DataBind();
                ddlCategoria.Items.Insert(0, new ListItem("Seleccionar Categoría", "0"));

            }
        }

        protected void Filtro_TextChanged(object sender, EventArgs e)
        {
            FiltrarArticulos();
        }

        protected void FiltroMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarArticulos();
        }

        protected void FiltroCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarArticulos();
        }

        protected void FiltroInactivos_CheckedChanged(object sender, EventArgs e)
        {
            FiltrarArticulos();
        }

        private void FiltrarArticulos()
        {
            List<Articulo> lista = (List<Articulo>)Session["ListaArticulos"];
            if (lista != null)
            {
                List<Articulo> listaFiltrada = lista.FindAll(x =>
                    (x.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()) ||
                    x.Codigo.ToUpper().Contains(txtFiltro.Text.ToUpper()) ||
                    x.Descripcion.ToUpper().Contains(txtFiltro.Text.ToUpper()) ||
                    x.ID.ToString().Contains(txtFiltro.Text.ToUpper())) &&
                    (ddlMarca.SelectedValue == "0" || x.Marca.Descripcion.Equals(ddlMarca.SelectedItem.Text)) &&
                    (ddlCategoria.SelectedValue == "0" || x.Categoria.Descripcion.Equals(ddlCategoria.SelectedItem.Text)) &&
                    (x.Activo || chkIncluirInactivos.Checked));
                gvArticulos.DataSource = listaFiltrada;
            }
            else
            {
                gvArticulos.DataSource = null;
            }
            gvArticulos.DataBind();
        }


        protected void BtnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            txtFiltro.Text = string.Empty;
            ddlMarca.SelectedIndex = 0;
            ddlCategoria.SelectedIndex = 0;
            chkIncluirInactivos.Checked = false;

            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            List<Articulo> listaArticulos = articuloNegocio.ListarConImagenes();
            gvArticulos.DataSource = listaArticulos;
            gvArticulos.DataBind();
        }

    }

}
