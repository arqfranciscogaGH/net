<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcWebMensaje.ascx.cs" Inherits="Sitio.Comun.Controles.UcWebMensaje" %>

<%--<link href="../../App_Themes/Base/Base.css" rel="stylesheet" type="text/css" />--%>

<script language="javascript" type="text/javascript">
    function CerrarMensaje(Instancia) {
        document.getElementById(Instancia).style.display = 'none';
    }
</script>

<div id="divFondoMensaje" class="fondoMensaje" runat="server" clientidmode="Static">
	<div id="divContenedorMensaje" class="contenedorMensaje">
		<div id="divTituloMensaje" class="tituloMensaje" runat="server">
			Título‌ del‌ mensaje</div>
		<div id="divContenidoMensaje" class="contenidoMensaje">
			<div id="divImagenMensaje" class="imagenMensaje">
				<div id="divIconoMensajeAviso" class="iconoMensajeAviso" runat="server" style="display: block;">
				</div>
			</div>
			<div id="divTextoMensaje" class="textoMensaje" runat="server">
				Contenido‌ del‌ mensaje
			</div>
            <br/>
			<div id="divDetalleTextoMensaje" class="textoMensaje" runat="server" visible="false">
				  
			</div>
  			<div id="divPanelBotones" class="contenedorBotonesMensaje">
              	<asp:LinkButton ID="ahrefBoton1" runat="server" Text=""   CssClass="IconoFuente colorTema  Sombra Ch" visible="true"  /> 
				<asp:LinkButton ID="ahrefBoton2" runat="server" Text=""   CssClass="IconoFuente colorTema  Sombra Ch" visible="true"  /> 
				<asp:LinkButton ID="ahrefBoton3" runat="server" Text=""   CssClass="IconoFuente colorTema  Sombra Ch" visible="true"  />  
<%--				<a id="ahrefBoton1" runat="server" style="text-decoration: none;" class="botonApagado" visible="true" >Boton 1</a> 
				<a id="ahrefBoton3" runat="server" class="botonApagado" visible="true">Boton 3</a>--%>
			</div>
		</div>
	</div>
</div>
