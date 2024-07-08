using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPC_Equipo26.Dominio
{
    public enum TipoUsuario
    {
        ADMIN = 1,
        VENDEDOR = 2
    }
    public class Usuario
    {
        public int ID { get; set; }
        public string User {get; set;}
        public string Pass { get; set; }
        public TipoUsuario TipoUsuario {  get; set; }


        public Usuario(string user, string pass, bool admin)
        {
            User = user;
            Pass = pass;
            TipoUsuario = admin ? TipoUsuario.ADMIN : TipoUsuario.VENDEDOR;
        }

    }
}