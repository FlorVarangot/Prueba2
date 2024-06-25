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
    public partial class AltaCompra : System.Web.UI.Page
    {
        private List<DetalleCompra> detallesCompra;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //cargo los ddl
                CargarProveedores();
                CargarMarcas();
                CargarArticulos();
                //esto muestra la fecha actual
                txtFechaCompra.Text = DateTime.Now.ToString("yyyy-MM-dd");

                detallesCompra = new List<DetalleCompra>();
                Session["DetallesCompra"] = detallesCompra;
            }
            else
            {
                detallesCompra = Session["DetallesCompra"] as List<DetalleCompra>;
            }

        }

        private void CargarProveedores()
        {
            try
            {
                ProveedorNegocio negocio = new ProveedorNegocio();
                List<Proveedor> proveedores = negocio.Listar();

                ddlProveedor.DataSource = proveedores;
                ddlProveedor.DataTextField = "Nombre";
                ddlProveedor.DataValueField = "ID";
                ddlProveedor.DataBind();
                ddlProveedor.Items.Insert(0, new ListItem("Seleccione Proveedor", ""));
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx", false);
            }
        }
        private void CargarMarcas()
        {
            ddlMarca.Items.Insert(0, new ListItem("Seleccione Marca", ""));
        }
        private void CargarArticulos()
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                List<Articulo> articulos = negocio.Listar();

                ddlArticulo.DataSource = articulos;
                ddlArticulo.DataTextField = "Nombre";
                ddlArticulo.DataValueField = "ID";
                ddlArticulo.DataBind();
                ddlArticulo.Items.Insert(0, new ListItem("Seleccione Artículo", ""));
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx", false);
            }
        }
        protected void ddlProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idProveedor = int.Parse(ddlProveedor.SelectedValue);

            if (idProveedor > 0)
            {
                MarcaNegocio negocioMarca = new MarcaNegocio();
                List<Marca> marcas = negocioMarca.Listar().Where(x => x.IdProveedor == idProveedor).ToList();

                ddlMarca.DataSource = marcas;
                ddlMarca.DataTextField = "Descripcion";
                ddlMarca.DataValueField = "ID";
                ddlMarca.DataBind();
                ddlMarca.Items.Insert(0, new ListItem("Seleccione Marca", ""));
            }
            else
            {
                ddlMarca.Items.Clear();
                ddlMarca.Items.Insert(0, new ListItem("Seleccione Proveedor primero", ""));
            }

            ddlArticulo.Items.Clear();
            ddlArticulo.Items.Insert(0, new ListItem("Seleccione Marca primero", ""));
        }
        protected void ddlMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idMarca = int.Parse(ddlMarca.SelectedValue);

            if (idMarca > 0)
            {
                ArticuloNegocio negocioArticulo = new ArticuloNegocio();
                List<Articulo> articulos = negocioArticulo.ListarPorMarca(idMarca);

                ddlArticulo.DataSource = articulos;
                ddlArticulo.DataTextField = "Nombre";
                ddlArticulo.DataValueField = "ID";
                ddlArticulo.DataBind();
                ddlArticulo.Items.Insert(0, new ListItem("Seleccione Artículo", ""));
            }
            else
            {
                ddlArticulo.Items.Clear();
                ddlArticulo.Items.Insert(0, new ListItem("Seleccione Marca primero", ""));
            }
        }
        protected void btnAgregar_Click1(object sender, EventArgs e)
        {
            try
            {
                DetalleCompra detalle = new DetalleCompra();
                detalle.IdArticulo = long.Parse(ddlArticulo.SelectedValue);
                detalle.Cantidad = int.Parse(txtCantidad.Text);
                detalle.Precio = decimal.Parse(txtPrecio.Text);

                detallesCompra.Add(detalle);
                Session["DetallesCompra"] = detallesCompra;
                //Actulizo dinamicamente arriba de los ddl
                ActualizarArticulosAgregados();
                ActualizarCompra();
                //limpio los campos despues de de darle al boton +
                ddlMarca.SelectedIndex = 0;
                ddlArticulo.Items.Clear(); 
                ddlArticulo.Items.Insert(0, new ListItem("Seleccione Artículo", ""));
                txtCantidad.Text = "";
                txtPrecio.Text = "";
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx");
            }
        }


        private void ActualizarArticulosAgregados()
        {
            rptArticulosAgregados.DataSource = detallesCompra;
            rptArticulosAgregados.DataBind();
        }

        private void ActualizarCompra()
        {
            decimal total = Calcular();
            lblTotalCompra.Text = "Total compra: $" + total.ToString("N2");
            Session["Total"] = total;
        }
        private decimal Calcular()
        {
            decimal total = 0;
            foreach (DetalleCompra detalle in detallesCompra)
            {
                total += detalle.Precio * detalle.Cantidad;
            }
            return total;
        }


        protected void btnGuardarCompra_Click(object sender, EventArgs e)
        {
            try
            {
                Compra compra = new Compra();
                compra.FechaCompra = DateTime.ParseExact(txtFechaCompra.Text, "yyyy-MM-dd", null);
                compra.IdProveedor = int.Parse(ddlProveedor.SelectedValue);
                compra.Detalles = detallesCompra;
                compra.TotalCompra = Calcular();             

                 CompraNegocio negocio = new CompraNegocio();
                 negocio.AgregarCompra(compra);
                Session["DetallesCompra"] = null;
                Session["Total"] = null;
                Response.Redirect("Compras.aspx", false);
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx");
            }
        }


    }
}