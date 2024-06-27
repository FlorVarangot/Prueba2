<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Marcas.aspx.cs" Inherits="TPC_Equipo26.Marcas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="text-center my-4">
            <h1>MARCAS</h1>
        </div>
        <div class="row">
            <div class="col-4">
                <asp:TextBox runat="server" ID="txtFiltro" CssClass="form-control" AutoPostBack="true" OnTextChanged="Filtro_TextChanged" Placeholder="Buscar" />
            </div>
            <div class="col-4">
                <asp:DropDownList runat="server" ID="ddlProveedor" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="FiltroProveedor_SelectedIndexChanged" />
            </div>
            <div class="col-1">
                <asp:Button runat="server" ID="btnLimpiarFiltros" Text="Reestablecer filtros" OnClick="BtnLimpiarFiltros_Click" CssClass="btn btn-light" Style="background-color: lightgray; color: dimgray" />
            </div>
        </div>
        <div class="row">
            <div class="col-12 mb-2">
                <asp:CheckBox runat="server" ID="chkIncluirInactivos" Text="Incluir inactivos" AutoPostBack="true" OnCheckedChanged="FiltroInactivos_CheckedChanged" />
            </div>
        </div>
    </div>


    <asp:GridView ID="gvMarcas" runat="server" DataKeyNames="ID" CssClass="table table-success table-hover"
        Style="text-align: center" AutoGenerateColumns="false"
        OnSelectedIndexChanged="GvMarcas_SelectedIndexChanged" OnPageIndexChanging="GvMarcas_PageIndexChanging"
        AllowPaging="true" PageSize="10" OnRowDataBound="GvMarcas_RowDataBound">
        <Columns>
            <asp:BoundField HeaderText="Id" DataField="ID" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField HeaderText="Marca" DataField="Descripcion" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField HeaderText="Proveedor" DataField="IdProveedor" ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText="Imagen">
                <ItemTemplate>
                    <asp:Image ID="imgMarca" runat="server" ImageUrl='<%# Eval("ImagenUrl") %>' AlternateText="Logo de marca" Style="height: 40px; width: 45px;" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Estado">
                <ItemTemplate>
                    <%# Convert.ToBoolean(Eval("Activo")) ? "Disponible" : "No disponible" %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <a href='<%# "AltaMarca.aspx?ID=" + Eval("ID") %>' class="icono" title="Gestionar">
                        <i class="fa-solid fa-pen" style="color: dimgrey; margin: 10px"></i>
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:Label ID="lblVacio" Text="No se encontraron registros." runat="server" />
    <hr />
    <div class="text-end">
        <a href="AltaMarca.aspx" class="btn btn-success btn-success">Agregar una marca</a>
    </div>
</asp:Content>

