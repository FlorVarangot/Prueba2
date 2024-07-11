using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using TPC_Equipo26.Dominio;
using TPC_Equipo26.Negocio;

namespace TPC_Equipo26
{
    public partial class AltaVenta : System.Web.UI.Page
    {
        private List<DetalleVenta> detallesVenta;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ValidarSesionActiva())
            {
                try
                {
                    if (!IsPostBack)
                    {
                        CargarClientes();
                        CargarArticulos();
                        txtFechaVenta.Text = DateTime.Now.ToString("yyyy-MM-dd");

                        decimal totalVenta = Calcular();
                        lblTotalVenta.Text = "Total Venta: $" + totalVenta;

                        detallesVenta = new List<DetalleVenta>();
                        Session["DetallesVenta"] = detallesVenta;

                        selectores.Visible = false;
                        lblTotalVenta.Visible = false;
                        btnGuardarVenta.Visible = false;
                    }
                    else
                    {
                        detallesVenta = Session["DetallesVenta"] as List<DetalleVenta>;
                    }
                }catch(Exception ex)
                {
                    Session.Add("Error",ex.ToString());
                    Response.Redirect("Error.aspx", false);
                }
            }
        }

        protected void CargarClientes()
        {
            try
            {
                ClienteNegocio negocio = new ClienteNegocio();
                List<Cliente> clientes = negocio.Listar().Where(cli => cli.Activo == true).ToList();

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
                Session.Add("Error", ex);
                Response.Redirect("Error.aspx", false);
            }
        }

        private void CargarArticulos()
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                List<Articulo> articulos = negocio.Listar();

                var articulo = articulos.Select(art => new
                {
                    ID = art.ID,
                    ArticuloCompleto = $"{art.Codigo}, {art.Nombre}, {art.Descripcion}"
                }).ToList();

                ddlArticulo.DataSource = articulo;
                ddlArticulo.DataTextField = "ArticuloCompleto";
                ddlArticulo.DataValueField = "ID";
                ddlArticulo.DataBind();
                ddlArticulo.Items.Insert(0, new ListItem("Seleccionar Artículo", ""));
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCliente.SelectedIndex > 0)
            {
                selectores.Visible = true;
                lblTotalVenta.Visible = true;
                ddlCliente.Enabled = false;
                lblExists.Visible = false;
                lnkAltaCli.Visible = false;
            }
        }

        protected void ddlArticulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnGuardarVenta.Visible = true;
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                VentaNegocio negocio = new VentaNegocio();
                long idVenta = negocio.TraerUltimoId();
                DetalleVenta detalle = new DetalleVenta();

                detalle.IdVenta = idVenta;
                detalle.IdArticulo = long.Parse(ddlArticulo.SelectedValue);
                detalle.Cantidad = int.Parse(numCantidad.Value);

                if (validarStock(detalle))
                {
                    detallesVenta.Add(detalle);
                }
                else
                {
                    Session.Add("Error", "No hay stock suficiente.");
                    Response.Redirect("Error.aspx");
                }

                Session["DetallesVenta"] = detallesVenta;

                ActualizarArticulos();
                ActualizarVenta();

                ddlArticulo.Items.Clear();
                ddlArticulo.SelectedIndex = -1;
                numCantidad.Value = "1";

                CargarArticulos();

            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        private bool validarStock(DetalleVenta detalle)
        {
            DatoArticuloNegocio datoNegocio = new DatoArticuloNegocio();

            long idArticulo = detalle.IdArticulo;
            int cantidadSolicitada = detalle.Cantidad;
            int cantidadEnStock = datoNegocio.ObtenerStockArticulo(idArticulo);

            if (cantidadSolicitada <= cantidadEnStock)
            {
                return true;
            }
            return false;
        }

        private void ActualizarArticulos()
        {
            gvAltaVenta.DataSource = detallesVenta;
            gvAltaVenta.DataBind();
        }

        private void ActualizarVenta()
        {
            decimal totalVenta = Calcular();
            lblTotalVenta.Text = "Total venta: " + totalVenta.ToString("C2");
            Session["Total"] = totalVenta;
        }

        protected decimal Calcular()
        {
            decimal totalVenta = 0;
            foreach (GridViewRow row in gvAltaVenta.Rows)
            {
                decimal totalParcial = Convert.ToDecimal(row.Cells[3].Text.Replace("$", ""));
                totalVenta += totalParcial;
            }
            return totalVenta;
        }

        protected void btnConfirmarVenta_Click(object sender, EventArgs e)
        {
            try
            {
                Venta venta = new Venta();
                venta.FechaVenta = DateTime.ParseExact(txtFechaVenta.Text, "yyyy-MM-dd", null);
                venta.IdCliente = int.Parse(ddlCliente.SelectedValue);
                venta.Detalles = detallesVenta;
                venta.Total = Calcular();

                VentaNegocio negocio = new VentaNegocio();
                negocio.AgregarVenta(venta);

                DatoArticuloNegocio datoNegocio = new DatoArticuloNegocio();
                datoNegocio.ActualizarStockPostVenta(venta);

                EnviarCorreoPostVenta(venta);

                Session["DetallesVenta"] = null;
                Session["Total"] = null;
                lblTotalVenta.Text = "Total venta = $0.00";
                Response.Redirect("Ventas.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void EnviarCorreoPostVenta(Venta venta)
        {
            try
            {
                EmailService emailService = new EmailService();

                ClienteNegocio clienteNegocio = new ClienteNegocio();
                Cliente cliente = clienteNegocio.ObtenerClientePorId(venta.IdCliente);

                string emailDestino = cliente.Email; 
                string asunto = "Confirmación de compra en nuestra tienda";
                string cuerpo = $"¡Gracias por comprar en nuestra tienda!<br><br>" +
                                $"Detalles de la compra:<br>" +
                                $"Fecha: {venta.FechaVenta.ToString("dd/MM/yyyy")}<br>" +
                                $"Total: {venta.Total.ToString("C2")}<br><br>" +
                                $"Productos comprados:<br>";

                foreach (var detalle in venta.Detalles)
                {
                    ArticuloNegocio articuloNegocio = new ArticuloNegocio();
                    Articulo articulo = articuloNegocio.ObtenerArticuloPorID(detalle.IdArticulo);

                    cuerpo += $"{articulo.Nombre} - Cantidad: {detalle.Cantidad}<br>";
                }

                cuerpo += "<br>Esperamos que disfrutes de tus productos.";
                emailService.ArmarCorreo(emailDestino, asunto, cuerpo);

                emailService.enviarEmail();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al enviar el correo de confirmación de compra.", ex);
            }
        }

        protected void gvAltaVenta_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAltaVenta.PageIndex = e.NewPageIndex;
            gvAltaVenta.DataSource = Session["DetallesVenta"];
            gvAltaVenta.DataBind();
        }

        protected void gvAltaVenta_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                long idArt = Convert.ToInt64(DataBinder.Eval(e.Row.DataItem, "IdArticulo"));
                Articulo articulo = negocio.ObtenerArticuloPorID(idArt);
                string descripcionArticulo = articulo.Descripcion;
                DatoArticuloNegocio datoNegocio = new DatoArticuloNegocio();
                DateTime fechaVenta = DateTime.Parse(txtFechaVenta.Text);

                decimal ganancia = articulo.Ganancia;
                decimal precio = datoNegocio.ObtenerPrecioHistorico(idArt, fechaVenta);
                int cantidad = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Cantidad"));
                decimal precioConGanancia = precio + (precio * ganancia / 100);
                decimal totalParcial = precio * cantidad;

                e.Row.Cells[0].Text = descripcionArticulo;
                e.Row.Cells[1].Text = precioConGanancia.ToString("C2");
                e.Row.Cells[3].Text = totalParcial.ToString("C2");
            }
        }

        protected void gvAltaVenta_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string argumento = e.CommandArgument.ToString();
                long idDetalle;

                if (long.TryParse(argumento.Split('_')[0], out idDetalle))
                {
                    DetalleVenta detalle = detallesVenta.FirstOrDefault(d => d.Id == idDetalle);
                    if (detalle != null)
                    {
                        detallesVenta.Remove(detalle);
                        Session["DetallesVenta"] = detallesVenta;
                        ActualizarArticulos();
                        ActualizarVenta();
                    }
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
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