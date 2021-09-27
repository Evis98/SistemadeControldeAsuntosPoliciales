using System.Linq;
using System.Web;
using System.Web.Mvc;
using BackEnd.DAL;

namespace SistemadeControldeAsuntosPoliciales.Controllers
{
    public class PoliciaController : Controller
    {
        // GET: Policia
        //private PoliciaDAL dalPolicia;
        public ActionResult Index()
        {
           

            return View();
        }
    }
}