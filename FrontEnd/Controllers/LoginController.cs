using BackEnd.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FrontEnd.Models;
using FrontEnd.Models.ViewModels;
using BackEnd;

namespace FrontEnd.Controllers
{
    public class LoginController : Controller
    {
        private IUsuarioDAL usuarioDAL;
        // GET: Login
        public ActionResult Index()
        {
            UsuarioViewModel userView = new UsuarioViewModel();
            userView.LoginErrorMessage = "";
            return View(userView);
        }

        public ActionResult Prueba()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Autherize(UsuarioViewModel userModel)
        {
            usuarioDAL = new UsuarioDAL();

            string adPath = "LDAP://munialajuela.priv/DC=munialajuela.DC=priv";

            LdapAuthentication authentication = new LdapAuthentication(adPath);

            /*if (!authentication.IsAuthenticated("munialajuela", userModel.UserName, userModel.Password))

            {
                userModel.LoginErrorMessage = "Usuario no Autorizado";
                return View("Index", userModel);

            }
            else
            {*/
            Usuarios userDetails = new Usuarios();
            if (usuarioDAL.GetUsuarioCorreo(userModel.UserName) != null)
            {
                userDetails = usuarioDAL.GetUsuarioCorreo(userModel.UserName);
            }
         
       
                if (userDetails.usuario == null)
                {
                    userModel.LoginErrorMessage = "Usuario no Autorizado";
                    return View("Index", userModel);  
                }
                if (usuarioDAL.GetRolesUsuario(userDetails.idUsuario) == false)
                {
                    userModel.LoginErrorMessage = "Usuario no tiene Roles";
                    return View("Index", userModel);
                }

                Session["userID"] = userDetails.idUsuario;
                Session["userName"] = userDetails.usuario;
                Session["Nombre"] = userDetails.nombre;
                Session["cedula"] = userDetails.cedula;
                var authTicket = new FormsAuthenticationTicket(userDetails.usuario, true, 100000);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName,
                                            FormsAuthentication.Encrypt(authTicket));
                Response.Cookies.Add(cookie);
                var name = User.Identity.Name;
                return RedirectToAction("Index", "Home");
            //}
        }

        public ActionResult LogOut()
        {
            //int userId = (int)Session["userID"];
            Session.Clear();
            Session.Abandon();

            //clear authentication cookie
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);

            //clear session cookie
            HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId","");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie2);
            return RedirectToAction("Index","Login");
        }
    }
}