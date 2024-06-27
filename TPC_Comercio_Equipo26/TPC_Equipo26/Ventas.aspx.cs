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
            VentaNegocio negocio = new VentaNegocio();
            List<Venta> ventas = negocio.Listar();

            Session["listaVentas"] = ventas;
            FiltrarVentas();
            CargarClientes();

            GvVentas.DataSource = ventas;
            GvVentas.DataBind();


            if (ventas != null)
            {
                lblVacio.Visible = false;
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

        protected void FiltrarVentas()
        {

            List<Venta> ventas = (List<Venta>)Session["listaVentas"];

            if (!string.IsNullOrEmpty(TxtFiltro.Text.Trim()))
            {
                string filtro = TxtFiltro.Text.Trim().ToUpper();
                ventas = ventas.Where(x => x.Total.Equals(filtro)).ToList();
            }

            Session["ventasFiltrada"] = ventas;

            if (ventas.Count > 0)
            {
                lblVacio.Visible = false;
            }
            else
            {
                lblVacio.Visible = true;
            }
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
                ChkOrdenarPorFecha.Checked = false;
                ChkOrdenarPorTotal.Checked = false;
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
        protected void GvVentas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GvVentas.PageIndex = e.NewPageIndex;
                List<Venta> listaVentas = (List<Venta>)Session["ListaFiltrada"];

                lblVacio.Visible = (listaVentas.Count == 0);
                GvVentas.DataSource = listaVentas;
                GvVentas.DataBind();
            }
            catch (Exception)
            {
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
                //    Response.Redirect("Error.aspx");
                return "Error al obtener el nombre del cliente";
            }
        }



    }
}