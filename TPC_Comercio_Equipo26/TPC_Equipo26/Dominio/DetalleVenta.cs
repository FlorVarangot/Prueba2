using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPC_Equipo26.Dominio
{
    public class DetalleVenta
    {
        public long Id { get; set; }
        public long IdVenta { get; set; }
        public long IdArticulo { get; set; }
        public int Cantidad { get; set; }
    }
}