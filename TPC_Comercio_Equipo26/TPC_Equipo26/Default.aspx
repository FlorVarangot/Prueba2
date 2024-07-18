<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TPC_Equipo26.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="text-center my-4">
            <h1>ARTÍCULOS</h1>
        </div>

        <div class="row mb-2">
            <div class="col-6">
                <asp:TextBox runat="server" ID="txtFiltro" ClientIDMode="Static" CssClass="form-control" AutoPostBack="true" OnTextChanged="Filtro_TextChanged" Placeholder="Buscar artículos, marcas y más..." TextMode="Search"/>
             </div>
            <div class="col-6" style="display: flex; flex-direction: column">
                <asp:CheckBox Text="Filtro" CssClass="" ID="chkAvanzado" runat="server" AutoPostBack="true" OnCheckedChanged="chkAvanzado_CheckedChanged" />
            </div>
        </div>
        <div class="row mb-2">
            <div class="col-12">
                <asp:CheckBox runat="server" ID="chkIncluirInactivos" Text="Incluir inactivos" AutoPostBack="true" OnCheckedChanged="chkIncluirInactivos_CheckedChanged" />
            </div>
        </div>
        <asp:Panel ID="pnlFiltroAvanzado" runat="server" Visible="false">
            <div class="row">
                <div class="col-3 mb-3">
                    <asp:Label Text="Marca" ID="lblMarca" runat="server" />
                    <asp:DropDownList runat="server" CssClass="form-select" ID="ddlMarca" AutoPostBack="true" OnSelectedIndexChanged="ddlMarca_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="col-3 mb-3">
                    <asp:Label Text="Categoría" ID="lblCategoria" runat="server" />
                    <asp:DropDownList runat="server" CssClass="form-select" ID="ddlCategoria" AutoPostBack="true" OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="col-3 mb-3 mt-4">
                    <asp:DropDownList ID="ddlOrdenarPor" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlOrdenarPor_SelectedIndexChanged">
                        <asp:ListItem Text="Ordenar por..." Value="" />
                        <asp:ListItem Text="Descripcion A-Z" Value="DescripcionAZ" />
                        <asp:ListItem Text="Descripcion Z-A" Value="DescripcionZA" />
                        <asp:ListItem Text="Stock ↓" Value="StockDisponibleDesc" />
                        <asp:ListItem Text="Stock ↑" Value="StockDisponibleAsc" />
                        <asp:ListItem Text="Precio ↓" Value="PrecioUnitarioDesc" />
                        <asp:ListItem Text="ID ↑" Value="IdAsc" />
                        <asp:ListItem Text="ID ↓" Value="IdDesc" />
                    </asp:DropDownList>
                </div>
                <div class="col-3 mb-3 mt-4 text-end">
                    <asp:Button runat="server" ID="btnLimpiarFiltros" Text="Reestablecer filtros" OnClick="BtnLimpiarFiltros_Click" CssClass="btn btn-light" Style="background-color: lightgray; color: dimgray" />
                </div>
            </div>
        </asp:Panel>
    </div>

    <asp:GridView ID="gvArticulos" runat="server" DataKeyNames="ID" CssClass="table table-success table-hover"
        Style="text-align: center" AutoGenerateColumns="false" EmptyDataText="No hay datos disponibles"
        OnSelectedIndexChanged="gvArticulos_SelectedIndexChanged" OnPageIndexChanging="gvArticulos_PageIndexChanging"
        AllowPaging="true" PageSize="10" OnRowDataBound="gvArticulos_RowDataBound">
        <Columns>
            <asp:BoundField HeaderText="Id" DataField="ID" />
            <asp:BoundField HeaderText="Código" DataField="Codigo" />
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />
            <asp:BoundField HeaderText="Marca" DataField="Marca.Descripcion" />
            <asp:BoundField HeaderText="Categoría" DataField="Categoria.Descripcion" />
            <asp:TemplateField HeaderText="Imagen">
                <ItemTemplate>
                    <asp:Image ID="imgArticulo" runat="server" ImageUrl='<%# Eval("Imagen") %>' AlternateText="Imagen del artículo" Style="height: 40px; width: 45px;" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField HeaderText="Precio Unitario ($)" />
            <asp:BoundField HeaderText="Stock disponible" />
            <asp:BoundField HeaderText="Stock Mínimo" DataField="StockMin" />

            <asp:TemplateField HeaderText="Estado">
                <ItemTemplate>
                    <%# Convert.ToBoolean(Eval("Activo")) ? "Disponible" : "No disponible" %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <a href='<%# "AltaArticulo.aspx?ID=" + Eval("ID") %>' class="icono" title="Gestionar">
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
        <a href="AltaArticulo.aspx" class="btn btn-success">Agregar un artículo</a>
    </div>
    <% } %>

    <script type="text/javascript">
    document.addEventListener('DOMContentLoaded', function() {
        var searchBox = document.getElementById('<%= txtFiltro.ClientID %>');
        
        if (searchBox) {
            searchBox.addEventListener('input', function() {
                if (searchBox.value === '') {
                    searchBox.dispatchEvent(new Event('change'));
                }
            });

            searchBox.addEventListener('change', function() {
                __doPostBack(searchBox.id, '');
            });
        }
    });
</script>
</asp:Content>
