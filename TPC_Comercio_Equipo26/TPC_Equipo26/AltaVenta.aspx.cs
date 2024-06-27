using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPC_Equipo26.Dominio;
using TPC_Equipo26.Negocio;

namespace TPC_Equipo26
{
    public partial class AltaVenta : System.Web.UI.Page
    {
        private List<DetalleVenta> detallesVenta;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarClientes();
                CargarArticulos();
                txtFechaVenta.Text = DateTime.Now.ToString("yyyy-MM-dd");
                lblTotalVenta.Text = "$0.00";
                detallesVenta = new List<DetalleVenta>();
                Session["DetallesVenta"] = detallesVenta;
            }
            else
            {
                detallesVenta = Session["DetallesVenta"] as List<DetalleVenta>;
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
            catch (Exception)
            {
                Response.Redirect("Error.aspx");
            }
        }

        protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
           
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
                detallesVenta.Add(detalle);
                Session["DetallesVentaa"] = detallesVenta;
                ActualizarArticulos();
                ActualizarVenta();
                ddlArticulo.Items.Clear();
                ddlArticulo.SelectedIndex= -1;
                numCantidad.Value = "0";
                lblPrecio.Text = "$0.00";
                CargarArticulos();

            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx");
            }
        }

        private void ActualizarArticulos()
        {
            rptArticulos.DataSource = detallesVenta;
            rptArticulos.DataBind();
        }

        private void ActualizarVenta()
        {
            decimal total = Calcular();
            lblTotalVenta.Text = total.ToString("C2");
            Session["Total"] = total;
        }

        private decimal Calcular()
        {
            decimal total = 0;
            //foreach (DetalleCompra detalle in detallesVenta)
            //{
            //    total += Precio * detalle.Cantidad;
            //}
            return total;
        }

        protected void btnConfirmarVenta_Click(object sender, EventArgs e)
        {
            try
            {
                //Venta venta= new Venta();
                //venta.FechaVenta = DateTime.ParseExact(txtFechaVenta.Text, "yyyy-MM-dd", null);
                //venta.IdCliente= int.Parse(ddlCliente.SelectedValue);
                //venta.Detalles = detallesVenta;
                //venta.Total = Calcular();

                //VentaNegocio negocio = new VentaNegocio();
                //negocio.AgregarVenta(venta);


                //Session["DetallesVenta"] = null;
                //Session["Total"] = null;
                //lblTotalVenta.Text = "$0.00";
                //Response.Redirect("Ventas.aspx", false);
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx");
            }
        }







    }

}