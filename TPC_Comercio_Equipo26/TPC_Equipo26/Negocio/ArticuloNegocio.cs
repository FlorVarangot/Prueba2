using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Globalization;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.Web.UI.WebControls;
using TPC_Equipo26.Dominio;


namespace TPC_Equipo26.Negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> Listar()
        {
            List<Articulo> listaArticulos = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, M.Descripcion AS Marca, C.Descripcion AS Categoria, A.Ganancia_Porcentaje, A.Stock_Minimo, A.Imagen, A.Activo FROM ARTICULOS A INNER JOIN MARCAS M ON M.Id = A.IdMarca INNER JOIN CATEGORIAS C ON C.Id = A.IdCategoria";

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo arti = new Articulo();
                    {
                        arti.ID = datos.Lector.GetInt64(0);
                        arti.Codigo = datos.Lector["Codigo"].ToString();
                        arti.Nombre = datos.Lector["Nombre"].ToString();
                        arti.Descripcion = datos.Lector["Descripcion"].ToString();
                        arti.Marca = new Marca { Descripcion = datos.Lector["Marca"].ToString() };
                        arti.Categoria = new Categoria { Descripcion = datos.Lector["Categoria"].ToString() };
                        arti.Ganancia = (decimal)datos.Lector["Ganancia_Porcentaje"];
                        arti.StockMin = datos.Lector.GetInt32(7);
                        arti.Imagen = datos.Lector["Imagen"] != DBNull.Value ? datos.Lector["Imagen"].ToString() : null;
                        arti.Activo = bool.Parse(datos.Lector["Activo"].ToString());

                        if (string.IsNullOrEmpty(arti.Imagen))
                        {
                            arti.Imagen = "https://www.shutterstock.com/image-vector/default-ui-image-placeholder-wireframes-600nw-1037719192.jpg";
                        }


                    };                 
                    listaArticulos.Add(arti);
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
            return listaArticulos;
        }

        public List<Articulo> ListarPorProveedor(int idProveedor)
        {
            List<Articulo> listaArticulos = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = @"SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, M.Descripcion AS Marca, C.Descripcion AS Categoria, A.Ganancia_Porcentaje, A.Stock_Minimo, A.Imagen, A.Activo 
                            FROM ARTICULOS A 
                            INNER JOIN MARCAS M ON M.Id = A.IdMarca 
                            INNER JOIN CATEGORIAS C ON C.Id = A.IdCategoria
                            WHERE M.IdProveedor = @IdProveedor";

                datos.setearConsulta(consulta);
                datos.setearParametro("@IdProveedor", idProveedor);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo arti = new Articulo();
                    {
                        arti.ID = datos.Lector.GetInt64(0);
                        arti.Codigo = datos.Lector["Codigo"].ToString();
                        arti.Nombre = datos.Lector["Nombre"].ToString();
                        arti.Descripcion = datos.Lector["Descripcion"].ToString();
                        arti.Marca = new Marca { Descripcion = datos.Lector["Marca"].ToString() };
                        arti.Categoria = new Categoria { Descripcion = datos.Lector["Categoria"].ToString() };
                        arti.Ganancia = (decimal)datos.Lector["Ganancia_Porcentaje"];
                        arti.StockMin = datos.Lector.GetInt32(7);
                        arti.Imagen = datos.Lector["Imagen"] != DBNull.Value ? datos.Lector["Imagen"].ToString() : null;
                        arti.Activo = bool.Parse(datos.Lector["Activo"].ToString());
                    };

                    if (string.IsNullOrEmpty(arti.Imagen))
                    {
                        arti.Imagen = "https://www.shutterstock.com/image-vector/default-ui-image-placeholder-wireframes-600nw-1037719192.jpg";
                    }

                    listaArticulos.Add(arti);
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
            return listaArticulos;
        }

        public List<Articulo> ListarPorMarca(int idMarca)
        {
            List<Articulo> listaArticulos = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = @"SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, M.Descripcion AS Marca, C.Descripcion AS Categoria, A.Ganancia_Porcentaje, A.Stock_Minimo, A.Imagen, A.Activo 
                            FROM ARTICULOS A 
                            INNER JOIN MARCAS M ON M.Id = A.IdMarca 
                            INNER JOIN CATEGORIAS C ON C.Id = A.IdCategoria
                            WHERE A.IdMarca = @IdMarca";

                datos.setearConsulta(consulta);
                datos.setearParametro("@IdMarca", idMarca);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo arti = new Articulo();
                    {
                        arti.ID = datos.Lector.GetInt64(0);
                        arti.Codigo = datos.Lector["Codigo"].ToString();
                        arti.Nombre = datos.Lector["Nombre"].ToString();
                        arti.Descripcion = datos.Lector["Descripcion"].ToString();
                        arti.Marca = new Marca { Descripcion = datos.Lector["Marca"].ToString() };
                        arti.Categoria = new Categoria { Descripcion = datos.Lector["Categoria"].ToString() };
                        arti.Ganancia = (decimal)datos.Lector["Ganancia_Porcentaje"];
                        arti.StockMin = datos.Lector.GetInt32(7);
                        arti.Imagen = datos.Lector["Imagen"] != DBNull.Value ? datos.Lector["Imagen"].ToString() : null;
                        arti.Activo = bool.Parse(datos.Lector["Activo"].ToString());
                    };

                    if (string.IsNullOrEmpty(arti.Imagen))
                    {
                        arti.Imagen = "https://www.shutterstock.com/image-vector/default-ui-image-placeholder-wireframes-600nw-1037719192.jpg";
                    }

                    listaArticulos.Add(arti);
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
            return listaArticulos;
        }

        public string VerificarArticulo(string codigo, string nombre)
        {
            AccesoDatos datosCodigo = new AccesoDatos();
            AccesoDatos datosNombre = new AccesoDatos();
            try
            {
                datosCodigo.setearConsulta("SELECT COUNT(*) FROM ARTICULOS WHERE Codigo = @Codigo");
                datosCodigo.setearParametro("@Codigo", codigo);
                datosCodigo.ejecutarLectura();

                if (datosCodigo.Lector.Read())
                {
                    int count = (int)datosCodigo.Lector[0];
                    if (count > 0)
                    {
                        return "Ya existe un artículo con ese Código";
                    }
                }

                datosNombre.setearConsulta("SELECT COUNT(*) FROM ARTICULOS WHERE Nombre = @Nombre");
                datosNombre.setearParametro("@Nombre", nombre);
                datosNombre.ejecutarLectura();

                if (datosNombre.Lector.Read())
                {
                    int count = (int)datosNombre.Lector[0];
                    if (count > 0)
                    {
                        return "Ya existe un artículo con ese Nombre";
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
                datosCodigo.cerrarConexion();
                datosNombre.cerrarConexion();
            }
        }
        public string VerificarArticulo(string codigo, string nombre, long idArticulo)
        {
            AccesoDatos datosCodigo = new AccesoDatos();
            AccesoDatos datosNombre = new AccesoDatos();

            try
            {             
                datosCodigo.setearConsulta("SELECT COUNT(*) FROM ARTICULOS WHERE Codigo = @Codigo AND ID <> @IdArticulo");
                datosCodigo.setearParametro("@Codigo", codigo);
                datosCodigo.setearParametro("@IdArticulo", idArticulo);
                datosCodigo.ejecutarLectura();

                if (datosCodigo.Lector.Read())
                {
                    int count = Convert.ToInt32(datosCodigo.Lector[0]);
                    if (count > 0)
                    {
                        return "Ya existe un artículo con ese Código";
                    }
                }

                datosNombre.setearConsulta("SELECT COUNT(*) FROM ARTICULOS WHERE Nombre = @Nombre AND ID <> @IdArticulo");
                datosNombre.setearParametro("@Nombre", nombre);
                datosNombre.setearParametro("@IdArticulo", idArticulo);
                datosNombre.ejecutarLectura();

                if (datosNombre.Lector.Read())
                {
                    int count = Convert.ToInt32(datosNombre.Lector[0]);
                    if (count > 0)
                    {
                        return "Ya existe un artículo con ese Nombre";
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
                datosCodigo.cerrarConexion();
                datosNombre.cerrarConexion();
            }
        }

        public void Agregar(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Stock_Minimo, Imagen, Activo, Ganancia_Porcentaje) VALUES (@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @Stock_Minimo, @Imagen, @Activo, @Ganancia_Porcentaje)");

                datos.setearParametro("@Codigo", articulo.Codigo);
                datos.setearParametro("@Nombre", articulo.Nombre);
                datos.setearParametro("@Descripcion", articulo.Descripcion);
                datos.setearParametro("@IdMarca", articulo.Marca.ID);
                datos.setearParametro("@IdCategoria", articulo.Categoria.ID);
                datos.setearParametro("@Stock_Minimo", articulo.StockMin);
                datos.setearParametro("@Ganancia_Porcentaje", articulo.Ganancia);
                datos.setearParametro("@Imagen", !string.IsNullOrEmpty(articulo.Imagen) ? articulo.Imagen : "https://www.shutterstock.com/image-vector/default-ui-image-placeholder-wireframes-600nw-1037719192.jpg");
                datos.setearParametro("@Activo", articulo.Activo);

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

        public int SeleccionarUltimoRegistro()
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT TOP(1) Id FROM ARTICULOS ORDER BY Id DESC");
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return Convert.ToInt32(datos.Lector["Id"]);
                }
                else
                {
                    throw new Exception("No se pudo obtener el ID del último registro insertado.");
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

        public Articulo ObtenerArticuloPorID(long idArticulo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT * FROM ARTICULOS WHERE Id = @IdArticulo");
                datos.setearParametro("@IdArticulo", idArticulo);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Articulo arti = new Articulo();
                    arti.ID = long.Parse(datos.Lector["Id"].ToString());
                    arti.Codigo = datos.Lector["Codigo"].ToString();
                    arti.Nombre = datos.Lector["Nombre"].ToString();
                    arti.Descripcion = datos.Lector["Descripcion"].ToString();

                    arti.Marca = new Marca();
                    arti.Marca.ID = int.Parse(datos.Lector["IdMarca"].ToString());

                    arti.Categoria = new Categoria();
                    arti.Categoria.ID = int.Parse(datos.Lector["IdCategoria"].ToString());

                    arti.Ganancia = decimal.Parse(datos.Lector["Ganancia_Porcentaje"].ToString());
                    arti.Imagen = datos.Lector["Imagen"].ToString();
                    arti.StockMin = int.Parse(datos.Lector["Stock_Minimo"].ToString());
                    arti.Activo = bool.Parse(datos.Lector["Activo"].ToString());
                    return arti;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el artículo por ID", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Modificar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE ARTICULOS SET Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @IdMarca, IdCategoria = @IdCategoria, Ganancia_Porcentaje = @Ganancia_Porcentaje, Imagen = @Imagen, Stock_Minimo = @Stock_Minimo, Activo = @Activo WHERE Id = @Id");
                datos.setearParametro("@Codigo", nuevo.Codigo);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
                datos.setearParametro("@IdMarca", nuevo.Marca.ID);
                datos.setearParametro("@IdCategoria", nuevo.Categoria.ID);

                datos.setearParametro("@Ganancia_Porcentaje", nuevo.Ganancia);

                datos.setearParametro("@Stock_Minimo", nuevo.StockMin);
                datos.setearParametro("@Imagen", nuevo.Imagen);
                datos.setearParametro("@Activo", nuevo.Activo);
                datos.setearParametro("@Id", nuevo.ID);
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

        public void EliminarLogico(long Id, bool estado)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE ARTICULOS SET Activo = @activo WHERE Id = @Id");
                datos.setearParametro("@Id", Id);
                datos.setearParametro("@activo", estado);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { datos.cerrarConexion(); }
        }

        public void ReactivarModificar(Articulo arti)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE ARTICULOS SET Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @IdMarca, IdCategoria = @IdCategoria, Ganancia_Porcentaje = @Ganancia_Porcentaje, Stock_Minimo = @Stock_Minimo, Imagen = @Imagen, Activo = @Activo WHERE Id = @Id");
                
                datos.setearParametro("@Codigo", arti.Codigo);
                datos.setearParametro("@Nombre", arti.Nombre);
                datos.setearParametro("@Descripcion", arti.Descripcion);
                datos.setearParametro("@IdMarca", arti.Marca.ID);
                datos.setearParametro("@IdCategoria", arti.Categoria.ID);
                datos.setearParametro("@Ganancia_Porcentaje", arti.Ganancia);
                datos.setearParametro("@Stock_Minimo", arti.StockMin);
                datos.setearParametro("@Imagen", !string.IsNullOrEmpty(arti.Imagen) ? arti.Imagen : "https://www.shutterstock.com/image-vector/default-ui-image-placeholder-wireframes-600nw-1037719192.jpg");
                datos.setearParametro("@Activo", arti.Activo);
                datos.setearParametro("@Id", arti.ID);

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