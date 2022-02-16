using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitio.Comun.Controles
{
    public partial class UcWebBanner : System.Web.UI.UserControl
    {
        private string _Activo = string.Empty;
        public string Activo { get { return _Activo; } set { _Activo = value; } }

        private string _Titulo = string.Empty;
        public string Titulo { get { return _Titulo; } set { _Titulo = value; } }

        private string _SubTitulo = string.Empty;
        public string SubTitulo { get { return _SubTitulo; } set { _SubTitulo = value; } }

        private string _Imagen = string.Empty;
        public string Imagen { get { return _Imagen; } set { _Imagen = value; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            Imagen = "Comun/Img/fondoIT.jpg";

            this.ConnedorBanner.Visible =false;
            if (Activo != string.Empty && (Activo.ToUpper()=="SI"  || Activo.ToUpper() == "TRUE") )
                this.ConnedorBanner.Visible = true;
            if (Titulo != string.Empty )
               ContedorTitulo.InnerText = Titulo;
            if (SubTitulo != string.Empty)
               ContedorSubTitulo.InnerText = SubTitulo;
            if (Imagen != string.Empty)
            {
                Image i = new Image();
                i.ImageUrl = Imagen;
                //ConnedorBanner.Controls.Add(i);
            }
                
        }
    }
}