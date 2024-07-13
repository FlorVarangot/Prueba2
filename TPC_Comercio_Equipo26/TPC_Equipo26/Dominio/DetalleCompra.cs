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
        private decimal precio;
        public decimal Precio
        {
            get { return precio; }
            set
            {
                if (value > 0)
                    precio = value;
                else
                    throw new Exception("El precio debe ser mayor a 0.");
            }
        }

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
        public int IdMarca { get; set; }
    }
}