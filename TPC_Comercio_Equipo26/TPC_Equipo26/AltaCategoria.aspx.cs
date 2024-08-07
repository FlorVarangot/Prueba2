﻿using System;
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
            ValidarAdmin();
            try
            {
                if (!IsPostBack)
                {
                    ConfirmarInactivar = false;
                    ConfirmarReactivar = false;
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
                        BtnInactivar.Visible = false;
                        btnReactivar.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }

        }

        private void CargarDatosCategoria(int Id)
        {
            try
            {
                CategoriaNegocio negocio = new CategoriaNegocio();
                Categoria categoria = negocio.ObtenerCategoriaPorId(Id);

                if (categoria != null)
                {
                    txtDescripcion.Text = categoria.Descripcion;

                    if (categoria.Activo == true)
                    {
                        BtnInactivar.Visible = true;
                        btnReactivar.Visible = false;
                    }
                    else
                    {
                        BtnInactivar.Visible = false;
                        btnReactivar.Visible = true;
                    }
                }
                else
                {
                    Session.Add("Error", "La categoría no se encontró.");
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
            txtDescripcion.Text = string.Empty;
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                lblDescripcion.Visible = true;
                lblError.Text = "El campo de Descripcion no puede quedar Incompleto";
                lblError.Visible = true;
                return false;
            }
            else
            {
                lblDescripcion.Visible = false;
                lblError.Visible = false;
                return true;
            }
        }

        protected void BtnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos())
                {
                    return;
                }
                Page.Validate();
                if (!Page.IsValid)
                {
                    return;
                }
                Categoria categoria = new Categoria();
                CategoriaNegocio negocio = new CategoriaNegocio();

                categoria.Descripcion = txtDescripcion.Text;
                categoria.Activo = true;

                string mensaje;
                if (Request.QueryString["ID"] != null)
                {
                    categoria.ID = int.Parse(Request.QueryString["ID"]);
                    string verificarDuplicado = negocio.VerificarCategoria(categoria.Descripcion, categoria.ID);
                    if (verificarDuplicado != null)
                    {
                        lblError.Text = verificarDuplicado;
                        lblError.Visible = true;
                        return;
                    }
                    negocio.Modificar(categoria);
                    mensaje = "Categoría modificada con éxito";
                }
                else
                {
                    string verificarDuplicado = negocio.VerificarCategoria(categoria.Descripcion);
                    if (verificarDuplicado != null)
                    {
                        lblError.Text = verificarDuplicado;
                        lblError.Visible = true;
                        return;
                    }
                    negocio.Agregar(categoria);
                    mensaje = "Categoría agregada con éxito";
                }

                LimpiarCampos();
                ClientScript.RegisterStartupScript(GetType(), "mostrarMensajeExito", $"alert('{mensaje}'); window.location.href = 'Categorias.aspx';", true);

                
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

        protected void btnConfirmaInactivar_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkConfirmaInactivacion.Checked)
                {
                    int id = Convert.ToInt32(Request.QueryString["ID"]);
                    CategoriaNegocio negocio = new CategoriaNegocio();
                    negocio.EliminarLogico(id, false);
                    Response.Redirect("Categorias.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
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
                    if (!ValidarCampos())
                    {
                        return;
                    }

                    Categoria nueva = new Categoria();
                    CategoriaNegocio negocio = new CategoriaNegocio();

                    nueva.ID = int.Parse(Request.QueryString["ID"]);
                    nueva.Descripcion = txtDescripcion.Text;
                    nueva.Activo = true;
                    negocio.ReactivarModificar(nueva);
                    Response.Redirect("Categorias.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
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