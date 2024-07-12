using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPC_Equipo26.Dominio;
using TPC_Equipo26.Negocio;

namespace TPC_Equipo26
{
    public partial class LogIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            UsuarioNegocio negocio = new UsuarioNegocio();
            try
            {
                if (negocio.ValidarUsuario(txtUser.Text, txtPassword.Text))
                {
                    usuario.User = txtUser.Text;
                    usuario.Pass = txtPassword.Text;

                    if (negocio.LogIn(usuario))
                    {
                        Session.Add("Usuario", usuario);
                        Response.Redirect("MiPerfil.aspx", false);
                    }
                }
                else
                {
                    Session.Add("Error", "Nombre de usuario y/o contraseña incorrectos.");
                    Response.Redirect("Error.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}