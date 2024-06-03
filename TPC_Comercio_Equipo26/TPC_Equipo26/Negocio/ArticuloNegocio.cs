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
                datos.setearConsulta("SELECT * FROM ARTICULOS;");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo arti= new Articulo();
                    {
                        arti.ID = datos.Lector.GetInt32(0);
                        arti.Codigo = datos.Lector["Codigo"].ToString();
                        arti.Nombre = datos.Lector["Nombre"].ToString();
                        arti.Descripcion = datos.Lector["Descripcion"].ToString();
                        //arti.Marca = new Marca { Descripcion = datos.Lector["Marca"].ToString};
                        //arti.Categoria = new Categoria { Descripcion = datos.Lector["Categoria"].ToString};
                        arti.Precio = (float)datos.Lector.GetDecimal(4);
                        arti.Activo = (bool)datos.Lector["Activo"];
                    };

                    //string urlImagen = datos.Lector["Imagen"] as string;
                    //Imagen imagen = new Imagen { UrlImagen = urlImagen };
                    //arti.Imagenes.Add(imagen);
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
        ///prueba
    }
}