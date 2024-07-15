<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="DetallesVenta.aspx.cs" Inherits="TPC_Equipo26.DetallesVenta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row mt-3 mb-2">
            <%--<asp:Label runat="server" ID="lblTitulo" CssClass="titulo-label"></asp:Label>--%>
            <h1>Detalles de Venta</h1>
        </div>
        <div class="row mt-4">
            <div class="col-1">
                <asp:HyperLink ID="lnkVentaAnterior" runat="server" CssClass="grid-header" Style="color: lightgoldenrodyellow" title="Ver venta anterior"> << </asp:HyperLink>
            </div>
            <div class="col-4 text-center">
                <asp:Label runat="server" ID="lblCliente" Text=""></asp:Label>
            </div>
            <div class="col-3 text-center">
                <asp:Label runat="server" ID="lblFecha" Text=""></asp:Label>
            </div>
            <div class="col-3 text-center">
                <asp:Label runat="server" ID="lblVentaID" Text=""></asp:Label>
            </div>
            <div class="col-1 text-end">
                <asp:HyperLink ID="lnkVentaSiguiente" runat="server" CssClass="grid-header" Style="color: lightgoldenrodyellow; font: bold" title="Ver venta siguiente"> >> </asp:HyperLink>
            </div>
        </div>
        <hr />

        <asp:GridView ID="gvDetalle" runat="server" CssClass="detalle" AutoGenerateColumns="false" Style="text-align: center; align-content: center; margin: 5px 20px 15px 30px;" Width="1200px"
            OnSelectedIndexChanged="gvDetalle_SelectedIndexChanged" OnPageIndexChanging="gvDetalle_PageIndexChanging" OnRowDataBound="gvDetalle_RowDataBound" EmptyDataText="No hay datos disponibles">
            <Columns>
                <asp:BoundField HeaderText="Artículo" DataField="IdArticulo" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="45%" />
                <asp:BoundField HeaderText="Precio unitario" DataField="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20%" />
                <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" />
                <asp:BoundField HeaderText="Total parcial" DataField="" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25%" />
            </Columns>
        </asp:GridView>

        <div class="row mt-3">
            <div class="col-11 text-end">
                <asp:Label runat="server" ID="lblTotal" Text=""></asp:Label>
            </div>
        </div>
        <a href="Ventas.aspx" class="btn btn-danger">Salir</a>
    </div>
</asp:Content>
