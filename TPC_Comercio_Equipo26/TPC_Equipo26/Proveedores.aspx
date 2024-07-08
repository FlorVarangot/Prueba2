<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Proveedores.aspx.cs" Inherits="TPC_Equipo26.Proveedores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="text-center my-4">
            <h1>PROVEEDORES</h1>
        </div>
        <div class="row mb-2">
            <div class="col-4">
                <asp:TextBox runat="server" ID="TxtFiltro" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtFiltro_TextChanged" Placeholder="Buscar..." />
            </div>
            <div class="col-3">
                <asp:DropDownList ID="DdlOrdenarPor" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlOrdenarPor_SelectedIndexChanged">
                    <asp:ListItem Text="Ordenar por..." Value="" />
                    <asp:ListItem Text="Nombre A-Z" Value="NombreAZ" />
                    <asp:ListItem Text="Nombre Z-A" Value="NombreZA" />
                    <asp:ListItem Text="ID ↑" Value="IdAsc" />
                    <asp:ListItem Text="ID ↓" Value="IdDesc" />
                </asp:DropDownList>
            </div>
            <div class="col-4 text-end">
                <asp:Button runat="server" ID="BtnLimpiarFiltros" Text="Reestablecer filtros" OnClick="BtnLimpiarFiltros_Click" CssClass="btn btn-light" Style="background-color: lightgray; color: dimgray" />
            </div>
        </div>
        <div class="row mb-3">
            <div class="col-12">
                <asp:CheckBox runat="server" ID="ChkIncluirInactivos" Text="Incluir inactivos" AutoPostBack="true" OnCheckedChanged="FiltroInactivos_CheckedChanged" />
            </div>
        </div>

    </div>

    <asp:GridView ID="GvProveedores" runat="server" DataKeyNames="ID" CssClass="table table-success table-hover" Style="text-align: center" AutoGenerateColumns="false"
        EmptyDataText="No hay datos disponibles." OnSelectedIndexChanged="GvProveedores_SelectedIndexChanged">
        <Columns>
            <asp:BoundField HeaderText="Id" DataField="ID" />
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:BoundField HeaderText="CUIT" DataField="CUIT" />
            <asp:BoundField HeaderText="E-Mail" DataField="Email" />
            <asp:BoundField HeaderText="Teléfono" DataField="Telefono" />
            <asp:BoundField HeaderText="Dirección" DataField="Direccion" />
            <asp:TemplateField HeaderText="Estado">
                <ItemTemplate>
                    <%# Convert.ToBoolean(Eval("Activo")) ? "Disponible" : "No disponible" %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <a href='<%# "AltaProveedor.aspx?ID=" + Eval("ID") %>' class="icono" title="Gestionar">
                        <i class="fa-solid fa-pen" style="color: dimgrey; margin: 10px"></i>
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <hr />
     <% if (ValidarSesion())
     {%>
    <div class="text-end">
        <a href="AltaProveedor.aspx" class="btn btn-success">Agregar un Proveedor</a>
    </div>
    <% } %>
</asp:Content>
