﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPC_Equipo26.Dominio;
using TPC_Equipo26.Negocio;

namespace TPC_Equipo26
{
    public partial class Categorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MarcaNegocio marcaNegocio = new MarcaNegocio();
                List<Marca> marcas = marcaNegocio.Listar();
                gvCategorias.DataSource = marcas;
                gvCategorias.DataBind();
            }
        }
    }
}