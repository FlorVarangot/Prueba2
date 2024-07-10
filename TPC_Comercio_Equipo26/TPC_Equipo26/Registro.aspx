<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="TPC_Equipo26.Registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="text-center my-4">
        <h1>REGISTRO</h1>
    </div>
    <div class="mb-3">
        <h4>Crear un nuevo perfil vendedor</h4>
    </div>
    <main>
        <div class="formulario">
            <div class="mb-2">
                <label for="txtUser" class="form-label">Nombre de usuario:</label>
                <asp:TextBox runat="server" ID="txtUser" CssClass="form-control" />
            </div>
            <div class="mb-2">
                <label for="InputPassword" class="form-label">Contraseña:</label>
                <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control" TextMode="Password" />
            </div>
            <div class="mb-2">
                <asp:Button Text="Registrarme" runat="server" CssClass="btn btn-primary" ID="btnRegistro" OnClick="btnRegistro_Click" />
                <asp:Button Text="Cancelar" runat="server" CssClass="btn btn-danger" ID="btnCancelar" OnClick="btnCancelar_Click" />
            </div>
        </div>
    </main>
</asp:Content>
