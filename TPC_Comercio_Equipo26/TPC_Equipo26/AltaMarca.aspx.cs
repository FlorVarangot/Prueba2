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
    public partial class AltaMarca : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtID.Enabled = false;
            try
            {
                if (!IsPostBack)
                {
                    ProveedorNegocio negocio = new ProveedorNegocio();
                    List<Proveedor> proveedores = negocio.Listar();
                    
                    ddlProveedor.DataSource = proveedores;
                    ddlProveedor.DataValueField = "ID";
                    ddlProveedor.DataTextField = "Nombre";
                    ddlProveedor.DataBind();

                    if (Request.QueryString["ID"] != null)
                    {
                        lblTituloModificar.Visible = true;
                        int idMarca = int.Parse(Request.QueryString["ID"]);
                        PrecargarDatos(idMarca);
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
                Marca marca = new Marca();
                MarcaNegocio negocio = new MarcaNegocio();

                marca.Descripcion = txtDescripcion.Text;
                marca.ImagenUrl = txtImagenUrl.Text;
                marca.IdProveedor = int.Parse(ddlProveedor.SelectedValue);

                if (Request.QueryString["ID"] != null)
                {
                    marca.ID = int.Parse(Request.QueryString["ID"]);
                    negocio.Modificar(marca);
                }
                else
                {
                    negocio.Agregar(marca);
                }

                LimpiarCampos();
                Response.Redirect("Marcas.aspx", false);
            }
            catch
            {
                Response.Redirect("Error.aspx");
            }
        }
        private void LimpiarCampos()
        {
            txtDescripcion.Text = "";
            txtImagenUrl.Text = "";
            ddlProveedor.SelectedIndex = 0;
            imgMarcas.ImageUrl = "https://grupoact.com.ar/wp-content/uploads/2020/04/placeholder.png";
        }

        protected void txtImagenUrl_TextChanged(object sender, EventArgs e)
        {
            imgMarcas.ImageUrl = txtImagenUrl.Text;
        }

        protected void BtnInactivar_Click(object sender, EventArgs e)
        {
            try
            {
                MarcaNegocio negocio = new MarcaNegocio();
                negocio.EliminarLogico(int.Parse(txtID.Text));
                Response.Redirect("Marcas.aspx");

            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
            }
        }

        private void PrecargarDatos(int Id)
        {
            MarcaNegocio negocio = new MarcaNegocio();
            Marca marca = negocio.ObtenerMarcaPorId(Id);

            if (marca != null)
            {
                txtID.Text = marca.ID.ToString();
                txtDescripcion.Text = marca.Descripcion;
                txtImagenUrl.Text = marca.ImagenUrl.ToString();

                ddlProveedor.SelectedValue = marca.IdProveedor.ToString();

                if (marca.ImagenUrl != null)
                {
                    txtImagenUrl.Text = marca.ImagenUrl.ToString();
                    imgMarcas.ImageUrl = marca.ImagenUrl.ToString();
                }
                else
                {
                    txtImagenUrl.Text = "";
                    imgMarcas.ImageUrl = "https://grupoact.com.ar/wp-content/uploads/2020/04/placeholder.png";
                }
            }
            else
            {
                Response.Redirect("Error.aspx");
            }
        }




    }
}