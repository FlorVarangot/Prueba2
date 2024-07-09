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
    public partial class Categorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    CargarCategorias();
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        private void CargarCategorias()
        {
            try
            {
                chkIncluirInactivos.Checked = false;
                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                List<Categoria> categorias = categoriaNegocio.Listar();
                Session["listaCategorias"] = categorias;
                FiltrarCategorias();
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            FiltrarCategorias();
        }
       
        private void FiltrarCategorias()
        {
            List<Categoria> lista = (List<Categoria>)Session["listaCategorias"];
            string filtro = txtFiltro.Text.Trim().ToUpper();
            bool incluirInactivos = chkIncluirInactivos.Checked;

            if (!string.IsNullOrEmpty(filtro))
            {
                lista = lista.Where(x => x.ID.ToString().Contains(filtro) || x.Descripcion.Contains(filtro)).ToList();
            }
            if (!incluirInactivos)
            {
                lista = lista.Where(x => x.Activo == true).ToList();
            }

            string orden = ddlOrdenarPor.SelectedValue;
            switch (orden)
            {
                case "DescripcionAZ":
                    lista = lista.OrderBy(x => x.Descripcion).ToList();
                    break;
                case "DescripcionZA":
                    lista = lista.OrderByDescending(x => x.Descripcion).ToList();
                    break;
                case "IdAsc":
                    lista = lista.OrderBy(x => x.ID).ToList();
                    break;
                case "IdDesc":
                    lista = lista.OrderByDescending(x => x.ID).ToList();
                    break;
                default:
                    lista = lista.OrderBy(x => x.ID).ToList();
                    break;
            }

            MostrarBotonReestablecer();

            Session["ListaFiltrada"] = lista;
            gvCategorias.DataSource = lista;
            gvCategorias.DataBind();
        }
        
        protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            txtFiltro.Text = string.Empty;
            chkIncluirInactivos.Checked = false;
            ddlOrdenarPor.SelectedIndex = -1;
            CargarCategorias();
        }

        protected void gvCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = gvCategorias.SelectedDataKey.Value.ToString();
            Response.Redirect("AltaCategoria.aspx?ID=" + id);
        }

        protected void chkIncluirInactivos_CheckedChanged(object sender, EventArgs e)
        {
            FiltrarCategorias();
        }

        protected void gvCategorias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvCategorias.PageIndex = e.NewPageIndex;
                List<Categoria> categorias = (List<Categoria>)Session["listaFiltrada"];

                gvCategorias.DataSource = categorias;
                gvCategorias.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void ddlOrdenarPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarCategorias();
        }

        private void MostrarBotonReestablecer()
        {
            btnLimpiarFiltros.Visible = !string.IsNullOrEmpty(txtFiltro.Text.Trim()) ||
                                        !string.IsNullOrEmpty(ddlOrdenarPor.SelectedValue);
        }

        protected bool ValidarSesion()
        {
            if (Session["Usuario"] != null && ((Usuario)Session["Usuario"]).TipoUsuario == TipoUsuario.ADMIN)
            {
                return true;
            }
            return false;
        }

    }
}