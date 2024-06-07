using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using TPC_Equipo26.Dominio;


namespace TPC_Equipo26.Negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> ListarConImagenes()
        {
            List<Articulo> listaArticulos = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, M.Descripcion AS Marca, C.Descripcion AS Categoria, A.PrecioVenta, A.Stock, A.Stock_Minimo, A.Activo FROM ARTICULOS A INNER JOIN MARCAS M ON M.Id = A.IdMarca INNER JOIN CATEGORIAS C ON C.Id = A.IdCategoria");
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
                        arti.Precio = datos.Lector.GetDecimal(6);
                        arti.Stock = datos.Lector.GetInt32(7);
                        arti.StockMin = datos.Lector.GetInt32(8);
                        arti.Activo = bool.Parse(datos.Lector["Activo"].ToString());
                    };

                    AccesoDatos datosImagenes = new AccesoDatos();
                    datosImagenes.setearConsulta("SELECT * FROM IMAGENES WHERE IdArticulo = @idArticulo");
                    datosImagenes.setearParametro("@idArticulo", arti.ID);
                    datosImagenes.ejecutarLectura();

                    arti.Imagenes = new List<Imagen>();
                    while (datosImagenes.Lector.Read())
                    {
                        Imagen imagen = new Imagen
                        {
                            ID = datosImagenes.Lector.GetInt64(0),
                            IdArticulo = datosImagenes.Lector.GetInt64(1),
                            UrlImagen = datosImagenes.Lector["ImagenUrl"].ToString(),
                            Activo = (bool)datosImagenes.Lector["Activo"]
                        };
                        arti.Imagenes.Add(imagen);
                    }
                    datosImagenes.cerrarConexion();

                    if (arti.Imagenes.Count == 0)
                    {
                        Imagen imagenPorDefecto = new Imagen
                        {
                            UrlImagen = "https://www.shutterstock.com/image-vector/default-ui-image-placeholder-wireframes-600nw-1037719192.jpg"
                        };
                        arti.Imagenes.Add(imagenPorDefecto);
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

        public void agregar(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();
            AccesoDatos datosImagenes = new AccesoDatos();

            try
            {     
                datos.setearConsulta("INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, PrecioVenta, Stock, Stock_Minimo, Activo, Ganancia_Porcentaje) " +
                                     "VALUES (@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @PrecioVenta, @Stock, @Stock_Minimo, @Activo, @Ganancia_Porcentaje)");

                datos.setearParametro("@Codigo", articulo.Codigo);
                datos.setearParametro("@Nombre", articulo.Nombre);
                datos.setearParametro("@Descripcion", articulo.Descripcion);
                datos.setearParametro("@IdMarca", articulo.Marca.ID);
                datos.setearParametro("@IdCategoria", articulo.Categoria.ID);
                datos.setearParametro("@PrecioVenta", articulo.Precio);
                datos.setearParametro("@Stock", articulo.Stock);
                datos.setearParametro("@Stock_Minimo", articulo.StockMin);
                datos.setearParametro("@Activo", articulo.Activo);
                datos.setearParametro("@Ganancia_Porcentaje", 0); 

                datos.ejecutarAccion();
                int idArticulo = seleccionoUltimoRegistro();
                foreach (Imagen imagen in articulo.Imagenes)
                {
                    datosImagenes.setearConsulta("INSERT INTO IMAGENES (IdArticulo, ImagenUrl, Activo) " +
                                         "VALUES (@IdArticulo, @ImagenUrl, @ActivoImagen)");
                    datosImagenes.setearParametro("@IdArticulo", idArticulo);
                    datosImagenes.setearParametro("@ImagenUrl", imagen.UrlImagen);
                    datosImagenes.setearParametro("@ActivoImagen", imagen.Activo);
                    datosImagenes.ejecutarAccion();
                }

            }
            catch (Exception ex)
            {
                throw ex;             
            }
            finally
            {
                datos.cerrarConexion();
                datosImagenes.cerrarConexion();
            }
        }

        public int seleccionoUltimoRegistro()
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select top(1) Id from ARTICULOS order by Id desc");
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
            Imagen image = new Imagen();

            try
            {
                string consulta = "SELECT * FROM ARTICULOS WHERE Id = @IdArticulo";
                datos.setearConsulta(consulta);
                datos.setearParametro("@IdArticulo", idArticulo);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Articulo arti = new Articulo();
                    arti.ID = Convert.ToInt64(datos.Lector["Id"]);
                    arti.Codigo = datos.Lector["Codigo"].ToString();
                    arti.Nombre = datos.Lector["Nombre"].ToString();
                    arti.Descripcion = datos.Lector["Descripcion"].ToString();

                    arti.Marca = new Marca();
                    arti.Marca.ID = Convert.ToInt32(datos.Lector["IdMarca"]);

                    arti.Categoria = new Categoria();
                    arti.Categoria.ID = Convert.ToInt32(datos.Lector["IdCategoria"]);

                    arti.Precio = Convert.ToDecimal(datos.Lector["PrecioVenta"]);
                    arti.Stock = Convert.ToInt32(datos.Lector["Stock"]);
                    arti.StockMin = Convert.ToInt32(datos.Lector["Stock_Minimo"]);

                    return arti;
                }
                else
                {
                    return null;
                }

            } catch (Exception ex)
            {
                throw new Exception("Error al obtener el artículo por ID", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Imagen> ObtenerImagenesPorID(long idArticulo)
        {
            List<Imagen> imagenes = new List<Imagen>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT ImagenUrl FROM Imagenes WHERE IdArticulo = @IdArticulo");
                datos.setearParametro("@IdArticulo", idArticulo);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Imagen imagen = new Imagen
                    {
                        UrlImagen = (string)datos.Lector["ImagenUrl"]
                    };
                    imagenes.Add(imagen);
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

            return imagenes;
        }

        public void EliminarLogico(int id)
            {
                //PENDIENTE
            }

        public void modificar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            AccesoDatos datosImagenes = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE ARTICULOS SET Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion,IdMarca = @IdMarca, IdCategoria = @IdCategoria, PrecioVenta = @PrecioVenta, Stock = @Stock, Stock_Minimo = @Stock_Minimo, Activo = @Activo, Ganancia_Porcentaje = @Ganancia_Porcentaje WHERE Id = @Id");
                datos.setearParametro("@Codigo", nuevo.Codigo);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Descripcion",nuevo.Descripcion);
                datos.setearParametro("@IdMarca", nuevo.Marca.ID);
                datos.setearParametro("@IdCategoria", nuevo.Categoria.ID);
                datos.setearParametro("@PrecioVenta", nuevo.Precio);
                datos.setearParametro("@Stock", nuevo.Stock);
                datos.setearParametro("@Stock_Minimo", nuevo.StockMin);
                datos.setearParametro("@Activo", nuevo.Activo);
                datos.setearParametro("@Ganancia_Porcentaje", 0);
                datos.setearParametro("@Id", nuevo.ID);

                datos.ejecutarAccion();

                datosImagenes.setearConsulta("DELETE FROM IMAGENES WHERE IdArticulo = @IdArticulo");
                datosImagenes.setearParametro("@IdArticulo",nuevo.ID);
                datosImagenes.ejecutarAccion();

                foreach (Imagen imagen in nuevo.Imagenes)
                {
                    datosImagenes.setearConsulta("INSERT INTO IMAGENES (IdArticulo, ImagenUrl, Activo) " +
                                         "VALUES (@IdArticulo, @ImagenUrl, @ActivoImagen)");
                    datosImagenes.setearParametro("@IdArticulo",nuevo.ID);
                    datosImagenes.setearParametro("@ImagenUrl", imagen.UrlImagen);
                    datosImagenes.setearParametro("@ActivoImagen", imagen.Activo);
                    datosImagenes.ejecutarAccion();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
                datosImagenes.cerrarConexion();
            }
        }
    }
}