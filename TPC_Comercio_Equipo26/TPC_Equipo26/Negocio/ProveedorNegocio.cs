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

        public void Agregar(Proveedor proveedor)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO PROVEEDORES VALUES (@Nombre, @CUIT, @Email, @Telefono, @Direccion, @Activo)");
                
                datos.setearParametro("@Nombre", proveedor.Nombre);
                datos.setearParametro("@CUIT", proveedor.CUIT);
                datos.setearParametro("@Email", proveedor.Email);
                datos.setearParametro("@Telefono", proveedor.Telefono);
                datos.setearParametro("@Direccion", proveedor.Direccion);
                datos.setearParametro("@Activo", 1);

                datos.ejecutarAccion();
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

        public void Modificar(Proveedor proveedor)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE PROVEEDORES SET Nombre = @Nombre, CUIT = @CUIT, Email = @Email, Telefono = @Telefono, Direccion = @Direccion, Activo = @Activo WHERE Id = @Id");
                
                datos.setearParametro("@Nombre", proveedor.Nombre);
                datos.setearParametro("@CUIT", proveedor.CUIT);
                datos.setearParametro("@Email", proveedor.Email);
                datos.setearParametro("@Telefono", proveedor.Telefono);
                datos.setearParametro("@Direccion", proveedor.Direccion);
                datos.setearParametro("@Activo", proveedor.Activo);

                datos.ejecutarAccion();
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

        internal void ActivarLogico(int id, bool activo = false)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE PROVEEDORES SET Activo = @activo WHERE Id = @id");
                datos.setearParametro("@id", id);
                datos.setearParametro("@activo", activo);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { datos.cerrarConexion(); }
        }

        public Proveedor ObtenerProveedorPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT * FROM PROVEEDORES WHERE Id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Proveedor prov= new Proveedor();
                    prov.ID = int.Parse(datos.Lector["Id"].ToString());
                    prov.Nombre= datos.Lector["Nombre"].ToString();
                    prov.CUIT= datos.Lector["CUIT"].ToString();
                    prov.Email= datos.Lector["Email"].ToString();
                    prov.Telefono= datos.Lector["Telefono"].ToString();
                    prov.Direccion= datos.Lector["Direccion"].ToString();
                    prov.Activo= (bool)datos.Lector["Activo"];

                    return prov;
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