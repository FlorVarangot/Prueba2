<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Categorias.aspx.cs" Inherits="TPC_Equipo26.Categorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="text-center my-4">
            <h1>CATEGORÍAS</h1>
        </div>
        <div class="row mb-2">
            <div class="col-4">
                <asp:TextBox runat="server" ID="txtFiltro" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtFiltro_TextChanged" Placeholder="Buscar" />
            </div>
            <div class="col-3">
                <asp:DropDownList ID="ddlOrdenarPor" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlOrdenarPor_SelectedIndexChanged">
                    <asp:ListItem Text="Ordenar por..." Value="" />
                    <asp:ListItem Text="Nombre A-Z" Value="DescripcionAZ" />
                    <asp:ListItem Text="Nombre Z-A" Value="DescripcionZA" />
                    <asp:ListItem Text="ID ↓" Value="IdDesc" />
                </asp:DropDownList>
            </div>
            <div class="col-4 text-end">
                <asp:Button runat="server" ID="btnLimpiarFiltros" Text="Reestablecer filtros" OnClick="btnLimpiarFiltros_Click" CssClass="btn btn-light" Style="background-color: lightgray; color: dimgray" />
            </div>
        </div>
        <div class="row mb-3">
            <div class="col-12">
                <asp:CheckBox runat="server" ID="chkIncluirInactivos" Text="Incluir inactivos" AutoPostBack="true" OnCheckedChanged="chkIncluirInactivos_CheckedChanged" />
            </div>
        </div>

        <asp:GridView ID="gvCategorias" runat="server" DataKeyNames="ID" CssClass="table table-success table-hover"
            Style="text-align: center" AutoGenerateColumns="false" EmptyDataText="No hay datos disponibles."
            AllowPaging="true" PageSize="10" OnPageIndexChanging="gvCategorias_PageIndexChanging"
            OnSelectedIndexChanged="gvCategorias_SelectedIndexChanged">
            <Columns>
                <asp:BoundField HeaderText="Id" DataField="ID" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField HeaderText="Categoría" DataField="Descripcion" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="Estado">
                    <ItemTemplate>
                        <%# Convert.ToBoolean(Eval("Activo")) ? "Disponible" : "No disponible" %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <a href='<%# "AltaCategoria.aspx?ID=" + Eval("ID") %>' class="icono" title="Gestionar">
                            <i class="fa-solid fa-pen" style="color: dimgrey; margin: 10px"></i>
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <hr />
    <div class="text-end">
        <a href="AltaCategoria.aspx" class="btn btn-success">Agregar una Categoria</a>
    </div>
</asp:Content>
