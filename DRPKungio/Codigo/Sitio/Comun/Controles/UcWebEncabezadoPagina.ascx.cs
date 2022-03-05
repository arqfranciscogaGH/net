using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//  administrador  de aplicaciones  y  seguridad
using MeNet.Seguridad.Administrador;
//  clases  comunes
using Sitio.Comun.Clases;

using System.Configuration;

namespace Sitio.Comun.Controles
{
    public partial class UcWebEncabezadoPagina : System.Web.UI.UserControl
    {
        public string Usuario
        {
            set {
                if (value != null && value != string.Empty)
                {
                    this.NombreUsuario.InnerText = value;
                }
                else
                    this.NombreUsuario.InnerText = "Usuario";
            }
            get { return this.NombreUsuario.InnerText; }
        }

        public string Perfil
        {
            set
            {
                if (value != null && value != string.Empty)
                {
                    this.NombrePerfil.InnerText = value;
                }
                else
                    this.NombrePerfil.InnerText = "Perfil";
            }
            get { return this.NombrePerfil.InnerText; }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void IdBotonElementosSeguridad_Click(object sender, EventArgs e)
        {
            String pagina = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.SesionSistemaActual.PaginaAcceso;
            AdministradorSistema.CerrarSesion();
            Response.Redirect(pagina);
            //Response.Redirect("~/Acceso.aspx");

            //if  (IdContenedorSeguridad.Visible)
            //     IdContenedorSeguridad.Visible = false;
            //else
            //    IdContenedorSeguridad.Visible = true;
        }

    }
}