using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using BackEnd.DAL;
using BackEnd;
using FrontEnd.Models;
using FrontEnd.Models.ViewModels;
using System.IO;

namespace FrontEnd.Controllers
{
    public class ActaHallazgoController : Controller
    {
        ITablaGeneralDAL tablaGeneralDAL;
        IActaHallazgoDAL actaHallazgoDAL;
        IPoliciaDAL policiaDAL;
        IAuditoriaDAL auditoriaDAL;
        IUsuarioDAL usuarioDAL;
        IPersonaDAL personaDAL;

        public ActasHallazgo ConvertirActaHallazgo(ActaHallazgoViewModel modelo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            personaDAL = new PersonaDAL();

            ActasHallazgo actaHallazgo = new ActasHallazgo();
            {
                actaHallazgo.idActaHallazgo = modelo.IdActaHallazgo;
                actaHallazgo.numeroFolio = modelo.NumeroFolio;
                actaHallazgo.distrito = tablaGeneralDAL.GetCodigo("Generales", "distrito", modelo.Distrito.ToString()).idTablaGeneral;
                actaHallazgo.fechaHora = modelo.Fecha;
                actaHallazgo.avenida = modelo.Avenida;
                actaHallazgo.calle = modelo.Calle;
                actaHallazgo.otrasSenas = modelo.OtrasSenas;
                actaHallazgo.inventario = modelo.Inventario;
                actaHallazgo.estado = tablaGeneralDAL.GetCodigo("Actas", "estadoActa", modelo.Estado.ToString()).idTablaGeneral;
                actaHallazgo.observaciones = modelo.Observaciones;
                actaHallazgo.encargado = policiaDAL.GetPoliciaCedula(modelo.Encargado).idPolicia;
                actaHallazgo.tipoTestigo = tablaGeneralDAL.GetCodigo("Actas", "tipoTestigo", modelo.TipoTestigo.ToString()).idTablaGeneral;

                if (modelo.TipoTestigo != 3)
                {
                    if (modelo.TipoTestigo == 1)
                    {
                        if (modelo.IdTestigoPolicia != null && policiaDAL.CedulaPoliciaExiste(modelo.IdTestigoPolicia))
                        {
                            actaHallazgo.testigo = policiaDAL.GetPoliciaCedula(modelo.IdTestigoPolicia).idPolicia;
                        }
                        else
                        {
                            actaHallazgo.tipoTestigo = tablaGeneralDAL.GetCodigo("Actas", "tipoTestigo", "3").idTablaGeneral;
                        }
                    }
                    else if (modelo.TipoTestigo == 2)
                    {
                        if (modelo.IdTestigoPersona != null && personaDAL.IdentificacionExiste(modelo.IdTestigoPersona))
                        {
                            actaHallazgo.testigo = personaDAL.GetPersonaIdentificacion(modelo.IdTestigoPersona).idPersona;
                        }
                        else
                        {
                            actaHallazgo.tipoTestigo = tablaGeneralDAL.GetCodigo("Actas", "tipoTestigo", "3").idTablaGeneral;
                        }
                    }
                }
                actaHallazgo.supervisor = policiaDAL.GetPoliciaCedula(modelo.Supervisor).idPolicia;
            };
            return actaHallazgo;
        }
        public ActaHallazgoViewModel CargarActaHallazgo(ActasHallazgo actaHallazgo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            personaDAL = new PersonaDAL();
            ActaHallazgoViewModel actaHallazgoCarga = new ActaHallazgoViewModel();
            {
                actaHallazgoCarga.IdActaHallazgo = actaHallazgo.idActaHallazgo;
                actaHallazgoCarga.NumeroFolio = actaHallazgo.numeroFolio;
                actaHallazgoCarga.Encargado = policiaDAL.GetPolicia(actaHallazgo.encargado).cedula;
                actaHallazgoCarga.Supervisor = policiaDAL.GetPolicia(actaHallazgo.supervisor).cedula;
                actaHallazgoCarga.Distrito = int.Parse(tablaGeneralDAL.Get(actaHallazgo.distrito).codigo);
                actaHallazgoCarga.VistaDistrito = tablaGeneralDAL.Get(actaHallazgo.distrito).descripcion;
                actaHallazgoCarga.Fecha = actaHallazgo.fechaHora.Value;
                actaHallazgoCarga.Hora = actaHallazgo.fechaHora.Value;
                actaHallazgoCarga.Avenida = actaHallazgo.avenida;
                actaHallazgoCarga.Calle = actaHallazgo.calle;
                actaHallazgoCarga.OtrasSenas = actaHallazgo.otrasSenas;
                actaHallazgoCarga.Inventario = actaHallazgo.inventario;
                actaHallazgoCarga.Observaciones = actaHallazgo.observaciones;
                actaHallazgoCarga.VistaPoliciaEncargado = policiaDAL.GetPolicia(actaHallazgo.encargado).nombre;
                actaHallazgoCarga.VistaPoliciaSupervisor = policiaDAL.GetPolicia(actaHallazgo.supervisor).nombre;
                actaHallazgoCarga.Estado = int.Parse(tablaGeneralDAL.Get(actaHallazgo.estado).codigo);
                actaHallazgoCarga.VistaEstadoActa = tablaGeneralDAL.Get(actaHallazgo.estado).descripcion;
                actaHallazgoCarga.TipoTestigo = int.Parse(tablaGeneralDAL.Get(actaHallazgo.tipoTestigo).codigo);

                if (int.Parse(tablaGeneralDAL.Get(actaHallazgo.tipoTestigo).codigo) != 3 && actaHallazgo.testigo != null)
                {
                    if (int.Parse(tablaGeneralDAL.Get(actaHallazgo.tipoTestigo).codigo) == 1 && policiaDAL.GetPolicia((int)actaHallazgo.testigo) != null)
                    {
                        actaHallazgoCarga.VistaTestigo = policiaDAL.GetPolicia((int)actaHallazgo.testigo).nombre;
                        actaHallazgoCarga.IdTestigoPolicia = policiaDAL.GetPolicia((int)actaHallazgo.testigo).cedula;
                        actaHallazgoCarga.VistaIdTestigo = policiaDAL.GetPolicia((int)actaHallazgo.testigo).cedula;
                    }
                    else if (int.Parse(tablaGeneralDAL.Get(actaHallazgo.tipoTestigo).codigo) == 2 && personaDAL.GetPersona((int)actaHallazgo.testigo) != null)
                    {
                        actaHallazgoCarga.VistaTestigo = personaDAL.GetPersona((int)actaHallazgo.testigo).nombre;
                        actaHallazgoCarga.IdTestigoPersona = personaDAL.GetPersona((int)actaHallazgo.testigo).identificacion;
                        actaHallazgoCarga.VistaIdTestigo = personaDAL.GetPersona((int)actaHallazgo.testigo).identificacion;
                    }

                }
                actaHallazgoCarga.VistaTipoTestigo = tablaGeneralDAL.Get(actaHallazgo.tipoTestigo).descripcion;
            };
            return actaHallazgoCarga;
        }
        public List<PoliciaViewModel> ConvertirListaPoliciasFiltrados(List<Policias> policias)
        {
            return (from d in policias
                    select new PoliciaViewModel
                    {
                        Cedula = d.cedula,
                        Nombre = d.nombre,
                    }).ToList();
        }
        public PartialViewResult ListaPoliciasBuscar(string nombre)
        {
            List<PoliciaViewModel> policias = new List<PoliciaViewModel>();

            return PartialView("_ListaPoliciasBuscar", policias);
        }

        public PartialViewResult ListaPoliciasParcial(string nombre)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            List<Policias> policias = policiaDAL.Get();
            List<Policias> policiasFiltrados = new List<Policias>();

            if (nombre == "")
            {
                policiasFiltrados = policias;
            }
            else
            {
                foreach (Policias policia in policias)
                {
                    if (policia.nombre.Contains(nombre))
                    {
                        policiasFiltrados.Add(policia);
                    }
                }
            }
            policiasFiltrados = policiasFiltrados.OrderBy(x => x.nombre).ToList();

            return PartialView("_ListaPoliciasParcial", ConvertirListaPoliciasFiltrados(policiasFiltrados));
        }

        public List<PersonaViewModel> ConvertirListaPersonasFiltrados(List<Personas> personas)
        {
            return (from d in personas
                    select new PersonaViewModel
                    {
                        Identificacion = d.identificacion,
                        NombrePersona = d.nombre,
                    }).ToList();
        }
        public PartialViewResult ListaPersonasBuscar(string nombre)
        {
            List<PersonaViewModel> personas = new List<PersonaViewModel>();

            return PartialView("_ListaPersonasBuscar", personas);
        }

        public PartialViewResult ListaPersonasParcial(string nombre)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            personaDAL = new PersonaDAL();
            List<Personas> personas = personaDAL.Get();
            List<Personas> personasFiltradas = new List<Personas>();
            foreach (Personas persona in personas)
            {
                if (persona.nombre.Contains(nombre))
                {
                    if (tablaGeneralDAL.Get(persona.tipoIdentificacion).descripcion != "Cédula Jurídica")
                    {
                        personasFiltradas.Add(persona);
                    }
                }
            }
            personasFiltradas = personasFiltradas.OrderBy(x => x.nombre).ToList();
            return PartialView("_ListaPersonasParcial", ConvertirListaPersonasFiltrados(personasFiltradas));
        }
        public void Autorizar()
        {
            if (Session["userID"] != null)
            {
                if (Session["Rol"].ToString() == "4")
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
            }
            else
            {
                Response.Redirect("~/Login/Index");
            }
        }




        public ActionResult Index(string filtroSeleccionados, string busqueda, string busquedaFechaInicioH, string busquedaFechaFinalH)
        {
            Autorizar();
            actaHallazgoDAL = new ActaHallazgoDAL();
            policiaDAL = new PoliciaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            List<ActaHallazgoViewModel> actasHallazgo = new List<ActaHallazgoViewModel>();
            List<ActaHallazgoViewModel> actasHallazgoFiltradas = new List<ActaHallazgoViewModel>();
            List<TablaGeneral> comboindex = tablaGeneralDAL.Get("ActasHallazgo", "index");
            List<SelectListItem> items = comboindex.ConvertAll(d =>
            {               
                return new SelectListItem()
                {
                    Text = d.descripcion                                           
                };              
            });
            ViewBag.items = items;
            foreach (ActasHallazgo actaHallazgo in actaHallazgoDAL.Get())
            {
                actasHallazgo.Add(CargarActaHallazgo(actaHallazgo));
            }
            
            if (busqueda != null && filtroSeleccionados != "" )
            {
             
                foreach (ActaHallazgoViewModel actaHallazgo in actasHallazgo)
                {                   
                    if (filtroSeleccionados == "Número de Folio")
                    {
                        if (actaHallazgo.NumeroFolio.Contains(busqueda))
                        {
                            actasHallazgoFiltradas.Add(actaHallazgo);
                        }
                    }
                    if (filtroSeleccionados == "Nombre Policía Encargado")
                    {
                        if (policiaDAL.GetPoliciaCedula(actaHallazgo.Encargado).nombre.Contains(busqueda))
                        {
                            actasHallazgoFiltradas.Add(actaHallazgo);
                        }
                    }
                }
                if (filtroSeleccionados == "Fecha")
                {
                    DateTime fechaInicio = DateTime.Parse(busquedaFechaInicioH);
                    DateTime fechaFinal = DateTime.Parse(busquedaFechaFinalH);
                    if (fechaInicio < fechaFinal)
                    {
                        if (actaHallazgoDAL.GetActaHallazgoRango(fechaInicio, fechaFinal) != null)
                        {
                            foreach (ActasHallazgo actaHallazgoFecha in actaHallazgoDAL.GetActaHallazgoRango(fechaInicio, fechaFinal).ToList())
                            {
                                actasHallazgoFiltradas.Add(CargarActaHallazgo(actaHallazgoFecha));
                            }
                        }
                    }
                }                
                actasHallazgo = actasHallazgoFiltradas;                
            }            
            return View(actasHallazgo.OrderBy(x => x.NumeroFolio).ToList());       
            
}
        public ActionResult Nuevo()
        {
            Autorizar();
            tablaGeneralDAL = new TablaGeneralDAL();
            ActaHallazgoViewModel modelo = new ActaHallazgoViewModel()
            {
                Distritos = tablaGeneralDAL.Get("Generales", "distrito").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                Fecha = DateTime.Today,
                TiposTestigo = tablaGeneralDAL.Get("Actas", "tipoTestigo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo })
            };
            return View(modelo);
        }

        [HttpPost]
        public ActionResult Nuevo(ActaHallazgoViewModel model)
        {
            Autorizar();
            actaHallazgoDAL = new ActaHallazgoDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            model.Estado = int.Parse(tablaGeneralDAL.GetCodigo("Actas", "estadoActa", "1").codigo);
            model.Distritos = tablaGeneralDAL.Get("Generales", "distrito").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.TiposTestigo = tablaGeneralDAL.Get("Actas", "tipoTestigo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.NumeroFolio = (actaHallazgoDAL.GetCount() + 1).ToString() + "-" + DateTime.Now.Year;
            DateTime newDateTime = model.Fecha.Date + model.Hora.TimeOfDay;
            model.Fecha = newDateTime;
            model.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "1").idTablaGeneral;
            model.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "7").idTablaGeneral;
            model.IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario;
            try
            {
                if (ModelState.IsValid)
                {
                    actaHallazgoDAL.Add(ConvertirActaHallazgo(model));
                    model.IdElemento = actaHallazgoDAL.GetActaHallazgoFolio(model.NumeroFolio).idActaHallazgo;
                    auditoriaDAL.Add(ConvertirAuditoria(model));
                    int aux = actaHallazgoDAL.GetActaHallazgoFolio(model.NumeroFolio).idActaHallazgo;
                   
                    return Redirect("~/ActaHallazgo/Detalle/" + aux);
               
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
            Autorizar();
            actaHallazgoDAL = new ActaHallazgoDAL();
            Session["idActaHallazgo"] = id;
            Session["numeroFolio"] = actaHallazgoDAL.GetActaHallazgo(id).numeroFolio;  
            ActaHallazgoViewModel modelo = CargarActaHallazgo(actaHallazgoDAL.GetActaHallazgo(id));
           return View(modelo);
        }

        //Devuelve la página de edición de policías con sus apartados llenos
        public ActionResult Editar(int id)
        {
            AutorizarEditar();
            tablaGeneralDAL = new TablaGeneralDAL();
            actaHallazgoDAL = new ActaHallazgoDAL();
            ActaHallazgoViewModel modelo = CargarActaHallazgo(actaHallazgoDAL.GetActaHallazgo(id));
            modelo.Distritos = tablaGeneralDAL.Get("Generales", "distrito").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            modelo.Estados = tablaGeneralDAL.Get("Actas", "estadoActa").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            modelo.TiposTestigo = tablaGeneralDAL.Get("Actas", "tipoTestigo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            return View(modelo);
        }

        [HttpPost]
        public ActionResult Editar(ActaHallazgoViewModel model)
        {
            AutorizarEditar();
            actaHallazgoDAL = new ActaHallazgoDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            model.Distritos = tablaGeneralDAL.Get("Generales", "distrito").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.Estados = tablaGeneralDAL.Get("Actas", "estadoActa").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.TiposTestigo = tablaGeneralDAL.Get("Actas", "tipoTestigo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            DateTime newDateTime = model.Fecha.Date + model.Hora.TimeOfDay;
            model.Fecha = newDateTime;
            model.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "2").idTablaGeneral;
            model.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "7").idTablaGeneral;
            model.IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario;
            int estado = actaHallazgoDAL.GetActaHallazgo(model.IdActaHallazgo).estado;
            try
            {
                if (ModelState.IsValid)
                {
                    if (tablaGeneralDAL.GetCodigo("Actas","estadoActa",model.Estado.ToString()).idTablaGeneral != estado && model.IdActaHallazgo != 0)
                    {
                        auditoriaDAL.Add(CambiarEstadoAuditoria(model.IdActaHallazgo));
                    }
                    actaHallazgoDAL.Edit(ConvertirActaHallazgo(model));
                    model.IdElemento = actaHallazgoDAL.GetActaHallazgoFolio(model.NumeroFolio).idActaHallazgo;
                    auditoriaDAL.Add(ConvertirAuditoria(model));
                    return Redirect("~/ActaHallazgo/Detalle/" + model.IdActaHallazgo);
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Auditorias ConvertirAuditoria(ActaHallazgoViewModel modelo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            actaHallazgoDAL = new ActaHallazgoDAL();
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
        public Auditorias CambiarEstadoAuditoria(int idActaHallazgo)
        {
            ActaHallazgoViewModel modelo = new ActaHallazgoViewModel();
            tablaGeneralDAL = new TablaGeneralDAL();
            actaHallazgoDAL = new ActaHallazgoDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            return new Auditorias
            {
                idAuditoria = modelo.IdAuditoria,
                accion = modelo.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "3").idTablaGeneral,
                idCategoria = modelo.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "7").idTablaGeneral,
                idUsuario = modelo.IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario,
                fecha = DateTime.Now,
                idElemento = actaHallazgoDAL.GetActaHallazgo(idActaHallazgo).idActaHallazgo

            };
        }
    }
}