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
            CargarCategorias();
        }

        private void CargarCategorias()
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            List<Categoria> categorias = categoriaNegocio.Listar();       
            Session["listaCategorias"] = categorias;
            gvCategorias.DataSource = categorias;
            gvCategorias.DataBind();
        }
        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Categoria> lista = (List<Categoria>)Session["listaCategorias"];
            string filtro = txtFiltro.Text.Trim().ToUpper();
            bool incluirInactivos = chkIncluirInactivos.Checked;

            List<Categoria> listaFiltrada;

            if (string.IsNullOrEmpty(filtro))
            {
                if (incluirInactivos)
                {
                    listaFiltrada = lista;
                }
                else
                {
                    listaFiltrada = lista.Where(x => x.Activo).ToList();
                }
            }
            else
            {
                listaFiltrada = lista.Where(x =>
                    x.Descripcion.ToUpper().Contains(filtro) &&
                    (incluirInactivos || x.Activo)
                ).ToList();
            }
            gvCategorias.DataSource = listaFiltrada;
            gvCategorias.DataBind();
        }
        protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            txtFiltro.Text = string.Empty;
            chkIncluirInactivos.Checked = false;
            CategoriaNegocio negocio = new CategoriaNegocio();
            List<Categoria> listaArticulos = negocio.Listar();
            gvCategorias.DataSource = listaArticulos;
            gvCategorias.DataBind();
        }

        protected void gvCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = gvCategorias.SelectedDataKey.Value.ToString();
            Response.Redirect("AltaCategoria.aspx?ID=" + id);
        }
    }
}