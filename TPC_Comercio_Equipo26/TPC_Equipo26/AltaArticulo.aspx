<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AltaArticulo.aspx.cs" Inherits="TPC_Equipo26.AltaArticulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblTituloAgregar" runat="server" Text="AGREGAR ARTICULO NUEVO" Visible="false" CssClass="titulo-label"></asp:Label>
    <asp:Label ID="lblTituloModificar" runat="server" Text="MODIFICAR ARTICULO" Visible="false" CssClass="titulo-label"></asp:Label>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <div class="col-6">
            <div class="mb-3">
                <label for="txtID" class="form-label">ID</label>
                <asp:TextBox runat="server" ID="txtID" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtCodigo" class="form-label">Codigo: </label>
                <asp:TextBox runat="server" ID="txtCodigo" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtNombre" class="form-label">Nombre: </label>
                <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtDescripcion" class="form-label">Descripcion: </label>
                <asp:TextBox runat="server" ID="txtDescripcion" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="ddlMarca" class="form-label">Marca</label>
                <asp:DropDownList ID="ddlMarca" CssClass="form-select" runat="server" AutoPostBack="true"></asp:DropDownList>
            </div>
            <div class="mb-3">
                <label for="ddlCategoria" class="form-label">Categoria</label>
                <asp:DropDownList ID="ddlCategoria" CssClass="form-select" runat="server" AutoPostBack="true"></asp:DropDownList>
            </div>
            <div class="mb-3">
                <asp:Button Text="Aceptar" ID="BtnAceptar" CssClass="btn btn-success" OnClick="BtnAceptar_Click" runat="server" />
                <a href="Articulos.aspx" class="btn btn-danger">Cancelar</a>
                <asp:Button Text="Inactivar" ID="BtnInactivar" CssClass="btn btn-warning" OnClick="BtnInactivar_Click" runat="server" />
            </div>
        </div>

        <div class="col-6">
            <div class="mb-3">
                <label for="txtPrecio" class="form-label">Precio Unitario $</label>
                <asp:TextBox ID="txtPrecio" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label for="ddlStock" class="form-label">Stock</label>
                <asp:DropDownList ID="ddlStock" CssClass="form-select" runat="server"></asp:DropDownList>
            </div>
            <div class="mb-3">
                <label for="ddlStockMinimo" class="form-label">Stock Minimo</label>
                <asp:DropDownList ID="ddlStockMinimo" CssClass="form-select" runat="server"></asp:DropDownList>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="mb-3">
                        <label for="txtImagenUrl" class="form-label">Url Imagen</label>
                        <asp:TextBox runat="server" ID="txtImagenUrl" CssClass="form-control"
                            AutoPostBack="true" OnTextChanged="txtImagenUrl_TextChanged" />
                    </div>
                    <asp:Image runat="server" ID="imgArticulos" Width="60%" Style="max-width: 100%; height: auto;"
                        ImageUrl="https://grupoact.com.ar/wp-content/uploads/2020/04/placeholder.png" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

</asp:Content>
