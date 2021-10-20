using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    public class InfractorController : Controller
    {
        // GET: Infractor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Nuevo() 
        {
            return View();
        }
    }
}
