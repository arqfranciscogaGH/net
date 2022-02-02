/***************************************************************************************************************
*********************************************      ES XML        ***********************************************
****************************************************************************************************************
* Descripción: Verifica si un Objeto esta en formato Xml.                                                      *
* Objeto: Objeto a verificar (Puede estar en tipo Cadena u objeto).                                            *
*                                                                                                              *
* Retorno: true si el objeto es xml, false de lo contrario.                                                    *
* *************************************************************************************************************/

function EsXml(Objeto) {
    var Resultado = true;
    try {
        $.parseXML(Objeto);
    }
    catch (ErrorEsXml) {
        Resultado = false;
    }
    return Resultado;
}

/***************************************************************************************************************
******************************************      XML A JSON        **********************************************
****************************************************************************************************************
* Descripción: Convierte un archivo Xml a Objeto Json.                                                         *
* Xml: Objeto o Cadena en formato Xml a convertir.                                                             *
*                                                                                                              *
* Retorno: Objeto Json que representa a el Xml pasado como argumento.                                          *
* *************************************************************************************************************/
function XmlAJson(Xml) {
    return $.xml2json(Xml);
}

/***************************************************************************************************************
********************************************      ES JSON        ***********************************************
****************************************************************************************************************
* Descripción: Verifica si un Objeto esta en formato JSon.                                                     *
* Objeto: Objeto a verificar (Puede estar en tipo Cadena u objeto).                                            *
*                                                                                                              *
* Retorno: true si el objeto es json, false de lo contrario.                                                   *
* *************************************************************************************************************/
function EsJSon(Objeto) {
    var Resultado = true;
    try {
        $.parseJSON(Objeto);
    }
    catch (ErrorEsJSon) {
        Resultado = false;
    }
    return Resultado;
}

/***************************************************************************************************************
*************************************      BUSCAR ELEMENTO JSON        *****************************************
****************************************************************************************************************
* Descripción: Busca un elemento en el objeto JSon pasado como parámetro.                                      *
* ObjectoJson: Objeto Json en el cual se va a buscar.                                                          *
* NombreElemento: Nombre del elemento que se busca.                                                            *
* RetornarElementoPadre: true si se requiere encontrar el elemento padre, false si se                          *
*                        requiere encontrar el valor del elemento.                                             *
*                                                                                                              *
* Retorno: Elemento padre que contiene el valor buscado, ó valor del elemento buscado.                         *
* *************************************************************************************************************/
function BuscarElementoJSon(ObjectoJson, NombreElemento, RetornarElementoPadre) {
    if (NombreElemento in ObjectoJson)
        return (RetornarElementoPadre) ? ObjectoJson : ObjectoJson[NombreElemento];
    for (Llave in ObjectoJson) {
        if ((typeof (ObjectoJson[Llave])) == 'object') {
            var Objeto = BuscarElementoJSon(ObjectoJson[Llave], NombreElemento, RetornarElementoPadre);
            if (Objeto) return Objeto;
        }
    }
    return null;
}

/***************************************************************************************************************
********************************      ADMINISTRADOR AUTOCOMPLETAR        ***************************************
****************************************************************************************************************
* Nombre: AdministradorAutoCompletar                                                                           *
* Descripción: Clase que controla la creación y funcionalidad de controles autocompletar.                      *
* Elemento: Objeto que comunmente es un elemento text al cual se asignará la funcionalidad.                      *
* Datos: Arreglo de tipo string con el cual se iniciará el objeto. Estos son los elementos en los cuales se    * 
*        buscará el texto a autocompletar.                                                                     *
* FuncionSeleccion: Función que se ejecuta cuando el usuario selecciona una opción.                            *
* *************************************************************************************************************/

function AdministradorAutoCompletar(Elemento, Datos, FuncionSeleccion) {
    this.Datos = Datos;
    this.Elemento = Elemento;
    this.FuncionSeleccion = FuncionSeleccion;

    this.MensajeError = "AdministradorAutoCompletar: ";

    var Instancia = this;

    /*
    * Descripción: Activa la funcionalidad de autocompletar en el objeto.
    */
    this.ActivarAutocompletar = function () {
        $(Instancia.Elemento).autocomplete({ disabled: false });
    };
    /*
    * Descripción: DesActiva la funcionalidad de autocompletar en el objeto.
    */
    this.DesActivarAutoCompletar = function () {
        $(Instancia.Elemento).autocomplete({ disabled: true });
    };
    /*
    * Descripción: Cierra el menu que muestra las sugerencias.
    */
    this.CerrarMenuAutoCompletar = function () {
        $(Instancia.Elemento).autocomplete("close");
    };
    /*
    * Descripción: Asigna el arreglo de cadenas como nuevos datos sobre los cuales buscará las sugerencias para autocompletar.
    * Datos: Areglo de cadenas sobre el cual se realiza la busquedad.
    */
    this.AsignarDatos = function (Datos) {
        if (!ValidarDatos(Datos)) return;
        Instancia.Datos = Datos;
        $(Instancia.Elemento).autocomplete({ source: Instancia.Datos, focus: function (Evento, Dato) {
            $(Instancia.Elemento).val(Dato.item.value);
            return false;
        }, search: ''
        });
    };
    /*
    * Descripción: Muestra alguna palabra especifica en las sugerencias.
    * Palabra: Palabra a mostrar.
    */
    this.MostrarPalabra = function (Palabra) {
        $(Instancia.Elemento).autocomplete({ minLength: 0 });
        $(Instancia.Elemento).autocomplete("search", Palabra);
    };
    /*
    * Descripción: Asigna la función que se ejecutará cuando el usuario seleccione una opción.
    * FuncionSeleccion: Funcion a asignar, se pasan dos parametros(Evento, Elemento). Donde el Evento es el que origina el control
    *                   y el Elemento es el elemento seleccionado.
    */
    this.AsignarFuncionSeleccion = function (FuncionSeleccion) {
        if (!ValidarFuncionSeleccion(FuncionSeleccion)) return;
        $(Instancia.Elemento).unbind("autocompleteselect", Instancia.FuncionSeleccion);
        Instancia.FuncionSeleccion = FuncionSeleccion;
        $(Instancia.Elemento).bind("autocompleteselect", Instancia.FuncionSeleccion);
    };
    /*
    * Descripción: Valida los datos que sean un array.
    * Datos: Objeto a validar.
    */
    function ValidarDatos(Datos) {
        var Resultado = true;
        if (!$.isArray(Datos)) {
            alert(Instancia.MensajeError + ' El objeto Datos debe ser un arreglo.');
            Resultado = false;
        }
        return Resultado;
    }
    /*
    * Descripción: Valida si existe la función a ejecutar.
    * FuncionSeleccionar: Función a validar.
    */
    function ValidarFuncionSeleccion(FuncionSeleccionar) {
        var Resultado = true;
        if (FuncionSeleccionar) {
            if (typeof (FuncionSeleccionar) !== 'function') {
                alert(Instancia.MensajeError + ' La función con nombre [' + FuncionSeleccionar + '] no existe.');
                Resultado = false;
            }
        }
        else
            Resultado = false;
        return Resultado;
    }
    /*
    * Descripción: Valida si existe el elemento.
    * Elemento: Elemento a validar.
    */
    function ValidarElemento(Elemento) {
        var Resultado = true;
        if (!Elemento) {
            alert(Instancia.MensajeError + ' El elemento con Id [' + IdElemento + '] no existe.');
            Resultado = false;
        }
        return Resultado;
    }

    if (!ValidarElemento(Instancia.Elemento)) return;
    if (!ValidarDatos(Instancia.Datos)) return;
    Instancia.AsignarDatos(Instancia.Datos);
    Instancia.ActivarAutocompletar();
    ValidarFuncionSeleccion(Instancia.FuncionSeleccion);
    Instancia.AsignarFuncionSeleccion(Instancia.FuncionSeleccion);

}

/***************************************************************************************************************
*********************************      ADMINISTRADOR GEOCODIFICACIÓN        ************************************
****************************************************************************************************************
* Nombre: AdministradorGeocodificacion                          *
* *************************************************************************************************************/
function AdministradorGeocodificacion() {

    this.Geocodificador = new google.maps.Geocoder();
    this.MensajeError = "AdministradorGeocodificacion: ";
    var Instancia = this;

    /*
    * Descripción: Busca una dirección(es) con el objeto geocode de google maps con la dirección pasada como parámetro.
    * Direccion: Objeto de tipo string que representa la dirección a buscar.
    * FuncionRetorno: Función que se ejecuta cuando el objeto geocode de google maps regrese resultados.
    */
    this.BuscarDireccion = function (Direccion, FuncionRetorno) {
        Direccion += ', MX';
        if (!ValidarDireccion(Direccion)) return;
        if (!ValidarFuncionRetorno(FuncionRetorno)) return;
        Instancia.Geocodificador.geocode({ 'address': Direccion, region: 'mx' }, FuncionRetorno);
    };
    /*
    * Descripción: Busca una dirección(es) con el objeto geocode de google maps con las coordenadas X,Y.
    * X: Coordenada X.
    * Y: Coordenada Y.
    * FuncionRetorno: Función que se ejecuta cuando el objeto geocode de google maps regrese resultados.
    */
    this.BuscarDireccionDeXY = function (X, Y, FuncionRetorno) {
        if (!ValidarFuncionRetorno(FuncionRetorno)) return;
        if (!ValidarEsNumero(X)) return;
        if (!ValidarEsNumero(Y)) return;

        var Coordenada = new google.maps.LatLng(Y, X);
        Instancia.Geocodificador.geocode({ 'location': Coordenada }, FuncionRetorno);
    };
    /*
    * Descripción: Valida si el objeto Numero es de tipo numérico.
    * Numero: Objeto a validar.
    */
    function ValidarEsNumero(Numero) {
        var Resultado = true;
        if (Numero) {
            if (typeof (Numero) !== 'numeric') {
                alert(Instancia.MensajeError + ' El parámetro no es númerico.');
                Resultado = false;
            }
        }
        else
            Resultado = false;
        return Resultado;
    }
    /*
    * Descripción: Valida si el objeto es una cadena string.
    * Direccion: Objeto a validar.
    */
    function ValidarDireccion(Direccion) {
        var Resultado = true;
        if (typeof (Direccion) !== 'string')
            Resultado = false;
        return Resultado;
    }
    /*
    * Descripción: Valida si existe la función a ejecutar.
    * FuncionSeleccionar: Función a validar.
    */
    function ValidarFuncionRetorno(FuncionRetorno) {
        var Resultado = true;
        if (FuncionRetorno) {
            if (typeof (FuncionRetorno) !== 'function') {
                alert(Instancia.MensajeError + ' La función con nombre [' + FuncionRetorno + '] no existe.');
                Resultado = false;
            }
        }
        else
            Resultado = false;
        return Resultado;
    }
}

/***************************************************************************************************************
***********************************      ADMINISTRADOR MENSAJE        ******************************************
****************************************************************************************************************
* Nombre: AdministradorMensaje                                                                                 *
* Descripción: Clase que controla la creación y funcionalidad del control de mensajes para la aplicación.      *
* **************************************************************************************************************
                                                                                                         
* EnuTipoMensaje: Controla el tipo de mensaje a desplegar.                                                  
* EnuResultadoMensaje: Controla el resultado del mensaje cuando el usuario hace click sobre algun botón.    
* EnuBotonesMensaje: Controla los botones que se mostrarán en el mensaje.                                                                                                                                       
*/

window.EnuTipoMensaje = { "Informativo": 1, "Pregunta": 2, "Advertencia": 3, "Error": 4, "Ninguno": 5 };
window.EnuResultadoMensaje = { "Aceptar": 1, "Cancelar": 2, "Anular": 3, "Reintentar": 4, "Omitir": 5, "Si": 6, "No": 7 };
window.EnuBotonesMensaje = { "AnularReintentarOmitir": 1, "Aceptar": 2, "AceptarCancelar": 3, "ReintentarCancelar": 4, "SiNo": 5, "SiNoCancelar": 6 };

function AdministradorMensaje() {
    /*
    * Descripción: Muestra el mensaje con los datos pasados como parámetros.
    *
    * Titulo: Título del mensaje a mostrar.
    * MensajeTexto: Mensaje a mostrar.
    * TipoMensaje: Tipo de mensaje a mostrar, el valor debe ser uno de la enumeración EnuTipoMensaje.
    * BotonesMensaje: Los botones que se mostrarán, el valor debe ser uno de la enumeración EnuBotonesMensaje.
    * Funcion: Función que se ejecuta cuando el usuario da click en alguno de los botones.
    *
    * Retorno: Objeto del tipo EnuResultadoMensaje que se incluye en la función pasada como parámetro.
    */
    this.MostrarMensaje = function (Titulo, MensajeTexto, TipoMensaje, BotonesMensaje, Funcion) {
        var DivContenedorMensaje = document.getElementById('divContenedorMensaje');
        if (!DivContenedorMensaje) {
            CrearMensaje();
        }

        var ElementoTitulo = document.getElementById("divTituloMensaje");
        var ElementoMensaje = document.getElementById("divTextoMensaje");
        if (ElementoTitulo != undefined) { $(ElementoTitulo).text(Titulo); }
        if (ElementoMensaje != undefined) { $(ElementoMensaje).text(MensajeTexto); }

        OcultarElemento("divIconoMensajeAviso");
        OcultarElemento("divIconoMensajePregunta");
        OcultarElemento("divIconoMensajeAdvertencia");
        OcultarElemento("divIconoMensajeError");

        //Oculta todos los botones
        OcultarElemento("ahrefBotonAceptar");
        OcultarElemento("ahrefBotonCancelar");
        OcultarElemento("ahrefBotonAnular");
        OcultarElemento("ahrefBotonReintentar");
        OcultarElemento("ahrefBotonOmitir");
        OcultarElemento("ahrefBotonSi");
        OcultarElemento("ahrefBotonNo");

        var ElementoDivICono = null;

        if (TipoMensaje === null || (TipoMensaje !== undefined && $.trim(TipoMensaje).length === 0)) TipoMensaje = EnuTipoMensaje.Informativo; //Tipo de Mensaje por default

        switch (TipoMensaje) {
            case EnuTipoMensaje.Informativo:
                {
                    ElementoDivICono = document.getElementById('divIconoMensajeAviso');
                    break;
                }
            case EnuTipoMensaje.Pregunta:
                {
                    ElementoDivICono = document.getElementById('divIconoMensajePregunta');
                    break;
                }
            case EnuTipoMensaje.Advertencia:
                {
                    ElementoDivICono = document.getElementById('divIconoMensajeAdvertencia');
                    break;
                }
            case EnuTipoMensaje.Error:
                {
                    ElementoDivICono = document.getElementById('divIconoMensajeError');
                    break;
                }
            case EnuTipoMensaje.Ninguno:
                {
                    break;
                }
            default:
                {
                    alert("El valor para TipoMensaje solo puede ser Informativo, Pregunta, Advertencia, Error ó Ninguno, correspondiente a la enumeración EnuTipoMensaje.");
                    OcultarMensaje(null);
                    return;
                    break;
                }
        }

        if (BotonesMensaje === null || (BotonesMensaje !== undefined && $.trim(BotonesMensaje).length === 0)) BotonesMensaje = EnuBotonesMensaje.Aceptar; //Botones del mensaje por default

        if (ElementoDivICono) { ElementoDivICono.style.display = 'block'; }

        switch (BotonesMensaje) {
            case EnuBotonesMensaje.AnularReintentarOmitir:
                {
                    MostrarElemento('ahrefBotonAnular');
                    MostrarElemento('ahrefBotonReintentar');
                    MostrarElemento('ahrefBotonOmitir');
                }
                break;
            case EnuBotonesMensaje.Aceptar:
                {
                    MostrarElemento('ahrefBotonAceptar');
                }
                break;
            case EnuBotonesMensaje.AceptarCancelar:
                {
                    MostrarElemento('ahrefBotonAceptar');
                    MostrarElemento('ahrefBotonCancelar');
                }
                break;
            case EnuBotonesMensaje.ReintentarCancelar:
                {
                    MostrarElemento('ahrefBotonReintentar');
                    MostrarElemento('ahrefBotonCancelar');
                }
                break;
            case EnuBotonesMensaje.SiNo:
                {
                    MostrarElemento('ahrefBotonSi');
                    MostrarElemento('ahrefBotonNo');
                }
                break;
            case EnuBotonesMensaje.SiNoCancelar:
                {
                    MostrarElemento('ahrefBotonSi');
                    MostrarElemento('ahrefBotonNo');
                    MostrarElemento('ahrefBotonCancelar');
                }
                break;
            default:
                {
                    alert("El valor para BotonesMensaje solo puede ser Aceptar, AceptarCancelar o algún valor válido de la Enumeración EnuBotonesMensaje.");
                    OcultarMensaje(null);
                    return;
                    break;
                }
        }

        var ContenedorMensaje = document.getElementById("divFondoMensaje");
        if (ContenedorMensaje != undefined) {
            ContenedorMensaje.style.display = 'block';
            ContenedorMensaje.style.visibility = 'visible';
            this.FuncionAceptarMensaje = Funcion;
        }

        function OcultarElemento(IdElemento) {
            var Elemento = document.getElementById(IdElemento);
            if (Elemento)
                Elemento.style.display = 'none';
        }
        function MostrarElemento(IdElemento) {
            var Elemento = document.getElementById(IdElemento);
            if (Elemento)
                Elemento.style.display = '';
        }
    };

    /*
    * Descripción: Oculta el mensaje y ejecuta la funcion que se pasa como parámetro.
    *
    * ResultadoMensaje: Resultado del mensaje, este valor es uno de la enumeración EnuResultadoMensaje.
    */

    this.OcultarMensaje = function (ResultadoMensaje) {
        var ContenedorMensaje = document.getElementById("divFondoMensaje");
        if (ContenedorMensaje != undefined) {
            ContenedorMensaje.style.display = 'none';
            if (this.FuncionAceptarMensaje) { this.FuncionAceptarMensaje(ResultadoMensaje); }
        }
    };

    /*
    * Descripción: Crea el mensaje. Se contruye el codigo html DOM.
    */

    function CrearMensaje() {
        if (!document.body) {
            alert("El documento debe tener un elemento body.");
            return false;
        }

        var ElementoPadre = null;
        ElementoPadre = document.body;

        if (!ElementoPadre) {
            alert("No se encontro un elemento body en el documento. Es necesario agregarlo.");
            return false;
        }

        var DivFondoMensaje = CrearElemento('div', 'divFondoMensaje', 'fondoMensaje', null);
        var DivContenedorMensaje = CrearElemento('div', 'divContenedorMensaje', 'contenedorMensaje', null);
        var DivTituloMensaje = CrearElemento('div', 'divTituloMensaje', 'tituloMensaje', null);
        var DivContenidoMensaje = CrearElemento('div', 'divContenidoMensaje', 'contenidoMensaje', null);
        var DivImagenMensaje = CrearElemento('div', 'divImagenMensaje', 'imagenMensaje', null);
        var DivIconoMensajeAviso = CrearElemento('div', 'divIconoMensajeAviso', 'iconoMensajeAviso', null);
        var DivIconoMensajePregunta = CrearElemento('div', 'divIconoMensajePregunta', 'iconoMensajePregunta', null);
        var DivIconoMensajeAdvertencia = CrearElemento('div', 'divIconoMensajeAdvertencia', 'iconoMensajeAdvertencia', null);
        var DivIconoMensajeError = CrearElemento('div', 'divIconoMensajeError', 'iconoMensajeError', null);

        $(DivImagenMensaje).append(DivIconoMensajeAviso);
        $(DivImagenMensaje).append(DivIconoMensajePregunta);
        $(DivImagenMensaje).append(DivIconoMensajeAdvertencia);
        $(DivImagenMensaje).append(DivIconoMensajeError);

        var DivTextoMensaje = CrearElemento('div', 'divTextoMensaje', 'textoMensaje', null);
        var DivPanelBotones = CrearElemento('div', 'divPanelBotones', 'panelBotones', null);

        var AhrefBoton = CrearElemento('a', 'ahrefBotonAceptar', 'boton', 'OcultarMensaje(EnuResultadoMensaje.Aceptar);', 'Aceptar');
        $(DivPanelBotones).append(AhrefBoton);
        AhrefBoton = CrearElemento('a', 'ahrefBotonAnular', 'boton', 'OcultarMensaje(EnuResultadoMensaje.Anular);', 'Anular');
        $(DivPanelBotones).append(AhrefBoton);
        AhrefBoton = CrearElemento('a', 'ahrefBotonReintentar', 'boton', 'OcultarMensaje(EnuResultadoMensaje.Reintentar);', 'Reintentar');
        $(DivPanelBotones).append(AhrefBoton);
        AhrefBoton = CrearElemento('a', 'ahrefBotonOmitir', 'boton', 'OcultarMensaje(EnuResultadoMensaje.Omitir);', 'Omitir');
        $(DivPanelBotones).append(AhrefBoton);
        AhrefBoton = CrearElemento('a', 'ahrefBotonSi', 'boton', 'OcultarMensaje(EnuResultadoMensaje.Si);', 'Si');
        $(DivPanelBotones).append(AhrefBoton);
        AhrefBoton = CrearElemento('a', 'ahrefBotonNo', 'boton', 'OcultarMensaje(EnuResultadoMensaje.No);', 'No');
        $(DivPanelBotones).append(AhrefBoton);
        AhrefBoton = CrearElemento('a', 'ahrefBotonCancelar', 'boton', 'OcultarMensaje(EnuResultadoMensaje.Cancelar);', 'Cancelar');
        $(DivPanelBotones).append(AhrefBoton);

        $(DivContenidoMensaje).append(DivImagenMensaje);
        $(DivContenidoMensaje).append(DivTextoMensaje);
        $(DivContenidoMensaje).append(DivPanelBotones);

        $(DivContenedorMensaje).append(DivTituloMensaje);
        $(DivContenedorMensaje).append(DivContenidoMensaje);
        $(DivFondoMensaje).append(DivContenedorMensaje);

        $(ElementoPadre).append(DivFondoMensaje);

        this.OcultarMensaje(null);

        function CrearElemento(TipoElemento, IdElemento, ClaseElemento, FuncionClick, Texto) {
            var Elemento = $(document.createElement(TipoElemento)).
                     attr("id", IdElemento).
                     addClass(ClaseElemento);
            if (FuncionClick)
                $(Elemento).attr("onclick", FuncionClick);
            if (Texto)
                $(Elemento).text(Texto);

            return Elemento;
        }
    }

    /*
    * Se ponen visibles las funciones para que puedan ser invocadas desde el objeto window.
    */
    window.MostrarMensaje = this.MostrarMensaje;
    window.OcultarMensaje = this.OcultarMensaje;
}

/***************************************************************************************************************
******************************      ADMINISTRADOR INDICADOR ESPERA        **************************************
****************************************************************************************************************
* Nombre: AdministradorIndicadorEspera                                                                         *
* Descripción: Clase que controla la creación y funcionalidad del control de indicador espera de la aplicación.*
* **************************************************************************************************************/

function AdministradorIndicadorEspera() {
    var ElementoPadre = null;
    /*
    * Descripción: Muestra el control de indicador espera segun los parámetros pasados.
    * IdContenedor: Id del contenedor del indicador espera.
    * NombreClase: Nombre de la clase css que se pondrá al IndicadorEspera.
    */
    this.MostrarIndicadorEspera = function (IdContenedor, NombreClase) {
        CrearIndicadorEspera();
        var Posicion = null;
        var ContenedorIndicadorEspera = ElementoPadre;
        var Contenedor = document.getElementById(IdContenedor);
        if (Contenedor)
            ContenedorIndicadorEspera = Contenedor;

        var NombreClaseIndicador = "contenedorCargador";

        if (!IdContenedor) {
            IdContenedor = '';
            Posicion = 'fixed';
        }
        else {
            IdContenedor = '_' + IdContenedor;
        }
        var IdIndicadorEspera = 'IndicadorEspera' + IdContenedor;

        var IndicadorEspera = document.getElementById(IdIndicadorEspera);

        if (!IndicadorEspera) {
            IndicadorEspera = $(document.createElement("div")).attr("id", IdIndicadorEspera);
            var ElementoSpan = $(document.createElement("span")).attr("id", "IndicadorEsperaImagen_" + IdContenedor);
            $(IndicadorEspera).append(ElementoSpan);
            $(ContenedorIndicadorEspera).append(IndicadorEspera);
            $(IndicadorEspera).addClass(NombreClaseIndicador);
        }

        if (NombreClase && $.trim(NombreClase).length > 0)
            $(IndicadorEspera).removeClass().addClass(NombreClase);

        ContenedorIndicadorEspera.style.position = 'relative';

        if (Posicion !== null) $(IndicadorEspera)[0].style.position = Posicion;
        $(IndicadorEspera)[0].style.display = 'block';
        $(IndicadorEspera)[0].style.visibility = 'visible';
    };

    /*
    * Descripción: Oculta el control de indicador espera.
    * IdContenedor: Id del contenedor del indicador espera.
    */
    this.OcultarIndicadorEspera = function (IdContenedor) {
        if (!IdContenedor)
            IdContenedor = '';
        else
            IdContenedor = '_' + IdContenedor;

        var IdIndicadorEspera = 'IndicadorEspera' + IdContenedor;
        var DivContenedor = document.getElementById(IdIndicadorEspera);
        if (DivContenedor) {
            DivContenedor.style.display = 'none';
            DivContenedor.style.visibility = 'hidden';
        }
    };

    function CrearIndicadorEspera() {
        if (!document.body) {
            alert("El documento debe tener un elemento body.");
            return false;
        }

        ElementoPadre = document.body;

        if (!ElementoPadre) {
            alert("No se encontro un elemento body en el documento. Es necesario agregarlo.");
            return false;
        }
    }

    /*
    * Se ponen las funciones para que puedan ser invocadas desde el objeto window.
    */
    window.MostrarIndicadorEspera = this.MostrarIndicadorEspera;
    window.OcultarIndicadorEspera = this.OcultarIndicadorEspera;
}

/***************************************************************************************************************
************************************      ADMINISTRADOR SERVICIO        ****************************************
****************************************************************************************************************
* Nombre: EjecutorServicio                                                                                     *
* Descripción: Clase que controla la llamada a servicios web desde Ajax con JQuery.                            *
****************************************************************************************************************

* EnuTecnologiaServicio: Controla el tipo del web services.
*/

window.EnuTecnologiaServicio = { "NET": 1, "JAVA": 2 };

function AdministradorServicio() {
    /*
    * Descripción: Consulta el servicio web pasado como parámetro.
    *
    * UrlMetodoServicio: Url del servicio web y método a consultar.
    *                    puede ser una ruta absoluta o relativa.
    * Parametros: Parametros en formato JSON que recibe el método a consultar. Preferible en orden.
    * TecnologiaServicio: Objeto de tipo EnuTecnologiaServicio que describe la tecnología en la que el servicio web esta implementado.
    * EspacioNombres: Espacio de nombres del servicio web.
    * FuncionExito: Función que controla el resultado exitoso de la petición.
    * FuncionError: Función que controla el resultado erroneo de la petición.
    */
    this.EjecutarServicio = function (UrlServicio, Parametros, TecnologiaServicio, EspacioNombres, FuncionExito, FuncionError) {
        if (!jQuery.support.cors)
            jQuery.support.cors = true;
        var ParametrosJSON = null;

        try {
            ParametrosJSON = eval('(' + Parametros + ')');
        }
        catch (Error) {

        }

        if (ParametrosJSON === null) {
            alert('Debe proporcionar los párametros en una cadena con formato JSON.');
            return;
        }

        var Metodo = UrlServicio.substring(UrlServicio.lastIndexOf('/') + 1);

        var UrlMetodoServicio = ObtenerUrlMetodoServicio(UrlServicio, Metodo, TecnologiaServicio);

        var MensajeSoap = ObtenerMensajeSoap(Metodo, ParametrosJSON, EspacioNombres, TecnologiaServicio);

        if (UrlMetodoServicio === null) {
            alert('Debe proporcionar una Tecnología de Servicio válida.');
            return;
        }

        $.ajax({
            type: 'POST',
            contentType: 'text/xml; charset=utf-8',
            url: UrlMetodoServicio,
            chache: false,
            processData: false,
            dataType: 'xml',
            data: MensajeSoap,
            success: function (data, textStatus, jqXHR) {
                return FuncionExito(data, textStatus, jqXHR);
            },
            error: (FuncionError ? FuncionError : this.ManejadorError)
        });
    };

    /*
    * Descripción: Obtiene el mensaje xml de la peticion SOAP.
    *
    * NombreMetodo: Nombre del método a consultar.
    * Parametros: Parámetros del método en formato JSON.
    * EspacioNombres: Espacio de nombres del servicio web.
    * TecnologiaServicio: Objeto de tipo EnuTecnologiaServicio que describe la tecnología del servicio web.
    *
    * Retorno: Xml que define la petición SOAP.
    */
    function ObtenerMensajeSoap(NombreMetodo, Parametros, EspacioNombres, TecnologiaServicio) {

        var ParametrosXML = '';
        var CadenaParametro = '';
        switch (TecnologiaServicio) {
            case EnuTecnologiaServicio.NET: { break; }
            case EnuTecnologiaServicio.JAVA:
                {
                    CadenaParametro = ' xmlns=""';
                    break;
                }
        }

        for (var Variable in Parametros) {
            ParametrosXML += '<' + Variable + CadenaParametro + '>' + Parametros[Variable] + '</' + Variable + '>';
        }

        var MensajeSoap = '<?xml version="1.0" encoding="utf-8"?>' +
                            '<soap:Envelope xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">' +
	                            '<soap:Body xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">' +
		                            '<' + NombreMetodo + ' xmlns="' + EspacioNombres + '">' +
			                             ParametrosXML +
		                            '</' + NombreMetodo + '>' +
	                            '</soap:Body>' +
                            '</soap:Envelope>';
        return MensajeSoap;
    }

    /*
    * Descripción: Obtiene la url según sea el tipo de tecnología del servicio web.
    *
    * UrlServicio: Url del servicio web.
    * NombreMetodo: Nombre del método del servicio a consultar.
    * TecnologiaServicio: Objeto de tipo EnuTecnologiaServicio que describe la tecnología del servicio web.
    *
    * Retorno: Url para la petición SOAP.
    */
    function ObtenerUrlMetodoServicio(UrlServicio, NombreMetodo, TecnologiaServicio) {
        var UrlMetodoServicio = null;
        UrlServicio = UrlServicio.substring(0, UrlServicio.lastIndexOf('/'));
        switch (TecnologiaServicio) {
            case EnuTecnologiaServicio.NET:
                {
                    UrlMetodoServicio = UrlServicio + '?opt=' + NombreMetodo;
                    break;
                }
            case EnuTecnologiaServicio.JAVA:
                {
                    UrlMetodoServicio = UrlServicio + '?wsdl';
                    break;
                }
        }
        return UrlMetodoServicio;
    }

    /*
    * Descripción: Controla el error de la petición al servicio web.
    *
    * jqXHR: Objeto HTTPRequest que contiene la petición web.
    * textStatus: Describe el tipo de error que ocurrió en la petición web.
    * errorThrown: Descripción del estado HTTP, un ejemplo es "Not Found".
    *
    * Esta función no se ejecuta si se proporciona la FuncionError en el método EjecutarServicio.
    */
    this.ManejadorError = function (jqXHR, textStatus, errorThrown) {
        if (textStatus == 'error') {
            if (errorThrown == 'Unauthorized') {
                MostrarMensaje("Advertencia", "Error1", EnuTipoMensaje.Advertencia, EnuBotonesMensaje.Aceptar, null);
                alert('Se ha alcanzado el tiempo de inactividad!');
            }
            else {
                MostrarMensaje("Advertencia", "Error2", EnuTipoMensaje.Advertencia, EnuBotonesMensaje.Aceptar, null);
                alert('Ha ocurrido un error en el servidor!' + (jqXHR.responseText ? jqXHR.responseText : errorThrown));
            }
        }
    };

    window.EjecutarServicio = this.EjecutarServicio;
}

/***************************************************************************************************************
************************************       ADMINISTRADOR CONTROL        ****************************************
****************************************************************************************************************
* Nombre: AdministradorControl                                                                                 *
* Descripcion: Construye el control indicado.                                                                  *
***************************************************************************************************************/

function AdministradorControl() {
    /*                                                                                  
    * Descripción: Construye el control desde la configuración pasada.
    * NombreControl: Nombre del control a construir.
    * IdInstanciaControl: Id de la instancia a construir, esta instancia esta disponible en el objeto window.
    * IdContenedorControl: Id del elemento contenedor del control que se va a construir.
    * ParametrosControl: Parámetros que se pasarán al constructor del control.
    * ElementosConfiguracion: Elementos que se consultarán del xml de configuración general.
    * RequiereXml: true si se requiere que se pase el Xml de la configuración del control como parámetro al constructor del control, false de lo contrario.
    * RequiereHtml: true si se require que se devuelva e inyecte el código html del control en el IdContenedorControl Indicado, false de lo contrario.
    *
    */
    this.ConstruirControl = function (NombreControl, IdInstanciaControl, IdContenedorControl, ParametrosControl, ElementosConfiguracion, RequiereXml, RequiereHtml) {

        if (!ElementosConfiguracion)
            ElementosConfiguracion = "";

        //Remplaza comillas simples por comillas dobles.
        if (typeof (ParametrosControl) === 'string')
            ParametrosControl = "'" + ParametrosControl.replace(/'/g, "\"") + "'";

        if (typeof (RequiereXml) !== 'boolean') RequiereXml = true;
        if (typeof (RequiereHtml) !== 'boolean') RequiereHtml = true;

        if (!ValidarParametrosControl(NombreControl, IdInstanciaControl, IdContenedorControl, RequiereXml, RequiereHtml)) {
            return;
        }

        if (!RequiereXml && !RequiereHtml) {
            EjecutarConstructor(NombreControl, IdInstanciaControl, null, ParametrosControl, null);
        }

        var UrlServicio = window.UrlConsultorControl + "/ObtenerControl";

        var ParametrosServicio = "{" +
                                 "'Nombre':'" + NombreControl + "'," +
                                 "'RequiereXml':'" + RequiereXml + "'," +
                                 "'RequiereHtml':'" + RequiereHtml + "'," +
                                 "'ElementosConfiguracion':'" + ElementosConfiguracion + "'," +
                                 "'IdAplicacion':'" + window.IdAplicacion + "'" +
                                 "}";

        EjecutarServicio(UrlServicio, ParametrosServicio, TecnologiaServicioConsultorControl,
         window.EspacioNombresConsultorControl, function (Datos, Estado, Respuesta) {

             if (Respuesta.statusText == "OK") {
                 try {
                     var CadenaXml = Respuesta.responseText;
                     var JsonRespuesta = XmlAJson(CadenaXml);

                     var JsonRespuestaElementos = BuscarElementoJSon(JsonRespuesta, 'Codigo', true);
                     if (!JsonRespuestaElementos) JsonRespuestaElementos = BuscarElementoJSon(JsonRespuesta, 'CadenaRespuesta', true);
                     if (!JsonRespuestaElementos) JsonRespuestaElementos = BuscarElementoJSon(JsonRespuesta, 'Lista', true);

                     if (!JsonRespuestaElementos || (JsonRespuestaElementos &&
                                       !(JsonRespuestaElementos.Codigo != undefined && JsonRespuestaElementos.CadenaRespuesta != undefined
                                       && JsonRespuestaElementos.Lista != undefined))) {
                         alert('[' + NombreControl + ']: ' + 'No existe Codigo, CadenaRespuesta y Lista en la respuesta');
                         return;
                     }

                     if (JsonRespuestaElementos.Codigo != "0") {
                         alert('[' + NombreControl + ']: ' + JsonRespuestaElementos.CadenaRespuesta);
                         return;
                     }

                     if (!ValidarElementosControl(NombreControl, IdContenedorControl)) {
                         return;
                     }

                     EjecutarControl(NombreControl, IdInstanciaControl, IdContenedorControl, ParametrosControl, RequiereXml, RequiereHtml, JsonRespuestaElementos);
                 }
                 catch (Exepcion) {
                     alert('[' + NombreControl + ']: ' + Exepcion);
                 }
             }
             else {
                 alert('[' + NombreControl + ']: ' + Respuesta.responseText);
             }
         }, function (Datos, Estado, Respuesta) {
             alert('[' + NombreControl + ']: ' + Estado + " ," + Respuesta + " ," + Datos.responseText);
         });
    };

    /*                                                                                  
    * Descripción: Ejecuta el constructor del control a partir de código Xml(Configuración) y Html del Control. La función inyecta el Html y pasa la configuración 
    *              al constructor del control.
    * NombreControl: Nombre del control a construir.
    * IdInstanciaControl: Id de la instancia a construir, esta instancia esta disponible en el objeto window.
    * IdContenedorControl: Id del elemento contenedor del control que se va a construir.
    * ParametrosControl: Parámetros que se pasarán al constructor del control.
    * RequiereXml: true si se requiere que se pase el Xml de la configuración del control como parámetro al constructor del control, false de lo contrario.
    * RequiereHtml: true si se require que se devuelva e inyecte el código html del control en el IdContenedorControl Indicado, false de lo contrario.
    * JsonRespuestaElementos: Ojbeto de tipo Json que contiene la respuesta del servicio Web.
    *
    */
    function EjecutarControl(NombreControl, IdInstanciaControl, IdContenedorControl, ParametrosControl, RequiereXml, RequiereHtml, JsonRespuestaElementos) {
        var CodigoHtml = '';
        var ConfiguracionControl = null;
        var ConfiguracionAplicacion = null;
        switch (TecnologiaServicioConsultorControl) {
            case EnuTecnologiaServicio.NET:
                {
                    ConfiguracionControl = JsonRespuestaElementos.Lista.string[0];
                    CodigoHtml = JsonRespuestaElementos.Lista.string[1];
                    ConfiguracionAplicacion = JsonRespuestaElementos.Lista.string[2];
                    break;
                }
            case EnuTecnologiaServicio.JAVA:
                {
                    break;
                }
            default:
                {
                    alert('[' + NombreControl + ']: ' + 'Tecología no soportada. Debe ser un valor de la enumeración EnuTecnologiaServicio.');
                    return;
                }
        }

        if (!RequiereXml) ConfiguracionControl = null;

        if (RequiereHtml) {
            var Contenedor = document.getElementById(IdContenedorControl);
            CodigoHtml = CodigoHtml.replace(/%IdInstancia%/g, IdInstanciaControl);
            Contenedor.innerHTML = CodigoHtml;
        }

        if (typeof (ConfiguracionControl) === 'string')
            ConfiguracionControl = "'" + ConfiguracionControl.replace(/'/g, "\"") + "'";

        if (typeof (ConfiguracionControl) === 'string') {
            ConfiguracionAplicacion = "'" + ConfiguracionAplicacion.replace(/'/g, "\"") + "'";
            //Remplaza los saltos de linea por espacios vacios.
            ConfiguracionAplicacion = ConfiguracionAplicacion.replace(/(\r\n|\n|\r)/gm, "");
        }
        EjecutarConstructor(NombreControl, IdInstanciaControl, ConfiguracionControl, ParametrosControl, ConfiguracionAplicacion);
    }

    /*                                                                                  
    * Descripción: Ejecuta el constructor del control con los parámetros pasados.
    * NombreControl: Nombre del constructor del control a ejecutar.
    * IdInstanciaControl: Id de la instancia a construir, esta instancia esta disponible en el objeto window.
    * ConfiguracionControl: Configuración del control en una cadena Xml.
    * ParametrosControl: Parámetros que se pasarán al constructor del control.
    * ConfiguracionAplicacion: Configuración de la aplicación en caso requerido, esta configuración es resultado de 
    *                          la consulta al Xml de la aplicación mediante XPath.
    *
    */
    function EjecutarConstructor(NombreControl, IdInstanciaControl, ConfiguracionControl, ParametrosControl, ConfiguracionAplicacion) {
        var Funcion = "new " + NombreControl + "(" + ConfiguracionControl + ",'" + IdInstanciaControl + "'," + ParametrosControl + "," + ConfiguracionAplicacion + ");";
        var Instancia = eval(Funcion);
        window[IdInstanciaControl] = Instancia;
        eval(IdInstanciaControl + '.' + NombreControl + '();');
    }

    /*                                                                                  
    * Descripción: Valida la existencia de los parámetros.
    * NombreConstructor: Nombre del constructor(funcion) que se validará.
    * IdContenedorControl: Id del control a verificar.
    *
    * Retorno: true si existen los elementos pasados como parámetros. false en caso contrario.
    */
    function ValidarElementosControl(NombreConstructor, IdContenedorControl) {
        var Contenedor = document.getElementById(IdContenedorControl);
        if (!Contenedor) {
            alert('[' + NombreConstructor + ']: ' + 'El contenedor ' + IdContenedorControl + ' no existe.');
            return false;
        }

        try {
            if (typeof (eval(NombreConstructor)) != 'function') {
                alert('[' + NombreConstructor + ']: ' + 'El constructor ' + NombreConstructor + ' no existe.');
                return false;
            }
        }
        catch (ErrorElementosControl) {
            alert('[' + NombreConstructor + ']: ' + 'El constructor ' + NombreConstructor + ' no existe. ' + ErrorElementosControl);
            return false;

        }

        return true;
    }
    /*                                                                                  
    * Descripción: Valida los parámetros del control.
    * NombreControl: Nombre del control a validar.
    * IdInstanciaControl: Id instancia del control a validar.
    * RequiereXml: Valor boolean a validar.
    * RequiereHtml: Valor boolean a validar.
    *
    * Retorno: true si los elementos son válidos. false en caso contrario.
    */
    function ValidarParametrosControl(NombreControl, IdInstanciaControl, IdContenedorControl, RequiereXml, RequiereHtml) {
        if (typeof (NombreControl) !== 'string' || $.trim(NombreControl).length == 0) {
            alert('El nombre del control no es válido.');
            return false;
        }

        if (typeof (IdInstanciaControl) !== 'string' || $.trim(IdInstanciaControl).length == 0) {
            alert('[' + NombreControl + ']: ' + 'El IdInstanciaControl del control no es válido.');
            return false;
        }

        if (typeof (IdContenedorControl) !== 'string' || $.trim(IdContenedorControl).length == 0) {
            alert('[' + NombreControl + ']: ' + 'El IdContenedorControl no es válido.');
            return false;
        }

        if (!window.UrlConsultorControl) {
            alert('[' + NombreControl + ']: ' + 'La variable UrlConsultorControl no esta definida.');
            return false;
        }

        if (!window.IdAplicacion) {
            alert('[' + NombreControl + ']: ' + 'La variable IdAplicacion no esta definida.');
            return false;
        }

        if (!window.EspacioNombresConsultorControl) {
            alert('[' + NombreControl + ']: ' + 'La variable EspacioNombresConsultorControl no esta definida.');
            return false;
        }

        if (!window.TecnologiaServicioConsultorControl) {
            alert('[' + NombreControl + ']: ' + 'La variable TecnologiaServicioConsultorControl no esta definida.');
            return false;
        }

        if (typeof (RequiereXml) !== "boolean") {
            alert('[' + NombreControl + ']: ' + 'El parámetro RequiereXml debe de ser de tipo boolean.');
            return false;
        }

        if (typeof (RequiereHtml) !== "boolean") {
            alert('[' + NombreControl + ']: ' + 'El parámetro RequiereHtml debe de ser de tipo boolean.');
            return false;
        }
        return true;
    }

    window.ConstruirControl = this.ConstruirControl;
}

/*
* Inicialización de objetos(variables, funciones, etc.) de uso global.
*/

var Mensaje = new AdministradorMensaje();
var IndicadorEspera = new AdministradorIndicadorEspera();
var Servicio = new AdministradorServicio();
var Control = new AdministradorControl();