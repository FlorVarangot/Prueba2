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
            <%--<asp:BoundField HeaderText="Descripción" DataField="Descripcion" />
            <asp:BoundField HeaderText="Marca" DataField="Marca"  />
            <asp:BoundField HeaderText="Categoría" DataField="Categoria"/>
            <asp:TemplateField HeaderText="Imagen">
                <ItemTemplate>
                    <asp:Repeater ID="rptImagenes" runat="server" DataSource='<%# Eval("Imagenes") %>'>
                        <ItemTemplate>
                            <img src='<%# Eval("UrlImagen") %>' alt="Imgaen del artículo" />
                        </ItemTemplate>
                    </asp:Repeater>
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:BoundField HeaderText="Precio Unitario ($)" DataField="Precio" />
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

        <%-- Corregir:
                1. Boton AgregarArticulo no dirige a página correcta, sí a ERROR.ASPX (ver evento en Articulo.aspx.cs)--%>
    </asp:GridView>
    <a href="AltaArticulo.aspx">Agregar un artículo</a>
    <%--<asp:Button ID="btnAgregarArticulo" runat="server" Text="Agregar Artículo" OnClick="BtnAgregarArticulo_Click" CssClass="btn btn-success" />--%>
</asp:Content>
