﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AltaCliente.aspx.cs" Inherits="TPC_Equipo26.AltaCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="my-4">
        <asp:Label ID="lblTituloAgregar" runat="server" Text="AGREGAR CLIENTE" Visible="false" CssClass="titulo-label"></asp:Label>
        <asp:Label ID="lblTituloModificar" runat="server" Text="MODIFICAR CLIENTE" Visible="false" CssClass="titulo-label"></asp:Label>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="row">
        <div class="col">
            <div class="mb-3">
                <label for="txtNombre" class="form-label">Nombre:</label>
                <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" Required="true" />
                <asp:Label ID="lblNombre" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
                <asp:RegularExpressionValidator runat="server" ControlToValidate="txtNombre" ErrorMessage="El Nombre debe contener letras" ValidationExpression="^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$" ForeColor="Red" />
            </div>
            <div class="mb-3">
                <label for="txtApellido" class="form-label">Apellido:</label>
                <asp:TextBox runat="server" ID="txtApellido" CssClass="form-control" Required="true" />
                <asp:Label ID="lblApellido" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
                <asp:RegularExpressionValidator runat="server" ControlToValidate="txtApellido" ErrorMessage="El Apellido debe contener letras" ValidationExpression="^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$" ForeColor="Red" />
            </div>
            <div class="mb-3">
                <label for="txtDni" class="form-label">Dni:</label>
                <asp:TextBox runat="server" ID="txtDNI" CssClass="form-control" Required="true" />
                <asp:Label ID="lblDNI" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
                <asp:RegularExpressionValidator runat="server" ControlToValidate="txtDNI" ErrorMessage="El DNI debe contener únicamente números" ValidationExpression="^\d+$" ForeColor="Red" />
            </div>
            <div class="mb-3">
                <label for="txtTelefono" class="form-label">Teléfono:</label>
                <asp:TextBox runat="server" ID="txtTelefono" CssClass="form-control" Required="true" />
                <asp:Label ID="lblTelefono" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
                <asp:RegularExpressionValidator runat="server" ControlToValidate="txtTelefono" ErrorMessage="El Teléfono debe contener únicamente números" ValidationExpression="^\d{2}[-\d ]+\d{1}$" ForeColor="Red" />
            </div>
            <div class="mb-3">
                <label for="txtEmail" class="form-label">Email:</label>
                <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" Required="true" />
                <asp:Label ID="lblEmail" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
                <asp:RegularExpressionValidator runat="server" ControlToValidate="txtEmail" ErrorMessage="Ingrese un email válido" ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$" ForeColor="Red"></asp:RegularExpressionValidator>
            </div>
            <div class="mb-3">
                <label for="txtDireccion" class="form-label">Dirección:</label>
                <asp:TextBox runat="server" ID="txtDireccion" CssClass="form-control" Required="true" />
                <asp:Label ID="lblDireccion" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
            </div>
        </div>
        <div class="mb-3" style="align-content: center; text-align: center; margin: 20px">
            <asp:Button Text="Aceptar" ID="BtnAceptar" CssClass="btn btn-success" OnClick="BtnAceptar_Click" runat="server" />
            <a href="Clientes.aspx" class="btn btn-danger">Cancelar</a>
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
