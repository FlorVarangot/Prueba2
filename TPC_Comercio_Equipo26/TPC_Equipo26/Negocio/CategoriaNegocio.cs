using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPC_Equipo26.Dominio;

namespace TPC_Equipo26.Negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> Listar()
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("sp_listarCategorias");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria aux = new Categoria();
                    aux.ID = (int)datos.Lector["Id"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Activo = (bool)datos.Lector["Activo"];
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

        public Categoria ObtenerCategoriaPorId(int Id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("sp_obtenerCategoriaPorId");
                datos.setearParametro("@id", Id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Categoria categoria = new Categoria();
                    categoria.ID = int.Parse(datos.Lector["Id"].ToString());
                    categoria.Descripcion = datos.Lector["Descripcion"].ToString();
                    categoria.Activo = (bool)datos.Lector["Activo"];

                    return categoria;
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
            finally
            {
                datos.cerrarConexion();
            }
        }

        public string VerificarCategoria(string descripcion)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("sp_verificarCategoria");
                datos.setearParametro("@Descripcion", descripcion);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    int count = (int)datos.Lector[0];
                    if (count > 0)
                    {
                        return "Ya existe una categoria con ese Nombre";
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
        public string VerificarCategoria(string descripcion, int idCategoria)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("sp_verificarCategoriaConID");
                datos.setearParametro("@Descripcion", descripcion);
                datos.setearParametro("@IdCategoria", idCategoria);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    int count = Convert.ToInt32(datos.Lector[0]);
                    if (count > 0)
                    {
                        return "Ya existe una categoría con ese Nombre";
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

        public void Agregar(Categoria categoria)
        {          
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("sp_agregarCategoria");
                datos.setearParametro("@Descripcion", categoria.Descripcion);
                datos.setearParametro("@Activo", categoria.Activo);

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

        public void Modificar(Categoria categoria)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("sp_modificarCategoria");
                datos.setearParametro("@Descripcion", categoria.Descripcion);
                datos.setearParametro("@Id", categoria.ID);

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

        public void EliminarLogico(int Id, bool estado)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("sp_eliminarLogico");
                datos.setearParametro("@Id", Id);
                datos.setearParametro("@activo", estado);
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

        public void ReactivarModificar (Categoria categoria)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("sp_reactivarModificar");

                datos.setearParametro("@Descripcion", categoria.Descripcion);
                datos.setearParametro("@Activo", categoria.Activo);
                datos.setearParametro("@Id", categoria.ID);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { datos.cerrarConexion();}
        }
    }
}