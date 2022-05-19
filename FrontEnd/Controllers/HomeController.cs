using BackEnd;
using BackEnd.DAL;
using FrontEnd.Models.ViewModels;
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
       
            
            if (Session["userID"] != null)
            {
                return View();
            }
            else
            {
                return  Redirect("~/Shared/Error.cshtml");
            }
            
        }
        public ActionResult SetSession()
        {
            Session["Parte"] = null;
            return Json(new { status = "success" });
        }
        public ActionResult SetSessionActaHallazgo()
        {
            Session["ActaHallazgo"] = null;
            return Json(new { status = "success" });
        }

    }

}
