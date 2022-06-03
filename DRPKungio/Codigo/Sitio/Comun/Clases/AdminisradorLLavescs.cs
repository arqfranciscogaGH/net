using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Sitio.Models;

namespace Sitio.Comun.Clases
{
    public class AdminisradorLLaves
    {
        Modelo db = new Modelo();
        public static String llaveActual=String.Empty;
        public static String generar(int id, String llaveEntrada)
        {
            Guid llave = Guid.NewGuid();
            Modelo db = new Modelo();
            if(llaveEntrada == "prueba")
                llaveActual= llaveEntrada;
            //else
            //{
            //    Suscripcion suscripcion = db.Suscripcion.Find(id);
            //    if (suscripcion != null && suscripcion.llave!="")
            //        llaveActual = id + llave.ToString() + suscripcion.llave;
            //    else
            //        llaveActual = "";
            //}
            return llaveActual;

        }
        public static bool validar(String llaveEntrada)
        {
            Modelo db = new Modelo();
            Suscripcion suscripcion = db.Suscripcion.Find(1);

            bool resultado = false;

            if (llaveEntrada == "prueba")
            {
                //    if (suscripcion != null && suscripcion.llave != String.Empty)
                resultado = true;
            }
            //else
            //{
                //    //if (llaveEntrada != null && llaveEntrada != String.Empty)
                //    //    resultado = llaveActual == llaveEntrada;
                //    //if  ( llaveActual.Contains(suscripcion.llave) )

                //    if ( (suscripcion != null && suscripcion.llave != "" )  )
                //    {
                //        if (llaveActual != String.Empty && llaveActual.Contains(suscripcion.llave))
                //            resultado = true;
                //    }
            //}
      
            return resultado;
        }
    }
}