<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Compras.aspx.cs" Inherits="TPC_Equipo26.Compras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="text-center my-4">
        <h1>COMPRAS</h1>
    </div>
    <div class="container">
         <div class="row mb-4">
            <div class="col-md-4">
                <asp:Label runat="server" Text="Fecha de Compra:" AssociatedControlID="txtFechaCompra" />
               <asp:TextBox runat="server" ID="txtFechaCompra" CssClass="form-control" placeholder="dd/mm/aa" />
            </div>
            <div class="col-md-4">
                <asp:Label runat="server" Text="Proveedor:" AssociatedControlID="ddlProveedor" />
                <asp:DropDownList runat="server" ID="ddlProveedor" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="col-md-4">
                
                <asp:Label ID="lblTotalCompra" runat="server" Text="Total compra: $" visible="true"/>
            </div>
        </div>

        <h3 class="my-4">Detalles de la Compra</h3>
        <div class="row mb-2">
            <div class="col-md-4">
                <asp:Label runat="server" Text="Artículo:" AssociatedControlID="ddlArticulo" />
                <asp:DropDownList runat="server" ID="ddlArticulo" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="col-md-2">
                <asp:Label runat="server" Text="Cantidad:" AssociatedControlID="txtCantidad" />
                <asp:TextBox runat="server" ID="txtCantidad" CssClass="form-control" />
            </div>
            <div class="col-md-2">
                <asp:Label runat="server" Text="Precio Unitario:" AssociatedControlID="txtPrecio" />
                <asp:TextBox runat="server" ID="txtPrecio" CssClass="form-control" />
            </div>
            <div class="col-md-2 d-flex align-items-end">
                <asp:Button runat="server" ID="btnAgregarDetalle" Text="+" CssClass="btn btn-primary btn-agregar" />
            </div>
        </div>
</asp:Content>
