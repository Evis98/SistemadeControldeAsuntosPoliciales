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
    public class ActaNotificacionController : Controller
    {
        ITablaGeneralDAL tablaGeneralDAL;
        IActaDeNotificacionDAL actaDeNotificacionDAL;
        IPoliciaDAL policiaDAL;
        IPersonaDAL personaDAL;
        IAuditoriaDAL auditoriaDAL;
        IUsuarioDAL usuarioDAL;

        //metodos útiles
        public ActasDeNotificacion ConvertirActaDeNotificacion(ActaNotificacionViewModel modelo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            personaDAL = new PersonaDAL();
            actaDeNotificacionDAL = new ActaDeNotificacionDAL();

            ActasDeNotificacion actaNotificacion = new ActasDeNotificacion
            {
                idActaDeNotificacion = modelo.IdActaDeNotificacion,
                numeroFolio = modelo.NumeroFolio,
                personaNotificada = personaDAL.GetPersonaIdentificacion(modelo.Notificado).idPersona,
                oficialActuante = policiaDAL.GetPoliciaCedula(modelo.Oficial).idPolicia,
                distrito = tablaGeneralDAL.GetCodigo("Generales", "distrito", modelo.Distrito.ToString()).idTablaGeneral,
                fechaHora = modelo.Fecha,
                direccionExacta = modelo.DireccionExactaProcedimiento,
                barrio = modelo.Barrio,
                disposiciones = modelo.Disposiciones,
                tipoTestigo = tablaGeneralDAL.GetCodigo("Actas", "tipoTestigo", modelo.TipoTestigo.ToString()).idTablaGeneral,
                estado = tablaGeneralDAL.GetCodigo("Actas", "estadoActa", modelo.Estado.ToString()).idTablaGeneral
            };

            if (modelo.TipoTestigo == 1)
            {
                actaNotificacion.testigo = policiaDAL.GetPoliciaCedula(modelo.IdTestigoPolicia).idPolicia;
            }
            if (modelo.TipoTestigo == 2)
            {
                actaNotificacion.testigo = personaDAL.GetPersonaIdentificacion(modelo.IdTestigoPersona).idPersona;
            }
            if (modelo.Estado != 0)
            {
                actaNotificacion.estado = tablaGeneralDAL.GetCodigo("Actas", "estadoActa", modelo.Estado.ToString()).idTablaGeneral;
            }
            return actaNotificacion;
        }

        public ActaNotificacionViewModel CargarActaNotificacion(ActasDeNotificacion actaDeNotificacion)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            personaDAL = new PersonaDAL();
            ActaNotificacionViewModel acta = new ActaNotificacionViewModel
            {
                IdActaDeNotificacion = actaDeNotificacion.idActaDeNotificacion,
                NumeroFolio = actaDeNotificacion.numeroFolio,
                TipoTestigo = int.Parse(tablaGeneralDAL.Get(actaDeNotificacion.tipoTestigo).codigo),
                Estado = int.Parse(tablaGeneralDAL.Get(actaDeNotificacion.estado).codigo),
                VistaEstadoActa = tablaGeneralDAL.Get(actaDeNotificacion.estado).descripcion,
                Oficial = policiaDAL.GetPolicia(actaDeNotificacion.oficialActuante).cedula,
                Notificado = personaDAL.GetPersona(actaDeNotificacion.personaNotificada).identificacion,
                Fecha = actaDeNotificacion.fechaHora,
                Hora = actaDeNotificacion.fechaHora,
                Distrito = int.Parse(tablaGeneralDAL.Get(actaDeNotificacion.distrito).codigo),
                DireccionExactaProcedimiento = actaDeNotificacion.direccionExacta,
                Barrio = actaDeNotificacion.barrio,
                Disposiciones = actaDeNotificacion.disposiciones,
                VistaTipoTestigo = tablaGeneralDAL.Get(actaDeNotificacion.tipoTestigo).descripcion,
                VistaOficialActuante = policiaDAL.GetPolicia(actaDeNotificacion.oficialActuante).nombre,
                VistaPersonaNotificada = personaDAL.GetPersona(actaDeNotificacion.personaNotificada).nombre,
                VistaDistrito = tablaGeneralDAL.Get(actaDeNotificacion.distrito).descripcion
            };

            if (actaDeNotificacion.testigo != null)
            {
                if (tablaGeneralDAL.Get(actaDeNotificacion.tipoTestigo).codigo == "1")
                {
                    acta.VistaTestigo = policiaDAL.GetPolicia((int)actaDeNotificacion.testigo).nombre;
                    acta.IdTestigoPolicia = policiaDAL.GetPolicia((int)actaDeNotificacion.testigo).cedula;
                    acta.VistaIdTestigo = policiaDAL.GetPolicia((int)actaDeNotificacion.testigo).cedula;
                }
                if (tablaGeneralDAL.Get(actaDeNotificacion.tipoTestigo).codigo == "2")
                {
                    acta.VistaTestigo = personaDAL.GetPersona((int)actaDeNotificacion.testigo).nombre;
                    acta.IdTestigoPersona = personaDAL.GetPersona((int)actaDeNotificacion.testigo).identificacion;
                    acta.VistaIdTestigo = personaDAL.GetPersona((int)actaDeNotificacion.testigo).identificacion;
                }
            }
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
            actaDeNotificacionDAL = new ActaDeNotificacionDAL();
            List<PersonaViewModel> lista = new List<PersonaViewModel>();
            foreach (Personas persona in personas)
            {
                PersonaViewModel aux = new PersonaViewModel
                {
                    Identificacion = persona.identificacion,
                    NombrePersona = persona.nombre,
                    VecesNotificado = actaDeNotificacionDAL.VecesNotificado(persona.idPersona)
                };
                lista.Add(aux);
            }
            return lista;
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

        //metodos de las vistas de las actas
        public ActionResult Index(string filtrosSeleccionado, string busqueda, string busquedaFechaInicioH, string busquedaFechaFinalH)
        {
            Autorizar();
            actaDeNotificacionDAL = new ActaDeNotificacionDAL();
            policiaDAL = new PoliciaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();

            //Carga combobox busqueda
            List<TablaGeneral> comboindex = tablaGeneralDAL.Get("ActasDeNotificacion", "index");
            List<SelectListItem> items = comboindex.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.descripcion
                };
            });
            ViewBag.items = items;

            //Carga lista de actas de hallazgo
            List<ActaNotificacionViewModel> actasNotificacion = new List<ActaNotificacionViewModel>();
            List<ActaNotificacionViewModel> actasNotificacionFiltradas = new List<ActaNotificacionViewModel>();
            foreach (ActasDeNotificacion actaDeNotificacion in actaDeNotificacionDAL.Get())
            {
                actasNotificacion.Add(CargarActaNotificacion(actaDeNotificacion));
            }
            if (busqueda != null)
            {
                foreach (ActaNotificacionViewModel actaDeNotificacion in actasNotificacion)
                {
                    if (filtrosSeleccionado == "Número de Folio")
                    {
                        if (actaDeNotificacion.NumeroFolio.Contains(busqueda))
                        {
                            actasNotificacionFiltradas.Add(actaDeNotificacion);
                        }
                    }
                    if (filtrosSeleccionado == "Nombre Policía Actuante")
                    {
                        if (policiaDAL.GetPoliciaCedula(actaDeNotificacion.Oficial).nombre.Contains(busqueda))
                        {
                            actasNotificacionFiltradas.Add(actaDeNotificacion);
                        }
                    }
                    if (filtrosSeleccionado == "Cédula del Notificado")
                    {
                        if (actaDeNotificacion.Notificado.Contains(busqueda))
                        {
                            actasNotificacionFiltradas.Add(actaDeNotificacion);
                        }
                    }
                    if (filtrosSeleccionado == "Nombre del Notificado")
                    {
                        if (actaDeNotificacion.VistaPersonaNotificada.Contains(busqueda))
                        {
                            actasNotificacionFiltradas.Add(actaDeNotificacion);
                        }
                    }

                }
                if (filtrosSeleccionado == "Fecha")
                {
                    DateTime fechaInicio = DateTime.Parse(busquedaFechaInicioH);
                    DateTime fechaFinal = DateTime.Parse(busquedaFechaFinalH);
                    if (fechaInicio < fechaFinal)
                    {
                        if (actaDeNotificacionDAL.GetActaDeNotificacionRango(fechaInicio, fechaFinal) != null)
                        {
                            foreach (ActasDeNotificacion actaNotificacionFecha in actaDeNotificacionDAL.GetActaDeNotificacionRango(fechaInicio, fechaFinal).ToList())
                            {
                                actasNotificacionFiltradas.Add(CargarActaNotificacion(actaNotificacionFecha));
                            }
                        }
                    }
                }
                actasNotificacion = actasNotificacionFiltradas;
            }
            return View(actasNotificacion.OrderBy(x => x.NumeroFolio).ToList());


        }

        public ActionResult Nuevo()
        {
            Autorizar();
            tablaGeneralDAL = new TablaGeneralDAL();
            ActaNotificacionViewModel modelo = new ActaNotificacionViewModel()
            {
                Distritos = tablaGeneralDAL.Get("Generales", "distrito").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                TiposTestigo = tablaGeneralDAL.Get("Actas", "tipoTestigo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                Fecha = DateTime.Today

            };
            return View(modelo);
        }

        [HttpPost]
        public ActionResult Nuevo(ActaNotificacionViewModel model)
        {
            Autorizar();
            actaDeNotificacionDAL = new ActaDeNotificacionDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            AuditoriaViewModel auditoria_model = new AuditoriaViewModel
            {
                Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "1").idTablaGeneral,
                IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "12").idTablaGeneral,
                IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario
            };

            model.NumeroFolio = (actaDeNotificacionDAL.GetCount(model.Fecha.Date) + 1).ToString() + "-" + model.Fecha.Date.Year;
            DateTime newDateTime = model.Fecha.Date + model.Hora.TimeOfDay;
            model.Fecha = newDateTime;
            model.Estado = int.Parse(tablaGeneralDAL.GetCodigo("Actas", "estadoActa", "1").codigo);

            try
            {
                if (ModelState.IsValid)
                {
                    actaDeNotificacionDAL.Add(ConvertirActaDeNotificacion(model));
                    auditoria_model.IdElemento = actaDeNotificacionDAL.GetActaDeNotificacionFolio(model.NumeroFolio).idActaDeNotificacion;
                    auditoriaDAL.Add(ConvertirAuditoria(auditoria_model));
                    int aux = actaDeNotificacionDAL.GetActaDeNotificacionFolio(model.NumeroFolio).idActaDeNotificacion;
                    return Redirect("~/ActaNotificacion/Detalle/" + aux);

                }
                model.Distritos = tablaGeneralDAL.Get("Generales", "distrito").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                model.TiposTestigo = tablaGeneralDAL.Get("Actas", "tipoTestigo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
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
            actaDeNotificacionDAL = new ActaDeNotificacionDAL();
            Session["idActaNotificacion"] = id;
            Session["auditoria"] = actaDeNotificacionDAL.GetActaDeNotificacion(id).numeroFolio;
            Session["tabla"] = "Acta de Notificación";

            ActaNotificacionViewModel modelo = CargarActaNotificacion(actaDeNotificacionDAL.GetActaDeNotificacion(id));
            return View(modelo);
        }

        public ActionResult Editar(int id)
        {
            AutorizarEditar();
            tablaGeneralDAL = new TablaGeneralDAL();
            actaDeNotificacionDAL = new ActaDeNotificacionDAL();
            ActaNotificacionViewModel modelo = CargarActaNotificacion(actaDeNotificacionDAL.GetActaDeNotificacion(id));
            modelo.Distritos = tablaGeneralDAL.Get("Generales", "distrito").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            modelo.TiposTestigo = tablaGeneralDAL.Get("Actas", "tipoTestigo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            modelo.Estados = tablaGeneralDAL.Get("Actas", "estadoActa").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            return View(modelo);
        }

        [HttpPost]
        public ActionResult Editar(ActaNotificacionViewModel model)
        {
            AutorizarEditar();
            actaDeNotificacionDAL = new ActaDeNotificacionDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            AuditoriaViewModel auditoria_model = new AuditoriaViewModel
            {
                Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "2").idTablaGeneral,
                IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "12").idTablaGeneral,
                IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario
            };
            
            DateTime newDateTime = model.Fecha.Date + model.Hora.TimeOfDay;
            model.Fecha = newDateTime;
            int estado = actaDeNotificacionDAL.GetActaDeNotificacion(model.IdActaDeNotificacion).estado;
            try
            {
                if (ModelState.IsValid)
                {
                    if (tablaGeneralDAL.GetCodigo("Actas", "estadoActa", model.Estado.ToString()).idTablaGeneral != estado)
                    {
                        auditoriaDAL.Add(CambiarEstadoAuditoria(model.IdActaDeNotificacion));
                    }

                    actaDeNotificacionDAL.Edit(ConvertirActaDeNotificacion(model));
                    auditoria_model.IdElemento = actaDeNotificacionDAL.GetActaDeNotificacionFolio(model.NumeroFolio).idActaDeNotificacion;
                    auditoriaDAL.Add(ConvertirAuditoria(auditoria_model));
                    return Redirect("~/ActaNotificacion/Detalle/" + model.IdActaDeNotificacion);
                }
                model.Distritos = tablaGeneralDAL.Get("Generales", "distrito").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                model.Estados = tablaGeneralDAL.Get("Actas", "estadoActa").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                model.TiposTestigo = tablaGeneralDAL.Get("Actas", "tipoTestigo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
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
            actaDeNotificacionDAL = new ActaDeNotificacionDAL();
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
        public Auditorias CambiarEstadoAuditoria(int idActaDeNotificacion)
        {
            AuditoriaViewModel modelo = new AuditoriaViewModel();
            tablaGeneralDAL = new TablaGeneralDAL();
            actaDeNotificacionDAL = new ActaDeNotificacionDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            return new Auditorias
            {
                idAuditoria = modelo.IdAuditoria,
                accion = modelo.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "3").idTablaGeneral,
                idCategoria = modelo.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "12").idTablaGeneral,
                idUsuario = modelo.IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario,
                fecha = DateTime.Now,
                idElemento = actaDeNotificacionDAL.GetActaDeNotificacion(idActaDeNotificacion).idActaDeNotificacion

            };
        }

        public void CreatePDF(int id)
        {
            //--------------------------Creacion de los DataSet--------------------------
            actaDeNotificacionDAL = new ActaDeNotificacionDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            personaDAL = new PersonaDAL();

            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = Path.Combine(Server.MapPath("~/PDFs"), "ReporteNotificacion.rdlc");

            //Notificacion
            ActasDeNotificacion notificacion = actaDeNotificacionDAL.GetActaDeNotificacion(id);
            List<ActasDeNotificacion> notificaciones = new List<ActasDeNotificacion>();
            notificaciones.Add(notificacion);

            //Persona
            Personas notificado = personaDAL.GetPersona(notificacion.personaNotificada);
            List<Personas> notificados = new List<Personas>();
            notificados.Add(notificado);

            if (notificado.direccionPersona == null)
            {
                notificado.direccionPersona = "No aplica";
            }

            //Policia
            Policias actuante = policiaDAL.GetPolicia(notificacion.oficialActuante);
            List<Policias> actuantes = new List<Policias>();
            actuantes.Add(actuante);

            //TablaGeneral
            TablaGeneral tipoTestigo = new TablaGeneral();
            List<TablaGeneral> tiposTestigos = new List<TablaGeneral>();
            tipoTestigo.descripcion = tablaGeneralDAL.Get(notificacion.tipoTestigo).descripcion;
            tiposTestigos.Add(tipoTestigo);

            TablaGeneral distrito = new TablaGeneral();
            List<TablaGeneral> distritos = new List<TablaGeneral>();
            distrito.descripcion = tablaGeneralDAL.Get(notificacion.distrito).descripcion;
            distritos.Add(distrito);

            //Agregado a Data Set
            viewer.LocalReport.DataSources.Add(new ReportDataSource("NotificacionDataSet", notificaciones));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("NotificadoDataSet", notificados));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("ActuanteDataSet", actuantes));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("tipoTestigoDataSet", tiposTestigos));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("TGDistritoDataSet", distritos));

            //Tipos de Testigo
            if (tipoTestigo.descripcion == "Policía")
            {
                Policias policiaT = policiaDAL.GetPolicia((int)notificacion.testigo);
                List<Policias> policiasT = new List<Policias>();
                policiasT.Add(policiaT);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("PoliciaTDataSet", policiasT));

                Personas persona = new Personas();
                List<Personas> personas = new List<Personas>();
                personas.Add(persona);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("PersonaTDataSet", personas));

                TablaGeneral noAplica = new TablaGeneral();
                List<TablaGeneral> noAplican = new List<TablaGeneral>();
                noAplican.Add(noAplica);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("aplicaDataSet", noAplican));
            }
            else
            {
                if (tipoTestigo.descripcion == "Persona")
                {
                    Personas personaTestigo = personaDAL.GetPersona((int)notificacion.testigo);
                    List<Personas> personasTestigo = new List<Personas>();
                    personasTestigo.Add(personaTestigo);

                    viewer.LocalReport.DataSources.Add(new ReportDataSource("PersonaTDataSet", personasTestigo));

                    Policias policiaTestigo = new Policias();
                    List<Policias> policiasT = new List<Policias>();
                    policiasT.Add(policiaTestigo);

                    viewer.LocalReport.DataSources.Add(new ReportDataSource("PoliciaTDataSet", policiasT));

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

                    Policias policiaTestigo = new Policias();
                    List<Policias> policiasT = new List<Policias>();
                    policiasT.Add(policiaTestigo);

                    viewer.LocalReport.DataSources.Add(new ReportDataSource("PoliciaTDataSet", policiasT));

                    Personas persona = new Personas();
                    List<Personas> personas = new List<Personas>();
                    personas.Add(persona);

                    viewer.LocalReport.DataSources.Add(new ReportDataSource("PersonaTDataSet", personas));
                }

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
            Response.AddHeader("content-disposition", "attachment; filename=" + "Acta de notificación No. " + notificacion.numeroFolio + "." + extension);
            Response.BinaryWrite(bytes);
            Response.Flush();
        }
    }
}