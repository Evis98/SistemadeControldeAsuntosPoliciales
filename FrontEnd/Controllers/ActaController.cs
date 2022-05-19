using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    public class ActaController : Controller
    {
        // GET: Acta
        public ActionResult Index()
        {
            if (Session["userID"] != null)
            {
                return View();
            }
            else
            {
                return Redirect("~/Shared/Error.cshtml");
            }
        }
    }
}