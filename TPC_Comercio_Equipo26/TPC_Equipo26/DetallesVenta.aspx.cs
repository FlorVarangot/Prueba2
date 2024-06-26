﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPC_Equipo26.Dominio;
using TPC_Equipo26.Negocio;

namespace TPC_Equipo26
{
    public partial class DetallesVenta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["ID"] != null)
                {
                    lblTitulo.Text = "Detalles: ";
                    long ventaID = Convert.ToInt64(Request.QueryString["ID"]);
                    lblVentaID.Text = "venta n° 00" + ventaID;
                    lblCliente.Text = "Cliente: " + TraerNombreCliente(ventaID);
                    lblFecha.Text = "Fecha: " + TraerFechaVenta(ventaID).ToString("dd/MM/yyyy");
                    lblTotal.Text = "Total: $ " + TraerTotalVenta(ventaID).ToString("N2");
                    CargarDetallesVenta(ventaID);
                }
                else
                {
                    Response.Redirect("Ventas.aspx");
                }

            }
        }

        private void CargarDetallesVenta(long id)
        {
            DetalleVentaNegocio negocio = new DetalleVentaNegocio();
            List<DetalleVenta> detalles = negocio.Listar(id);

            gvDetalle.DataSource = detalles;
            gvDetalle.DataBind();
        }

        protected void gvDetalle_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDetalle.PageIndex = e.NewPageIndex;
        }

        protected void gvDetalle_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = gvDetalle.SelectedDataKey.Value.ToString();
            Response.Redirect("DetallesVenta.aspx?ID=" + id);

        }

        protected void gvDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DatoArticuloNegocio datoArticuloNegocio = new DatoArticuloNegocio();
                ArticuloNegocio negocio = new ArticuloNegocio();
                long idArt = Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "IdArticulo"));
                Articulo articulo = negocio.ObtenerArticuloPorID(idArt);
                string nombreArticulo = articulo.Descripcion;
                long idVenta = long.Parse(Request.QueryString["ID"]);
                DateTime fechaVenta = TraerFechaVenta(idVenta);
                int cantidad = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Cantidad"));
                decimal ganancia = articulo.Ganancia;

                decimal precioUnitario = datoArticuloNegocio.ObtenerPrecioHistorico(idArt, fechaVenta);
                decimal precioConGanancia = precioUnitario + (precioUnitario * ganancia / 100);
                decimal totalParcial = cantidad * precioConGanancia;

                e.Row.Cells[0].Text = nombreArticulo;
                e.Row.Cells[1].Text = precioConGanancia.ToString("C");
                e.Row.Cells[3].Text = totalParcial.ToString("C");
            }
        }

        //F: Revisar optimización de este metodo: se repite tal cual en Venta y en DetallesVenta.
        private string TraerNombreCliente(long id)
        {
            try
            {
                VentaNegocio ventaNegocio = new VentaNegocio();
                Venta venta = ventaNegocio.ObtenerVentaPorId(id);
                string apellidoNombre = "cliente";

                if (venta != null)
                {
                    long idCliente = venta.IdCliente;
                    ClienteNegocio clienteNegocio = new ClienteNegocio();
                    Cliente cliente = clienteNegocio.ObtenerClientePorId(idCliente);

                    if (cliente != null)
                    {
                        apellidoNombre = $"{cliente.Apellido}, {cliente.Nombre}";
                        return apellidoNombre;
                    }
                }
                return apellidoNombre;
            }
            catch (Exception)
            {
                return "Error al obtener el nombre del cliente";
            }
        }

        private DateTime TraerFechaVenta(long id)
        {
            try
            {
                VentaNegocio ventaNegocio = new VentaNegocio();
                Venta venta = ventaNegocio.ObtenerVentaPorId(id);
                DateTime fechaVenta;

                if (venta != null)
                {
                    fechaVenta = venta.FechaVenta;
                }
                else
                {
                    fechaVenta = new DateTime(1900, 1, 1);
                }

                return fechaVenta;
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }

        private decimal TraerTotalVenta(long id)
        {
            try
            {
                VentaNegocio ventaNegocio = new VentaNegocio();
                Venta venta = ventaNegocio.ObtenerVentaPorId(id);
                decimal totalVenta = 0;

                if (venta != null)
                {
                    totalVenta = venta.Total;
                }
                else
                {
                    totalVenta = 0;
                }

                return totalVenta;
            }
            catch (Exception)
            {
                return 0;
            }

        }

    }
}