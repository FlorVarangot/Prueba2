<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AltaCompra.aspx.cs" Inherits="TPC_Equipo26.AltaCompra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="text-center my-4">
        <h1>FORMULARIO COMPRA</h1>
    </div>
    <div class="container">
        <div class="row mb-4">
            <div class="col-md-4">
                <label for="txtFechaCompra" class="form-label">Fecha de Compra:</label>
                <asp:TextBox runat="server" type="date" ID="txtFechaCompra" CssClass="form-control" placeholder="dd/MM/yyyy" />
            </div>

            <div class="col-md-4">
                <label for="ddlProveedor" class="form-label">Proveedor:</label>
                <asp:DropDownList runat="server" ID="ddlProveedor" CssClass="form-control" AutoPostBack="True" Required="true" OnSelectedIndexChanged="ddlProveedor_SelectedIndexChanged"></asp:DropDownList>
                ¿No existe el proveedor? <a href="AltaProveedor.aspx">Agregar proveedor</a>
            </div>
            <div class="col-md-2">
                <label for="lblTotalCompra" class="form-label">Total compra:</label>
                <asp:Label ID="lblTotalCompra" runat="server" CssClass="form-control"></asp:Label>
            </div>
        </div>


        <div class="row mb-2">
            <div class="col-md-3">
                <label for="ddlMarca" class="form-label">Marca:</label>
                <asp:DropDownList runat="server" ID="ddlMarca" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlMarca_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div class="col-md-3">
                <label for="ddlArticulo" class="form-label">Artículo:</label>
                <asp:DropDownList runat="server" ID="ddlArticulo" CssClass="form-control" OnSelectedIndexChanged="ddlArticulo_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div class="col-md-2">
                <label for="txtCantidad" class="form-label">Cantidad:</label>
                <asp:TextBox runat="server" ID="txtCantidad" CssClass="form-control" />
                <asp:RegularExpressionValidator runat="server" ControlToValidate="txtCantidad" ErrorMessage="Ingrese una cantidad valida" ValidationExpression="^\d+$" ForeColor="Red" />
            </div>
            <div class="col-md-2">
                <label for="txtPrecio" class="form-label">Precio Unitario:</label>
                <asp:TextBox runat="server" ID="txtPrecio" CssClass="form-control" />
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtPrecio" ErrorMessage="Ingrese un precio valido" ValidationExpression="^\d+$" ForeColor="Red" />         
            </div>
            <div class="col-md-2 d-flex align-items-end">
                <asp:Button runat="server" ID="btnAgregar" Text="+" CssClass="btn btn-primary btn-agregar" Autopostback="true" OnClick="btnAgregar_Click1" />
            </div>
        </div>
        <div class="row mt-4">
            <div class="col-md-12 text-center">
                <asp:Button runat="server" ID="btnGuardarCompra" Text="Guardar Compra" CssClass="btn btn-success" OnClick="btnGuardarCompra_Click" OnClientClick="alert('Compra a proveedor registrada con éxito.'); return true;" Visible="false" />
            </div>
        </div>
    </div>
    <h3 class="my-4">Detalles de la Compra</h3>
    <div class="row">
        <div class="col-md-12">
            <div class="card mb-2">
                <div class="card-body">
                    <asp:Panel ID="panelDetallesCompra" runat="server">
                        <asp:Repeater ID="rptArticulosAgregados" runat="server" OnItemCommand="rptArticulosAgregados_ItemCommand">
                            <ItemTemplate>
                                <div class="row mb-2">
                                    <div class="col-md-4">
                                        <strong>Artículo:</strong> <%# Eval("NombreArticulo") %>
                                    </div>
                                    <div class="col-md-3">
                                        <strong>Cantidad:</strong> <%# Eval("Cantidad") %>
                                    </div>
                                    <div class="col-md-3">
                                        <strong>Precio:</strong> <%# Eval("Precio", "{0:C2}") %>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Button runat="server" CommandName="Editar" CommandArgument='<%# Eval("Id") + "_Editar" %>'
                                            Text="Editar" CssClass="btn btn-sm btn-primary" OnClientClick="return confirm('¿Está seguro que desea editar este artículo?');" />
                                        <asp:Button runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("Id") + "_Eliminar" %>'
                                            Text="Eliminar" CssClass="btn btn-sm btn-danger ml-1" OnClientClick="return confirm('¿Está seguro que desea eliminar este artículo?');" />
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
    <a href="Compras.aspx" class="btn btn-danger">Cancelar</a>
    <asp:Label ID="lblError" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
</asp:Content>
