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
    public partial class Clientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    CargarClientes();
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        private void CargarClientes()
        {
            try
            {
                chkIncluirInactivos.Checked = false;
                ClienteNegocio clienteNegocio = new ClienteNegocio();
                List<Cliente> clientes = clienteNegocio.Listar();
                Session["listaClientes"] = clientes;
                FiltrarClientes();
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            FiltrarClientes();
        }

        private void FiltrarClientes()
        {
            List<Cliente> lista = (List<Cliente>)Session["listaClientes"];
            string filtro = txtFiltro.Text.Trim().ToUpper();
            bool incluirInactivos = chkIncluirInactivos.Checked;

            if (!chkIncluirInactivos.Checked)
            {
                lista = lista.Where(x => x.Activo).ToList();
            }

            if (string.IsNullOrEmpty(filtro))
            { 
                lista = lista.Where(x => x.ID.ToString().Contains(filtro) || x.Nombre.ToUpper().Contains(filtro) || x.Apellido.ToUpper().Contains(filtro) || x.Dni.ToString().Contains(filtro)
                    || x.Telefono.ToUpper().Contains(filtro) || x.Direccion.ToUpper().Contains(filtro) || x.Email.ToUpper().Contains(filtro)).ToList();
            }

            string ordenSeleccionado = ddlOrdenarPor.SelectedValue;
            switch (ordenSeleccionado)
            {
                case "ApellidoAZ":
                    lista = lista.OrderBy(x => x.Apellido).ToList();
                    break;
                case "ApellidoZA":
                    lista = lista.OrderByDescending(x => x.Apellido).ToList();
                    break;
                case "DniAsc":
                    lista = lista.OrderBy(x => x.Dni).ToList();
                    break;
                case "DniDesc":
                    lista = lista.OrderByDescending(x => x.Dni).ToList();
                    break;
                default:
                    lista = lista.OrderBy(x => x.ID).ToList();
                    break;
            }

            MostrarBotonLimpiar();
            Session["ListaFiltrada"] = lista;
            gvClientes.DataSource = lista;
            gvClientes.DataBind();

        }

        protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            txtFiltro.Text = string.Empty;
            chkIncluirInactivos.Checked = false;
            ddlOrdenarPor.SelectedIndex = 0;
            CargarClientes();
        }

        protected void gvClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = gvClientes.SelectedDataKey.Value.ToString();
            Response.Redirect($"AltaCliente.aspx?ID={id}");
        }

        protected void chkIncluirInactivos_CheckedChanged(object sender, EventArgs e)
        {
            FiltrarClientes();
        }

        protected void ddlOrdenarPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarClientes();
        }
        
        protected void MostrarBotonLimpiar()
        {
            bool filtroActivo = !string.IsNullOrEmpty(txtFiltro.Text.Trim()) || !string.IsNullOrEmpty(ddlOrdenarPor.SelectedValue) || chkIncluirInactivos.Checked;
            btnLimpiarFiltros.Visible = filtroActivo;
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