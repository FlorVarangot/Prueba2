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
            catch
            { Response.Redirect("Error.aspx", false); }
        }

        private void CargarCategorias()
        {
            try
            {
                chkIncluirInactivos.Checked = true;
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            List<Categoria> categorias = categoriaNegocio.Listar();
            Session["listaCategorias"] = categorias;
            gvCategorias.DataSource = categorias;
            gvCategorias.DataBind();
            }
            catch (Exception)
            {
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

            List<Categoria> listaFiltrada;

            if (lista != null)
            {
                if (string.IsNullOrEmpty(filtro))
                {
                    listaFiltrada = incluirInactivos ? lista : lista.Where(x => x.Activo).ToList();
                }
                else
                {
                    listaFiltrada = lista.Where(x =>
                        x.Descripcion.ToUpper().Contains(filtro) &&
                        (x.Activo || incluirInactivos)).ToList();
                }
                gvCategorias.DataSource = listaFiltrada;
            }
            else
            {
                gvCategorias.DataSource = lista;
            }
            gvCategorias.DataBind();
        }
        protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            txtFiltro.Text = string.Empty;
            chkIncluirInactivos.Checked = false;
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
    }
}