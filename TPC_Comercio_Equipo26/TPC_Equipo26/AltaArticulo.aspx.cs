using System;
using System.Collections.Generic;
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
                    for (int i = 1; i <= 10; i++)
                    {
                        ddlStock.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                    for (int i = 1; i <= 10; i++)
                    {
                        ddlStockMinimo.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }

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
                }
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx");
            }
        }
        protected void btnAceptar_Click(object sender, EventArgs e)
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
                nuevo.Precio = decimal.Parse(txtPrecio.Text);
                nuevo.Stock = int.Parse(ddlStock.SelectedValue);
                nuevo.StockMin = int.Parse(ddlStockMinimo.SelectedValue);
                nuevo.Activo = true;

                Imagen imagen = new Imagen();
                imagen.UrlImagen = txtImagenUrl.Text;
                imagen.Activo = true;
                nuevo.Imagenes = new List<Imagen> { imagen };

                negocio.agregar(nuevo);
                Response.Redirect("Articulos.aspx", false);
            }
            catch
            {
                Response.Redirect("Error.aspx");
            }
        }
        protected void txtImagenUrl_TextChanged(object sender, EventArgs e)
        {
            //actualiza la URL de la imagen cada vez que cambia el texto
            imgArticulos.ImageUrl = txtImagenUrl.Text;        
        }

    }
}