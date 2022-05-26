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
    public class UsuarioController : Controller
    {
        
        ITablaGeneralDAL tablaGeneralDAL;
        IAuditoriaDAL auditoriaDAL;
        IUsuarioDAL usuarioDAL;
        IRolDAL rolDAL;

        public Usuarios ConvertirUsuario(UsuarioViewModel modelo)
        {
            usuarioDAL = new UsuarioDAL();
            rolDAL = new RolDAL();
            Usuarios usuario = new Usuarios();

            usuario.idUsuario = modelo.IdUsuario;
            usuario.nombre = modelo.Nombre;
            usuario.cedula = modelo.Cedula;
            usuario.usuario = modelo.UserName;
            usuario.rol = rolDAL.GetRol(modelo.Rol).idRol;

            return usuario;
        }

        public UsuarioViewModel CargarUsuario(Usuarios usuario) 
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            usuarioDAL = new UsuarioDAL();
            rolDAL = new RolDAL();
            UsuarioViewModel modelo = new UsuarioViewModel();

            modelo.IdUsuario = usuario.idUsuario;
            modelo.Nombre = usuario.nombre;
            modelo.Cedula = usuario.cedula;
            modelo.UserName = usuario.usuario;
            modelo.Rol = (int)usuario.rol;
            modelo.VsitaRol = rolDAL.GetRol(usuario.rol).descripcion;

            return modelo;
        }


        public ActionResult Index(string filtrosSeleccionado, string busqueda)
        {
            if (Session["userID"] != null)
            {
                usuarioDAL = new UsuarioDAL();
                tablaGeneralDAL = new TablaGeneralDAL();
                List<UsuarioViewModel> usuarios = new List<UsuarioViewModel>();
                List<UsuarioViewModel> usuariosFiltrados = new List<UsuarioViewModel>();
                List<TablaGeneral> comboindex = tablaGeneralDAL.Get("Usuarios", "index");
                List<SelectListItem> items = comboindex.ConvertAll(d =>
                {
                    return new SelectListItem()
                    {
                        Text = d.descripcion
                    };
                });
                ViewBag.items = items;
                foreach (Usuarios usuario in usuarioDAL.Get())
                {
                    usuarios.Add(CargarUsuario(usuario));
                }
                if (busqueda != null)
                {
                    foreach (UsuarioViewModel usuario in usuarios)
                    {
                        if (filtrosSeleccionado == "Cédula")
                        {
                            if (usuario.Cedula.Contains(busqueda))
                            {
                                usuariosFiltrados.Add(usuario);
                            }
                        }
                        if (filtrosSeleccionado == "Nombre")
                        {
                            if (usuario.Nombre.Contains(busqueda))
                            {
                                usuariosFiltrados.Add(usuario);
                            }
                        }
                    }
                    usuarios = usuariosFiltrados;
                }
                return View(usuarios.OrderBy(x => x.Nombre).ToList());
            }
            else
            {
                return Redirect("~/Shared/Error");
            }
        }



        public ActionResult Nuevo()
        {
            if (Session["userID"] != null)
            {
                rolDAL = new RolDAL();
                UsuarioViewModel modelo = new UsuarioViewModel();
                modelo.TiposDeRol = rolDAL.Get().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.idRol.ToString() });
                return View(modelo);
            }
            else
            {
                return Redirect("~/Shared/Error");
            }
        }

        [HttpPost]
        public ActionResult Nuevo(UsuarioViewModel model)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            model.CedulaUsuarioFiltrada = usuarioDAL.GetCedulaUsuario(model.Cedula);
            model.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "1").idTablaGeneral;
            model.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "1").idTablaGeneral;
            model.IdUsuarioAuditoria = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario;
            try
            {
                if (!usuarioDAL.CedulaUsuarioExiste(model.Cedula))
                {
                    if (ModelState.IsValid)
                    {
                        usuarioDAL.Add(ConvertirUsuario(model));
                        model.IdElemento = usuarioDAL.GetUsuarioCedula(model.Cedula).idUsuario;
                        auditoriaDAL.Add(ConvertirAuditoria(model));
                        int aux = usuarioDAL.GetUsuarioCedula(model.Cedula).idUsuario;
                        return Redirect("~/Usuario/Detalle/" + aux);
                    }
                }
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Editar(int id)
        {
            usuarioDAL = new UsuarioDAL();
            UsuarioViewModel modelo = CargarUsuario(usuarioDAL.GetUsuario(id));
            modelo.TiposDeRol = rolDAL.Get().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.idRol.ToString() });
            return View(modelo);
        }

        [HttpPost]
        public ActionResult Editar(UsuarioViewModel model)
        {
            usuarioDAL = new UsuarioDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            auditoriaDAL = new AuditoriaDAL();
            model.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "2").idTablaGeneral;
            model.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "1").idTablaGeneral;
            model.IdUsuarioAuditoria = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario;
            try
            {
                if (ModelState.IsValid)
                {
                    usuarioDAL.Edit(ConvertirUsuario(model));
                    model.IdElemento = usuarioDAL.GetUsuarioCedula(model.Cedula).idUsuario;
                    auditoriaDAL.Add(ConvertirAuditoria(model));
                    return Redirect("~/Usuario/Detalle/" + model.IdUsuario);
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult Detalle(int id)
        {
            usuarioDAL = new UsuarioDAL();
            Session["idPolicia"] = id;
            Session["nombrePolicia"] = usuarioDAL.GetUsuario(id).nombre;
            UsuarioViewModel modelo = CargarUsuario(usuarioDAL.GetUsuario(id));
            return View(modelo);
        }

        public Auditorias ConvertirAuditoria(UsuarioViewModel modelo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            usuarioDAL = new UsuarioDAL();
            return new Auditorias
            {
                idAuditoria = modelo.IdAuditoria,
                idCategoria = modelo.IdCategoria,
                idElemento = modelo.IdElemento,
                fecha = DateTime.Now,
                accion = modelo.Accion,
                idUsuario = modelo.IdUsuarioAuditoria,

            };
        }

    }
}
