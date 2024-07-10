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
            if (ValidarSesionActiva())
            {
                try
                {
                    if (!IsPostBack)
                    {
                        CargarVentas();
                    }
                }
                catch (Exception ex)
                {
                    Session.Add("Error", ex.ToString());
                    Response.Redirect("Error.aspx");
                }
            }
            else
            {
                Session.Add("Error", "No tenes permisos para ingresar a esta pantalla.");
                Response.Redirect("Error.aspx", false);
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
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
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
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

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

                string ordenSeleccionado = ddlOrdenarPor.SelectedValue;
                switch (ordenSeleccionado)
                {
                    case "MayorPrecio":
                        ventasFiltrada = ventasFiltrada.OrderByDescending(x => x.Total).ToList();
                        break;
                    case "MenorPrecio":
                        ventasFiltrada = ventasFiltrada.OrderBy(x => x.Total).ToList();
                        break;
                    case "FechaReciente":
                        ventasFiltrada = ventasFiltrada.OrderByDescending(x => x.FechaVenta).ToList();
                        break;
                    case "FechaAntigua":
                        ventasFiltrada = ventasFiltrada.OrderBy(x => x.FechaVenta).ToList();
                        break;
                    case "VentaAsc":
                        ventasFiltrada = ventasFiltrada.OrderBy(x => x.ID).ToList();
                        break;
                    case "VentaDesc":
                        ventasFiltrada = ventasFiltrada.OrderByDescending(x => x.ID).ToList();
                        break;
                    default:
                        ventasFiltrada = ventasFiltrada.OrderByDescending(x => x.FechaVenta).ToList();
                        break;
                }

                Session["ListaVentasFiltrada"] = ventasFiltrada;
                GvVentas.DataSource = ventasFiltrada;
            }
            else
            {
                GvVentas.DataSource = ventas;
            }
            GvVentas.DataBind();
        }

        private void MostrarBotonRestablecer()
        {
            bool filtroActivo = !string.IsNullOrEmpty(ddlOrdenarPor.SelectedValue) || !string.IsNullOrEmpty(ddlCliente.SelectedValue);
            BtnLimpiarFiltros.Visible = filtroActivo;
        }

        protected void TxtFiltro_TextChanged(object sender, EventArgs e)
        {
            FiltrarVentas();
            MostrarBotonRestablecer();
        }

        protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarVentas();
            MostrarBotonRestablecer();
        }

        protected void ddlOrdenarPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarVentas();
            MostrarBotonRestablecer();
        }

        protected void BtnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            try
            {
                ddlCliente.SelectedIndex = -1;
                BtnLimpiarFiltros.Visible = false;
                CargarVentas();

                FiltrarVentas();
                MostrarBotonRestablecer();
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void GvVentas_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Id = GvVentas.SelectedDataKey.Value.ToString();
            Response.Redirect("AltaVenta.aspx?ID=" + Id);
        }

        protected void GvVentas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GvVentas.PageIndex = e.NewPageIndex;
                List<Venta> listaVentas = (List<Venta>)Session["ListaVentasFiltrada"];

                GvVentas.DataSource = listaVentas;
                GvVentas.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

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
                Session.Add("Error", "Error al obtener el nombre del cliente");
                Response.Redirect("Error.aspx");
                return null;
            }
        }

        protected bool ValidarSesionActiva()
        {
            if (Seguridad.sesionActiva(Session["Usuario"]))
                return true;
            return false;
        }

    }

}