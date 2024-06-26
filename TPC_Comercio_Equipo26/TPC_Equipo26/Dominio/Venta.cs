using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPC_Equipo26.Dominio
{
    public class Venta
    {
        public long ID { get; set; }
        public DateTime FechaVenta { get; set; }
        public long IdCliente { get; set; }
        public decimal Total { get; set; }
        public List<DetalleVenta> Detalles { get; set; }
        
        //public bool Activo {get; set;}
    }
}