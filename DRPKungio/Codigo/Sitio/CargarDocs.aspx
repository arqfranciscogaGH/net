<%@ Page Language="C#" AutoEventWireup="false" EnableEventValidation="false" CodeBehind="CargarDocs.aspx.cs" Inherits="Sitio.CargarDocs" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="es" class="no-js">
<head runat="server">

    <title>Cargar Documentos Kungio</title>
    <meta charset="utf-8"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="description" content="Servicios Credito infonavit linea IV en Kungio">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="msapplication-tap-highlight" content="no" />
    <meta name="robots" content="index,follow,all" />
    <meta name="keywords" content="Servicios Credito infonavit linea IV Pension Afore  Prcalificate" />
    <meta name="author" content="Francisco Garcia | STI" />

    <link rel="shortcut icon" href="Comun/favicon/Aplicacion.ico" type="image/x-icon">
    <link rel="icon" href="Comun/favicon/Aplicacion.ico" type="image/x-icon">
                             
    <meta property="og:title" content="KunGio" />
    <meta property="og:type" content="video-movie" />
    <meta property="og:url" content="http://kugio-mx/inicio.aspx"" />
    <meta property="og:image" content="Comun/favicon/Aplicacion.ico"" />

    <link href="Comun/plugins/fontawesome-free-5.0.1/css/fontawesome-all.css" rel="stylesheet" type="text/css">
     <link rel="stylesheet" href="Comun/css/animate.css">
    <!-- Main Stylesheet -->
    <link rel="stylesheet" href="Comun/css/main.css">
    <!-- Main Responsive -->
    <link rel="stylesheet" href="Comun/css/responsive.css">
</head>

   <body>
    <form id="form1" runat="server" >
         <asp:Label id="lblDocumento" Runat="server" Text =""></asp:Label>
         <asp:Panel id="Panel2" Visible="true" Runat="server">

            <asp:FileUpload id="CargaArchivo" runat="server" CssClass="cajaTexto"  Width="260px" />
            <br />
            <asp:Button id="btnCargar" runat="server" Text="Cargar"  CssClass = "botonApagado" onclick="btnUpload_Click"  />	
            <br />
            <asp:Button id="btnDes" runat="server" Text="Descargar"  CssClass = "botonApagado" OnClick="btnDes_Click"  />	
            <br />
        </asp:Panel>
        <asp:Panel id="frmConfirmation" Visible="False" Runat="server">
             <asp:Label id="lblUploadResult" Runat="server"></asp:Label>
        </asp:Panel>
    </form>
</body>
</html>

  