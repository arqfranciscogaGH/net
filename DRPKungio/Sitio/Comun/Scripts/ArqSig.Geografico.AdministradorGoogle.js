//********************** Clase Geocodificador **************************
var latLongTemp = null;
var GeocodificadorSingleton = null;
var cadenaDireccionTemp = null;

function Geocodificador() {
    this.GeocodificadorInterno = new google.maps.Geocoder();

    this.Inicializar = function () {
        var arreglo = [];
        var latlngIni;
        this.BuscaDireccionCadena('', arreglo);
        this.BuscaDireccionCadenaClase('', arreglo);
        this.BuscaDireccionClase(new Direccion(), arreglo);
        this.ObtenXYCadena('', latlngIni);
        this.ObtenXYClase(new Direccion(), latlngIni);
        this.ObtenDireccionCadena(0, 0, arreglo);
        this.ObtenDireccionClase(0, 0, arreglo);
    }

    this.BuscaDireccionCadena = function (cadenaDireccion, arregloDirecciones) {

        this.GeocodificadorInterno.geocode({ 'address': cadenaDireccion }, function (results, status) {
            if (status = google.maps.GeocoderStatus.OK) {

                if (arregloDirecciones.length > 0) {
                    while (arregloDirecciones.length > 0) {
                        arregloDirecciones.pop();
                    }
                }

                if (results == null) {
                    arregloDirecciones = [];
                    google.maps.event.trigger(GeocodificadorSingleton, 'BuscaDireccionCadenaTermino');
                    return;
                }
                for (var i = 0; i < results.length; i++) {
                    arregloDirecciones.push(results[i].formatted_address);

                }
                google.maps.event.trigger(GeocodificadorSingleton, 'BuscaDireccionCadenaTermino');

            }
            else {
                arregloDirecciones = null;
                google.maps.event.trigger(GeocodificadorSingleton, 'BuscaDireccionCadenaTermino');
            }
        });
    }

    this.BuscaDireccionCadenaFuncion = function (cadenaDireccion, funcionRetorno) {
        var arregloDirecciones = [];
        this.GeocodificadorInterno.geocode({ 'address': cadenaDireccion }, function (results, status) {
            if (status = google.maps.GeocoderStatus.OK) {
                if (results == null) {
                    arregloDirecciones = [];
                    funcionRetorno(arregloDirecciones, status);
                    return;
                }
                for (var i = 0; i < results.length; i++) {
                    arregloDirecciones.push({label:results[i].formatted_address, direccion: results[i].formatted_address, X: results[i].geometry.location.lng(), Y: results[i].geometry.location.lat() });

                }
                funcionRetorno(arregloDirecciones, status);

            }
            else {
                arregloDirecciones = null;
                funcionRetorno(arregloDirecciones, status);
            }
        });
    }

    this.BuscaDireccionCadenaClase = function (cadenaDireccion, arregloClaseDireccion) {

        this.GeocodificadorInterno.geocode({ 'address': cadenaDireccion }, function (results, status) {
            if (status = google.maps.GeocoderStatus.OK) {

                if (arregloClaseDireccion.length > 0) {
                    while (arregloClaseDireccion.length > 0) {
                        arregloClaseDireccion.pop();
                    }

                }

                if (results == null) {
                    arregloDirecciones = [];
                    google.maps.event.trigger(GeocodificadorSingleton, 'BuscaDireccionCadenaClaseTermino');
                    return;
                }
                for (var i = 0; i < results.length; i++) {
                    var direccion = new Direccion();
                    for (var j = 0; j < results[i].address_components.length; j++) {
                        if (results[i].address_components[j].types[0] == 'street_number') {
                            direccion.Numero = results[i].address_components[j].long_name;
                        }

                        if (results[i].address_components[j].types[0] == 'route') {
                            direccion.Calle = results[i].address_components[j].long_name;
                        }

                        if (results[i].address_components[j].types[0] == 'neighborhood') {
                            direccion.Colonia = results[i].address_components[j].long_name;
                        }

                        if (results[i].address_components[j].types[0] == 'locality') {
                            direccion.Localidad = results[i].address_components[j].long_name;
                            direccion.Municipio = results[i].address_components[j].long_name;
                        }

                        if (results[i].address_components[j].types[0] == 'administrative_area_level_1') {
                            direccion.Estado = results[i].address_components[j].long_name;
                        }

                        if (results[i].address_components[j].types[0] == 'country') {
                            direccion.Pais = results[i].address_components[j].long_name;
                        }
                    }

                    direccion.DireccionFormateada = results[0].formatted_address;
                    direccion.X = results[i].geometry.location.lng();
                    direccion.Y = results[i].geometry.location.lat();

                    arregloClaseDireccion.push(direccion);
                }
                google.maps.event.trigger(GeocodificadorSingleton, 'BuscaDireccionCadenaClaseTermino');
            }
            else {
                arregloDirecciones = null;
                google.maps.event.trigger(GeocodificadorSingleton, 'BuscaDireccionCadenaClaseTermino');
            }
        });

    }

    this.BuscaDireccionClase = function (claseDireccion, arregloClaseDireccion) {
        this.GeocodificadorInterno.geocode({ 'address': claseDireccion.Acadena() }, function (results, status) {
            if (status = google.maps.GeocoderStatus.OK) {
                if (arregloClaseDireccion.length > 0) {
                    while (arregloClaseDireccion.length > 0) {
                        arregloClaseDireccion.pop();
                    }
                }
                if (results == null) {
                    arregloDirecciones = [];
                    google.maps.event.trigger(GeocodificadorSingleton, 'BuscaDireccionClaseTermino');
                    return;
                }
                for (var i = 0; i < results.length; i++) {
                    var direccion = new Direccion();
                    for (var j = 0; j < results[i].address_components.length; j++) {
                        if (results[i].address_components[j].types[0] == 'street_number') {
                            direccion.Numero = results[i].address_components[j].long_name;
                        }

                        if (results[i].address_components[j].types[0] == 'route') {
                            direccion.Calle = results[i].address_components[j].long_name;
                        }

                        if (results[i].address_components[j].types[0] == 'neighborhood') {
                            direccion.Colonia = results[i].address_components[j].long_name;
                        }

                        if (results[i].address_components[j].types[0] == 'locality') {
                            direccion.Localidad = results[i].address_components[j].long_name;
                            direccion.Municipio = results[i].address_components[j].long_name;
                        }

                        if (results[i].address_components[j].types[0] == 'administrative_area_level_1') {
                            direccion.Estado = results[i].address_components[j].long_name;
                        }

                        if (results[i].address_components[j].types[0] == 'country') {
                            direccion.Pais = results[i].address_components[j].long_name;
                        }
                    }

                    direccion.DireccionFormateada = result[0].formatted_address;
                    direccion.X = results[i].geometry.location.lng();
                    direccion.Y = results[i].geometry.location.lat();

                    arregloClaseDireccion.push(direccion);
                }
                google.maps.event.trigger(GeocodificadorSingleton, 'BuscaDireccionClaseTermino');
            }
            else {
                arregloDirecciones = null;
                google.maps.event.trigger(GeocodificadorSingleton, 'BuscaDireccionClaseTermino');
            }
        });
    }

    this.ObtenXYCadena = function (cadenaDireccion, xy) {
        this.GeocodificadorInterno.geocode({ 'address': cadenaDireccion }, function (result, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                if (xy.length > 0) {
                    while (xy.length > 0) {
                        xy.pop();
                    }

                }
                if (results == null) {
                    xy = [];
                    google.maps.event.trigger(GeocodificadorSingleton, 'ObtenXYCadenaTermino');
                    return;
                }
                for (var i = 0; i < result.length; i++) {
                    xy.push(result[i].geometry.location);
                }

                google.maps.event.trigger(GeocodificadorSingleton, 'ObtenXYCadenaTermino');
            }
            else {
                xy = null;
                google.maps.event.trigger(GeocodificadorSingleton, 'ObtenXYCadenaTermino');
            }

        });


    }

    this.ObtenXYClase = function (claseDireccion, xy) {
        this.GeocodificadorInterno.geocode({ 'address': claseDireccion.Acadena() }, function (result, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                if (xy.length > 0) {
                    while (xy.length > 0) {
                        xy.pop();
                    }
                }

                if (results == null) {
                    xy = [];
                    google.maps.event.trigger(GeocodificadorSingleton, 'ObtenXYClaseTermino');
                    return;
                }
                for (var i = 0; i < result.length; i++) {
                    xy.push(result[0].geometry.location);
                }

                google.maps.event.trigger(GeocodificadorSingleton, 'ObtenXYClaseTermino');
            }
            else {
                xy = null;
                google.maps.event.trigger(GeocodificadorSingleton, 'ObtenXYClaseTermino');
            }
        });
    }


    this.ObtenDireccionCadena = function (latlng, arregloDireccion) {

        this.GeocodificadorInterno.geocode({ 'location': latlng }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                if (results == null) {
                    arregloDireccion = [];
                    google.maps.event.trigger(GeocodificadorSingleton, 'ObtenDireccionCadenaTermino');
                    return;
                }
                if (arregloDireccion.length > 0) {
                    arregloDireccion[0] = results[0].formatted_address;
                    google.maps.event.trigger(GeocodificadorSingleton, 'ObtenDireccionCadenaTermino');
                }
                else {
                    arregloDireccion.push(results[0].formatted_address);
                    google.maps.event.trigger(GeocodificadorSingleton, 'ObtenDireccionCadenaTermino');
                }

            }
            else {
                arregloDireccion = null;
                google.maps.event.trigger(GeocodificadorSingleton, 'ObtenDireccionCadenaTermino');
            }
        });


    }

    this.ObtenDireccionClase = function (latlng, arregloClaseDireccion) {

        this.GeocodificadorInterno.geocode({ 'location': latlng }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                var direccion = new Direccion();
                if (results == null) {
                    arregloClaseDireccion = [];
                    google.maps.event.trigger(GeocodificadorSingleton, 'ObtenDireccionClaseTermino');
                    return;
                }
                var existeSubLocalidad = false;
                for (var j = 0; j < results[0].address_components.length; j++) {
                    if (results[0].address_components[j].types[0] == 'street_number') {
                        direccion.Numero = results[0].address_components[j].long_name;
                    }

                    if (results[0].address_components[j].types[0] == 'route') {
                        direccion.Calle = results[0].address_components[j].long_name;
                    }

                    if (results[0].address_components[j].types[0] == 'neighborhood') {
                        direccion.Colonia = results[0].address_components[j].long_name;
                    }


                    if (results[0].address_components[j].types[0] == 'sublocality') {
                        direccion.Localidad = results[0].address_components[j].long_name;
                        existeSubLocalidad = true;
                    }

                    if (results[0].address_components[j].types[0] == 'locality') {
                        if (existeSubLocalidad == true) {
                            direccion.Municipio = results[0].address_components[j].long_name;
                        }
                        else {
                            direccion.Municipio = results[0].address_components[j].long_name;
                            direccion.Localidad = results[0].address_components[j].long_name;
                        }
                    }


                    if (results[0].address_components[j].types[0] == 'administrative_area_level_1') {
                        direccion.Estado = results[0].address_components[j].long_name;
                    }

                    if (results[0].address_components[j].types[0] == 'country') {
                        direccion.Pais = results[0].address_components[j].long_name;
                    }
                }

                direccion.DireccionFormateada = results[0].formatted_address;
                direccion.X = results[0].geometry.location.lng();
                direccion.Y = results[0].geometry.location.lat();

                if (arregloClaseDireccion.length > 0) {
                    arregloClaseDireccion[0] = direccion;
                    google.maps.event.trigger(GeocodificadorSingleton, 'ObtenDireccionClaseTermino');
                }
                else {
                    arregloClaseDireccion.push(direccion);
                    google.maps.event.trigger(GeocodificadorSingleton, 'ObtenDireccionClaseTermino');
                }
            }
            else {
                arregloClaseDireccion = null;
                google.maps.event.trigger(GeocodificadorSingleton, 'ObtenDireccionClaseTermino');
            }
        });
    }
}

ObtenInstanciaGeocodificador = function () {
    if (GeocodificadorSingleton == null) {
        GeocodificadorSingleton = new Geocodificador();
        GeocodificadorSingleton.Inicializar();
    }

    return GeocodificadorSingleton;
}

//*******************Clase Direccion****************************
function Direccion() {
    var direccion = null;

    this.Pais = null;
    this.Estado = null;
    this.Municipio = null;
    this.Localidad = null;
    this.Colonia = null;
    this.Calle = null;
    this.DireccionFormateada = null;
    
    this.X = null;
    this.Y = null;

    this.Numero = null;

    this.Acadena = function () {
        var cadenaDireccion = '';
        if (this.Calle != null && this.Calle != 'undefined') {
            cadenaDireccion += this.Calle;
        }

        if (this.Numero != null && this.Numero != 'undefined') {
            cadenaDireccion += ' ' + this.Numero;
        }

        if (this.Colonia != null && this.Colonia != 'undefined') {
            cadenaDireccion += ((cadenaDireccion.length!=0) ?(', ' + this.Colonia) : this.Colonia);
        }

        if (this.Localidad != null && this.Localidad != 'undefined') {
            cadenaDireccion += ((cadenaDireccion.length != 0) ? (', ' + this.Localidad) : this.Localidad);
        }

        if (this.Municipio != null && this.Municipio != 'undefined') {
            cadenaDireccion += ((cadenaDireccion.length != 0) ? (', ' + this.Municipio) : this.Municipio);
        }

        if (this.Estado != null && this.Estado != 'undefined') {
            cadenaDireccion += ((cadenaDireccion.length != 0) ? (', ' + this.Estado) : this.Estado);
        }

        return cadenaDireccion;
    }
}