using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPC_Equipo26.Dominio
{
    public class Imagen
    {
        public int ID {get; set;}
        public int IdArticulo { get; set; }
        public string UrlImagen { get; set; }
        public bool Activo { get; set; }

}
}