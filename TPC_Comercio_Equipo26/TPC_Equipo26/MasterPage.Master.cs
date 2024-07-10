using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPC_Equipo26.Dominio;
using TPC_Equipo26.Negocio;

namespace TPC_Equipo26
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            imgAvatar.ImageUrl = "https://th.bing.com/th/id/OIP.fqSvfYQB0rQ-6EG_oqvonQHaHa?rs=1&pid=ImgDetMain";
            if (!(Page is LogIn || Page is Registro || Page is Default || Page is Error))
            {
                if (!Seguridad.sesionActiva(Session["Usuario"]))
                {
                    btnLogIn.Visible = false;
                    btnLogOut.Visible = true;
                    btnPerfil.Visible = true;
                    btnRegistro.Visible = false;
                    Response.Redirect("Login.aspx", false);
                }
                else
                {
                    Usuario user = (Usuario)Session["Usuario"];
                    //lblUser.Text = user.User;
                    if (!string.IsNullOrEmpty(user.ImagenPerfil))
                        imgAvatar.ImageUrl = "~/Images/" + user.ImagenPerfil;

                    btnLogIn.Visible = true;
                    btnLogOut.Visible = false;
                    btnPerfil.Visible = false;
                    btnRegistro.Visible = true;
                }
            }

        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Session.Clear();
            //Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
            Response.Redirect("Default.aspx");
        }
        protected void btnLogIn_Click(object sender, EventArgs e)
        {
            Response.Redirect("LogIn.aspx");
        }
        protected void btnPerfil_Click(object sender, EventArgs e)
        {
            Response.Redirect("MiPerfil.aspx");
        }
        protected void btnRegistro_Click(object sender, EventArgs e)
        {
            Response.Redirect("Registro.aspx");
        }
    }
}