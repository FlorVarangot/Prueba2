<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="DetallesVenta.aspx.cs" Inherits="TPC_Equipo26.DetallesVenta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <div class="row mt-3 mb-2">
                    <div class="col-md-8">
                        <h3>Detalles de venta</h3>
                    </div>
                    <div class="row mb-2">
                        <div class="col-6" style="margin:10px">
                            <asp:Label runat="server" ID="lblCliente" Text=""></asp:Label>
                        </div>
                        <div class="col-4 text-end">
                            <asp:Label runat="server" ID="lblVentaID" Text=""></asp:Label>
                        </div>

                    </div>
                </div>
                
                <asp:GridView ID="gvDetalle" runat="server" CssClass="detalle" AutoGenerateColumns="false" Style="text-align: center; align-content: center; margin:10px 30px;" ItemStyleWidth="1100px"
                    OnSelectedIndexChanged="gvDetalle_SelectedIndexChanged" OnPageIndexChanging="gvDetalle_PageIndexChanging" OnRowDataBound="gvDetalle_RowDataBound">
                    <Columns>
                        <asp:BoundField HeaderText="Artículo" DataField="IdArticulo" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="700px" />
                        <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="150px" />
                        <asp:BoundField HeaderText="Total parcial" DataField="" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="250px" />
                    </Columns>
                </asp:GridView>
                
                <div class="row mt-3">
                    <div class="col-10 text-end">
                        <asp:Label runat="server" ID="lblTotal" Text=""></asp:Label>
                    </div>
                </div>

            </div>
        </div>
    </div>

</asp:Content>
