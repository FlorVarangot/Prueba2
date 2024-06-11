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
        public bool FiltroAvanzado { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            FiltroAvanzado = chkAvanzado.Checked;
            if (!IsPostBack)
            {
                ArticuloNegocio articuloNegocio = new ArticuloNegocio();
                Session.Add("ListaArticulos", articuloNegocio.Listar());
                gvArticulos.DataSource = Session["ListaArticulos"];
                gvArticulos.DataBind();

                MarcaNegocio marcaNegocio = new MarcaNegocio();
                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                List<Marca> marcas = marcaNegocio.Listar();
                List<Categoria> categorias = categoriaNegocio.Listar();
                pnlFiltroAvanzado.Visible = chkAvanzado.Checked;

                /*ddlMarca.DataSource = marcas;
                ddlMarca.DataTextField = "Descripcion";
                ddlMarca.DataValueField = "ID";
                ddlMarca.DataBind();
                ddlMarca.Items.Insert(0, new ListItem("Seleccionar Marca", "0"));

                ddlCategoria.DataSource = categorias;
                ddlCategoria.DataTextField = "Descripcion";
                ddlCategoria.DataValueField = "ID";
                ddlCategoria.DataBind();
                ddlCategoria.Items.Insert(0, new ListItem("Seleccionar Categoría", "0"));*/

            }
        }


        protected void Filtro_TextChanged(object sender, EventArgs e)
        {
            //aca filtro rapido
            List<Articulo> lista = (List<Articulo>)Session["listaArticulos"];
            List<Articulo> listaFiltrada = lista.FindAll(x => x.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()));
            gvArticulos.DataSource = listaFiltrada;
            gvArticulos.DataBind();
            //lo de abajo comentado lo hizo flor
            //FiltrarArticulos();
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
                    // (ddlMarca.SelectedValue == "0" || x.Marca.Descripcion.Equals(ddlMarca.SelectedItem.Text)) &&
                    //(ddlCategoria.SelectedValue == "0" || x.Categoria.Descripcion.Equals(ddlCategoria.SelectedItem.Text)) &&
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
            chkIncluirInactivos.Checked = false;
            //ddlMarca.SelectedIndex = 0;
            //ddlCategoria.SelectedIndex = 0;
            chkIncluirInactivos.Checked = false;

            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            List<Articulo> listaArticulos = articuloNegocio.Listar();
            gvArticulos.DataSource = listaArticulos;
            gvArticulos.DataBind();
        }

        protected void gvArticulos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvArticulos.PageIndex = e.NewPageIndex;

            gvArticulos.DataSource = Session["ListaArticulos"];
            gvArticulos.DataBind();
        }

        protected void gvArticulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = gvArticulos.SelectedDataKey.Value.ToString();
            Response.Redirect("AltaArticulos.aspx?ID=" + id);
        }

        protected void chkAvanzado_CheckedChanged(object sender, EventArgs e)
        {
            pnlFiltroAvanzado.Visible = chkAvanzado.Checked;
            txtFiltro.Enabled = !chkAvanzado.Checked;
        }

        protected void ddlCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCriterio.Items.Clear();

            string selectedItemText = ddlCampo.SelectedItem.Text;

            if (selectedItemText == "Precio Unitario ($)")
            {
                ddlCriterio.Items.Add("Igual a");
                ddlCriterio.Items.Add("Mayor a");
                ddlCriterio.Items.Add("Menor a");
            }
            else
            {
                ddlCriterio.Items.Add("Comienza con");
                ddlCriterio.Items.Add("Contiene");               
                ddlCriterio.Items.Add("Termina con");
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                gvArticulos.DataSource = negocio.Filtrar(
                    ddlCampo.SelectedItem.ToString(),
                    ddlCriterio.SelectedItem.ToString(),
                   txtFiltroAvanzado.Text,
            chkIncluirInactivos.Checked);
                gvArticulos.DataBind();
            }
            catch (Exception )
            {
                Response.Redirect("Error.aspx");
            }
        }
    }

}
