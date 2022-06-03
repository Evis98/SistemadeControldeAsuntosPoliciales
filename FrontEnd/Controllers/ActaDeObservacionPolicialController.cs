using BackEnd;
using BackEnd.DAL;
using FrontEnd.Models.ViewModels;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
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

        //Metodos Útiles
        public ActasDeObservacionPolicial ConvertirActaDeObservacionPolicial(ActaDeObservacionPolicialViewModel modelo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            personaDAL = new PersonaDAL();
            ActasDeObservacionPolicial actaDeObservacionPolicial = new ActasDeObservacionPolicial()
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
            return actaDeObservacionPolicial;
        }
        public ActaDeObservacionPolicialViewModel CargarActaDeObservacionPolicial(ActasDeObservacionPolicial actaDeObservacionPolicial)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            personaDAL = new PersonaDAL();
            ActaDeObservacionPolicialViewModel modelo = new ActaDeObservacionPolicialViewModel()
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

            return modelo;
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
                    if (policia.nombre.Contains(nombre.ToUpper()))
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
                if (persona.nombre.Contains(nombre.ToUpper()))
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

        //Metodos de las Vistas
        public ActionResult Index(string filtrosSeleccionado, string busqueda, string busquedaFechaInicioH, string busquedaFechaFinalH)
        {
            Autorizar();
            actaDeObservacionPolicialDAL = new ActaDeObservacionPolicialDAL();
            policiaDAL = new PoliciaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            personaDAL = new PersonaDAL();

            //Carga combobox busqueda
            List<TablaGeneral> comboindex = tablaGeneralDAL.Get("ActasDeObservacionPolicial", "index");
            List<SelectListItem> items = comboindex.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.descripcion
                };
            });
            ViewBag.items = items;

            //Carga lista de policias
            List<ActaDeObservacionPolicialViewModel> actasDeObservacionPolicial = new List<ActaDeObservacionPolicialViewModel>();
            List<ActaDeObservacionPolicialViewModel> actasDeObservacionPolicialFiltradas = new List<ActaDeObservacionPolicialViewModel>();
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
                        if (policiaDAL.GetPoliciaCedula(actaDeObservacionPolicial.OficialActuante).nombre.Contains(busqueda.ToUpper()))
                        {
                            actasDeObservacionPolicialFiltradas.Add(actaDeObservacionPolicial);
                        }
                    }
                    if (filtrosSeleccionado == "Persona Interesada")
                    {
                        if (personaDAL.GetPersonaIdentificacion(actaDeObservacionPolicial.IdInteresado).nombre.Contains(busqueda.ToUpper()))
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
            AuditoriaViewModel auditoria_model = new AuditoriaViewModel
            {
                Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "1").idTablaGeneral,
                IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "8").idTablaGeneral,
                IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario
            };

            model.NumeroFolio = (actaDeObservacionPolicialDAL.GetCount() + 1).ToString() + "-" + DateTime.Now.Year;
            DateTime newDateTime = model.Fecha.Date + model.Hora.TimeOfDay;
            model.Fecha = newDateTime;
            model.Estado = int.Parse(tablaGeneralDAL.GetCodigo("Actas", "estadoActa", "1").codigo);

            try
            {
                if (ModelState.IsValid)
                {
                    actaDeObservacionPolicialDAL.Add(ConvertirActaDeObservacionPolicial(model));
                    auditoria_model.IdElemento = actaDeObservacionPolicialDAL.GetActaDeObservacionPolicialFolio(model.NumeroFolio).idActaDeObservacionPolicial;
                    auditoriaDAL.Add(ConvertirAuditoria(auditoria_model));
                    int aux = actaDeObservacionPolicialDAL.GetActaDeObservacionPolicialFolio(model.NumeroFolio).idActaDeObservacionPolicial;
                    return Redirect("~/ActaDeObservacionPolicial/Detalle/" + aux);

                }
                model.Distritos = tablaGeneralDAL.Get("Generales", "distrito").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
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
            Session["auditoria"] = actaDeObservacionPolicialDAL.GetActaDeObservacionPolicial(id).numeroFolio;
            Session["tabla"] = "Acta de Observación Policial";
            ActaDeObservacionPolicialViewModel modelo = CargarActaDeObservacionPolicial(actaDeObservacionPolicialDAL.GetActaDeObservacionPolicial(id));
            return View(modelo);
        }

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
            AuditoriaViewModel auditoria_model = new AuditoriaViewModel
            {
                Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "2").idTablaGeneral,
                IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "8").idTablaGeneral,
                IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario
            };

            DateTime newDateTime = model.Fecha.Date + model.Hora.TimeOfDay;
            model.Fecha = newDateTime;
            int estado = actaDeObservacionPolicialDAL.GetActaDeObservacionPolicial(model.IdActaDeObservacionPolicial).estado;

            try
            {
                if (ModelState.IsValid)
                {
                    if (tablaGeneralDAL.GetCodigo("Actas", "estadoActa", model.Estado.ToString()).idTablaGeneral != estado)
                    {
                        auditoriaDAL.Add(CambiarEstadoAuditoria(model.IdActaDeObservacionPolicial));
                    }
                    actaDeObservacionPolicialDAL.Edit(ConvertirActaDeObservacionPolicial(model));
                    auditoria_model.IdElemento = actaDeObservacionPolicialDAL.GetActaDeObservacionPolicialFolio(model.NumeroFolio).idActaDeObservacionPolicial;
                    auditoriaDAL.Add(ConvertirAuditoria(auditoria_model));
                    return Redirect("~/ActaDeObservacionPolicial/Detalle/" + model.IdActaDeObservacionPolicial);
                }
                model.Distritos = tablaGeneralDAL.Get("Generales", "distrito").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                model.Estados = tablaGeneralDAL.Get("Actas", "estadoActa").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
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
            AuditoriaViewModel modelo = new AuditoriaViewModel();
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
        public void CreatePDF(int id)
        {
            //--------------------------Creacion de los DataSet--------------------------
            actaDeObservacionPolicialDAL = new ActaDeObservacionPolicialDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            personaDAL = new PersonaDAL();

            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = Path.Combine(Server.MapPath("~/PDFs"), "ReporteObservacionPolicial.rdlc");

            //Hallazgos
            ActasDeObservacionPolicial observacion = actaDeObservacionPolicialDAL.GetActaDeObservacionPolicial(id);
            List<ActasDeObservacionPolicial> observaciones = new List<ActasDeObservacionPolicial>();
            observaciones.Add(observacion);

            //Policias
            Policias policiaActuante = policiaDAL.GetPolicia(observacion.oficialActuante);
            List<Policias> policiasActuantes = new List<Policias>();
            policiasActuantes.Add(policiaActuante);

            Policias policiaAcompanante = policiaDAL.GetPolicia(observacion.oficialAcompanante);
            List<Policias> policiasAcompanantes = new List<Policias>();
            policiasAcompanantes.Add(policiaAcompanante);

            //Personas
            Personas interesado = personaDAL.GetPersona(observacion.idInteresado);
            List<Personas> interesados = new List<Personas>();
            interesados.Add(interesado);

            //TablaGeneral
            TablaGeneral distrito = new TablaGeneral();
            List<TablaGeneral> distritos = new List<TablaGeneral>();
            distrito.descripcion = tablaGeneralDAL.Get(observacion.distrito).descripcion;
            distritos.Add(distrito);

            //Agregado a Data Set
            viewer.LocalReport.DataSources.Add(new ReportDataSource("ObservacionDataSet", observaciones));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("ActuanteDataSet", policiasActuantes));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("AcompañanteDataSet", policiasAcompanantes));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("InteresadoDataSet", interesados));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DistritoDataSet", distritos));

            //Tipos de Testigo
            if (observacion.observaciones != null)
            {
                TablaGeneral noAplica = new TablaGeneral();
                List<TablaGeneral> noAplican = new List<TablaGeneral>();
                noAplican.Add(noAplica);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("aplicaDataSet", noAplican));
            }
            else
            {
                TablaGeneral noAplica = new TablaGeneral();
                List<TablaGeneral> noAplican = new List<TablaGeneral>();
                noAplica.descripcion = "No Aplica";
                noAplican.Add(noAplica);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("aplicaDataSet", noAplican));
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
            Response.AddHeader("content-disposition", "attachment; filename=" + "Acta de observación policial No. " + observacion.numeroFolio + "." + extension);
            Response.BinaryWrite(bytes);
            Response.Flush();
        }
    }
}