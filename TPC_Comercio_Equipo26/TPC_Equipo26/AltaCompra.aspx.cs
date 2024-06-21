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
    public partial class Compras : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProveedores();
                CargarMarcas();
                CargarArticulos();
                //esto muestra la fecha actual
               // txtFechaCompra.Text = DateTime.Now.ToString("yyyy-MM-dd");

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
        
    }
}