using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPC_Equipo26.Dominio;

namespace TPC_Equipo26
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ValidarSesion())
            {
                btnLogIn.Visible = false;
                btnLogOut.Visible = true;
                btnPerfil.Visible = true;
                opcionesMenu.Visible = true;
            }
            else
            {
                btnLogIn.Visible = true;
                btnLogOut.Visible = false;
                btnPerfil.Visible = false;
                opcionesMenu.Visible = false;
            }


        }
        protected bool ValidarSesion()
        {
            if (Session["Usuario"] != null)
            {
                return true;
            }
            return false;
        }

    }
}