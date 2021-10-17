using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using BackEnd.DAL;
using BackEnd;
using FrontEnd.Models;
using FrontEnd.Models.ViewModels;


namespace FrontEnd.Controllers
{
    public class ArmaController : Controller
    {
        // GET: Arma
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detalle()
        {
            return View();
        }

        public ActionResult Nuevo()
        {
            return View();
        }

    }
}