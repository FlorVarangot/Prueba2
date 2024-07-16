using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Policy;
using System.Web;
using System.Web.Services.Description;
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
            ValidarAdmin();
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
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        private void CargarDatosArticulo(long idArticulo)
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                Articulo articulo = negocio.ObtenerArticuloPorID(idArticulo);

                if (articulo != null)
                {
                    txtCodigo.Text = articulo.Codigo;
                    txtNombre.Text = articulo.Nombre;
                    txtDescripcion.Text = articulo.Descripcion;
                    txtImagenUrl.Text = articulo.Imagen;

                    if (articulo.Ganancia > 1)
                    {                      
                        numGanancia.Value = articulo.Ganancia.ToString(CultureInfo.InvariantCulture);
                    }
                    else
                    {                      
                        numGanancia.Value = (articulo.Ganancia * 100).ToString(CultureInfo.InvariantCulture);
                    }
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
                    Session.Add("Error", "El artículo no se encontró");
                    Response.Redirect("Error.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
                Response.Redirect("Error.aspx", false);
            }
        }

        private bool ValidarCampos()
        {
            bool camposValidos = true;

            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                lblCodigo.Text = "El código no puede estar vacío";
                lblCodigo.Visible = true;
                camposValidos = false;
            }
            else if (!ValidarCodigo())
            {
                camposValidos = false;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                lblNombre.Text = "El nombre no puede estar vacío";
                lblNombre.Visible = true;
                camposValidos = false;
            }
            else if (!ValidarNombre())
            {
                camposValidos = false;
            }

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                lblDescripcion.Text = "La descripción no puede estar vacía";
                lblDescripcion.Visible = true;
                camposValidos = false;
            }
            else if (!ValidarDescripcion())
            {
                camposValidos = false;
            }

            if (string.IsNullOrWhiteSpace(numGanancia.Value))
            {
                lblGanancia.Text = "El porcentaje de ganancia no puede estar vacío";
                lblGanancia.Visible = true;
                camposValidos = false;
            }
            else if (!ValidarGanancia())
            {
                camposValidos = false;
            }

            if (string.IsNullOrWhiteSpace(numStockMinimo.Value))
            {
                lblStockMinimo.Text = "El stock mínimo no puede estar vacío";
                lblStockMinimo.Visible = true;
                camposValidos = false;
            }
            else if (!ValidarStockMin())
            {
                camposValidos = false;
            }

            if (ddlMarca.SelectedValue == "-1")
            {
                lblMarca.Text = "Debe seleccionar una marca";
                lblMarca.Visible = true;
            }
            else
            {
                lblMarca.Visible = false;
            }

            if (ddlCategoria.SelectedValue == "-1")
            {
                lblCategoria.Text = "Debe seleccionar una categoría";
                lblCategoria.Visible = true;
            }
            else
            {
                lblCategoria.Visible = false;
            }

            return camposValidos;
        }

        protected void BtnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarLabels();
                if (!ValidarCampos())
                {
                    return;
                }
                Articulo nuevo = new Articulo();
                ArticuloNegocio negocio = new ArticuloNegocio();

                nuevo.Codigo = txtCodigo.Text;
                nuevo.Nombre = txtNombre.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                decimal ganancia;
                if (decimal.TryParse(numGanancia.Value, NumberStyles.Number, CultureInfo.InvariantCulture, out ganancia))
                {
                    if (ganancia < 1)
                    {
                        ganancia *= 100;
                    }
                    nuevo.Ganancia = ganancia;
                }
                else
                {
                    lblError.Text = "Formato de ganancia inválido";
                    lblError.Visible = true;
                    return;
                }

                nuevo.Imagen = txtImagenUrl.Text;
                nuevo.StockMin = int.Parse(numStockMinimo.Value);
                nuevo.Activo = true;
                setearMarcaYCategoria(nuevo);

                #pragma warning disable CS0219 // La variable está asignada pero nunca se usa su valor
                string mensaje;
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
                    mensaje = "Artículo modificado con éxito";
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
                    mensaje = "Artículo agregado con éxito";
                }

                LimpiarCampos();
                ScriptManager.RegisterStartupScript(this, GetType(), "showSuccessMessage", $"mostrarMensajeExitoArticulo('{mensaje}');", true);
                //Response.Redirect("Default.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
                Response.Redirect("Error.aspx", false);
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

        private void LimpiarLabels()
        {
            lblCodigo.Text = "";
            lblNombre.Text = "";
            lblDescripcion.Text = "";
            lblMarca.Text = "";
            lblCategoria.Text = "";
            lblGanancia.Text = "";
            lblStockMinimo.Text = "";
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
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
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
                    decimal ganancia;
                    if (decimal.TryParse(numGanancia.Value, NumberStyles.Number, CultureInfo.InvariantCulture, out ganancia))
                    {
                        if (ganancia < 1)
                        {
                            ganancia *= 100;
                        }
                        nuevo.Ganancia = ganancia;
                    }
                    else
                    {
                        lblError.Text = "Formato de ganancia inválido";
                        lblError.Visible = true;
                        return;
                    }
                    nuevo.Imagen = txtImagenUrl.Text;
                    nuevo.StockMin = int.Parse(numStockMinimo.Value);
                    nuevo.Activo = true;
                    setearMarcaYCategoria(nuevo);

                    negocio.ReactivarModificar(nuevo);
                    Response.Redirect("Default.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
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
            try
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
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }


        }

        protected void CargarMarcasYCategoriasTodas()
        {
            try
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
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void ValidarAdmin()
        {
            if (!Seguridad.esAdmin(Session["Usuario"]))
            {
                Session.Add("Error", "No tenes permisos para ingresar a esta pantalla.");
                Response.Redirect("Error.aspx", false);
            }
        }

        private bool ValidarCodigo()
        {
            bool isValid = true;

            if ((txtCodigo.Text.Length) > 6 || !System.Text.RegularExpressions.Regex.IsMatch(txtCodigo.Text, "^[a-zA-Z0-9-]+$"))
            {
                lblCodigo.Text = "El código puede contener hasta 6 caracteres, letras y/o números";
                lblCodigo.Visible = true;
                isValid = false;
            }

            return isValid;
        }

        private bool ValidarNombre()
        {
            bool isValid = true;

            if (!System.Text.RegularExpressions.Regex.IsMatch(txtNombre.Text, "^[a-zA-Z0-9\\s-]+$"))
            {
                lblNombre.Text = "El nombre no puede contener números o caracteres especiales";
                lblNombre.Visible = true;
                isValid = false;
            }

            return isValid;
        }

        private bool ValidarDescripcion()
        {
            bool isValid = true;

            if (System.Text.RegularExpressions.Regex.IsMatch(txtNombre.Text, "^[A-Za-z0-9]$"))
            {
                lblDescripcion.Text = "La descripción no puede contener caracteres especiales";
                lblDescripcion.Visible = true;
                isValid = false;
            }

            return isValid;
        }

        private bool ValidarGanancia()
        {
            decimal ganancia;
            if (!decimal.TryParse(numGanancia.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out ganancia) || ganancia < 0 || ganancia > 99.99m)
            {
                lblGanancia.Text = "La ganancia debe estar entre 0 y 99.99";
                lblGanancia.Visible = true;
                return false;
            }
            return true;
        }

        private bool ValidarStockMin()
        {
            bool isValid = true;

            if ((int.Parse(numStockMinimo.Value) <= 0 || !System.Text.RegularExpressions.Regex.IsMatch(numStockMinimo.Value.ToString(), "^[0-9]+$")))
            {
                lblStockMinimo.Text = "El stock mínimo no puede contener letras o ser menor o igual 0";
                lblStockMinimo.Visible = true;
                isValid = false;
            }
            return isValid;
        }

    }
}