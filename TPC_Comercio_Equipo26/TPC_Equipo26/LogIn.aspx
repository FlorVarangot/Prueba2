<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="TPC_Equipo26.LogIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="text-center my-4">
        <h1>INGRESAR</h1>
    </div>
    <div class="mb-3">
        <h4>¡Bienvenido/a a nuestro gestor online de artículos de librería!</h4>
    </div>
    <main>
        <div class="formulario">
            <div class="mb-3">
                <label for="txtUser" class="form-label">Usuario:</label>
                <asp:TextBox runat="server" ID="txtUser" PlaceHolder="User name" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="InputPassword" class="form-label">Contraseña:</label>
                <asp:TextBox runat="server" ID="txtPassword" PlaceHolder="********" CssClass="form-control" TextMode="Password" />
            </div>
            <asp:Button Text="Ingresar" runat="server" CssClass="btn btn-primary" ID="btnIngresar" OnClick="btnIngresar_Click" />
            <div class="registro">
                <p>¿No tenés cuenta? <a href="Registro.aspx">Registrate</a></p>
                <p>O hacé <a href="Default.aspx">CLIC ACÁ</a> para seguir navegando sin iniciar sesión.</p>
            </div>
        </div>
    </main>
</asp:Content>
