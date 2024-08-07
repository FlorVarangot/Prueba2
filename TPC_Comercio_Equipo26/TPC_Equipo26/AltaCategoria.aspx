﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AltaCategoria.aspx.cs" Inherits="TPC_Equipo26.AltaCategoria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="my-4">
        <asp:Label ID="lblTituloAgregar" runat="server" Text="AGREGAR CATEGORIA" Visible="false" CssClass="titulo-label"></asp:Label>
        <asp:Label ID="lblTituloModificar" runat="server" Text="MODIFICAR CATEGORIA" Visible="false" CssClass="titulo-label"></asp:Label>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="row">
        <div class="col">
            <div class="mb-3">
                <label for="txtDescripcion" class="form-label">Nombre: </label>
                <asp:TextBox runat="server" ID="txtDescripcion" CssClass="form-control" Required="true" />
                <asp:Label ID="lblDescripcion" runat="server" Text="*" ForeColor="Red" CssClass="required-field" Visible="false"></asp:Label>
                <asp:RegularExpressionValidator runat="server" ControlToValidate="txtDescripcion" ErrorMessage="El nombre no puede contener números" ValidationExpression="^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$" ForeColor="Red" />
            </div>
        </div>
        <div class="mb-3" style="align-content: center; text-align: center; margin: 20px">
            <asp:Button Text="Aceptar" ID="BtnAceptar" CssClass="btn btn-success" OnClick="BtnAceptar_Click" runat="server" />
            <a href="Categorias.aspx" class="btn btn-danger">Cancelar</a>
            <asp:Button Text="Inactivar" ID="BtnInactivar" CssClass="btn btn-warning" OnClick="BtnInactivar_Click" runat="server" />
            <asp:Button Text="Reactivar" ID="btnReactivar" CssClass="btn btn-primary" OnClick="btnReactivar_Click" runat="server" />
        </div>
        <asp:Label ID="lblError" runat="server" Text="" CssClass="text-danger" Visible="false"></asp:Label>
        <% if (ConfirmarInactivar)
            { %>
        <div class="mb-3" style="align-content: center; text-align: center; margin: 20px">
            <asp:CheckBox Text="Confirmar Inactivación" ID="chkConfirmaInactivacion" runat="server" />
            <asp:Button Text="Inactivar" ID="btnConfirmaInactivar" OnClick="btnConfirmaInactivar_Click" CssClass="btn btn-outline-danger" runat="server" />
        </div>
        <% } %>
        <% if (ConfirmarReactivar)
            { %>
        <div class="mb-3" style="align-content: center; text-align: center; margin: 20px">
            <asp:CheckBox Text="Confirmar Reactivación" ID="chkConfirmaReactivacion" runat="server" />
            <asp:Button Text="Reactivar" ID="btnConfirmaReactivar" OnClick="btnConfirmaReactivar_Click" CssClass="btn btn-outline-primary" runat="server" />
        </div>
        <% } %>
    </div>
</asp:Content>
