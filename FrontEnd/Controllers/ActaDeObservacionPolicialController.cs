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
               //estadoActa = tablaGeneralDAL.GetCodigo("Actas", "estadoActa", "1").idTablaGeneral,
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
                //EstadoActa = actaDeObservacionPolicial.estadoActa,
                //VistaEstadoActa = tablaGeneralDAL.Get(actaDeObservacionPolicial.estadoActa).descripcion,
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
            if (nombre == "")
            {
                personasFiltradas = personas;
            }
            else
            {
                foreach (Personas persona in personas)
                {
                    if (persona.nombre.Contains(nombre))
                    {
                        personasFiltradas.Add(persona);
                    }
                }
            }
            personasFiltradas = personasFiltradas.OrderBy(x => x.nombre).ToList();
            return PartialView("_ListaPersonasParcial", ConvertirListaPersonasFiltrados(personasFiltradas));
        }
        public ActionResult Index(string filtrosSeleccionado, string busqueda, string busquedaFechaInicioH, string busquedaFechaFinalH)
        {
            actaDeObservacionPolicialDAL = new ActaDeObservacionPolicialDAL();
            policiaDAL = new PoliciaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            List<ActaDeObservacionPolicialViewModel> actasDeObservacionPolicial = new List<ActaDeObservacionPolicialViewModel>();
            List<ActaDeObservacionPolicialViewModel> actasDeObservacionPolicialFiltradas = new List<ActaDeObservacionPolicialViewModel>();
            List<TablaGeneral> comboindex = tablaGeneralDAL.Get("ActasDecomiso", "index");
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
            actaDeObservacionPolicialDAL = new ActaDeObservacionPolicialDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            model.Distritos = tablaGeneralDAL.Get("Generales", "distrito").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.NumeroFolio = (actaDeObservacionPolicialDAL.GetCount() + 1).ToString() + "-" + DateTime.Now.Year;
            DateTime newDateTime = model.Fecha.Date + model.Hora.TimeOfDay;
            model.Fecha = newDateTime;
            try
            {
                if (ModelState.IsValid)
                {
                    actaDeObservacionPolicialDAL.Add(ConvertirActaDeObservacionPolicial(model));
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
            Session["idDeObservacionPolicial"] = id;
            actaDeObservacionPolicialDAL = new ActaDeObservacionPolicialDAL();
            ActaDeObservacionPolicialViewModel modelo = CargarActaDeObservacionPolicial(actaDeObservacionPolicialDAL.GetActaDeObservacionPolicial(id));
            return View(modelo);
        }

        //Devuelve la página de edición de policías con sus apartados llenos
        public ActionResult Editar(int id)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            actaDeObservacionPolicialDAL = new ActaDeObservacionPolicialDAL();
            ActaDeObservacionPolicialViewModel modelo = CargarActaDeObservacionPolicial(actaDeObservacionPolicialDAL.GetActaDeObservacionPolicial(id));
            modelo.Distritos = tablaGeneralDAL.Get("Generales", "distrito").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            return View(modelo);
        }

        [HttpPost]
        public ActionResult Editar(ActaDeObservacionPolicialViewModel model)
        {
            actaDeObservacionPolicialDAL = new ActaDeObservacionPolicialDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            model.Distritos = tablaGeneralDAL.Get("Generales", "distrito").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            DateTime newDateTime = model.Fecha.Date + model.Hora.TimeOfDay;
            model.Fecha = newDateTime;
            try
            {
                if (ModelState.IsValid)
                {
                    actaDeObservacionPolicialDAL.Edit(ConvertirActaDeObservacionPolicial(model));
                    return Redirect("~/ActaDeObservacionPolicial/Detalle/" + model.IdActaDeObservacionPolicial);
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //public ActionResult CambioEstadoActa(int id)
        //{
        //    int estado;
        //    actaDecomisoDAL = new ActaDecomisoDAL();
        //    tablaGeneralDAL = new TablaGeneralDAL();
        //    try
        //    {
        //        if (tablaGeneralDAL.Get((int)id).descripcion == "Activa")
        //        {
        //            estado = tablaGeneralDAL.Get("Actas", "estadoActa", "Inactiva").idTablaGeneral;
        //        }
        //        else
        //        {
        //            estado = tablaGeneralDAL.Get("Actas", "estadoActa", "Activa").idTablaGeneral;
        //        }
        //        actaDecomisoDAL.CambiaEstadoActa((int)Session["idActaDecomiso"], estado);
        //        return Redirect("~/ActaDecomiso/Detalle/" + Session["idActaDecomiso"]);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}


    }
}