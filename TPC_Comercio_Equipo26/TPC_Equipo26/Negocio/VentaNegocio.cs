using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPC_Equipo26.Dominio;

namespace TPC_Equipo26.Negocio
{
    public class VentaNegocio
    {
        public List<Venta> Listar()
        {
            List<Venta> lista = new List<Venta>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT * FROM VENTAS");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Venta aux = new Venta();
                    Cliente cli = new Cliente();
                    ClienteNegocio negocio = new ClienteNegocio();

                    aux.ID = long.Parse(datos.Lector["Id"].ToString());
                    aux.FechaVenta = DateTime.Parse(datos.Lector["FechaVenta"].ToString());
                    aux.IdCliente = long.Parse(datos.Lector["IdCliente"].ToString());
                    aux.Total = decimal.Parse(datos.Lector["TotalVenta"].ToString());

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        public Venta ObtenerVentaPorId(long id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT * FROM VENTAS WHERE Id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Venta venta = new Venta();

                    venta.FechaVenta = DateTime.Parse(datos.Lector["FechaVenta"].ToString());
                    venta.IdCliente = long.Parse(datos.Lector["IdCliente"].ToString());
                    venta.Total = decimal.Parse(datos.Lector["TotalVenta"].ToString());

                    //PENDIENTE:
                    //datos.Lector.Close();
                    //datos.setearConsulta("SELECT * FROM DETALLE_VENTAS WHERE IdVenta = @id");
                    //datos.setearParametro("@id", id);
                    //datos.ejecutarLectura();

                    //venta.Detalles = new List<DetalleVenta>();
                    ////venta.Detalles = ObtenerDetalles(id); >>
                    //while (datos.Lector.Read())
                    //{
                    //    DetalleVenta det = new DetalleVenta();

                    //    det.Id = long.Parse(datos.Lector["Id"].ToString());
                    //    det.IdVenta = id;
                    //    det.IdArticulo = long.Parse(datos.Lector["IdArticulo"].ToString());
                    //    det.Cantidad = int.Parse(datos.Lector["Cantidad"].ToString());
                    //    venta.Detalles.Add(det);
                    //}
                    return venta;
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
            finally { datos.cerrarConexion(); }

        }


        public void AgregarVenta(Venta venta)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO VENTAS (FechaVenta, IdCliente, TotalVenta) VALUES (@FechaVenta, @IdCliente, @TotalVenta)");
                datos.setearParametro("@FechaVenta", venta.FechaVenta);
                datos.setearParametro("@IdCliente", venta.IdCliente);
                datos.setearParametro("@TotalVenta", venta.Total);

                datos.ejecutarAccion();

                long idVenta = TraerUltimoId();

                foreach (var detalle in venta.Detalles)
                {
                    using (AccesoDatos datosDetalle = new AccesoDatos())
                    {
                        datosDetalle.setearConsulta("INSERT INTO DETALLE_VENTAS (IdVenta, IdArticulo, Cantidad) VALUES (@IdVenta, @IdArticulo, @Cantidad)");
                        datosDetalle.setearParametro("@IdVenta", idVenta);
                        datosDetalle.setearParametro("@IdArticulo", detalle.IdArticulo);
                        datosDetalle.setearParametro("@Cantidad", detalle.Cantidad);

                        datosDetalle.ejecutarAccion();
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


        public long TraerUltimoId()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                //Cambiar por SCOPEIDENTITY()
                datos.setearConsulta("SELECT MAX(Id) AS 'UltimoId' FROM VENTAS");
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return Convert.ToInt64(datos.Lector["UltimoId"]);
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