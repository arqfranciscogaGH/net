<%@ Page Language="C#"  AutoEventWireup="true" EnableEventValidation = "false"  CodeBehind="AdministracionMenu.aspx.cs" Inherits="Sitio.Seguridad.AdministracionMenu" %>

<%@ Register src="../Comun/Controles/ucWebBarraProgreso.ascx" tagname="ucWebBarraProgreso" tagprefix="uc1" %>
<%@ Register src="../Comun/Controles/UcWebMenuFuncionalidad.ascx" tagname="UcWebMenuFuncionalidad" tagprefix="uc2" %>
<%@ Register src="../Comun/Controles/UcWebBanner.ascx" tagname="UcWebBanner" tagprefix="uc3" %>
<%@ Register src="../Comun/Controles/UcWebEncabezadoPagina.ascx" tagname="UcWebEncabezadoPagina" tagprefix="uc8" %>

<%@ Register src="../Comun/Controles/ucWebConsultorDinamico.ascx" tagname="ucWebConsultorDinamico" tagprefix="uc4" %>
<%@ Register src="../Comun/Controles/ucWebConsultorDinamico.ascx" tagname="ucWebConsultorDinamico" tagprefix="uc6" %>
<%@ Register src="../Comun/Controles/UcWebMensaje.ascx" tagname="UcWebMensaje" tagprefix="uc5" %>
<%@ Register src="../Comun/Controles/UcWebPiePagina.ascx" tagname="UcWebPiePagina" tagprefix="uc10" %>



<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <title>Administración de Menú</title>
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


    <link href="../Comun/LibreriasIconos/fontello/css/fontello.css" rel="stylesheet" />
    <link href="../Comun/LibreriasIconos/fontello/css/animation.css" rel="stylesheet" />

    <script src="../Comun/Scripts/jquery-1.8.0.min.js"></script>
    <script src="../Comun/Scripts/ArqSigNet.Nucleo.AdministradorAplicaciones.js" type="text/javascript"></script>
    <script src="../Comun/Scripts/AdministradorElementosCompuestos.js" type="text/javascript"></script>
    <script src="../Comun/Scripts/Menu.js"></script>

</head>
<body>
    <form id="frmAdministracionMenu" runat="server">
      <div class="Pagina_Seccion">    
            <uc1:ucWebBarraProgreso ID="ucWebBarraProgreso1" runat="server" />
            <div id="textoAyuda"></div> 
            <header  class="Encabezado_Seccion">
                 <div class= "Menu_Seccion" >            
                    <uc8:UcWebEncabezadoPagina ID="UcWebEncabezadoPagina1" runat="server" />
                    <uc2:UcWebMenuFuncionalidad ID="UcWebMenuFuncionalidad2" runat="server" Idmenu="14" />
                </div>
            </header>  
           <section class="Banner_Seccion">
                  <uc3:UcWebBanner ID="UcWebBanner1" runat="server"  Activo="true"  />
            </section>
            <section class="Consultor_Seccion" runat="server">
                  <uc4:ucwebconsultordinamico ID="ucWebConsultorDinamico1"  runat="server"  max-Width="100px" max-Heigth="100px"   Wrap="True"  ActivarFiltro="true" ActivarSeleccion="true" ActivarResumir="false"  ActivarOrden="true"  TituloSeleccion=""  NumeroRegistrosPagina="5" GridLines="Both"  />    
            </section>
            <section class="Captura_Seccion" runat="server">
                  <div ID="SelccionOpciones" class= "Marco"  runat="server"> 
                          <asp:Label  ID="lblModulo" runat="server"> Módulo</asp:Label>
                          <asp:DropDownList ID="DropDownListAplicacion" runat="server"></asp:DropDownList>
                 </div> 
                 <div ID="BloqueCaptura"  runat="server"> 
                        
                 </div> 
            </section>
            <section class="Consultor_Seccion" runat="server">
                   <uc6:ucwebconsultordinamico ID="ucWebConsultorDinamico2"  runat="server"  max-Width="100px" max-Heigth="100px"   Wrap="True"  ActivarFiltro="true" ActivarSeleccion="true" ActivarResumir="false"  ActivarOrden="true"  TituloSeleccion=""  NumeroRegistrosPagina="5" GridLines="None"  CellSpacing="1" />    
            </section>
            <section class="Captura_Seccion" runat="server">

                <div ID="BloqueCaptura2"  runat="server"> 

                </div> 
                <uc5:UcWebMensaje ID="UcWebMensaje1" runat="server" Visible="false" TituloMensaje ="Operación"    ComportamientoEventos="Servidor"   />
            </section>
            <footer class="PiePagina_Seccion">
                <uc10:ucwebpiepagina ID="UcWebPiePagina1" runat="server" />
            </footer>
        </div>
    </form>
</body>
</html>

