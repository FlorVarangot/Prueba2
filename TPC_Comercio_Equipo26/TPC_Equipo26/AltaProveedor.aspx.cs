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
            ValidarAdmin();
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
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        private void PrecargarProveedores(int Id)
        {
            try
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
                    Session.Add("Error", "No se encontró el proveedor.");
                    Response.Redirect("Error.aspx");
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
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
            bool camposValidos = true;

            if (string.IsNullOrWhiteSpace(TxtNombre.Text))
            {
                lblNombre.Visible = true;
                camposValidos = false;
            }
            else
            {
                lblNombre.Visible = false;
            }
            if (string.IsNullOrWhiteSpace(TxtCuit.Text))
            {
                lblCuit.Visible = true;
                camposValidos = false;
            }
            else
            {
                lblCuit.Visible = false;
            }
            if (string.IsNullOrWhiteSpace(TxtEmail.Text))
            {
                lblEmail.Visible = false;
            }
            else
            {
                lblEmail.Visible = false;
            }
            if (string.IsNullOrWhiteSpace(TxtTel.Text))
            {
                lblTel.Visible = true;
                camposValidos = false;
            }
            else
            {
                lblTel.Visible = false;
            }
            if (string.IsNullOrWhiteSpace(TxtDirec.Text))
            {
                lblDirec.Visible = true;
                camposValidos = false;
            }
            else
            {
                lblDirec.Visible = false;
            }

            if (!camposValidos)
            {
                lblError.Text = "Todos los campos obligatorios deben ser completados.";
                lblError.Visible = true;
            }
            return camposValidos;
        }

        protected void BtnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos())
                {
                    return;
                }
                Page.Validate();
                if (!Page.IsValid)
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

                if (Request.QueryString["ID"] != null)
                {
                    proveedor.ID = int.Parse(Request.QueryString["ID"]);
                    string verificarDuplicado = negocio.VerificarProveedor(proveedor.Nombre, proveedor.CUIT, proveedor.Email, proveedor.ID);
                    if (verificarDuplicado != null)
                    {
                        lblError.Text = verificarDuplicado;
                        lblError.Visible = true;
                        return;
                    }
                    negocio.Modificar(proveedor);
                }
                else
                {
                    string verificarDuplicado = negocio.VerificarProveedor(proveedor.Nombre, proveedor.CUIT, proveedor.Email);
                    if (verificarDuplicado != null)
                    {
                        lblError.Text = verificarDuplicado;
                        lblError.Visible = true;
                        return;
                    }
                    negocio.Agregar(proveedor);
                }

                LimpiarCampos();
                Response.Redirect("Proveedores.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
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
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
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
                    if (!ValidarCampos())
                    {
                        return;
                    }

                    Proveedor proveedor = new Proveedor();
                    ProveedorNegocio negocio = new ProveedorNegocio();

                    proveedor.ID = int.Parse(Request.QueryString["ID"]);
                    proveedor.Nombre = TxtNombre.Text;
                    proveedor.CUIT = TxtCuit.Text;
                    proveedor.Email = TxtEmail.Text;
                    proveedor.Telefono = TxtTel.Text;
                    proveedor.Direccion = TxtDirec.Text;
                    proveedor.Activo = true;

                    negocio.ReactivarModificar(proveedor);
                    Response.Redirect("Proveedores.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void ValidarAdmin()
        {
            if (!Seguridad.esAdmin(Session["Usuario"]))
            {
                Session.Add("Error", "No tenes permisos para ingresar a esta pantalla.");
                Response.Redirect("Error.aspx", false);
            }
        }

    }
}