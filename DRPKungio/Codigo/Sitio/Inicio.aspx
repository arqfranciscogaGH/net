<%@ Page Language="C#" AutoEventWireup="true"  CodeBehind="inicio.aspx.cs" Inherits="Sitio.inicio" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="es" class="no-js">
<head runat="server">




    <title>Kungio</title>
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

    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700" rel="stylesheet" type="text/css">


<%--    <!-- Fontawesome -->
    <link href="css/font-awesome.min.css" rel="stylesheet" type="text/css">--%>
    <link href="Comun/plugins/fontawesome-free-5.0.1/css/fontawesome-all.css" rel="stylesheet" type="text/css">
    <!-- Bootstrap -->
    <link rel="stylesheet" href="Comun/css/bootstrap.min.css">
    <!-- Fancybox -->
    <link rel="stylesheet" href="Comun/css/jquery.fancybox.css">
    <!-- owl carousel -->
<%--    <link rel="stylesheet" href="Comun/css/owl.carousel.css">--%>
    <!-- Animate -->
    <link rel="stylesheet" href="Comun/css/animate.css">
    <!-- Main Stylesheet -->
    <link rel="stylesheet" href="Comun/css/main.css">
    <!-- Main Responsive -->
    <link rel="stylesheet" href="Comun/css/responsive.css">

    <!-- owl carousel -->

    <link rel="stylesheet" href="comun/owlCarousel/css/owl.carousel.min.css">
    <link rel="stylesheet" href="comun/owlCarousel/css/owl.theme.default.min.css">

      <!-- app -->
    <link rel="stylesheet" href="comun/app/css/app.css">

</head>


<body>
   <form id="form1" runat="server">
    <!--
    Fixed Navigation
    ==================================== -->
    <header id="navigation" class="navbar-fixed-top">
        <div class="container">
            <div class=navbar-header"">
                <!-- responsive nav button -->
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <!-- /responsive nav button -->
                <!-- logo -->
                <h1 class="navbar-brand">
                    <a href="#body">
                        <img src="img/logo/logoKungio13.png" alt="kungio Logo">
                    </a>
                </h1>
                <!-- /logo -->
            </div>
            <!-- main nav -->
            <nav class="collapse navigation navbar-collapse navbar-right" role="navigation">
                <ul id="nav" class="nav navbar-nav">
                    <li class="current"><a href="#home">Inicio</a></li>
                    <li><a href="#about">Nosotros</a></li>
                    <li><a href="#service">Servicios</a></li>
                    <li><a href="#portfolio">Galeria</a></li>
                    <!--<li><a href="#pricing">Precios</a></li>-->
                    <li><a href="#contact">Contacto</a></li>
                    <li><a href="#precalificate">Precalificar</a></li>
                   <li><a href="http://kungio.com/acceso">Ingresar</a></li>

                </ul>
            </nav>
            <!-- /main nav -->
        </div>
        </div>
    </header>
    <!--
    End Fixed Navigation
    ==================================== -->
    <!--
    Home Slider
    ==================================== -->
    <section id="home">
 <%--       <div id="home-carousel" class="carousel slide" data-interval="false" >--%>
       
          <div  class="owl-container"  >

          <div  id="ContenedorCarruselInicio" class="owl-carousel" Runat="server" > 
<%--            <ol class="carousel-indicators">
                    <li data-target="#ContenedorCarruselInicio" data-slide-to="0" class="active"></li>
                    <li data-target="#ContenedorCarruselInicio" data-slide-to="1"></li>
                    <li data-target="#ContenedorCarruselInicio" data-slide-to="2"></li>
                </ol>--%>
                <!--/.carousel-indicators-->
 <%--               <div class="item active" style="background-image: url('Carrusel/s1.jpg')">
                    <div class="carrusel-titulo">
                        <div class="animated bounceInRight">
                            <h2>BIENVENIDO !! <br>NOSOTROS  TE  ASESOREAMOS.</h2>
                            <p>Contamos con grupo de expertos que están listos para asesorarte. </p>
                        </div>
                    </div>
                </div>--%>
<%--            <div class="item" style="background-image: url('Carrusel/s2.jpg')">
                    <div class="carrusel-titulo">
                        <div class="animated bounceInDown">
                            <h2>BIENVENIDO !! <br>NOSOTROS  TE  ASESOREAMOS.</h2>
                            <p>Estás a un clic de poder iniciar la remodelación de tu casa. </p>
                        </div>
                    </div>
                </div>
                <div class="item" style="background-image: url('Carrusel/s3.jpg')">
                    <div class="carrusel-titulo">
                        <div class="animated bounceInUp">
                            <h2>BIENVENIDO !! <br>NOSOTROS  TE  ASESOREAMOS.</h2>
                            <p>Las oportunidades grandes nacen de haber sabido aprovechar las pequeñas. </p>
                        </div>
                    </div>
                </div>--%>
            </div>
            <!--/.carousel-inner-->
            <nav id="nav-arrows" class="nav-arrows hidden-xs hidden-sm visible-md visible-lg">
                <a class="sl-prev hidden-xs" href="#ContenedorCarruselInicio" data-slide="prev">
                    <i class="fa fa-angle-left fa-3x"></i>
                </a>
                <a class="sl-next" href="#ContenedorCarruselInicio" data-slide="next">
                    <i class="fa fa-angle-right fa-3x"></i>
                </a>
            </nav>
        </div>
    </section>
    <!--
    End #home Slider
    ========================== -->
    <!--
    #about
    ========================== -->
    <section id="about">
        <div class="container">
            <div class="row">
                <div class="section-title text-center wow fadeInUp">
                    <h2>Acerca de  Nosotros</h2>
                    </br>
                    <h3>Misión</h3>
                    </br>
                    <p>
                        Mejorar la calidad de vida de nuestros clientes, sanando sus finanzas a través de la obtención de recursos con servicios de alta calidad.
                    </p>
                    </br>
                    <h3>Visión</h3>
                    </br>
                    <p>
                        Liderar en la industria ofreciendo la mejor experiencia y contribuir a mejorar la vida de muchas personas
                    </p>
                    </br>
                    <h3>Valores
                    </br>
                        <h4>
                            L E    P L U S   
                        </h4>
                    </h3>
                    </br>
                    <ul>
                        <li>Lealtad</li>
                        <li>Éxito</li>
                        <li>Pasión </li>
                        <li>Liderazgo</li>
                        <li>Unión </li>
                        <li>Servicio</li>
                    </ul>
                </div>

                <!-- <div class="about-us text-center wow fadeInDown">
                    <img src="img/logoKungio5.png" alt="About Us" class="img-responsive">
                </div> -->

<%--                <div class="service-features wow fadeInRight">
                    <h3>Nuestros valores</h3>
                    <ul>
                        <li>Lealtad</li>
                        <li>Éxito</li>
                        <li>Pasión </li>
                        <li>Liderazgo</li>
                        <li>Unión </li>
                        <li>Servicio</li>
                    </ul>
                </div>--%>
            </div>
        </div>
    </section>
    <!--
    End #about
    ========================== -->
    <!--
    #service
    ========================== -->
    <section id="service">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="section-title text-center wow fadeInDown">
                        <h2>Servicios</h2>
                        <p>Somos un grupo de profesionales que estaremos a su  dispoción para ayudarle. </p>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6 col-sm-12 wow fadeInLeft">
                    <div class="media">
                        <a href="#" class="pull-left">
                            <img src="img/icons/cog.png" class="media-object" alt="Monitor">
                        </a>
                        <div class="media-body">
                            <h3>RECUPERO MI INFONAVIT</h3>
                            <li>
                            El crédito de la línea 4 es otorgado por el Infonavit, sin garantía hipotecaria y de forma individual.
                            </li>
                            <li>
                            Los recursos obtenidos a través de este crédito deben ser aplicados a mejoras o remodelación en vivienda, siempre y cuando estos trabajos no afecten la estructura con construcciones, demolición de muros, columnas, losas, entre otras. 
                            </li>
                            <li>
                            No importa que la vivienda este a nombre de otra persona diferente al DH, ya que para esto un perito valuador elabora un avalúo de la propiedad donde va a se van a realizar los trabajos, este costo será cubierto por el fideicomiso que administra los recursos (va de $2,088 a $2,500 “+ IVA”) y descontado del saldo disponible para el trámite, con la finalidad de que el DH no desembolse estos recursos.
                            </li>
                            <li>
                            Las mejoras o remodelaciones, deben llevarse a cabo en un plazo máximo de 180 días y su costo debe ser menor al 50% del valor de la estructura de la propiedad. Durante este plazo, el Infonavit realizará descuentos mensuales como pago de éste “crédito temporal”, por lo que recomendamos que sean en un plazo menor a un mes para que exista mayor probabilidad de que el Infonavit realice sólo 3 descuentos por nómina.
                            </li>
                            <li>
                            Los recursos de la subcuenta de vivienda, serán depositados y administrados por un fideicomiso y al final serán depositados en la cuenta del DH para que el los administre, escoja a su gusto los materiales y con sus proveedores de confianza. La obra será verificada por funcionarios del Infonavit en dos ocasiones (una antes de los trabajos y otra al concluir). El costo de cada verificación es de $900.00 + iva y esto también lo paga el fideicomiso con los recursos que serán descontados del saldo disponible para el trámite, con la finalidad de que el DH no desembolse estos recursos.
                            </li>
                            <li>
                            Al finalizar los trabajos, el DH no tendrá ninguna deuda, porque el saldo de la subcuenta de vivienda se aplicará a la liquidación del crédito. Todo esto, siempre y cuando la mejora de la vivienda se pueda comprobar, conforme al presupuesto y programa autorizados, siendo así el Infonavit emite un “Dictamen 
                            </li>
                            <li>
                            Los descuentos por nomina se cancelan al entregar en RH de la empresa donde labora el DH, el “Aviso de suspensión de descuentos” documento que emite el Infonavit siempre y cuando exista el DAR positivo. El Aviso de Suspensión será enviado personalmente al DH a través del portal de Infonavit en la sección de “Mi cuenta Infonavit”. El DH debe dar seguimiento a este proceso ya que el acceso es personal y con contraseña confidencial.
                            </li>
                            <li>
                            Los tramites los podemos realizar por ahora en la Ciudad  de Mexico, Estado de Mexico y Monterrey
                            </li>
                        </div>
                    </div>
                </div>
                <div class="col-md-6 col-sm-12 wow fadeInRight" data-wow-delay="0.2s">
                    <div class="media">
                        <a href="#" class="pull-left">
                            <img src="img/icons/cog.png" alt="Cog">
                        </a>
                        <div class="media-body">
                            <h3>RECUPERO MI AFORE</h3>
                            <li>
                             Logramos la recuperación de gran porcentaje del saldo del estado de cuenta de la Afore de nuestros clientes, a través de un Juicio especial que dura de 6 meses a 1 año, incluso recuperamos saldos de gente finada para ayudar a sus deudos con estos recursos.
                            </li>
                            <li>
                             En este juicio no reclamamos los conceptos de “Cesantía en edad avanzada y vejez” ni “Cuota social” ya que dichos conceptos son indispensables para financiar la pensión del cliente y con la finalidad de no afectar en ese sentido.
                            </li>
                            <li>
                             Este proceso es muy simple para nuestros clientes ya que deben otorgar un poder simple a nuestros abogados para que ellos sean los que den seguimiento a todo el proceso, logrando con esto que el cliente se tenga que presentar sólo en 2 ocasiones, máximo 3: una cuando se lleva a cabo un testimonial y la segunda cuando acude a recibir el cheque del monto recuperado.
                            </li>
                            <li>
                             Una forma de garantizar nuestro servicio es que los honorarios de nuestro equipo de abogados, sus gastos y las dadivas que se eroguen durante todo el proceso, no deben ser cubiertos en ese momento por el cliente, ya que van incluidos en el porcentaje de honorarios de la empresa, el cual deberá ser liquidado al momento de que el cliente cobra el cheque de su recuperación y dichos honorarios son por el 30% sobre el monto recuperado.
                            </li>


                        </div>
                    </div>

                </div>
                <div class="col-md-6 col-sm-12 wow fadeInLeft">
                    <div class="media">
                        <a href="#" class="pull-left">
                            <img src="img/icons/cog.png" alt="Ruler">
                        </a>
                        <div class="media-body">
                            <h3>ME PENSIONO</h3>
                            <li>
                             A partir de julio de 1997 entro en vigor una nueva ley del seguro social que cambió totalmente el esquema de pensiones, a partir de un sistema de “Capitalización individual”. Su intención es asegurar la viabilidad financiera del Sistema de Pensiones y una mayor equidad en el mediano y largo plazo. 
                            </li>
                            <li>
                            Los trabajadores que cotizaron en la ley anterior (73) siguen con el derecho de pensionarse con recursos provenientes del estado, y tienen la posibilidad de incrementar el monto del cheque de pensión llevando a cabo estrategias que planificamos tomando como base un “Proyecto de pensión especializado”.
                            </li>
                            <li>
                             Ofrecemos a nuestros clientes las siguientes opciones de pensión:
                            <li>
                                     Pensión directa
                             </li> 
                            <li>
                                     Modalidad 40
                            </li>
                            <li>
                                     Otras modalidades de pensión
                            </li>

                        </div>
                    </div>
                </div>

            </div> <!-- end .row -->
        </div> <!-- end .container -->
    </section>
    <!--
    End #service
    ========================== -->
    <!--
    #service-bottom
    ========================== -->

    <section id="service-bottom">
        <div class="container">

            <!-- <div class="service-features wow fadeInRight">
                <h3>Nuestros valores</h3>
                <ul>
                    <li>El cliente es primero</li>
                    <li>Compromiso </li>
                    <li>Entrega  oportuna </li>
                    <li>Seguridad de la información</li>
                </ul>
            </div> -->
            <!-- <div class="mobile-device"> -->
            <!-- <img data-wow-delay="0.2s" class="img-responsive white  wow fadeInLeft" src="img/icons/iphone-black.png" alt="iPhone Black"> -->
            <!-- <img data-wow-delay="0.5s" class="img-responsive white  wow fadeInLeftBig" src="img/icons/logoKungio3.png" alt="iPhone White"> -->
            <!-- </div> -->
        </div>
    </section>
    <!--
    End #service-bottom
    ========================== -->
    <!--
    #Portfolio
    ========================== -->

    <section id="portfolio">
        <div class="section-title text-center wow fadeInDown">
            <h2>Galeria</h2>
            <p>Galeria de nuestro servicios</p>
        </div>

        <nav class="project-filter clearfix text-center wow fadeInLeft" data-wow-delay="0.5s">
            <ul class="list-inline">
                <li><a href="javascript:;" class="filter" data-filter="all">Todos</a></li>
                <li><a href="javascript:;" class="filter" data-filter=".LineaIV">RECUPERO MI INFONAVIT</a></li>
                <li><a href="javascript:;" class="filter" data-filter=".RecuperaAfore">RECUPERO MI AFORE</a></li>
                <li><a href="javascript:;" class="filter" data-filter=".Pensionate">ME PENSIONO</a></li>
<%--                <li><a href="javascript:;" class="filter" data-filter=".print">Otro</a></li>--%>
            </ul>
        </nav>
        <div id="projects" class="clearfix">
            <figure class="mix portfolio-item LineaIV">
                <img class="img-responsive" src="img/portfolio/portfolio-1.jpg" alt="Portfolio Item">
                <a href="img/portfolio/portfolio-1.jpg" title="Title One" rel="portfolio" class="fancybox"><span class="plus"></span></a>
                <figcaption class="mask">
                    <h3>RECUPERO MI INFONAVIT</h3>
                    <span></span>
                </figcaption>
            </figure>
            <figure class="mix portfolio-item RecuperaAfore">
                <img class="img-responsive" src="img/portfolio/portfolio-2.jpg" alt="Portfolio Item">
                <a href="img/portfolio/portfolio-2.jpg" title="Title Two" rel="portfolio" class="fancybox"><span class="plus"></span></a>
                <figcaption class="mask">
                    <h3>RECUPERO MI AFORE</h3>
                    <span>  </span>
                </figcaption>
            </figure>
            <!--<figure class="mix portfolio-item Pensionate">
                <img class="img-responsive" src="img/portfolio/portfolio-3.jpg" alt="Portfolio Item">
                <a href="img/portfolio/portfolio-3.jpg" title="Title Three" rel="portfolio" class="fancybox"><span class="plus"></span></a>
                <figcaption class="mask">
                    <h3>ME PENSIONO</h3>
                    <span></span>
                </figcaption>
            </figure>
            <figure class="mix portfolio-item print">
                <img class="img-responsive" src="img/portfolio/portfolio-4.jpg" alt="Portfolio Item">
                <a href="img/portfolio/portfolio-4.jpg" title="Title Four" rel="portfolio" class="fancybox"><span class="plus"></span></a>
                <figcaption class="mask">
                    <h3>Otro</h3>
                    <span>Fotografias</span>
                </figcaption>
            </figure>-->


        </div> <!-- end #projects -->
    </section>
    <!--
    End #Portfolio
    ========================== -->
    <!--
    #count
    ========================== -->

    <section id="count">
        <div class="container">
            <div class="row">
                <div class="counter-section clearfix">
                    <div class="col-md-3 col-sm-6 col-xs-12 wow fadeInLeft" data-wow-delay="0.8s">
                        <div class="fact-item text-center">
                            <div class="fact-icon">
                                <i class="fa fa-users fa-lg"></i>
                            </div>
                            <span data-to="152">0</span>
                            <p>Clientes Satisfechos</p>
                        </div>
                    </div>
                    <div class="col-md-3 col-sm-6 col-xs-12 wow fadeInLeft" data-wow-delay="1.3s">
                        <div class="fact-item text-center last">
                            <div class="fact-icon">
                                <i class="fab fa-slideshare fa-lg"></i>
                            </div>
                            <span data-to="150">0</span>
                            <p>Tramites </p>
                        </div>
                    </div>

                    <div class="col-md-3 col-sm-6 col-xs-12 wow fadeInLeft" data-wow-delay="1.1s">
                        <div class="fact-item text-center last">
                            <div class="fact-icon">
                                <i class="fa fa-clock fa-lg"></i>
                            </div>
                            <span data-to="2500">0</span>
                            <p>Horas Trabajadas</p>
                        </div>
                    </div>

                    <div class="col-md-3 col-sm-6 col-xs-12 wow fadeInLeft" data-wow-delay="0.5s">
                        <div class="fact-item text-center">
                            <div class="fact-icon">
                                <i class="fa fa-check-square fa-lg"></i>
                            </div>
                            <span data-to="120">0</span>
                            <p>Servicios</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!--
    End #count
    ========================== -->
    <!--
    #about-us
    ========================== -->
    <section id="about-us">
        <div class="container">
            <div class="row">

                <div class="col-md-5 col-md-offset-1 wow fadeInLeft">
                    <div class="subtitle text-center">
                        <h3>Testimonios</h3>
                        <p>Nuestros clientes</p>
                    </div>
                    <div id="testimonial">
                        <div class="tst-item clearfix">
                            <div class="tst-single clearfix">
                                <img src="img/client/1.jpg" alt="Client" class="img-circle">
                                <div class="tst-content">
                                    <p>Testomio 1</p>
                                    <span>Juan Rodriguez ,Linea IV</span>
                                </div>
                            </div>
                            <div class="tst-single clearfix">
                                <img src="img/client/2.jpg" alt="Client" class="img-circle">
                                <div class="tst-content">
                                    <p>Testomio 2</p>
                                    <span>Victor Rosas, Linea IV</span>
                                </div>
                            </div>
                        </div>
                        <div class="tst-item">
                            <div class="tst-single clearfix">
                                <img src="img/client/3.jpg" alt="Client" class="img-circle">
                                <div class="tst-content">
                                    <p>Testomio 3 </p>
                                    <span>Pedro Torres,Pensionate</span>
                                </div>
                            </div>
                            <div class="tst-single clearfix">
                                <img src="img/client/1.jpg" alt="Client" class="img-circle">
                                <div class="tst-content">
                                    <p>Testomio 4 </p>
                                    <span>Francisco Garcia, Linea IV</span>
                                </div>
                            </div>
                        </div>
                        <!--<div class="tst-item">
                            <div class="tst-single clearfix">
                                <img src="img/client/2.jpg" alt="Client" class="img-circle">
                                <div class="tst-content">
                                    <p>Testomio 5 </p>
                                    <span>Fran Garcia, Linea IV</span>
                                </div>
                            </div>
                            <div class="tst-single clearfix">
                                <img src="img/client/3.jpg" alt="Client" class="img-circle">
                                <div class="tst-content">
                                    <p>Testomio 6 </p>
                                    <span>Fran Garcia, Linea IV</span>
                                </div>
                            </div>
                        </div>-->
                    </div> <!-- end #testimonial -->
                </div> <!-- end .col-md-5 -->
                <div class="col-md-5 col-md-offset-1 wow fadeInRight">
                    <div class="subtitle text-center">
                        <h3>Nuestras   Habilidades</h3>
                        <p>Las cosas van mejor con KUNGIO</p>
                    </div>
                    <div class="progress-bars">

                        <span>Confibilidad <small>100%</small></span>
                        <div class="progress" data-progress="100">
                            <div class="bar"></div>
                        </div>
                        <span>Seguridad <small>100%</small></span>
                        <div class="progress" data-progress="100">
                            <div class="bar"></div>
                        </div>
                        <span>Simplicidad <small>100%</small></span>
                        <div class="progress" data-progress="100">
                            <div class="bar"></div>
                        </div>
                        <span>Agilidad <small>100%</small></span>
                        <div class="progress kill-margin" data-progress="100">
                            <div class="bar"></div>
                        </div>
                    </div>  <!-- progress-bars -->
                </div>  <!-- end .col-md-5 -->
            </div>
        </div>
    </section>
    <!--
    End #about-us
    ========================== -->
    <!--
    #quotes
    ========================== -->
    <section id="quotes">
        <div class="container">
            <div class="row wow zoomIn">
                <div class="col-lg-12">
                    <div class="call-to-action text-center">
                        <p>“ Las cosas van mejor con KUNGIO”</p>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!--
    End #quotes
    ========================== -->
    <!--
    #contact
    ========================== -->
    <section id="contact">
        <div class="container">
            <div class="row">
                <div class="section-title text-center wow fadeInDown">
                    <h2>Contáctanos</h2>
                    <p>Desea realizar  su  trámite  o información,proporcione su información,en breve le contactarémos, estamos para servirle. </p>
                </div>

                <div class="col-md-8 col-sm-9 wow fadeInLeft">
                    <div class="contact-form clearfix">
<%--                        <form action="index.html" method="post">--%>
                            <div class="input-field">
                                <asp:TextBox ID="txtNombre" placeholder="Nombre:" runat="server" CssClass="form-control" ></asp:TextBox>
                                <%--<input type="text" class="form-control" name="name" placeholder="Nombre" required="">--%>

                            </div>
                            <div class="input-field">
                                <asp:TextBox ID="txtCorreo" placeholder="Correo:" runat="server" CssClass="form-control" ></asp:TextBox>
<%--                            <input type="email" class="form-control" name="email" placeholder="Correo" required="">--%>
                            </div>
                            <div class="input-field message">
                                <asp:TextBox ID="txtMensaje"  placeholder="Mensaje:" runat="server" CssClass="form-control"  mode="multiline" maxlength="1200" rows="5" lines="10" cols="10" wrap="true"  ></asp:TextBox>
<%--                            <textarea name="message" class="form-control" placeholder="Mensaje" required=""></textarea>--%>
                            </div>
                            <asp:Button ID="btnEnviar" CssClass="btn btn-blue" runat="server" Text="Enviar" OnClick="btnEnviar_Click" />
     
                            <%--<input type="submit" class="btn btn-blue pull-right" value="Enviar" id="msg-submit">--%>
<%--                        </form>--%>
                    </div> <!-- end .contact-form -->
                </div> <!-- .col-md-8 -->
                <div class="col-md-4 col-sm-3 wow fadeInRight">
                    <div class="contact-details">
                        <span>Teléfono</span>
                        <p>  <a href="http://5587670941"><i class="fa fa-phone fa-lg"></i></a>  55 87 67 09 41  </p>
                        <br>  <br>
                        <span>Correo</span>
                        <p>  <a href="mailto:contacto@kungio.mx"><i class="fa fa-comment fa-lg"></i></a> contacto@kungio.mx  </p>
                    </div> <!-- end .contact-details -->
                </div> <!-- .col-md-4 -->
            </div>
        </div>
    </section>
    <!--
    End #contact
    ========================== -->
    <!--
    #prcaificate
    ========================== -->
    <section id="precalificate">
        <div class="container">
            <div class="row">
                <div class="section-title text-center wow fadeInDown">
                    <h2>Precalificate</h2>
                    <p>Proporcione su información por favor, le informaremos su calificación en breve por medio de su correo. </p>
                </div>

                <div class="col-md-8 col-sm-9 wow fadeInLeft">
                    <div class="contact-form clearfix">
                        <form action="index.html" method="post">
                            <div class="input-field">
                                <asp:TextBox ID="txtPNombre" placeholder="Nombre:" runat="server" CssClass="form-control" ></asp:TextBox>
<%--                            <input type="text" class="form-control" name="name" placeholder="Nombre" required="">--%>
                            </div>
                            <div class="input-field">
                                <asp:TextBox ID="txtImss" placeholder="No. Imss:" runat="server" CssClass="form-control" ></asp:TextBox>
<%--                                <input type="text" class="form-control" name="noimss" placeholder="No. Imss " required="">--%>
                            </div>
                            <div class="input-field">
                                <asp:TextBox ID="txtFecNac" placeholder="Fecha Nacimiento:" runat="server" CssClass="form-control" ></asp:TextBox>
<%--                                <input type="text" class="form-control" name="fecnac" placeholder="Fecha Nacimiento " required="">--%>
                            </div>
                            <div class="input-field">
                                <asp:TextBox ID="txtPCorreo" placeholder="Correo:" runat="server" CssClass="form-control" ></asp:TextBox>
<%--                                <input type="email" class="form-control" name="email" placeholder="Correo" required="">--%>
                            </div>
                            <asp:Button ID="btnCali" CssClass="btn btn-blue" runat="server" Text="Precalificar" OnClick="btnCali_Click" />
<%--                        <input type="submit" class="btn btn-blue pull-right" value="Calificar" id="msg-submit">--%>
                        </form>
                    </div> <!-- end .contact-form -->
                </div> <!-- .col-md-8 -->
            </div>
        </div>
    </section>
    <!--
    End #prcaificate
    ========================== -->
    <!--
    #footer
    ========================== -->
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
                        <p>Derechos Reservados KUNGIO ,  <a href="http://KunGio.mx"></a> Diseñado por Francisco Garcia <a target="_blank" href="http://KunGio.mx"></a></p>
                    </div>
                </div>
            </div>
        </div>
    </footer>
    <!--
    End #footer
    ========================== -->
    <!--
    JavaScripts
    ========================== -->
    <!-- main jQuery -->
    <script src="Comun/js/vendor/jquery-1.11.1.min.js"></script>
    <!-- Bootstrap -->
    <script src="Comun/js/bootstrap.min.js"></script>
    <!-- jquery.nav -->
    <script src="Comun/js/jquery.nav.js"></script>
    <!-- Portfolio Filtering -->
    <script src="Comun/js/jquery.mixitup.min.js"></script>
    <!-- Fancybox -->
    <script src="Comun/js/jquery.fancybox.pack.js"></script>
    <!-- Parallax sections -->
    <script src="Comun/js/jquery.parallax-1.1.3.js"></script>
    <!-- jQuery Appear -->
    <script src="Comun/js/jquery.appear.js"></script>
    <!-- countTo -->
    <script src="Comun/js/jquery-countTo.js"></script>
    <!-- owl carousel -->
    <script src="Comun/js/owl.carousel.min.js"></script>
    <!-- WOW script -->
    <script src="Comun/js/wow.min.js"></script>
    <!-- theme custom scripts -->
    <script src="Comun/js/main.js"></script>

    <script src="comun/jquery/jquery.min.js"></script>
    <script src="comun/owlCarousel/js/owl.carousel.min.js"></script>
    <script src="comun/app/js/main.js"></script>

    </form>
</body>
</html>






