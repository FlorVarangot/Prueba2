using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPC_Equipo26.Dominio
{
    public class Compra
    {
        public long ID { get; set; }
        public DateTime FechaCompra { get; set; }
        public int IdProveedor { get; set; }
        public decimal TotalCompra { get; set; }
        public List<DetalleCompra> Detalles { get; set; }

        //public bool Activo {get; set;}
        public Compra()
        {
            Detalles = new List<DetalleCompra>();
        }

    }
}