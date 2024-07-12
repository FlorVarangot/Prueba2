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
                <asp:TextBox runat="server" ID="txtUser" CssClass="form-control" Required="true" TextMode="Search"/>
            </div>
            <div class="mb-3">
                <label for="InputPassword" class="form-label">Contraseña:</label>
                <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control" TextMode="Password" Required="true" Style="border-radius:5px; height:40px;" />
            </div>
            <asp:Button Text="Ingresar" runat="server" CssClass="btn btn-primary" ID="btnIngresar" OnClick="btnIngresar_Click" Style="border-radius:5px; margin:3px;" />
            <div class="registro">
                <p>¿No tenés cuenta? <a href="Registro.aspx" style="color:darkslategrey; font-weight:bold; text-decoration:none;">Registrate</a></p>
                <p>O hacé <a href="Default.aspx" style="color:darkslategrey; font-weight:bold; text-decoration:none;">CLIC ACÁ</a> para seguir navegando sin iniciar sesión.</p>
            </div>
        </div>
    </main>
</asp:Content>
