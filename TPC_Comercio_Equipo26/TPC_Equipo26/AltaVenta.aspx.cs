using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
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
                }
                catch (Exception ex)
                {
                    Session.Add("Error", ex.ToString());
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

                var articulosConStock = articulos.Select(art => new
                {
                    ID = art.ID,
                    ArticuloCompleto = $"{art.Codigo}, {art.Nombre}, {art.Descripcion} - Stock disponible: {ObtenerStockDisponible(art.ID)}"
                }).Where(a => ObtenerStockDisponible(a.ID) > 0).ToList();

                ddlArticulo.DataSource = articulosConStock;
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

        private int ObtenerStockDisponible(long idArticulo)
        {
            DatoArticuloNegocio dato = new DatoArticuloNegocio();
            return dato.ObtenerStockArticulo(idArticulo);
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

        protected void VerificarStock()
        {
            if (ddlArticulo.SelectedIndex > 0)
            {
                btnGuardarVenta.Visible = true;
            }
            else
            {
                btnGuardarVenta.Visible = false;
            }
        }

        private string ValidarCampos()
        {
            if (ddlArticulo.SelectedIndex == 0)
            {
                return "Debe seleccionar un artículo y su cantidad";
            }

            int cantidad;
            bool cantidadValida = int.TryParse(numCantidad.Value, out cantidad);

            if (!cantidadValida)
            {
                return "Debe ingresar una cantidad válida";
            }

            if (cantidad <= 0)
            {
                return "La cantidad debe ser mayor a cero";
            }

            return string.Empty;
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            OcultarError();
            string error = ValidarCampos();
            if (string.IsNullOrEmpty(error))
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
                        MostrarError("No hay stock suficiente");
                        return;
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
            else
            {
                MostrarError(error);
            }
        }

        private void MostrarError(string mensaje)
        {
            lblError.Text = mensaje;
            lblError.Visible = true;
        }

        private void OcultarError()
        {
            lblError.Text = "";
            lblError.Visible = false;
        }

        private bool validarStock(DetalleVenta detalle)
        {
            try
            {
                DatoArticuloNegocio datoNegocio = new DatoArticuloNegocio();

                long idArticulo = detalle.IdArticulo;
                int cantidadSolicitada = detalle.Cantidad;
                int cantidadEnStock = datoNegocio.ObtenerStockArticulo(idArticulo);

                if (cantidadSolicitada > cantidadEnStock)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                return false;
            }
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

                ArticuloNegocio artiNegocio = new ArticuloNegocio();
                Articulo aux = new Articulo();

                Usuario user = (Usuario)Session["Usuario"];

                int stockMinimoAlcanzado = 0;
                string mensaje;

                foreach (var detalle in venta.Detalles)
                {
                    long idArticulo = detalle.IdArticulo;
                    aux = artiNegocio.ObtenerArticuloPorID(idArticulo);
                    int stockActual = datoNegocio.ObtenerStockArticulo(idArticulo);
                    int stockMinimo = aux.StockMin;

                    if (stockActual <= stockMinimo)
                    {
                        stockMinimoAlcanzado += 1;
                    }

                    if (stockActual <= 1)
                    {
                        //Chequear si anda ok.
                        EnviarCorreoRecordatorio(user, idArticulo);
                    }
                }

                if (stockMinimoAlcanzado > 0)
                {
                    mensaje = "Uno o más artículos de la lista han alcanzado el stock mínimo establecido. Por favor reabastezca.";
                }
                else
                {
                    mensaje = "Venta registrada con éxito";
                }

                EnviarCorreoPostVenta(venta);
                ScriptManager.RegisterStartupScript(this, GetType(), "showSuccessMessage", $"mostrarMensajeStock('{mensaje}');", true);

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

                    cuerpo += $"{articulo.Nombre} - {articulo.Descripcion} - Cantidad: {detalle.Cantidad}<br>";
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

        private void EnviarCorreoRecordatorio(Usuario user, long idArticulo)
        {
            try
            {
                EmailService emailService = new EmailService();

                ArticuloNegocio articuloNegocio = new ArticuloNegocio();
                Articulo articulo = articuloNegocio.ObtenerArticuloPorID(idArticulo);

                Marca marca = articulo.Marca;
                
                ProveedorNegocio proveedorNegocio = new ProveedorNegocio();
                Proveedor proveedor = proveedorNegocio.ObtenerProveedorPorId(marca.IdProveedor);

                DatoArticuloNegocio datoNegocio = new DatoArticuloNegocio();
                int stock = datoNegocio.ObtenerStockArticulo(idArticulo);

                string emailDestino = user.Email;
                string asunto = "Recordatorio de compra: Stock bajo";
                string cuerpo = $"Estimado/a {user.Nombre + user.Apellido},<br><br>" +
                        $"Le informamos que el artículo <strong>{articulo.Nombre}</strong> (ID: {idArticulo}) está por agotarse.<br>" +
                        $"Descripción: {articulo.Descripcion}<br>" +
                        $"Marca: {marca.Descripcion}<br>" +
                        $"Proveedor: {proveedor.Nombre}<br>" +
                        $"Stock actual: {stock}<br><br>" +
                        $"Para reabastecer este artículo, puede contactar al proveedor:<br>" +
                        $"Nombre: {proveedor.Nombre}<br>" +
                        $"Email: {proveedor.Email}<br>" +
                        $"Teléfono: {proveedor.Telefono}<br><br>" +
                        $"Le recomendamos realizar un nuevo pedido para evitar quedarse sin stock.<br><br>" +
                        $"Saludos cordiales,<br>" +
                        $"Su tienda de confianza";

                emailService.ArmarCorreo(emailDestino, asunto, cuerpo);
                emailService.enviarEmail();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al enviar el correo de recordatorio de compra.", ex);
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

                decimal precio = datoNegocio.ObtenerPrecioHistorico(idArt, fechaVenta);
                int cantidad = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Cantidad"));
                decimal totalParcial = precio * cantidad;

                e.Row.Cells[0].Text = descripcionArticulo;
                e.Row.Cells[1].Text = precio.ToString("C2");
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