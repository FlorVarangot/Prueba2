<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AltaCompra.aspx.cs" Inherits="TPC_Equipo26.Compras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="text-center my-4">
        <h1>CARGAR COMPRA</h1>
    </div>
    <div class="container">
        <div class="row mb-4">
            <div class="col-md-4">
                <label for="txtFechaCompra" class="form-label">Fecha de Compra:</label>
                <asp:TextBox runat="server" ID="txtFechaCompra" CssClass="form-control" placeholder="dd/mm/aa" />
            </div>
            <div class="col-md-4">
                <label for="ddlProveedor" class="form-label">Proveedor:</label>
                <asp:DropDownList runat="server" ID="ddlProveedor" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlProveedor_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div class="col-md-2">
                <label for="lblTotalCompra" class="form-label">Total compra:</label>
        <asp:Label ID="lblTotalCompra" runat="server" CssClass="form-control">
            <%# "Total compra: $" + ((decimal)Session["Total"]).ToString("N2") %>
        </asp:Label>
            </div>
        </div>

        <h3 class="my-4">Detalles de la Compra</h3>
        <asp:Panel ID="panelDetallesCompra" runat="server">
            <asp:Repeater ID="rptArticulosAgregados" runat="server">
                <ItemTemplate>
                    <div>
                        Artículo: <%# Eval("IdArticulo") %>, Cantidad: <%# Eval("Cantidad") %>, Precio: <%# Eval("Precio", "{0:C2}") %>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </asp:Panel>

        <div class="row mb-2">
            <div class="col-md-3">
                <label for="ddlMarca" class="form-label">Marca:</label>
                <asp:DropDownList runat="server" ID="ddlMarca" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlMarca_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div class="col-md-3">
                <label for="ddlArticulo" class="form-label">Artículo:</label>
                <asp:DropDownList runat="server" ID="ddlArticulo" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="col-md-2">
                <label for="txtCantidad" class="form-label">Cantidad:</label>
                <asp:TextBox runat="server" ID="txtCantidad" CssClass="form-control" />
            </div>
            <div class="col-md-2">
                <label for="txtPrecio" class="form-label">Precio Unitario:</label>
                <asp:TextBox runat="server" ID="txtPrecio" CssClass="form-control" />
            </div>
            <div class="col-md-2 d-flex align-items-end">
                <asp:Button runat="server" ID="btnAgregar" Text="+" CssClass="btn btn-primary btn-agregar" Autopostback="true" OnClick="btnAgregar_Click1" />
            </div>
        </div>
        <div class="row mt-4">
            <div class="col-md-12 text-center">
                <asp:Button runat="server" ID="btnGuardarCompra" Text="Guardar Compra" CssClass="btn btn-success" OnClick="btnGuardarCompra_Click" />
            </div>
        </div>
    </div>
</asp:Content>
