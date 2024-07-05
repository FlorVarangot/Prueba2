using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;

namespace TPC_Equipo26.Dominio
{
    public class DetalleCompra
    {
        public long Id { get; set; }
        public long IdCompra { get; set; }
        public long IdArticulo { get; set; }  
        public string NombreArticulo { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public int IdMarca { get; set; }
    }
}