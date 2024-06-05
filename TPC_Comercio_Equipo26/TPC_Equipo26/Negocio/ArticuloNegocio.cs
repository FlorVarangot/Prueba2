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
                        arti.Activo = (bool)datos.Lector["Activo"];
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
                            IdArticulo = datosImagenes.Lector.GetInt32(1),
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

    }
}