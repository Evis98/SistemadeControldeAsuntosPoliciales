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
                if(Session["Rol"].ToString() != "4") { 
              
                    return View();}
                else
                {
                    
                    Session["Error"] = "Usuario no autorizado";
                    return Redirect("~/Error/Error.cshtml");
                }
            }
            else
            {
                Session["Error"] = "Solicitud no procesada";
                return Redirect("~/Error/Error.cshtml");
            }
        }
    }
}