using System;
using System.Collections.Generic;
using System.Globalization;
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
            if (ValidarSesionActiva())
            {
                try
                {
                    if (!IsPostBack)
                    {
                        CargarProveedores();
                        CargarMarcas();
                        CargarArticulos();
                        txtFechaCompra.Text = DateTime.Now.ToString("yyyy-MM-dd");
                        lblTotalCompra.Text = "$0.00";
                        detallesCompra = new List<DetalleCompra>();
                        Session["DetallesCompra"] = detallesCompra;
                        btnGuardarCompra.Visible = false;
                    }
                    else
                    {
                        detallesCompra = Session["DetallesCompra"] as List<DetalleCompra>;
                    }
                }
                catch (Exception ex)
                {
                    Session.Add("Error", ex.ToString());
                    Response.Redirect("Error.aspx", false);
                }
            }
        }

        private void CargarProveedores()
        {
            try
            {
                ProveedorNegocio negocio = new ProveedorNegocio();
                List<Proveedor> proveedores = negocio.Listar().Where(prov => prov.Activo == true).ToList();

                ddlProveedor.DataSource = proveedores;
                ddlProveedor.DataTextField = "Nombre";
                ddlProveedor.DataValueField = "ID";
                ddlProveedor.DataBind();
                ddlProveedor.Items.Insert(0, new ListItem("Seleccione Proveedor", ""));
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        private void CargarMarcas()
        {
            ddlMarca.Items.Clear();
            ddlMarca.Items.Insert(0, new ListItem("Seleccione Proveedor primero", ""));
        }

        private void CargarArticulos()
        {
            ddlArticulo.Items.Clear();
            ddlArticulo.Items.Insert(0, new ListItem("Seleccione Marca primero", ""));
        }
        private int ObtenerStockDisponible(long idArticulo)
        {
            DatoArticuloNegocio negocio = new DatoArticuloNegocio();
            return negocio.ObtenerStockArticulo(idArticulo);
        }

        protected void ddlProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idProveedor = int.Parse(ddlProveedor.SelectedValue);

            if (idProveedor > 0)
            {
                MarcaNegocio negocioMarca = new MarcaNegocio();
                List<Marca> marcas = negocioMarca.Listar().Where(x => x.IdProveedor == idProveedor).ToList();
                if (marcas.Count > 0)
                {
                    ddlMarca.DataSource = marcas;
                    ddlMarca.DataTextField = "Descripcion";
                    ddlMarca.DataValueField = "ID";
                    ddlMarca.DataBind();
                    ddlMarca.Items.Insert(0, new ListItem("Seleccione Marca", ""));
                    CargarArticulos();
                }
                else
                {
                    ddlMarca.Items.Clear();
                    ddlMarca.Items.Insert(0, new ListItem("Sin marcas asociadas", ""));
                    ddlArticulo.Items.Clear();
                    ddlArticulo.Items.Insert(0, new ListItem("Sin artículos asociados", ""));
                }
            }
            else
            {
                ddlMarca.Items.Clear();
                ddlMarca.Items.Insert(0, new ListItem("Seleccione Proveedor primero", ""));
                ddlArticulo.Items.Clear();
                ddlArticulo.Items.Insert(0, new ListItem("Seleccione Marca primero", ""));
            }           
        }

        protected void ddlMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idMarca = int.Parse(ddlMarca.SelectedValue);

            if (idMarca > 0)
            {
                ArticuloNegocio negocioArticulo = new ArticuloNegocio();
                List<Articulo> articulos = negocioArticulo.ListarPorMarca(idMarca);
                if (articulos.Count > 0)
                {
                    List<ListItem> items = new List<ListItem>();
                    foreach (var articulo in articulos)
                    {
                        int stockDisponible = ObtenerStockDisponible(articulo.ID);
                        string articuloDisp = $"{articulo.Codigo} - {articulo.Nombre} - Stock: {stockDisponible}";

                        items.Add(new ListItem(articuloDisp, articulo.ID.ToString()));
                    }

                    
                    ddlArticulo.DataSource = items;
                    ddlArticulo.DataTextField = "Text";
                    ddlArticulo.DataValueField = "Value";
                    ddlArticulo.DataBind();
                    ddlArticulo.Items.Insert(0, new ListItem("Seleccione Artículo", ""));
                }
                else
                {
                    ddlArticulo.Items.Clear();
                    ddlArticulo.Items.Insert(0, new ListItem("Sin artículos asociados", ""));
                }
            }
            else
            {
                ddlArticulo.Items.Clear();
                ddlArticulo.Items.Insert(0, new ListItem("Seleccione Marca primero", ""));
            }
        }

        protected void ddlArticulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlArticulo.SelectedIndex > 0)
            {
                ddlProveedor.Enabled = false;
            }
        }

        protected void btnAgregar_Click1(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
                return;
            string error = ValidarCampos();

            if (string.IsNullOrEmpty(error))
            {
                try
                {
                    DetalleCompra detalle = new DetalleCompra();
                    detalle.IdArticulo = long.Parse(ddlArticulo.SelectedValue);
                    detalle.Cantidad = int.Parse(txtCantidad.Text);
                    detalle.Precio = decimal.Parse(txtPrecio.Text);
                    detalle.IdMarca = int.Parse(ddlMarca.SelectedValue);
                    List<DetalleCompra> detallesCompra = Session["DetallesCompra"] as List<DetalleCompra>;
                    if (detallesCompra == null)
                    {
                        detallesCompra = new List<DetalleCompra>();
                    }
                    detallesCompra.Add(detalle);

                    ArticuloNegocio negocioArticulo = new ArticuloNegocio();
                    foreach (var detalleCompra in detallesCompra)
                    {
                        Articulo articulo = negocioArticulo.ObtenerArticuloPorID(detalleCompra.IdArticulo);
                        if (articulo != null)
                        {
                            detalleCompra.NombreArticulo = articulo.Descripcion;
                        }
                        else
                        {
                            detalleCompra.NombreArticulo = "Artículo no encontrado";
                        }
                    }

                    Session["DetallesCompra"] = detallesCompra;

                    rptArticulosAgregados.DataSource = detallesCompra;
                    rptArticulosAgregados.DataBind();
                    btnGuardarCompra.Visible = true;
                    ActualizarArticulosAgregados();
                    ActualizarCompra();

                    LimpiarCampos();
                }
                catch (Exception ex)
                {
                    Session.Add("Error", ex.ToString());
                    Response.Redirect("Error.aspx");
                }
            }
            else
            {
                lblError.Text = error;
                lblError.Visible = true;
            }
        }

        private string ValidarCampos()
        {
            int cantidad;
            decimal precio;
            bool cantidadValida = int.TryParse(txtCantidad.Text, out cantidad);
            bool precioValido = decimal.TryParse(txtPrecio.Text, out precio);

            if (!cantidadValida || !precioValido)
            {
                return "Debe Ingresar Cantidad de Articulos a comprar o su Precio";
            }

            if (cantidad <= 0 && precio <= 0)
            {
                return "Ingrese cantidad y precio validos";
            }

            if (cantidad <= 0)
            {
                return "Ingrese una cantidad valida";
            }

            if (precio <= 0)
            {
                return "Ingrese un precio valido";
            }

            return string.Empty;
        }

        private void LimpiarCampos()
        {
            ddlMarca.SelectedIndex = 0;
            ddlArticulo.Items.Clear();
            ddlArticulo.Items.Insert(0, new ListItem("Seleccione Artículo", ""));
            txtCantidad.Text = "";
            txtPrecio.Text = "";
        }

        private void ActualizarArticulosAgregados()
        {
            rptArticulosAgregados.DataSource = detallesCompra;
            rptArticulosAgregados.DataBind();
        }

        private void ActualizarCompra()
        {
            decimal total = Calcular();
            lblTotalCompra.Text = total.ToString("C2");
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
            Page.Validate();
            if (!Page.IsValid)
                return;
            string error = ValidarCompra();

            if (string.IsNullOrEmpty(error))
            {
                try
                {
                    Compra compra = new Compra();
                    compra.FechaCompra = DateTime.ParseExact(txtFechaCompra.Text, "yyyy-MM-dd", null);
                    compra.IdProveedor = int.Parse(ddlProveedor.SelectedValue);
                    compra.Detalles = Session["DetallesCompra"] as List<DetalleCompra>;
                    compra.TotalCompra = Calcular();

                    CompraNegocio negocio = new CompraNegocio();
                    negocio.AgregarCompra(compra);

                    DatoArticuloNegocio datoNegocio = new DatoArticuloNegocio();
                    datoNegocio.ActualizarStockPostCompra(compra);

                    LimpiarSesion();
                    LimpiarCampos();
                    Response.Redirect("Compras.aspx", false);
                }
                catch (Exception)
                {
                    Session.Add("Error", lblError.ToString());
                    Response.Redirect("Error.aspx", false);
                }

            }
            else
            {
                lblError.Text = error;
                lblError.Visible = true;
            }
        }

      


        private void LimpiarSesion()
        {
            Session["DetallesCompra"] = null;
            Session["Total"] = null;
            lblTotalCompra.Text = "$0.00";
        }

        private string ValidarCompra()
        {
            if (ddlProveedor.SelectedIndex == 0)
            {
                return "Seleccione un proveedor";
            }

            if (detallesCompra == null || detallesCompra.Count == 0)
            {
                return "Agregue al menos un artículo a la compra";
            }

            return string.Empty;
        }

        protected void rptArticulosAgregados_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                string commandArgument = e.CommandArgument.ToString();
                long idDetalleCompra;

                if (long.TryParse(commandArgument.Split('_')[0], out idDetalleCompra))
                {
                    DetalleCompra detalle = detallesCompra.FirstOrDefault(d => d.Id == idDetalleCompra);
                    if (detalle != null)
                    {
                        txtCantidad.Text = detalle.Cantidad.ToString();
                        txtPrecio.Text = detalle.Precio.ToString();
                        ddlMarca.SelectedValue = detalle.IdMarca.ToString();
                        ddlMarca_SelectedIndexChanged(null, EventArgs.Empty);
                        ddlArticulo.SelectedValue = detalle.IdArticulo.ToString();

                        detallesCompra.Remove(detalle);
                        Session["DetallesCompra"] = detallesCompra;

                        ActualizarArticulosAgregados();
                        ActualizarCompra();
                    }
                }
            }
            else if (e.CommandName == "Eliminar")
            {
                string commandArgument = e.CommandArgument.ToString();
                long idDetalleCompra;

                if (long.TryParse(commandArgument.Split('_')[0], out idDetalleCompra))
                {
                    DetalleCompra detalle = detallesCompra.FirstOrDefault(d => d.Id == idDetalleCompra);
                    if (detalle != null)
                    {
                        detallesCompra.Remove(detalle);
                        Session["DetallesCompra"] = detallesCompra;

                        ActualizarArticulosAgregados();
                        ActualizarCompra();
                    }
                }
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