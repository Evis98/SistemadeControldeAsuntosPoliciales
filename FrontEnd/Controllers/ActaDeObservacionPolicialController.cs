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
    public class ActaDeObservacionPolicialController : Controller
    {
        ITablaGeneralDAL tablaGeneralDAL;
        IActaDeObservacionPolicialDAL actaDeObservacionPolicialDAL;
        IPoliciaDAL policiaDAL;
        IPersonaDAL personaDAL;
        IAuditoriaDAL auditoriaDAL;
        IUsuarioDAL usuarioDAL;
     
        public ActasDeObservacionPolicial ConvertirActaDeObservacionPolicial(ActaDeObservacionPolicialViewModel modelo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            personaDAL = new PersonaDAL();
            return new ActasDeObservacionPolicial
            {
                idActaDeObservacionPolicial = modelo.IdActaDeObservacionPolicial,
                numeroFolio = modelo.NumeroFolio,
                fechaHora = modelo.Fecha,
                idInteresado = personaDAL.GetPersonaIdentificacion(modelo.IdInteresado).idPersona,
                distrito = tablaGeneralDAL.GetCodigo("Generales", "distrito", modelo.Distrito.ToString()).idTablaGeneral,
                condicion = modelo.CondicionInteresado,
                direccion = modelo.Direccion,
                observaciones = modelo.Observaciones,
                oficialAcompanante = policiaDAL.GetPoliciaCedula(modelo.OficialAcompanante).idPolicia,
                oficialActuante = policiaDAL.GetPoliciaCedula(modelo.OficialActuante).idPolicia,
                estado = tablaGeneralDAL.GetCodigo("Actas", "estadoActa", modelo.Estado.ToString()).idTablaGeneral,
            };
        }
        public ActaDeObservacionPolicialViewModel CargarActaDeObservacionPolicial(ActasDeObservacionPolicial actaDeObservacionPolicial)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            personaDAL = new PersonaDAL();
            return new ActaDeObservacionPolicialViewModel
            {
                IdActaDeObservacionPolicial = actaDeObservacionPolicial.idActaDeObservacionPolicial,
                NumeroFolio = actaDeObservacionPolicial.numeroFolio,
                OficialAcompanante = policiaDAL.GetPolicia(actaDeObservacionPolicial.oficialAcompanante).cedula,
                OficialActuante = policiaDAL.GetPolicia(actaDeObservacionPolicial.oficialActuante).cedula,
                Distrito = int.Parse(tablaGeneralDAL.Get(actaDeObservacionPolicial.distrito).codigo),
                VistaTipoDistrito = tablaGeneralDAL.Get(actaDeObservacionPolicial.distrito).descripcion,
                Fecha = actaDeObservacionPolicial.fechaHora,
                Hora = actaDeObservacionPolicial.fechaHora,
                IdInteresado = personaDAL.GetPersona(actaDeObservacionPolicial.idInteresado).identificacion,
                CondicionInteresado = actaDeObservacionPolicial.condicion,
                Direccion = actaDeObservacionPolicial.direccion,
                Observaciones = actaDeObservacionPolicial.observaciones,
                Estado = int.Parse(tablaGeneralDAL.Get(actaDeObservacionPolicial.estado).codigo),
                VistaTipoEstado = tablaGeneralDAL.Get(actaDeObservacionPolicial.estado).descripcion,
                VistaOficialAcompanante = policiaDAL.GetPolicia(actaDeObservacionPolicial.oficialAcompanante).nombre,
                VistaOficialActuante = policiaDAL.GetPolicia(actaDeObservacionPolicial.oficialActuante).nombre,
                VistaPersonaInteresada = personaDAL.GetPersona(actaDeObservacionPolicial.idInteresado).nombre,
            };
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

        public ActionResult Index(string filtrosSeleccionado, string busqueda, string busquedaFechaInicioH, string busquedaFechaFinalH)
        {
            Autorizar();
            actaDeObservacionPolicialDAL = new ActaDeObservacionPolicialDAL();
            policiaDAL = new PoliciaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            personaDAL = new PersonaDAL();
            List<ActaDeObservacionPolicialViewModel> actasDeObservacionPolicial = new List<ActaDeObservacionPolicialViewModel>();
            List<ActaDeObservacionPolicialViewModel> actasDeObservacionPolicialFiltradas = new List<ActaDeObservacionPolicialViewModel>();
            List<TablaGeneral> comboindex = tablaGeneralDAL.Get("ActasDeObservacionPolicial", "index");
            List<SelectListItem> items = comboindex.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.descripcion
                };
            });
            ViewBag.items = items;
            foreach (ActasDeObservacionPolicial actaDeObservacionPolicial in actaDeObservacionPolicialDAL.Get())
            {
                actasDeObservacionPolicial.Add(CargarActaDeObservacionPolicial(actaDeObservacionPolicial));
            }
            if (busqueda != null)
            {
                foreach (ActaDeObservacionPolicialViewModel actaDeObservacionPolicial in actasDeObservacionPolicial)
                {
                    if (filtrosSeleccionado == "Número de Folio")
                    {
                        if (actaDeObservacionPolicial.NumeroFolio.Contains(busqueda))
                        {
                            actasDeObservacionPolicialFiltradas.Add(actaDeObservacionPolicial);
                        }
                    }
                    if (filtrosSeleccionado == "Nombre Policía Actuante")
                    {
                        if (policiaDAL.GetPoliciaCedula(actaDeObservacionPolicial.OficialActuante).nombre.Contains(busqueda))
                        {
                            actasDeObservacionPolicialFiltradas.Add(actaDeObservacionPolicial);
                        }
                    }
                    if (filtrosSeleccionado == "Persona Interesada")
                    {
                        if (personaDAL.GetPersonaIdentificacion(actaDeObservacionPolicial.IdInteresado).nombre.Contains(busqueda))
                        {
                            actasDeObservacionPolicialFiltradas.Add(actaDeObservacionPolicial);
                        }
                    }
                }
                if (filtrosSeleccionado == "Fecha")
                {
                    DateTime fechaInicio = DateTime.Parse(busquedaFechaInicioH);
                    DateTime fechaFinal = DateTime.Parse(busquedaFechaFinalH);
                    if (fechaInicio < fechaFinal)
                    {
                        if (actaDeObservacionPolicialDAL.GetActaDeObservacionPolicialRango(fechaInicio, fechaFinal) != null)
                        {
                            foreach (ActasDeObservacionPolicial actaDeObservacionPolicialFecha in actaDeObservacionPolicialDAL.GetActaDeObservacionPolicialRango(fechaInicio, fechaFinal).ToList())
                            {
                                actasDeObservacionPolicialFiltradas.Add(CargarActaDeObservacionPolicial(actaDeObservacionPolicialFecha));
                            }
                        }
                    }
                }
                actasDeObservacionPolicial = actasDeObservacionPolicialFiltradas;
            }
            return View(actasDeObservacionPolicial.OrderBy(x => x.NumeroFolio).ToList());
            
            
        }
        public ActionResult Nuevo()
        {
            Autorizar();
            tablaGeneralDAL = new TablaGeneralDAL();
            ActaDeObservacionPolicialViewModel modelo = new ActaDeObservacionPolicialViewModel()
            {
            Distritos = tablaGeneralDAL.Get("Generales", "distrito").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
            Fecha = DateTime.Today
            };
            return View(modelo);
        }

        [HttpPost]
        public ActionResult Nuevo(ActaDeObservacionPolicialViewModel model)
        {
            Autorizar();
            actaDeObservacionPolicialDAL = new ActaDeObservacionPolicialDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            auditoriaDAL = new AuditoriaDAL();
            usuarioDAL = new UsuarioDAL();
            model.Distritos = tablaGeneralDAL.Get("Generales", "distrito").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.NumeroFolio = (actaDeObservacionPolicialDAL.GetCount() + 1).ToString() + "-" + DateTime.Now.Year;
            DateTime newDateTime = model.Fecha.Date + model.Hora.TimeOfDay;
            model.Fecha = newDateTime;
            model.Estado = int.Parse(tablaGeneralDAL.GetCodigo("Actas", "estadoActa", "1").codigo);
            model.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "1").idTablaGeneral;
            model.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "8").idTablaGeneral;
            model.IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario;
            try
            {
                if (ModelState.IsValid)
                {
                    actaDeObservacionPolicialDAL.Add(ConvertirActaDeObservacionPolicial(model));
                    model.IdElemento = actaDeObservacionPolicialDAL.GetActaDeObservacionPolicialFolio(model.NumeroFolio).idActaDeObservacionPolicial;
                    auditoriaDAL.Add(ConvertirAuditoria(model));
                    int aux = actaDeObservacionPolicialDAL.GetActaDeObservacionPolicialFolio(model.NumeroFolio).idActaDeObservacionPolicial;
                    return Redirect("~/ActaDeObservacionPolicial/Detalle/" + aux);

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
            actaDeObservacionPolicialDAL = new ActaDeObservacionPolicialDAL();
            Session["idActaDeObservacionPolicial"] = id;
            Session["numeroFolio"] = actaDeObservacionPolicialDAL.GetActaDeObservacionPolicial(id).numeroFolio;           
            ActaDeObservacionPolicialViewModel modelo = CargarActaDeObservacionPolicial(actaDeObservacionPolicialDAL.GetActaDeObservacionPolicial(id));
            return View(modelo);
        }

        //Devuelve la página de edición de policías con sus apartados llenos
        public ActionResult Editar(int id)
        {
            AutorizarEditar();
            tablaGeneralDAL = new TablaGeneralDAL();
            actaDeObservacionPolicialDAL = new ActaDeObservacionPolicialDAL();
            ActaDeObservacionPolicialViewModel modelo = CargarActaDeObservacionPolicial(actaDeObservacionPolicialDAL.GetActaDeObservacionPolicial(id));
            modelo.Distritos = tablaGeneralDAL.Get("Generales", "distrito").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            modelo.Estados = tablaGeneralDAL.Get("Actas", "estadoActa").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            return View(modelo);
        }

        [HttpPost]
        public ActionResult Editar(ActaDeObservacionPolicialViewModel model)
        {
            AutorizarEditar();
            actaDeObservacionPolicialDAL = new ActaDeObservacionPolicialDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            auditoriaDAL = new AuditoriaDAL();
            usuarioDAL = new UsuarioDAL();
            model.Distritos = tablaGeneralDAL.Get("Generales", "distrito").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.Estados = tablaGeneralDAL.Get("Actas", "estadoActa").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            DateTime newDateTime = model.Fecha.Date + model.Hora.TimeOfDay;
            model.Fecha = newDateTime;
            model.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "2").idTablaGeneral;
            model.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "8").idTablaGeneral;
            model.IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario;
            int estado = actaDeObservacionPolicialDAL.GetActaDeObservacionPolicial(model.IdActaDeObservacionPolicial).estado;
            try
            {
                if (ModelState.IsValid)
                {
                    if (tablaGeneralDAL.GetCodigo("Actas", "estadoActa", model.Estado.ToString()).idTablaGeneral != estado && model.IdActaDeObservacionPolicial != 0)
                    {
                        auditoriaDAL.Add(CambiarEstadoAuditoria(model.IdActaDeObservacionPolicial));
                    }
                    actaDeObservacionPolicialDAL.Edit(ConvertirActaDeObservacionPolicial(model));
                    model.IdElemento = actaDeObservacionPolicialDAL.GetActaDeObservacionPolicialFolio(model.NumeroFolio).idActaDeObservacionPolicial;
                    auditoriaDAL.Add(ConvertirAuditoria(model));
                    return Redirect("~/ActaDeObservacionPolicial/Detalle/" + model.IdActaDeObservacionPolicial);
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
      
        public Auditorias ConvertirAuditoria(ActaDeObservacionPolicialViewModel modelo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            actaDeObservacionPolicialDAL = new ActaDeObservacionPolicialDAL();
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

        public Auditorias CambiarEstadoAuditoria(int idActaDeObservacionPolicial)
        {
            ActaHallazgoViewModel modelo = new ActaHallazgoViewModel();
            tablaGeneralDAL = new TablaGeneralDAL();
            actaDeObservacionPolicialDAL = new ActaDeObservacionPolicialDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            return new Auditorias
            {
                idAuditoria = modelo.IdAuditoria,
                accion = modelo.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "3").idTablaGeneral,
                idCategoria = modelo.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "8").idTablaGeneral,
                idUsuario = modelo.IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario,
                fecha = DateTime.Now,
                idElemento = actaDeObservacionPolicialDAL.GetActaDeObservacionPolicial(idActaDeObservacionPolicial).idActaDeObservacionPolicial

            };
        }

    }
}