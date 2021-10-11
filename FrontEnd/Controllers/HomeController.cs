using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        //Devuelve la pantalla inicial
        public ActionResult Index()
        {
            return View();
        }

    }
}