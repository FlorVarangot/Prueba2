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
    public partial class DetallesCompra : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["ID"] != null)
                {
                    long idCompra = Convert.ToInt64(Request.QueryString["ID"]);
                    CargarDetallesCompra(idCompra);
                }
                else
                {
                    Response.Redirect("Compras.aspx");
                }
            }
        }

        private void CargarDetallesCompra(long idCompra)
        {
            try
            {
                DetalleCompraNegocio negocio = new DetalleCompraNegocio();
                List<DetalleCompra> detalles = negocio.ListarDetalleCompra(idCompra);

               
                gvDetalle.DataSource = detalles;
                gvDetalle.DataBind();
            }
            catch (Exception)
            {             
                Response.Redirect("Error.aspx"); 
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

        

        protected void gvDetalle_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int idProveedor = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "IdProveedor"));
                string nombreProveedor = TraerNombreProveedor(idProveedor);
                e.Row.Cells[3].Text = nombreProveedor;
                e.Row.Cells[3].Visible = false;
            }
        }
    }
}