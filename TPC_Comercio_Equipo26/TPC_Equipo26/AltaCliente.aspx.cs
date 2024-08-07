﻿using System;
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
            if (ValidarSesionActiva())
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
                catch (Exception ex)
                {
                    Session.Add("Error", ex.ToString());
                    Response.Redirect("Error.aspx", false);
                }
            }
            else
            {
                Session.Add("Error", "No tenes permisos para ingresar a esta pantalla.");
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
            try
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
                    Session.Add("Error", "No se encontró el cliente.");
                    Response.Redirect("Error.aspx");
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        private bool ValidarCampos()
        {
            bool camposValidos = true;
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                lblNombre.Visible = true;
                camposValidos = false;
            }
            else
            {
                lblNombre.Visible = false;
            }
            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                lblApellido.Visible = true;
                camposValidos = false;
            }
            else
            {
                lblApellido.Visible = false;
            }
            if (string.IsNullOrWhiteSpace(txtDNI.Text))
            {
                lblDNI.Visible = true;
                camposValidos = false;
            }
            else
            {
                lblDNI.Visible = false;
            }
            if (string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                lblTelefono.Visible = true;
                camposValidos = false;
            }
            else
            {
                lblTelefono.Visible = false;
            }
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                lblEmail.Visible = true;
                camposValidos = false;
            }
            else
            {
                lblEmail.Visible = false;
            }
            if (string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                lblDireccion.Visible = true;
                camposValidos = false;
            }
            else
            {
                lblDireccion.Visible = false;
            }

            if (!camposValidos)
            {
                lblError.Text = "Todos los campos obligatorios deben ser completados";
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
                Cliente cliente = new Cliente();
                ClienteNegocio negocio = new ClienteNegocio();

                cliente.Nombre = txtNombre.Text;
                cliente.Apellido = txtApellido.Text;
                cliente.Dni = txtDNI.Text;
                cliente.Telefono = txtTelefono.Text;
                cliente.Email = txtEmail.Text;
                cliente.Direccion = txtDireccion.Text;
                cliente.Activo = true;

                string mensaje;
                if (Request.QueryString["ID"] != null)
                {
                    cliente.ID = long.Parse(Request.QueryString["ID"]);
                    string verificarDuplicado = negocio.VerificarCliente(cliente.Dni, cliente.ID);
                    if (verificarDuplicado != null)
                    {
                        lblError.Text = verificarDuplicado;
                        lblError.Visible = true;
                        return;
                    }
                    negocio.Modificar(cliente);
                    mensaje = "Cliente modificado con éxito";
                }
                else
                {
                    string verificarDuplicado = negocio.VerificarCliente(cliente.Dni);
                    if (verificarDuplicado != null)
                    {
                        lblError.Text = verificarDuplicado;
                        lblError.Visible = true;
                        return;
                    }
                    negocio.Agregar(cliente);
                    mensaje = "Cliente agregado con éxito";
                }

                LimpiarCampos();
                ClientScript.RegisterStartupScript(GetType(), "mostrarMensajeExito", $"alert('{mensaje}'); window.location.href = 'Clientes.aspx';", true);

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

        protected void btnConfirmaInactivar_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkConfirmaInactivacion.Checked)
                {
                    long id = Convert.ToInt64(Request.QueryString["ID"]);
                    ClienteNegocio negocio = new ClienteNegocio();
                    negocio.EliminarLogico(id, false);
                    Response.Redirect("Clientes.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
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
                    if (!ValidarCampos())
                    {
                        return;
                    }
                    Page.Validate();
                    if (!Page.IsValid)
                    {
                        return;
                    }

                    Cliente cliente = new Cliente();
                    ClienteNegocio negocio = new ClienteNegocio();

                    cliente.ID = long.Parse(Request.QueryString["ID"]);
                    cliente.Nombre = txtNombre.Text;
                    cliente.Apellido = txtApellido.Text;
                    cliente.Dni = txtDNI.Text;
                    cliente.Telefono = txtTelefono.Text;
                    cliente.Email = txtEmail.Text;
                    cliente.Direccion = txtDireccion.Text;
                    cliente.Activo = true;

                    negocio.ReactivarModificar(cliente);
                    Response.Redirect("Clientes.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected bool ValidarSesionActiva()
        {
            if (Seguridad.sesionActiva(Session["Usuario"]))
                return true;
            return false;
        }

    }
}