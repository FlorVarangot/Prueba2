<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TPC_Equipo26.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>ARTÍCULOS</h1>

    <div class="container">
        <div class="row">
            <asp:Label Text="Buscar:" runat="server" CssClass="form-label" />
            <div class="col-4">
                <asp:TextBox runat="server" ID="txtFiltro" CssClass="form-control" AutoPostBack="true" OnTextChanged="Filtro_TextChanged" Placeholder="Buscar artículos, marcas y más..." />
            </div>
            <div class="col-4" style="display: flex; flex-direction: column; justify-content: flex-end;">
                <div class="mb-3">
                    <asp:CheckBox Text="Filtro Avanzado" CssClass="" ID="chkAvanzado" runat="server" AutoPostBack="true" OnCheckedChanged="chkAvanzado_CheckedChanged" />
                </div>
            </div>
        </div>
        <asp:Panel ID="pnlFiltroAvanzado" runat="server" Visible="false">
            <div class="row">
                <div class="col-md-3">
                    <div class="mb-3">
                        <asp:Label Text="Marca" ID="lblMarca" runat="server" />
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlMarca" AutoPostBack="true" OnSelectedIndexChanged="ddlMarca_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="mb-3">
                        <asp:Label Text="Categoría" ID="lblCategoria" runat="server" />
                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCategoria" AutoPostBack="true" OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <div class="row">
            <div class="col-md-12">
                <div class="mb-3">
                    <asp:CheckBox runat="server" ID="chkIncluirInactivos" Text="Incluir inactivos" AutoPostBack="true" OnCheckedChanged="chkIncluirInactivos_CheckedChanged" />
                    <asp:CheckBox ID="chkOrdenarAZ" runat="server" Text="Ordenar A-Z" AutoPostBack="true" OnCheckedChanged="chkOrdenarAZ_CheckedChanged" />
                    <asp:CheckBox ID="chkOrdenarPorStock" runat="server" Text="Ordenar por Stock" AutoPostBack="true" OnCheckedChanged="chkOrdenarPorStock_CheckedChanged" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 mb-2">
                <asp:Button runat="server" ID="btnLimpiarFiltros" Text="Limpiar filtros" OnClick="BtnLimpiarFiltros_Click" CssClass="btn btn-light mt-3" />
            </div>
        </div>
    </div>

    <asp:GridView ID="gvArticulos" runat="server" DataKeyNames="ID" CssClass="table table-success table-hover"
        Style="text-align: center" AutoGenerateColumns="false"
        OnSelectedIndexChanged="gvArticulos_SelectedIndexChanged"
        OnPageIndexChanging="gvArticulos_PageIndexChanging"
        AllowPaging="true" PageSize="10">
        <Columns>
            <asp:BoundField HeaderText="Id" DataField="ID" />
            <asp:BoundField HeaderText="Código" DataField="Codigo" />
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />
            <asp:BoundField HeaderText="Marca" DataField="Marca.Descripcion" />
            <asp:BoundField HeaderText="Categoría" DataField="Categoria.Descripcion" />
            <asp:TemplateField HeaderText="Imagen">
                <ItemTemplate>
                    <asp:Image ID="imgArticulo" runat="server" ImageUrl='<%# Eval("Imagen") %>' AlternateText="Imagen del artículo" Style="height: 40px; width: 45px;" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Precio Unitario ($)" />
            <asp:BoundField HeaderText="Stock disponible" />
            <asp:BoundField HeaderText="Stock Mínimo" DataField="StockMin" />
            <asp:BoundField HeaderText="Activo" DataField="Activo" />
            <asp:TemplateField>
                <ItemTemplate>
                    <a href='<%# "AltaArticulo.aspx?ID=" + Eval("ID") %>' class="icono" title="Gestionar">
                        <i class="fa-solid fa-pen" style="color: dimgrey; margin: 10px"></i>
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <asp:Label ID="lblVacio" Text="No se encontraron Artículos con ese criterio." runat="server" />
    <hr />
    <div class="text-end">
        <a href="AltaArticulo.aspx" class="btn btn-success">Agregar un artículo</a>
    </div>
</asp:Content>
