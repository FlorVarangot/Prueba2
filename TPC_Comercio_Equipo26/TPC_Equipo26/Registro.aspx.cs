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

                ConfirmarRegistroCorreo(user);

                Session.Add("Usuario", user);
                Response.Redirect("Default.aspx", false);
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

        protected void ConfirmarRegistroCorreo(Usuario user)
        {
            try
            {
                EmailService emailService = new EmailService();
                string emailDestino = user.Email;
                string asunto = "Confirmación de registro";
                string cuerpo = $"¡Hola! ¡Bienvenido/a al sistema de gestión de nuestra tienda Librería!<br><br>" +
                                $"Detalles del registro:<br>" +
                                $"<strong>Nombre de usuario:</strong> {user.User}<br>" +
                                $"<strong>Contraseña:</strong> {user.Pass}<br><br>" +
                                $"Recuerda ingresar a MiPerfil para completar tus datos de registro." +
                                $"<br><br><em>La información detallada en este correo es sensible." +
                                $"No compartas esta información.</em>";

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


            if (txtUser.Text.Length > 0 && txtUser.Text.Length < 4)
            {
                lblUser.Text = "El nombre de usuario debe contener al menos 4 caracteres.";
                lblUser.Visible = true;
                camposValidos = false;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(txtPassword.Text, @"\d") && !string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                lblPassword.Text = "La contraseña debe contener al menos 1 caracter numérico.";
                lblPassword.Visible = true;
                camposValidos = false;
            }

            if (txtPassword.Text.Length > 0 && txtPassword.Text.Length < 4)
            {
                lblPassword.Text = "La contraseña debe contener al menos 4 caracteres";
                lblPassword.Visible = true;
                camposValidos = false;
            }

            return camposValidos;
        }

        protected void LimpiarLabels()
        {
            lblUser.Text = "";
            lblEmail.Visible = false;
            lblPassword.Visible = false;
        }


    }
}