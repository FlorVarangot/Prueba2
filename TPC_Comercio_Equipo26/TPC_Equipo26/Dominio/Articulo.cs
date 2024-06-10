using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TPC_Equipo26.Dominio
{
    public class Articulo
    {
        public long ID {get; set;}
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Marca Marca { get; set; }
        public Categoria Categoria { get; set; }
        public string Imagen { get; set; }
        public decimal Ganancia { get; set; }
        public int StockMin {get; set;}
        public bool Activo { get; set; }
    }
}