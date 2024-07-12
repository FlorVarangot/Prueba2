<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MiPerfil.aspx.cs" Inherits="TPC_Equipo26.MiPerfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="text-center my-4">
        <h1>MI PERFIL</h1>
        <asp:Label ID="lblUser" runat="server" CssClass="form-label" Visible="false" Style="font-size:large"></asp:Label>
    </div>
    <div class="row">
        <div class="col-6">
            <div class="mb-3">
                <label class="form-label">Nombre</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtNombre" />
            </div>
            <div class="mb-3">
                <label class="form-label">Apellido</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtApellido" />
            </div>
            <div class="mb-3">
                <label class="form-label">Email</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail"/>
            </div>
        </div>
        <div class="col-6">
            <div class="mb-3">
                <label class="form-label">Imagen de perfil</label>
                <input type="file" id="txtImagen" runat="server" class="form-control" />
            </div>
            <div>
                <asp:Image ID="imgNuevoPerfil" ImageUrl="https://www.freeiconspng.com/thumbs/no-image-icon/no-image-icon-6.png" runat="server" CssClass="img-fluid mb-3" Style="width: auto; max-height: 150px; align-content: center" />
            </div>
        </div>
    </div>
    <div class="row">
        <div>
            <asp:Button Text="Guardar" CssClass="btn btn-primary" ID="btnGuardar" OnClick="btnGuardar_Click" runat="server" Style="margin-right: 5px" />
            <a href="/">Regresar</a>
        </div>
    </div>
    <br />
</asp:Content>
