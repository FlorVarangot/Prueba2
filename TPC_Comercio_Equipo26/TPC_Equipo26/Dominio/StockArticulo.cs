using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPC_Equipo26.Dominio
{
    public class StockArticulo
    {
        public long IdArticulo { get; set; }
        public DateTime Fecha { get; set; }
        public int StockActual { get; set; }
        public decimal PrecioActual { get; set; }
    }
}