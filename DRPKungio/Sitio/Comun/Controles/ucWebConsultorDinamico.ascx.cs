using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using MeNet.Nucleo.AdministradorConsultas;

using MeNet.Nucleo.ConsultorDinamico;
using Sitio.Comun.Clases;
using MeNet.Seguridad.Administrador;
using MeNet.Nucleo.Controles;
using MeNet.Seguridad.GeneradorMenus;

namespace Sitio.Comun.Controles
{
    public partial class ucWebConsultorDinamico : System.Web.UI.UserControl
    {
        #region // variables


        private AdminisradorConsultor _adminisradorConsultor;

        private ArgumentosConsulta _argCon;

        public Dictionary<string, Columna> Columnas;

        private  GridView _vistaTabla;

        private  CheckBoxList chkListaCampos;
        //public event EventHandler<ArgumentosConsulta> EventoActualizarDatos;

        private AdministradorSeguridad _administradorSeguridad;




        #endregion

        #region // Propiedades


        public AdminisradorConsultor AdminisradorConsultorActual
        {
            get { if (_adminisradorConsultor == null) _adminisradorConsultor = ObtenerAdministrardorConsultor(); return _adminisradorConsultor; }
            set { _adminisradorConsultor = value; }
        }

        public  string ObtenerIdConsultor()
        {
            _administradorSeguridad = AdministradorSistema.AdministradorSeguridadActual;
            string Id = this.Page.GetType().Name + "_" + this.ID + "_" + _administradorSeguridad.SesionSistemaActual.LLaveSesion;
            return Id;
        }
        public AdminisradorConsultor ObtenerAdministrardorConsultor()
        { 
            string Id = ObtenerIdConsultor();
            AdminisradorConsultor adminisradorConsultor = (AdminisradorConsultor)AdministradorSistema.AdministradorVariablesSesion.Obtener(Id);
            if (adminisradorConsultor == null)
            {
                adminisradorConsultor = new AdminisradorConsultor();
                adminisradorConsultor.Id = Id;
                AdministradorSistema.AdministradorVariablesSesion.Agregar(Id, adminisradorConsultor);
            }
            return adminisradorConsultor;
        }

        public ArgumentosConsulta Argumentos
        {
            get { return AdminisradorConsultorActual.AdministradorConsultasActual.Argumentos; }
            set { AdminisradorConsultorActual.AdministradorConsultasActual.Argumentos = value; }
        }

        public string IdMenu
        {
            get { return AdminisradorConsultorActual.IdMenu; }
            set { AdminisradorConsultorActual.IdMenu = value; }
        }

        public string TituloVentana
        {
            get { return AdminisradorConsultorActual.TituloVentana; }
            set { AdminisradorConsultorActual.TituloVentana = value; }
        }

        public bool ActivarSeleccion { get { return Argumentos.ActivarSeleccion; } set { Argumentos.ActivarSeleccion = value; } }

        public bool ActivarFiltro { get { return Argumentos.ActivarFiltro; } set { Argumentos.ActivarFiltro = value; } }
        public bool ActivarResumir { get { return Argumentos.ActivarResumen; } set { Argumentos.ActivarResumen = value; } }
        public bool ActivarOrden { get { return Argumentos.ActivarOrden; } set { Argumentos.ActivarOrden = value; } }

        public string CamposOcultos
        {
            get { return AdminisradorConsultorActual.AdministradorConsultasActual.CamposOcultos; }
            set { AdminisradorConsultorActual.AdministradorConsultasActual.CamposOcultos = value; }
        }
        public string Campos
        {
            get { return Argumentos.Campos; }
            set  { Argumentos.Campos = value;  }
        }
        public string CampoSeleccion
        {
            get { return Argumentos.CampoSeleccion; }
            set
            {
                Argumentos.CampoSeleccion = value;
                if (Argumentos.CampoSeleccion != "Indice")
                    VistaTabla.DataKeyNames = new string[] { Argumentos.CampoSeleccion };
            }
        }

        
        public System.Web.UI.WebControls.GridLines GridLines { get { return AdminisradorConsultorActual.GridLines; } set { AdminisradorConsultorActual.GridLines = value; } }

        public int CellSpacing { get { return AdminisradorConsultorActual.CellSpacing; } set { AdminisradorConsultorActual.CellSpacing = value; } }


        public string TituloEcabezadoSeleccion { get { return Argumentos.TituloEcabezadoSeleccion; } set { Argumentos.TituloEcabezadoSeleccion = value; } }

        public int NumeroRegistrosPagina { get { return Argumentos.NumeroRegistrosPagina; } set { Argumentos.NumeroRegistrosPagina = value; VistaTabla.PageSize= value; } }
        public int NumeroRegistrosConsulta { get { return Argumentos.NumeroRegistrosConsulta; } set { Argumentos.NumeroRegistrosConsulta = value; } }

        public IEnumerable<object> Datos { get { return AdminisradorConsultorActual.AdministradorConsultasActual.Lista; } set { AdminisradorConsultorActual.AdministradorConsultasActual.Lista = value; } }

        public string TituloSeleccion { get { return Argumentos.TituloSeleccion; } set { Argumentos.TituloSeleccion = value; } }


        public bool Paginacion { get { return Argumentos.Paginacion; } set { VistaTabla.AllowPaging = value; Argumentos.Paginacion = value; } }

        public string TituloPaginaInicial { get { return Argumentos.TituloPaginaInicial; } set { VistaTabla.PagerSettings.FirstPageText = value; Argumentos.TituloPaginaInicial = value; } }
        public string TituloPaginaFinal { get { return Argumentos.TituloPaginaFinal; } set { VistaTabla.PagerSettings.LastPageText = value; Argumentos.TituloPaginaFinal = value; } }

        public string TituloPaginaAnterior { get { return Argumentos.TituloPaginaAnterior; } set { VistaTabla.PagerSettings.PreviousPageText = value; Argumentos.TituloPaginaAnterior = value; } }
        public string TituloPaginaSigueinte { get { return Argumentos.TituloPaginaSigueinte; } set { VistaTabla.PagerSettings.NextPageText = value; Argumentos.TituloPaginaSigueinte = value; } }

        public PagerButtons TipoBotones { get { return Argumentos.TipoBotones; } set { VistaTabla.PagerSettings.Mode = value; Argumentos.TipoBotones = value; } }
        public PagerPosition PosicionBotones { get { return Argumentos.PosicionBotones; } set { VistaTabla.PagerSettings.Position = value; Argumentos.PosicionBotones = value; } }


        private static Panel contenedorContenidoVentana;
        private bool cambioTipo = true;
        public GridView VistaTabla {
            get {
                if (_vistaTabla == null)
                {
                    _vistaTabla = new GridView();
                    _vistaTabla.ID = "grd_" + AdminisradorConsultorActual.Id;
                  
                    _vistaTabla.ViewStateMode =  ViewStateMode.Enabled;
                    _vistaTabla.Columns.Add(new TemplateField());
 
                }

                return _vistaTabla; }
            set { _vistaTabla = value; } }
        public EventHandler<ArgumentosConsulta> EventoElememtoSeleccionado { get
            { return AdminisradorConsultorActual.EventoElememtoSeleccionado; }
            set { AdminisradorConsultorActual.EventoElememtoSeleccionado = value; }
        }

        #endregion

        #region // métodos de eventos de control

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

            }
            AdminisradorConsultorActual.AdministradorConsultasActual.EventoElementoSeleccionadoBoton = Seleccionar;
            AdminisradorConsultorActual.AdministradorConsultasActual.ActualizarDatos = ActualizarTabla;


            //EventoActualizarDatos -= ActualizarTabla;
            //EventoActualizarDatos += ActualizarTabla;
        }
        protected void Seleccionar(object sender, CommandEventArgs e)
        {

            if (e.CommandName == "Seleccionar")
            {
                //Consultar(string.Empty, string.Empty);
                SeleccionarElemento(sender, e);
                EventHandler<ArgumentosConsulta> handler2 = EventoElememtoSeleccionado;
                if (handler2 != null)
                {
                    handler2(sender, Argumentos);
                }
            }
        }
        protected void grdTabla_RowCreated(object sender, GridViewRowEventArgs e)
        {
            AdminisradorConsultorActual.AdministradorConsultasActual.CrearElementoRegilla((GridView)sender, e, Click, Argumentos);
        }

        protected void grdTabla_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row;
            row = VistaTabla.SelectedRow;
            row.BackColor =  System.Drawing.Color.Black;
        }

        protected void Click(object sender, ImageClickEventArgs e)
        {
            ImageButton boton = (ImageButton)sender;
            Argumentos.CampoFiltro = boton.CommandArgument;
        }

        protected void grdTabla_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Campo = string.Empty;
            string DireccionOrden = string.Empty;
            IEnumerable<object> _lista = AdminisradorConsultorActual.AdministradorConsultasActual.ListaOriginal;
            Campo = e.SortExpression;
            DireccionOrden = AdminisradorConsultorActual.AdministradorConsultasActual.CambiarEncabezadoRegilla(VistaTabla, sender, e);
            //Consultar(Campo, DireccionOrden);
            if (DireccionOrden == SortDirection.Ascending.ToString())
                //grdTabla.DataSource = _lista.OrderBy(Campo).ToList();
                _lista = _lista.OrderBy(s => s.GetType().GetProperty(Campo).GetValue(s, null).ToString()).ToList();
            else
                //grdTabla.DataSource = _lista.OrderByDescending(Campo).ToList();
                _lista = _lista.OrderByDescending(s => s.GetType().GetProperty(Campo).GetValue(s, null).ToString()).ToList();
            VistaTabla.DataSource = _lista;
            VistaTabla.DataBind();
        }

        protected void grdTabla_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            //VistaTabla.PagerSettings.Mode = TipoBotones;
            //VistaTabla.PagerSettings.Position = PosicionBotones;

            //VistaTabla.PagerSettings.FirstPageText = TituloPaginaInicial;
            //VistaTabla.PagerSettings.LastPageText = TituloPaginaFinal;
            //VistaTabla.PagerSettings.PreviousPageText = TituloPaginaAnterior;
            //VistaTabla.PagerSettings.NextPageText = TituloPaginaSigueinte;

            VistaTabla.PageIndex = e.NewPageIndex;
            VistaTabla.DataSource = AdminisradorConsultorActual.AdministradorConsultasActual.Lista.ToList() ;
            VistaTabla.DataBind();
        }



        public IEnumerable<dynamic> SeleccionarColumnas()
        {
            IEnumerable<dynamic> listatemp = AdminisradorConsultorActual.AdministradorConsultasActual.ListaOriginal;
            listatemp = SeleccionarRegistros(listatemp);
            return listatemp;
        }


        #endregion

        #region // métodos de creación y configuración

        public void CrearControles()
        {
            GeneradorControlesWeb generadorControles = new GeneradorControlesWeb();
            generadorControles.Iniciar();
            List<TipoControl> listaTipoControl = generadorControles.ObtenerListaTipoControles();
        }
        public void CrearControles(GeneradorControlesWeb generadorControles)
        {
            CrearControles(generadorControles, new Captura());
        }
        public void CrearControles(GeneradorControlesWeb generadorControles, Captura captura)
        {
            if (captura != null)
            { 
                generadorControles.ObtenerCuestionarios(captura).Count();
                if (generadorControles.ListaCuestionarios.Count() > 0)
                 AdminisradorConsultorActual.AdministradorConsultasActual.ListaPropiedadesControles = generadorControles.ObtenerControles(generadorControles.ListaCuestionarios.FirstOrDefault().IdContenedor);
            }
            AdminisradorConsultorActual.AdministradorConsultasActual.GeneradorControlesWebActual = generadorControles;

            // crea ventana
            ContenedorControl ventana = generadorControles.ObtenerListaContendores().FirstOrDefault(s => s.IdContenedor == 2021);
            if (ventana != null)
            {

                ventana.Id = ObtenerIdConsultor();
                contenedorContenidoVentana = generadorControles.CrearVentana(BloqueConsulta, ventana, TituloVentana);
                // crea Lista Campos
                chkListaCampos = CrearListaCampos();
                contenedorContenidoVentana.Controls.Add(chkListaCampos);
                //chkListaCampos.SelectedIndexChanged += chkListaCampos_SelectedIndexChanged;

                // crea grid 
                //_vistaTabla = null;
                //VistaTabla = new GridView();
                contenedorContenidoVentana.Controls.Add(VistaTabla);
                VistaTabla.RowCreated += grdTabla_RowCreated;

                VistaTabla.SelectedIndexChanged += grdTabla_SelectedIndexChanged;
                //VistaTabla.Sorting += grdTabla_Sorting;
                //VistaTabla.PageIndexChanging += grdTabla_PageIndexChanging;
                // crea panel de acciones de consultor 
                Panel contenedorAcciones = new Panel();
                //contenedorAcciones.CssClass = TipoMarco;
                contenedorContenidoVentana.Controls.Add(contenedorAcciones);

                // crea panel de acciones  

                //public string TipoBoton = "IconoFuente";
                GeneradorMenus generador = new GeneradorMenus();
                generador.ObtenerMenu(IdMenu);

                contenedorAcciones.Attributes["class"] = generadorControles.AsignarClase(generador.TipoMarco, "");
                generador.TipoBotonCss = generadorControles.AsignarClase(generador.Tipo, "");

                generador.GenerarMenuEnPanel(contenedorAcciones, Accion_Click, _administradorSeguridad, this.ID);
                generador = null;
                generadorControles = null;
                //LinkButton botonImg1 = new LinkButton();
                //botonImg1.CssClass = generadorControles.AsignarClase("IconoColumnas", "");
                //botonImg1.CommandName = "Columnas";
                //botonImg1.ToolTip = "Columnas";
                //botonImg1.ID =   botonImg1.CommandName;
                //botonImg1.Click += btnMostrar_Click;
                //contenedorAcciones.Controls.Add(botonImg1);

                //LinkButton botonImg2 = new LinkButton();
                //botonImg2.CssClass = generadorControles.AsignarClase("IconoConsultar", "");
                //botonImg2.CommandName = "Consultar";
                //botonImg2.ToolTip = "Consultar";
                //botonImg2.ID = botonImg2.CommandName;
                //botonImg2.Click += btnConsultar_Click;
                //contenedorAcciones.Controls.Add(botonImg2);

                //LinkButton botonImg3 = new LinkButton();
                //botonImg3.CssClass = generadorControles.AsignarClase("IconoExportar", "");
                //botonImg3.CommandName = "Exportar";
                //botonImg3.ToolTip = "Exportar";
                //botonImg3.ID = botonImg3.CommandName;
                //botonImg3.Click += btnExportar_Click;
                //contenedorAcciones.Controls.Add(botonImg3);

                Configurar();
            }
        }

        public void Configurar()
        {
            VistaTabla.ShowHeaderWhenEmpty = true;
            VistaTabla.EmptyDataText = "No  hay información";
            VistaTabla.ShowFooter = true;

            //VistaTabla.HeaderStyle.BackColor = Color.Gray;
            //VistaTabla.HeaderStyle.ForeColor = Color.White;
            //VistaTabla.AlternatingRowStyle.BackColor = Color.MediumAquamarine;
            //VistaTabla.AlternatingRowStyle.ForeColor = Color.Black;



            if (GridLines == null)
                GridLines = GridLines.None; ;
            VistaTabla.GridLines = GridLines;

            VistaTabla.CellSpacing = CellSpacing;
            VistaTabla.CellPadding = 1;
            // para que  se muestre  boerder deber ser VistaTabla.GridLines = GridLines.Both  y VistaTabla.CellSpacing = 1  
            // para que no se  muestre el border y se muestre el espacio VistaTabla.GridLines =GridLines.None;   y VistaTabla.CellSpacing = 1  entre más grande mas ve mas  grueso el espacio sin border

            //VistaTabla.GridLines = GridLines.None;
            //VistaTabla.GridLines = GridLines.Both;



            VistaTabla.HeaderStyle.CssClass = "RenglonEncabezado";
            VistaTabla.RowStyle.CssClass = "Renglon";
            VistaTabla.AlternatingRowStyle.CssClass = "RenglonAlternativo";

            VistaTabla.SelectedRowStyle.CssClass = "RenglonSeleccionado";

            VistaTabla.SortedAscendingHeaderStyle.CssClass = "RenglonAscendente";
            VistaTabla.SortedDescendingHeaderStyle.CssClass = "RenglonDescendente";

            VistaTabla.SortedAscendingCellStyle.CssClass = "RenglonAscendente";
            VistaTabla.SortedDescendingCellStyle.CssClass = "RenglonDescendente";



            //VistaTabla.CellSpacing = 1;
            VistaTabla.AllowPaging = Paginacion;
            VistaTabla.PageIndexChanging += new GridViewPageEventHandler(grdTabla_PageIndexChanging);
            VistaTabla.AllowSorting = false;
            VistaTabla.PageSize = NumeroRegistrosPagina;
            VistaTabla.PagerSettings.Mode = TipoBotones;


            VistaTabla.PagerStyle.HorizontalAlign = HorizontalAlign.Left;
            VistaTabla.PagerStyle.VerticalAlign = VerticalAlign.Middle;
  
            VistaTabla.PagerSettings.Position = PosicionBotones;

            VistaTabla.PagerSettings.FirstPageText = TituloPaginaInicial;
            VistaTabla.PagerSettings.LastPageText = TituloPaginaFinal;
            VistaTabla.PagerSettings.PreviousPageText = TituloPaginaAnterior;
            VistaTabla.PagerSettings.NextPageText = TituloPaginaSigueinte;

        }

        //public void Asignar(IEnumerable<dynamic> datos, Type tipo, string campos, string campoSeleccion)
        //{
        //    VistaTabla.DataSource = datos;
        //    VistaTabla.DataBind();
        //}
        #endregion

        #region // métodos de acciones de consultor

        protected void Accion_Click(object sender, EventArgs e)
        {
            LinkButton boton = (LinkButton)sender;
            string accion = boton.CommandName;
            if (accion == "Mostrar Columnas")
            {
                btnMostrar_Click(sender, e);
            }
            else if (accion == "Consultar")
            {
                btnConsultar_Click(sender, e);
            }
            else if (accion == "Exportar")
            {
                btnExportar_Click(sender, e);
            }
        }
        protected void btnConsultar_Click(object sender, EventArgs e)
        {

            AdminisradorConsultorActual.AdministradorConsultasActual.ActualizarCamposSeleccionados(ObtenerCamposSeleccionados());
            EjecutarSqlDinamicamente();
  
        }
        List<string> ObtenerCamposSeleccionados()
        {
            List<string> valoresSeleccionados = chkListaCampos.Items.Cast<ListItem>()
           .Where(li => li.Selected)
           .Select(li => li.Value)
           .Distinct().ToList();
            return valoresSeleccionados;
        }
        protected void btnMostrar_Click(object sender, EventArgs e)
        {

            if (chkListaCampos.Visible == true)
            {
                List<string> valoresSeleccionados = chkListaCampos.Items.Cast<ListItem>()
                .Where(li => li.Selected)
                .Select(li => li.Value)
                .Distinct().ToList();
                AdminisradorConsultorActual.AdministradorConsultasActual.ActualizarCamposSeleccionados(valoresSeleccionados);
                EjecutarSqlDinamicamente();
                chkListaCampos.Visible = false;
                chkListaCampos.Items.Clear();
            }
            else
            {
                CargarCamposTodosAlLista();
                chkListaCampos.Visible = true;
            }
        }

        public CheckBoxList CrearListaCampos()
        {
            chkListaCampos = new CheckBoxList();
            chkListaCampos.ID = "chkListaCampos_" + AdminisradorConsultorActual.Id;
            chkListaCampos.AutoPostBack = false;
            chkListaCampos.Visible = false;
            chkListaCampos.Items.Clear();
            return chkListaCampos;
        }


        public void CargarCamposTodosAlLista()
        {
            chkListaCampos.Items.Clear();
            CargarCamposAlLista();
        }
        public void CargarCamposAlLista()
        {
            foreach (KeyValuePair<string, Columna> columna in Argumentos.Columnas)
            {
                ListItem item = new ListItem();
                item.Text = columna.Value.Titulo;
                item.Value = columna.Value.Nombre;
                item.Selected = columna.Value.Seleccionado;
                chkListaCampos.Items.Add(item);
            }
            chkListaCampos.AutoPostBack = false;
            chkListaCampos.DataBind();
        }
 

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            AdministradorFuenteDatos admfd = new AdministradorFuenteDatos();
            //admfd.ExportarVista(this.Page, ucConsultor1.Tabla, ucConsultor1.Datos, "word", "infome");
            admfd.ExportarVista(this.Page, VistaTabla, AdminisradorConsultorActual.AdministradorConsultasActual.Lista, "excel", string.Empty);
        }


 

        public void AsigarOrigenDatos(IEnumerable<dynamic> datos, Type tipo, string campos, string campoSeleccion)
        {
            DefinirCampoSeleccion(campoSeleccion);
            AsigarOrigenDatos(datos, tipo, campos);
        }

        public void AsigarOrigenDatos(IEnumerable<dynamic> datos, Type tipo, string campos)
        {
            DefinirCampoTipoConsulta(tipo);
            DefinirCampos(campos);
            AsigarOrigenDatos(datos);
        }
        public void AsigarOrigenDatos(IEnumerable<dynamic> datos, Type tipo)
        {
            DefinirCampoTipoConsulta(tipo);
            AsigarOrigenDatos(datos);

        }
        public void AsigarOrigenDatos(IEnumerable<dynamic> datos)
        {
            //string campos = Argumentos.Campos;
            //Type Tipo = Argumentos.TipoEntidad;
            if (datos!= null && datos.Count()>0)
            {
                AdminisradorConsultorActual.AdministradorConsultasActual.Lista = datos;
                AdminisradorConsultorActual.AdministradorConsultasActual.ListaOriginal = datos;
                if (cambioTipo)
                {
                    //if (Argumentos.Campos != null && Argumentos.Campos != string.Empty)
                    Argumentos.Campos = AdminisradorConsultorActual.AdministradorConsultasActual.CargarCamposInicialmente( Argumentos, Argumentos.Campos);
                    chkListaCampos.Items.Clear();
                    CargarCamposTodosAlLista();
                }
                else
                {

                    if  (Argumentos.Campos!=null  && Argumentos.Campos!=string.Empty)
                        Argumentos.Campos = AdminisradorConsultorActual.AdministradorConsultasActual.ObtenerCamposSeleccionados();
                    else
                    {
                        Argumentos.Campos = AdminisradorConsultorActual.AdministradorConsultasActual.CargarCamposInicialmente(Argumentos, Argumentos.Campos);
                        chkListaCampos.Items.Clear();
                        CargarCamposTodosAlLista();
                    }
                }

                EjecutarSqlDinamicamente();
            }
            else
            {
                VistaTabla.DataSource = null;
                VistaTabla.DataBind();
            }
        }

        public void DefinirColumnasConsulta(Type tipo, string campos, string campoSeleccion)
        {
            DefinirCampoTipoConsulta(tipo);
            DefinirCampoSeleccion(campoSeleccion);
            DefinirCampos(campos);
        }
        public void DefinirCampoTipoConsulta(Type tipo)
        {
            if (Argumentos.TipoEntidad == tipo)
                cambioTipo = false;
            else
                cambioTipo = true;
            if ( AdministradorSistema.AdministradorSeguridadActual.SesionSistemaActual.IdIdioma != AdministradorSistema.AdministradorSeguridadActual.SesionUsuarioActual.IdIdioma)
                cambioTipo = true;
            //else
            //    cambioTipo = true;
            Argumentos.TipoEntidad = tipo;
        }
        public void DefinirCampoSeleccion(string campoSeleccion)
        {
            if (campoSeleccion != null && campoSeleccion != string.Empty  /*&& Argumentos.CampoSeleccion==string.Empty*/ )
            {
                Argumentos.CampoSeleccion = campoSeleccion;
            }
        }
        public void DefinirCampos(string campos)
        {
            //string campotem = Argumentos.Campos;

            // && (Argumentos.Campos == null || Argumentos.Campos == string.Empty)
            if ((campos != null || campos != string.Empty) )
            {
                if (cambioTipo==true)
                {
                    campos = AdminisradorConsultorActual.AdministradorConsultasActual.CargarCamposInicialmente(Argumentos, campos);
                    Argumentos.Campos = campos;
                    List<string> listaCampos = campos.ToString().Split(',').ToList();
                    AdminisradorConsultorActual.AdministradorConsultasActual.ActualizarCamposSeleccionados(listaCampos);
                    cambioTipo = false;
                }

            }
        }

        public void EjecutarSqlDinamicamente()
        {
            VistaTabla.DataSource = null;
            IEnumerable<dynamic> lista = SeleccionarColumnas();
            if (lista != null )
            {
                if (NumeroRegistrosConsulta != null && NumeroRegistrosConsulta > 0)
                {
                    lista = lista.Take(NumeroRegistrosConsulta);
                    //lista = lista.Take(NumeroRegistros).ToList();
                }

                lista = Ordenar(lista);
                lista = Filtrar(null, lista);
                Resumir(lista);
                AdminisradorConsultorActual.AdministradorConsultasActual.Lista = lista;

                VistaTabla.DataSource = lista.ToList();
                VistaTabla.DataBind();
            }
        }

        protected void ActualizarTabla(object sender, ArgumentosConsulta e)
        {
            Actualizar(sender, e);
        }

        public void Actualizar(object sender, ArgumentosConsulta argsConsulta)
        {
            string Seleccion = string.Empty;
            string accion = Argumentos.Accion;
            IEnumerable<dynamic> listatemp = AdminisradorConsultorActual.AdministradorConsultasActual.Lista;

            if (listatemp == null)
                listatemp = SeleccionarRegistros(AdminisradorConsultorActual.AdministradorConsultasActual.ListaOriginal);

            //if (VistaTabla.DataSource != null)
            //    _listatemp = VistaTabla.DataSource as IEnumerable<object>;
            //else


            else if (Argumentos.Accion == "Ordenar")
            {
                listatemp = Ordenar(listatemp);
            }
            if (Argumentos.Accion == "Filtrar")
            {
                listatemp = Filtrar(sender, listatemp);
                Resumir(listatemp);
            }
            else if (Argumentos.Accion == "CancelarFiltro")
            {
                listatemp = SeleccionarRegistros(AdminisradorConsultorActual.AdministradorConsultasActual.ListaOriginal);
                listatemp = Filtrar(sender, listatemp);
                //_listatemp = CancelarFiltros();
                Resumir(listatemp);
            }
            else if (Argumentos.Accion == "SeleccionarResumen")
            {

            }
            else if (Argumentos.Accion == "Resumir")
            {
                Resumir(listatemp);
            }

            AdminisradorConsultorActual.AdministradorConsultasActual.Lista = listatemp;
            VistaTabla.DataSource = listatemp.ToList();
            VistaTabla.DataBind();
        }




        #endregion

        #region // seleccionar  

        public IEnumerable<dynamic> SeleccionarRegistros(IEnumerable<dynamic> _lista)
        {
            string campos = Argumentos.Campos;
            if (_lista != null && _lista.Count() > 0 && Argumentos.Campos!=string.Empty)
                _lista = _lista.Select<dynamic>(Argumentos.Campos, Argumentos.TipoEntidad).ToList();
            return (IEnumerable<dynamic>)_lista;
        }

        public void SeleccionarElemento(object sender, CommandEventArgs e)
        {
            string argumento = e.CommandArgument.ToString();
            if (argumento != null && argumento != string.Empty)
            {
                Argumentos.Indice = Int16.Parse(argumento);
                if (Argumentos.Indice >= 0 && Argumentos.Indice < VistaTabla.Rows.Count)
                {
                    Argumentos.Registro = VistaTabla.Rows[Argumentos.Indice];
                    Argumentos.Comando = e.CommandName;
                    VistaTabla.SelectedIndex = Argumentos.Indice;
                }
            }
        }

        #endregion

        #region // ordenar
        public IEnumerable<dynamic> Ordenar(IEnumerable<dynamic> lista)
        {
            string Campo = string.Empty;
            string DireccionOrden = string.Empty;
            IEnumerable<dynamic> listatemp = lista;
            Columna columna;
            foreach (KeyValuePair<string, Columna> elemento in Argumentos.Columnas)
            {
                columna = elemento.Value;
                DireccionOrden = columna.Orden;
                Campo = columna.Nombre;
                if (columna.Seleccionado && columna.SeleccionOrden)
                {
                    if (DireccionOrden == SortDirection.Ascending.ToString())
                        //VistaTabla.DataSource = _lista.OrderBy(Campo).ToList();
                        listatemp = lista.OrderBy(s => ObtenerValor(s, Campo)).ToList();
                    else
                        //VistaTabla.DataSource = _lista.OrderByDescending(Campo).ToList();
                        listatemp = lista.OrderByDescending(s => ObtenerValor(s, Campo)).ToList();
                    lista = listatemp;
                }
            }
            return listatemp;
        }
        #endregion

        #region // filtrar

        public IEnumerable<dynamic> Filtrar(object sender, IEnumerable<dynamic> lista)
        {
            Columna columna = null;
            //IEnumerable<dynamic> listaOrigen = lista;
            IEnumerable<dynamic> listatemp = lista;
            foreach (KeyValuePair<string, Columna> elemento in Argumentos.Columnas)
            {
                columna = elemento.Value;
                if ((columna.Seleccionado && columna.SeleccionFiltro) &&
                    (columna.ValoresSeleccionadosFiltro != null && columna.ValoresSeleccionadosFiltro.Count > 0))
                {
                    listatemp = FiltrarElementos(listatemp, columna);
                    //if (listatemp != null)
                    //    listaOrigen = listatemp;

                }

            }

            return listatemp;
        }
        public IEnumerable<dynamic> FiltrarElementos(IEnumerable<dynamic> lista, Columna columna)
        {
            IEnumerable<dynamic> listatemp = lista.Where(s =>
                       columna.ValoresSeleccionadosFiltro.Any(v => v.ToString().Equals(ObtenerValor(s, columna.Nombre))));
            return listatemp;
        }



        //public IEnumerable<dynamic> CancelarFiltros()
        //{
        //    IEnumerable<dynamic> listatemp= AdminisradorConsultorActual.AdministradorConsultasActual.ListaOriginal;

        //    Columna columna = null;
        //    Argumentos.Accion = string.Empty;
        //    foreach (KeyValuePair<string, Columna> elemento in Argumentos.Columnas)
        //    {
        //        columna = elemento.Value;
        //        columna.SeleccionFiltro=false;
        //        columna.ValoresSeleccionadosFiltro = null;
        //    }
        //    if (Argumentos.Campos != null && Argumentos.Campos != string.Empty)
        //    {
        //        listatemp = AdminisradorConsultorActual.AdministradorConsultasActual.ListaOriginal.Select<dynamic>(Argumentos.Campos).ToList();
        //    }
        //    return listatemp;
        //}

        #endregion

        #region   // resumir 

        protected void Resumir(IEnumerable<dynamic> _listatemp)
        {
            Columna columna;
            //Argumentos = argsConsulta;
            //AdminisradorConsultorActual.AdministradorConsultasActual.Argumentos= Argumentos;

            if (Argumentos.ActivarResumen && Argumentos.Columnas != null)
            {
                foreach (KeyValuePair<string, Columna> elemento in Argumentos.Columnas)
                {
                    columna = elemento.Value;
                    if (columna.Seleccionado && columna.SeleccionResumen)
                        ResumirColumna(_listatemp, columna);
                }
            }

        }
        public void ResumirColumna(IEnumerable<dynamic> listatemp, Columna columna)
        {
            bool esNumerico = true;
            if (columna.Tipo == "String" || columna.Tipo == "DateTime" || columna.Tipo == "Boolean")
                esNumerico = false;
            foreach (ResumenColumna row in columna.ObtenerOperacionResumen())
            {
                if (row.Seleccionado)
                {
                    if (row.Operacion == "Cuenta")
                        row.Total = listatemp.Count().ToString();

                    else if (esNumerico && row.Operacion == "Suma")
                    {
                        row.Total = listatemp.Sum(i => ObtenerValorPorTipo(i, columna.Nombre)).ToString();
                    }

                    else if (esNumerico && row.Operacion == "Promedio")
                    {
                        row.Total = listatemp.Average(i => ObtenerValorPorTipo(i, columna.Nombre)).ToString();
                    }
                    else if (row.Operacion == "Minino")
                    {
                        row.Total = listatemp.Min(i => ObtenerValorPorTipo(i, columna.Nombre)).ToString();

                    }
                    else if (row.Operacion == "Maximo")
                    {
                        row.Total = listatemp.Max(i => ObtenerValorPorTipo(i, columna.Nombre)).ToString();

                    }
                }
            }
        }
        #endregion

        #region    //métodos  generales
        public string ObtenerValor(object objeto, string campo)
        {
            object valor = null;
            string valorConvertido = string.Empty;
            if (objeto != null)
            {
                valor = objeto.GetType().GetProperty(campo).GetValue(objeto, null);
                valorConvertido = (valor == null) ? string.Empty : valor.ToString();
            }
            return valorConvertido;
        }
        public object ObtenerValorPorTipo(object objeto, string campo)
        {
            object valor = null;
            object valorConvertido = null;
            Type tipoPropiedad;
            PropertyInfo propInfo = objeto.GetType().GetProperty(campo);
            tipoPropiedad = Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType;
            if (objeto != null)
            {
                valor = objeto.GetType().GetProperty(campo).GetValue(objeto, null);
                if (valor != null)
                    valorConvertido = Convert.ChangeType(valor, tipoPropiedad);
                //valorConvertido = (valor == null) ? null : Convert.ChangeType(valor, tipoPropiedad);
                else
                {
                    if (tipoPropiedad.Name == "String")
                        valorConvertido = string.Empty;
                    else if(tipoPropiedad.Name == "Date")
                        valorConvertido = string.Empty;
                    else if (tipoPropiedad.Name == "Decimal")
                        valorConvertido = 0;
                    else if (tipoPropiedad.Name.Contains("Int"))
                        valorConvertido = 0;
                    else
                        valorConvertido = 0;

                }
            }
            else
                valorConvertido = string.Empty;
            return valorConvertido;
        }
        #endregion
    }
}