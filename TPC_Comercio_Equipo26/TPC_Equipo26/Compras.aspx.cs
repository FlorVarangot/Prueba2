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
                CompraNegocio negocio = new CompraNegocio();
                ArticuloNegocio negocioArticulo = new ArticuloNegocio();
                List<Compra> compras;

                compras = negocio.Listar();
                if (ddlProveedor.SelectedIndex > 0)
                {
                    int idProveedor = Convert.ToInt32(ddlProveedor.SelectedValue);
                    List<Articulo> listaArticulos = negocioArticulo.ListarPorProveedor(idProveedor);

                    compras = negocio.Listar().Where(c => listaArticulos.Any(a => a.ID == c.IdProveedor)).ToList();
                }
                else
                {
                    compras = negocio.Listar();
                }

                if (ChkOrdenarPorFecha.Checked)
                {
                    compras = compras.OrderByDescending(x => x.FechaCompra).ToList();
                }
                else if (ChkOrdenarPorPrecio.Checked)
                {
                    compras = compras.OrderBy(x => x.TotalCompra).ToList();
                }
                else
                {
                    compras = compras.OrderBy(x => x.ID).ToList();
                }

                Session["ListaFiltrada"] = compras;
                gvCompras.DataSource = compras;
                gvCompras.DataBind();
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx");
            }
        }


        protected void gvCompras_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvCompras.PageIndex = e.NewPageIndex;
                List<Compra> compras = (List<Compra>)Session["ListaElementosFiltrada"];

               // lblVacio.Visible = (compras.Count == 0);
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
                int id = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ID"));
                string proveedor = TraerNombreProveedor(id);
                e.Row.Cells[2].Text = proveedor;
            }
        }
        private string TraerNombreProveedor(int id)
        {
            try
            {
                MarcaNegocio marcaNegocio = new MarcaNegocio();
                Marca marca = marcaNegocio.ObtenerMarcaPorId(id);
                string proveedor = "";

                if (marca != null)
                {
                    int idProveedor = marca.IdProveedor;
                    ProveedorNegocio proveedorNegocio = new ProveedorNegocio();
                    Proveedor prov = proveedorNegocio.ObtenerProveedorPorId(idProveedor);

                    if (prov != null)
                    {
                        proveedor = prov.Nombre;
                        return proveedor;
                    }
                }

                return proveedor;
            }
            catch (Exception)
            {
                return "Error al obtener el nombre del proveedor";
            }
        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ChkOrdenarPorFecha_CheckedChanged(object sender, EventArgs e)
        {
            CargarCompras();
        }

        protected void ChkOrdenarPorPrecio_CheckedChanged(object sender, EventArgs e)
        {
            CargarCompras();
        }

        protected void btnRestablecer_Click(object sender, EventArgs e)
        {
            ChkOrdenarPorFecha.Checked = false;
            ChkOrdenarPorPrecio.Checked = false;
            ddlProveedor.SelectedIndex = 0;
            CargarCompras();
        }

        protected void ddlProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarCompras();
        }
    }
}