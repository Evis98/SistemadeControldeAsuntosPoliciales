using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BackEnd.DAL;
using BackEnd;
using FrontEnd.Models;
using FrontEnd.Models.ViewModels;

namespace FrontEnd.Controllers
{
    public class RequisitoController : Controller
    {
        IRequisitoDAL requisitoDAL;
        IPoliciaDAL policiaDAL;
        ITablaGeneralDAL tablaGeneralDAL;
        IAuditoriaDAL auditoriaDAL;
        IUsuarioDAL usuarioDAL;
        public Requisitos ConvertirRequisito(RequisitoViewModel modelo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            return new Requisitos
            {
                idRequisito = modelo.IdRequisito,
                idPolicia = (int)Session["idPolicia"],
                detalles = modelo.Detalles,
                fechaVencimiento = modelo.FechaVencimiento,
                tipoRequisito = tablaGeneralDAL.GetCodigo("Requisitos", "tipoRequisito", modelo.TipoRequisito.ToString()).idTablaGeneral,
                imagen = modelo.Imagen
            };
        }

        public RequisitoViewModel CargarRequisito(Requisitos requisito)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            string fechaVencimiento = null;
            if (requisito.fechaVencimiento.HasValue)
            {
                fechaVencimiento = requisito.fechaVencimiento.Value.ToShortDateString();
            }
            return new RequisitoViewModel
            {
                Imagen = requisito.imagen,
                IdRequisito = requisito.idRequisito,
                FechaVencimiento = requisito.fechaVencimiento,
                VistaFechaVencimiento = fechaVencimiento,
                TipoRequisito = int.Parse(tablaGeneralDAL.Get(requisito.tipoRequisito).codigo),
                VistaTipoRequisito = tablaGeneralDAL.Get(requisito.tipoRequisito).descripcion,
                Detalles = requisito.detalles,
                IdPolicia = requisito.idPolicia,
                NombrePolicia = policiaDAL.GetPolicia(requisito.idPolicia).nombre,
            };
        }

        public void Autorizar()
        {
            if (Session["userID"] != null)
            {
                if (Session["Rol"].ToString() == "1")
                {
                    Session["Error"] = "Usuario no autorizado";
                    Response.Redirect("~/Error/ErrorUsuario.cshtml");
                }
            }
            else
            {
                Response.Redirect("~/Login/Index");
            }
        }
        public void AutorizarEditar()
        {
            if (Session["userID"] != null)
            {
                if (Session["Rol"].ToString() == "4" || Session["Rol"].ToString() == "1")
                {
                    Session["Error"] = "Usuario no autorizado";
                    Response.Redirect("~/Error/ErrorUsuario.cshtml");
                }
                else
                {
                    Session["Error"] = "wwwww";
                }
            }
            else
            {
                Response.Redirect("~/Login/Index");
            }
        }

        //Devuelve la página con el listado de todos los requisitos creados
        public ActionResult Index(string filtrosSeleccionado, string busqueda, string tiposRequisito)
        {
            Autorizar();
            requisitoDAL = new RequisitoDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            List<RequisitoViewModel> requisitos = new List<RequisitoViewModel>();
            List<RequisitoViewModel> requisitosFiltrados = new List<RequisitoViewModel>();
            List<TablaGeneral> comboindex1 = tablaGeneralDAL.Get("Requisitos", "index");
            List<TablaGeneral> comboindex2 = tablaGeneralDAL.Get("Requisitos", "tipoRequisito");
            List<SelectListItem> items1 = comboindex1.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.descripcion
                };
            });
            ViewBag.items1 = items1;
            List<SelectListItem> items2 = comboindex2.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.descripcion
                };
            });
            ViewBag.items2 = items2;
            foreach (Requisitos requisito in requisitoDAL.Get())
            {
                requisitos.Add(CargarRequisito(requisito));
            }
            if (busqueda != null)
            {
                foreach (RequisitoViewModel requisito in requisitos)
                {
                    if (filtrosSeleccionado == "Detalle de Requisito")
                    {
                        if (requisito.Detalles.Contains(busqueda))
                        {
                            requisitosFiltrados.Add(requisito);
                        }
                    }
                    if (filtrosSeleccionado == "Tipo de Requisito")
                    {
                        if (tablaGeneralDAL.GetCodigo("Requisitos", "tipoRequisito", requisito.TipoRequisito.ToString()).descripcion.Contains(tiposRequisito))
                        {
                            requisitosFiltrados.Add(requisito);
                        }
                    }
                }
                requisitos = requisitosFiltrados;
            }
            return View(requisitos.OrderBy(x => x.FechaVencimiento).ToList());
            }
           

        //*Devuelve la página con el listado de todos los requisitos creados para el policía seleccionado
        public ActionResult Listado(int id, string filtrosSeleccionado, string busqueda, string tiposRequisito)
        {
            AutorizarEditar();
            requisitoDAL = new RequisitoDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            List<RequisitoViewModel> requisitos = new List<RequisitoViewModel>();
            List<RequisitoViewModel> requisitosFiltrados = new List<RequisitoViewModel>();
            List<TablaGeneral> comboindex1 = tablaGeneralDAL.Get("Requisitos", "index");
            List<TablaGeneral> comboindex2 = tablaGeneralDAL.Get("Requisitos", "tipoRequisito");
            List<SelectListItem> items1 = comboindex1.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.descripcion
                };
            });
            ViewBag.items1 = items1;
            List<SelectListItem> items2 = comboindex2.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.descripcion
                };
            });
            ViewBag.items2 = items2;
            foreach (Requisitos requisito in requisitoDAL.Get())
            {
                if (id == requisito.idPolicia)
                {
                    requisitos.Add(CargarRequisito(requisito));
                }
            }
            if (busqueda != null)
            {
                foreach (RequisitoViewModel requisito in requisitos)
                {
                    if (filtrosSeleccionado == "Detalle de Requisito")
                    {
                        if (requisito.Detalles.Contains(busqueda))
                        {
                            requisitosFiltrados.Add(requisito);
                        }
                    }
                    if (filtrosSeleccionado == "Tipo de Requisito")
                    {
                        if (tablaGeneralDAL.GetCodigo("Requisitos", "tipoRequisito", requisito.TipoRequisito.ToString()).descripcion.Contains(tiposRequisito))
                        {
                            requisitosFiltrados.Add(requisito);
                        }
                    }
                }
                requisitos = requisitosFiltrados;
            }
            return View(requisitos.OrderBy(x => x.FechaVencimiento).ToList());
        }

        //Devuelve la página que agrega nuevos requisitos
        public ActionResult Nuevo(int id)
        {
            AutorizarEditar();
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            RequisitoViewModel requisito = new RequisitoViewModel
            {
                TiposRequisito = tablaGeneralDAL.Get("Requisitos", "tipoRequisito").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                IdPolicia = id,
                NombrePolicia = policiaDAL.GetPolicia(id).nombre,
            };
            return View(requisito);
        }

        //Guarda la información ingresada en la página para crear requisitos
        [HttpPost]
        public void CrearCarpetaRequisitos()
        {
            policiaDAL = new PoliciaDAL();
            string folderPath = Server.MapPath(@"~\ArchivosSCAP\Policias\" + policiaDAL.GetPolicia((int)Session["idPolicia"]).cedula + " - " + policiaDAL.GetPolicia((int)Session["idPolicia"]).nombre + @"\Requisitos\");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                Console.WriteLine(folderPath);
            }
        }

        //Guarda la información ingresada en la página para crear requisitos
        [HttpPost]
        public ActionResult Nuevo(RequisitoViewModel modelo)
        {
            AutorizarEditar();
            requisitoDAL = new RequisitoDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            AuditoriaViewModel auditoria_modelo = new AuditoriaViewModel();
            auditoria_modelo.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "1").idTablaGeneral;
            auditoria_modelo.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "4").idTablaGeneral;
            auditoria_modelo.IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario;
            try
            {
                if (ModelState.IsValid)
                {
                    CrearCarpetaRequisitos();
                    string rutaSitio = Server.MapPath("~/");
                    string pathArchivo = Path.Combine(rutaSitio + @"ArchivosSCAP\Policias\" + policiaDAL.GetPolicia((int)Session["idPolicia"]).cedula + " - " + policiaDAL.GetPolicia((int)Session["idPolicia"]).nombre + @"\Requisitos\" + modelo.Detalles + " - " + policiaDAL.GetPolicia((int)Session["idPolicia"]).nombre + ".pdf");
                    Requisitos requisito = ConvertirRequisito(modelo);
                    if (modelo.FechaVencimiento != null)
                    {
                        requisito.fechaVencimiento = modelo.FechaVencimiento;
                    }
                    else
                    {
                        requisito.fechaVencimiento = null;
                    }
                    if (modelo.Archivo != null)
                    {
                        requisito.imagen = @"~\ArchivosSCAP\Policias\" + policiaDAL.GetPolicia((int)Session["idPolicia"]).cedula + " - " + policiaDAL.GetPolicia((int)Session["idPolicia"]).nombre + @"\Requisitos\" + modelo.Detalles + " - " + policiaDAL.GetPolicia((int)Session["idPolicia"]).nombre + ".pdf";
                        modelo.Archivo.SaveAs(pathArchivo);
                    }
                    else
                    {
                        requisito.imagen = null;
                    }
                    requisitoDAL.Add(requisito);
                    auditoria_modelo.IdElemento = requisitoDAL.GetRequisitoId(requisito.idRequisito).idRequisito;
                    auditoriaDAL.Add(ConvertirAuditoria(auditoria_modelo));
                    return Redirect("~/Requisito/Listado/" + Session["idPolicia"].ToString());
                }
                modelo.TiposRequisito = tablaGeneralDAL.Get("Requisitos", "tipoRequisito").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                return View(modelo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        //Muestra la información detallada del requisito seleccionado
        public ActionResult Detalle(int id)
        {
            Autorizar();
            policiaDAL = new PoliciaDAL();
            requisitoDAL = new RequisitoDAL();
            Session["idRequisito"] = id;
            Session["auditoria"] = requisitoDAL.GetRequisito(id).detalles;
            Session["tabla"] = "Requisito";
            RequisitoViewModel modelo = CargarRequisito(requisitoDAL.GetRequisito(id));
            return View(modelo);
        }

        public ActionResult DetalleIndex(int id)
        {
            Autorizar();
            policiaDAL = new PoliciaDAL();
            requisitoDAL = new RequisitoDAL();
            Session["idRequisito"] = id;
            Session["detalleRequisito"] = requisitoDAL.GetRequisito(id).detalles;
            Session["auditoria"] = requisitoDAL.GetRequisito(id).detalles;
            Session["tabla"] = "Requisito";
            RequisitoViewModel modelo = CargarRequisito(requisitoDAL.GetRequisito(id));
            return View(modelo);
        }
        //Permite la eliminación de requisitos de la base de datos
        public ActionResult Eliminar(int id)
        {
            AutorizarEditar();
            requisitoDAL = new RequisitoDAL();
            auditoriaDAL = new AuditoriaDAL();
            Requisitos requisito = requisitoDAL.GetRequisito(id);
            int? idPolicia = requisito.idPolicia;
            auditoriaDAL.Add(EliminarAuditoria(requisito.idRequisito));
            requisitoDAL.EliminaRequisito(requisito);          
            return Redirect("~/Requisito/Listado/" + idPolicia);
        }

        //Devuelve la página de edición de requisitos con sus apartados llenos
        public ActionResult Editar(int id)
        {
            AutorizarEditar();
            tablaGeneralDAL = new TablaGeneralDAL();
            requisitoDAL = new RequisitoDAL();
            RequisitoViewModel modelo = CargarRequisito(requisitoDAL.GetRequisito(id));
            modelo.TiposRequisito = tablaGeneralDAL.Get("Requisitos", "tipoRequisito").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            return View(modelo);
        }

        //Guarda la información modificada de los requisitos
        [HttpPost]
        public ActionResult Editar(RequisitoViewModel modelo)
        {
            AutorizarEditar();
            requisitoDAL = new RequisitoDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            AuditoriaViewModel auditoria_modelo = new AuditoriaViewModel();
            auditoria_modelo.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "2").idTablaGeneral;
            auditoria_modelo.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "4").idTablaGeneral;
            auditoria_modelo.IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario;
            try
            {
                if (ModelState.IsValid)
                {
                    if (modelo.Archivo != null && System.IO.File.Exists(requisitoDAL.GetRequisito(modelo.IdRequisito).imagen))
                    {
                        System.IO.File.Delete(requisitoDAL.GetRequisito(modelo.IdRequisito).imagen);
                        modelo.Archivo.SaveAs(modelo.Imagen);
                    }
                    requisitoDAL.Edit(ConvertirRequisito(modelo));
                    auditoria_modelo.IdElemento = requisitoDAL.GetRequisitoId(modelo.IdRequisito).idRequisito;
                    auditoriaDAL.Add(ConvertirAuditoria(auditoria_modelo));
                    return Redirect("~/Requisito/Listado/" + modelo.IdPolicia);
                }
                return View(ConvertirRequisito(modelo));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Auditorias ConvertirAuditoria(AuditoriaViewModel modelo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            requisitoDAL = new RequisitoDAL();
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

        public Auditorias EliminarAuditoria(int idRequisito)
        {
            AuditoriaViewModel modelo = new AuditoriaViewModel();
            tablaGeneralDAL = new TablaGeneralDAL();
            requisitoDAL = new RequisitoDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            return new Auditorias
            {
                idAuditoria = modelo.IdAuditoria,
                accion = modelo.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "4").idTablaGeneral,
                idCategoria = modelo.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "4").idTablaGeneral,
                idUsuario = modelo.IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario,
                fecha = DateTime.Now,
                idElemento = requisitoDAL.GetRequisitoId(idRequisito).idRequisito

            };
        }
    }
}