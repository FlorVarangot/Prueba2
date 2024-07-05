using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPC_Equipo26.Dominio;

namespace TPC_Equipo26.Negocio
{
    public class CompraNegocio
    {
        public void AgregarCompra(Compra compra)
        {
            AccesoDatos datosCompra = new AccesoDatos();

            try
            {
                datosCompra.setearConsulta("INSERT INTO COMPRAS (FechaCompra, IdProveedor, TotalCompra) VALUES (@FechaCompra, @IdProveedor, @TotalCompra)");
                datosCompra.setearParametro("@FechaCompra", compra.FechaCompra);
                datosCompra.setearParametro("@IdProveedor", compra.IdProveedor);
                datosCompra.setearParametro("@TotalCompra", compra.TotalCompra);

                datosCompra.ejecutarAccion();

                long idCompra = ObtenerUltimoIdCompra();

                foreach (var detalle in compra.Detalles)
                {
                    //limpia al salir del bloque y optimiza la memoria
                    using (AccesoDatos datosDetalle = new AccesoDatos())
                    {
                        datosDetalle.setearConsulta("INSERT INTO DETALLE_COMPRAS (IdCompra, IdArticulo, Precio, Cantidad) VALUES (@IdCompra, @IdArticulo, @Precio, @Cantidad)");
                        datosDetalle.setearParametro("@IdCompra", idCompra);
                        datosDetalle.setearParametro("@IdArticulo", detalle.IdArticulo);
                        datosDetalle.setearParametro("@Precio", detalle.Precio);
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
                datosCompra.cerrarConexion();
            }
        }

        public long ObtenerUltimoIdCompra()
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT TOP(1) Id FROM COMPRAS ORDER BY Id DESC");
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return Convert.ToInt64(datos.Lector["Id"]);
                }
                else
                {
                    throw new Exception("No se pudo obtener el ID del último registro insertado en COMPRAS.");
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

        public List<Compra> Listar()
        {
            List<Compra> lista = new List<Compra>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT Id, FechaCompra, IdProveedor, TotalCompra FROM COMPRAS");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Compra compra = new Compra();
                    compra.ID = (long)datos.Lector["Id"];
                    compra.FechaCompra = (DateTime)datos.Lector["FechaCompra"];
                    compra.IdProveedor = (int)datos.Lector["IdProveedor"];
                    compra.TotalCompra = (decimal)datos.Lector["TotalCompra"];

                    lista.Add(compra);
                }

                return lista;
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
                datos.setearConsulta("SELECT MAX(Id) AS 'UltimoId' FROM COMPRAS");
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