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
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            //{

            //}
            //catch (Exception ex)
            //{
            //    Session.Add("Error", ex.ToString());
            //    Response.Redirect("Error.aspx", false);
            //}
        }

        protected void btnRegistro_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos())
                    return;

                Usuario user = new Usuario();
                UsuarioNegocio negocio = new UsuarioNegocio();
                user.User = txtUser.Text;
                user.Pass = txtPassword.Text;
                user.Email = txtEmail.Text;
                user.ID = negocio.InsertarNuevo(user);

                ConfirmarRegistroCorreo(user.Email);

                Session.Add("Usuario", user);
                Response.Redirect("Default.aspx",false);
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("LogIn.aspx", false);
        }

        protected void ConfirmarRegistroCorreo(string mail)
        {
            try
            {
                EmailService emailService = new EmailService(); 
                string emailDestino = mail;
                string asunto = "Confirmación de registro";
                string cuerpo = $"¡Bienvenido/a al gestor de artículos de Librería!<br><br>" +
                                $"Detalles del registro:<br>" +
                                $"Nombre de usuario: {txtUser.Text}<br>" +
                                $"Contraseña: {txtPassword.Text}<br><br>" +
                                $"<br><br>La información detallada en este correo es sensible." +
                                $"No compartas esta información.";

                emailService.ArmarCorreo(emailDestino, asunto, cuerpo);
                emailService.enviarEmail();
            }
            catch (Exception)
            {
                Session.Add("Error", "Error al enviar el correo de confirmación de registro.");
                Response.Redirect("Error.aspx", false);
            }
        }

        protected bool ValidarCampos()
        {
            LimpiarLabels();
            bool camposValidos = true;

            if (string.IsNullOrWhiteSpace(txtUser.Text))
            {
                lblUser.Text = "El nombre de usuario no puede estar vacío.";
                lblUser.Visible = true;
                camposValidos = false;
            }
            
            if (txtUser.Text.Length < 4) {
                lblUser.Text = "El nombre de usuario debe contener al menos 4 caracteres.";
                lblUser.Visible = true;
                camposValidos = false;
            }

            
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                lblEmail.Text = "El mail no puede estar vacío.";
                lblEmail.Visible = true;
                camposValidos = false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                lblPassword.Text = "La contraseña no puede estar vacío.";
                lblPassword.Visible = true;
                camposValidos = false;
            }
            
            if(!System.Text.RegularExpressions.Regex.IsMatch(txtPassword.Text, @"\d"))
            {
                lblPassword.Text = "La contraseña debe contener al menos 1 caracter numérico.";
                lblPassword.Visible = true;
                camposValidos = false;
            }

            if (txtPassword.Text.Length < 4)
            {
                lblPassword.Text = "La contraseña debe contener al menos 4 caracteres";
                lblPassword.Visible = true;
                camposValidos = false;
            }

            return camposValidos;
        }

        protected void LimpiarLabels()
        {
            lblUser.Text= "";
            lblEmail.Visible = false;
            lblPassword.Visible= false;
        }


    }
}