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
                datos.setearConsulta("SELECT * FROM MARCAS");
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

        public string VerificarMarca(string descripcion, int idProveedor)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM MARCAS WHERE Descripcion = @Descripcion AND IdProveedor = @IdProveedor");
                datos.setearParametro("@Descripcion", descripcion);
                datos.setearParametro("@IdProveedor", idProveedor);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    int count = (int)datos.Lector[0];
                    if (count > 0)
                    {
                        return "El proveedor ya tiene una marca con ese nombre";
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
                datos.setearConsulta("INSERT INTO MARCAS VALUES (@Descripcion, @IdProveedor, @ImagenUrl, @Activo)");
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
                datos.setearConsulta("UPDATE MARCAS SET Descripcion = @Descripcion, IdProveedor = @IdProveedor, ImagenUrl = @ImagenUrl WHERE Id = @Id");

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
                datos.setearConsulta("UPDATE MARCAS SET Activo = @activo WHERE Id = @id");
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
                datos.setearConsulta("SELECT * FROM MARCAS WHERE Id = @id");
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
                datos.setearConsulta("UPDATE MARCAS SET Descripcion = @Descripcion, IdProveedor = @IdProveedor, ImagenUrl = @ImagenUrl, Activo = @Activo WHERE Id = @Id");

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