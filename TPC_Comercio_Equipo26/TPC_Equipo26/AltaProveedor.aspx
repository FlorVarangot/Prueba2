<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AltaProveedor.aspx.cs" Inherits="TPC_Equipo26.AltaProveedor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="my-4">
        <asp:Label ID="lblTituloAgregar" runat="server" Text="AGREGAR PROVEEDOR" Visible="false" CssClass="titulo-label"></asp:Label>
        <asp:Label ID="lblTituloModificar" runat="server" Text="MODIFICAR PROVEEDOR" Visible="false" CssClass="titulo-label"></asp:Label>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="row">
        <div class="col">
            <div class="mb-3">
                <label for="txtNombre" class="form-label">Nombre</label>
                <asp:TextBox runat="server" ID="TxtNombre" CssClass="form-control" Required="true" />
                <asp:Label runat="server" ID="lblNombre" Text="*" ForeColor="Red" Visible="false"></asp:Label>
                <asp:RegularExpressionValidator runat="server" ControlToValidate="TxtNombre" ErrorMessage="El Nombre debe contener letras" ValidationExpression="^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$" ForeColor="Red" />
            </div>
        </div>
        <div class="mb-3">
            <label for="txtCUIT" class="form-label">CUIT</label>
            <asp:TextBox runat="server" ID="TxtCuit" CssClass="form-control" Required="true" />
            <asp:Label runat="server" ID="lblCuit" Text="*" ForeColor="Red" Visible="false"></asp:Label>
            <asp:RegularExpressionValidator runat="server" ControlToValidate="TxtCuit" ErrorMessage="El Cuit debe contener únicamente números y - " ValidationExpression="^\d{2}-\d{7,8}-\d{1}$" ForeColor="Red" />
        </div>
    </div>
    <div class="mb-3">
        <label for="txtEmail" class="form-label">E-Mail</label>
        <asp:TextBox runat="server" ID="TxtEmail" CssClass="form-control" Required="true" />
        <asp:Label runat="server" ID="lblEmail" Text="*" ForeColor="Red" Visible="false"></asp:Label>
        <asp:RegularExpressionValidator runat="server" ControlToValidate="TxtEmail" ErrorMessage="Ingrese un email válido" ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$" ForeColor="Red"></asp:RegularExpressionValidator>
    </div>
    <div class="mb-3">
        <label for="txtTelefono" class="form-label">Teléfono</label>
        <asp:TextBox runat="server" ID="TxtTel" CssClass="form-control" Required="true" />
        <asp:Label runat="server" ID="lblTel" Text="*" ForeColor="Red" Visible="false"></asp:Label>
        <asp:RegularExpressionValidator runat="server" ControlToValidate="TxtTel" ErrorMessage="El Telefono debe contener únicamente números" ValidationExpression="^\d+$" ForeColor="Red" />
    </div>
    <div class="mb-3">
        <label for="txtDireccion" class="form-label">Dirección</label>
        <asp:TextBox runat="server" ID="TxtDirec" CssClass="form-control" Required="true" />
        <asp:Label runat="server" ID="lblDirec" Text="*" ForeColor="Red" Visible="false"></asp:Label>
    </div>
    <div class="mb-3" style="align-content: center; text-align: center; margin: 20px">
        <asp:Button Text="Aceptar" ID="BtnAceptar" CssClass="btn btn-success" OnClick="BtnAceptar_Click" runat="server" />
        <a href="Proveedores.aspx" class="btn btn-danger">Cancelar</a>
        <asp:Button Text="Inactivar" ID="BtnInactivar" CssClass="btn btn-warning" OnClick="BtnInactivar_Click" runat="server" />
        <asp:Button Text="Reactivar" ID="BtnReactivar" CssClass="btn btn-primary" OnClick="BtnReactivar_Click" runat="server" />
    </div>
    <asp:Label ID="lblError" runat="server" Text="" CssClass="text-danger" Visible="false"></asp:Label>
    <% if (ConfirmarInactivar)
        { %>
    <div class="mb-3" style="align-content: center; text-align: center; margin: 20px">
        <asp:CheckBox Text="Confirmar inactivación" ID="ChkConfirmaInactivacion" runat="server" />
        <asp:Button Text="Inactivar" ID="BtnConfirmaInactivar" OnClick="BtnConfirmaInactivar_Click" CssClass="btn btn-outline-danger" runat="server" />
    </div>
    <% } %>
    <% if (ConfirmarReactivar)
        { %>
    <div class="mb-3" style="align-content: center; text-align: center; margin: 20px">
        <asp:CheckBox Text="Confirmar reactivación" ID="ChkConfirmaReactivacion" runat="server" />
        <asp:Button Text="Reactivar" ID="BtnConfirmaReactivar" OnClick="BtnConfirmaReactivar_Click" CssClass="btn btn-outline-primary" runat="server" />
    </div>
    <% } %>
</asp:Content>
