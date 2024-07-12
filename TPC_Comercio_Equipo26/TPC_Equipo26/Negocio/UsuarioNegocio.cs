using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI.WebControls;
using TPC_Equipo26.Dominio;

namespace TPC_Equipo26.Negocio
{
    public class UsuarioNegocio
    {
        public bool LogIn(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT * FROM USUARIOS WHERE Usuario = @user AND Contraseña = @pass");
                datos.setearParametro("@user", usuario.User);
                datos.setearParametro("@pass", usuario.Pass);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    usuario.ID = (int)datos.Lector["Id"];
                    usuario.TipoUsuario = (bool)datos.Lector["Tipo"];

                    if (!(datos.Lector["Nombre"] is DBNull))
                        usuario.Nombre = (string)datos.Lector["Nombre"];

                    if (!(datos.Lector["Apellido"] is DBNull))
                        usuario.Apellido = (string)datos.Lector["Apellido"];

                    if (!(datos.Lector["Email"] is DBNull))
                        usuario.Email = (string)datos.Lector["Email"];

                    if (!(datos.Lector["Imagen"] is DBNull))
                        usuario.ImagenPerfil = (string)datos.Lector["Imagen"];

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

        public int InsertarNuevo(Usuario nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("SP_NuevoUsuario");
                datos.setearParametro("@User", nuevo.User);
                datos.setearParametro("@Pass", nuevo.Pass);
                datos.setearParametro("@Email", nuevo.Email);
                return datos.ejecutarAccionScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { datos.cerrarConexion(); }
        }

        internal void Actualizar(Usuario user)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("SP_ModificarUsuario");
                datos.setearParametro("@Nombre", user.Nombre);
                datos.setearParametro("@Apellido", user.Apellido);
                datos.setearParametro("@Email", user.Email);
                datos.setearParametro("@Imagen", user.ImagenPerfil != null ? user.ImagenPerfil : (object)DBNull.Value);
                //datos.setearParametro("@img", (object)DBNull.Value ?? DBNull.Value);
                datos.setearParametro("@Id", user.ID);
                datos.ejecutarAccion();

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

        public bool ValidarUsuario(string user, string pass)
        {
            AccesoDatos datos = new AccesoDatos();

            datos.setearConsulta("SELECT * FROM USUARIOS WHERE Usuario = @user AND Contraseña = @pass");
            datos.setearParametro("@user", user);
            datos.setearParametro("@pass", pass);
            datos.ejecutarLectura();

            if (datos.Lector.Read())
                return true;
            return false;
            
        }
    }
}