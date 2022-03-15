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
        IUsuarioDAL usuarioDAL;
        //Devuelve la pantalla inicial
        public ActionResult Index()
        {
            usuarioDAL = new UsuarioDAL();
            var usuario =  usuarioDAL.GetUsuario(2001);          
            if(usuario != null)
            {
                Session.Add("Username", usuario.nombre);
            }
            return View();
        }

    }
}