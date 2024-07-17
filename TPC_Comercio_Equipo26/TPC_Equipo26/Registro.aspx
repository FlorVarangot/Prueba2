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
        <asp:Panel ID="pnlRegistro" runat="server" DefaultButton="btnRegistro">
            <div class="formulario">
                <div class="mb-2">
                    <label for="txtUser" class="form-label">Nombre de usuario:</label>
                    <asp:TextBox runat="server" ID="txtUser" ClientIDMode="Static" CssClass="form-control" TextMode="Search" />
                    <asp:Label ID="lblUser" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
                </div>
                <div class="mb-2">
                    <label for="txtEmail" class="form-label">Correo electrónico:</label>
                    <asp:TextBox runat="server" ID="txtEmail" ClientIDMode="Static" CssClass="form-control" TextMode="Email" />
                    <asp:Label ID="lblEmail" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
                </div>
                <div class="mb-2">
                    <label for="txtPassword" class="form-label">Contraseña:</label>
                    <asp:TextBox runat="server" ID="txtPassword" ClientIDMode="Static" CssClass="form-control" TextMode="Password" Style="border-radius: 5px; height: 40px;" />
                    <asp:Label ID="lblPassword" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
                </div>
                <div class="mb-2">
                    <asp:Button Text="Registrarme" runat="server" CssClass="btn btn-primary" ID="btnRegistro"
                        OnClick="btnRegistro_Click" Style="padding: 10px; border-radius: 5px" />
                    <asp:Button Text="Cancelar" runat="server" CssClass="btn btn-danger" ID="btnCancelar" OnClick="btnCancelar_Click" Style="padding: 10px; border-radius: 5px; margin: 5px;" />
                </div>
            </div>
        </asp:Panel>
    </main>
</asp:Content>
