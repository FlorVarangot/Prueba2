﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Categorias.aspx.cs" Inherits="TPC_Equipo26.Categorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>CATEGORÍAS</h1>
    <asp:GridView ID="gvCategorias" class="table" Style="text-align: center" runat="server" CssClass="table table-light table-bordered" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField HeaderText="Id" DataField="ID" />
            <asp:BoundField HeaderText="Categoría" DataField="Descripcion" />
            <asp:BoundField HeaderText="Activo" DataField="Activo" />
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
    <a href="#">Agregar una categoría</a>
</asp:Content>
