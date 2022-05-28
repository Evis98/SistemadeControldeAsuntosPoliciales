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
using Microsoft.Reporting.WinForms;

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
            actaDecomisoDAL = new ActaDecomisoDAL();
            actaHallazgoDAL = new ActaHallazgoDAL();
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
                    if(tablaGeneralDAL.Get((int)acta.tipoTestigo).descripcion == "Policía")
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
                if (model.TipoActa == 1 && actaHallazgoDAL.FolioExiste(model.ConsecutivoActaHallazgo))
                {
                    acta.idActaLigada = actaHallazgoDAL.GetActaHallazgoFolio(model.ConsecutivoActaHallazgo).idActaHallazgo;

                }
                else if (model.TipoActa == 2 && actaDecomisoDAL.FolioExiste(model.ConsecutivoActaDecomiso))
                {
                    acta.idActaLigada = actaDecomisoDAL.GetActaDecomisoFolio(model.ConsecutivoActaDecomiso).idActaDecomiso;
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
            actaHallazgoDAL = new ActaHallazgoDAL();
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

                if (tablaGeneralDAL.Get(actaEntrega.tipoActa).descripcion == "Hallazgo")
                {
                    acta.IdActaHallazgo = (int)actaEntrega.idActaLigada;
                    acta.NumeroActaLigada = actaHallazgoDAL.GetActaHallazgo((int)actaEntrega.idActaLigada).numeroFolio;
                    acta.ConsecutivoActaHallazgo = actaHallazgoDAL.GetActaHallazgo((int)actaEntrega.idActaLigada).numeroFolio;


                }
                else if (tablaGeneralDAL.Get(actaEntrega.tipoActa).descripcion == "Decomiso")
                {
                    acta.IdActaDecomiso = (int)actaEntrega.idActaLigada;
                    acta.NumeroActaLigada = actaDecomisoDAL.GetActaDecomiso((int)actaEntrega.idActaLigada).numeroFolio;
                    acta.ConsecutivoActaDecomiso = actaDecomisoDAL.GetActaDecomiso((int)actaEntrega.idActaLigada).numeroFolio;
                    acta.NombreDe = actaDecomisoDAL.GetPersonaPorIdActa((int)actaEntrega.idActaLigada).nombre;

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
            actaEntregaDAL = new ActaEntregaDAL();
            policiaDAL = new PoliciaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            personaDAL = new PersonaDAL();
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
                    if (filtrosSeleccionado == "Persona que Recibe")
                    {
                        if (personaDAL.GetPersonaIdentificacion(actaEntrega.Recibe).nombre.Contains(busqueda))
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
         

        public ActionResult Nuevo()
        {
            Autorizar();
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
            Autorizar();
            actaEntregaDAL = new ActaEntregaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            AuditoriaViewModel auditoria_model = new AuditoriaViewModel();
            model.TiposActa = tablaGeneralDAL.Get("Actas", "tipoActa").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.TiposTestigo = tablaGeneralDAL.Get("Actas", "tipoTestigo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.NumeroFolio = (actaEntregaDAL.GetCount() + 1).ToString() + "-" + DateTime.Now.Year;
            DateTime newDateTime = model.Fecha.Date + model.Hora.TimeOfDay;
            model.Fecha = newDateTime;
            model.Estado = int.Parse(tablaGeneralDAL.GetCodigo("Actas", "estadoActa", "1").codigo);
            auditoria_model.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "1").idTablaGeneral;
            auditoria_model.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "14").idTablaGeneral;
            auditoria_model.IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario;
            try
            {
                if (ModelState.IsValid)
                {
                    actaEntregaDAL.Add(ConvertirActaEntrega(model));
                    auditoria_model.IdElemento = actaEntregaDAL.GetActaEntregaFolio(model.NumeroFolio).idActaEntrega;
                    auditoriaDAL.Add(ConvertirAuditoria(auditoria_model));
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
            Autorizar();
            actaEntregaDAL = new ActaEntregaDAL();
            Session["idActaEntrega"] = id;
            Session["auditoria"] = actaEntregaDAL.GetActaEntrega(id).numeroFolio;
            Session["tabla"] = "Acta de Entrega";
            ActaEntregaViewModel modelo = CargarActaEntrega(actaEntregaDAL.GetActaEntrega(id));
            return View(modelo);
        }
        public ActionResult Editar(int id)
        {
            AutorizarEditar();
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
            AutorizarEditar();
            actaEntregaDAL = new ActaEntregaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            AuditoriaViewModel auditoria_model = new AuditoriaViewModel();
            model.TiposActa = tablaGeneralDAL.Get("Actas", "tipoActa").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.TiposTestigo = tablaGeneralDAL.Get("Actas", "tipoTestigo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.Estados = tablaGeneralDAL.Get("Actas", "estadoActa").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            DateTime newDateTime = model.Fecha.Date + model.Hora.TimeOfDay;
            model.Fecha = newDateTime;
            auditoria_model.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "2").idTablaGeneral;
            auditoria_model.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "14").idTablaGeneral;
            auditoria_model.IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario;
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
                    auditoria_model.IdElemento = actaEntregaDAL.GetActaEntregaFolio(model.NumeroFolio).idActaEntrega;
                    auditoriaDAL.Add(ConvertirAuditoria(auditoria_model));
                    return Redirect("~/ActaEntrega/Detalle/" + model.IdActaEntrega);
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
            AuditoriaViewModel modelo = new AuditoriaViewModel();
            tablaGeneralDAL = new TablaGeneralDAL();
            actaEntregaDAL = new ActaEntregaDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            return new Auditorias
            {
                idAuditoria = modelo.IdAuditoria,
                accion = modelo.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "3").idTablaGeneral,
                idCategoria = modelo.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "14").idTablaGeneral,
                idUsuario = modelo.IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario,
                fecha = DateTime.Now,
                idElemento = actaEntregaDAL.GetActaEntrega(idActaEntrega).idActaEntrega

            };
        }
        public void CreatePDF(int id)
        {
            //--------------------------Creacion de los DataSet--------------------------
            actaEntregaDAL = new ActaEntregaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            personaDAL = new PersonaDAL();
            actaHallazgoDAL = new ActaHallazgoDAL();
            actaDecomisoDAL = new ActaDecomisoDAL();

            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = Path.Combine(Server.MapPath("~/PDFs"), "ReporteEntrega.rdlc");

            //Entrega
            ActasEntrega entrega = actaEntregaDAL.GetActaEntrega(id);
            List<ActasEntrega> entregas = new List<ActasEntrega>();
            entregas.Add(entrega);

            //Policias
            Policias policiaSupervisor = policiaDAL.GetPolicia(entrega.supervisor);
            List<Policias> policiasS = new List<Policias>();
            policiasS.Add(policiaSupervisor);

            Policias policiaEncargado = policiaDAL.GetPolicia(entrega.encargado);
            List<Policias> policiasE = new List<Policias>();
            policiasE.Add(policiaEncargado);

            //TablaGeneral
            TablaGeneral tipoDeActa = new TablaGeneral();
            List<TablaGeneral> tiposDeActas = new List<TablaGeneral>();
            tipoDeActa.descripcion = tablaGeneralDAL.Get(entrega.tipoActa).descripcion;
            tiposDeActas.Add(tipoDeActa);

            TablaGeneral tipoDeTestigo = new TablaGeneral();
            List<TablaGeneral> tiposDeTestigos = new List<TablaGeneral>();
            tipoDeTestigo.descripcion = tablaGeneralDAL.Get((int)entrega.tipoTestigo).descripcion;
            tiposDeTestigos.Add(tipoDeTestigo);

            //Persona
            Personas persona = personaDAL.GetPersona(entrega.recibe);
            List<Personas> personas = new List<Personas>();
            personas.Add(persona);

            viewer.LocalReport.DataSources.Add(new ReportDataSource("PerRecibeDataSet", personas));

            //Agregado a Data Set
            viewer.LocalReport.DataSources.Add(new ReportDataSource("EntregaDataSet", entregas));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("PEncargadoDataSet", policiasE));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("PSupervisorDataSet", policiasS));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("TGTipoDataSet", tiposDeActas));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("TGTestigoDataSet", tiposDeTestigos));

            //Actas de Hallazgo y Decomiso
            if (tipoDeActa.descripcion == "Hallazgo")
            {
                ActasHallazgo actaHallazgo = actaHallazgoDAL.GetActaHallazgo(entrega.idActaLigada);
                List<ActasHallazgo> hallazgos = new List<ActasHallazgo>();
                hallazgos.Add(actaHallazgo);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("HallazgoDataSet", hallazgos));

                ActasDecomiso actaDecomiso = new ActasDecomiso();
                List<ActasDecomiso> decomisos = new List<ActasDecomiso>();
                decomisos.Add(actaDecomiso);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("DecomisoDataSet", decomisos));

                Personas personaDec = new Personas();
                List<Personas> personasDec = new List<Personas>();
                personaDec.nombre = "N/A";
                personaDec.identificacion = "N/A";
                personasDec.Add(personaDec);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("PerDecomisadaDataSet", personasDec));
            }
            else
            {
                ActasDecomiso actaDecomiso = actaDecomisoDAL.GetActaDecomiso(entrega.idActaLigada);
                List<ActasDecomiso> decomisos = new List<ActasDecomiso>();
                decomisos.Add(actaDecomiso);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("DecomisoDataSet", decomisos));

                ActasHallazgo actaHallazgo = new ActasHallazgo();
                List<ActasHallazgo> hallazgos = new List<ActasHallazgo>();
                hallazgos.Add(actaHallazgo);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("HallazgoDataSet", hallazgos));

                Personas personaDec = personaDAL.GetPersona(actaDecomiso.idDecomisado);
                List<Personas> personasDec = new List<Personas>();
                personasDec.Add(personaDec);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("PerDecomisadaDataSet", personasDec));
            }

            //Tipos de Testigo
            if (tipoDeTestigo.descripcion == "Policía")
            {
                Policias policiaTestigo = policiaDAL.GetPolicia((int)entrega.testigo);
                List<Policias> policiasT = new List<Policias>();
                policiasT.Add(policiaTestigo);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("PTestigoDataSet", policiasT));

                Personas personaTes = new Personas();
                List<Personas> personasTes = new List<Personas>();
                personasTes.Add(personaTes);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("PerTestigoDataSet", personasTes));

                TablaGeneral noAplica = new TablaGeneral();
                List<TablaGeneral> noAplican = new List<TablaGeneral>();
                noAplican.Add(noAplica);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("aplicaDataSet", noAplican));
            }
            else
            {
                if (tipoDeTestigo.descripcion == "Persona")
                {
                    Personas personaTestigo = personaDAL.GetPersona((int)entrega.testigo);
                    List<Personas> personasTestigo = new List<Personas>();
                    personasTestigo.Add(personaTestigo);

                    viewer.LocalReport.DataSources.Add(new ReportDataSource("PerTestigoDataSet", personasTestigo));

                    Policias policiaTestigo = new Policias();
                    List<Policias> policiasT = new List<Policias>();
                    policiasT.Add(policiaTestigo);

                    viewer.LocalReport.DataSources.Add(new ReportDataSource("PTestigoDataSet", policiasT));

                    TablaGeneral noAplica = new TablaGeneral();
                    List<TablaGeneral> noAplican = new List<TablaGeneral>();
                    noAplican.Add(noAplica);

                    viewer.LocalReport.DataSources.Add(new ReportDataSource("aplicaDataSet", noAplican));
                }
                else
                {
                    TablaGeneral noAplica = new TablaGeneral();
                    List<TablaGeneral> noAplican = new List<TablaGeneral>();
                    noAplica.descripcion = "N/A";
                    noAplican.Add(noAplica);

                    viewer.LocalReport.DataSources.Add(new ReportDataSource("aplicaDataSet", noAplican));

                    Policias policiaTestigo = new Policias();
                    List<Policias> policiasT = new List<Policias>();
                    policiasT.Add(policiaTestigo);

                    viewer.LocalReport.DataSources.Add(new ReportDataSource("PTestigoDataSet", policiasT));

                    Personas persona5 = new Personas();
                    List<Personas> personas5 = new List<Personas>();
                    personas.Add(persona);

                    viewer.LocalReport.DataSources.Add(new ReportDataSource("PerTestigoDataSet", personas5));
                }

            }

            if (entrega.razonSocial != null)
            {
                Personas razonsocial = personaDAL.GetPersona((int)entrega.razonSocial);
                List<Personas> razonessociales = new List<Personas>();
                razonessociales.Add(razonsocial);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("PerRSDataSet", razonessociales));

                TablaGeneral noAplica = new TablaGeneral();
                List<TablaGeneral> noAplican = new List<TablaGeneral>();
                noAplican.Add(noAplica);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("aplicaRSDataSet", noAplican));
            }
            else
            {
                TablaGeneral noAplica = new TablaGeneral();
                List<TablaGeneral> noAplican = new List<TablaGeneral>();
                noAplica.descripcion = "N/A";
                noAplican.Add(noAplica);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("aplicaRSDataSet", noAplican));

                Personas razonsocial = new Personas();
                List<Personas> razonessociales = new List<Personas>();
                razonessociales.Add(razonsocial);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("PerRSDataSet", razonessociales));
            }

            //--------------------------Creacion de las variables--------------------------
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            var deviceInfo = @"<DeviceInfo>
            <EmbedFonts>None</EmbedFonts>
            <OutputFormat>PDF</OutputFormat>
            <PageWidth>8.5in</PageWidth>
            <PageHeight>11in</PageHeight>
            <MarginTop>0.25in</MarginTop>
            <MarginLeft>0.25in</MarginLeft>
            <MarginRight>0.25in</MarginRight>
            <MarginBottom>0.25in</MarginBottom>
            </DeviceInfo>";

            byte[] bytes = viewer.LocalReport.Render("PDF", deviceInfo, out mimeType, out encoding, out extension, out streamIds, out warnings);

            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename=" + "Acta de entrega No. " + entrega.numeroFolio + "." + extension);
            Response.BinaryWrite(bytes);
            Response.Flush();
        }
    }
}