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
       
        protected void Page_Load(object sender, EventArgs e)
        {
            txtID.Enabled = false;
            try
            {
                if (!IsPostBack)
                {
                    //No limitaría el stock a 10. Debieran ser numeros más grandes.
                    //Aunque El STOCK se maneja desde COMPRAS a PROVEEDORES, no desde ALTA ARTICULO.
                    //Solo debería ir StockMin acá

                    for (int i = 1; i <= 10; i++)
                    {
                        ddlStockMinimo.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }

                    //F:La carga de desplegables de MARCAS y CATEGORIAS lo metería en Métodos().

                    ///Cargo los desplegables para marcas
                    MarcaNegocio negocioMarca = new MarcaNegocio();
                    List<Marca> listaMarca = negocioMarca.Listar();

                    ddlMarca.DataSource = listaMarca;
                    ddlMarca.DataValueField = "ID";
                    ddlMarca.DataTextField = "Descripcion";
                    ddlMarca.DataBind();

                    ///lo mismo para categoria
                    CategoriaNegocio negocioCategoria = new CategoriaNegocio();
                    List<Categoria> listaCategoria = negocioCategoria.Listar();

                    ddlCategoria.DataSource = listaCategoria;
                    ddlCategoria.DataValueField = "ID";
                    ddlCategoria.DataTextField = "Descripcion";
                    ddlCategoria.DataBind();

                    if (Request.QueryString["ID"] != null)
                    {
                        lblTituloModificar.Visible = true;
                        int idArticulo = int.Parse(Request.QueryString["ID"]);
                        CargarDatosArticulo(idArticulo);
                    }
                    else
                    {                 
                        lblTituloAgregar.Visible = true;
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

                nuevo.Ganancia = decimal.Parse(txtGanancia.Text);
                nuevo.StockMin = int.Parse(ddlStockMinimo.SelectedValue);
                nuevo.Imagen = txtImagenUrl.Text;
                nuevo.Activo = true;

                //Imagen imagen = new Imagen();
                //imagen.UrlImagen = txtImagenUrl.Text;
                //imagen.Activo = true;

                //nuevo.Imagenes = new List<Imagen> { imagen };

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
            txtGanancia.Text = "";
            txtImagenUrl.Text = "";
            ddlMarca.SelectedIndex = 0;
            ddlCategoria.SelectedIndex = 0;
            ddlStockMinimo.SelectedIndex = 0;
            imgArticulos.ImageUrl = "https://grupoact.com.ar/wp-content/uploads/2020/04/placeholder.png";
        }

        protected void txtImagenUrl_TextChanged(object sender, EventArgs e)
        {
            //actualiza la URL de la imagen cada vez que cambia el texto
            imgArticulos.ImageUrl = txtImagenUrl.Text;
        }


        protected void BtnInactivar_Click(object sender, EventArgs e)
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();                
                negocio.EliminarLogico(int.Parse(txtID.Text));
                Response.Redirect("Default.aspx");

            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
            }
        }

        private void CargarDatosArticulo(int idArticulo)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo articulo = negocio.ObtenerArticuloPorID(idArticulo);
            
            if(articulo != null)
            {
                txtID.Text = articulo.ID.ToString();
                txtCodigo.Text = articulo.Codigo;
                txtNombre.Text = articulo.Nombre;
                txtDescripcion.Text = articulo.Descripcion;
                txtGanancia.Text = articulo.Ganancia.ToString();
                txtImagenUrl.Text = articulo.Imagen.ToString();

                ddlMarca.SelectedValue = articulo.Marca.ID.ToString();
                ddlCategoria.SelectedValue = articulo.Categoria.ID.ToString();

                ddlStockMinimo.SelectedValue = articulo.StockMin.ToString();
                
                if (articulo.Imagen != null)
                {
                    txtImagenUrl.Text = articulo.Imagen.ToString();
                    imgArticulos.ImageUrl = articulo.Imagen.ToString();
                } else {
                    txtImagenUrl.Text = "";
                    imgArticulos.ImageUrl = "https://grupoact.com.ar/wp-content/uploads/2020/04/placeholder.png";
                }

                //List<Imagen> imagenes = negocio.ObtenerImagenesPorID(articulo.ID);
                //if (imagenes != null && imagenes.Count > 0)
                //{
                //    txtImagenUrl.Text = imagenes[0].UrlImagen;
                //    imgArticulos.ImageUrl = imagenes[0].UrlImagen;
                //}
                //else
                //{
                //    txtImagenUrl.Text = "";
                //    imgArticulos.ImageUrl = "https://grupoact.com.ar/wp-content/uploads/2020/04/placeholder.png";
                //}
            }
            else
            {
                Response.Redirect("Error.aspx");
            }
        }

    }
}