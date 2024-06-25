<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="DetallesCompra.aspx.cs" Inherits="TPC_Equipo26.DetallesCompra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <h1>Detalles de Compra</h1>
                <asp:GridView ID="gvDetalle" runat="server" CssClass="table table-success table-hover"
                    Style="text-align: center" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField HeaderText="ID Artículo" DataField="IdArticulo" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="Precio" DataField="Precio" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
