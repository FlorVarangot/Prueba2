using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TPC_Equipo26.Dominio
{
    public class Articulo
    {
        public int ID {get; set;}
        [DisplayName("Código")]
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }
        //public Marca Marca { get; set; }
        //[DisplayName("Categoría")]
        //public Categoria Categoria { get; set; }
        //public List<Imagen> Imagenes { get; set; }
        //public int Cantidad { get; set; }
        public float Precio { get; set; }
        //public int StockMin {get; set;}
        public bool Activo { get; set; }
    }
}