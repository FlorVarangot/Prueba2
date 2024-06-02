<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Articulos.aspx.cs" Inherits="TPC_Equipo26.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>ARTíCULOS</h1>
    <asp:GridView ID="gvArticulos" runat="server" CssClass="Table">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="Id" />
            <asp:BoundField DataField="Codigo" HeaderText="Cod." />
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
            <%--<asp:BoundField DataField="Descripcion" HeaderText="Desc." />
            <asp:BoundField DataField="Marca" HeaderText="Marca" />
            <asp:BoundField DataField="Categoria" HeaderText="Categ." />
            <asp:TemplateField HeaderText="Imagen">
                <ItemTemplate>
                    <asp:Repeater ID="rptImagenes" runat="server" DataSource='<%# Eval("Imagenes") %>'>
                        <ItemTemplate>
                            <img src='<%# Eval("UrlImagen") %>' alt="Imgaen del artículo" />
                        </ItemTemplate>
                    </asp:Repeater>
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:BoundField DataField="Precio" HeaderText="Precio U." />
            <asp:BoundField DataField="Activo" HeaderText="Activo" />
            <asp:TemplateField>
                <ItemTemplate>
                    <a href="AltaArticulo.aspx">
                        <i class="fa-solid fa-pen" style="color: black"></i>
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <a href="#">
                        <i class="fa-solid fa-trash-can" style="color: black"></i>
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

        <%-- Corregir:
                1. Muestra la grilla dos veces (Con distinta nomenclatura, puede ser un indicador del por qué)
                2. Boton AgregarArticulo no dirige a página correcta.--%>

    </asp:GridView>
    <asp:Button ID="btnAgregarArticulo" runat="server" Text="Agregar Artículo" OnClick="btnAgregarArticulo_Click" CssClass="btn btn-primary" />
</asp:Content>
