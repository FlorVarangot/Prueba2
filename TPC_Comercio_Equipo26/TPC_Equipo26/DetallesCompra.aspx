<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="DetallesCompra.aspx.cs" Inherits="TPC_Equipo26.DetallesCompra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <h1>Detalles de Compra</h1>
                <div class="row">
                    <div class="col">
                        <asp:HyperLink ID="lnkVentaAnterior" runat="server" CssClass="grid-header" Style="color: lightgoldenrodyellow" title="Ver venta anterior"> << </asp:HyperLink>
                    </div>
                    <div class="col text-end">
                        <asp:HyperLink ID="lnkVentaSiguiente" runat="server" CssClass="grid-header" Style="color: lightgoldenrodyellow" title="Ver venta siguiente"> >> </asp:HyperLink>
                    </div>
                </div>
                <asp:GridView ID="gvDetalle" runat="server" CssClass="table table-success table-hover" EmptyDataText="No hay datos disponibles."
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
    <a href="Ventas.aspx" class="btn btn-danger">Salir</a>
</asp:Content>
