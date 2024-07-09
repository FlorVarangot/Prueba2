using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPC_Equipo26.Dominio
{
    public class Usuario
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string User { get; set; }
        public string Pass { get; set; }
        public string ImagenPerfil { get; set; }
        public bool TipoUsuario { get; set; }


        public Usuario()
        {

        }

        public Usuario(string user, string pass, bool admin)
        {
            User = user;
            Pass = pass;
            TipoUsuario = admin ? TipoUsuario = true : TipoUsuario = false;
        }

    }
}