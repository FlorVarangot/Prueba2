<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AltaArticulo.aspx.cs" Inherits="TPC_Equipo26.AltaArticulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="my-4">
        <asp:Label ID="lblTituloAgregar" runat="server" Text="AGREGAR ARTICULO NUEVO" Visible="false" CssClass="titulo-label"></asp:Label>
        <asp:Label ID="lblTituloModificar" runat="server" Text="MODIFICAR ARTICULO" Visible="false" CssClass="titulo-label"></asp:Label>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <div class="col-6">
            <div class="mb-3">
                <label for="txtCodigo" class="form-label">Codigo: </label>
                <asp:TextBox runat="server" ID="txtCodigo" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtNombre" class="form-label">Nombre: </label>
                <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtDescripcion" class="form-label">Descripcion: </label>
                <asp:TextBox runat="server" ID="txtDescripcion" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="ddlMarca" class="form-label">Marca:</label>
                <asp:DropDownList ID="ddlMarca" CssClass="form-select" runat="server" AutoPostBack="true" AppendDataBoundItems="true">
                    <asp:ListItem Text="Seleccionar..." Value="-1" />
                </asp:DropDownList>
            </div>
            <div class="mb-3">
                <label for="ddlCategoria" class="form-label">Categoria: </label>
                <asp:DropDownList ID="ddlCategoria" CssClass="form-select" runat="server" AutoPostBack="true" AppendDataBoundItems="true">
                    <asp:ListItem Text="Seleccionar..." Value="-1" />
                </asp:DropDownList>
            </div>
        </div>

        <div class="col-6">
            <div class="mb-3">
                <label for="numGanancia" class="form-label">Porcentaje de ganancia aplicable sobre el costo (%): </label>
                <div>
                    <input type="number" id="numGanancia" value="10" cssClass="form-control" runat="server" style="border-radius: 5px; height:40px; width:635px" step=".01">
                </div>
            </div>
            <div class="mb-3">
                <label for="numStockMinimo" class="form-label">Stock mínimo: </label>
                <div>
                    <input type="number" id="numStockMinimo" value="1" min="1" cssClass="form-control" runat="server" style="border-radius: 5px; height:40px; width:635px">
                </div>
            </div>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="mb-3">
                        <label for="txtImagenUrl" class="form-label">Url de imagen: </label>
                        <asp:TextBox runat="server" ID="txtImagenUrl" CssClass="form-control"
                            AutoPostBack="true" OnTextChanged="txtImagenUrl_TextChanged" />
                    </div>
                    <asp:Image runat="server" ID="imgArticulos" Width="20%" Style="max-width: 100%; height: auto;"
                        ImageUrl="https://grupoact.com.ar/wp-content/uploads/2020/04/placeholder.png" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="mb-3" style="align-content: center; text-align: center; margin: 20px">
            <asp:Button Text="Aceptar" ID="BtnAceptar" CssClass="btn btn-success" OnClick="BtnAceptar_Click" runat="server" />
            <a href="Default.aspx" class="btn btn-danger">Cancelar</a>
            <asp:Button Text="Inactivar" ID="BtnInactivar" CssClass="btn btn-warning" OnClick="BtnInactivar_Click" runat="server" />
            <asp:Button Text="Reactivar" ID="btnReactivar" CssClass="btn btn-primary" OnClick="btnReactivar_Click" runat="server" />
        </div>

        <% if (ConfirmarInactivar)
            { %>
        <div class="mb-3" style="align-content: center; text-align: center; margin: 20px">
            <asp:CheckBox Text="Confirmar Inactivación" ID="chkConfirmaInactivacion" runat="server" />
            <asp:Button Text="Inactivar" ID="btnConfirmaInactivar" OnClick="btnConfirmaInactivar_Click" CssClass="btn btn-outline-danger" runat="server" />
        </div>
        <% } %>
        <% if (ConfirmarReactivar)
            { %>
        <div class="mb-3" style="align-content: center; text-align: center; margin: 20px">
            <asp:CheckBox Text="Confirmar Reactivación" ID="chkConfirmaReactivacion" runat="server" />
            <asp:Button Text="Reactivar" ID="btnConfirmaReactivar" OnClick="btnConfirmaReactivar_Click" CssClass="btn btn-outline-primary" runat="server" />
        </div>
        <% } %>
    </div>
</asp:Content>
