<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="DetallesCompra.aspx.cs" Inherits="TPC_Equipo26.DetallesCompra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <div class="row mt-3 mb-2">
                    <h1>Detalles de Compra</h1>
                </div>
                <div class="row mt-4">
                    <div class="col">
                        <asp:HyperLink ID="lnkVentaAnterior" runat="server" CssClass="grid-header" Style="color: lightgoldenrodyellow" title="Ver venta anterior"> << </asp:HyperLink>
                    </div>
                    <div class="col-4 text-center">
                        <asp:Label runat="server" ID="lblProveedor" Text=""></asp:Label>
                    </div>
                    <div class="col-3 text-center">
                        <asp:Label runat="server" ID="lblFecha" Text=""></asp:Label>
                    </div>
                    <div class="col-3 text-center">
                        <asp:Label runat="server" ID="lblCompraID" Text=""></asp:Label>
                    </div>
                    <div class="col text-end">
                        <asp:HyperLink ID="lnkVentaSiguiente" runat="server" CssClass="grid-header" Style="color: lightgoldenrodyellow; font-weight:300" title="Ver venta siguiente"> >> </asp:HyperLink>
                    </div>
                </div>
                <asp:GridView ID="gvDetalle" runat="server" CssClass="table table-success table-hover" EmptyDataText="No hay datos disponibles"
                    Style="text-align: center" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField HeaderText="Artículo" DataField="NombreArticulo" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="Precio" DataField="Precio" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <a href="Compras.aspx" class="btn btn-danger">Salir</a>
</asp:Content>
