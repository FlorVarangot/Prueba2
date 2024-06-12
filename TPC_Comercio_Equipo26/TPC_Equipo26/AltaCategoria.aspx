<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AltaCategoria.aspx.cs" Inherits="TPC_Equipo26.AltaCategoria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblTituloAgregar" runat="server" Text="AGREGAR CATEGORIA" Visible="false" CssClass="titulo-label"></asp:Label>
    <asp:Label ID="lblTituloModificar" runat="server" Text="MODIFICAR CATEGORIA" Visible="false" CssClass="titulo-label"></asp:Label>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="row">
        <div class="col">
            <div class="mb-3">
                <label for="txtID" class="form-label">ID</label>
                <asp:TextBox runat="server" ID="txtID" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtDescripcion" class="form-label">Nombre: </label>
                <asp:TextBox runat="server" ID="txtDescripcion" CssClass="form-control" />
            </div>
        </div>
        <div class="mb-3" style="align-content: center; text-align: center; margin: 20px">
            <asp:Button Text="Aceptar" ID="BtnAceptar" CssClass="btn btn-success" OnClick="BtnAceptar_Click" runat="server" />
            <a href="Categorias.aspx" class="btn btn-danger">Cancelar</a>
            <asp:Button Text="Inactivar" ID="BtnInactivar" CssClass="btn btn-warning" OnClick="BtnInactivar_Click" runat="server" />
            <asp:Button Text="Reactivar" ID="btnReactivar" CssClass="btn btn-primary" OnClick="btnReactivar_Click" runat="server" />
        </div>
        <% if (ConfirmarInactivar)
            { %>
        <div class="mb-3">
            <asp:CheckBox Text="Confirmar Inactivación" ID="chkConfirmaInactivacion" runat="server" />
            <asp:Button Text="Inactivar" ID="btnConfirmaInactivar" OnClick="btnConfirmaInactivar_Click" CssClass="btn btn-outline-danger" runat="server" />
        </div>
        <% } %>
        <% if (ConfirmarReactivar)
            { %>
        <div class="mb-3">
            <asp:CheckBox Text="Confirmar Reactivación" ID="chkConfirmaReactivacion" runat="server" />
            <asp:Button Text="Reactivar" ID="btnConfirmaReactivar" OnClick="btnConfirmaReactivar_Click" CssClass="btn btn-outline-primary" runat="server" />
        </div>
        <% } %>
    </div>
</asp:Content>
