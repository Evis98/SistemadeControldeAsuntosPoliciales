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
    public class ActaNotificacionVendedorAmbulanteController : Controller
    {
        ITablaGeneralDAL tablaGeneralDAL;
        IActaNotificacionVendedorAmbulanteDAL actaNotificacionVendedorAmbulanteDAL;
        IPoliciaDAL policiaDAL;
        IPersonaDAL personaDAL;
        IAuditoriaDAL auditoriaDAL;
        IUsuarioDAL usuarioDAL;

        public ActasNotificacionVendedorAmbulante ConvertirActaNotificacionVendedorAmbulante(ActaNotificacionVendedorAmbulanteViewModel modelo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            personaDAL = new PersonaDAL();
            actaNotificacionVendedorAmbulanteDAL = new ActaNotificacionVendedorAmbulanteDAL();
            ActasNotificacionVendedorAmbulante acta = new ActasNotificacionVendedorAmbulante();
            acta.idNotificacionVendedorAmbulante = modelo.IdActaNotificacionVendedorAmbulante;
            acta.numeroFolio = modelo.NumeroFolio;
            acta.fechaHora = modelo.Fecha;
            acta.estado = tablaGeneralDAL.GetCodigo("Actas", "estadoActa", modelo.Estado.ToString()).idTablaGeneral;
            acta.idNotificado = personaDAL.GetPersonaIdentificacion(modelo.IdNotificado).idPersona;
            acta.distrito = tablaGeneralDAL.GetCodigo("Generales", "distrito", modelo.Distrito.ToString()).idTablaGeneral;
            acta.caserio = modelo.Caserio;
            acta.tipoTestigo = tablaGeneralDAL.GetCodigo("Actas", "tipoTestigo", modelo.TipoTestigo.ToString()).idTablaGeneral;

            if (modelo.TipoTestigo != 3)
            {
                if (modelo.TipoTestigo == 1)
                {
                    if (modelo.IdTestigoPolicia != null && policiaDAL.CedulaPoliciaExiste(modelo.IdTestigoPolicia))
                    {
                        acta.testigo = policiaDAL.GetPoliciaCedula(modelo.IdTestigoPolicia).idPolicia;
                    }
                    else
                    {
                        acta.tipoTestigo = tablaGeneralDAL.GetCodigo("Actas", "tipoTestigo", "3").idTablaGeneral;
                    }
                }
                else if (modelo.TipoTestigo == 2)
                {
                    if (modelo.IdTestigoPersona != null && personaDAL.IdentificacionExiste(modelo.IdTestigoPersona))
                    {
                        acta.testigo = personaDAL.GetPersonaIdentificacion(modelo.IdTestigoPersona).idPersona;
                    }
                    else
                    {
                        acta.tipoTestigo = tablaGeneralDAL.GetCodigo("Actas", "tipoTestigo", "3").idTablaGeneral;
                    }
                }
            }

            acta.direccionNotificacion = modelo.DireccionNotificacion;
            acta.formaDeVenta = tablaGeneralDAL.GetCodigo("ActasNotificacionVendedorAmbulante", "formaDeVenta", modelo.FormaDeVenta.ToString()).idTablaGeneral;
            acta.actividadVenta = modelo.ActividadVenta;

            if(modelo.FormaDeVenta == 1 && modelo.PlacaVehiculo != null)
            {
                acta.placaDeVehiculo = modelo.PlacaVehiculo;
            }

            acta.oficialActuante = policiaDAL.GetPoliciaCedula(modelo.OficialActuante).idPolicia;
            acta.direccionNotificado = modelo.DireccionNotificado;

           
            return acta;
        }
        public ActaNotificacionVendedorAmbulanteViewModel CargarActaNotificacionVendedorAmbulante(ActasNotificacionVendedorAmbulante actaNotificacionVendedorAmbulante)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            personaDAL = new PersonaDAL();
            ActaNotificacionVendedorAmbulanteViewModel acta = new ActaNotificacionVendedorAmbulanteViewModel();

            acta.IdActaNotificacionVendedorAmbulante = actaNotificacionVendedorAmbulante.idNotificacionVendedorAmbulante;
            acta.NumeroFolio = actaNotificacionVendedorAmbulante.numeroFolio;
            acta.TipoTestigo = int.Parse(tablaGeneralDAL.Get(actaNotificacionVendedorAmbulante.tipoTestigo).codigo);
            acta.Estado = int.Parse(tablaGeneralDAL.Get(actaNotificacionVendedorAmbulante.estado).codigo);
            acta.VistaEstadoActa = tablaGeneralDAL.Get(actaNotificacionVendedorAmbulante.estado).descripcion;
            if (int.Parse(tablaGeneralDAL.Get(actaNotificacionVendedorAmbulante.tipoTestigo).codigo) != 3 && actaNotificacionVendedorAmbulante.testigo != null)
            {
                if (int.Parse(tablaGeneralDAL.Get(actaNotificacionVendedorAmbulante.tipoTestigo).codigo) == 1 && policiaDAL.GetPolicia((int)actaNotificacionVendedorAmbulante.testigo) != null)
                {
                    acta.VistaTestigo = policiaDAL.GetPolicia((int)actaNotificacionVendedorAmbulante.testigo).nombre;
                    acta.IdTestigoPolicia = policiaDAL.GetPolicia((int)actaNotificacionVendedorAmbulante.testigo).cedula;
                    acta.VistaIdTestigo = policiaDAL.GetPolicia((int)actaNotificacionVendedorAmbulante.testigo).cedula;
                }
                else if (int.Parse(tablaGeneralDAL.Get(actaNotificacionVendedorAmbulante.tipoTestigo).codigo) == 2 && personaDAL.GetPersona((int)actaNotificacionVendedorAmbulante.testigo) != null)
                {
                    acta.VistaTestigo = personaDAL.GetPersona((int)actaNotificacionVendedorAmbulante.testigo).nombre;
                    acta.IdTestigoPersona = personaDAL.GetPersona((int)actaNotificacionVendedorAmbulante.testigo).identificacion;
                    acta.VistaIdTestigo = personaDAL.GetPersona((int)actaNotificacionVendedorAmbulante.testigo).identificacion;
                }
            }
            acta.Distrito = int.Parse(tablaGeneralDAL.Get(actaNotificacionVendedorAmbulante.distrito).codigo);
            acta.VistaTipoDistrito = tablaGeneralDAL.Get(actaNotificacionVendedorAmbulante.distrito).descripcion;
            acta.VistaTipoTestigo = tablaGeneralDAL.Get(actaNotificacionVendedorAmbulante.tipoTestigo).descripcion;
            acta.FormaDeVenta = int.Parse(tablaGeneralDAL.Get(actaNotificacionVendedorAmbulante.formaDeVenta).codigo);
            acta.VistaFormaDeVenta = tablaGeneralDAL.Get(actaNotificacionVendedorAmbulante.formaDeVenta).descripcion;

            if(actaNotificacionVendedorAmbulante.placaDeVehiculo != null)
            {
                acta.PlacaVehiculo = actaNotificacionVendedorAmbulante.placaDeVehiculo;
            }

            acta.Fecha = actaNotificacionVendedorAmbulante.fechaHora;
            acta.Hora = actaNotificacionVendedorAmbulante.fechaHora;
            acta.VistaPersonaNotificada = personaDAL.GetPersona(actaNotificacionVendedorAmbulante.idNotificado).nombre;
            acta.IdNotificado = personaDAL.GetPersona(actaNotificacionVendedorAmbulante.idNotificado).identificacion;
            acta.DireccionNotificacion = actaNotificacionVendedorAmbulante.direccionNotificacion;
            acta.DireccionNotificado = actaNotificacionVendedorAmbulante.direccionNotificado;
            acta.Caserio = actaNotificacionVendedorAmbulante.caserio;
            acta.ActividadVenta = actaNotificacionVendedorAmbulante.actividadVenta;
            acta.OficialActuante = policiaDAL.GetPolicia(actaNotificacionVendedorAmbulante.oficialActuante).cedula;
            acta.VistaOficialActuante = policiaDAL.GetPolicia(actaNotificacionVendedorAmbulante.oficialActuante).nombre;
            return acta;
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
            actaNotificacionVendedorAmbulanteDAL = new ActaNotificacionVendedorAmbulanteDAL();
            policiaDAL = new PoliciaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            personaDAL = new PersonaDAL();
            List<ActaNotificacionVendedorAmbulanteViewModel> actasNotificacionVendedorAmbulante = new List<ActaNotificacionVendedorAmbulanteViewModel>();
            List<ActaNotificacionVendedorAmbulanteViewModel> actasNotificacionVendedorAmbulanteFiltradas = new List<ActaNotificacionVendedorAmbulanteViewModel>();
            List<TablaGeneral> comboindex = tablaGeneralDAL.Get("ActasNotificacionVendedorAmbulante", "index");
            List<SelectListItem> items = comboindex.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.descripcion
                };
            });
            ViewBag.items = items;
            foreach (ActasNotificacionVendedorAmbulante actaNotificacionVendedorAmbulante in actaNotificacionVendedorAmbulanteDAL.Get())
            {
                actasNotificacionVendedorAmbulante.Add(CargarActaNotificacionVendedorAmbulante(actaNotificacionVendedorAmbulante));
            }
            if (busqueda != null)
            {
                foreach (ActaNotificacionVendedorAmbulanteViewModel actaNotificacionVendedorAmbulante in actasNotificacionVendedorAmbulante)
                {
                    if (filtrosSeleccionado == "Número de Folio")
                    {
                        if (actaNotificacionVendedorAmbulante.NumeroFolio.Contains(busqueda))
                        {
                            actasNotificacionVendedorAmbulanteFiltradas.Add(actaNotificacionVendedorAmbulante);
                        }
                    }
                    if (filtrosSeleccionado == "Nombre Policía Actuante")
                    {
                        if (policiaDAL.GetPoliciaCedula(actaNotificacionVendedorAmbulante.OficialActuante).nombre.Contains(busqueda))
                        {
                            actasNotificacionVendedorAmbulanteFiltradas.Add(actaNotificacionVendedorAmbulante);
                        }
                    }
                    if (filtrosSeleccionado == "Persona que Recibe")
                    {
                        if (personaDAL.GetPersonaIdentificacion(actaNotificacionVendedorAmbulante.IdNotificado).nombre.Contains(busqueda))
                        {
                            actasNotificacionVendedorAmbulanteFiltradas.Add(actaNotificacionVendedorAmbulante);
                        }
                    }
                }
                if (filtrosSeleccionado == "Fecha")
                {
                    DateTime fechaInicio = DateTime.Parse(busquedaFechaInicioH);
                    DateTime fechaFinal = DateTime.Parse(busquedaFechaFinalH);
                    if (fechaInicio < fechaFinal)
                    {
                        if (actaNotificacionVendedorAmbulanteDAL.GetActaNotificacionVendedorAmbulanteRango(fechaInicio, fechaFinal) != null)
                        {
                            foreach (ActasNotificacionVendedorAmbulante actaNotificacionVendedorAmbulanteFecha in actaNotificacionVendedorAmbulanteDAL.GetActaNotificacionVendedorAmbulanteRango(fechaInicio, fechaFinal).ToList())
                            {
                                actasNotificacionVendedorAmbulanteFiltradas.Add(CargarActaNotificacionVendedorAmbulante(actaNotificacionVendedorAmbulanteFecha));
                            }
                        }
                    }
                }
                actasNotificacionVendedorAmbulante = actasNotificacionVendedorAmbulanteFiltradas;
            }
            return View(actasNotificacionVendedorAmbulante.OrderBy(x => x.NumeroFolio).ToList());
                  
}
        public ActionResult Nuevo()
        {
            Autorizar();
            tablaGeneralDAL = new TablaGeneralDAL();
            ActaNotificacionVendedorAmbulanteViewModel modelo = new ActaNotificacionVendedorAmbulanteViewModel()
            {
                Distritos = tablaGeneralDAL.Get("Generales", "distrito").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                TiposTestigo = tablaGeneralDAL.Get("Actas", "tipoTestigo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                FormasVenta = tablaGeneralDAL.Get("ActasNotificacionVendedorAmbulante", "formaDeVenta").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                Fecha = DateTime.Today

            };
            return View(modelo);
        }

        [HttpPost]
        public ActionResult Nuevo(ActaNotificacionVendedorAmbulanteViewModel model)
        {
            Autorizar();
            actaNotificacionVendedorAmbulanteDAL = new ActaNotificacionVendedorAmbulanteDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            AuditoriaViewModel auditoria_model = new AuditoriaViewModel();
            model.Distritos = tablaGeneralDAL.Get("Generales", "distrito").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.TiposTestigo = tablaGeneralDAL.Get("Actas", "tipoTestigo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.FormasVenta = tablaGeneralDAL.Get("ActasNotificacionVendedorAmbulante", "formaDeVenta").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.NumeroFolio = (actaNotificacionVendedorAmbulanteDAL.GetCount() + 1).ToString() + "-" + DateTime.Now.Year;
            DateTime newDateTime = model.Fecha.Date + model.Hora.TimeOfDay;
            model.Fecha = newDateTime;
            model.Estado = int.Parse(tablaGeneralDAL.GetCodigo("Actas", "estadoActa", "1").codigo);
            auditoria_model.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "1").idTablaGeneral;
            auditoria_model.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "13").idTablaGeneral;
            auditoria_model.IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario;
            try
            {
                if (ModelState.IsValid)
                {
                    actaNotificacionVendedorAmbulanteDAL.Add(ConvertirActaNotificacionVendedorAmbulante(model));
                    auditoria_model.IdElemento = actaNotificacionVendedorAmbulanteDAL.GetActaNotificacionVendedorAmbulanteFolio(model.NumeroFolio).idNotificacionVendedorAmbulante;
                    auditoriaDAL.Add(ConvertirAuditoria(auditoria_model));
                    int aux = actaNotificacionVendedorAmbulanteDAL.GetActaNotificacionVendedorAmbulanteFolio(model.NumeroFolio).idNotificacionVendedorAmbulante;
                    return Redirect("~/ActaNotificacionVendedorAmbulante/Detalle/" + aux);

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
            actaNotificacionVendedorAmbulanteDAL = new ActaNotificacionVendedorAmbulanteDAL();
            Session["idActaNotificacionVendedorAmbulante"] = id;
            Session["auditoria"] = actaNotificacionVendedorAmbulanteDAL.GetActaNotificacionVendedorAmbulante(id).numeroFolio;
            Session["tabla"] = "Acta de Notificación a Vendedor Ambulante";
            ActaNotificacionVendedorAmbulanteViewModel modelo = CargarActaNotificacionVendedorAmbulante(actaNotificacionVendedorAmbulanteDAL.GetActaNotificacionVendedorAmbulante(id));
            return View(modelo);
        }

        //Devuelve la página de edición de policías con sus apartados llenos
        public ActionResult Editar(int id)
        {
            AutorizarEditar();
            tablaGeneralDAL = new TablaGeneralDAL();
            actaNotificacionVendedorAmbulanteDAL = new ActaNotificacionVendedorAmbulanteDAL();
            ActaNotificacionVendedorAmbulanteViewModel modelo = CargarActaNotificacionVendedorAmbulante(actaNotificacionVendedorAmbulanteDAL.GetActaNotificacionVendedorAmbulante(id));
            modelo.Distritos = tablaGeneralDAL.Get("Generales", "distrito").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            modelo.FormasVenta = tablaGeneralDAL.Get("ActasNotificacionVendedorAmbulante", "formaDeVenta").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            modelo.TiposTestigo = tablaGeneralDAL.Get("Actas", "tipoTestigo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            modelo.Estados = tablaGeneralDAL.Get("Actas", "estadoActa").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            return View(modelo);
        }

        [HttpPost]
        public ActionResult Editar(ActaNotificacionVendedorAmbulanteViewModel model)
        {
            AutorizarEditar();
            actaNotificacionVendedorAmbulanteDAL = new ActaNotificacionVendedorAmbulanteDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            AuditoriaViewModel auditoria_model = new AuditoriaViewModel();
            model.Distritos = tablaGeneralDAL.Get("Generales", "distrito").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.FormasVenta = tablaGeneralDAL.Get("ActasNotificacionVendedorAmbulante", "formaDeVenta").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.TiposTestigo = tablaGeneralDAL.Get("Actas", "tipoTestigo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.Estados = tablaGeneralDAL.Get("Actas", "estadoActa").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            DateTime newDateTime = model.Fecha.Date + model.Hora.TimeOfDay;
            model.Fecha = newDateTime;
            auditoria_model.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "2").idTablaGeneral;
            auditoria_model.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "13").idTablaGeneral;
            auditoria_model.IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario;
            int estado = actaNotificacionVendedorAmbulanteDAL.GetActaNotificacionVendedorAmbulante(model.IdActaNotificacionVendedorAmbulante).estado;
            try
            {
                if (ModelState.IsValid)
                {
                    if (tablaGeneralDAL.GetCodigo("Actas", "estadoActa", model.Estado.ToString()).idTablaGeneral != estado && model.IdActaNotificacionVendedorAmbulante != 0)
                    {
                        auditoriaDAL.Add(CambiarEstadoAuditoria(model.IdActaNotificacionVendedorAmbulante));
                    }
                    actaNotificacionVendedorAmbulanteDAL.Edit(ConvertirActaNotificacionVendedorAmbulante(model));
                    auditoria_model.IdElemento = actaNotificacionVendedorAmbulanteDAL.GetActaNotificacionVendedorAmbulanteFolio(model.NumeroFolio).idNotificacionVendedorAmbulante;
                    auditoriaDAL.Add(ConvertirAuditoria(auditoria_model));
                    return Redirect("~/ActaNotificacionVendedorAmbulante/Detalle/" + model.IdActaNotificacionVendedorAmbulante);
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Auditorias ConvertirAuditoria(AuditoriaViewModel modelo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            actaNotificacionVendedorAmbulanteDAL = new ActaNotificacionVendedorAmbulanteDAL();
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
        public Auditorias CambiarEstadoAuditoria(int idActaNotificacionVendedorAmbulante)
        {
            AuditoriaViewModel modelo = new AuditoriaViewModel();
            tablaGeneralDAL = new TablaGeneralDAL();
            actaNotificacionVendedorAmbulanteDAL = new ActaNotificacionVendedorAmbulanteDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            return new Auditorias
            {
                idAuditoria = modelo.IdAuditoria,
                accion = modelo.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "3").idTablaGeneral,
                idCategoria = modelo.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "13").idTablaGeneral,
                idUsuario = modelo.IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario,
                fecha = DateTime.Now,
                idElemento = actaNotificacionVendedorAmbulanteDAL.GetActaNotificacionVendedorAmbulante(idActaNotificacionVendedorAmbulante).idNotificacionVendedorAmbulante

            };
        }
    }
}