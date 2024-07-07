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

        public void ActualizarStockPostVenta(Venta venta)
        {
            AccesoDatos datos = new AccesoDatos();
            DateTime fecha = venta.FechaVenta;
            try
            {
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

                        using (AccesoDatos datosArticulo = new AccesoDatos())
                        {
                            datosArticulo.setearConsulta("INSERT INTO DATOS_ARTICULOS (IdArticulo, Fecha, Stock, Precio) VALUES (@IdArticulo, @Fecha, @Stock, @Precio)");
                            datosArticulo.setearParametro("@IdArticulo", idArticulo);
                            datosArticulo.setearParametro("@Fecha", fecha);
                            datosArticulo.setearParametro("@Stock", stock);
                            datosArticulo.setearParametro("@Precio", ultimoPrecio);
                            datosArticulo.ejecutarAccion();
                        }
                    }
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

        public void ActualizarStockPostCompra(Compra compra)
        {
            AccesoDatos datos = new AccesoDatos();
            DateTime fecha = compra.FechaCompra;

            try
            {
                datos.setearConsulta("SELECT IdArticulo, Cantidad, Precio FROM Detalle_Compras WHERE IdCompra = @IdCompra");
                datos.setearParametro("@IdCompra", compra.ID);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    long idArticulo = Convert.ToInt64(datos.Lector["IdArticulo"]);
                    int cantidad = Convert.ToInt32(datos.Lector["Cantidad"]);
                    decimal precioCompra = Convert.ToDecimal(datos.Lector["Precio"]);

                    ArticuloNegocio articuloNegocio = new ArticuloNegocio();
                    Articulo articulo = articuloNegocio.ObtenerArticuloPorID(idArticulo);
                    decimal ganancia = articulo.Ganancia;
                    decimal precioFinal = precioCompra + (precioCompra * ganancia / 100);
                    int stockActual = ObtenerStockArticulo(idArticulo);

                    int nuevoStock = stockActual + cantidad;

                    using (AccesoDatos datosArticulo = new AccesoDatos())
                    {
                        datosArticulo.setearConsulta("UPDATE DATOS_ARTICULOS SET Stock = @Stock, Precio = @Precio WHERE IdArticulo = @IdArticulo AND Fecha = @Fecha");
                        datosArticulo.setearParametro("@IdArticulo", idArticulo);
                        datosArticulo.setearParametro("@Stock", nuevoStock);
                        datosArticulo.setearParametro("@Precio", precioFinal);
                        datosArticulo.setearParametro("@Fecha", fecha);
                        datosArticulo.ejecutarAccion();
                    }
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