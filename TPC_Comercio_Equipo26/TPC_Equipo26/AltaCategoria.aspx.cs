using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPC_Equipo26.Dominio;
using TPC_Equipo26.Negocio;

namespace TPC_Equipo26
{
    public partial class AltaCategoria : System.Web.UI.Page
    {
        public bool ConfirmarInactivar { get; set; }
        public bool ConfirmarReactivar { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            ConfirmarInactivar = false;
            ConfirmarReactivar = false;
            if (!IsPostBack)
            {
                if (Request.QueryString["ID"] != null)
                {
                    int idCategoria = int.Parse(Request.QueryString["ID"]);
                    CargarDatosCategoria(idCategoria);
                    lblTituloModificar.Visible = true;
                }
                else
                {
                    lblTituloAgregar.Visible = true;
                    LimpiarCampos();
                }
            }
        }

        private void CargarDatosCategoria(int Id)
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            Categoria categoria = negocio.ObtenerCategoriaPorId(Id);

            if (categoria != null)
            {
                txtID.Text = categoria.ID.ToString();
                txtDescripcion.Text = categoria.Descripcion;
            }
        }
        private void LimpiarCampos()
        {
            txtID.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
        }

        protected void BtnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Categoria categoria = new Categoria();
                CategoriaNegocio negocio = new CategoriaNegocio();

                categoria.Descripcion = txtDescripcion.Text;
                categoria.Activo = true;
                if (Request.QueryString["ID"] != null)
                {
                    categoria.ID = int.Parse(Request.QueryString["ID"]);
                    negocio.Modificar(categoria);
                }
                else
                {
                    negocio.Agregar(categoria);
                }

                LimpiarCampos();
                Response.Redirect("Categorias.aspx", false);
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx");
            }
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
                    CategoriaNegocio negocio = new CategoriaNegocio();
                    negocio.EliminarLogico(int.Parse(txtID.Text), false);
                    Response.Redirect("Categorias.aspx", false);
                }
            }
            catch (Exception)
            {
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
                    CategoriaNegocio negocio = new CategoriaNegocio();
                    negocio.EliminarLogico(int.Parse(txtID.Text), true);
                    Response.Redirect("Categorias.aspx", false);
                }
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx",false);
            }
        }
    }
}