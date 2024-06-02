using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPC_Equipo26.Dominio
{
    public class Factura
    {
        public int ID { get; set; }
        public DateTime FechaFactura { get; set; }
        public Venta Venta {get; set;}

        //public bool Activo {get; set;}
        
        //public int IdVenta { get; set; }
        //total, artículos, cantidades, precio unitario
    }
}