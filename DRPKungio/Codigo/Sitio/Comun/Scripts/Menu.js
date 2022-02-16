//window.onload = function () {

$(document).ready(function () {
    //var botonMenu = document.getElementById('IdBotonMenu');http://localhost:10442/../css
    //var contenedorMenu = document.getElementById('IdMenuNav');
    //botonMenu.addEventListener('click', function () {
    //    contenedorMenu.classList.toggle('MenuActivo');
    //})
    //$("#IdBotonMenu").on('click', function () {
    //    $("#IdMenuNav").toggleClass("MenuActivo");
    //})

    $('#IdBotonMenu').on('click', function () {
        var clase = $("#IdBotonMenu").attr('class');

        if ( clase == "MenuBotonActivacion icon-window-minimize") {

            $('#IdBotonMenu').removeClass('MenuBotonActivacion icon-window-minimize').addClass('MenuBotonActivacion icon-menu');
            //$('nav').css({ 'left': '-900px' });
            $('nav').css({ 'margin-left': '-100%' });
            //$('nav').css('visibility', 'hidden' );

        }
        else {

            $('#IdBotonMenu').removeClass('MenuBotonActivacion icon-menu').addClass('MenuBotonActivacion icon-window-minimize');
            //$('nav').css({ 'left': '0px' });
            $('nav').css({ 'margin-left': '0' });
            //$('nav').css("visibility", 'visible');
        }

    });



    $(".submenu").click(function () {
        //if ($('#btn-menu').attr('checked', true))
        $(this).children("ul").slideToggle();
    });
    $("ul").click(function (p) {
        p.stopPropagation();
    });
    $('#IdBotonElementosSeguridad').on('hover', function () {

        var claseseg = $("#IdContenedorSeguridad").attr('class');
        if (claseseg == 'ContenedorSeguridad')
        {
            $('#IdContenedorSeguridad').removeClass('ContenedorSeguridad').addClass('ContenedorSeguridadVisible');
        }
        else if (claseseg == 'ContenedorSeguridadVisible')
        {
            $('#IdContenedorSeguridad').removeClass('ContenedorSeguridadVisible').addClass('ContenedorSeguridad');
        }
        //$('#IdContenedorSeguridad').removeClass('ContenedorSeguridad').addClass('ContenedorSeguridadVisible');
        //if ($('#IdContenedorSeguridad').is(':visible')) {
        //    $("#IdContenedorSeguridad").hide();
            //$("#IdContenedorSeguridad").show();
            //$("#IdContenedoSeguridad").fadeOut();
            //$("#IdContenedoSeguridad").fadeOut("slow");
            //$("#IdContenedoSeguridad").fadeOut(2000);

        //}
        //else {
        //    $("#IdContenedorSeguridad").show();
            //$("#IdContenedoSeguridad").fadeIn();
            //$("#IdContenedoSeguridad").fadeIn("slow");
            //$("#IdContenedoSeguridad").fadeIn(2000);

        //}
        //$("#IdContenedorSeguridad").show();
        
    });
});




//$(".submenu").click(function () {
//    $(this).children("ul").slideUp();
//})

