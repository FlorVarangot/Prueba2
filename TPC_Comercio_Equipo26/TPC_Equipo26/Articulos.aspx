<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Articulos.aspx.cs" Inherits="TPC_Equipo26.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>ARTíCULOS</h1>

    <div class="container">
        <div class="row">
            <div class="col-2">
                <asp:Label Text="Buscar:" runat="server" CssClass="form-label" />
                <asp:TextBox runat="server" ID="txtFiltro" CssClass="form-control" AutoPostBack="true" OnTextChanged="Filtro_TextChanged" />
            </div>
            <div class="col-2">
                <asp:Label Text="Marca:" runat="server" CssClass="form-label" />
                <asp:DropDownList runat="server" ID="ddlMarca" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="FiltroMarca_SelectedIndexChanged" />
            </div>
            <div class="col-2">
                <asp:Label Text="Categoría:" runat="server" CssClass="form-label" />
                <asp:DropDownList runat="server" ID="ddlCategoria" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="FiltroCategoria_SelectedIndexChanged" />
            </div>
        </div>
        <div class="row">
            <div class="col-2">
                <asp:CheckBox runat="server" ID="chkIncluirInactivos" Text="Incluir inactivos" AutoPostBack="true" OnCheckedChanged="FiltroInactivos_CheckedChanged" />
            </div>
        </div>
        <div class="row">
            <div class="col-2">
                <asp:Button runat="server" ID="btnLimpiarFiltros" Text="Limpiar filtros" OnClick="BtnLimpiarFiltros_Click" CssClass="btn btn-light mt-3" Style="margin:15px" />
            </div>
        </div>
    </div>



    <asp:GridView class="table" Style="text-align: center" runat="server" CssClass="table table-light table-bordered" ID="gvArticulos" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField HeaderText="Id" DataField="ID" />
            <asp:BoundField HeaderText="Código" DataField="Codigo" />
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />
            <asp:BoundField HeaderText="Marca" DataField="Marca.Descripcion" />
            <asp:BoundField HeaderText="Categoría" DataField="Categoria.Descripcion" />
            <asp:TemplateField HeaderText="Imágenes">
                <ItemTemplate>
                    <asp:Image ID="imgArticulo" runat="server" ImageUrl='<%# Eval("Imagenes[0].UrlImagen") %>' AlternateText="Imagen del artículo" Style="height: 40px; width: 45px;" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Precio Unitario ($)" DataField="Precio" />
            <asp:BoundField HeaderText="Stock disponible" DataField="Stock" />
            <asp:BoundField HeaderText="Stock Mínimo" DataField="StockMin" />
            <asp:BoundField HeaderText="Activo" DataField="Activo" />
            <asp:TemplateField HeaderText="Editar">
                <ItemTemplate>
                    <a href="AltaArticulo.aspx">
                        <i class="fa-solid fa-pen" style="color: dimgrey; margin: 10px"></i>
                    </a>
                    <a href="#" class="icon">
                        <i class="fa-solid fa-trash-can" style="color: dimgrey; margin: 10px"></i>
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

    </asp:GridView>
    <a href="AltaArticulo.aspx">Agregar un artículo</a>
</asp:Content>
