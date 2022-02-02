<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucWebCambiarIdioma.ascx.cs" Inherits="Sitio.Comun.Controles.ucWebCambiarIdioma" %>
<%--<div id="VentanaCambiarIdioma"  class="ventanaFlotante"  style="  z-index:10;  top:250px ; left:60px ; width:230px ; height:120px; display:block;">
       <div class="fondoTituloVentana">    
            <h2> Cambiar Idioma </h2>
       </div>
    <br />--%>
<%--    <span class="requerido">*</span>--%>
    <asp:Label ID="lblIdiona" runat="server" Text="Idioma " CssClass="Etiqueta ColorTema" Width="100px"></asp:Label>
    <asp:DropDownList ID="lisIdioma" runat="server" Width="100px"  AutoPostBack="true"
    onmouseout="OcultarTextoAyuda(' Seleccione  Idioma');" 
        onmouseover="MostrarTextoAyuda(' Seleccione  Idioma');" 
        onselectedindexchanged="lisIdioma_SelectedIndexChanged" 
                          
    >
    </asp:DropDownList>

    <asp:LinkButton ID="btnGuardar" runat="server" CssClass ="Boton  BordeAbajo ColorTema"  Text="Guardar"  ToolTip="Guardar"   onclick="btnGuardar_Click" />
<%--</div>--%>
