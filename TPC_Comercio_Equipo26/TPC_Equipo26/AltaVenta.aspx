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
                <asp:DropDownList runat="server" ID="ddlCliente" CssClass="form-select" AutoPostBack="True" OnSelectedIndexChanged="ddlCliente_SelectedIndexChanged"></asp:DropDownList>
                <asp:Label runat="server" ID="lblExists" Text="¿No existe el cliente?"></asp:Label>
                <asp:HyperLink runat="server" ID="lnkAltaCli" href="AltaCliente.aspx">Agregar cliente</asp:HyperLink>
            </div>
        </div>

        <asp:Panel ID="selectores" runat="server" Style="align-content: center">
            <div class="row">
                <div class="col-5">
                    <label for="ddlArticulo" class="form-label">Artículo:</label>
                </div>
                <div class="col-2">
                    <label for="numCantidad" class="form-label">Cantidad:</label>
                </div>
            </div>
            <div class="row mb-3">
                <div class="col-5">
                    <asp:DropDownList runat="server" ID="ddlArticulo" CssClass="form-select" OnSelectedIndexChanged="ddlArticulo_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="col-1">
                    <input type="number" id="numCantidad" min="1" cssclass="form-control" runat="server" style="border-radius: 5px; height: 38px; width: 70px">
                </div>
                <div class="col-2">
                    <asp:Button runat="server" ID="btnAgregar" Text="+" CssClass="btn btn-success" Autopostback="true" OnClick="btnAgregar_Click" />
                </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="panelDetallesVenta" runat="server">
            <asp:Label ID="lblDetalles" runat="server" Text="Detalles de la venta:" Visible="false" CssClass="titulo-label"></asp:Label>
            <asp:GridView ID="gvAltaVenta" runat="server" CssClass="detalle" AutoGenerateColumns="false" Style="text-align: center; align-content: center" AutoSizeMode="true"
                OnPageIndexChanging="gvAltaVenta_PageIndexChanging" OnRowDataBound="gvAltaVenta_RowDataBound" OnRowCommand="gvAltaVenta_RowCommand">
                <Columns>
                    <asp:BoundField HeaderText="Artículo" DataField="IdArticulo" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40%" />
                    <asp:BoundField HeaderText="Precio unitario" DataField="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%" />
                    <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%" />
                    <asp:BoundField HeaderText="Total" DataField="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25%" />
                    <asp:TemplateField ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Button ID="btnEliminar" runat="server" Text="🗑️" Title="Eliminar" CssClass="fa-regular fa-trash-can"
                                CommandName="Editar" CommandArgument='<%# Eval("Id") + "_Editar" %>' OnRowCommand="gvAltaVenta_RowCommand" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </asp:Panel>

        <div class="col-12 text-end">
            <asp:Label ID="lblTotalVenta" runat="server"></asp:Label>
        </div>
        <div class="row my-4">
            <div class="col-md-12 text-center">
                <asp:Button runat="server" ID="btnGuardarVenta" Text="Confirmar venta" CssClass="btn btn-success" OnClick="btnConfirmarVenta_Click" />
            </div>
        </div>
        <a href="Ventas.aspx" class="btn btn-danger">Cancelar</a>

    </div>
</asp:Content>
