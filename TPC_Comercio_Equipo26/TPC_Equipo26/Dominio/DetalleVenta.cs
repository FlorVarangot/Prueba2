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
        private int cantidad;
        public int Cantidad
        {
            get { return cantidad; }
            set
            {
                if (value > 0)
                    cantidad = value;
                else
                    throw new Exception("La cantidad debe ser mayor a 0.");
            }
        }

    }
}