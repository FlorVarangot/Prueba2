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
    public partial class AltaProveedor : System.Web.UI.Page
    {
        public bool ConfirmarInactivar { get; set; }
        public bool ConfirmarReactivar { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    if (Request.QueryString["ID"] != null)
                    {
                        int idProv = int.Parse(Request.QueryString["ID"]);
                        lblTituloModificar.Visible = true;
                        PrecargarProveedores(idProv);
                    }
                    else
                    {
                        lblTituloAgregar.Visible = true;
                        BtnInactivar.Visible = false;
                        BtnReactivar.Visible = false;
                        LimpiarCampos();
                    }
                }
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx");
            }
        }

        private void PrecargarProveedores(int Id)
        {
            ProveedorNegocio negocio = new ProveedorNegocio();
            Proveedor proveedor = negocio.ObtenerProveedorPorId(Id);

            if (proveedor != null)
            {
                TxtNombre.Text = proveedor.Nombre;
                TxtCuit.Text = proveedor.CUIT;
                TxtEmail.Text = proveedor.Email;
                TxtTel.Text = proveedor.Telefono;
                TxtDirec.Text = proveedor.Direccion;

                if (proveedor.Activo == true)
                {
                    BtnInactivar.Visible = true;
                    BtnReactivar.Visible = false;
                }
                else
                {
                    BtnInactivar.Visible = false;
                    BtnReactivar.Visible = true;
                }
            }
            else
            {
                Response.Redirect("Error.aspx");
            }
        }

        private void LimpiarCampos()
        {
            TxtNombre.Text = string.Empty;
            TxtCuit.Text = string.Empty;
            TxtEmail.Text = string.Empty;
            TxtTel.Text = string.Empty;
            TxtDirec.Text = string.Empty;
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(TxtNombre.Text) ||
                string.IsNullOrWhiteSpace(TxtCuit.Text) ||
                string.IsNullOrWhiteSpace(TxtEmail.Text) ||
                string.IsNullOrWhiteSpace(TxtTel.Text) ||
                string.IsNullOrWhiteSpace(TxtDirec.Text))
            {
                lblError.Text = "Todos los campos deben ser completados.";
                lblError.Visible = true;
                return false;
            }
            return true;
        }
        protected void BtnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos())
                {
                    return;
                }
                Proveedor proveedor = new Proveedor();
                ProveedorNegocio negocio = new ProveedorNegocio();

                proveedor.Nombre = TxtNombre.Text;
                proveedor.CUIT = TxtCuit.Text;
                proveedor.Email = TxtEmail.Text;
                proveedor.Telefono = TxtTel.Text;
                proveedor.Direccion = TxtDirec.Text;

                string verificarDuplicado = negocio.VerificarProveedor(proveedor.Nombre, proveedor.CUIT, proveedor.Email);
                if (verificarDuplicado != null)
                {
                    lblError.Text = verificarDuplicado;
                    lblError.Visible = true;
                    return;
                }

                if (Request.QueryString["ID"] != null)
                {
                    proveedor.ID = int.Parse(Request.QueryString["ID"]);
                    negocio.Modificar(proveedor);
                }
                else
                {
                    negocio.Agregar(proveedor);
                }

                LimpiarCampos();
                Response.Redirect("Proveedores.aspx", false);
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

        protected void BtnConfirmaInactivar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ChkConfirmaInactivacion.Checked)
                {
                    int id = Convert.ToInt32(Request.QueryString["ID"]);
                    ProveedorNegocio negocio = new ProveedorNegocio();
                    negocio.ActivarLogico(id, false);
                    Response.Redirect("Proveedores.aspx", false);
                }
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void BtnReactivar_Click(object sender, EventArgs e)
        {
            ConfirmarInactivar = false;
            ConfirmarReactivar = true;
        }

        protected void BtnConfirmaReactivar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ChkConfirmaReactivacion.Checked)
                {
                    int id = Convert.ToInt32(Request.QueryString["ID"]);
                    ProveedorNegocio negocio = new ProveedorNegocio();
                    negocio.ActivarLogico(id, true);
                    Response.Redirect("Proveedores.aspx", false);
                }
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}