<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Proveedores.aspx.cs" Inherits="TPC_Equipo26.Proveedores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>PROVEEDORES</h1>
    <div class="container">
        <div class="row">
            <asp:Label Text="Buscar:" runat="server" CssClass="form-label" />
            <div class="col-2">
                <asp:TextBox runat="server" ID="TxtFiltro" CssClass="form-control" AutoPostBack="true" OnTextChanged="TxtFiltro_TextChanged" />
            </div>
            <div class="row">
                <div class="col-2">
                    <asp:CheckBox runat="server" ID="ChkIncluirInactivos" Text="Incluir inactivos" AutoPostBack="true" OnCheckedChanged="FiltroInactivos_CheckedChanged"/>
                </div>
            </div>
            <div class="row">
                <div class="col-2">
                    <asp:Button runat="server" ID="BtnLimpiarFiltros" Text="Reestablecer filtros" OnClick="BtnLimpiarFiltros_Click" CssClass="btn btn-light mt-3" Style="margin: 15px" />
                </div>
            </div>
        </div>
    </div>

    <asp:GridView ID="GvProveedores" runat="server" DataKeyNames="ID" CssClass="table table-success table-hover"
        Style="text-align: center" AutoGenerateColumns="false"
        OnSelectedIndexChanged="GvProveedores_SelectedIndexChanged">
        <Columns>
            <asp:BoundField HeaderText="Id" DataField="ID" />
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:BoundField HeaderText="CUIT" DataField="CUIT" />
            <asp:BoundField HeaderText="E-Mail" DataField="Email" />
            <asp:BoundField HeaderText="Teléfono" DataField="Telefono" />
            <asp:BoundField HeaderText="Dirección" DataField="Direccion" />
            <asp:BoundField HeaderText="Estado" DataField="Activo" />
            <asp:TemplateField>
                <ItemTemplate>
                    <a href='<%# "AltaProveedor.aspx?ID=" + Eval("ID") %>' class="icono" title="Gestionar">
                        <i class="fa-solid fa-pen" style="color: dimgrey; margin: 10px"></i>
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:Label ID="lblVacio" Text="No se encontraron Proveedores con ese criterio." runat="server" />
    <hr />
    <a href="AltaProveedor.aspx" class="btn btn-success">Agregar un Proveedor</a>
</asp:Content>
