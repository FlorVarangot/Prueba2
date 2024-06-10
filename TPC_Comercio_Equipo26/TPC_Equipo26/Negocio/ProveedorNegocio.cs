using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using TPC_Equipo26.Dominio;

namespace TPC_Equipo26.Negocio
{
    public class ProveedorNegocio
    {
        public List<Proveedor> Listar()
        {
            List<Proveedor> lista = new List<Proveedor>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT * FROM PROVEEDORES");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Proveedor aux = new Proveedor();
                    aux.ID = int.Parse(datos.Lector["Id"].ToString());
                    aux.Nombre = datos.Lector["Nombre"].ToString();
                    aux.CUIT = datos.Lector["CUIT"].ToString();
                    aux.Telefono = datos.Lector["Telefono"].ToString();
                    aux.Email = datos.Lector["Email"].ToString();
                    aux.Direccion = datos.Lector["Direccion"].ToString();
                    aux.Activo = (bool)datos.Lector["Activo"];

                    lista.Add(aux);
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