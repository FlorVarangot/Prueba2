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
            ValidarAdmin();
            try
            {
                if (!IsPostBack)
                {
                    CargarProveedores();
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        private void CargarProveedores()
        {
            try
            {
                ChkIncluirInactivos.Checked = false;
                ProveedorNegocio negocio = new ProveedorNegocio();
                List<Proveedor> proveedores = negocio.Listar();

                Session["listaProveedores"] = proveedores;
                FiltrarProveedores();
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
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
                    lista = lista.OrderBy(x => x.ID).ToList();
                    break;
                case "IdDesc":
                    lista = lista.OrderByDescending(x => x.ID).ToList();
                    break;
                default:
                    lista = lista.OrderBy(x => x.Nombre).ToList();
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
            GvProveedores.PageIndex = 0;
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

        protected void ValidarAdmin()
        {
            if (!Seguridad.esAdmin(Session["Usuario"]))
            {
                Session.Add("Error", "No tenes permisos para ingresar a esta pantalla.");
                Response.Redirect("Error.aspx", false);
            }
        }
        
        protected bool ValidarSesionActiva()
        {
            if (Seguridad.sesionActiva(Session["Usuario"]))
                return true;
            return false;
        }
   
        protected void GvProveedores_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GvProveedores.PageIndex = e.NewPageIndex;
                List<Proveedor> proveedor = (List<Proveedor>)Session["ProveedoresFiltrada"];

                GvProveedores.DataSource = proveedor;
                GvProveedores.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}