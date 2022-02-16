<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucWebBarraProgreso.ascx.cs" Inherits="Sitio.Comun.Controles.ucWebBarraProgreso" %>
    <div id="ContenedorBarraProceso" runat="server" ClientIDMode = "Static" ClientID = "ContenedorBarraProceso" >
        <div class="BarraProgreso" runat="server">
            <%--                    <div class="ImagenProgresoAlterna" runat="server">
            </div>--%>
            <span  id="divImagenProgreso" class="ImagenProgreso icon-spin4 animate-spin" runat="server" > </span>
            <div   id="divMensajeBarraProceso" class="MensajeBarraProceso" runat="server">
                Procesando, espero unmomento por favor...
            </div>  
        </div>
    </div>  

  