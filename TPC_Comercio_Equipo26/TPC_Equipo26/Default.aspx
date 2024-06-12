<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TPC_Equipo26.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>ARTíCULOS</h1>

    <div class="container">
        <div class="row">
            <asp:Label Text="Buscar:" runat="server" CssClass="form-label" />
            <div class="col-4">
                <asp:TextBox runat="server" ID="txtFiltro" CssClass="form-control" AutoPostBack="true" OnTextChanged="Filtro_TextChanged" />
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
                        <asp:Label Text="Campo" ID="lblCampo" runat="server" />
                        <asp:DropDownList runat="server" AutoPostBack="true" CssClass="form-control" ID="ddlCampo" OnSelectedIndexChanged="ddlCampo_SelectedIndexChanged">
                            <asp:ListItem Text="Descripción" />
                            <asp:ListItem Text="Marca" />
                            <asp:ListItem Text="Categoría" />
                            <asp:ListItem Text="Precio Unitario ($)" />
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="mb-3">
                        <asp:Label Text="Criterio" runat="server" />
                        <asp:DropDownList runat="server" ID="ddlCriterio" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="mb-3">
                        <asp:Label Text="Filtro" runat="server" />
                        <asp:TextBox runat="server" ID="txtFiltroAvanzado" CssClass="form-control" />
                    </div>
                </div>
                <div class="col-md-3 align-self-end">
                    <div class="mb-3">
                        <asp:Button Text="Buscar" runat="server" CssClass="btn btn-primary" ID="btnBuscar" OnClick="btnBuscar_Click" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="mb-3">
                        <asp:CheckBox runat="server" ID="chkIncluirInactivos" Text="Incluir inactivos" AutoPostBack="true"/>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <div class="row">
            <div class="col-md-2">
                <asp:Button runat="server" ID="btnLimpiarFiltros" Text="Limpiar filtros" OnClick="BtnLimpiarFiltros_Click" CssClass="btn btn-light mt-3" />
            </div>
        </div>
        <br />
        <asp:GridView ID="gvArticulos" runat="server" DataKeyNames="ID" CssClass="table table-light table-bordered"
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
        <a href="AltaArticulo.aspx" class="btn btn-success">Agregar un artículo</a>
    </div>
</asp:Content>
