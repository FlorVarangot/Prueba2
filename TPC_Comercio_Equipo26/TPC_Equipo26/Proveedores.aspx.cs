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
    public partial class Proveedores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProveedores();
            }
        }

        private void CargarProveedores()
        {
            ChkIncluirInactivos.Checked = true;
            ProveedorNegocio negocio = new ProveedorNegocio();
            List<Proveedor> proveedores = negocio.Listar();

            Session["listaProveedores"] = proveedores;
            GvProveedores.DataSource = proveedores;
            if (proveedores != null)
            {
                lblVacio.Visible = false;
            }
            GvProveedores.DataBind();
        }

        private void FiltrarProveedores()
        {
            List<Proveedor> lista = (List<Proveedor>)Session["ListaProveedores"];
            string filtro = TxtFiltro.Text.Trim().ToUpper();
            bool incluirInactivos = ChkIncluirInactivos.Checked;

            List<Proveedor> listaFiltrada;

            if (lista != null)
            {
                if (string.IsNullOrEmpty(filtro))
                {
                    listaFiltrada = incluirInactivos
                        ? lista
                        : lista.FindAll(x => x.Activo);
                }
                else
                {
                    listaFiltrada = lista.FindAll(x =>
                        x.Nombre.ToUpper().Contains(filtro) ||
                        x.CUIT.ToUpper().Contains(filtro) ||
                        x.Email.ToUpper().Contains(filtro) ||
                        x.Telefono.ToUpper().Contains(filtro) ||
                        x.Direccion.ToUpper().Contains(filtro) &&
                        (x.Activo || incluirInactivos));
                }

                if (listaFiltrada.Count > 0)
                {
                    lblVacio.Visible = false;
                }
                else
                {
                    lblVacio.Visible = true;
                }

                GvProveedores.DataSource = listaFiltrada;
            }
            else
            {
                GvProveedores.DataSource = lista;
            }
            GvProveedores.DataBind();
        }

        protected void BtnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            LimpiarFiltros();
        }

        protected void GvProveedores_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvProveedores.PageIndex = e.NewPageIndex;
            GvProveedores.DataSource = Session["ListaProveedores"];
            GvProveedores.DataBind();
        }

        protected void GvProveedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = GvProveedores.SelectedDataKey.Value.ToString();
            Response.Redirect("AltaProveedor.aspx?ID=" + id);
        }

        protected void FiltroInactivos_CheckedChanged(object sender, EventArgs e)
        {
            FiltrarProveedores();
        }
        protected void TxtFiltro_TextChanged(object sender, EventArgs e)
        {
            FiltrarProveedores();
        }

        public void LimpiarFiltros()
        {
            TxtFiltro.Text = string.Empty;
            ChkIncluirInactivos.Checked = false;
            CargarProveedores();
        }

    }
}