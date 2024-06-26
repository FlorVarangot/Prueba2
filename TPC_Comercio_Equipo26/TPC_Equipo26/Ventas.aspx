<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Ventas.aspx.cs" Inherits="TPC_Equipo26.Ventas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="text-center my-4">
            <h1>VENTAS</h1>
        </div>
        <div class="row">
            <div class="col-4 mb-3">
                <asp:TextBox runat="server" ID="TxtFiltro" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtFiltro_TextChanged" Placeholder="Buscar" />
            </div>
            <div class="col-4 md-3">
                <div class="mb-3">
                    <asp:DropDownList ID="ddlCliente" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCliente_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row mb-3">
                <div class="col-1">
                    <asp:CheckBox ID="ChkOrdenarPorFecha" runat="server" Text="↑↓ Fecha" AutoPostBack="true" OnCheckedChanged="ChkOrdenarPorFecha_CheckedChanged" />
                </div>
                <div class="col-1">
                    <asp:CheckBox ID="ChkOrdenarPorTotal" runat="server" Text="↑↓ Total" AutoPostBack="true" OnCheckedChanged="ChkOrdenarPorTotal_CheckedChanged" />
                </div>
                <div class="col-10 text-end">
                    <asp:Button runat="server" ID="BtnLimpiarFiltros" Text="Reestablecer filtros" OnClick="BtnLimpiarFiltros_Click" CssClass="btn btn-light" Style="background-color: lightgray; color: dimgray" />
                </div>
            </div>
        </div>
    </div>


    <asp:GridView ID="GvVentas" runat="server" DataKeyNames="ID" CssClass="table table-success table-hover" Style="text-align: center" AutoGenerateColumns="false"
        OnSelectedIndexChanged="GvVentas_SelectedIndexChanged" OnPageIndexChanging="GvVentas_PageIndexChanging" EmptyDataText="Aún no hay datos disponibles"
        AllowPaging="true" PageSize="10" OnRowDataBound="GvVentas_RowDataBound">
        <Columns>
            <asp:BoundField HeaderText="Venta" DataField="ID" />
            <asp:BoundField HeaderText="Fecha" DataField="FechaVenta" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField HeaderText="Cliente" DataField="IdCliente" />
            <asp:BoundField HeaderText="Total" DataField="Total" DataFormatString="{0:C}" />

            <asp:TemplateField>
                <ItemTemplate>
                    <a href='<%# "DetalleVenta.aspx?ID=" + Eval("ID") %>' class="icono" title="Ver detalle">
                        <i class="fa-solid fa-magnifying-glass" style="color: dimgrey; margin: 10px"></i>
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <asp:Label ID="lblVacio" Text="No se encontraron registros con ese criterio." runat="server" />
    <hr />
    <div class="text-end">
        <a href="AltaVenta.aspx" class="btn btn-success btn-success">Registrar una venta</a>
    </div>

</asp:Content>

