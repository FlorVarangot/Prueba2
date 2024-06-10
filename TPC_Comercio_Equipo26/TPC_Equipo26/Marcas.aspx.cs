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
    public partial class Marcas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                MarcaNegocio marcaNegocio = new MarcaNegocio();
                Session.Add("ListaMarcas", marcaNegocio.Listar());
                gvMarcas.DataSource = Session["ListaMarcas"];
                gvMarcas.DataBind();

                ProveedorNegocio proveedorNegocio = new ProveedorNegocio();
                List<Proveedor> proveedor = proveedorNegocio.Listar();

                ddlProveedor.DataSource = proveedor;
                ddlProveedor.DataTextField = "Nombre";
                ddlProveedor.DataValueField = "ID";
                ddlProveedor.DataBind();
                ddlProveedor.Items.Insert(0, new ListItem("Seleccionar Proveedor", "0"));
            }
        }

        protected void Filtro_TextChanged(object sender, EventArgs e)
        {
            FiltrarMarcas();
        }

        protected void FiltroProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarMarcas();
        }

        protected void FiltroInactivos_CheckedChanged(object sender, EventArgs e)
        {
            FiltrarMarcas();
        }

        private void FiltrarMarcas()
        {
            List<Marca> lista = (List<Marca>)Session["ListaMarcas"];
            if (lista != null)
            {
                string filtroTexto = txtFiltro.Text.ToUpper();
                List<Marca> listaFiltrada = lista.FindAll(x =>
                    x.Descripcion.ToUpper().Contains(filtroTexto) ||
                    x.ID.ToString().Contains(filtroTexto) &&
                    (ddlProveedor.SelectedValue == "0" || x.IdProveedor.Equals(ddlProveedor.SelectedValue)) &&
                    (x.Activo || chkIncluirInactivos.Checked));
                gvMarcas.DataSource = listaFiltrada;
            }
            else
            {
                gvMarcas.DataSource = null;
            }
            gvMarcas.DataBind();
        }

        protected void BtnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            txtFiltro.Text = string.Empty;
            ddlProveedor.SelectedIndex = 0;
            chkIncluirInactivos.Checked = false;

            MarcaNegocio negocio = new MarcaNegocio();
            List<Marca> listaMarcas = negocio.Listar();
            gvMarcas.DataSource = listaMarcas;
            gvMarcas.DataBind();
        }

        protected void gvMarcas_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = gvMarcas.SelectedDataKey.Value.ToString();
            Response.Redirect("AltaMarca.aspx?ID=" + id);
        }

        protected void gvMarcas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMarcas.PageIndex = e.NewPageIndex;
            gvMarcas.DataSource = Session["ListaMarcas"];
            gvMarcas.DataBind();
        }

    }

}