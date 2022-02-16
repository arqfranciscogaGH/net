<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListaPersonalizada.ascx.cs" Inherits="Sitio.Comun.Controles.ListaPersonalizada" %>
  <div id="ContenedorLista" class="MarcoListaPersonalizada" runat="server">
        <asp:TextBox id="txtBusqueda"  CssClass="CajaTextoPersonalizada ColorTema BordeIzq Col0" runat="server"  AutoPostBack="true" ontextchanged="txtBusqueda_TextChanged"></asp:TextBox>
        <div id="ContenedorListaElementos" class="MarcoElementosListaPersonalizada"  runat="server"  >
        </div>
  </div>