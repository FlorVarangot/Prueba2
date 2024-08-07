﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Ventas.aspx.cs" Inherits="TPC_Equipo26.Ventas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="text-center my-4">
            <h1>VENTAS</h1>
        </div>

        <div class="row mb-4">
            <div class="col-4">
                <asp:DropDownList ID="ddlCliente" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCliente_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div class="col-2">
                <asp:DropDownList ID="ddlOrdenarPor" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlOrdenarPor_SelectedIndexChanged">
                    <asp:ListItem Text="Ordenar por..." Value="" />
                    <asp:ListItem Text="Mayor precio" Value="MayorPrecio" />
                    <asp:ListItem Text="Menor precio" Value="MenorPrecio" />
                    <asp:ListItem Text="Fecha más reciente" Value="FechaReciente" />
                    <asp:ListItem Text="Fecha más antigua" Value="FechaAntigua" />
                    <asp:ListItem Text="Número de venta ↑" Value="VentaAsc" />
                    <asp:ListItem Text="Número de venta ↓" Value="VentaDesc" />
                </asp:DropDownList>
            </div>
            <div class="col-6 text-end">
                <asp:Button runat="server" ID="BtnLimpiarFiltros" Text="Reestablecer filtros" OnClick="BtnLimpiarFiltros_Click" CssClass="btn btn-light" Style="background-color: lightgray; color: dimgray" Visible="false" />
            </div>
        </div>
    </div>

    <asp:GridView ID="GvVentas" runat="server" DataKeyNames="ID" CssClass="table table-success table-hover" Style="text-align: center" AutoGenerateColumns="false"
        OnSelectedIndexChanged="GvVentas_SelectedIndexChanged" EmptyDataText="No hay datos disponibles." OnRowDataBound="GvVentas_RowDataBound" OnPageIndexChanging="GvVentas_PageIndexChanging"
        AllowPaging="true" PageSize="10">
        <Columns>
            <asp:BoundField HeaderText="Venta" DataField="ID" />
            <asp:BoundField HeaderText="Fecha" DataField="FechaVenta" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField HeaderText="Cliente" DataField="IdCliente" />
            <asp:BoundField HeaderText="Total" DataField="Total" DataFormatString="{0:C}" />

            <asp:TemplateField>
                <ItemTemplate>
                    <a href='<%# "DetallesVenta.aspx?ID=" + Eval("ID") %>' class="icono" title="Ver detalle">
                        <i class="fa-solid fa-magnifying-glass" style="color: dimgrey; margin: 10px"></i>
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <hr />
    <div class="text-end">
        <a href="AltaVenta.aspx" class="btn btn-success btn-success">Registrar una venta</a>
    </div>
</asp:Content>

