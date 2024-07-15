using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TPC_Equipo26.Dominio
{
    public class Articulo
    {
        public long ID { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Marca Marca { get; set; }
        public Categoria Categoria { get; set; }
        public string Imagen { get; set; }

        private decimal ganancia;
        public decimal Ganancia
        {
            get { return ganancia; }
            set
            {
                if (value >= 0)
                    ganancia = value;
                else
                {
                    string mensajeError = "El porcentaje de ganancia debe ser mayor a 0";
                    throw new Exception(mensajeError);
                }
            }
        }
        
        private int stockMin;
        public int StockMin
        {
            get { return stockMin; }
            set
            {
                if (value >= 0)
                    stockMin = value;
                else
                {
                    string mensajeError = "El stock mínimo debe ser mayor a 0";
                    throw new Exception(mensajeError);
                }
            }
        }
        public bool Activo { get; set; }
    }
}