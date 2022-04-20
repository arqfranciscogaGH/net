
  // jquery
  // OwlCarousel2-2.3.4

$(document).ready(function(){
    $("#ContenedorCarruselInicio").owlCarousel(
    {
        autoplay:1000,
        slideSpeed:300,
        paginationSpeed:4000,
        singleItem:true,
        loop:true,
        margin:10,
        nav:false,
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

  const api_url="http://api.kungio.com/api/Clientes/prueba";
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

let headers = new Headers();

headers.append('Content-Type', 'application/json');
headers.append('Accept', 'application/json');

headers.append('Access-Control-Allow-Origin', 'http://127.0.0.1:5500/');
headers.append('Access-Control-Allow-Credentials', 'true');

headers.append('GET', 'POST', 'OPTIONS');

// headers.append('Authorization', 'Basic ' + base64.encode(username + ":" + password));

fetch(api_url, {
    //mode: 'no-cors',
    credentials: 'include',
    method: 'GET',
    headers: headers
  })

fetch(api_url
    , {
        //mode: 'no-cors',
        credentials: 'include',
        method: 'POST',
        headers: headers
      })
.then( (response)=> response.json() )
.then( (clientes) => {
    const html= clientes.map( (cliente) => `<li> ${cliente.nombre} </li>` );
    resultadoConsulta.innerHTML= `<ul> ${html} </ul>` ;
  
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