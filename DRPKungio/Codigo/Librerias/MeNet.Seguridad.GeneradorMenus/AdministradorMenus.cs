using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using System.Data.Common;
using System.Data.SqlClient;

using MeNet.Nucleo.Contexto;
using MeNet.Nucleo.Negocio;
using MeNet.Nucleo.Modelo;
using DRP.Modelo;

namespace MeNet.Seguridad.GeneradorMenus
{
    public  class AdministradorMenus : AdministradorNegocioGeneral
    {
        private ModeloSistema _contexto;
        public AdministradorMenus()
        {
           _contexto = (ModeloSistema)AdministradorContexto.Iniciar<ModeloSistema>();
            // se asigna contexto a clase base
            this.Contexto = _contexto;
        }

        public Menu ObtenerMenu(Menu entidad)
        {
            return this.Obtener<Menu>(s => s.IdMenu == entidad.IdMenu);
        }
        public List<DRP.Modelo.Menu> ObtenerMenus(DRP.Modelo.Menu entidad)
        {
            return this.Consultar<DRP.Modelo.Menu>(s => s.IdModulo == entidad.IdModulo).ToList();
        }
        public List<OpcionMenu> ObtenerOpcionesMenu(CapOpcionMenu entidad)
        {
            return this.Consultar<OpcionMenu>(s => s.IdMenu == entidad.IdMenu).ToList();
        }
        public CapOpcionMenu ObtenerOpcionMenu(CapOpcionMenu capentidad)
        {

            OpcionMenu entidad = this.Obtener<OpcionMenu>(s => s.IdOpcionMenu == capentidad.IdOpcionMenu);
            if (capentidad != null && entidad != null)
            {
                capentidad.Activo = entidad.Activo;
                capentidad.Ayuda = entidad.Ayuda;
                capentidad.Caracteristicas = entidad.Caracteristicas;
                capentidad.Color = entidad.Color;
                capentidad.Comando = entidad.Comando;
                capentidad.IdMenu = entidad.IdMenu;
                capentidad.IdPrivilegio = entidad.IdPrivilegio;
                capentidad.Imagen = entidad.Imagen;
                capentidad.Orden = entidad.Orden;
                capentidad.Parametros = entidad.Parametros;
                capentidad.TipoComando = entidad.TipoComando;
                capentidad.Titulo = entidad.Titulo;
                OpcionMenuIdioma opcionMenuIdioma = this.Obtener<OpcionMenuIdioma>(s => s.IdOpcionMenu == entidad.IdOpcionMenu && s.IdIdioma == (int)capentidad.IdIdiomaActual);
                if (opcionMenuIdioma != null)
                {
                    if (capentidad.IdIdiomaActual!= capentidad.IdIdiomaPorDefecto)
                        capentidad.Titulo = opcionMenuIdioma.Titulo;
                }
            }
            return capentidad;
        }

        public OpcionMenu Guardar(CapOpcionMenu capentidad, OpcionMenu entidad)
        {
            entidad.Activo = capentidad.Activo;
            entidad.Ayuda = capentidad.Ayuda;
            entidad.Caracteristicas = capentidad.Caracteristicas;
            entidad.Color = capentidad.Color;
            entidad.Comando = capentidad.Comando;
            entidad.IdMenu = capentidad.IdMenu;
            entidad.IdPrivilegio = capentidad.IdPrivilegio;
            entidad.Imagen = capentidad.Imagen;
            entidad.Orden = capentidad.Orden;
            entidad.Parametros = capentidad.Parametros;
            entidad.TipoComando = capentidad.TipoComando;
            if (capentidad.IdIdiomaActual == capentidad.IdIdiomaPorDefecto)
                entidad.Titulo = capentidad.Titulo;
            return entidad;
        }
        public CapOpcionMenu AgregarOpcionMenu(CapOpcionMenu capentidad)
        {
            OpcionMenu entidad = this.Obtener<OpcionMenu>(s => s.IdOpcionMenu == capentidad.IdOpcionMenu );
            if (entidad == null )
            {
                entidad = new OpcionMenu();
                entidad=Guardar(capentidad, entidad);
                Agregar<OpcionMenu>((OpcionMenu)entidad);
                _contexto.SaveChanges();
                capentidad.IdOpcionMenu = entidad.IdOpcionMenu;
            }
            OpcionMenuIdioma opcionMenuIdioma = this.Obtener<OpcionMenuIdioma>(s => s.IdOpcionMenu == entidad.IdOpcionMenu && s.IdIdioma == capentidad.IdIdiomaActual);
            if (opcionMenuIdioma == null)
            {
                opcionMenuIdioma = new OpcionMenuIdioma();
                opcionMenuIdioma.IdOpcionMenu = entidad.IdOpcionMenu;
                opcionMenuIdioma.Titulo = capentidad.Titulo;
                opcionMenuIdioma.Ayuda = capentidad.Ayuda;
                opcionMenuIdioma.IdIdioma = (int)capentidad.IdIdiomaActual;
                Agregar<OpcionMenuIdioma>((OpcionMenuIdioma)opcionMenuIdioma);
            }
            return capentidad;
        }
        public void ActualizarOpcionMenu(CapOpcionMenu capentidad)
        {
            OpcionMenu opcionMenu;
            OpcionMenuIdioma opcionMenuIdioma;
            opcionMenuIdioma = this.Obtener<OpcionMenuIdioma>(s => s.IdOpcionMenu == capentidad.IdOpcionMenu && s.IdIdioma== capentidad.IdIdiomaActual);
            if  (opcionMenuIdioma!= null )
            {
                opcionMenuIdioma.Titulo = capentidad.Titulo;
                opcionMenuIdioma.Ayuda = capentidad.Ayuda;
                Actualizar<OpcionMenuIdioma>((OpcionMenuIdioma)opcionMenuIdioma);
            }
             else
            {
                opcionMenuIdioma = new OpcionMenuIdioma();
                opcionMenuIdioma.IdOpcionMenu = capentidad.IdOpcionMenu;
                opcionMenuIdioma.IdIdioma = (int)capentidad.IdIdiomaActual;
                opcionMenuIdioma.Titulo = capentidad.Titulo;
                opcionMenuIdioma.Ayuda = capentidad.Ayuda;
                Agregar<OpcionMenuIdioma>((OpcionMenuIdioma)opcionMenuIdioma);
            }
            OpcionMenu entidad = this.Obtener<OpcionMenu>(s => s.IdOpcionMenu == capentidad.IdOpcionMenu);
            if (entidad != null)
            {

                entidad= Guardar(capentidad, entidad);
                Actualizar<OpcionMenu>((OpcionMenu)entidad);
            }
            //else
            //{
                //opcionMenuIdioma = this.Obtener<OpcionMenuIdioma>(s => s.IdOpcionMenu == entidad.IdOpcionMenu && s.IdIdioma == capentidad.IdIdiomaPorDefecto);

                //if (opcionMenuIdioma != null)
                //{
                    //entidad.Titulo = opcionMenuIdioma.Titulo;
                //    Actualizar<OpcionMenu>((OpcionMenu)entidad);
                //}
            //}
        }
        public void EliminarOpcionMenu(CapOpcionMenu capentidad)
        {
            OpcionMenuIdioma opcionMenuIdioma;
            opcionMenuIdioma = this.Obtener<OpcionMenuIdioma>(s => s.IdOpcionMenu == capentidad.IdOpcionMenu && s.IdIdioma == capentidad.IdIdiomaActual);
            if (opcionMenuIdioma != null)
            {
                Eliminar<OpcionMenuIdioma>((OpcionMenuIdioma)opcionMenuIdioma);
            }
            _contexto.SaveChanges();
            OpcionMenu entidad = this.Obtener<OpcionMenu>(s => s.IdOpcionMenu == capentidad.IdOpcionMenu);
            if (entidad != null)
            {
                OpcionMenuIdioma opcionMenuIdiomaOtro = this.Obtener<OpcionMenuIdioma>(s => s.IdOpcionMenu == capentidad.IdOpcionMenu);
                if  (opcionMenuIdiomaOtro == null)
                     Eliminar<OpcionMenu>((OpcionMenu)entidad);
            }

        }
    }
}
