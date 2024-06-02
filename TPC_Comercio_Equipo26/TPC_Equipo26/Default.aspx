<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TPC_Equipo26.LogIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h4>¡Bienvenido/a a nuestro gestor online de artículos de librería!</h4>
    <main>
        <div class="formulario">
            <div class="mb-3">
                <label for="InputEmail" class="form-label">Usuario:</label>
                <input type="email" class="form-control" id="inputEmail" aria-describedby="emailHelp">
            </div>
            <div class="mb-3">
                <label for="InputPassword" class="form-label">Contraseña:</label>
                <input type="password" class="form-control" id="inputPassword">
            </div>
            <button type="submit" class="btn btn-primary">Ingresar</button>
            <div class="registro">
                <p>¿No tenés cuenta? <a href="Registro.aspx">Registrate</a></p>
                <p>O hacé <a href="Articulos.aspx">clic acá</a> para seguir navegando sin iniciar sesión.</p>
            </div>
        </div>
    </main>
</asp:Content>
