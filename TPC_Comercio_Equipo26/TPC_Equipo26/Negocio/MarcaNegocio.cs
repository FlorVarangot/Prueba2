using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using TPC_Equipo26.Dominio;
using static System.Net.WebRequestMethods;

namespace TPC_Equipo26.Negocio
{
    public class MarcaNegocio
    {
        public List<Marca> Listar()
        {
            List<Marca> lista = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT * FROM MARCAS");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Marca aux = new Marca();
                    aux.ID = int.Parse(datos.Lector["Id"].ToString());
                    aux.Descripcion = datos.Lector["Descripcion"].ToString();
                    aux.IdProveedor = int.Parse(datos.Lector["IdProveedor"].ToString());
                    //F: agrego imagen por defecto si no tiene img:
                    aux.ImagenUrl = datos.Lector["ImagenUrl"] != DBNull.Value ? (string)datos.Lector["ImagenUrl"] : "https://www.shutterstock.com/image-vector/default-ui-image-placeholder-wireframes-600nw-1037719192.jpg";
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

        //public datatable listar2()
        //{
        //    accesodatos datos = new accesodatos();
        //    datatable dt = new datatable();

        //    try
        //    {
        //        datos.setearconsulta("select * from vistamarcasconproveedores");
        //        datos.ejecutarlectura();

        //        dt.load(datos.lector);
        //        return dt;
        //    }
        //    catch (exception ex)
        //    {

        //        throw ex;
        //    }
        //    finally
        //    {
        //        datos.cerrarconexion();
        //    }
        //}

        //public list<dynamic> listarconproveedor()
        //{
        //    list<dynamic> lista = new list<dynamic>();
        //    accesodatos datos = new accesodatos();

        //    datos.setearconsulta("select m.id, m.descripcion, p.nombre as proveedor, m.imagenurl, m.activo from marcas m inner join proveedores p on m.idproveedor = p.id");
        //    datos.ejecutarlectura();
        //    try
        //    {
        //        while (datos.lector.read())
        //        {
        //            var marcaconproveedor = new
        //            {
        //                id = int.parse(datos.lector["id"].tostring()),
        //                descripcion = datos.lector["descripcion"].tostring(),
        //                proveedor = datos.lector["proveedor"].tostring(),
        //                imagenurl = datos.lector["imagenurl"].tostring(),
        //                activo = (bool)datos.lector["activo"],
        //            };
        //            lista.add(marcaconproveedor);
        //        }
        //        return lista;
        //    }
        //    catch (exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        datos.cerrarconexion();
        //    }
        //}

        public void Agregar(Marca marca)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO MARCAS VALUES (@Descripcion, @IdProveedor, @ImagenUrl, @Activo)");
                datos.setearParametro("@Descripcion", marca.Descripcion);
                datos.setearParametro("@IdProveedor", marca.IdProveedor);
                datos.setearParametro("@ImagenUrl", marca.ImagenUrl);
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

        public void Modificar(Marca marca)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE MARCAS SET Descripcion = @Descripcion, IdProveedor = @IdProveedor, ImagenUrl = @ImagenUrl, Activo = @Activo WHERE Id = @Id");

                datos.setearParametro("@Descripcion", marca.Descripcion);
                datos.setearParametro("@IdProveedor", marca.IdProveedor);
                datos.setearParametro("@ImagenUrl", marca.ImagenUrl);
                datos.setearParametro("@Activo", marca.Activo);
                datos.setearParametro("@Id", marca.ID);

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
                datos.setearConsulta("UPDATE MARCAS SET Activo = @activo WHERE Id = @id");
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

        public Marca ObtenerMarcaPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT * FROM MARCAS WHERE Id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Marca marca = new Marca();
                    marca.ID = int.Parse(datos.Lector["Id"].ToString());
                    marca.Descripcion = datos.Lector["Descripcion"].ToString();
                    marca.IdProveedor = int.Parse(datos.Lector["IdProveedor"].ToString());
                    marca.ImagenUrl = datos.Lector["ImagenUrl"].ToString();
                    marca.Activo = (bool)datos.Lector["Activo"];

                    return marca;
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