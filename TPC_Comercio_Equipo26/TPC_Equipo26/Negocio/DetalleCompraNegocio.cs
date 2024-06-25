using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPC_Equipo26.Dominio;

namespace TPC_Equipo26.Negocio
{
    public class DetalleCompraNegocio
    {
        public List<DetalleCompra> ListarDetalleCompra(long idCompra)
        {
            List<DetalleCompra> detalles = new List<DetalleCompra>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT Id, IdArticulo, Precio, Cantidad FROM DETALLE_COMPRAS WHERE IdCompra = @IdCompra");
                datos.setearParametro("@IdCompra", idCompra);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    DetalleCompra detalle = new DetalleCompra();
                    detalle.Id = (long)datos.Lector["Id"];
                    detalle.IdArticulo = (long)datos.Lector["IdArticulo"];
                    detalle.Precio = (decimal)datos.Lector["Precio"];
                    detalle.Cantidad = (int)datos.Lector["Cantidad"];

                    detalles.Add(detalle);
                }

                return detalles;
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