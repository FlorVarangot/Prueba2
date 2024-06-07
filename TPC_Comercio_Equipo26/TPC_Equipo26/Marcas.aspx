<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Marcas.aspx.cs" Inherits="TPC_Equipo26.Marcas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>MARCAS</h1>
    <asp:GridView class="table" Style="text-align: center" runat="server" CssClass="table table-light table-bordered" ID="gvMarcas" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField HeaderText="Id" DataField="ID" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField HeaderText="Marca" DataField="Descripcion" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField HeaderText="Proveedor" DataField="IdProveedor" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField HeaderText="Activo" DataField="Activo" ItemStyle-HorizontalAlign="Center" />
            <asp:ImageField HeaderText="Imagen" DataImageUrlField="ImagenUrl" ControlStyle-Height="100" ControlStyle-Width="100" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ControlStyle Height="40px" Width="40px" CssClass="rounded-circle" />
            </asp:ImageField>
            <asp:TemplateField>
                <ItemTemplate>
                    <a href="#" class="icono" title="Editar">
                        <i class="fa-solid fa-pen" style="color: dimgrey; margin: 10px"></i>
                    </a>
                    <a href="#" class="icono" title="Ver más detalles">
                        <i class="fa-solid fa-plus" style="color: dimgrey; margin: 10px"></i>
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <a href="#">Agregar una marca</a>
</asp:Content>

