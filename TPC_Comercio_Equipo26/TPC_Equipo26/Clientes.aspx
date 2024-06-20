<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="TPC_Equipo26.Clientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="text-center my-4">
            <h1>CLIENTES</h1>
        </div>
        <div class="row">
            <div class="col-4">
                <asp:TextBox runat="server" ID="txtFiltro" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtFiltro_TextChanged" Placeholder="Buscar" />
            </div>
            <div class="col-1">
                <asp:Button runat="server" ID="btnLimpiarFiltros" Text="Reestablecer filtros" OnClick="btnLimpiarFiltros_Click" CssClass="btn btn-light" Style="background-color: lightgray; color: dimgray" />
            </div>
        </div>
        <div class="row">
            <div class="col-12 mb-2">
                <asp:CheckBox runat="server" ID="chkIncluirInactivos" Text="Incluir inactivos" AutoPostBack="true" OnCheckedChanged="chkIncluirInactivos_CheckedChanged" />
            </div>
        </div>

        <asp:GridView ID="gvClientes" runat="server" DataKeyNames="Id" CssClass="table table-success table-hover"
            Style="text-align: center" AutoGenerateColumns="false"
            OnSelectedIndexChanged="gvClientes_SelectedIndexChanged">
            <Columns>
                <asp:BoundField HeaderText="Id" DataField="ID" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="Nombre" DataField="Nombre" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="Apellido" DataField="Apellido" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="Dni" DataField="Dni" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="Teléfono" DataField="Telefono" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="Email" DataField="Email" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="Dirección" DataField="Direccion" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="Estado">
                    <ItemTemplate>
                        <%# Convert.ToBoolean(Eval("Activo")) ? "Disponible" : "No disponible" %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <a href='<%# "AltaCliente.aspx?ID=" + Eval("ID") %>' class="icono" title="Gestionar">
                            <i class="fa-solid fa-pen" style="color: dimgrey; margin: 10px"></i>
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Label ID="lblVacio" Text="No se encontraron clientes con ese criterio." runat="server" />
        <hr />
        <div class="text-end">
            <a href="AltaCliente.aspx" class="btn btn-success">Agregar un Cliente</a>
        </div>
    </div>
</asp:Content>
