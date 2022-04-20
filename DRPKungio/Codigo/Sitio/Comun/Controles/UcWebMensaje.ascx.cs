using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sitio.Comun.Clases;
using MeNet.Nucleo.Eventos;
using DRP.Modelo;
using MeNet.Nucleo.Error;
using System.Web.UI.HtmlControls;
using System.Data.Entity.Core.Objects;
//using System.Data.Entity.Core.Objects;
namespace Sitio.Comun.Controles
{
    public partial class UcWebMensaje : System.Web.UI.UserControl
    {
        #region Evento de respuesta en el servidor
        //public event EventHandler RespuestaMensaje;
        #endregion

        #region Variables locales
        private string _IdControlMensaje = null;
        private BotonesMensaje _TipoBotonesMensaje;
        private TipoImagen _TipoImagenMensaje;
        private string _NombreInstanciaCliente;
        private TipoEvento _ComportamientoEventos;
        private string _TextoBotonAnular;
        private string _TextoBotonReintentar;
        private string _TextoBotonOmitir;
        private string _TextoBotonAceptar;
        private string _TextoBotonCancelar;
        private string _TextoBotonSi;
        private string _TextoBotonNo;
        private static BitacoraError _bitacoraError;

        #endregion

        #region Eventos de cliente
        private string _EventoClienteAnular;
        private string _EventoClienteReintentar;
        private string _EventoClienteOmitir;
        private string _EventoClienteAceptar;
        private string _EventoClienteCancelar;
        private string _EventoClienteSi;
        private string _EventoClienteNo;
        #endregion

        #region Propiedades de los eventos de cliente
        public string EventoClienteAnular
        {
            set { this._EventoClienteAnular = value; }
            get { return this._EventoClienteAnular; }
        }

        public string EventoClienteReintentar
        {
            set { this._EventoClienteReintentar = value; }
            get { return this._EventoClienteReintentar; }
        }

        public string EventoClienteOmitir
        {
            set { this._EventoClienteOmitir = value; }
            get { return this._EventoClienteOmitir; }
        }

        public string EventoClienteAceptar
        {
            set { this._EventoClienteAceptar = value; }
            get { return this._EventoClienteAceptar; }
        }

        public string EventoClienteCancelar
        {
            set { this._EventoClienteCancelar = value; }
            get { return this._EventoClienteCancelar; }
        }

        public string EventoClienteSi
        {
            set { this._EventoClienteSi = value; }
            get { return this._EventoClienteSi; }
        }

        public string EventoClienteNo
        {
            set { this._EventoClienteNo = value; }
            get { return this._EventoClienteNo; }
        }
        #endregion

        #region Propiedades del control de mensaje
        public string NombreInstanciaCliente
        {
            set { this._NombreInstanciaCliente = value; }
            get { return this._NombreInstanciaCliente; }
        }

        public string TituloMensaje
        {
            set { this.divTituloMensaje.InnerText = value; }
            get { return this.divTituloMensaje.InnerText; }
        }
        public Control DetalleTextoMensaje
        {
            get { return (Control)divDetalleTextoMensaje; }
        }

        public string ContenidoMensaje
        {
            set { this.divTextoMensaje.InnerHtml = value; }
            get { return this.divTextoMensaje.InnerHtml; }
        }


        public string IdControl
        {
            set
            {
                Session["IdControlMensaje"] = value;
                this._IdControlMensaje = value;
            }
            get
            {
                if (Session["IdControlMensaje"] != null)
                {
                    this._IdControlMensaje = (string)Session["IdControlMensaje"];
                }
                return this._IdControlMensaje;
            }
        }

        public BotonesMensaje TipoBotonesMensaje
        {
            set
            {
                Session["TipoBotonesMensaje"] = value;
                this._TipoBotonesMensaje = value;
            }
            get
            {
                if (Session["TipoBotonesMensaje"] != null)
                {
                    this._TipoBotonesMensaje = (BotonesMensaje)Session["TipoBotonesMensaje"];
                }
                return this._TipoBotonesMensaje;
            }
        }

        public TipoImagen TipoImagenMensaje
        {
            set { this._TipoImagenMensaje = value; }
            get { return this._TipoImagenMensaje; }
        }

        public TipoEvento ComportamientoEventos
        {
            set { this._ComportamientoEventos = value; }
            get { return this._ComportamientoEventos; }
        }
        #endregion

        #region Propiedades del texto de los botones
        public string TextoBotonAnular
        {
            set
            { this._TextoBotonAnular = value; }
            get
            {
                if (String.IsNullOrEmpty(this._TextoBotonAnular) == true)
                {
                    return "Anular";
                }
                else
                {
                    return this._TextoBotonAnular;
                }
            }
        }

        public string TextoBotonReintentar
        {
            set
            { this._TextoBotonReintentar = value; }
            get
            {
                if (String.IsNullOrEmpty(this._TextoBotonReintentar) == true)
                {
                    return "Reintentar";
                }
                else
                {
                    return this._TextoBotonReintentar;
                }
            }
        }

        public string TextoBotonOmitir
        {
            set
            { this._TextoBotonOmitir = value; }
            get
            {
                if (String.IsNullOrEmpty(this._TextoBotonOmitir) == true)
                {
                    return "Omitir";
                }
                else
                {
                    return this._TextoBotonOmitir;
                }
            }
        }

        public string TextoBotonAceptar
        {
            set
            { this._TextoBotonAceptar = value; }
            get
            {
                if (String.IsNullOrEmpty(this._TextoBotonAceptar) == true)
                {
                    return "Aceptar";
                }
                else
                {
                    return this._TextoBotonAceptar;
                }
            }
        }

        public string TextoBotonCancelar
        {
            set
            { this._TextoBotonCancelar = value; }
            get
            {
                if (String.IsNullOrEmpty(this._TextoBotonCancelar) == true)
                {
                    return "Cancelar";
                }
                else
                {
                    return this._TextoBotonCancelar;
                }
            }
        }

        public string TextoBotonSi
        {
            set
            { this._TextoBotonSi = value; }
            get
            {
                if (String.IsNullOrEmpty(this._TextoBotonSi) == true)
                {
                    return "Si";
                }
                else
                {
                    return this._TextoBotonSi;
                }
            }
        }

        public string TextoBotonNo
        {
            set
            { this._TextoBotonNo = value; }
            get
            {
                if (String.IsNullOrEmpty(this._TextoBotonNo) == true)
                {
                    return "No";
                }
                else
                {
                    return this._TextoBotonNo;
                }
            }
        }
        #endregion

        #region Enumeraciones para validar las propiedades
        public enum TipoEvento
        {
            Cliente, Servidor
        }

        public enum TipoImagen
        {
            Informativo, Pregunta, Advertencia, Error, Ninguno
        }

        public enum BotonesMensaje
        {
            AnularReintentarOmitir, Aceptar, AceptarCancelar, ReintentarCancelar, SiNo, SiNoCancelar
        }

        public enum EventoMensaje
        {
            Anular, Reintentar, Omitir, Aceptar, Cancelar, Si, No
        }
        #endregion

        /// <summary>
        /// Funcion que despliega el mensaje
        /// </summary>
        /// <param name="TituloMensaje">Texto que se desplegara en el titulo del mensaje</param>
        /// <param name="ContenidoMensaje">Texto que se desplegara en el contenido del mensaje</param>
        /// <param name="TipoImagenMensaje">Tipo de imagen que se desplegara en el mensaje</param>
        /// <param name="TipoBotonesMensaje">Tipo de botones que se desplegaran en el mensaje</param>
        /// 
		protected void Page_Load(object sender, EventArgs e)
        {
            if (this.ComportamientoEventos == TipoEvento.Servidor)
            {
                //ahrefBoton1.ServerClick += new EventHandler(EventoClickBoton1);
                ahrefBoton1.Click += new EventHandler(EventoClickBoton1);
                ahrefBoton2.Click += new EventHandler(EventoClickBoton2);
                ahrefBoton3.Click += new EventHandler(EventoClickBoton3);
            }

            if (this.ComportamientoEventos == TipoEvento.Cliente)
            {
                if (this.NombreInstanciaCliente != string.Empty && this.NombreInstanciaCliente != null)
                {
                    divFondoMensaje.ID = this.NombreInstanciaCliente;
                }
            }
        }

        // mensaje por clave   con delegado  EventHandler

        public void MostrarMensaje(string claveMensaje, TipoImagen TipoImagenMensaje, BotonesMensaje TipoBotonesMensaje, Object padre, EventHandler accion)
        {
            string mensaje = string.Empty;
            string titulo = string.Empty;

            try
            {
                ObjectResult<ObtenerTerminologia_Result> terminologia = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.AdministradorTerminologia.ObtenerTerminologia(claveMensaje, AdministradorSistema.AdministradorSeguridadActual.IdIdioma);
                if (terminologia != null)
                {
                    titulo = terminologia.FirstOrDefault().Titulo;
                    mensaje = terminologia.FirstOrDefault().Texto;
                }
                MostrarMensaje(titulo, mensaje, UcWebMensaje.TipoImagen.Informativo, UcWebMensaje.BotonesMensaje.Aceptar, padre, accion);
            }
            catch
            {

            }
        }

        // mensaje por clave   con delegado  DelegadoRespuestaGeneral

        public void MostrarMensaje(string claveMensaje, TipoImagen TipoImagenMensaje, BotonesMensaje TipoBotonesMensaje, Object padre, DelegadoRespuestaGeneral accion)
        {
            string mensaje = string.Empty;
            string titulo = string.Empty;
            String idPagina = padre.GetType().Name;
            try
            {
                ObjectResult<ObtenerTerminologia_Result> terminologia = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.AdministradorTerminologia.ObtenerTerminologia(claveMensaje, AdministradorSistema.AdministradorSeguridadActual.IdIdioma);

                if (terminologia != null)
                {
                    ObtenerTerminologia_Result terminologiaElemento = terminologia.ToList().FirstOrDefault();
                    titulo = terminologiaElemento.Titulo;
                    mensaje = terminologiaElemento.Texto;
                }
                MostrarMensaje(titulo, mensaje, UcWebMensaje.TipoImagen.Informativo, UcWebMensaje.BotonesMensaje.Aceptar, this, accion);
            }
            catch (Exception error)
            {

            }
        }

        //
        // mensaje  con delegado  DelegadoRespuestaGeneral
        //

        // con  id  de pagina
        public void MostrarMensaje(string TituloMensaje, string ContenidoMensaje, TipoImagen TipoImagenMensaje, BotonesMensaje TipoBotonesMensaje, Object padre, DelegadoRespuestaGeneral accion)
        {
            String idPagina = padre.GetType().Name;
            IdControl = idPagina;
            MostrarMensaje(TituloMensaje, ContenidoMensaje, TipoImagenMensaje, TipoBotonesMensaje, idPagina, accion);
        }
        // con  Id idependiente

        public void MostrarMensaje(string TituloMensaje, string ContenidoMensaje, TipoImagen TipoImagenMensaje, BotonesMensaje TipoBotonesMensaje, String idPagina, DelegadoRespuestaGeneral accion)
        {
            IdControl = idPagina;
            AdministradorSistema.ControaldorEventosActual.InscribirMetodoGeneral(idPagina, accion);
            mostrar(TituloMensaje, ContenidoMensaje, TipoImagenMensaje, TipoBotonesMensaje, idPagina);
        }

        // mensaje  con delegado  EventHandler

        // con  id  de pagina

        public void MostrarMensaje(string TituloMensaje, string ContenidoMensaje, TipoImagen TipoImagenMensaje, BotonesMensaje TipoBotonesMensaje, Object padre, EventHandler accion)
        {
            String idPagina = padre.GetType().Name;
            IdControl = idPagina;
            MostrarMensaje(TituloMensaje, ContenidoMensaje, TipoImagenMensaje, TipoBotonesMensaje, idPagina, accion);
        }

        // con  Id idependiente

        public void MostrarMensaje(string TituloMensaje, string ContenidoMensaje, TipoImagen TipoImagenMensaje, BotonesMensaje TipoBotonesMensaje, String idPagina, EventHandler accion)
        {
            IdControl = idPagina;
            AdministradorSistema.ControaldorEventosActual.InscribirMetodoEventHandler(idPagina, accion);
            mostrar(TituloMensaje, ContenidoMensaje, TipoImagenMensaje, TipoBotonesMensaje, idPagina);
        }

        private  void mostrar(string TituloMensaje, string ContenidoMensaje, TipoImagen TipoImagenMensaje, BotonesMensaje TipoBotonesMensaje, string id)
		{
			this.TituloMensaje = TituloMensaje;
			this.ContenidoMensaje = ContenidoMensaje;
			this.TipoImagenMensaje = TipoImagenMensaje;
			this.TipoBotonesMensaje = TipoBotonesMensaje;
 
            switch (TipoBotonesMensaje)
			{
				case BotonesMensaje.Aceptar:
					{
						this.ahrefBoton1.Text = this.TextoBotonAceptar;
   
                        this.ahrefBoton1.Visible = true;
						this.ahrefBoton2.Visible = false;
						this.ahrefBoton3.Visible = false;
						break;
					}
				case BotonesMensaje.AceptarCancelar:
					{
						this.ahrefBoton1.Text = this.TextoBotonAceptar;
						this.ahrefBoton2.Text = this.TextoBotonCancelar;
						this.ahrefBoton1.Visible = true;
						this.ahrefBoton2.Visible = true;
						this.ahrefBoton3.Visible = false;
						break;
					}
				case BotonesMensaje.AnularReintentarOmitir:
					{
						this.ahrefBoton1.Text = this.TextoBotonAnular;
						this.ahrefBoton2.Text = this.TextoBotonReintentar;
						this.ahrefBoton3.Text = this.TextoBotonOmitir;
						this.ahrefBoton1.Visible = true;
						this.ahrefBoton2.Visible = true;
						this.ahrefBoton3.Visible = true;
						break;
					}
				case BotonesMensaje.ReintentarCancelar:
					{
						this.ahrefBoton1.Text = this.TextoBotonReintentar;
						this.ahrefBoton2.Text = this.TextoBotonCancelar;
						this.ahrefBoton1.Visible = true;
						this.ahrefBoton2.Visible = true;
						this.ahrefBoton3.Visible = false;
						break;
					}
				case BotonesMensaje.SiNo:
					{
						this.ahrefBoton1.Text = this.TextoBotonSi;
						this.ahrefBoton2.Text = this.TextoBotonNo;
						this.ahrefBoton1.Visible = true;
						this.ahrefBoton2.Visible = true;
						this.ahrefBoton3.Visible = false;
						break;
					}
				case BotonesMensaje.SiNoCancelar:
					{
                        //this.ahrefBoton1.InnerText = this.TextoBotonSi;
                        this.ahrefBoton1.Text = this.TextoBotonSi;
                        this.ahrefBoton2.Text = this.TextoBotonNo;
						this.ahrefBoton3.Text = this.TextoBotonCancelar;
						this.ahrefBoton1.Visible = true;
						this.ahrefBoton2.Visible = true;
						this.ahrefBoton3.Visible = true;
						break;
					}
			}

			divIconoMensajeAviso.Visible = true;

			switch (TipoImagenMensaje)
			{
				case TipoImagen.Advertencia:
					{
						divIconoMensajeAviso.Attributes.Add("class", "iconoMensajeAdvertencia");
						break;
					}
				case TipoImagen.Error:
					{
						divIconoMensajeAviso.Attributes.Add("class", "iconoMensajeError");
						break;
					}
				case TipoImagen.Informativo:
					{
						divIconoMensajeAviso.Attributes.Add("class", "iconoMensajeAviso");
						break;
					}
				case TipoImagen.Ninguno:
					{
						divIconoMensajeAviso.Visible = false;
						break;
					}
				case TipoImagen.Pregunta:
					{
						divIconoMensajeAviso.Attributes.Add("class", "iconoMensajePregunta");
						break;
					}
			}


            ahrefBoton1.CausesValidation = false;
            ahrefBoton2.CausesValidation = false;
            ahrefBoton3.CausesValidation = false;
			if (this.ComportamientoEventos == TipoEvento.Cliente)
			{
                ahrefBoton1.Attributes.Remove("OnClick");
                ahrefBoton2.Attributes.Remove("OnClick");
                ahrefBoton3.Attributes.Remove("OnClick");
                switch (TipoBotonesMensaje)
				{
					case BotonesMensaje.Aceptar:
						{
							ahrefBoton1.Attributes.Add("OnClick", "CerrarMensaje('" + this.NombreInstanciaCliente + "');" + this.EventoClienteAceptar);
							break;
						}
					case BotonesMensaje.AceptarCancelar:
						{
							ahrefBoton1.Attributes.Add("OnClick", "CerrarMensaje('" + this.NombreInstanciaCliente + "');" + this.EventoClienteAceptar);
							ahrefBoton2.Attributes.Add("OnClick", "CerrarMensaje('" + this.NombreInstanciaCliente + "');" + this.EventoClienteCancelar);
							break;
						}
					case BotonesMensaje.AnularReintentarOmitir:
						{
							ahrefBoton1.Attributes.Add("OnClick", "CerrarMensaje('" + this.NombreInstanciaCliente + "');" + this.EventoClienteAnular);
							ahrefBoton2.Attributes.Add("OnClick", "CerrarMensaje('" + this.NombreInstanciaCliente + "');" + this.EventoClienteReintentar);
							ahrefBoton3.Attributes.Add("OnClick", "CerrarMensaje('" + this.NombreInstanciaCliente + "');" + this.EventoClienteOmitir);
							break;
						}
					case BotonesMensaje.ReintentarCancelar:
						{
							ahrefBoton1.Attributes.Add("OnClick", "CerrarMensaje('" + this.NombreInstanciaCliente + "');" + this.EventoClienteReintentar);
							ahrefBoton2.Attributes.Add("OnClick", "CerrarMensaje('" + this.NombreInstanciaCliente + "');" + this.EventoClienteCancelar);
							break;
						}
					case BotonesMensaje.SiNo:
						{
							ahrefBoton1.Attributes.Add("OnClick", "CerrarMensaje('" + this.NombreInstanciaCliente + "');" + this.EventoClienteSi);
							ahrefBoton2.Attributes.Add("OnClick", "CerrarMensaje('" + this.NombreInstanciaCliente + "');" + this.EventoClienteNo);
							break;
						}
					case BotonesMensaje.SiNoCancelar:
						{
							ahrefBoton1.Attributes.Add("OnClick", "CerrarMensaje('" + this.NombreInstanciaCliente + "');" + this.EventoClienteSi);
							ahrefBoton2.Attributes.Add("OnClick", "CerrarMensaje('" + this.NombreInstanciaCliente + "');" + this.EventoClienteNo);
							ahrefBoton3.Attributes.Add("OnClick", "CerrarMensaje('" + this.NombreInstanciaCliente + "');" + this.EventoClienteCancelar);
							break;
						}
				}
			}
            this.Visible = true;
		}


        public void MostrarMensajeError(BitacoraError bitacora, Object padre, DelegadoRespuestaGeneral accion)
        {
            _bitacoraError = bitacora;
            ContenidoMensaje = bitacora.Mensaje;
            IdControl = padre.GetType().Name;
            TituloMensaje = "Incidencia";
            MostrarMensaje(TituloMensaje, ContenidoMensaje, TipoImagen.Error, BotonesMensaje.Aceptar, IdControl, accion);
            GenerarMensajeError();
            this.ahrefBoton2.Text = "Ver  Detalle";
            this.ahrefBoton2.Visible = true;
        }


        public void GenerarMensajeError()
        {
            Control contenedorDetalle = (Control)DetalleTextoMensaje;
            if (contenedorDetalle != null  && _bitacoraError != null)
            {
    
                Label TituloDescricion = new Label();
                TituloDescricion.Text = "Descripcion:";
                contenedorDetalle.Controls.Add(TituloDescricion);   

                Label textoMensaje = new Label();
                textoMensaje.CssClass = "textoMensaje";
                textoMensaje.Text = _bitacoraError.Descripcion ;
                contenedorDetalle.Controls.Add(textoMensaje);

                HtmlGenericControl nl = new HtmlGenericControl("br");
                contenedorDetalle.Controls.Add(nl);

                Label TituloReferencia = new Label();
                TituloReferencia.Text = "Referencia:";
                contenedorDetalle.Controls.Add(TituloReferencia);

                Label textoReferencia = new Label();
                textoReferencia.CssClass = "textoMensaje";
                textoReferencia.Text = _bitacoraError.Referencia;
                contenedorDetalle.Controls.Add(textoReferencia);
 
            }
        }
        protected void EventoClickBoton1(object sender, EventArgs e)
        {
            this.Visible = false;
            SeleccionarRespuesta(sender, e);

        }

        protected void EventoClickBoton2(object sender, EventArgs e)
        {
            if (_bitacoraError != null)
            {
                this.Visible = true;
                DetalleTextoMensaje.Visible = true;
            }

            SeleccionarRespuesta(sender, e);
        }
        protected void EventoClickBoton3(object sender, EventArgs e)
        {
            this.Visible = false;
            SeleccionarRespuesta(sender, e);
        }

        public void SeleccionarRespuesta(object sender, EventArgs e)
        {


            LinkButton accion = (LinkButton)sender;

            //AdministradorSistema.ControaldorEventosActual.GenerarEvento(IdControl, sender, accion.Text);
            //AdministradorSistema.ControaldorEventosActual.AnularMetodo(IdControl);
            //AdministradorSistema.ControaldorEventosActual.GenerarEvento(IdControl, sender,(EventArgs) e);
            //RespuestaMensaje(sender, e);
            String idPagina = accion.Page.GetType().Name;

            AdministradorSistema.ControaldorEventosActual.GenerarEvento(idPagina, sender, accion.Text);
            AdministradorSistema.ControaldorEventosActual.AnularMetodo(idPagina);

        }
    }
}