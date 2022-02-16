/*Clase para la Geocodificacion*/
function AdministradorGeoCodificador() {
    this.GeoCodificadorInterno = new google.maps.Geocoder();

    this.Inicializar = function () {
        this.ObtenerCadenaPorPunto(new google.maps.LatLng(0,0), function (resultados, estatus) { });
        this.ObtenerDireccionPorPunto(new google.maps.LatLng(0, 0), function (resultados, estatus) { });
        this.ObtenerDireccionPorPalabras(new google.maps.LatLng(0, 0), function (resultados, estatus) { });
        this.ObtenerCadenaPorPalabras(new google.maps.LatLng(0, 0), function (resultados, estatus) { });
    }

    this.ObtenerCadenaPorPunto = function (latlng, funcionRetorno) {
        this.GeoCodificadorInterno.geocode({ location: latlng }, function (resultados, estatus) {
            var CadenaResultado = null;
            if (estatus == google.maps.GeocoderStatus.OK) {
                if (resultados != null) {
                    CadenaResultado = resultados[0].formatted_address;
                    funcionRetorno(estatus, 'exito', CadenaResultado);
                }
                else {
                    funcionRetorno(estatus, 'sin resultados', '');
                }
            }
            else {
                funcionRetorno(estatus, 'error', '');
            }
        });
    }

    this.ObtenerDireccionPorPunto = function (latlng, funcionRetorno) {
        this.GeoCodificadorInterno.geocode({ location: latlng }, function (resultados, estatus) {
            var _Direccion = new Direccion();
            if (estatus == google.maps.GeocoderStatus.OK) {
                if (resultados != null) {
                    for (var i = 0; i < resultados[0].address_components.length; i++) {
                        if (resultados[0].address_components[i].types[0] == 'street_number') {
                            _Direccion.Numero = resultados[0].address_components[i].long_name;
                        }

                        if (resultados[0].address_components[i].types[0] == 'route') {
                            _Direccion.Calle = resultados[0].address_components[i].long_name;
                        }

                        if (resultados[0].address_components[i].types[0] == 'neighborhood') {
                            _Direccion.Colonia = resultados[0].address_components[i].long_name;
                        }

                        if (resultados[0].address_components[i].types[0] == 'locality') {
                            direccion.Localidad = resultados[0].address_components[i].long_name;
                            _Direccion.Municipio = resultados[0].address_components[i].long_name;
                        }

                        if (resultados[0].address_components[i].types[0] == 'administrative_area_level_1') {
                            _Direccion.Estado = resultados[0].address_components[i].long_name;
                        }

                        if (resultados[0].address_components[i].types[0] == 'country') {
                            _Direccion.Pais = resultados[0].address_components[i].long_name;
                        }
                    }
                    _Direccion.DireccionFormateada = resultados[0].formatted_address;
                    _Direccion.X = resultados[0].geometry.location.lng();
                    _Direccion.Y = resultados[0].geometry.location.lat();

                    funcionRetorno(estatus, 'exito', _Direccion);
                }
                else {
                    funcionRetorno(estatus, 'sin resultados', null);
                }
            }
            else {
                funcionRetorno(estatus, 'error', null);
            }
        });
    }

    this.ObtenerDireccionPorPalabras = function (palabra, funcionRetorno) {
        this.GeoCodificadorInterno.Geocode({ address: palabra }, function (resultados, estatus) {
            var _ArregloDireccion = [];
            if (estatus == google.maps.GeocoderStatus.OK) {
                if (resultados != null) {
                    for (var i = 0; i < results.length; i++) {
                        var direccion = new Direccion();
                        for (var j = 0; j < resultados[i].address_components.length; j++) {
                            if (resultados[i].address_components[j].types[0] == 'street_number') {
                                direccion.Numero = resultados[i].address_components[j].long_name;
                            }

                            if (resultados[i].address_components[j].types[0] == 'route') {
                                direccion.Calle = resultados[i].address_components[j].long_name;
                            }

                            if (resultados[i].address_components[j].types[0] == 'neighborhood') {
                                direccion.Colonia = resultados[i].address_components[j].long_name;
                            }

                            if (resultados[i].address_components[j].types[0] == 'locality') {
                                direccion.Localidad = resultados[i].address_components[j].long_name;
                                direccion.Municipio = resultados[i].address_components[j].long_name;
                            }

                            if (resultados[i].address_components[j].types[0] == 'administrative_area_level_1') {
                                direccion.Estado = resultados[i].address_components[j].long_name;
                            }

                            if (resultados[i].address_components[j].types[0] == 'country') {
                                direccion.Pais = resultados[i].address_components[j].long_name;
                            }
                        }

                        direccion.DireccionFormateada = resultados[0].formatted_address;
                        direccion.X = resultados[i].geometry.location.lng();
                        direccion.Y = resultados[i].geometry.location.lat();

                        _ArregloDireccion.push(direccion);
                    }
                    funcionRetorno(estatus, 'exito', _ArregloDireccion);
                }
                else {
                    funcionRetorno(estatus, 'sin resultados', null);
                }
            }
            else {
                funcionRetorno(estatus, 'error', null);
            }
        });
    }

    this.ObtenerCadenaPorPalabras = function (palabra, funcionRetorno) {
        this.GeoCodificadorInterno.geocode({ address: palabra }, function (resultados, estatus) {
            var _ArregloResultados = [];
            if (estatus == google.maps.GeocoderStatus.OK) {
                if (resultados != null) {
                    for (var i = 0; i < resultados.length; i++) {
                        _ArregloResultados.push(resultados[i].formatted_address);
                    }
                    funcionRetorno(estatus, 'exito', _ArregloResultados);
                }
                else {
                    funcionRetorno(estatus, 'sin resultados', null);
                }
            }
            else {
                funcionRetorno(estatus, 'error', null);
            }
        });
    }

    this.ObtenerCadenaPorPalabrasAutoComplete = function (palabra, funcionRetorno) {
        this.GeoCodificadorInterno.geocode({ address: palabra }, function (resultados, estatus) {
            var _ArregloResultados = [];
            if (estatus == google.maps.GeocoderStatus.OK) {
                if (resultados != null) {
                    for (var i = 0; i < resultados.length; i++) {
                        var direccion = new Direccion();
                        for (var j = 0; j < resultados[i].address_components.length; j++) {
                            if (resultados[i].address_components[j].types[0] == 'street_number') {
                                direccion.Numero = resultados[i].address_components[j].long_name;
                            }

                            if (resultados[i].address_components[j].types[0] == 'route') {
                                direccion.Calle = resultados[i].address_components[j].long_name;
                            }

                            if (resultados[i].address_components[j].types[0] == 'neighborhood') {
                                direccion.Colonia = resultados[i].address_components[j].long_name;
                            }

                            if (resultados[i].address_components[j].types[0] == 'locality') {
                                direccion.Localidad = resultados[i].address_components[j].long_name;
                                direccion.Municipio = resultados[i].address_components[j].long_name;
                            }

                            if (resultados[i].address_components[j].types[0] == 'administrative_area_level_1') {
                                direccion.Estado = resultados[i].address_components[j].long_name;
                            }

                            if (resultados[i].address_components[j].types[0] == 'country') {
                                direccion.Pais = resultados[i].address_components[j].long_name;
                            }
                        }

                        direccion.DireccionFormateada = resultados[0].formatted_address;
                        direccion.X = resultados[i].geometry.location.lng();
                        direccion.Y = resultados[i].geometry.location.lat();

                        _ArregloResultados.push({ label: resultados[i].formatted_address, direccion: direccion });
                    }
                    funcionRetorno(estatus, 'exito', _ArregloResultados);
                }
                else {
                    funcionRetorno(estatus, 'sin resultados', null);
                }
            }
            else {
                funcionRetorno(estatus, 'error', null);
            }
        });
    }
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
            cadenaDireccion += ((cadenaDireccion.length != 0) ? (', ' + this.Colonia) : this.Colonia);
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