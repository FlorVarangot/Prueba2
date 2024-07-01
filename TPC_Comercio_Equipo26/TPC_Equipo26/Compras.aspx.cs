using System;
using System.Collections.Generic;
using System.Globalization;
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
                CargarCompras();
                CargarProveedores();                          
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

        private void CargarCompras()
        {
            try
            {
                CompraNegocio compraNegocio = new CompraNegocio();
                List<Compra> listaCompras = compraNegocio.Listar();
                Session["ListaCompras"] = listaCompras;
                FiltrarCompras();
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx", false);
            }
        }

        private void FiltrarCompras()
        {
            List<Compra> listaCompras = (List<Compra>)Session["ListaCompras"];
            string filtroFecha = txtFiltro.Text.Trim();
            if (!string.IsNullOrEmpty(filtroFecha))
            {
                listaCompras = listaCompras.Where(x => x.FechaCompra.ToString("dd/MM/yyyy").Contains(filtroFecha)).ToList();
            }
            if (ddlProveedor.SelectedIndex > 0)
            {
                int proveedorSeleccionado = int.Parse(ddlProveedor.SelectedValue);
                listaCompras = listaCompras.Where(x => x.IdProveedor == proveedorSeleccionado).ToList();
            }
            if (chkOrdenarFechaAsc.Checked)
            {
                listaCompras = listaCompras.OrderBy(x => x.FechaCompra).ToList();
            }
            else if (chkOrdenarFechaDesc.Checked)
            {
                listaCompras = listaCompras.OrderByDescending(x => x.FechaCompra).ToList();
            }

            if (chkOrdenarPrecioAsc.Checked)
            {
                if (chkOrdenarFechaAsc.Checked || chkOrdenarFechaDesc.Checked)
                {
                    listaCompras = listaCompras.OrderBy(x => x.FechaCompra).ThenByDescending(x => x.TotalCompra).ToList();
                }
                else
                {
                    listaCompras = listaCompras.OrderByDescending(x => x.TotalCompra).ToList();
                }
            }
            else if (chkOrdenarPrecioDesc.Checked)
            {
                if (chkOrdenarFechaAsc.Checked || chkOrdenarFechaDesc.Checked)
                {
                    listaCompras = listaCompras.OrderBy(x => x.FechaCompra).ThenBy(x => x.TotalCompra).ToList();
                }
                else
                {
                    listaCompras = listaCompras.OrderBy(x => x.TotalCompra).ToList();
                }
            }

            Session["ListaFiltrada"] = listaCompras;

            gvCompras.DataSource = listaCompras;
            gvCompras.DataBind();
        }

        protected void gvCompras_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvCompras.PageIndex = e.NewPageIndex;
                List<Compra> compras = (List<Compra>)Session["ListaFiltrada"];

                gvCompras.DataSource = compras;
                gvCompras.DataBind();
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx");
            }
        }

        protected void gvCompras_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Compra compra = (Compra)e.Row.DataItem;
                int idProveedor = compra.IdProveedor;
                string proveedor = TraerNombreProveedor(idProveedor);
                e.Row.Cells[2].Text = proveedor; 
            }
        }
        private string TraerNombreProveedor(int idProveedor)
        {
            try
            {
                ProveedorNegocio proveedorNegocio = new ProveedorNegocio();
                Proveedor proveedor = proveedorNegocio.ObtenerProveedorPorId(idProveedor);

                if (proveedor != null)
                {
                    return proveedor.Nombre;
                }
                return "Proveedor no encontrado";
            }
            catch (Exception)
            {
                return "Error al obtener el nombre del proveedor";
            }
        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            FiltrarCompras();
        }

        protected void btnRestablecer_Click(object sender, EventArgs e)
        {
            chkOrdenarFechaAsc.Checked = false;
            chkOrdenarFechaDesc.Checked = false;
            chkOrdenarPrecioAsc.Checked = false;
            chkOrdenarPrecioDesc.Checked = false;
            chkAvanzado.Checked = false;
            pnlFiltroAvanzado.Visible = false;
            ddlProveedor.SelectedIndex = 0;
            txtFiltro.Text = string.Empty;
            CargarCompras();
        }

        protected void ddlProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarCompras();
        }

        protected void chkOrdenarFechaAsc_CheckedChanged(object sender, EventArgs e)
        {
            FiltrarCompras();
        }

        protected void chkOrdenarFechaDesc_CheckedChanged(object sender, EventArgs e)
        {
            FiltrarCompras();
        }

        protected void chkOrdenarPrecioAsc_CheckedChanged(object sender, EventArgs e)
        {
            FiltrarCompras();
        }

        protected void chkOrdenarPrecioDesc_CheckedChanged(object sender, EventArgs e)
        {
            FiltrarCompras();
        }

        protected void chkAvanzado_CheckedChanged(object sender, EventArgs e)
        {
            pnlFiltroAvanzado.Visible = chkAvanzado.Checked;
        }
    }
}