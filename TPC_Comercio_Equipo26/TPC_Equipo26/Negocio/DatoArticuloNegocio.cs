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
            DateTime fecha = venta.FechaVenta;
            try
            {
                foreach (DetalleVenta detalle in venta.Detalles)
                {
                    long idArticulo = detalle.IdArticulo;
                    int cantidad = detalle.Cantidad;
                    int ultimoStock = 0;
                    decimal ultimoPrecio = 0;

                    using (AccesoDatos datos = new AccesoDatos())
                    {
                        datos.setearConsulta("SELECT TOP 1 Stock, Precio FROM DATOS_ARTICULOS WHERE IdArticulo = @IdArticulo ORDER BY Fecha DESC");
                        datos.setearParametro("@IdArticulo", idArticulo);
                        datos.ejecutarLectura();

                        if (datos.Lector.Read())
                        {
                            ultimoStock = Convert.ToInt32(datos.Lector["Stock"]);
                            ultimoPrecio = decimal.Parse(datos.Lector["Precio"].ToString());
                        }
                    }

                    if (ultimoStock > 0)
                    {
                        int nuevoStock = ultimoStock - cantidad;

                        int count = 0;
                        using (AccesoDatos datosArticulo = new AccesoDatos())
                        {
                            datosArticulo.setearConsulta("SELECT COUNT(*) FROM DATOS_ARTICULOS WHERE IdArticulo = @IdArticulo AND Fecha = @Fecha");
                            datosArticulo.setearParametro("@IdArticulo", idArticulo);
                            datosArticulo.setearParametro("@Fecha", fecha);
                            datosArticulo.ejecutarLectura();

                            if (datosArticulo.Lector.Read())
                            {
                                count = Convert.ToInt32(datosArticulo.Lector[0]);
                            }
                        }

                       
                        using (AccesoDatos datosArticulo = new AccesoDatos())
                        {
                            if (count > 0)
                            {
                                
                                datosArticulo.setearConsulta("UPDATE DATOS_ARTICULOS SET Stock = @Stock WHERE IdArticulo = @IdArticulo AND Fecha = @Fecha");
                            }
                            else
                            {
                                 
                                datosArticulo.setearConsulta("INSERT INTO DATOS_ARTICULOS (IdArticulo, Fecha, Stock, Precio) VALUES (@IdArticulo, @Fecha, @Stock, @Precio)");
                                datosArticulo.setearParametro("@Precio", ultimoPrecio); 
                            }

                            datosArticulo.setearParametro("@IdArticulo", idArticulo);
                            datosArticulo.setearParametro("@Fecha", fecha);
                            datosArticulo.setearParametro("@Stock", nuevoStock);
                            datosArticulo.ejecutarAccion();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void ActualizarStockPostCompra(Compra compra)
        {
            AccesoDatos datos = new AccesoDatos();
            DateTime fecha = compra.FechaCompra;

            try
            {
                foreach (DetalleCompra detalle in compra.Detalles)
                {
                    long idArticulo = detalle.IdArticulo;
                    int cantidad = detalle.Cantidad;
                    decimal precioCompra = detalle.Precio;

                    int ultimoStock = 0;

                    using (AccesoDatos datosArticulo = new AccesoDatos())
                    {
                        datosArticulo.setearConsulta("SELECT TOP 1 Stock FROM DATOS_ARTICULOS WHERE IdArticulo = @IdArticulo ORDER BY Fecha DESC");
                        datosArticulo.setearParametro("@IdArticulo", idArticulo);
                        datosArticulo.ejecutarLectura();

                        if (datosArticulo.Lector.Read())
                        {
                            ultimoStock = Convert.ToInt32(datosArticulo.Lector["Stock"]);                     
                        }
                    }

                    ArticuloNegocio articuloNegocio = new ArticuloNegocio();
                    Articulo articulo = articuloNegocio.ObtenerArticuloPorID(idArticulo);
                    decimal ganancia = articulo.Ganancia;
                    decimal precioFinal = precioCompra + (precioCompra * ganancia / 100);

                    int nuevoStock = ultimoStock + cantidad;

                    using (AccesoDatos datosArticulo = new AccesoDatos())
                    {
                        if (ultimoStock == 0)
                        {
                            datosArticulo.setearConsulta("INSERT INTO DATOS_ARTICULOS (IdArticulo, Fecha, Stock, Precio) VALUES (@IdArticulo, @Fecha, @Stock, @Precio)");
                        }
                        else
                        {
                            datosArticulo.setearConsulta("UPDATE DATOS_ARTICULOS SET Stock = @Stock, Precio = @Precio WHERE IdArticulo = @IdArticulo AND Fecha = @Fecha");
                        }
                        datosArticulo.setearParametro("@IdArticulo", idArticulo);
                        datosArticulo.setearParametro("@Fecha", fecha);
                        datosArticulo.setearParametro("@Stock", nuevoStock);
                        datosArticulo.setearParametro("@Precio", precioFinal);
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