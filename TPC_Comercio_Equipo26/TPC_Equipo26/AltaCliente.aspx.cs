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
    public partial class AltaCliente : System.Web.UI.Page
    {
        public bool ConfirmarInactivar { get; set; }
        public bool ConfirmarReactivar { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ConfirmarInactivar = false;
                    ConfirmarReactivar = false;

                    if (Request.QueryString["ID"] != null)
                    {
                        long Id = long.Parse(Request.QueryString["ID"]);
                        CargarDatosCliente(Id);
                        lblTituloModificar.Visible = true;
                    }
                    else
                    {
                        lblTituloAgregar.Visible = true;
                        LimpiarCampos();
                        BtnInactivar.Visible = false;
                        btnReactivar.Visible = false;
                    }
                }
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx", false);
            }
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtDNI.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtDireccion.Text = string.Empty;
        }

        private void CargarDatosCliente(long Id)
        {
            ClienteNegocio negocio = new ClienteNegocio();
            Cliente cliente = negocio.ObtenerClientePorId(Id);

            if (cliente != null)
            {
                txtNombre.Text = cliente.Nombre;
                txtApellido.Text = cliente.Apellido;
                txtDNI.Text = cliente.Dni.ToString();
                txtTelefono.Text = cliente.Telefono;
                txtEmail.Text = cliente.Email;
                txtDireccion.Text = cliente.Direccion;

                if (cliente.Activo)
                {
                    BtnInactivar.Visible = true;
                    btnReactivar.Visible = false;
                }
                else
                {
                    BtnInactivar.Visible = false;
                    btnReactivar.Visible = true;
                }
            }
            else
            {
                Response.Redirect("Error.aspx");
            }
        }

        protected void BtnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Cliente cliente = new Cliente();
                ClienteNegocio negocio = new ClienteNegocio();

                cliente.Nombre = txtNombre.Text;
                cliente.Apellido = txtApellido.Text;
                cliente.Dni = int.Parse(txtDNI.Text);
                cliente.Telefono = txtTelefono.Text;
                cliente.Email = txtEmail.Text;
                cliente.Direccion = txtDireccion.Text;
                cliente.Activo = true;

                if (Request.QueryString["ID"] != null)
                {
                    cliente.ID = long.Parse(Request.QueryString["ID"]);
                    negocio.Modificar(cliente);
                }
                else
                {
                    negocio.Agregar(cliente);
                }

                LimpiarCampos();
                Response.Redirect("Clientes.aspx", false);
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void BtnInactivar_Click(object sender, EventArgs e)
        {
            ConfirmarInactivar = true;
            ConfirmarReactivar = false;
        }

        protected void btnConfirmaInactivar_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkConfirmaInactivacion.Checked)
                {
                    ClienteNegocio negocio = new ClienteNegocio();
                    negocio.EliminarLogico(long.Parse(Request.QueryString["ID"]));
                    Response.Redirect("Clientes.aspx", false);
                }
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnReactivar_Click(object sender, EventArgs e)
        {
            ConfirmarInactivar = false;
            ConfirmarReactivar = true;
        }
      
        protected void btnConfirmaReactivar_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkConfirmaReactivacion.Checked)
                {
                    ClienteNegocio negocio = new ClienteNegocio();
                    negocio.EliminarLogico(long.Parse(Request.QueryString["ID"]));
                    Response.Redirect("Clientes.aspx", false);
                }
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}