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

        //Metodos Útiles
        public Usuarios ConvertirUsuario(UsuarioViewModel modelo)
        {
            usuarioDAL = new UsuarioDAL();
            rolDAL = new RolDAL();
            Usuarios usuario = new Usuarios
            {
                idUsuario = modelo.IdUsuario,
                nombre = modelo.Nombre.ToUpper(),
                cedula = modelo.Cedula.ToUpper(),
                usuario = modelo.UserName,
                rol = rolDAL.GetRol(modelo.Rol).idRol
            };

            return usuario;
        }

        public UsuarioViewModel CargarUsuario(Usuarios usuario)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            usuarioDAL = new UsuarioDAL();
            rolDAL = new RolDAL();
            UsuarioViewModel modelo = new UsuarioViewModel
            {
                IdUsuario = usuario.idUsuario,
                Nombre = usuario.nombre,
                Cedula = usuario.cedula,
                UserName = usuario.usuario,
                Rol = (int)usuario.rol,
                VsitaRol = rolDAL.GetRol(usuario.rol).descripcion
            };

            return modelo;
        }
        public Auditorias ConvertirAuditoria(AuditoriaViewModel modelo)
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
                idUsuario = modelo.IdUsuario,

            };
        }

        //Metodos de las Vistas
        public ActionResult Index(string filtrosSeleccionado, string busqueda)
        {
            if (Session["userID"] != null)
            {
                usuarioDAL = new UsuarioDAL();
                tablaGeneralDAL = new TablaGeneralDAL();

                //Carga combobox busqueda
                List<TablaGeneral> comboindex = tablaGeneralDAL.Get("Usuarios", "index");
                List<SelectListItem> items = comboindex.ConvertAll(d =>
                {
                    return new SelectListItem()
                    {
                        Text = d.descripcion
                    };
                });
                ViewBag.items = items;

                //Carga lista de policias
                List<UsuarioViewModel> usuarios = new List<UsuarioViewModel>();
                List<UsuarioViewModel> usuariosFiltrados = new List<UsuarioViewModel>();
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
                UsuarioViewModel modelo = new UsuarioViewModel
                {
                    TiposDeRol = rolDAL.Get().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.idRol.ToString() })
                };
                return View(modelo);
            }
            else
            {
                return Redirect("~/Shared/Error");
            }
        }

        [HttpPost]
        public ActionResult Nuevo(UsuarioViewModel modelo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            rolDAL = new RolDAL();

            AuditoriaViewModel auditoria_model = new AuditoriaViewModel
            {
                Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "1").idTablaGeneral,
                IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "16").idTablaGeneral,
                IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario
            };

            try
            {
                if (ModelState.IsValid)
                {
                    int errores = 0;
                    if (usuarioDAL.CedulaUsuarioExiste(modelo.Cedula))
                    {
                        ModelState.AddModelError(nameof(modelo.Cedula), "La cédula ingresada ya existe");
                        errores++;
                    }
                    if (usuarioDAL.UsernameUsuarioExiste(modelo.UserName))
                    {
                        ModelState.AddModelError(nameof(modelo.UserName), "El username ingresada ya existe");
                        errores++;
                    }
                    if (errores == 0)
                    {
                        usuarioDAL.Add(ConvertirUsuario(modelo));
                        auditoria_model.IdElemento = usuarioDAL.GetUsuarioCedula(modelo.Cedula).idUsuario;
                        auditoriaDAL.Add(ConvertirAuditoria(auditoria_model));
                        int aux = usuarioDAL.GetUsuarioCedula(modelo.Cedula).idUsuario;
                        return Redirect("~/Usuario/Detalle/" + aux);
                    }
                }
                modelo.TiposDeRol = rolDAL.Get().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.idRol.ToString() });
                return View(modelo);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Editar(int id)
        {
            usuarioDAL = new UsuarioDAL();
            rolDAL = new RolDAL();
            UsuarioViewModel modelo = CargarUsuario(usuarioDAL.GetUsuario(id));
            modelo.TiposDeRol = rolDAL.Get().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.idRol.ToString() });
            return View(modelo);
        }

        [HttpPost]
        public ActionResult Editar(UsuarioViewModel modelo)
        {
            usuarioDAL = new UsuarioDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            auditoriaDAL = new AuditoriaDAL();
            rolDAL = new RolDAL();

            AuditoriaViewModel auditoria_model = new AuditoriaViewModel
            {
                Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "2").idTablaGeneral,
                IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "16").idTablaGeneral,
                IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario
            };

            try
            {
                if (ModelState.IsValid)
                {
                    int errores = 0;
                    if (usuarioDAL.CedulaUsuarioExiste(modelo.Cedula) && modelo.Cedula != usuarioDAL.GetUsuario(modelo.IdUsuario).cedula)
                    {
                        ModelState.AddModelError(nameof(modelo.Cedula), "La cédula ingresada ya existe");
                        errores++;
                    }
                    if (usuarioDAL.UsernameUsuarioExiste(modelo.UserName) && modelo.UserName != usuarioDAL.GetUsuario(modelo.IdUsuario).usuario)
                    {
                        ModelState.AddModelError(nameof(modelo.UserName), "El username ingresada ya existe");
                        errores++;
                    }
                    if (errores == 0)
                    {
                        usuarioDAL.Edit(ConvertirUsuario(modelo));
                        auditoria_model.IdElemento = usuarioDAL.GetUsuarioCedula(modelo.Cedula).idUsuario;
                        auditoriaDAL.Add(ConvertirAuditoria(auditoria_model));
                        return Redirect("~/Usuario/Detalle/" + modelo.IdUsuario);
                    }
                }
                modelo.TiposDeRol = rolDAL.Get().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.idRol.ToString() });
                return View(modelo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult Detalle(int id)
        {
            usuarioDAL = new UsuarioDAL();
            Session["idUsuario"] = id;
            //Session["nombrePolicia"] = usuarioDAL.GetUsuario(id).nombre;
            Session["auditoria"] = usuarioDAL.GetUsuario(id).nombre;
            Session["tabla"] = "Usuario";
            UsuarioViewModel modelo = CargarUsuario(usuarioDAL.GetUsuario(id));
            return View(modelo);
        }
    }
}
