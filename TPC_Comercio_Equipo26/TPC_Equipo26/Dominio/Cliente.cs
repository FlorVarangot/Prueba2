using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TPC_Equipo26.Dominio
{
    public class Cliente
    {
        public long ID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        private string dni;
        public string Dni
        {
            get { return dni; }
            set
            {
                if (value.Length >= 8)
                    dni = value;
                else
                {
                    string mensajeError = "El DNI debe contener al menos 8 caracteres";
                    throw new Exception(mensajeError);
                }

            }
        }

        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public bool Activo { get; set; }

    }
}