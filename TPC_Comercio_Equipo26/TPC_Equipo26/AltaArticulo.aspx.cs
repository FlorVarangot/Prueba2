using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPC_Equipo26.Dominio;
using TPC_Equipo26.Negocio;

namespace TPC_Equipo26
{
    public partial class AltaArticulo : System.Web.UI.Page
    {
        public bool ConfirmarInactivar { get; set; }
        public bool ConfirmarReactivar { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ValidarSesion())
            {
                try
                {
                    if (!IsPostBack)
                    {
                        ConfirmarInactivar = false;
                        ConfirmarReactivar = false;


                        if (Request.QueryString["ID"] != null)
                        {
                            CargarMarcasYCategoriasTodas();
                            lblTituloModificar.Visible = true;
                            long idArticulo = long.Parse(Request.QueryString["ID"]);
                            CargarDatosArticulo(idArticulo);
                        }
                        else
                        {
                            CargarMarcasYCategoriasActivas();
                            lblTituloAgregar.Visible = true;
                            BtnInactivar.Visible = false;
                            btnReactivar.Visible = false;
                            LimpiarCampos();
                        }

                    }
                }
                catch (Exception)
                {
                    Session.Add("Error", "Error en alta de artículo");
                    Response.Redirect("Error.aspx");
                }
            }
            else
            {
                Session.Add("Error", "No tenes permisos para ingresar a esta pantalla.");
                Response.Redirect("Error.aspx", false);
            }
        }

        private void CargarDatosArticulo(long idArticulo)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo articulo = negocio.ObtenerArticuloPorID(idArticulo);

            if (articulo != null)
            {
                txtCodigo.Text = articulo.Codigo;
                txtNombre.Text = articulo.Nombre;
                txtDescripcion.Text = articulo.Descripcion;
                txtImagenUrl.Text = articulo.Imagen;

                numGanancia.Value = articulo.Ganancia.ToString("F4", CultureInfo.InvariantCulture);
                numStockMinimo.Value = articulo.StockMin.ToString();

                ddlMarca.SelectedValue = articulo.Marca.ID.ToString();
                ddlCategoria.SelectedValue = articulo.Categoria.ID.ToString();


                if (articulo.Imagen != null)
                {
                    txtImagenUrl.Text = articulo.Imagen;
                    imgArticulos.ImageUrl = articulo.Imagen;
                }
                else
                {
                    txtImagenUrl.Text = "";
                    imgArticulos.ImageUrl = "https://grupoact.com.ar/wp-content/uploads/2020/04/placeholder.png";
                }

                if (articulo.Activo == true)
                {
                    BtnInactivar.Visible = true;
                    btnReactivar.Visible = false;
                }
                else
                {
                    BtnInactivar.Visible = false;
                    btnReactivar.Visible = true;
                }

            }
            else
            {
                Response.Redirect("Error.aspx");
            }
        }

        private bool ValidarCampos()
        {
            bool camposValidos = true;

            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                lblCodigo.Visible = true;
                camposValidos = false;
            }
            else
            {
                lblCodigo.Visible = false;
            }
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                lblNombre.Visible = true;
                camposValidos = false;
            }
            else
            {
                lblNombre.Visible = false;
            }

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                lblDescripcion.Visible = true;
                camposValidos = false;
            }
            else
            {
                lblDescripcion.Visible = false;
            }

            if (ddlMarca.SelectedValue == "-1")
            {
                lblMarca.Visible = false;
            }
            else
            {
                lblMarca.Visible = false;
            }


            if (ddlCategoria.SelectedValue == "-1")
            {
                lblCategoria.Visible = false;
            }
            else
            {
                lblCategoria.Visible = false;
            }


            if (string.IsNullOrWhiteSpace(numGanancia.Value))
            {
                lblGanancia.Visible = true;
                camposValidos = false;
            }
            else
            {
                lblGanancia.Visible = false;
            }

            if (string.IsNullOrWhiteSpace(txtImagenUrl.Text))
            {
                lblImagenUrl.Visible = false;
            }
            else
            {
                lblImagenUrl.Visible = false;
            }
            if (string.IsNullOrWhiteSpace(numStockMinimo.Value))
            {
                lblStockMinimo.Visible = true;
                camposValidos = false;
            }
            else
            {
                lblStockMinimo.Visible = false;
            }

            if (!camposValidos)
            {
                lblError.Text = "Todos los campos obligatorios deben ser completados";
                lblError.Visible = true;
            }

            return camposValidos;
        }

        protected void BtnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos())
                {
                    return;
                }
                Articulo nuevo = new Articulo();
                ArticuloNegocio negocio = new ArticuloNegocio();

                nuevo.Codigo = txtCodigo.Text;
                nuevo.Nombre = txtNombre.Text;
                nuevo.Descripcion = txtDescripcion.Text;

                //PROVISORIO ARREGLAR
                //nuevo.Ganancia = decimal.Parse(numGanancia.Value);
                nuevo.Ganancia = decimal.Parse(numGanancia.Value) / 100;

                nuevo.Imagen = txtImagenUrl.Text;
                nuevo.StockMin = int.Parse(numStockMinimo.Value);
                nuevo.Activo = true;
                setearMarcaYCategoria(nuevo);


                if (Request.QueryString["ID"] != null)
                {
                    nuevo.ID = long.Parse(Request.QueryString["ID"]);
                    string verificarDuplicadoCodigo = negocio.VerificarArticulo(nuevo.Codigo, nuevo.Nombre, nuevo.ID);
                    if (verificarDuplicadoCodigo != null)
                    {
                        lblError.Text = verificarDuplicadoCodigo;
                        lblError.Visible = true;
                        return;
                    }
                    negocio.Modificar(nuevo);
                }
                else
                {
                    string verificarDuplicado = negocio.VerificarArticulo(nuevo.Codigo, nuevo.Nombre);
                    if (verificarDuplicado != null)
                    {
                        lblError.Text = verificarDuplicado;
                        lblError.Visible = true;
                        return;
                    }
                    negocio.Agregar(nuevo);
                }

                LimpiarCampos();
                Response.Redirect("Default.aspx", false);
            }
            catch
            {
                Response.Redirect("Error.aspx");
            }
        }

        private void LimpiarCampos()
        {
            txtCodigo.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtImagenUrl.Text = "";
            numGanancia.Value = "";
            numStockMinimo.Value = "";
            ddlMarca.SelectedIndex = -1;
            ddlCategoria.SelectedIndex = -1;
            imgArticulos.ImageUrl = "https://grupoact.com.ar/wp-content/uploads/2020/04/placeholder.png";
        }

        protected void txtImagenUrl_TextChanged(object sender, EventArgs e)
        {
            imgArticulos.ImageUrl = txtImagenUrl.Text;
        }

        protected void BtnInactivar_Click(object sender, EventArgs e)
        {
            ConfirmarInactivar = true;
            ConfirmarReactivar = false;
        }

        protected void btnConfirmaInactivar_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkConfirmaInactivacion.Checked)
                {
                    long id = Convert.ToInt64(Request.QueryString["ID"]);
                    ArticuloNegocio negocio = new ArticuloNegocio();
                    negocio.EliminarLogico(id, false);
                    Response.Redirect("Default.aspx", false);
                }
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnReactivar_Click(object sender, EventArgs e)
        {
            ConfirmarInactivar = false;
            ConfirmarReactivar = true;
        }

        protected void btnConfirmaReactivar_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkConfirmaReactivacion.Checked)
                {
                    if (!ValidarCampos())
                    {
                        return;
                    }

                    Articulo nuevo = new Articulo();
                    ArticuloNegocio negocio = new ArticuloNegocio();

                    nuevo.ID = long.Parse(Request.QueryString["ID"]);
                    nuevo.Codigo = txtCodigo.Text;
                    nuevo.Nombre = txtNombre.Text;
                    nuevo.Descripcion = txtDescripcion.Text;
                    nuevo.Ganancia = decimal.Parse(numGanancia.Value);
                    nuevo.Imagen = txtImagenUrl.Text;
                    nuevo.StockMin = int.Parse(numStockMinimo.Value);
                    nuevo.Activo = true;
                    setearMarcaYCategoria(nuevo);

                    negocio.ReactivarModificar(nuevo);
                    Response.Redirect("Default.aspx", false);
                }
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx");
            }
        }

        private void setearMarcaYCategoria(Articulo arti)
        {
            arti.Marca = new Marca();
            string marcaIDString = ddlMarca.SelectedValue;
            if (!string.IsNullOrEmpty(marcaIDString))
            {
                arti.Marca.ID = int.Parse(marcaIDString);
            }
            arti.Categoria = new Categoria();
            string categoriaIDString = ddlCategoria.SelectedValue;
            if (!string.IsNullOrEmpty(categoriaIDString))
            {
                arti.Categoria.ID = int.Parse(categoriaIDString);
            }
        }

        protected void CargarMarcasYCategoriasActivas()
        {
            MarcaNegocio negocioMarca = new MarcaNegocio();
            List<Marca> listaMarca = negocioMarca.Listar().Where(mar => mar.Activo).ToList();

            ddlMarca.DataSource = listaMarca;
            ddlMarca.DataValueField = "ID";
            ddlMarca.DataTextField = "Descripcion";
            ddlMarca.DataBind();

            CategoriaNegocio negocioCategoria = new CategoriaNegocio();
            List<Categoria> listaCategoria = negocioCategoria.Listar().Where(cat => cat.Activo).ToList();

            ddlCategoria.DataSource = listaCategoria;
            ddlCategoria.DataValueField = "ID";
            ddlCategoria.DataTextField = "Descripcion";
            ddlCategoria.DataBind();

        }

        protected void CargarMarcasYCategoriasTodas()
        {
            MarcaNegocio negocioMarca = new MarcaNegocio();
            List<Marca> listaMarca = negocioMarca.Listar();

            ddlMarca.DataSource = listaMarca;
            ddlMarca.DataValueField = "ID";
            ddlMarca.DataTextField = "Descripcion";
            ddlMarca.DataBind();

            CategoriaNegocio negocioCategoria = new CategoriaNegocio();
            List<Categoria> listaCategoria = negocioCategoria.Listar();

            ddlCategoria.DataSource = listaCategoria;
            ddlCategoria.DataValueField = "ID";
            ddlCategoria.DataTextField = "Descripcion";
            ddlCategoria.DataBind();
        }

        protected bool ValidarSesion()
        {
            if (Session["Usuario"] != null && ((Usuario)Session["Usuario"]).TipoUsuario == TipoUsuario.ADMIN)
            {
                return true;
            }
            return false;
        }

    }
}