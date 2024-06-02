using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPC_Equipo26.Dominio
{
    public class Usuario
    {
        public int ID { get; set; }
        public string NombreUsuario{get; set;}
        public string Contraseña { get; set; }
        
        //Rol: 1-Cliente, 2-Vendedor, 3-Administrador (“Id_Tipo”)
        public int Rol { get; set; }

        //public bool Activo { get; set; }

    }
}