﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPC_Equipo26.Dominio;

namespace TPC_Equipo26.Negocio
{
    public class DetalleVentaNegocio
    {
        public List<DetalleVenta> Listar(long id)
        {
            AccesoDatos datos = new AccesoDatos();
            List<DetalleVenta> detalles = new List<DetalleVenta>();
            try
            {
                datos.setearConsulta("SELECT * FROM DETALLE_VENTAS WHERE IdVenta = @IdVenta");
                datos.setearParametro("@IdVenta", id);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    DetalleVenta det = new DetalleVenta();
                    det.Id = long.Parse(datos.Lector["Id"].ToString());
                    det.IdVenta = id;
                    det.IdArticulo = long.Parse(datos.Lector["IdArticulo"].ToString());
                    det.Cantidad= int.Parse(datos.Lector["Cantidad"].ToString());

                    detalles.Add(det);
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

        public DetalleVenta ObtenerDetalleVentaPorId(long id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT * FROM DETALLE_VENTAS WHERE Id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    DetalleVenta detalle = new DetalleVenta();

                    detalle.IdVenta = long.Parse(datos.Lector["IdVenta"].ToString());
                    detalle.IdArticulo = long.Parse(datos.Lector["IdArticulo"].ToString());
                    detalle.Cantidad = int.Parse(datos.Lector["Cantidad"].ToString());
                    
                    return detalle;
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


    }
}