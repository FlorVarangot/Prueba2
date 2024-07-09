using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using TPC_Equipo26.Dominio;
using static System.Net.WebRequestMethods;

namespace TPC_Equipo26.Negocio
{
    public class MarcaNegocio
    {
        public List<Marca> Listar()
        {
            List<Marca> lista = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("sp_ListarMarcas");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Marca aux = new Marca();
                    aux.ID = int.Parse(datos.Lector["Id"].ToString());
                    aux.Descripcion = datos.Lector["Descripcion"].ToString();
                    aux.IdProveedor = int.Parse(datos.Lector["IdProveedor"].ToString());
                    aux.ImagenUrl = datos.Lector["ImagenUrl"] != DBNull.Value ? datos.Lector["ImagenUrl"].ToString() : null;
                    aux.Activo = (bool)datos.Lector["Activo"];

                    if (string.IsNullOrEmpty(aux.ImagenUrl))
                    {
                        aux.ImagenUrl = "https://www.shutterstock.com/image-vector/default-ui-image-placeholder-wireframes-600nw-1037719192.jpg";
                    }

                    lista.Add(aux);
                }
                return lista;
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

        public string VerificarMarca(string descripcion)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("sp_verificarMarca");
                datos.setearParametro("@Descripcion", descripcion);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    int count = (int)datos.Lector[0];
                    if (count > 0)
                    {
                        return "Ya existe una marca con ese nombre";
                    }
                }
                return null;
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

        public string VerificarMarca(string descripcion, int idMarca)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("sp_verificarMarcaConId");
                datos.setearParametro("@Descripcion", descripcion);
                datos.setearParametro("@IdMarca", idMarca);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    int count = (int)datos.Lector[0];
                    if (count > 0)
                    {
                        return "Ya existe una marca con ese nombre";
                    }
                }
                return null;
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

        public void Agregar(Marca marca)
        {          
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("sp_AgregarMarca");
                datos.setearParametro("@Descripcion", marca.Descripcion);
                datos.setearParametro("@IdProveedor", marca.IdProveedor);
                datos.setearParametro("@ImagenUrl", marca.ImagenUrl);
                datos.setearParametro("@Activo", 1);

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

        public void Modificar(Marca marca)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("sp_modificarMarca");

                datos.setearParametro("@Descripcion", marca.Descripcion);
                datos.setearParametro("@IdProveedor", marca.IdProveedor);
                datos.setearParametro("@ImagenUrl", marca.ImagenUrl);
                datos.setearParametro("@Id", marca.ID);

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

        internal void ActivarLogico(int id, bool activo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("sp_eliminarLogicoMarca");
                datos.setearParametro("@id", id);
                datos.setearParametro("@activo", activo);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { datos.cerrarConexion(); }
        }

        public Marca ObtenerMarcaPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("sp_obtenerMarcaPorId");
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Marca marca = new Marca();
                    marca.ID = int.Parse(datos.Lector["Id"].ToString());
                    marca.Descripcion = datos.Lector["Descripcion"].ToString();
                    marca.IdProveedor = int.Parse(datos.Lector["IdProveedor"].ToString());
                    marca.ImagenUrl = datos.Lector["ImagenUrl"].ToString();
                    marca.Activo = (bool)datos.Lector["Activo"];

                    return marca;
                }
                else
                {
                    return null;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally { datos.cerrarConexion(); }

        }

        public void ReactivarModificar(Marca marca)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("sp_reactivarModificarMarca");

                datos.setearParametro("@Descripcion", marca.Descripcion);
                datos.setearParametro("@IdProveedor", marca.IdProveedor);
                datos.setearParametro("@ImagenUrl", marca.ImagenUrl);
                datos.setearParametro("@Activo", marca.Activo);
                datos.setearParametro("@Id", marca.ID);

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
    }
}