using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Sitio.Comun.Clases;

// paso  1 indicar los  espacios de nombre  usados

using MeNet.Nucleo.Catalogo;
using MeNet.Nucleo.AdministradorConsultas;
using System.Reflection;

namespace Sitio.Comun.Controles
{
    public partial class AdministradorListaPersonalizada
    {
        #region  paso  2  declaracion variables
        public string Id;
        public int IdCatalogo;
        public List<ElementoCatalogo> ListaElementos;
        private EventHandler _EventoElememtoSeleccionado;
        private  string _idElementoSeleccionado;
        private string _textoElementoSeleccionado;

 

        public String IdElementoSeleccionado
        {
            get   {  return _idElementoSeleccionado;    }
            set  {  _idElementoSeleccionado = value;   }
        }

        public String TextoElementoSeleccionado
        {
            get  {   return _textoElementoSeleccionado;     }
            set {   _textoElementoSeleccionado = value;   }
        }
        #endregion

        #region  paso  2  declaracion propiedades

        public EventHandler EventoElememtoSeleccionado
        {
            get
            { return _EventoElememtoSeleccionado; }
            set { _EventoElememtoSeleccionado = value; }
        }

        #endregion
    }
    public partial class ListaPersonalizada : System.Web.UI.UserControl
    {

        #region  paso  2  declaracion variables

        private  AdministradorListaPersonalizada _adminisrador;


        public string TipoMenu = "";
        public string Id;
        public string Valor;
        public string Imagen = string.Empty;
        public string Color = string.Empty;
        private AdmistradorCatalogos _admistradorCatalogos;
        private string _cssClass;
        private string _noCol;

        #endregion

        #region  paso  3  declaracion propiedades

        public int IdCatalogo
        {
            get { return AdminisradorActual.IdCatalogo; }
            set {  AdminisradorActual.IdCatalogo = value; }
        }
        public String IdElementoSeleccionado
        {
            get { return AdminisradorActual.IdElementoSeleccionado;   }
            set {  AdminisradorActual.IdElementoSeleccionado = value;
                if (AdminisradorActual.ListaElementos!=null)
                {
                    ElementoCatalogo e = AdminisradorActual.ListaElementos.FirstOrDefault(s => s.Id == AdminisradorActual.IdElementoSeleccionado);
                    if (e != null)
                        TextoElementoSeleccionado = e.Valor;
                }
            }
        }
        public String TextoElementoSeleccionado
        {
            get  {  return AdminisradorActual.TextoElementoSeleccionado;   }
            set  { AdminisradorActual.TextoElementoSeleccionado = value; txtBusqueda.Text = value;  }
        }

        public String CssClass
        {
            get { return _cssClass; }
            set { _cssClass = value; txtBusqueda.CssClass = value;  }
        }
        public String NoCol
        {
            get { return _noCol; }
            set { _noCol = value; ContenedorLista.Attributes.Add("class", value); }
        }
        public AdministradorListaPersonalizada AdminisradorActual
        {
            get { if (_adminisrador == null) _adminisrador = ObtenerAdministrardor(); return _adminisrador; }
            set { _adminisrador = value; }
        }

        public AdministradorListaPersonalizada ObtenerAdministrardor()
        { 
            string Id = this.Page.GetType().Name + "_" + this.ID + "_" + AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.SesionSistemaActual.LLaveSesion;

            AdministradorListaPersonalizada adminisrador = (AdministradorListaPersonalizada)AdministradorSistema.ControaldorAplicacion.AdministradorVariablesSesion.Obtener(Id);
            if (adminisrador == null)
            {
                adminisrador = new AdministradorListaPersonalizada();
                adminisrador.Id = Id;
                AdministradorSistema.AdministradorVariablesSesion.Agregar(Id, adminisrador);
            }
            return adminisrador;
        }

        #endregion

        #region  paso  4  métodos de eventos de página
        protected void Page_Load(object sender, EventArgs e)
        {

            List<ElementoCatalogo> ListaElementos=AdminisradorActual.ListaElementos;
            int IdCatalogo = AdminisradorActual.IdCatalogo;
            if (ListaElementos == null)
            {
                AdmistradorCatalogos _admistradorCatalogos = new AdmistradorCatalogos();
                //ListaElementos = (List<ElementoCatalogo>)_admistradorCatalogos.ObtenerListaElementosCatalogo(3126, 1);
                //ListaElementos = (List<ElementoCatalogo>)_admistradorCatalogos.ObtenerListaElementosCatalogo(3096, 1); // colores
                if (AdminisradorActual.IdCatalogo != null && AdminisradorActual.IdCatalogo != 0)
                    AdminisradorActual.ListaElementos = (List<ElementoCatalogo>)_admistradorCatalogos.ObtenerListaElementosCatalogo(AdminisradorActual.IdCatalogo, AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.IdIdioma); // colores
                else
                    AdminisradorActual.ListaElementos = (List<ElementoCatalogo>)_admistradorCatalogos.ObtenerListaElementosCatalogo(3126, AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.IdIdioma); // colores
                Cargar(AdminisradorActual.ListaElementos);
            }
            else
                Cargar(AdminisradorActual.ListaElementos);

        }


        #endregion

        public void Elemento_Click(object sender, EventArgs e)
        {
            Seleccionar(sender, e);
        }
        public void Seleccionar(object sender, EventArgs e)
        {
            LinkButton boton = (LinkButton)sender;
            if (boton != null)
            {
                txtBusqueda.Text = boton.Text;
                 AdminisradorActual.TextoElementoSeleccionado = boton.Text;
                AdminisradorActual.IdElementoSeleccionado = boton.CommandArgument;
            }
            EventHandler handler2 = AdminisradorActual.EventoElememtoSeleccionado;
            if (handler2 != null)
            {
                handler2(sender, e);
            }

        }
        protected void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            string Filtro = "";
            Filtro = txtBusqueda.Text.Trim();
            AplicarFiltros(sender, Filtro);
        }
        public void AplicarFiltros(object sender, string filtro)
        {
            if (filtro != null)
            {
                List<ElementoCatalogo> ListaElementosTemp = AdminisradorActual.ListaElementos.Where(s => s.Valor.ToUpper().Contains(filtro.ToUpper())).ToList();
                Cargar(ListaElementosTemp);
            }

        }
        public void SeleccionarElemento(object sender, EventArgs e)
        {
            //string argumento = e.CommandArgument.ToString();
            //ArgumentosConsulta Argumentos =new ArgumentosConsulta();
            //if (argumento != null && argumento != string.Empty)
            //{
            //    Argumentos.Indice = Int16.Parse(argumento);
            //    if (Argumentos.Indice >= 0 /*&& Argumentos.Indice < VistaTabla.Rows.Count*/)
            //    {
            //        //Argumentos.Registro = VistaTabla.Rows[Argumentos.Indice];
            //        Argumentos.Comando = e.CommandName;
            //        //VistaTabla.SelectedIndex = Argumentos.Indice;
            //    }
            //}
        }

        public void Cargar(List<ElementoCatalogo> ListaElementos)
        {
            Panel contendor = new Panel();
            contendor.ID = "conte" + this.ID;

            //contendor.Width = Unit.Percentage(45);
            //contendor.Height = Unit.Pixel(400);
            //contendor.Height = Unit.Percentage(20);

       
            TipoMenu = "MarcoPopUP ";

            contendor.CssClass = "Menu Mosaico";
            contendor.CssClass = "Menu Animado";
            contendor.CssClass = "Menu Lista";
            contendor.CssClass = "Menu Horizontal";
            contendor.CssClass = "Menu Vertical";

            TipoMenu = "MarcoControl ";
            TipoMenu = "ElementosListaPersonalizada ";

            contendor.CssClass = TipoMenu;

            HtmlGenericControl conUl = new HtmlGenericControl("ul");

            //style = " position:absolute; z-index:10;  top:190px ; left:20px ; width:500px ; height:485px; display:block;"
            //this.Controls.Add(contendor);
            ContenedorListaElementos.Controls.Clear();
            ContenedorListaElementos.Controls.Add(contendor);

            contendor.Controls.Add(conUl);
            if (ListaElementos!=null)
                 CargarElementosCatalogo(ListaElementos,contendor, conUl);

        }
        public void CargarElementosCatalogo(List<ElementoCatalogo> ListaElementos, Panel contendor, HtmlGenericControl conUl)
        {
            foreach (ElementoCatalogo elemento in ListaElementos)
            {
                HtmlGenericControl conLi = new HtmlGenericControl("li");

                //if (Color != string.Empty)
                //    conLi.Attributes.Add("class", Color);
                //else
                //    conLi.Attributes.Add("class", elemento.Id);
                //

                LinkButton boton = new LinkButton();
                boton.Text = elemento.Valor;
                boton.ID = contendor.ID + "ele_" + elemento.Id + "_" + elemento.IdElemento;
                boton.CommandName = elemento.IdCatalogo.ToString();
                boton.CommandArgument = elemento.Id;
                //boton.CssClass = " " + elemento.Id;
                boton.Click += Elemento_Click;
                if (TipoMenu.Contains("Menu Mosaico") || TipoMenu.Contains("Menu Animado"))
                {
                    HtmlGenericControl conspan = new HtmlGenericControl("span");
                    boton.Controls.Add(conspan);
                    HtmlGenericControl conicono = new HtmlGenericControl("i");
                    if (Imagen != string.Empty)
                        conicono.Attributes.Add("class", " iconoOpcion " + Imagen);
                    else
                        conicono.Attributes.Add("class", " iconoOpcion " + elemento.Id);
                    conspan.Controls.Add(conicono);
                    // < i class="iconoOpcion  icon-help-circled"></i>
                    HtmlGenericControl condiv = new HtmlGenericControl("div");
                    condiv.InnerText = elemento.Valor;
                    boton.Controls.Add(condiv);
                }
                else
                {
                    if (Imagen != string.Empty)
                        boton.CssClass = " " + Imagen + "   ";
                    else
                        boton.CssClass = " " + elemento.Id + "   ";
                }
                conLi.Controls.Add(boton);
                conUl.Controls.Add(conLi);
            }
        }

    }
}