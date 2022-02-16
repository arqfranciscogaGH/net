using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitio.Comun.Controles
{
    public partial class ucWebBarraProgreso : System.Web.UI.UserControl
    {
        #region Propiedades
        
        
        public string MensajeBarraProceso
        {
            set {
                if  (value ==null || value == string.Empty)
                    this.divMensajeBarraProceso.InnerText = "Procesando, espero unmomento por favor...";
                  else
                    this.divMensajeBarraProceso.InnerText = value;
            }
            get { return this.divMensajeBarraProceso.InnerText;     }
        }
        public string ImagenProgreso
        {
            set {
                if (value == null || value == string.Empty)
                    this.divImagenProgreso.Attributes["class"] = "ImagenProgreso icon-spin4 animate-spin";
                else
                    this.divImagenProgreso.Attributes["class"] = value; }
            get { return this.divImagenProgreso.Attributes["class"]; }
        }

        #endregion

        #region Eventos  de  control  o pagina

        protected void Page_Load(object sender, EventArgs e)
        {
    
        }

        #endregion

        #region Métodos

        public void Activar()
        {
            ContenedorBarraProceso.Visible = true;
            this.ContenedorBarraProceso.Attributes["opacity"] = "1";
        }
        public void DesActivar()
        {
            ContenedorBarraProceso.Visible = false;
            this.ContenedorBarraProceso.Attributes["opacity"] = "0";
        }
        #endregion
        
    }
}