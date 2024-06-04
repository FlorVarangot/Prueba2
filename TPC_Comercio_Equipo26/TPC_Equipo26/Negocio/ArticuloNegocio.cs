using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPC_Equipo26.Dominio;


namespace TPC_Equipo26.Negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> ListarArticulosConImagenes()
        {
            List<Articulo> listaArticulos = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT A.*, M.Descripcion AS Marca, C.Descripcion AS Categoria FROM ARTICULOS A " +
                             "LEFT JOIN MARCAS M ON A.IdMarca = M.Id " +
                             "LEFT JOIN CATEGORIAS C ON A.IdCategoria = C.Id;");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo arti= new Articulo();
                    {
                        arti.ID = datos.Lector.GetInt64(0);
                        arti.Codigo = datos.Lector["Codigo"].ToString();
                        arti.Nombre = datos.Lector["Nombre"].ToString();
                        arti.Descripcion = datos.Lector["Descripcion"].ToString();
                        arti.Marca = new Marca { Descripcion = datos.Lector["Marca"].ToString()};
                        arti.Categoria = new Categoria { Descripcion = datos.Lector["Categoria"].ToString()};
                        
                        //string urlImagen = datos.Lector["Imagen"] as string;
                        //Imagen imagen = new Imagen { UrlImagen = urlImagen };
                        //arti.Imagenes.Add(imagen);
                        //listaArticulos.Add(arti);


                        arti.Precio = (float)datos.Lector.GetDecimal(7);
                        arti.Stock = datos.Lector.GetInt32(8);
                        arti.StockMin = datos.Lector.GetInt32(9);
                        arti.Activo = (bool)datos.Lector["Activo"];
                    };

                    
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