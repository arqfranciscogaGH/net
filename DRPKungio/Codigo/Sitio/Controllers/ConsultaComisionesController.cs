using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sitio.Models;

namespace Sitio.Controllers
{
    public class ConsultaComisionesController : Controller
    {
        private Modelo db = new Modelo();
        // GET: ConsultaComisiones
        public async Task<ActionResult> Index()
        {
            dynamic resultado = db.ConsultarComisiones();
            return View( resultado);
        }
    }
}