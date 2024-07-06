using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        public bool ConfirmarInactivar { get; set; }
        public bool ConfirmarReactivar { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ProveedorNegocio negocio = new ProveedorNegocio();
                    List<Proveedor> proveedores = negocio.Listar().Where(prov => prov.Activo).ToList();

                    ddlProveedor.DataSource = proveedores;
                    ddlProveedor.DataValueField = "ID";
                    ddlProveedor.DataTextField = "Nombre";
                    ddlProveedor.DataBind();

                    ConfirmarInactivar = false;
                    ConfirmarReactivar = false;

                    if (Request.QueryString["ID"] != null)
                    {
                        lblTituloModificar.Visible = true;
                        int idMarca = int.Parse(Request.QueryString["ID"]);
                        PrecargarDatos(idMarca);
                    }
                    else
                    {
                        lblTituloAgregar.Visible = true;
                        BtnInactivar.Visible = false;
                        BtnReactivar.Visible = false;
                        LimpiarCampos();
                    }
                }
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx");
            }
        }

        private void PrecargarDatos(int Id)
        {
            MarcaNegocio negocio = new MarcaNegocio();
            Marca marca = negocio.ObtenerMarcaPorId(Id);

            if (marca != null)
            {
                txtDescripcion.Text = marca.Descripcion;
                txtImagenUrl.Text = marca.ImagenUrl ?? string.Empty; 
                imgMarcas.ImageUrl = !string.IsNullOrEmpty(marca.ImagenUrl) ? marca.ImagenUrl : "https://grupoact.com.ar/wp-content/uploads/2020/04/placeholder.png";

                ddlProveedor.SelectedValue = marca.IdProveedor.ToString();            

                if (marca.Activo == true)
                {
                    BtnInactivar.Visible = true;
                    BtnReactivar.Visible = false;
                }
                else
                {
                    BtnInactivar.Visible = false;
                    BtnReactivar.Visible = true;
                }

            }
            else
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

        protected void TxtImagenUrl_TextChanged(object sender, EventArgs e)
        {
            imgMarcas.ImageUrl = txtImagenUrl.Text;
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text) || string.IsNullOrWhiteSpace(txtImagenUrl.Text) || ddlProveedor.SelectedValue == "-1")
            {
                lblError.Text = "Todos los campos deben ser completados.";
                lblError.Visible = true;
                return false;
            }
            return true;
        }
        
        protected void BtnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos())
                {
                    return;
                }

                Marca marca = new Marca();
                MarcaNegocio negocio = new MarcaNegocio();

                marca.Descripcion = txtDescripcion.Text;
                marca.ImagenUrl = txtImagenUrl.Text;
                marca.IdProveedor = int.Parse(ddlProveedor.SelectedValue);

                string verificarDuplicado = negocio.VerificarMarca(marca.Descripcion, marca.IdProveedor);
                if (verificarDuplicado != null)
                {
                    lblError.Text = verificarDuplicado;
                    lblError.Visible = true;
                    return; 
                }
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
            catch (Exception)
            {
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void BtnInactivar_Click(object sender, EventArgs e)
        {

            ConfirmarInactivar = true;
            ConfirmarReactivar = false;
        }

        protected void BtnConfirmaInactivar_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkConfirmaInactivacion.Checked)
                {
                    int id = Convert.ToInt32(Request.QueryString["ID"]);
                    MarcaNegocio negocio = new MarcaNegocio();
                    negocio.ActivarLogico(id, false);
                    Response.Redirect("Marcas.aspx", false);
                }
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void BtnReactivar_Click(object sender, EventArgs e)
        {
            ConfirmarInactivar = false;
            ConfirmarReactivar = true;
        }

        protected void BtnConfirmaReactivar_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkConfirmaReactivacion.Checked)
                {
                    if (!ValidarCampos())
                    {
                        return;
                    }

                    Marca marca = new Marca();
                    MarcaNegocio negocio = new MarcaNegocio();

                    marca.ID = int.Parse(Request.QueryString["ID"]);
                    marca.Descripcion = txtDescripcion.Text;
                    marca.ImagenUrl = txtImagenUrl.Text;
                    marca.IdProveedor = int.Parse(ddlProveedor.SelectedValue);
                    marca.Activo = true;

                    negocio.ReactivarModificar(marca);
                    Response.Redirect("Marcas.aspx", false);
                }
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx", false);
            }
        }

    }
}