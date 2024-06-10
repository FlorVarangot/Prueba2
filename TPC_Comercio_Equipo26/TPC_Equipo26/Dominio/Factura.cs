using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPC_Equipo26.Dominio
{
    public class Factura
    {
        public long ID { get; set; }
        public long Numero { get; set; }
        public DateTime FechaFactura { get; set; }
        public long IdVenta {get; set;}
        public bool Activo {get; set;}
        
    }
}