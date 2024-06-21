using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
            try
            {
                if (!IsPostBack)
                {
                    ConfirmarInactivar = false;
                    ConfirmarReactivar = false;

                    ///Cargo los desplegables para marcas
                    MarcaNegocio negocioMarca = new MarcaNegocio();

                    //F: Ahora si va a modificar sólo trae las marcas y categorias Activas.
                    List<Marca> listaMarca = negocioMarca.Listar().Where(mar => mar.Activo).ToList();
                    //List<Marca> listaMarca = negocioMarca.Listar();

                    ddlMarca.DataSource = listaMarca;
                    ddlMarca.DataValueField = "ID";
                    ddlMarca.DataTextField = "Descripcion";
                    ddlMarca.DataBind();

                    ///lo mismo para categoria
                    CategoriaNegocio negocioCategoria = new CategoriaNegocio();
                    List<Categoria> listaCategoria = negocioCategoria.Listar().Where(cat => cat.Activo).ToList();
                    //List<Categoria> listaCategoria = negocioCategoria.Listar();


                    ddlCategoria.DataSource = listaCategoria;
                    ddlCategoria.DataValueField = "ID";
                    ddlCategoria.DataTextField = "Descripcion";
                    ddlCategoria.DataBind();

                    if (Request.QueryString["ID"] != null)
                    {
                        lblTituloModificar.Visible = true;
                        long idArticulo = long.Parse(Request.QueryString["ID"]);
                        CargarDatosArticulo(idArticulo);
                    }
                    else
                    {
                        lblTituloAgregar.Visible = true;
                        BtnInactivar.Visible = false;
                        btnReactivar.Visible = false;
                        LimpiarCampos();
                    }
                }
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx");
            }
        }

        protected void BtnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Articulo nuevo = new Articulo();
                ArticuloNegocio negocio = new ArticuloNegocio();

                nuevo.Codigo = txtCodigo.Text;
                nuevo.Nombre = txtNombre.Text;
                nuevo.Descripcion = txtDescripcion.Text;

                nuevo.Marca = new Marca();
                string marcaIDString = ddlMarca.SelectedValue;
                if (!string.IsNullOrEmpty(marcaIDString))
                {
                    nuevo.Marca.ID = int.Parse(marcaIDString);
                }
                nuevo.Categoria = new Categoria();
                string categoriaIDString = ddlCategoria.SelectedValue;
                if (!string.IsNullOrEmpty(categoriaIDString))
                {
                    nuevo.Categoria.ID = int.Parse(categoriaIDString);
                }

                nuevo.Ganancia = decimal.Parse(numGanancia.Value);
                nuevo.StockMin = int.Parse(numStockMinimo.Value);
                nuevo.Imagen = txtImagenUrl.Text;
                nuevo.Activo = true;

                if (Request.QueryString["ID"] != null)
                {
                    nuevo.ID = long.Parse(Request.QueryString["ID"]);
                    negocio.Modificar(nuevo);
                }
                else
                {
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
            //actualiza la URL de la imagen cada vez que cambia el texto
            imgArticulos.ImageUrl = txtImagenUrl.Text;
        }
        //Inactivar
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

        //Reactivar
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
                    long id = Convert.ToInt64(Request.QueryString["ID"]);
                    ArticuloNegocio negocio = new ArticuloNegocio();
                    negocio.EliminarLogico(id, true);
                    Response.Redirect("Default.aspx", false);
                }
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx");
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

                numGanancia.Value = articulo.Ganancia.ToString();
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
    }
}