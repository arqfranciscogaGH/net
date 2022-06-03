using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using MeNet.Nucleo.Sesion;
using MeNet.Nucleo.Contexto;
using MeNet.Nucleo.AdministradorBaseDatos;
using MeNet.Nucleo.Modelo;
using DRP.Modelo;
using MeNet.Nucleo.Configuracion;
using MeNet.Nucleo.Comun;
using MeNet.Nucleo.Negocio;
using MeNet.Seguridad.Administrador;

namespace MeNet.Seguridad.GeneradorMenus
{
    public class GeneradorMenus
    {
        public  DRP.Modelo.Menu MenuPanel = new DRP.Modelo.Menu();
        public List<OpcionMenu> ListaOpciones = new List<OpcionMenu>();
        public Int16 NumeroIdMenu ;

        public string Tipo = string.Empty;
        public string ColorMenu = string.Empty;
        public string ColorMarcoMenu = string.Empty;
        public string TipoMarco = "Marco";

        
        public string TipoMarcoCss = string.Empty;
        public string TipoBotonCss = string.Empty;


        private ModeloSistema _contexto;
        private AdministradorConfiguracion _administradorConfiguracion;
        private List<Configuracion> _listaConfiguracion;
        //public event EventHandler AccionMenu;
        public GeneradorMenus()
        {
            _contexto = AdministradorContexto.Iniciar<ModeloSistema>();
            _administradorConfiguracion = new AdministradorConfiguracion();
            _listaConfiguracion = _administradorConfiguracion.ObtenerConfiguracionAplicacion(1);
        }
        public string GenerarMenu(short idMenu, AdministradorSeguridad administradorSeguridad, int nivel, HtmlGenericControl contenedor, EventHandler accion)
        {
            DRP.Modelo.Menu _menu = new DRP.Modelo.Menu();
            List<OpcionMenu> listaOpciones = new List<OpcionMenu>();
            _menu.IdMenu = idMenu;

            ////Primero obtenemos el menu
            //Menu = ObtenerMenu(Menu);
            _menu = _contexto.Menu.FirstOrDefault(c => c.IdMenu == idMenu);
            ////Obtenemos opciones para el menu
            //this.listaOpciones = ObtenerOpcionMenu(Menu);
            listaOpciones = _contexto.OpcionMenu.Where(c => c.IdMenu == idMenu).OrderBy(o => o.Orden).ToList();
            //listaOpciones = _contexto.OpcionMenu.Where(c => c.IdMenu == idMenu).ToList();
            //listaOpciones.RemoveAll(x => !listaPrivilegios.Exists(w => w == x.IdPrivilegio));

            //Por ultimo generamos el HTML
            string HTML = GenerarMenuHTML(_menu, listaOpciones, administradorSeguridad, nivel,  contenedor, accion);

            return HTML;

        }

        private string GenerarMenuHTML(DRP.Modelo.Menu _menu, List<OpcionMenu> listaOpciones, AdministradorSeguridad administradorSeguridad, int nivel, HtmlGenericControl contenedor, EventHandler  accion)
        {
            string HTML = "";
            string HTMLMenu = string.Empty;
            string Tipo = string.Empty;
            string Color = string.Empty;
            if (_menu != null)
            {
                if (_menu.Tipo != null)
                    Tipo = _menu.Tipo;
                if (_menu.ColorMenu != null)
                    Color = _menu.ColorMenu;

                if (_menu.MenuPrincipal != null && _menu.MenuPrincipal.ToUpper().Contains("S"))
                    HTMLMenu = "<nav id='IdMenuNav'>";
                if (nivel == 0)
                    HTMLMenu += "<div   class='" + Tipo + "'>";

                if (Color == string.Empty)
                    HTMLMenu += " <ul>";
                else
                    HTMLMenu += " <ul class='" + Color + "'>";

                HTMLMenu += GeneraOpcionHtml(_menu, listaOpciones, administradorSeguridad, nivel, contenedor, accion);
                HTMLMenu += "</ul>";

                if (nivel == 0)
                    HTMLMenu += "</div>";
                if (_menu.MenuPrincipal != null && _menu.MenuPrincipal.ToUpper().Contains("S"))
                    HTMLMenu += "</nav>";
            }
            return HTMLMenu;
        }

        //<li class="submenu colorNaranja" >   <a href = "#" >< span class=" icon-hospital ">  </span>Servicios<span class="icon-down-dir "> </span> </a>
        //        <ul class="colorNaranja" >        
        //            <li>  <a href = "Contacto.aspx" >< span class="Icono Contacto ">  </span>cuatro</a></li>
        //            <li>  <a href = "Ejemplos/AbcEntidad.aspx" >< span class=" icon-calendar-empty ">  </span>cico</a></li>
        //            <li>  <a href = "Acceso.aspx" >< span class="Icono Ayuda ">  </span>seis</a></li>
        //        </ul>  
        //</li>

        public void ObtenerMenu(string IdMenu)
        {

             MenuPanel = new DRP.Modelo.Menu();
             ListaOpciones = new List<OpcionMenu>();

            if (IdMenu == string.Empty)
                IdMenu = "1";

            NumeroIdMenu = Int16.Parse(IdMenu);

            MenuPanel = _contexto.Menu.FirstOrDefault(c => c.IdMenu == NumeroIdMenu);

            ListaOpciones = _contexto.OpcionMenu.Where(c => c.IdMenu == NumeroIdMenu).OrderBy(o => o.Orden).ToList();

            if (MenuPanel.ColorMenu != null)
                ColorMenu = MenuPanel.ColorMenu;
            if (MenuPanel.ColorMarco != null)
                ColorMarcoMenu = MenuPanel.ColorMarco;
            if (MenuPanel.Marco != null)
                TipoMarco = MenuPanel.Marco;
            if (MenuPanel.Marco != null)
                TipoMarco = MenuPanel.Marco;
            if (MenuPanel.Tipo!=null)
                 Tipo = MenuPanel.Tipo;

        }
        private string DefinirEstilo( DRP.Modelo.Menu menu, OpcionMenu opcion)
        {
            string Estilo = string.Empty;
            if (menu.Efecto != null && menu.Efecto != string.Empty)
                Estilo = Estilo + " " + menu.Efecto;
            if (menu.Tamano != null && menu.Tamano != string.Empty)
                Estilo = Estilo + " " + menu.Tamano;
            if (menu.Borde != null && menu.Borde != string.Empty)
                Estilo = Estilo + " " + menu.Borde;
            if (menu.Sombra != null && menu.Sombra != string.Empty)
                Estilo = Estilo + " " + menu.Sombra;
            if (opcion.Imagen != null && opcion.Imagen != string.Empty)
                Estilo = Estilo + " " + opcion.Imagen;
            if (opcion.Color != null && opcion.Color != string.Empty)
                Estilo = Estilo + " " + opcion.Color;
            return Estilo;
        }
        public void GenerarMenuEnPanel(Control contenedor, EventHandler accion, AdministradorSeguridad administradorSeguridad, string IdObjeto)
        {
            GeneraOpcionEnPanel(contenedor, accion,administradorSeguridad, IdObjeto, Tipo, MenuPanel, ListaOpciones) ;
        }
        private void GeneraOpcionEnPanel(Control contenedor, EventHandler accion, AdministradorSeguridad administradorSeguridad, string IdObjeto, string Tipo, DRP.Modelo.Menu menu, List<OpcionMenu> listaOpciones)
        {
            foreach (OpcionMenu opcion in listaOpciones)
            {
                string[] CaracteristicasOpcion = Convertidor.ObtenerListaAtributos(opcion.Caracteristicas);
                //if (opcion.IdPrivilegio != null && administradorSeguridad.ValidarExistenciaPrivilegio(opcion.IdPrivilegio.ToString()))
                //{
                    OpcionMenuIdioma opcionIdioma = _contexto.OpcionMenuIdioma.FirstOrDefault(c => c.IdOpcionMenu == opcion.IdOpcionMenu && c.IdIdioma == administradorSeguridad.IdIdioma);
                    if (opcionIdioma != null && opcionIdioma.Titulo != string.Empty)
                        opcion.Titulo = opcionIdioma.Titulo;
                    if (menu.Tipo != null && menu.Tipo.ToUpper().Contains("BOTON"))
                    {
                        if (TipoBotonCss == string.Empty)
                            TipoBotonCss = menu.Tipo;
                        Button boton = new Button();
                        boton.Text = opcion.Titulo;

                        boton.CommandName = opcion.Titulo;
                        boton.ID = IdObjeto + "_" + opcion.IdOpcionMenu + "_" + boton.CommandName;
                        boton.ToolTip = opcion.Titulo;
                        //boton.CssClass = "Boton Redondo ";
                        boton.CssClass = TipoBotonCss + " " + DefinirEstilo(menu, opcion);
                        boton.Click += accion;
                        contenedor.Controls.Add(boton);
                    }
                    else if (menu.Tipo != null && menu.Tipo.ToUpper().Contains("ICONO"))
                    {
                        if (TipoBotonCss == string.Empty)
                            TipoBotonCss = menu.Tipo;
                        LinkButton boton = new LinkButton();
                        if (menu.MostrarTitulo != null && menu.MostrarTitulo.ToUpper().Contains("SI"))
                            boton.Text = opcion.Titulo;
                        boton.CommandName = opcion.Comando != null && opcion.Comando != string.Empty ? opcion.Comando : opcion.Titulo;
                        boton.CommandArgument = opcion.Parametros;
                        boton.ID = IdObjeto + "_" + opcion.IdOpcionMenu + "_" + boton.CommandName;
                        boton.ToolTip = opcion.Titulo;
                        //boton.CssClass = "IconoFuente Ch Sombra " + ColorOpcion + " " + Imagen;
                        boton.CssClass = TipoBotonCss + " " + DefinirEstilo(menu, opcion);

                        boton.Click += accion;
                        contenedor.Controls.Add(boton);

                    }
                //}
            }
        }
        private string GeneraOpcionHtml(DRP.Modelo.Menu _menu, List<OpcionMenu> listaOpciones, AdministradorSeguridad administradorSeguridad,  int nivel, HtmlGenericControl contenedor, EventHandler accion)
        {
            string Titulo = string.Empty;
            string Imagen;
            string Color="colorTema";
            string FuncionMostrarSubMenu = string.Empty;
            string FuncionOcultarSubMenu = string.Empty;
            short IdMenu;
            string FechaActual = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            string[] CaracteristicasOpcion;
            string HtmlOpciones = string.Empty;

            foreach (OpcionMenu opcion in listaOpciones)
            {
                if (opcion.IdPrivilegio != null && ( administradorSeguridad.ValidarExistenciaPrivilegio(opcion.IdPrivilegio.ToString())) )
                {
                    CaracteristicasOpcion = Convertidor.ObtenerListaAtributos(opcion.Caracteristicas);
                    Imagen = opcion.Imagen;
                    Color = "colorTema";
                    string HtmlTitulo = string.Empty;
                    if (opcion.Color != null && opcion.Color != string.Empty)
                        Color = opcion.Color;

                    OpcionMenuIdioma opcionIdioma = _contexto.OpcionMenuIdioma.FirstOrDefault(c => c.IdOpcionMenu == opcion.IdOpcionMenu && c.IdIdioma == administradorSeguridad.IdIdioma);
                    if (opcionIdioma != null && opcionIdioma.Titulo != string.Empty)
                        opcion.Titulo = opcionIdioma.Titulo;

                    Titulo = opcion.Titulo;
                    string HtmlOpcion = string.Empty;
                    string HtmlIcono = string.Empty;
                    string HtmlComando = string.Empty;

                    if (Imagen != string.Empty)
                    {

                        if (nivel == 0)
                            HtmlIcono = "<span><i class='iconoOpcion " + Imagen + "'></i></span>";
                        else if (nivel > 0)
                            HtmlIcono = "<span><i class='" + Imagen + "'></i></span>";
                        //HtmlIcono = "<span> class='" + Imagen + "'></span>";
                    }
                    if (opcion.TipoComando.Equals("SubMenu"))
                    {

                        HtmlOpcion += "<li class='submenu " + Color + "' > <a title='" + Titulo + "' ";
                        string HtmlSubmenu = " <span class='icon-down-dir'> </span> ";
                        IdMenu = short.Parse(opcion.Comando);
                        HtmlComando = "href = '#' >";
                        if (Imagen != string.Empty)
                            HtmlOpcion += HtmlComando + HtmlIcono + Titulo + HtmlSubmenu + "</a>";
                        else
                            HtmlOpcion += HtmlComando + Titulo + HtmlSubmenu + "</a>";

                        HtmlOpcion += GenerarMenu(IdMenu, administradorSeguridad, 1, contenedor, accion);
                        HtmlOpcion += "</li>";
                    }
                    else
                    {
                        if (_menu.Tipo != null && _menu.Tipo.Contains("Mosaico"))
                        {
                            HtmlOpcion += "<li  class='" + Color + "'> <a title='" + Titulo + "' ";
                            HtmlTitulo = "<div>" + Titulo + "</div>";
                        }

                        else
                        {
                            HtmlOpcion += "<li  class='" + Color + "'> <a title='" + Titulo + "' ";
                            HtmlTitulo = Titulo;
                        }
                    }

                    if (opcion.TipoComando.Equals("URL") || opcion.TipoComando.ToUpper().Equals("METODO"))
                    {
                        string Parametros = "";

                        HtmlComando += " href=\"" + ArmarRutaElemento(opcion.Comando) + Parametros + "\">";
                        if (Imagen != string.Empty)
                            HtmlOpcion += HtmlComando + HtmlIcono + HtmlTitulo + " </a></li>";
                        else
                            HtmlOpcion += HtmlComando + HtmlTitulo + " </a></li>";

                    }
                    if (opcion.TipoComando.ToLower().Equals("ventana"))
                    {

                        string Modulo = Convertidor.ObtenerValor(CaracteristicasOpcion, "Modulo");
                        string VentanaNueva = Convertidor.ObtenerValor(CaracteristicasOpcion, "VentanaNueva");

                        if (VentanaNueva == "0")
                            HtmlComando += "<div class='ligaMenuAyuda' onClick='window.open(\"" + ArmarRutaElemento(opcion.Comando) + "?p" + HttpUtility.UrlEncode("=" + FechaActual + "|" + Modulo + "|" + opcion.Parametros) + "\", \"" + (VentanaNueva != "" ? VentanaNueva : "_blank") + "\");' style='cursor: pointer'>" + Titulo + "</div>";
                        else
                        {
                            string Alto = Convertidor.ObtenerValor(CaracteristicasOpcion, "Alto");
                            string Ancho = Convertidor.ObtenerValor(CaracteristicasOpcion, "Ancho");
                            string Ajustable = Convertidor.ObtenerValor(CaracteristicasOpcion, "Ajustable");
                            string Top = Convertidor.ObtenerValor(CaracteristicasOpcion, "Top");
                            string Left = Convertidor.ObtenerValor(CaracteristicasOpcion, "Left");

                            HtmlComando += "<div class='ligaMenuAyuda' onClick='window.open(\"" + ArmarRutaElemento(opcion.Comando) + "?p" + HttpUtility.UrlEncode("=" + FechaActual + "|" + Modulo + "|" + opcion.Parametros) + "\", \"VentanaMenu_" + opcion.IdOpcionMenu + "\", \"location=0,toolbar=0" + (Ancho != "" ? ",width=" + Ancho : ",width=\" + screen.width + \"") + "" + (Alto != "" ? ",height=" + Alto : ",height=\" + screen.height +\"") + "" + (Top != "" ? ",top=" + Top : "") + "" + (Left != "" ? ",left=" + Left : "") + ",directories=0,status=0,menubar=0,scrollbars=0,resizable=" + (Ajustable != "" ? Ajustable : "yes") + ",copyhistory=0\");' style='cursor: pointer' >" + Titulo + "</div>";

                        }
                        HtmlOpcion += " href =" + HtmlComando + " > " + Titulo + " </a></li>";
                    }
                    else if (opcion.TipoComando.Equals("JavaScript"))
                    {
                        HtmlComando += " href=\"#\" onclick=\"" + opcion.Comando + ";\"";
                        HtmlOpcion += HtmlComando + " > " + Titulo + " </a></li>";
                    }
                    else if (opcion.TipoComando.Equals("Exe"))
                    {
                        string FuncionEjecutaEXE = "";
                        FuncionEjecutaEXE += "function EjecutaEXE() { var oShell = new ActiveXObject('Shell.Application');";
                        FuncionEjecutaEXE += "var commandtoRun = '" + opcion.Comando + "'; oShell.ShellExecute(commandtoRun,'','','open','1'); }";
                        HtmlComando += " href=\"#\" onclick=\"javascript:" + FuncionEjecutaEXE + " EjecutaEXE();\">";
                        HtmlOpcion += HtmlComando + " > " + Titulo + " </a></li>";
                    }

                    HtmlOpciones += HtmlOpcion;
                }
            }
            return HtmlOpciones;
        }
        private string GenerarHTMLAnterior(DRP.Modelo.Menu menu, List<OpcionMenu> listaOpciones, List<int> listaPrivilegios)
        {
            string HTML = "";

            string[] Caracteristicas = Convertidor.ObtenerListaAtributos(menu.Caracteristicas);
            string Orientacion = Convertidor.ObtenerValor(Caracteristicas, "Orientacion");
            string Tipo = Convertidor.ObtenerValor(Caracteristicas, "Tipo");
            string Tabla = "<div class='ligaMenuContenedor'><table cellspacing=\"0\" cellpadding=\"0\" border='0' id='" + menu.Titulo + "' >";

            string InicioOpcion = string.Empty;
            string FinalOpcion = string.Empty;
            if (Orientacion.Equals("H"))
            {
                Tabla += "<tr>";
                //InicioOpcion = "<td align='center' class='" + menu.Estilo + "'>";
                FinalOpcion = "</td>";
                Tabla = GeneraOpcionHtmlAnterior(menu, listaOpciones, listaPrivilegios, Tipo, Tabla, InicioOpcion, FinalOpcion);
                HTML += Tabla + "</tr>";
            }
            else if (Orientacion.Equals("V"))
            {
                Tabla += "";
                InicioOpcion = "<tr align='center'><td align='center' class='" + "" + "'>";
                FinalOpcion = "</td></tr>";
                HTML = GeneraOpcionHtmlAnterior(menu, listaOpciones, listaPrivilegios, Tipo, Tabla, InicioOpcion, FinalOpcion);
            }
            HTML += "</table></div>";
            return HTML;
        }



        private string GeneraOpcionHtmlAnterior(DRP.Modelo.Menu menu, List<OpcionMenu> listaOpciones, List<int> listaPrivilegios, string Tipo, string Tabla, string InicioOpcion, string FinalOpcion)
        {
            string Titulo = string.Empty;
            string[] CaracteristicasOpcion;
            string Imagen;
            string FuncionMostrarSubMenu = string.Empty;
            string FuncionOcultarSubMenu = string.Empty;
            short IdMenu;
            string FechaActual = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            //MetodoRC4 Encriptador = new MetodoRC4();

            foreach (OpcionMenu opcion in listaOpciones)
            {
                CaracteristicasOpcion = Convertidor.ObtenerListaAtributos("");
                Imagen = Convertidor.ObtenerValor(CaracteristicasOpcion, "Imagen");

                Tabla += InicioOpcion;
                Titulo = opcion.Titulo;

                if (!opcion.TipoComando.Equals("SubMenu") && !opcion.TipoComando.ToLower().Equals("ventana"))
                {
                    Tabla += "<a title='" + Titulo + "'";
                }
                if (opcion.TipoComando.Equals("URL"))
                {
                    string Parametros = "";
                    //if (_AdministradorSeguridad.FuncionamientoAplicacionesSatelite.ToLower() == "pormenu")
                    //    Parametros = "?" + Encriptador.Encriptar("IdOpcionMenu=" + opcion.IdOpcionMenu, _AdministradorSeguridad.LlaveEncriptacion);

                    Tabla += " href=\"" + ArmarRutaElemento(opcion.Comando) + Parametros + "\">";
                }
                if (opcion.TipoComando.ToLower().Equals("ventana"))
                {
                    string[] Caracteristicas = Convertidor.ObtenerListaAtributos(opcion.Caracteristicas);
                    string Modulo = Convertidor.ObtenerValor(Caracteristicas, "Modulo");
                    string VentanaNueva = Convertidor.ObtenerValor(Caracteristicas, "VentanaNueva");

                    //if (opcion.Parametros.Contains("@IdEstructura@"))
                    //    opcion.Parametros = opcion.Parametros.Replace("@IdEstructura@", _AdministradorSeguridad.CuentaUsuario.ListaElementoGeografico.First().IdEstructura.ToString());

                    //if (opcion.Parametros.Contains("@IdArea@"))
                    //{
                    //    var IdArea = from Elemento in _AdministradorSeguridad.CuentaUsuario.ListaElementoGeografico
                    //                 where Elemento.CoberturaTerminal == 1
                    //                 select new { Elemento.IdArea };

                    //    opcion.Parametros = opcion.Parametros.Replace("@IdArea@", IdArea.First().IdArea.ToString());
                    //}

                    //if (opcion.Parametros.Contains("@IdUsuario@"))
                    //    opcion.Parametros = opcion.Parametros.Replace("@IdUsuario@", _AdministradorSeguridad.CuentaUsuario.Cuenta.IdUsuario.ToString());

                    //if (opcion.Parametros.Contains("@IdSuscriptor@"))
                    //    opcion.Parametros = opcion.Parametros.Replace("@IdSuscriptor@", _AdministradorSeguridad.CuentaUsuario.Cuenta.IdSuscriptor.ToString());

                    if (VentanaNueva == "0")
                        Tabla += "<div class='ligaMenuAyuda' onClick='window.open(\"" + ArmarRutaElemento(opcion.Comando) + "?p" + HttpUtility.UrlEncode("=" + FechaActual + "|" + Modulo + "|" + opcion.Parametros) + "\", \"" + (VentanaNueva != "" ? VentanaNueva : "_blank") + "\");' style='cursor: pointer'>" + Titulo + "</div>";
                    else
                    {
                        string Alto = Convertidor.ObtenerValor(Caracteristicas, "Alto");
                        string Ancho = Convertidor.ObtenerValor(Caracteristicas, "Ancho");
                        string Ajustable = Convertidor.ObtenerValor(Caracteristicas, "Ajustable");
                        string Top = Convertidor.ObtenerValor(Caracteristicas, "Top");
                        string Left = Convertidor.ObtenerValor(Caracteristicas, "Left");

                        Tabla += "<div class='ligaMenuAyuda' onClick='window.open(\"" + ArmarRutaElemento(opcion.Comando) + "?p" + HttpUtility.UrlEncode("=" + FechaActual + "|" + Modulo + "|" + opcion.Parametros) + "\", \"VentanaMenu_" + opcion.IdOpcionMenu + "\", \"location=0,toolbar=0" + (Ancho != "" ? ",width=" + Ancho : ",width=\" + screen.width + \"") + "" + (Alto != "" ? ",height=" + Alto : ",height=\" + screen.height +\"") + "" + (Top != "" ? ",top=" + Top : "") + "" + (Left != "" ? ",left=" + Left : "") + ",directories=0,status=0,menubar=0,scrollbars=0,resizable=" + (Ajustable != "" ? Ajustable : "yes") + ",copyhistory=0\");' style='cursor: pointer' >" + Titulo + "</div>";
                    }
                }
                else if (opcion.TipoComando.Equals("JavaScript"))
                {
                    Tabla += " href=\"#\" onclick=\"" + opcion.Comando + ";\">";
                }
                else if (opcion.TipoComando.Equals("Exe"))
                {
                    string FuncionEjecutaEXE = "";
                    FuncionEjecutaEXE += "function EjecutaEXE() { var oShell = new ActiveXObject('Shell.Application');";
                    FuncionEjecutaEXE += "var commandtoRun = '" + opcion.Comando + "'; oShell.ShellExecute(commandtoRun,'','','open','1'); }";
                    Tabla += " href=\"#\" onclick=\"javascript:" + FuncionEjecutaEXE + " EjecutaEXE();\">";
                }

                if (!opcion.TipoComando.Equals("SubMenu"))
                {
                    if (Tipo.Equals("Imagen"))
                    { //Si es de imagenes
                        Tabla += "<img src='" + opcion.Caracteristicas + "' class=\"" + "" + "\" />";
                    }
                }
                if (opcion.TipoComando.Equals("SubMenu"))
                {
                    IdMenu = short.Parse(opcion.Comando);
                    FuncionMostrarSubMenu = "function MostrarSubMenu() { document.getElementById('SubMenu" + opcion.IdOpcionMenu + IdMenu + "').style.display = 'block'; }";
                    FuncionOcultarSubMenu = "function OcultarSubMenu() { document.getElementById('SubMenu" + opcion.IdOpcionMenu + IdMenu + "').style.display = 'none'; }";
                    Tabla += "<div onMouseOver=\"javascript:" + FuncionMostrarSubMenu + " MostrarSubMenu();\"  onMouseOut=\"javascript:" + FuncionOcultarSubMenu + " OcultarSubMenu();\" >";
                    if (Tipo.Equals("Imagen"))
                    {
                        Tabla += "<img src='" + opcion.Caracteristicas + "' class=\"" + "" + "\" />";
                    }
                    Tabla += "<a title='" + Titulo + "' >";
                }
                if (!Tipo.Equals("Imagen") && !opcion.TipoComando.ToLower().Equals("ventana"))
                {
                    Tabla += Titulo;
                }

                if (!opcion.TipoComando.ToLower().Equals("ventana"))
                    Tabla += "</a>";

                if (!opcion.TipoComando.Equals("SubMenu"))
                {
                    Tabla += FinalOpcion;
                }
                if (opcion.TipoComando.Equals("SubMenu"))
                {
                    IdMenu = short.Parse(opcion.Comando);
                    Tabla += "<div ";
                    Tabla += "Id=SubMenu" + opcion.IdOpcionMenu + IdMenu + " class=\"" + "" + "\" style=\"display:none; position:absolute;\">";
                    //List<int> lista = new List<int>();
                    //lista.Add(0);
                    //Tabla += GenerarMenu(IdMenu, listaPrivilegios,0, AccionMenu);
                    Tabla += " </div></div></td>";
                }
            }
            return Tabla;
        }
        #region Armar Ruta Elemento
        private string ArmarRutaElemento(string Elemento)
        {
            string RutaLiga = "";

            try
            {
                if (!Elemento.StartsWith("http:"))
                {
                    string DirectorioVirtual = System.Web.HttpContext.Current.Request.ApplicationPath;
                    string Ruta = System.Web.HttpContext.Current.Request.Url.PathAndQuery;
                    string Url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri.Replace(Ruta, "");

                    if (Elemento.StartsWith("~/") || Elemento.StartsWith("../"))
                        RutaLiga = Url + (DirectorioVirtual != "/" ? DirectorioVirtual + "/" : DirectorioVirtual) + Elemento.Replace("~/", "").Replace("../", "");
                    else
                        RutaLiga = Elemento;
                }
                else
                    RutaLiga = Elemento;

                return RutaLiga;
            }
            catch (Exception Error)
            {
                return Elemento;
            }
            finally
            {
                RutaLiga = null;
            }
        }
        # endregion

    }
}
