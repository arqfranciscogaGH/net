<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Prueba.aspx.cs" Inherits="Sitio.Prueba" %>


<%@ Register src="Comun/Controles/UcWebBanner.ascx" tagname="UcWebBanner" tagprefix="uc1" %>
<%@ Register src="Comun/Controles/ucWebBarraProgreso.ascx" tagname="ucWebBarraProgreso" tagprefix="uc2" %>
<%@ Register src="Comun/Controles/UcWebPiePagina.ascx" tagname="UcWebPiePagina" tagprefix="uc3" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <title>Prueba</title>
    <meta charset="utf-8"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">

    <meta name="keywords" content="Servicios Credito infonavit linea IV Pension Afore  Prcalificate" />
    <meta name="description" content="Servicios Credito infonavit linea IV en Kungio">
    <meta name="viewport"  content="width=device-width, user-scalable=no, initial-scale=1, maximum-scale=1"/>

    <meta name="msapplication-tap-highlight" content="no" />
    <meta name="robots" content="index,follow,all" />
    <meta name="author" content="Francisco Garcia | STI" />

    <link rel="shortcut icon" href="Comun/favicon/Aplicacion.ico" type="image/x-icon">
    <link rel="icon" href="Comun/favicon/Aplicacion.ico" type="image/x-icon">

    <meta property="og:title" content="KunGio" />
    <meta property="og:type" content="video-movie" />
    <meta property="og:url" content="http://kugio-mx/inicio.aspx"" />
    <meta property="og:image" content="Comun/favicon/Aplicacion.ico"" />

<%--    <link rel="apple-touch-icon" sizes="57x57" href="/apple-icon-57x57.png">
    <link rel="apple-touch-icon" sizes="60x60" href="/apple-icon-60x60.png">
    <link rel="apple-touch-icon" sizes="72x72" href="/apple-icon-72x72.png">
    <link rel="apple-touch-icon" sizes="76x76" href="/apple-icon-76x76.png">
    <link rel="apple-touch-icon" sizes="114x114" href="/apple-icon-114x114.png">
    <link rel="apple-touch-icon" sizes="120x120" href="/apple-icon-120x120.png">
    <link rel="apple-touch-icon" sizes="144x144" href="/apple-icon-144x144.png">
    <link rel="apple-touch-icon" sizes="152x152" href="/apple-icon-152x152.png">
    <link rel="apple-touch-icon" sizes="180x180" href="/apple-icon-180x180.png">
    <link rel="icon" type="image/png" sizes="192x192"  href="/android-icon-192x192.png">
    <link rel="icon" type="image/png" sizes="32x32" href="/favicon-32x32.png">
    <link rel="icon" type="image/png" sizes="96x96" href="/favicon-96x96.png">
    <link rel="icon" type="image/png" sizes="16x16" href="/favicon-16x16.png">--%>

    <link href="Comun/LibreriasIconos/fontello/css/fontello.css" rel="stylesheet" />
    <link href="Comun/LibreriasIconos/fontello/css/animation.css" rel="stylesheet" />

    <script type="text/javascript" src="Comun/Scripts/jquery-1.8.0.min.js"></script>
    <script src="Comun/Scripts/AdministradorElementosCompuestos.js" type="text/javascript"></script>
    <script src="Comun/Scripts/ArqSigNet.Nucleo.AdministradorAplicaciones.js" type="text/javascript"></script>
    <script src="Comun/Scripts/Menu.js"></script>
</head>
<body>
    <form id="form1" runat="server">

<%--     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"   EnablePageMethods="true">
        </asp:ToolkitScriptManager>
        <asp:UpdatePanel ID="SeccionCaptura" runat="server" >
            <ContentTemplate> --%>
      <div class="Pagina_Seccion">    
            <uc2:ucWebBarraProgreso ID="ucWebBarraProgreso1" runat="server" />
            <div id="textoAyuda"></div> 
            <header  class="Encabezado_Seccion">
                 <div class= "Menu_Seccion" >            
                    <span class= "MenuLogo  icon-windows " > </span>
                    <span class= "MenuTitulo " > Titulo </span>
                    <span id="IdBotonMenu"  class="MenuBotonActivacion icon-window-minimize">  </span>
<%--                    <uc2:UcWebMenuFuncionalidad ID="UcWebMenuFuncionalidad2" runat="server" Idmenu="1" />--%>
          
                </div>
            </header>  
           <section class="Banner_Seccion">
                  <uc1:UcWebBanner ID="UcWebBanner1" runat="server"  Activo="true"  />
            </section>


            <footer class="PiePagina_Seccion">
                <uc3:ucwebpiepagina ID="UcWebPiePagina1" runat="server" />
            </footer>
        </div>
    </form>
</body>
</html>
