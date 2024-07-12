using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPC_Equipo26.Dominio
{
    public class Usuario
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }

        private string user;
        public string User
        {
            get { return user; }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length >= 4)
                {
                    user = value;
                }
                else
                {
                    string mensajeError = "El nombre de usuario debe contener al menos 4 caracteres.";
                    throw new Exception(mensajeError);
                }
            }
        }

        private string pass;
        public string Pass
        {
            get { return pass; }
            set
            {
                if (value.Length >= 4 && System.Text.RegularExpressions.Regex.IsMatch(value, @"\d"))
                {
                    pass = value;
                }
                else
                {
                    string mensajeError = "La contraseña debe contener al menos 4 caracteres y al menos 1 caracter numérico.";
                    throw new Exception(mensajeError);
                }

            }
        }
        public string ImagenPerfil { get; set; }
        public bool TipoUsuario { get; set; }


        public Usuario()
        {

        }

        public Usuario(string user, string pass, bool admin)
        {
            User = user;
            Pass = pass;
            TipoUsuario = admin ? TipoUsuario = true : TipoUsuario = false;
        }

    }
}