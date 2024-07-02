using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
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

        //Trae ultimo precio
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
                    decimal precio = Convert.ToDecimal(datos.Lector["Precio"]);
                    return precio;
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

        //Trae el precio mas reciente a la fecha que recibe (fecha <=)
        public decimal ObtenerPrecioHistorico(long idArticulo, DateTime fechaVenta)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT TOP 1 Precio FROM DATOS_ARTICULOS WHERE IdArticulo = @idArticulo AND Fecha <= @fechaVenta ORDER BY Fecha DESC");
                datos.setearParametro("@idArticulo", idArticulo);
                datos.setearParametro("@fechaVenta", fechaVenta);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    decimal precio = Convert.ToInt32(datos.Lector["Precio"]);
                    return precio;
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

        public void ActualizarStock(Venta venta)
        {
            AccesoDatos datos = new AccesoDatos();
            DateTime fecha = venta.FechaVenta;

            foreach (DetalleVenta detalle in venta.Detalles)
            {
                long idArticulo = detalle.IdArticulo;
                int cantidad = detalle.Cantidad;

                datos.setearConsulta("SELECT TOP 1 Stock FROM DATOS_ARTICULOS WHERE IdArticulo = @IdArticulo ORDER BY Fecha DESC");
                datos.setearParametro("@IdArticulo", idArticulo);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    int ultimoStock = Convert.ToInt32(datos.Lector["Stock"]);
                    decimal ultimoPrecio = decimal.Parse(datos.Lector["Precio"].ToString());
                    int stock = ultimoStock - cantidad;

                    datos.setearConsulta("INSERT INTO DATOS_ARTICULOS (IdArticulo, Fecha, Stock, Precio) VALUES (@IdArticulo, @Fecha, @Stock, @Precio)");
                    datos.setearParametro("@IdArticulo", idArticulo);
                    datos.setearParametro("@Fecha", fecha);
                    datos.setearParametro("@Stock", stock);
                    datos.setearParametro("@Precio", ultimoPrecio);
                    datos.ejecutarAccion();
                }
            }
        }

        public void ActualizarStockCompra(Compra compra)
        {
            AccesoDatos datos = new AccesoDatos();
            DateTime fecha = compra.FechaCompra;

            foreach (DetalleCompra detalle in compra.Detalles)
            {
                long idArticulo = detalle.IdArticulo;
                int cantidad = detalle.Cantidad;
                decimal precioCompra = detalle.Precio;
                datos.setearConsulta("SELECT TOP 1 Stock, Precio FROM DATOS_ARTICULOS WHERE IdArticulo = @IdArticulo ORDER BY Fecha DESC");
                datos.setearParametro("@IdArticulo", idArticulo);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    int ultimoStock = Convert.ToInt32(datos.Lector["Stock"]);
                    decimal ultimoPrecio = Convert.ToDecimal(datos.Lector["Precio"]);

                    int nuevoStock = ultimoStock + cantidad;

                    ArticuloNegocio articuloNegocio = new ArticuloNegocio();
                    Articulo articulo = articuloNegocio.ObtenerArticuloPorID(idArticulo);
                    decimal ganancia = articulo.Ganancia;

                    decimal precioFinal = precioCompra + (precioCompra * ganancia / 100);

                    datos.setearConsulta("INSERT INTO DATOS_ARTICULOS (IdArticulo, Fecha, Stock, Precio) VALUES (@IdArticulo, @Fecha, @Stock, @Precio)");
                    datos.setearParametro("@IdArticulo", idArticulo);
                    datos.setearParametro("@Fecha", fecha);
                    datos.setearParametro("@Stock", nuevoStock);
                    datos.setearParametro("@Precio", precioFinal);
                    datos.ejecutarAccion();
                }
            }
        }
    }

}