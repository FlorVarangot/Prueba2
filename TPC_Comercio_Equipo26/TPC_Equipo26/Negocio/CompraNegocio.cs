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
            AccesoDatos datosDetalle = new AccesoDatos();

            try
            {
                datosCompra.setearConsulta("INSERT INTO COMPRAS (FechaCompra, IdProveedor, TotalCompra) VALUES (@FechaCompra, @IdProveedor, @TotalCompra); SELECT SCOPE_IDENTITY();");
                datosCompra.setearParametro("@FechaCompra", compra.FechaCompra);
                datosCompra.setearParametro("@IdProveedor", compra.IdProveedor);
                datosCompra.setearParametro("@TotalCompra", compra.TotalCompra);
                long idCompra = datosCompra.ejecutarAccionScalar();

                foreach (var detalle in compra.Detalles)
                {
                    datosDetalle.setearConsulta("INSERT INTO DETALLE_COMPRAS (IdCompra, IdArticulo, Precio, Cantidad) VALUES (@IdCompra, @IdArticulo, @Precio, @Cantidad)");
                    datosDetalle.setearParametro("@IdCompra", idCompra);
                    datosDetalle.setearParametro("@IdArticulo", detalle.IdArticulo);
                    datosDetalle.setearParametro("@Precio", detalle.Precio);
                    datosDetalle.setearParametro("@Cantidad", detalle.Cantidad);
                    datosDetalle.ejecutarAccion(); 
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datosCompra.cerrarConexion();
                datosDetalle.cerrarConexion();
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

        
    }
}