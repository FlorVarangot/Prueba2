using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TPC_Equipo26.Dominio
{
    public class Proveedor
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        [DisplayName("Teléfono")]
        public string Telefono { get; set;}
        public string Email { get; set; }
        [DisplayName("Dirección")]
        public string Direccion { get; set; }
        public bool Activo { get; set; }

    }
}