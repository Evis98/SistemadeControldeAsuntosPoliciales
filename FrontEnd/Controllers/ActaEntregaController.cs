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
    public class ActaEntregaController : Controller
    {

        ITablaGeneralDAL tablaGeneralDAL;
        IActaEntregaDAL actaEntregaDAL;
        IPoliciaDAL policiaDAL;
        IPersonaDAL personaDAL;
        IActaHallazgoDAL actaHallazgoDAL;
        IActaDecomisoDAL actaDecomisoDAL;
        IAuditoriaDAL auditoriaDAL;
        IUsuarioDAL usuarioDAL;

        public ActasEntrega ConvertirActaEntrega(ActaEntregaViewModel model)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            personaDAL = new PersonaDAL();
            ActasEntrega acta = new ActasEntrega();
            {
                acta.idActaEntrega = model.IdActaEntrega;
                acta.numeroFolio = model.NumeroFolio;
                acta.encargado = policiaDAL.GetPoliciaCedula(model.Encargado).idPolicia;
                acta.supervisor = policiaDAL.GetPoliciaCedula(model.Supervisor).idPolicia;
                acta.fechaHora = model.Fecha;
                if (model.RazonSocial != null)
                {
                    acta.razonSocial = personaDAL.GetPersonaIdentificacion(model.RazonSocial).idPersona;
                }
                acta.tipoActa = tablaGeneralDAL.GetCodigo("Actas", "tipoActa", model.TipoActa.ToString()).idTablaGeneral;
                if (tablaGeneralDAL.GetCodigo("Actas", "tipoTestigo", model.TipoTestigo.ToString()).codigo != "3")
                {
                    acta.tipoTestigo = tablaGeneralDAL.GetCodigo("Actas", "tipoTestigo", model.TipoTestigo.ToString()).idTablaGeneral;
                    if (tablaGeneralDAL.Get((int)acta.tipoTestigo).descripcion == "Policía")
                    {
                        acta.testigo = policiaDAL.GetPoliciaCedula(model.TestigoPolicia).idPolicia;
                    }
                    else if(tablaGeneralDAL.Get((int)acta.tipoTestigo).descripcion == "Persona")
                    {
                        acta.testigo = personaDAL.GetPersonaIdentificacion(model.TestigoPersona).idPersona;
                    }
                }
                else
                {
                    acta.tipoTestigo = tablaGeneralDAL.GetCodigo("Actas", "tipoTestigo", "3").idTablaGeneral;
                }
                if (tablaGeneralDAL.Get(acta.tipoActa).descripcion == "Hallazgo")
                {
                    acta.consecutivoActa = model.ConsecutivoActaHallazgo;
                }
                else
                {
                    acta.consecutivoActa = model.ConsecutivoActaDecomiso;
                }
                acta.estado = tablaGeneralDAL.GetCodigo("Actas", "estadoActa", model.Estado.ToString()).idTablaGeneral;
                acta.recibe = personaDAL.GetPersonaIdentificacion(model.Recibe).idPersona;
                acta.inventarioEntregado = model.InventarioEntregado;

            };

            return acta;
        }
        public ActaEntregaViewModel CargarActaEntrega(ActasEntrega actaEntrega)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            personaDAL = new PersonaDAL();
            actaDecomisoDAL = new ActaDecomisoDAL();

            ActaEntregaViewModel acta = new ActaEntregaViewModel();
            {
                acta.IdActaEntrega = actaEntrega.idActaEntrega;
                acta.NumeroFolio = actaEntrega.numeroFolio;
                acta.Encargado = policiaDAL.GetPolicia(actaEntrega.encargado).cedula;
                acta.Supervisor = policiaDAL.GetPolicia(actaEntrega.supervisor).cedula;
                acta.Fecha = actaEntrega.fechaHora;
                acta.Hora = actaEntrega.fechaHora;
                if (actaEntrega.razonSocial != null)
                {
                    acta.RazonSocial = personaDAL.GetPersona((int)actaEntrega.razonSocial).identificacion;
                    acta.VistaRazonSocial = personaDAL.GetPersona((int)actaEntrega.razonSocial).nombre;
                }
                else
                {
                    acta.VistaRazonSocial = "N/A";

                }

                acta.TipoActa = int.Parse(tablaGeneralDAL.Get(actaEntrega.tipoActa).codigo);
                acta.VistaTipoActa = tablaGeneralDAL.Get(actaEntrega.tipoActa).descripcion;
                if (actaEntrega.testigo != null)
                {
                    acta.TipoTestigo = int.Parse(tablaGeneralDAL.Get((int)actaEntrega.tipoTestigo).codigo);
                    if (tablaGeneralDAL.Get((int)actaEntrega.tipoTestigo).descripcion == "Policía")
                    {
                        acta.TestigoPolicia = policiaDAL.GetPolicia((int)actaEntrega.testigo).cedula;
                        acta.VistaTestigoPolicia = policiaDAL.GetPolicia((int)actaEntrega.testigo).nombre;
                    }
                    else
                    {
                        acta.TestigoPersona = personaDAL.GetPersona((int)actaEntrega.testigo).identificacion;
                        acta.VistaTestigoPersona = personaDAL.GetPersona((int)actaEntrega.testigo).nombre;
                    }
                }

                if (tablaGeneralDAL.Get(actaEntrega.tipoActa).descripcion == "Decomiso")
                {
                    acta.ConsecutivoActaDecomiso = actaEntrega.consecutivoActa;
                    acta.NombreDe = actaDecomisoDAL.GetActaDecomisoFolio(actaEntrega.consecutivoActa).nombreDecomisado;

                }
                else
                {
                    acta.ConsecutivoActaHallazgo = actaEntrega.consecutivoActa;
                }
                acta.Recibe = personaDAL.GetPersona(actaEntrega.recibe).identificacion;
                acta.VistaRecibe = personaDAL.GetPersona(actaEntrega.recibe).nombre;
                acta.InventarioEntregado = actaEntrega.inventarioEntregado;
                acta.Estado = int.Parse(tablaGeneralDAL.Get(actaEntrega.estado).codigo);
                acta.VistaEstadoActa = tablaGeneralDAL.Get(actaEntrega.estado).descripcion;
                acta.VistaPoliciaEncargado = policiaDAL.GetPolicia(actaEntrega.encargado).nombre;
                acta.VistaPoliciaSupervisor = policiaDAL.GetPolicia(actaEntrega.supervisor).nombre;
            }
            return acta;
        }
        public List<PersonaViewModel> ConvertirListaPersonasJuridicasFiltrados(List<Personas> personas)
        {
            return (from d in personas
                    select new PersonaViewModel
                    {
                        Identificacion = d.identificacion,
                        NombrePersona = d.nombre,
                    }).ToList();
        }
        public PartialViewResult ListaPersonasJuridicasBuscar(string nombre)
        {
            List<PersonaViewModel> personas = new List<PersonaViewModel>();

            return PartialView("_ListaPersonasJuridicasBuscar", personas);
        }

        public PartialViewResult ListaPersonasJuridicasParcial(string nombre)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            personaDAL = new PersonaDAL();
            List<Personas> personas = personaDAL.Get();
            List<Personas> personasFiltradas = new List<Personas>();

            foreach (Personas persona in personas)
            {
                if (persona.nombre.Contains(nombre))
                {
                    if (tablaGeneralDAL.Get(persona.tipoIdentificacion).descripcion == "Cédula Jurídica")
                    {
                        personasFiltradas.Add(persona);
                    }
                }
            }

            personasFiltradas = personasFiltradas.OrderBy(x => x.nombre).ToList();
            return PartialView("_ListaPersonasJuridicasParcial", ConvertirListaPersonasJuridicasFiltrados(personasFiltradas));
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
        public List<ActaHallazgoViewModel> ConvertirListaHallazgosFiltrados(List<ActasHallazgo> actasHallazgos)
        {
            policiaDAL = new PoliciaDAL();
            return (from d in actasHallazgos
                    select new ActaHallazgoViewModel
                    {
                        NumeroFolio = d.numeroFolio,
                        VistaPoliciaEncargado = policiaDAL.GetPolicia(d.encargado).nombre,
                        Inventario = d.inventario
                    }).ToList();
        }
        public PartialViewResult ListaHallazgosBuscar(string numeroFolio)
        {
            List<ActaHallazgoViewModel> actasHallazgos = new List<ActaHallazgoViewModel>();

            return PartialView("_ListaHallazgosBuscar", actasHallazgos);
        }

        public PartialViewResult ListaHallazgosParcial(string numeroFolio)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            actaHallazgoDAL = new ActaHallazgoDAL();
            List<ActasHallazgo> actasHallazgo = actaHallazgoDAL.Get();
            List<ActasHallazgo> actasHallazgoFiltradas = new List<ActasHallazgo>();

            if (numeroFolio == "")
            {
                actasHallazgoFiltradas = actasHallazgo;
            }
            else
            {
                foreach (ActasHallazgo actaHallazgo in actasHallazgo)
                {
                    if (actaHallazgo.numeroFolio.Contains(numeroFolio))
                    {
                        actasHallazgoFiltradas.Add(actaHallazgo);
                    }
                }
            }
            actasHallazgoFiltradas = actasHallazgoFiltradas.OrderBy(x => x.numeroFolio).ToList();

            return PartialView("_ListaHallazgosParcial", ConvertirListaHallazgosFiltrados(actasHallazgoFiltradas));
        }

        public List<ActaDecomisoViewModel> ConvertirListaDecomisosFiltrados(List<ActasDecomiso> actasDecomisos)
        {
            policiaDAL = new PoliciaDAL();
            return (from d in actasDecomisos
                    select new ActaDecomisoViewModel
                    {
                        NumeroFolio = d.numeroFolio,
                        VistaOficialActuante = policiaDAL.GetPolicia(d.oficialActuante).nombre,
                        Inventario = d.inventario
                    }).ToList();
        }
        public PartialViewResult ListaDecomisosBuscar(string numeroFolio)
        {
            List<ActaDecomisoViewModel> actasDecomisos = new List<ActaDecomisoViewModel>();

            return PartialView("_ListaDecomisosBuscar", actasDecomisos);
        }

        public PartialViewResult ListaDecomisosParcial(string numeroFolio)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            actaDecomisoDAL = new ActaDecomisoDAL();
            List<ActasDecomiso> actasDecomisos = actaDecomisoDAL.Get();
            List<ActasDecomiso> actasDecomisoFiltradas = new List<ActasDecomiso>();

            if (numeroFolio == "")
            {
                actasDecomisoFiltradas = actasDecomisos;
            }
            else
            {
                foreach (ActasDecomiso actaDecomiso in actasDecomisos)
                {
                    if (actaDecomiso.numeroFolio.Contains(numeroFolio))
                    {

                        actasDecomisoFiltradas.Add(actaDecomiso);
                    }
                }
            }
            actasDecomisoFiltradas = actasDecomisoFiltradas.OrderBy(x => x.numeroFolio).ToList();

            return PartialView("_ListaDecomisosParcial", ConvertirListaDecomisosFiltrados(actasDecomisoFiltradas));
        }
        public ActionResult Index(string filtrosSeleccionado, string busqueda, string busquedaFechaInicioH, string busquedaFechaFinalH)
        {
            if (Session["userID"] != null)
            {

                actaEntregaDAL = new ActaEntregaDAL();
            policiaDAL = new PoliciaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            List<ActaEntregaViewModel> actasEntrega = new List<ActaEntregaViewModel>();
            List<ActaEntregaViewModel> actasEntregaFiltradas = new List<ActaEntregaViewModel>();
            List<TablaGeneral> comboindex = tablaGeneralDAL.Get("ActasEntrega", "index");
            List<SelectListItem> items = comboindex.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.descripcion
                };
            });
            ViewBag.items = items;
            foreach (ActasEntrega actaEntrega in actaEntregaDAL.Get())
            {
                actasEntrega.Add(CargarActaEntrega(actaEntrega));
            }
            if (busqueda != null)
            {
                foreach (ActaEntregaViewModel actaEntrega in actasEntrega)
                {
                    if (filtrosSeleccionado == "Número de Folio")
                    {
                        if (actaEntrega.NumeroFolio.Contains(busqueda))
                        {
                            actasEntregaFiltradas.Add(actaEntrega);
                        }
                    }
                    if (filtrosSeleccionado == "Nombre Policía Encargado")
                    {
                        if (policiaDAL.GetPoliciaCedula(actaEntrega.Encargado).nombre.Contains(busqueda))
                        {
                            actasEntregaFiltradas.Add(actaEntrega);
                        }
                    }
                }
                if (filtrosSeleccionado == "Fecha")
                {
                    DateTime fechaInicio = DateTime.Parse(busquedaFechaInicioH);
                    DateTime fechaFinal = DateTime.Parse(busquedaFechaFinalH);
                    if (fechaInicio < fechaFinal)
                    {
                        if (actaEntregaDAL.GetActaEntregaRango(fechaInicio, fechaFinal) != null)
                        {
                            foreach (ActasEntrega actaEntregaFecha in actaEntregaDAL.GetActaEntregaRango(fechaInicio, fechaFinal).ToList())
                            {
                                actasEntregaFiltradas.Add(CargarActaEntrega(actaEntregaFecha));
                            }
                        }
                    }
                }

                actasEntrega = actasEntregaFiltradas;
            }
            return View(actasEntrega.OrderBy(x => x.NumeroFolio).ToList());
        
            }
            else
            {
                return Redirect("~/Shared/Error.cshtml");
            }
        }

        public ActionResult Nuevo()
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            ActaEntregaViewModel modelo = new ActaEntregaViewModel()
            {
                TiposActa = tablaGeneralDAL.Get("Actas", "tipoActa").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                TiposTestigo = tablaGeneralDAL.Get("Actas", "tipoTestigo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                Fecha = DateTime.Today

            };
            return View(modelo);
        }

        [HttpPost]
        public ActionResult Nuevo(ActaEntregaViewModel model)
        {
            actaEntregaDAL = new ActaEntregaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            model.TiposActa = tablaGeneralDAL.Get("Actas", "tipoActa").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.TiposTestigo = tablaGeneralDAL.Get("Actas", "tipoTestigo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.NumeroFolio = (actaEntregaDAL.GetCount() + 1).ToString() + "-" + DateTime.Now.Year;
            DateTime newDateTime = model.Fecha.Date + model.Hora.TimeOfDay;
            model.Fecha = newDateTime;
            model.Estado = int.Parse(tablaGeneralDAL.GetCodigo("Actas", "estadoActa", "1").codigo);
            model.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "1").idTablaGeneral;
            model.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "14").idTablaGeneral;
            model.IdUsuario = usuarioDAL.GetUsuario(1).idUsuario;
            try
            {
                if (ModelState.IsValid)
                {
                    actaEntregaDAL.Add(ConvertirActaEntrega(model));
                    model.IdElemento = actaEntregaDAL.GetActaEntregaFolio(model.NumeroFolio).idActaEntrega;
                    auditoriaDAL.Add(ConvertirAuditoria(model));
                    int aux = actaEntregaDAL.GetActaEntregaFolio(model.NumeroFolio).idActaEntrega;
                    return Redirect("~/ActaEntrega/Detalle/" + aux);

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
            actaEntregaDAL = new ActaEntregaDAL();
            Session["idActaEntrega"] = id;
            Session["numeroFolio"] = actaEntregaDAL.GetActaEntrega(id).numeroFolio;
            ActaEntregaViewModel modelo = CargarActaEntrega(actaEntregaDAL.GetActaEntrega(id));
            return View(modelo);
        }
        public ActionResult Editar(int id)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            actaEntregaDAL = new ActaEntregaDAL();
            ActaEntregaViewModel model = CargarActaEntrega(actaEntregaDAL.GetActaEntrega(id));
            model.TiposActa = tablaGeneralDAL.Get("Actas", "tipoActa").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.TiposTestigo = tablaGeneralDAL.Get("Actas", "tipoTestigo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.Estados = tablaGeneralDAL.Get("Actas", "estadoActa").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(ActaEntregaViewModel model)
        {
            actaEntregaDAL = new ActaEntregaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            model.TiposActa = tablaGeneralDAL.Get("Actas", "tipoActa").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.TiposTestigo = tablaGeneralDAL.Get("Actas", "tipoTestigo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.Estados = tablaGeneralDAL.Get("Actas", "estadoActa").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            DateTime newDateTime = model.Fecha.Date + model.Hora.TimeOfDay;
            model.Fecha = newDateTime;
            model.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "2").idTablaGeneral;
            model.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "14").idTablaGeneral;
            model.IdUsuario = usuarioDAL.GetUsuario(1).idUsuario;
            int estado = actaEntregaDAL.GetActaEntrega(model.IdActaEntrega).estado;
            try
            {
                if (ModelState.IsValid)
                {
                    if (tablaGeneralDAL.GetCodigo("Actas", "estadoActa", model.Estado.ToString()).idTablaGeneral != estado && model.IdActaEntrega != 0)
                    {
                        auditoriaDAL.Add(CambiarEstadoAuditoria(model.IdActaEntrega));
                    }
                    actaEntregaDAL.Edit(ConvertirActaEntrega(model));
                    model.IdElemento = actaEntregaDAL.GetActaEntregaFolio(model.NumeroFolio).idActaEntrega;
                    auditoriaDAL.Add(ConvertirAuditoria(model));
                    return Redirect("~/ActaEntrega/Detalle/" + model.IdActaEntrega);
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Auditorias ConvertirAuditoria(ActaEntregaViewModel modelo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            actaEntregaDAL = new ActaEntregaDAL();
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
        public Auditorias CambiarEstadoAuditoria(int idActaEntrega)
        {
            ActaEntregaViewModel modelo = new ActaEntregaViewModel();
            tablaGeneralDAL = new TablaGeneralDAL();
            actaEntregaDAL = new ActaEntregaDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            return new Auditorias
            {
                idAuditoria = modelo.IdAuditoria,
                accion = modelo.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "3").idTablaGeneral,
                idCategoria = modelo.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "14").idTablaGeneral,
                idUsuario = modelo.IdUsuario = usuarioDAL.GetUsuario(1).idUsuario,
                fecha = DateTime.Now,
                idElemento = actaEntregaDAL.GetActaEntrega(idActaEntrega).idActaEntrega

            };
        }

    }
}