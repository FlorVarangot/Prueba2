<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Articulos.aspx.cs" Inherits="TPC_Equipo26.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>ARTICULOS</h1>

    <div class="row">
        <div class="col-6">
            <div class="mb-3">
                <asp:Label Text="Filtrar" runat="server" />
                <asp:TextBox runat="server" ID="txtFiltro" CssClass="form-control" AutoPostBack="true" OnTextChanged="filtro_TextChanged" />
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
                    <asp:Image ID="imgArticulo" runat="server" ImageUrl='<%# Eval("Imagenes[0].UrlImagen") %>' AlternateText="Imagen del artículo" Style="height:40px; width:45px;"/>
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
    <%--<asp:Button ID="btnAgregarArticulo" runat="server" Text="Agregar Artículo" OnClick="BtnAgregarArticulo_Click" CssClass="btn btn-success" />--%>
</asp:Content>
