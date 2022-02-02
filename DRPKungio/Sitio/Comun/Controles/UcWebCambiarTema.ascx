<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcWebCambiarTema.ascx.cs" Inherits="Sitio.Comun.Controles.UcWebCambiarTema" %>
<%-- <div id="VentanaCambiarTema"  runat="server"  class="ventanaFlotante"  style="  z-index:10;  top:450px ; left:60px ; width:230px ; height:120px; display:block;">
       <div class="fondoTituloVentana">    
            <h2> Cambiar Tema </h2>
       </div>
    <br />--%>
<%--    <span class="requerido">*</span>--%>
    <asp:Label ID="lbTema" runat="server" Text="Tema " CssClass="Etiqueta ColorTema" Width="100px"></asp:Label>
    <asp:DropDownList ID="lisTema" runat="server" Width="170px"  AutoPostBack="true"

        onselectedindexchanged="lisTema_SelectedIndexChanged" 
                onmouseout="OcultarTextoAyuda(' Seleccione  Tema');" 
                onmouseover="MostrarTextoAyuda(' Seleccione  Tema');"  >
    </asp:DropDownList>
    <asp:LinkButton ID="btnGuardar" runat="server"  CssClass ="Boton BordeAbajo ColorTema"  Text="Guardar"  ToolTip="Guardar"  onclick="btnGuardar_Click" />
<%--</div>--%>