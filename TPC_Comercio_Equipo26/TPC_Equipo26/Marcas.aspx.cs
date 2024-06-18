﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Discovery;
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
                CargarMarcas();
            }
        }

        private void CargarMarcas()
        {
            try
            {
                chkIncluirInactivos.Checked = false;
                MarcaNegocio marcaNegocio = new MarcaNegocio();
                List<Marca> marcas = marcaNegocio.Listar();
                CargarDdlProveedores();

                Session["listaMarcas"] = marcas;
                FiltrarMarcas();
                if (marcas != null)
                {
                    lblVacio.Visible = false;
                }
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx", false);
            }
        }

        private void FiltrarMarcas()
        {
            List<Marca> lista = (List<Marca>)Session["listaMarcas"];
            string filtro = txtFiltro.Text.Trim().ToUpper();
            bool incluirInactivos = chkIncluirInactivos.Checked;
            int proveedorSelec = int.Parse(ddlProveedor.SelectedValue);

            List<Marca> listaFiltrada;

            if (lista != null)
            {
                if (proveedorSelec == 0)
                {
                    if (string.IsNullOrEmpty(filtro))
                    {
                        listaFiltrada = incluirInactivos
                            ? lista
                            : lista.Where(x => x.Activo).ToList();
                    }
                    else
                    {
                        listaFiltrada = lista.Where(x =>
                        //x.ID.ToString().Contains(filtro) ||
                        x.Descripcion.ToUpper().Contains(filtro) &&
                        (x.Activo || incluirInactivos)).ToList();
                    }
                }
                else
                {
                    listaFiltrada = lista.Where(x =>
                    x.IdProveedor == proveedorSelec &&
                    x.Descripcion.ToUpper().Contains(filtro) &&
                    (x.Activo || incluirInactivos)).ToList();
                }
                if (listaFiltrada.Count > 0)
                {
                    lblVacio.Visible = false;
                }
                else
                {
                    lblVacio.Visible = true;
                }

                gvMarcas.DataSource = listaFiltrada;
            }
            else
            {
                gvMarcas.DataSource = lista;
            }

            gvMarcas.DataBind();
        }

        protected void BtnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            LimpiarFiltros();
        }

        protected void GvMarcas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMarcas.PageIndex = e.NewPageIndex;
            FiltrarMarcas();
        }

        protected void GvMarcas_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = gvMarcas.SelectedDataKey.Value.ToString();
            Response.Redirect("AltaMarca.aspx?ID=" + id);

        }

        protected void FiltroProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarMarcas();
        }
        protected void FiltroInactivos_CheckedChanged(object sender, EventArgs e)
        {
            FiltrarMarcas();
        }
        protected void Filtro_TextChanged(object sender, EventArgs e)
        {
            FiltrarMarcas();
        }

        public void LimpiarFiltros()
        {
            txtFiltro.Text = string.Empty;
            ddlProveedor.SelectedIndex = -1;
            chkIncluirInactivos.Checked = false;
            CargarMarcas();
        }

        public void CargarDdlProveedores()
        {
            ProveedorNegocio proveedorNegocio = new ProveedorNegocio();
            List<Proveedor> proveedores = proveedorNegocio.Listar();
            ddlProveedor.DataSource = proveedores;
            ddlProveedor.DataTextField = "Nombre";
            ddlProveedor.DataValueField = "ID";
            ddlProveedor.DataBind();
            ddlProveedor.Items.Insert(0, new ListItem("Proveedor...", "0"));
        }

    }

}