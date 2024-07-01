<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Compras.aspx.cs" Inherits="TPC_Equipo26.Compras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="text-center my-4">
            <h1>COMPRAS</h1>
        </div>
        <div class="row mb-2">
            <div class="col-4">
                <asp:TextBox runat="server" ID="txtFiltro" CssClass="form-control" AutoPostBack="true" Placeholder="Buscar por Fecha..." OnTextChanged="txtFiltro_TextChanged" />
            </div>
            <div class="col-4">
                <div>
                    <asp:DropDownList runat="server" ID="ddlProveedor" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlProveedor_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="col-4" style="display: flex; flex-direction: column">
                    <asp:CheckBox Text="Filtro Avanzado" CssClass="" ID="chkAvanzado" runat="server" AutoPostBack="true" OnCheckedChanged="chkAvanzado_CheckedChanged" />
                </div>
            </div>

            <asp:Panel ID="pnlFiltroAvanzado" runat="server" Visible="false">
                <div class="row mb-3 d-flex align-items-center">
                    <div class="col-2">
                        <asp:CheckBox ID="chkOrdenarFechaAsc" runat="server" Text="Fecha ↑" AutoPostBack="true" OnCheckedChanged="chkOrdenarFechaAsc_CheckedChanged" />
                    </div>
                    <div class="col-2">
                        <asp:CheckBox ID="chkOrdenarFechaDesc" runat="server" Text="Fecha ↓" AutoPostBack="true" OnCheckedChanged="chkOrdenarFechaDesc_CheckedChanged" />
                    </div>
                    <div class="col-2">
                        <asp:CheckBox ID="chkOrdenarPrecioAsc" runat="server" Text="Precio ↑" AutoPostBack="true" OnCheckedChanged="chkOrdenarPrecioAsc_CheckedChanged" />
                    </div>
                    <div class="col-2">
                        <asp:CheckBox ID="chkOrdenarPrecioDesc" runat="server" Text="Precio ↓" AutoPostBack="true" OnCheckedChanged="chkOrdenarPrecioDesc_CheckedChanged" />
                    </div>
                    <div class="col-4 text-end">
                        <asp:Button ID="btnRestablecer" runat="server" Text="Restablecer filtros" OnClick="btnRestablecer_Click" CssClass="btn btn-light" Style="background-color: lightgray; color: dimgray" />
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
    <asp:GridView ID="gvCompras" runat="server" DataKeyNames="ID" CssClass="table table-success table-hover"
        Style="text-align: center" AutoGenerateColumns="false" EmptyDataText="No hay datos disponibles."
        AllowPaging="true" PageSize="10"
        OnPageIndexChanging="gvCompras_PageIndexChanging" OnRowDataBound="gvCompras_RowDataBound">
        <Columns>
            <asp:BoundField HeaderText="Compra" DataField="ID" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField HeaderText="Fecha de Compra" DataField="FechaCompra" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField HeaderText="Proveedor" DataField="IdProveedor" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField HeaderText="Total de Compra" DataField="TotalCompra" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText="Detalles">
                <ItemTemplate>
                    <a href='<%# "DetallesCompra.aspx?ID=" + Eval("ID") %>' class="icono" title="Ver Detalles">
                        <i class="fa-solid fa-search" style="color: dimgrey; margin: 10px"></i>
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div class="text-end">
        <a href="AltaCompra.aspx" class="btn btn-success">Agregar una Compra</a>
    </div>

</asp:Content>
