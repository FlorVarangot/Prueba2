using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPC_Equipo26.Dominio;
using TPC_Equipo26.Negocio;

namespace TPC_Equipo26
{
    public partial class Ventas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    CargarVentas();
                }
            }
            catch
            {
                Response.Redirect("Error.aspx");
            }
        }

        protected void CargarVentas()
        {
            try
            {

                VentaNegocio negocio = new VentaNegocio();
                List<Venta> ventas = negocio.Listar();
                CargarClientes();

                Session["listaVentas"] = ventas;
                FiltrarVentas();
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx");
            }
        }

        protected void CargarClientes()
        {
            try
            {
                ClienteNegocio negocio = new ClienteNegocio();
                List<Cliente> clientes = negocio.Listar();

                var apellidoNombre = clientes.Select(cli => new
                {
                    ID = cli.ID,
                    NombreCompleto = $"{cli.Apellido}, {cli.Nombre}"
                }).ToList();

                ddlCliente.DataSource = apellidoNombre;
                ddlCliente.DataTextField = "NombreCompleto";
                ddlCliente.DataValueField = "ID";
                ddlCliente.DataBind();
                ddlCliente.Items.Insert(0, new ListItem("Seleccionar cliente", ""));
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx");
            }
        }

        // Funciona ok el ddlCliente.
        private void FiltrarVentas()
        {
            List<Venta> ventas = (List<Venta>)Session["listaVentas"];
            List<Venta> ventasFiltrada;

            if (ventas != null)
            {
                string valorSelec = ddlCliente.SelectedValue;

                if (!string.IsNullOrEmpty(valorSelec))
                {
                    long clienteSelec = long.Parse(valorSelec);
                    ventasFiltrada = ventas.Where(x => x.IdCliente == clienteSelec).ToList();
                }
                else
                {
                    ventasFiltrada = ventas;
                }

                GvVentas.DataSource = ventasFiltrada;
            }
            else
            {
                GvVentas.DataSource = ventas;
            }
            GvVentas.DataBind();
        }

        protected void TxtFiltro_TextChanged(object sender, EventArgs e)
        {
            FiltrarVentas();
        }

        protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarVentas();
        }

        protected void ChkOrdenarPorFecha_CheckedChanged(object sender, EventArgs e)
        {
            FiltrarVentas();
        }

        protected void ChkOrdenarPorTotal_CheckedChanged(object sender, EventArgs e)
        {
            FiltrarVentas();
        }

        protected void BtnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            try
            {
                TxtFiltro.Text = string.Empty;
                //ChkOrdenarPorFecha.Checked = false;
                //ChkOrdenarPorTotal.Checked = false;
                ddlCliente.SelectedIndex = -1;

                CargarVentas();
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx");
            }
        }

        protected void GvVentas_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Id = GvVentas.SelectedDataKey.Value.ToString();
            Response.Redirect("AltaVenta.aspx?ID=" + Id);
        }

        //protected void GvVentas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    try
        //    {
        //        GvVentas.PageIndex = e.NewPageIndex;
        //        List<Venta> listaVentas = (List<Venta>)Session["listaFiltrada"];

        //        GvVentas.DataSource = listaVentas;
        //        GvVentas.DataBind();
        //    }
        //    catch (Exception)
        //    {
        //        Response.Redirect("Error.aspx");
        //    }
        //}

        protected void GvVentas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                long id = Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "ID"));
                string apellidoNombre = TraerNombreCliente(id);
                e.Row.Cells[2].Text = apellidoNombre;
            }
        }

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

        //CORREGIR segundo click:
        protected void GvVentas_Sorting(object sender, GridViewSortEventArgs e)
        {
            List<Venta> lista = (List<Venta>)Session["listaVentas"];
            string columna = e.SortExpression;
            List<Venta> listaFiltrada;

            if (lista != null)
            {
                if (columna == "ID")
                {
                    listaFiltrada = e.SortDirection == SortDirection.Descending
                        ? lista.OrderByDescending(x => x.ID).ToList()
                        : lista.OrderBy(x => x.ID).ToList();
                }
                else if (columna == "FechaVenta")
                {
                    listaFiltrada = e.SortDirection == SortDirection.Descending
                        ? lista.OrderByDescending(x => x.FechaVenta).ToList()
                        : lista.OrderBy(x => x.FechaVenta).ToList();
                }
                else if (columna == "IdCliente")
                {
                    //pend ordenar x cliente >> A-Z apellido, nombre, no por id.asc/desc
                    listaFiltrada = e.SortDirection == SortDirection.Descending
                        ? lista.OrderByDescending(x => x.IdCliente).ToList()
                        : lista.OrderBy(x => x.IdCliente).ToList();
                }
                else
                {
                    listaFiltrada = e.SortDirection == SortDirection.Descending
                        ? lista.OrderByDescending(x => x.Total).ToList()
                        : lista.OrderBy(x => x.Total).ToList();
                }
                Session["listaVentas"] = listaFiltrada;
            }
            else
            {
                Session["listaVentas"] = lista;
            }
            GvVentas.DataSource = Session["listaVentas"];
            GvVentas.DataBind();
        }
    }

}