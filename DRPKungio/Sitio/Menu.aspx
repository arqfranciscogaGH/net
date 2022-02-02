<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="Sitio.Menu" %>


<%@ Register src="Comun/Controles/ucWebBarraProgreso.ascx" tagname="ucWebBarraProgreso" tagprefix="uc1" %>
<%@ Register src="Comun/Controles/UcWebMenuFuncionalidad.ascx" tagname="UcWebMenuFuncionalidad" tagprefix="uc2" %>
<%@ Register src="Comun/Controles/UcWebBanner.ascx" tagname="UcWebBanner" tagprefix="uc3" %>

<%@ Register src="Comun/Controles/UcWebMensaje.ascx" tagname="UcWebMensaje" tagprefix="uc5" %>

<%@ Register src="Comun/Controles/UcWebPiePagina.ascx" tagname="UcWebPiePagina" tagprefix="uc10" %>
<%@ Register src="Comun/Controles/UcWebEncabezadoPagina.ascx" tagname="UcWebEncabezadoPagina" tagprefix="uc8" %>




<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="es">

<head runat="server">
    <meta charset="utf-8"/>
    <meta http-equiv="X-UA-compatible" content="IE=edge" />
    <meta name="keywords" content="captura, dinamica, formulario,campo, responsiva " />
    <meta name="description" content="es una captura de forulario dinámico responsiva" />
    <meta name="viewport"  content="width=device-width, user-scalable=no, initial-scale=1, maximum-scale=1"/>

    <title>Ménu principal</title>
    <link rel="icon" href="Comun/Iconos/Aplicacion.ico" />

    <link href="Comun/LibreriasIconos/fontello/css/fontello.css" rel="stylesheet" />
    <link href="Comun/LibreriasIconos/fontello/css/animation.css" rel="stylesheet" />

    <script type="text/javascript" src="Comun/Scripts/jquery-1.8.0.min.js"></script>
    <script src="Comun/Scripts/AdministradorElementosCompuestos.js" type="text/javascript"></script>
    <script src="Comun/Scripts/ArqSigNet.Nucleo.AdministradorAplicaciones.js" type="text/javascript"></script>
    <script src="Comun/Scripts/Menu.js"></script>

</head>
<body>
    <form id="frmMenuPrincipal" runat="server">
<%--     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"   EnablePageMethods="true">
        </asp:ToolkitScriptManager>
        <asp:UpdatePanel ID="SeccionCaptura" runat="server" >
            <ContentTemplate> --%>
      <div class="Pagina_Seccion">    
            <uc1:ucWebBarraProgreso ID="ucWebBarraProgreso1" runat="server" />
            <div id="textoAyuda"></div> 
            <header  class="Encabezado_Seccion">
                 <div class= "Menu_Seccion" >            
                     <uc8:UcWebEncabezadoPagina ID="UcWebEncabezadoPagina1" runat="server" />
                     <uc2:UcWebMenuFuncionalidad ID="UcWebMenuFuncionalidad2" runat="server" Idmenu="11" />
                </div>
            </header>  
           <section class="Banner_Seccion">
                  <uc3:UcWebBanner ID="UcWebBanner1" runat="server"  Activo="true"  />
            </section>

            <section class="Contenido_Seccion">
                <h1>
                    Seccion_Contenido
                </h1>
    
                <article>
                    <p>
                        Esto  es una  prueba de contenido , Esto  es una  prueba de anuncios ,Esto  es una  prueba de anuncios ,  Esto  es una  prueba de anuncios 
                    </p>
 
       
                </article>
<%--               <uc5:UcWebMensaje ID="UcWebMensaje1" runat="server"   Visible="false" TituloMensaje ="Operación"    ComportamientoEventos="Servidor" />--%>
            </section>

            <aside class="Publicidad_Seccion">
                <h1>
                    Seccion_Publicidad
                </h1>
                <p>
                    Esto  es una  prueba de anuncios , Esto  es una  prueba de anuncios ,Esto  es una  prueba de anuncios ,  Esto  es una  prueba de anuncios 
                </p>
 
            </aside>

            <footer class="PiePagina_Seccion">
                <uc10:ucwebpiepagina ID="UcWebPiePagina1" runat="server" />

            </footer>
        </div>

    </form>
</body>
</html>
