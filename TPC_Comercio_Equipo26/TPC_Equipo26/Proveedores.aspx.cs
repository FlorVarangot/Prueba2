using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPC_Equipo26.Dominio;
using TPC_Equipo26.Negocio;

namespace TPC_Equipo26
{
    public partial class Proveedores : System.Web.UI.Page
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
            ChkIncluirInactivos.Checked = false;
            ProveedorNegocio negocio = new ProveedorNegocio();
            List<Proveedor> proveedores = negocio.Listar();

            Session["listaProveedores"] = proveedores;
            FiltrarProveedores();
        }

        private void FiltrarProveedores()
        {
            List<Proveedor> lista = (List<Proveedor>)Session["listaProveedores"];
            if (!ChkIncluirInactivos.Checked)
            {
                lista = lista.Where(x => x.Activo).ToList();
            }
            if (!string.IsNullOrEmpty(TxtFiltro.Text.Trim()))
            {
                string filtro = TxtFiltro.Text.Trim().ToUpper();
                lista = lista.Where(x => x.Nombre.ToUpper().Contains(filtro) || x.CUIT.ToUpper().Contains(filtro) || x.Email.ToUpper().Contains(filtro) ||
                    x.Telefono.ToString().Contains(filtro) || x.Direccion.ToUpper().Contains(filtro)).ToList();
            }

            string ordenSeleccionado = DdlOrdenarPor.SelectedValue;
            switch (ordenSeleccionado)
            {
                case "NombreAZ":
                    lista = lista.OrderBy(x => x.Nombre).ToList();
                    break;
                case "NombreZA":
                    lista = lista.OrderByDescending(x => x.Nombre).ToList();
                    break;
                case "IdAsc":
                    lista = lista.OrderByDescending(x => x.ID).ToList();
                    break;
                case "IdDesc":
                    lista = lista.OrderBy(x => x.ID).ToList();
                    break;
                default:
                    lista = lista.OrderBy(x => x.ID).ToList();
                    break;
            }

            MostrarBotonLimpiar();
            Session["ProveedoresFiltrada"] = lista;
            GvProveedores.DataSource = lista;
            GvProveedores.DataBind();
        }
            
        protected void BtnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            LimpiarFiltros();
        }

        protected void GvProveedores_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvProveedores.PageIndex = e.NewPageIndex;
            GvProveedores.DataSource = Session["ListaProveedores"];
            GvProveedores.DataBind();
        }

        protected void GvProveedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = GvProveedores.SelectedDataKey.Value.ToString();
            Response.Redirect("AltaProveedor.aspx?ID=" + id);
        }

        protected void FiltroInactivos_CheckedChanged(object sender, EventArgs e)
        {
            FiltrarProveedores();
        }
       
        protected void TxtFiltro_TextChanged(object sender, EventArgs e)
        {
            FiltrarProveedores();
        }

        public void LimpiarFiltros()
        {
            TxtFiltro.Text = string.Empty;
            ChkIncluirInactivos.Checked = false;
            DdlOrdenarPor.SelectedIndex = -1;
            CargarProveedores();
        }

        protected void MostrarBotonLimpiar()
        {
            BtnLimpiarFiltros.Visible = !string.IsNullOrEmpty(TxtFiltro.Text.Trim()) || !string.IsNullOrEmpty(DdlOrdenarPor.SelectedValue) || ChkIncluirInactivos.Checked;
        }

        protected void ddlOrdenarPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarProveedores();
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