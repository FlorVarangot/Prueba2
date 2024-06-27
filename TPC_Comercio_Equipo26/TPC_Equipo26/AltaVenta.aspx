<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AltaVenta.aspx.cs" Inherits="TPC_Equipo26.AltaVenta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="my-4">
        <asp:Label ID="lblTituloVenta" runat="server" Text="REGISTRO DE VENTA" Visible="false" CssClass="titulo-label"></asp:Label>
    </div>
    <div class="container">
        <div class="row mb-4">
            <div class="col-2">
                <label for="txtFechaVenta" class="form-label">Fecha de Venta:</label>
                <asp:TextBox runat="server" type="date" ID="txtFechaVenta" CssClass="form-control" placeholder="dd/mm/aa" />
            </div>
            <div class="col-4">
                <label for="ddlCliente" class="form-label">Cliente:</label>
                <asp:DropDownList runat="server" ID="ddlCliente" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlCliente_SelectedIndexChanged"></asp:DropDownList>
            </div>
        </div>

        <div class="row mb-2">
            <div class="col-6">
                <label for="ddlArticulo" class="form-label">Artículo:</label>
                <asp:DropDownList runat="server" ID="ddlArticulo" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="col-2">
                <label for="txtCantidad" class="form-label">Cantidad:</label>
                <input type="number" id="numCantidad" cssclass="form-control" runat="server" style="border-radius: 5px; height: 40px">
            </div>
            <div class="col-2">
                <label for="lblPrecio" class="form-label">Precio Unitario:</label>
                <asp:Label ID="lblPrecio" runat="server"></asp:Label>
            </div>
            <div class="col-2 d-flex align-items-end">
                <asp:Button runat="server" ID="btnAgregar" Text="+" CssClass="btn btn-primary btn-agregar" Autopostback="true" OnClick="btnAgregar_Click" />
            </div>
        </div>
        <div class="row mt-4">
            <div class="col-md-12 text-center">
                <asp:Button runat="server" ID="btnGuardarVenta" Text="Confirmar" CssClass="btn btn-success" OnClick="btnConfirmarVenta_Click" />
            </div>
        </div>

        <asp:Label ID="lblDetalles" runat="server" Text="Detalles de la Venta:" Visible="false" CssClass="titulo-label"></asp:Label>
        <asp:Panel ID="panelDetallesVenta" runat="server">
            <asp:Repeater ID="rptArticulos" runat="server">
                <ItemTemplate>
                    <div>
                        Artículo: <%# Eval("IdArticulo") %>, Cantidad: <%# Eval("Cantidad") %>, Total parcial:  %>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </asp:Panel>

        <div class="col-12 text-end">
            <label for="lblTotalVenta" class="form-label">Total venta:</label>
            <asp:Label ID="lblTotalVenta" runat="server"></asp:Label>
        </div>

    </div>
</asp:Content>
