﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdministracionPlaneacionCaptura.aspx.cs" Inherits="Sitio.AdministracionControles.AdministracionPlaneacionCaptura" %>

<%@ Register src="../Comun/Controles/ucWebBarraProgreso.ascx" tagname="ucWebBarraProgreso" tagprefix="uc1" %>
<%@ Register src="../Comun/Controles/UcWebMenuFuncionalidad.ascx" tagname="UcWebMenuFuncionalidad" tagprefix="uc2" %>
<%@ Register src="../Comun/Controles/UcWebBanner.ascx" tagname="UcWebBanner" tagprefix="uc3" %>


<%@ Register src="../Comun/Controles/ucWebConsultorDinamico.ascx" tagname="ucWebConsultorDinamico" tagprefix="uc4" %>
<%@ Register src="../Comun/Controles/ucWebConsultorDinamico.ascx" tagname="ucWebConsultorDinamico" tagprefix="uc6" %>
<%@ Register src="../Comun/Controles/UcWebMensaje.ascx" tagname="UcWebMensaje" tagprefix="uc5" %>
<%@ Register src="../Comun/Controles/UcWebPiePagina.ascx" tagname="UcWebPiePagina" tagprefix="uc10" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <meta charset="utf-8"/>
    <meta http-equiv="X-UA-compatible" content="IE=edge" />
    <meta name="keywords" content="captura, dinamica, formulario,campo, responsiva " />
    <meta name="description" content="es una captura de forulario dinámico responsiva" />
    <meta name="viewport"  content="width=device-width, user-scalable=no, initial-scale=1, maximum-scale=1"/>

    <title>Planeación  de  captura</title>
    <link rel="icon" href="../Comun/Iconos/Aplicacion.ico" />

    <link href="../Comun/LibreriasIconos/fontello/css/fontello.css" rel="stylesheet" />
    <link href="../Comun/LibreriasIconos/fontello/css/animation.css" rel="stylesheet" />

    <script type="text/javascript" src="../Comun/Scripts/jquery-1.8.0.min.js"></script>
    <script src="../Comun/Scripts/AdministradorElementosCompuestos.js" type="text/javascript"></script>
<%--    <script src="../Comun/Scripts/ArqSigNet.Nucleo.AdministradorAplicaciones.js" type="text/javascript"></script>--%>
    <script src="../Comun/Scripts/Menu.js"></script>

</head>
<body>
    <form id="frmPlaneaciónCaptura" runat="server">
      <div class="Pagina_Seccion">    
            <uc1:ucWebBarraProgreso ID="ucWebBarraProgreso1" runat="server" />
            <div id="textoAyuda"></div> 
            <header  class="Encabezado_Seccion">
                 <div class= "Menu_Seccion" >            
                    <span class= "MenuLogo  icon-windows " > </span>
                    <span class= "MenuTitulo " > Titulo </span>
                    <span id="IdBotonMenu"  class="MenuBotonActivacion icon-window-minimize">  </span>
                    <uc2:UcWebMenuFuncionalidad ID="UcWebMenuFuncionalidad2" runat="server" Idmenu="14" />
                                            
                </div>
            </header>  
           <section class="Banner_Seccion">
                  <uc3:UcWebBanner ID="UcWebBanner1" runat="server"  Activo="true"  />
            </section>
            <section class="Consultor_Seccion " runat="server">
                  <uc4:ucwebconsultordinamico ID="ucWebConsultorDinamico1"  runat="server"  max-Width="70px" max-Heigth="100px"   Wrap="True"  ActivarFiltro="true" ActivarSeleccion="true" ActivarResumir="false"  ActivarOrden="true"  TituloSeleccion=""  NumeroRegistrosPagina="5" GridLines="Both"  TituloVentana="Cuestionarios"  />    
                  
            </section>
            <section class="Captura_Seccion " runat="server">
                 <div ID="BloqueCaptura"  runat="server"> 
                 </div> 
            </section>
            <section class="Consultor_Seccion  " runat="server">
                  <uc6:ucwebconsultordinamico ID="ucWebConsultorDinamico2"  runat="server"  max-Width="70px" max-Heigth="100px"   Wrap="True"  ActivarFiltro="true" ActivarSeleccion="true" ActivarResumir="false"  ActivarOrden="true"  TituloSeleccion=""  NumeroRegistrosPagina="5" GridLines="None"  TituloVentana="Variables"   CellSpacing="1"  />            
            </section>
            <section class="Captura_Seccion " runat="server">
                 <uc5:UcWebMensaje ID="UcWebMensaje1" runat="server" Visible="false" TituloMensaje ="Operación"    ComportamientoEventos="Servidor"   />
                 <div ID="BloqueCaptura2"  runat="server"> 
                 </div> 
            </section>

            <footer class="PiePagina_Seccion">
                <uc10:ucwebpiepagina ID="UcWebPiePagina1" runat="server" />
            </footer>
        </div>
    </form>
</body>
</html>
