<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false"  CodeBehind="AdminDocs.aspx.cs" Inherits="Sitio.AdminDocs" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="es" >

<head runat="server">

    <title>Administración de documentos KunGio</title>
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
    <!--
    Google Fonts
    ============================================= -->
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700" rel="stylesheet" type="text/css">

    <!--
    CSS
    ============================================= -->
<%--    <!-- Fontawesome -->
    <link href="css/font-awesome.min.css" rel="stylesheet" type="text/css">--%>
    <link href="Comun/plugins/fontawesome-free-5.0.1/css/fontawesome-all.css" rel="stylesheet" type="text/css">
    <!-- Bootstrap -->
    <link rel="stylesheet" href="Comun/css/bootstrap.min.css">
    <!-- Fancybox -->
    <link rel="stylesheet" href="Comun/css/jquery.fancybox.css">
    <!-- owl carousel -->
    <link rel="stylesheet" href="Comun/css/owl.carousel.css">
    <!-- Animate -->
    <link rel="stylesheet" href="Comun/css/animate.css">
    <!-- Main Stylesheet -->
    <link rel="stylesheet" href="Comun/css/main.css">
    <!-- Main Responsive -->
    <link rel="stylesheet" href="css/responsive.css">
    <link href="App_Themes/Base/maqueta.css" rel="stylesheet" />
    <!-- Modernizer Script for old Browsers -->
    <style>
        .cuerpo {
            background-color: black;
        }
        bodys {
             background-color: black;
        }
    </style>
</head>


<body class="cuerpo">
    <form id="form1" runat="server">
       <section id="AdministracionDocumentos">
             <h3> Administración de documentos </h3>
             <br />
             <br />
             <asp:Label id="lblDocumento" Runat="server" Text =""  CssClass="Etiqueta ColorTema" ></asp:Label>
             <br />
             <br />
             <h3> Operaciones </h3>
             <br />
             <br />
             <asp:Panel id="Panel2" Visible="true" Runat="server">
                 <label class="cargaArchivo">
                    <span><strong>Seleccionar</strong></span>
                    <asp:FileUpload id="cargaArchivo" runat="server" >
                    </asp:FileUpload>
                </label>
                <br />
                <br />
                <asp:Button id="btnCargar" runat="server" Text="   Cargar   "  CssClass="boton"  onclick="btnUpload_Click"  />	
                <br />
                <br />
                <asp:Button id="btnDes" runat="server" Text="Descargar"  CssClass="boton"  OnClick="btnDes_Click"  />	
                <br />
                <br />
            </asp:Panel>
            <br />
            <br />
            <asp:Panel id="frmConfirmation" Visible="False" Runat="server">
                 <asp:Label id="lblMensaje" Runat="server"></asp:Label>
            </asp:Panel>
        </section >
        <footer id="footer" class="text-center">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="footer-logo wow fadeInDown">
                            <img src="img/logo2.png" alt="logo">
                        </div>
                        <div class="footer-social wow fadeInUp">
                            <h3>Nuestras redes sociales</h3>
                            <ul class="text-center list-inline">
                                <li><a href="https://www.facebook.com/KunGio.mx"><i class="fab fa-facebook-f fa-lg "></i></a></li>
                                <li><a href="https://www.facebook.com/KunGio.mx"><i class="fab fa-twitter fa-lg"></i></a></li>
                                <li><a href="https://www.facebook.com/KunGio.mx"><i class="fab fa-youtube fa-lg"></i></a></li>
                                <li><a href="https://www.facebook.com/KunGio.mx"><i class="fab fa-instagram fa-lg"></i></a></li>

                            </ul>
                        </div>
                        <div class="copyright">
                            <p>Derechos Reservados KunGio ,  <a href="http://KunGio.mx"></a> Diseñado por Francisco Garcia <a target="_blank" href="http://KunGio.mx"></a></p>
                        </div>
                    </div>
                </div>
            </div>
        </footer>
    </form>
</body>
</html>
