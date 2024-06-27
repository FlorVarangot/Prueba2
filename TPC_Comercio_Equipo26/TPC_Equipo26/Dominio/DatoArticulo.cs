using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPC_Equipo26.Dominio
{
    public class DatoArticulo
    {
        public long Id { get; set; }
        public long IdArticulo { get; set; }
        public DateTime Fecha { get; set; }
        public int Stock { get; set; }
        public decimal Precio { get; set; }

    }
}