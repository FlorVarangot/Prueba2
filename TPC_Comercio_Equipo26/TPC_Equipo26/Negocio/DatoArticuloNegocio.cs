using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPC_Equipo26.Dominio;

namespace TPC_Equipo26.Negocio
{
    public class DatoArticuloNegocio
    {
        public int ObtenerStockArticulo(long idArticulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT Stock FROM DATOS_ARTICULOS WHERE IdArticulo = @idArticulo AND Fecha = (SELECT MAX(Fecha) FROM DATOS_ARTICULOS WHERE IdArticulo = @idArticulo)");
                datos.setearParametro("@idArticulo", idArticulo);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    int stock = Convert.ToInt32(datos.Lector["Stock"]);
                    return stock;
                }
                else
                {
                    return 0;
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

        public decimal ObtenerPrecioArticulo(long idArticulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT Precio FROM DATOS_ARTICULOS WHERE IdArticulo = @idArticulo AND Fecha = (SELECT MAX(Fecha) FROM DATOS_ARTICULOS WHERE IdArticulo = @idArticulo)");
                datos.setearParametro("@idArticulo", idArticulo);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    int stock = Convert.ToInt32(datos.Lector["Precio"]);
                    return stock;
                }
                else
                {
                    return 0;
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
    
    }

}