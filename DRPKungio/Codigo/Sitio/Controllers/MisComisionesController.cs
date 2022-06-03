using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sitio.Models;
using Sitio.Comun.Clases;

namespace Sitio.Controllers
{
    public class MisComisionesController : Controller
    {
        // GET: MisComisiones
        private Modelo db = new Modelo();
     
        public async Task<ActionResult> Index()
        {
            string ClaveAplicacion = "Acceso";
            AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.SesionSistemaActual.ClaveAplicacion = ClaveAplicacion;
            AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.IniciarSesionUsuario();
            dynamic idUsuario = 2;
            string idUsuarioS = AdministradorSistema.ControaldorAplicacion.AdministradorSeguridad.ParametrosSeguridadActual.IdUsuario;
            if (idUsuarioS == null || idUsuarioS == "")
                idUsuario = 2;
            else
                idUsuario = int.Parse(idUsuarioS);
            dynamic resultado = db.ConsultarComisionesPorSocio(idUsuario, "Socio");
            return View(resultado);
        }
    }
}