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
            ValidarAdmin();
            try
            {
                if (!IsPostBack)
                {
                    ConfirmarInactivar = false;
                    ConfirmarReactivar = false;

                    if (Request.QueryString["ID"] != null)
                    {
                        CargarProveedoresTodos();
                        lblTituloModificar.Visible = true;
                        int idMarca = int.Parse(Request.QueryString["ID"]);
                        PrecargarDatos(idMarca);
                    }
                    else
                    {
                        CargarProveedoresActivos();
                        lblTituloAgregar.Visible = true;
                        BtnInactivar.Visible = false;
                        BtnReactivar.Visible = false;
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

        private void PrecargarDatos(int Id)
        {
            try
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
                    Session.Add("Error", "No se encontró la marca.");
                    Response.Redirect("Error.aspx");
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
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
            bool camposValidos = true;

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                lblDescripcion.Visible = false;
            }
            else
            {
                lblDescripcion.Visible = false;
            }

            if (ddlProveedor.SelectedValue == "-1")
            {
                lblProveedor.Visible = true;
                camposValidos = false;
            }
            else
            {
                lblProveedor.Visible = false;
            }

            if (string.IsNullOrWhiteSpace(txtImagenUrl.Text))
            {
                lblImagenUrl.Visible = true;
                camposValidos = false;
            }
            else
            {
                lblImagenUrl.Visible = false;
            }

            if (!camposValidos)
            {
                lblError.Text = "Todos los campos  deben ser completados";
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

                Marca marca = new Marca();
                MarcaNegocio negocio = new MarcaNegocio();

                marca.Descripcion = txtDescripcion.Text;
                marca.ImagenUrl = txtImagenUrl.Text;
                marca.IdProveedor = int.Parse(ddlProveedor.SelectedValue);

                string mensaje;
                if (Request.QueryString["ID"] != null)
                {
                    marca.ID = int.Parse(Request.QueryString["ID"]);
                    string verificarDuplicado = negocio.VerificarMarca(marca.Descripcion, marca.ID);
                    if (verificarDuplicado != null)
                    {
                        lblError.Text = verificarDuplicado;
                        lblError.Visible = true;
                        return;
                    }
                    negocio.Modificar(marca);
                    mensaje = "Marca modificada con éxito";
                }
                else
                {
                    string verificarDuplicado = negocio.VerificarMarca(marca.Descripcion);
                    if (verificarDuplicado != null)
                    {
                        lblError.Text = verificarDuplicado;
                        lblError.Visible = true;
                        return;
                    }
                    negocio.Agregar(marca);
                    mensaje = "Marca agregada con éxito";
                }

                LimpiarCampos();
                ClientScript.RegisterStartupScript(GetType(), "mostrarMensajeExito", $"alert('{mensaje}'); window.location.href = 'Marcas.aspx';", true);
              
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
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
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
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
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void CargarProveedoresActivos()
        {
            ProveedorNegocio negocio = new ProveedorNegocio();
            List<Proveedor> proveedores = negocio.Listar().Where(prov => prov.Activo).ToList();

            proveedores = proveedores.OrderBy(p => p.Nombre).ToList();
            
            ddlProveedor.DataSource = proveedores;
            ddlProveedor.DataValueField = "ID";
            ddlProveedor.DataTextField = "Nombre";
            ddlProveedor.DataBind();
        }

        protected void CargarProveedoresTodos()
        {
            ProveedorNegocio negocio = new ProveedorNegocio();
            List<Proveedor> proveedores = negocio.Listar();
            
            proveedores = proveedores.OrderBy(p => p.Nombre).ToList();
            ddlProveedor.DataSource = proveedores;
            ddlProveedor.DataValueField = "ID";
            ddlProveedor.DataTextField = "Nombre";
            ddlProveedor.DataBind();
        }

        protected void ValidarAdmin()
        {
            if (!Seguridad.esAdmin(Session["Usuario"]))
            {
                Session.Add("Error", "No tenes permisos para ingresar a esta pantalla.");
                Response.Redirect("Error.aspx", false);
            }
        }


    }
}