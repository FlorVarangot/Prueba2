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
            catch (Exception)
            {
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
            catch (Exception)
            {
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

            List<Cliente> listaFiltrada;

            if (lista != null)
            {
                if (string.IsNullOrEmpty(filtro))
                {
                    listaFiltrada = incluirInactivos ? lista : lista.Where(x => x.Activo).ToList();
                }
                else
                {
                    listaFiltrada = lista.Where(x =>
                x.Nombre.ToUpper().Contains(filtro) ||
                x.Apellido.ToUpper().Contains(filtro) ||
                x.Dni.ToUpper().Contains(filtro) ||
                x.Telefono.ToUpper().Contains(filtro) ||
                x.Email.ToUpper().Contains(filtro) ||
                x.Direccion.ToUpper().Contains(filtro) ||              
                (x.Activo && incluirInactivos)).ToList();
                }

                gvClientes.DataSource = listaFiltrada;
            }
            else
            {
                gvClientes.DataSource = lista;
            }
            gvClientes.DataBind();
        }

        protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            txtFiltro.Text = string.Empty;
            chkIncluirInactivos.Checked = false;
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
    }
}