<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Compras.aspx.cs" Inherits="TPC_Equipo26.Compras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="text-center my-4">
            <h1>COMPRAS</h1>
        </div>
        <asp:GridView ID="gvCompras" runat="server" DataKeyNames="ID" CssClass="table table-success table-hover"
            Style="text-align: center" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField HeaderText="ID Compra" DataField="ID" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="Fecha de Compra" DataField="FechaCompra" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="ID Proveedor" DataField="IdProveedor" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="Total de Compra" DataField="TotalCompra" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="Detalles">
                    <ItemTemplate>
                        <a href='<%# "DetallesCompra.aspx?ID=" + Eval("ID") %>' class="icono" title="Ver Detalles">
                            <i class="fa-solid fa-search" style="color: gray; margin: 10px"></i>
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <div class="text-end">
            <a href="AltaCompra.aspx" class="btn btn-success">Agregar una Compra</a>
        </div>
    </div>
    </asp:Conte
</asp:Content>
