<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="TPC_Equipo26.Refistro" %>

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
                <label for="txtMail" class="form-label">Email:</label>
                <asp:TextBox runat="server" ID="txtMail" PlaceHolder="User name" CssClass="form-control" TextMode="Email" />
            </div>
            <div class="mb-2">
                <label for="txtUser" class="form-label">Nombre de usuario:</label>
                <asp:TextBox runat="server" ID="TextBox1" PlaceHolder="User name" CssClass="form-control" />
            </div>
            <div class="mb-2">
                <label for="InputPassword" class="form-label">Contraseña:</label>
                <asp:TextBox runat="server" ID="txtPassword" PlaceHolder="********" CssClass="form-control" TextMode="Password" />
            </div>
            <div class="mb-2">
                <%--<asp:Button Text="Registrarme" runat="server" CssClass="btn btn-primary" ID="btnRegistro" />--%>
                <%--<asp:Button Text="Cancelar" runat="server" CssClass="btn btn-danger" ID="btnCancelar"/>--%>
                <a href="MiPerfil.aspx" class="btn btn-primary" runat="server" ID="btnRegistro">Registrarme</a>
                <a href="LogIn.aspx" class="btn btn-danger" runat="server"  ID="btnCancelar">Cancelar</a>
                <%--OnClick="btnIngresar_Click"--%>
            </div>
        </div>
    </main>
</asp:Content>
