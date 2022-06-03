
  // jquery
  // OwlCarousel2-2.3.4

$(document).ready(function () {
    //debugger
    $("#ContenedorCarruselInicio").owlCarousel(
    {
        autoplay:1000,
        slideSpeed:300,
        paginationSpeed:4000,
        singleItem:true,
        loop:true,
        margin:10,
        nav: true,
            navText: ["<div class='navegacionCarruselAnterior sl-prev hidden-xs'><i class='navegacionCarruselAnterior fa fa-angle-left fa-3x'></i></div>", "<div class='navegacionCarruselSiguiente sl-next'><i class='navegacionCarruselSiguiente fa fa-angle-right fa-3x'></i></div>"],

        responsive:{
            0:{
                items:1
            },
            600:{
                items:1
            },
            1000:{
                items:1
            }
        }
    });
  });

  const api_url="http://kungio.com/api/Clientes/prueba";
  const  api_url2="http://jsonplaceholder.typicode.com/users";
  // busqueda de informacion
  const productos=[
    {nombre:'platano',precio:10},
    {nombre:'Mango',precio:20},
    {nombre:'Pera',precio:30},
    {nombre:'Manzana',precio:40},
    {nombre:'Limon',precio:50}
]
const textoIngresado=document.querySelector('#textoIngresado');
const botonBuscar=document.querySelector('#botonBuscar');
const resultado=document.querySelector('#resultado');
const resultadoConsulta=document.querySelector('#resultadoConsulta');
const resultadoConsulta2 = document.querySelector('#resultadoConsulta2');

let headers = new Headers();

headers.append('Content-Type', 'application/json');
headers.append('Accept', 'application/json');

headers.append('Access-Control-Allow-Origin', 'http://127.0.0.1:5500/');
headers.append('Access-Control-Allow-Credentials', 'true');

headers.append('GET', 'POST', 'OPTIONS');

// headers.append('Authorization', 'Basic ' + base64.encode(username + ":" + password));

//fetch(api_url2, {
//    //mode: 'no-cors',
//    credentials: 'include',
//    method: 'GET',
//    headers: headers
//  })
//then((response) => { 
//    //response.json()
//});
fetch(api_url
    , {
        //mode: 'no-cors',
        credentials: 'include',
        method: 'POST',
        headers: headers
      })
.then( (clientes) => {
    const html= clientes.map( (cliente) => `<li> ${cliente.nombre} </li>` );
    resultadoConsulta2.innerHTML= `<ul> ${html} </ul>` ;
  
});

//  filtrar
const filtrar =()=>{

    resultado.innerHTML="";
    const filtro=textoIngresado.value.toLowerCase();
    for ( let producto of  productos)
    {
        let nombre=producto.nombre.toLowerCase();
        if(nombre.indexOf(filtro)!==-1)
        {
            resultado.innerHTML+=`<li> ${producto.nombre} </li>`;
            console.log(`<li> ${producto.nombre} </li>`);
        }
    }
    if(resultado.innerHTML==='')
    {
        resultado.innerHTML='<li> No hay información </li> ';
    }

}
//  eventos
botonBuscar.addEventListener('click',filtrar)
textoIngresado.addEventListener('keyup',filtrar)