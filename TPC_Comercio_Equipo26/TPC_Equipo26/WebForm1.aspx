<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="TPC_Equipo26.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>COMPRAS</h1>
    <div class="container">
        <div class="row">
            <div class="mb-3">
                <asp:Label Text="Seleccione el Proveedor" ID="lblProveedor" runat="server" />
                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlProveedor" AutoPostBack="true" ></asp:DropDownList>
            </div>
        </div>
    </div>

    <asp:GridView ID="gvCompras" runat="server" DataKeyNames="ID" CssClass="table table-success table-hover"
        Style="text-align: center" AutoGenerateColumns="false"
        OnSelectedIndexChanged="gvCompras_SelectedIndexChanged"
        OnPageIndexChanged="gvCompras_PageIndexChanged"
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
            <asp:BoundField HeaderText="Precio Unitario ($)" DataField="PrecioUnitario" />
            <asp:BoundField HeaderText="Stock disponible" DataField="StockDisponible" />
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
</asp:Content>
