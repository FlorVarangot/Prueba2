using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPC_Equipo26.Dominio
{
    public class Venta
    {
        public int ID { get; set; }
        public DateTime FechaVenta { get; set; }
        public int IdCliente { get; set; }
        public float Total { get; set; }
        public List<Articulo> DetalleVenta { get; set; }
        
        //public bool Activo {get; set;}
    }
}