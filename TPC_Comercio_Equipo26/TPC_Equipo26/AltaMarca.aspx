<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AltaMarca.aspx.cs" Inherits="TPC_Equipo26.AltaMarca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="my-4">
        <asp:Label ID="lblTituloAgregar" runat="server" Text="AGREGAR MARCA NUEVA" Visible="false" CssClass="titulo-label"></asp:Label>
        <asp:Label ID="lblTituloModificar" runat="server" Text="MODIFICAR MARCA" Visible="false" CssClass="titulo-label"></asp:Label>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="col">
        <div class="mb-3">
            <label for="txtDescripcion" class="form-label">Nombre: </label>
            <asp:TextBox runat="server" ID="txtDescripcion" CssClass="form-control" />
             <asp:Label ID="lblDescripcion" runat="server" Text="*" ForeColor="Red" CssClass="required-field" Visible="false"></asp:Label>
        </div>
    </div>

    <div class="col">
        <div class="mb-3">
            <label for="ddlProveedor" class="form-label">Proveedor</label>
            <asp:DropDownList ID="ddlProveedor" CssClass="form-select" runat="server" AutoPostBack="true" AppendDataBoundItems="true">
                <asp:ListItem Text="Seleccionar..." Value="-1" />
            </asp:DropDownList>
            <asp:Label ID="lblProveedor" runat="server" Text="*" ForeColor="Red" CssClass="required-field" Visible="false"></asp:Label>
        </div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="mb-3">
                    <label for="txtImagenUrl" class="form-label">Url Imagen:</label>
                    <asp:TextBox runat="server" ID="txtImagenUrl" CssClass="form-control"
                        AutoPostBack="true" OnTextChanged="TxtImagenUrl_TextChanged" />
                    <asp:Label ID="lblImagenUrl" runat="server" Text="*" ForeColor="Red" CssClass="required-field" Visible="false"></asp:Label>
                </div>
                <asp:Image runat="server" ID="imgMarcas" Width="20%" Style="max-width: 100%; height: auto;"
                    ImageUrl="https://grupoact.com.ar/wp-content/uploads/2020/04/placeholder.png" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="mb-3" style="align-content: center; text-align: center; margin: 20px">
        <asp:Button Text="Aceptar" ID="BtnAceptar" CssClass="btn btn-success" OnClick="BtnAceptar_Click" runat="server" />
        <a href="Marcas.aspx" class="btn btn-danger">Cancelar</a>

        <asp:Button Text="Inactivar" ID="BtnInactivar" CssClass="btn btn-warning" OnClick="BtnInactivar_Click" runat="server" />
        <asp:Button Text="Reactivar" ID="BtnReactivar" CssClass="btn btn-primary" OnClick="BtnReactivar_Click" runat="server" />
    </div>
    <asp:Label ID="lblError" runat="server" Text="" CssClass="text-danger" Visible="false"></asp:Label>
    <% if (ConfirmarInactivar)
        { %>
    <div class="mb-3" style="align-content: center; text-align: center; margin: 20px">
        <asp:CheckBox Text="Confirmar Inactivación" ID="chkConfirmaInactivacion" runat="server" />
        <asp:Button Text="Inactivar" ID="BtnConfirmaInactivar" OnClick="BtnConfirmaInactivar_Click" CssClass="btn btn-outline-danger" runat="server" />
    </div>
    <% } %>
    <% if (ConfirmarReactivar)
        { %>
    <div class="mb-3" style="align-content: center; text-align: center; margin: 20px">
        <asp:CheckBox Text="Confirmar Reactivación" ID="chkConfirmaReactivacion" runat="server" />
        <asp:Button Text="Reactivar" ID="BtnConfirmaReactivar" OnClick="BtnConfirmaReactivar_Click" CssClass="btn btn-outline-primary" runat="server" />
    </div>
    <% } %>
</asp:Content>
