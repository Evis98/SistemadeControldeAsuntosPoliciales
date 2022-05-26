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
        public ActasDeNotificacion ConvertirActaDeNotificacion(ActaNotificacionViewModel model)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            personaDAL = new PersonaDAL();
            actaDeNotificacionDAL = new ActaDeNotificacionDAL();

            ActasDeNotificacion acta = new ActasDeNotificacion();
            {
                acta.idActaDeNotificacion = model.IdActaDeNotificacion;
                acta.numeroFolio = model.NumeroFolio;
                acta.personaNotificada = personaDAL.GetPersonaIdentificacion(model.Notificado).idPersona;
                acta.oficialActuante = policiaDAL.GetPoliciaCedula(model.Oficial).idPolicia;
                acta.distrito = tablaGeneralDAL.GetCodigo("Generales", "distrito", model.Distrito.ToString()).idTablaGeneral;
                acta.fechaHora = model.Fecha;
                acta.direccionExacta = model.DireccionExactaProcedimiento;
                acta.barrio = model.Barrio;
                acta.disposiciones = model.Disposiciones;
                acta.tipoTestigo = tablaGeneralDAL.GetCodigo("Actas", "tipoTestigo", model.TipoTestigo.ToString()).idTablaGeneral;
                acta.estado = tablaGeneralDAL.GetCodigo("Actas", "estadoActa", model.Estado.ToString()).idTablaGeneral;
                if (model.TipoTestigo != 3)
                {
                    if (model.TipoTestigo == 1)
                    {
                        if (model.IdTestigoPolicia != null && policiaDAL.CedulaPoliciaExiste(model.IdTestigoPolicia))
                        {
                            acta.testigo = policiaDAL.GetPoliciaCedula(model.IdTestigoPolicia).idPolicia;
                        }
                        else
                        {
                            acta.tipoTestigo = tablaGeneralDAL.GetCodigo("Actas", "tipoTestigo", "3").idTablaGeneral;
                        }
                    }
                    else if (model.TipoTestigo == 2)
                    {
                        if (model.IdTestigoPersona != null && personaDAL.IdentificacionExiste(model.IdTestigoPersona))
                        {
                            acta.testigo = personaDAL.GetPersonaIdentificacion(model.IdTestigoPersona).idPersona;
                        }
                        else
                        {
                            acta.tipoTestigo = tablaGeneralDAL.GetCodigo("Actas", "tipoTestigo", "3").idTablaGeneral;
                        }
                    }
                }

            };
            return acta;
        }

        public ActaNotificacionViewModel CargarActaNotificacion(ActasDeNotificacion actaDeNotificacion)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            personaDAL = new PersonaDAL();
            ActaNotificacionViewModel acta = new ActaNotificacionViewModel();

            acta.IdActaDeNotificacion = actaDeNotificacion.idActaDeNotificacion;
            acta.NumeroFolio = actaDeNotificacion.numeroFolio;
            acta.TipoTestigo = int.Parse(tablaGeneralDAL.Get(actaDeNotificacion.tipoTestigo).codigo);


            if (int.Parse(tablaGeneralDAL.Get(actaDeNotificacion.tipoTestigo).codigo) != 3 && actaDeNotificacion.testigo != null)
            {
                if (int.Parse(tablaGeneralDAL.Get(actaDeNotificacion.tipoTestigo).codigo) == 1 && policiaDAL.GetPolicia((int)actaDeNotificacion.testigo) != null)
                {
                    acta.VistaTestigo = policiaDAL.GetPolicia((int)actaDeNotificacion.testigo).nombre;
                    acta.IdTestigoPolicia = policiaDAL.GetPolicia((int)actaDeNotificacion.testigo).cedula;
                    acta.VistaIdTestigo = policiaDAL.GetPolicia((int)actaDeNotificacion.testigo).cedula;
                }
                else if (int.Parse(tablaGeneralDAL.Get(actaDeNotificacion.tipoTestigo).codigo) == 2 && personaDAL.GetPersona((int)actaDeNotificacion.testigo) != null)
                {
                    acta.VistaTestigo = personaDAL.GetPersona((int)actaDeNotificacion.testigo).nombre;
                    acta.IdTestigoPersona = personaDAL.GetPersona((int)actaDeNotificacion.testigo).identificacion;
                    acta.VistaIdTestigo = personaDAL.GetPersona((int)actaDeNotificacion.testigo).identificacion;
                }
            }

            acta.Estado = int.Parse(tablaGeneralDAL.Get(actaDeNotificacion.estado).codigo);
            acta.VistaEstadoActa = tablaGeneralDAL.Get(actaDeNotificacion.estado).descripcion;
            acta.Oficial = policiaDAL.GetPolicia(actaDeNotificacion.oficialActuante).cedula;
            acta.Notificado = personaDAL.GetPersona(actaDeNotificacion.personaNotificada).identificacion;
            acta.Fecha = actaDeNotificacion.fechaHora;
            acta.Hora = actaDeNotificacion.fechaHora;
            acta.Distrito = int.Parse(tablaGeneralDAL.Get(actaDeNotificacion.distrito).codigo);
            acta.DireccionExactaProcedimiento = actaDeNotificacion.direccionExacta;
            acta.Barrio = actaDeNotificacion.barrio;
            acta.Disposiciones = actaDeNotificacion.disposiciones;

            acta.VistaTipoTestigo = tablaGeneralDAL.Get(actaDeNotificacion.tipoTestigo).descripcion;
            acta.VistaOficialActuante = policiaDAL.GetPolicia(actaDeNotificacion.oficialActuante).nombre;
            acta.VistaPersonaNotificada = personaDAL.GetPersona(actaDeNotificacion.personaNotificada).nombre;
            acta.VistaDistrito = tablaGeneralDAL.Get(actaDeNotificacion.distrito).descripcion;

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
                //aux.VecesNotificado = actaDeNotificacionDAL.VecesNotificado(persona.idPersona);
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


        public ActionResult Index(string filtrosSeleccionado, string busqueda, string busquedaFechaInicioH, string busquedaFechaFinalH)
        {
            Autorizar();
            actaDeNotificacionDAL = new ActaDeNotificacionDAL();
            policiaDAL = new PoliciaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            List<ActaNotificacionViewModel> actasNotificacion = new List<ActaNotificacionViewModel>();
            List<ActaNotificacionViewModel> actasNotificacionFiltradas = new List<ActaNotificacionViewModel>();
            List<TablaGeneral> comboindex = tablaGeneralDAL.Get("ActasDeNotificacion", "index");

            List<SelectListItem> items = comboindex.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.descripcion
                };
            });
            ViewBag.items = items;
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
            model.Distritos = tablaGeneralDAL.Get("Generales", "distrito").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.TiposTestigo = tablaGeneralDAL.Get("Actas", "tipoTestigo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.NumeroFolio = (actaDeNotificacionDAL.GetCount(model.Fecha.Date) + 1).ToString() + "-" + model.Fecha.Date.Year;
            DateTime newDateTime = model.Fecha.Date + model.Hora.TimeOfDay;
            model.Fecha = newDateTime;
            model.Estado = int.Parse(tablaGeneralDAL.GetCodigo("Actas", "estadoActa", "1").codigo);
            model.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "1").idTablaGeneral;
            model.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "12").idTablaGeneral;
            model.IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario;
            try
            {   
                if (ModelState.IsValid)
                {
                    actaDeNotificacionDAL.Add(ConvertirActaDeNotificacion(model));
                    model.IdElemento = actaDeNotificacionDAL.GetActaDeNotificacionFolio(model.NumeroFolio).idActaDeNotificacion;
                    auditoriaDAL.Add(ConvertirAuditoria(model));
                    int aux = actaDeNotificacionDAL.GetActaDeNotificacionFolio(model.NumeroFolio).idActaDeNotificacion;
                    return Redirect("~/ActaNotificacion/Detalle/" + aux);

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
            actaDeNotificacionDAL = new ActaDeNotificacionDAL();
            Session["idActaNotificacion"] = id;            
            Session["numeroFolio"] = actaDeNotificacionDAL.GetActaDeNotificacion(id).numeroFolio;
            ActaNotificacionViewModel modelo = CargarActaNotificacion(actaDeNotificacionDAL.GetActaDeNotificacion(id));
            return View(modelo);
        }

        //Devuelve la página de edición de policías con sus apartados llenos
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
            model.Distritos = tablaGeneralDAL.Get("Generales", "distrito").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.Estados = tablaGeneralDAL.Get("Actas", "estadoActa").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.TiposTestigo = tablaGeneralDAL.Get("Actas", "tipoTestigo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            DateTime newDateTime = model.Fecha.Date + model.Hora.TimeOfDay;
            model.Fecha = newDateTime;
            model.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "2").idTablaGeneral;
            model.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "12").idTablaGeneral;
            model.IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario;
            int estado = actaDeNotificacionDAL.GetActaDeNotificacion(model.IdActaDeNotificacion).estado;
            try
            {
                if (ModelState.IsValid)
                {
                    if (tablaGeneralDAL.GetCodigo("Actas", "estadoActa", model.Estado.ToString()).idTablaGeneral != estado && model.IdActaDeNotificacion != 0)
                    {
                        auditoriaDAL.Add(CambiarEstadoAuditoria(model.IdActaDeNotificacion));
                    }

                    actaDeNotificacionDAL.Edit(ConvertirActaDeNotificacion(model));
                    model.IdElemento = actaDeNotificacionDAL.GetActaDeNotificacionFolio(model.NumeroFolio).idActaDeNotificacion;
                    auditoriaDAL.Add(ConvertirAuditoria(model));
                    return Redirect("~/ActaNotificacion/Detalle/" + model.IdActaDeNotificacion);
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Auditorias ConvertirAuditoria(ActaNotificacionViewModel modelo)
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
            ActaNotificacionViewModel modelo = new ActaNotificacionViewModel();
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
                    noAplica.descripcion = "N/A";
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