using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using TPC_Equipo26.Dominio;

namespace TPC_Equipo26.Negocio
{
    public class UsuarioNegocio
    {
        public bool Loguear(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {

                datos.setearConsulta("SELECT Id, Tipo FROM USUARIOS WHERE Usuario = @user AND Contraseña = @pass");
                datos.setearParametro("@user",usuario.User);
                datos.setearParametro("@pass",usuario.Pass);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    usuario.ID = (int)datos.Lector["Id"];
                    usuario.TipoUsuario = (int)datos.Lector["Tipo"] == 1 ? TipoUsuario.ADMIN : TipoUsuario.VENDEDOR ;
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}