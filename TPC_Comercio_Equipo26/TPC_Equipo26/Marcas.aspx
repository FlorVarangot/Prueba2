<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Marcas.aspx.cs" Inherits="TPC_Equipo26.Marcas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<h1>MARCAS</h1>
<asp:GridView class="table" style="text-align:center" runat="server" CssClass="table table-light table-bordered" ID="gvMarcas" AutoGenerateColumns="false">
    <Columns>
        <asp:BoundField HeaderText="Id" DataField="ID"/>
        <asp:BoundField HeaderText="Marca" DataField="Descripcion" />
        <asp:BoundField HeaderText="Proveedor" DataField="IdProveedor" />
        <asp:BoundField HeaderText="Activo" DataField="Activo" />
        <asp:TemplateField HeaderText="Editar">
            <ItemTemplate>
                <a href="#">
                    <i class="fa-solid fa-pen" style="color:dimgrey; margin:10px"></i>
                </a>
                <a href="#" class="icon">
                    <i class="fa-solid fa-trash-can" style="color:dimgrey; margin:10px"></i>
                </a>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<a href="#">Agregar una marca</a>    
</asp:Content>
