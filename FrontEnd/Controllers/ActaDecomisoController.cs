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
    public class ActaDecomisoController : Controller
    {
        ITablaGeneralDAL tablaGeneralDAL;
        IActaDecomisoDAL actaDecomisoDAL;
        IPoliciaDAL policiaDAL;
        IAuditoriaDAL auditoriaDAL;
        IUsuarioDAL usuarioDAL;
        IPersonaDAL personaDAL;

        //metodos útiles
        public ActasDecomiso ConvertirActaDecomiso(ActaDecomisoViewModel modelo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            personaDAL = new PersonaDAL();
            return new ActasDecomiso
            {
                idActaDecomiso = modelo.IdActaDecomiso,
                numeroFolio = modelo.NumeroFolio,
                fecha = modelo.Fecha,
                idDecomisado = personaDAL.GetPersonaIdentificacion(modelo.IdDecomisado).idPersona,
                estadoCivilDecomisado = tablaGeneralDAL.GetCodigo("Generales", "estadoCivil", modelo.EstadoCivil.ToString()).idTablaGeneral,
                lugarProcedimiento = modelo.LugarDelProcedimiento,
                inventario = modelo.Inventario,
                observaciones = modelo.Observaciones,
                oficialAcompanante = policiaDAL.GetPoliciaCedula(modelo.OficialAcompanante).idPolicia,
                oficialActuante = policiaDAL.GetPoliciaCedula(modelo.OficialActuante).idPolicia,
                supervisorDecomiso = policiaDAL.GetPoliciaCedula(modelo.Supervisor).idPolicia,
                estado = tablaGeneralDAL.GetCodigo("Actas", "estadoActa", modelo.Estado.ToString()).idTablaGeneral
            };
        }
        public ActaDecomisoViewModel CargarActaDecomiso(ActasDecomiso actaDecomiso)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            personaDAL = new PersonaDAL();

            ActaDecomisoViewModel acta = new ActaDecomisoViewModel
            {
                IdActaDecomiso = actaDecomiso.idActaDecomiso,
                NumeroFolio = actaDecomiso.numeroFolio,
                OficialAcompanante = policiaDAL.GetPolicia(actaDecomiso.oficialAcompanante).cedula,
                OficialActuante = policiaDAL.GetPolicia(actaDecomiso.oficialActuante).cedula,
                Supervisor = policiaDAL.GetPolicia(actaDecomiso.supervisorDecomiso).cedula,
                EstadoCivil = int.Parse(tablaGeneralDAL.Get(actaDecomiso.estadoCivilDecomisado).codigo),
                VistaTipoEstadoCivil = tablaGeneralDAL.Get(actaDecomiso.estadoCivilDecomisado).descripcion,
                Fecha = actaDecomiso.fecha,
                Hora = actaDecomiso.fecha,
                NombreDecomisado = personaDAL.GetPersona(actaDecomiso.idDecomisado).nombre,
                IdDecomisado = personaDAL.GetPersona(actaDecomiso.idDecomisado).identificacion,
                LugarDelProcedimiento = actaDecomiso.lugarProcedimiento,
                Inventario = actaDecomiso.inventario,
                Observaciones = actaDecomiso.observaciones,
                Estado = int.Parse(tablaGeneralDAL.Get(actaDecomiso.estado).codigo),
                VistaEstadoActa = tablaGeneralDAL.Get(actaDecomiso.estado).descripcion,
                VistaOficialAcompanante = policiaDAL.GetPolicia(actaDecomiso.oficialAcompanante).nombre,
                VistaOficialActuante = policiaDAL.GetPolicia(actaDecomiso.oficialActuante).nombre,
                VistaPoliciaSupervisor = policiaDAL.GetPolicia(actaDecomiso.supervisorDecomiso).nombre
            };
            if (personaDAL.GetPersona(actaDecomiso.idDecomisado).telefonoCelular != null)
            {
                acta.TelefonoDecomisado = personaDAL.GetPersona(actaDecomiso.idDecomisado).telefonoCelular;
            }
            if (personaDAL.GetPersona(actaDecomiso.idDecomisado).direccionPersona != null)
            {
                acta.DireccionDecomisado = personaDAL.GetPersona(actaDecomiso.idDecomisado).direccionPersona;
            }
            return acta;
        }

        public string ToUpperCheckForNull(string input)
        {

            string retval = input;

            if (!string.IsNullOrEmpty(retval))
            {
                retval = retval.ToUpper();
            }

            return retval;

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

        //metodos de las vistas de las actas
        public ActionResult Index(string filtrosSeleccionado, string busqueda, string busquedaFechaInicioH, string busquedaFechaFinalH)
        {
            Autorizar();
            actaDecomisoDAL = new ActaDecomisoDAL();
            policiaDAL = new PoliciaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();

            //Carga combobox busqueda
            List<TablaGeneral> comboindex = tablaGeneralDAL.Get("ActasDecomiso", "index");
            List<SelectListItem> items = comboindex.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.descripcion
                };
            });
            ViewBag.items = items;

            //Carga lista de actas de decomiso
            List<ActaDecomisoViewModel> actasDecomiso = new List<ActaDecomisoViewModel>();
            List<ActaDecomisoViewModel> actasDecomisoFiltradas = new List<ActaDecomisoViewModel>();
            foreach (ActasDecomiso actaDecomiso in actaDecomisoDAL.Get())
            {
                actasDecomiso.Add(CargarActaDecomiso(actaDecomiso));
            }
            if (busqueda != null)
            {

                foreach (ActaDecomisoViewModel actaDecomiso in actasDecomiso)
                {
                    if (filtrosSeleccionado == "Número de Folio")
                    {
                        if (actaDecomiso.NumeroFolio.Contains(busqueda))
                        {
                            actasDecomisoFiltradas.Add(actaDecomiso);
                        }
                    }
                    if (filtrosSeleccionado == "Nombre Policía Actuante")
                    {
                        if (policiaDAL.GetPoliciaCedula(actaDecomiso.OficialActuante).nombre.Contains(busqueda.ToUpper()))
                        {
                            actasDecomisoFiltradas.Add(actaDecomiso);
                        }
                    }
                    if (filtrosSeleccionado == "Nombre a Quién Decomisan")
                    {
                        if (actaDecomiso.NombreDecomisado.Contains(busqueda.ToUpper()))
                        {
                            actasDecomisoFiltradas.Add(actaDecomiso);
                        }
                    }
                }
                if (filtrosSeleccionado == "Fecha")
                {
                    DateTime fechaInicio = DateTime.Parse(busquedaFechaInicioH);
                    DateTime fechaFinal = DateTime.Parse(busquedaFechaFinalH);
                    if (fechaInicio < fechaFinal)
                    {
                        if (actaDecomisoDAL.GetActaDecomisoRango(fechaInicio, fechaFinal) != null)
                        {
                            foreach (ActasDecomiso actaDecomisoFecha in actaDecomisoDAL.GetActaDecomisoRango(fechaInicio, fechaFinal).ToList())
                            {
                                actasDecomisoFiltradas.Add(CargarActaDecomiso(actaDecomisoFecha));
                            }
                        }
                    }
                }
                actasDecomiso = actasDecomisoFiltradas;
            }
            return View(actasDecomiso.OrderBy(x => x.NumeroFolio).ToList());

        }
        public ActionResult Nuevo()
        {
            Autorizar();
            tablaGeneralDAL = new TablaGeneralDAL();
            ActaDecomisoViewModel modelo = new ActaDecomisoViewModel()
            {
                EstadosCivil = tablaGeneralDAL.Get("Generales", "estadoCivil").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                Fecha = DateTime.Today,
                Estados = tablaGeneralDAL.Get("Actas", "estadoActa").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo })
            };
            return View(modelo);
        }

        [HttpPost]
        public ActionResult Nuevo(ActaDecomisoViewModel model)
        {
            Autorizar();
            actaDecomisoDAL = new ActaDecomisoDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            AuditoriaViewModel auditoria_model = new AuditoriaViewModel
            {
                Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "1").idTablaGeneral,
                IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "9").idTablaGeneral,
                IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario
            };
         
            model.Estado = int.Parse(tablaGeneralDAL.GetCodigo("Actas", "estadoActa", "1").codigo);
            model.NumeroFolio = (actaDecomisoDAL.GetCount() + 1).ToString() + "-" + DateTime.Now.Year;
            DateTime newDateTime = model.Fecha.Date + model.Hora.TimeOfDay;
            model.Fecha = newDateTime;

            try
            {
                if (ModelState.IsValid)
                {
                    actaDecomisoDAL.Add(ConvertirActaDecomiso(model));
                    int aux = actaDecomisoDAL.GetActaDecomisoFolio(model.NumeroFolio).idActaDecomiso;
                    auditoria_model.IdElemento = actaDecomisoDAL.GetActaDecomisoFolio(model.NumeroFolio).idActaDecomiso;
                    auditoriaDAL.Add(ConvertirAuditoria(auditoria_model));
                    return Redirect("~/ActaDecomiso/Detalle/" + aux);

                }
                model.EstadosCivil = tablaGeneralDAL.Get("Generales", "estadoCivil").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                model.Estados = tablaGeneralDAL.Get("Actas", "estadoActa").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult Detalle(int id)
        {
            //      Autorizar();
            actaDecomisoDAL = new ActaDecomisoDAL();
            Session["idActaDecomiso"] = id;
            Session["auditoria"] = actaDecomisoDAL.GetActaDecomiso(id).numeroFolio;
            Session["tabla"] = "Acta de Decomiso";
            ActaDecomisoViewModel modelo = CargarActaDecomiso(actaDecomisoDAL.GetActaDecomiso(id));
            return View(modelo);


        }


        //Devuelve la página de edición de policías con sus apartados llenos
        public ActionResult Editar(int id)
        {
            AutorizarEditar();
            tablaGeneralDAL = new TablaGeneralDAL();
            actaDecomisoDAL = new ActaDecomisoDAL();
            ActaDecomisoViewModel modelo = CargarActaDecomiso(actaDecomisoDAL.GetActaDecomiso(id));
            modelo.EstadosCivil = tablaGeneralDAL.Get("Generales", "estadoCivil").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            modelo.Estados = tablaGeneralDAL.Get("Actas", "estadoActa").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            return View(modelo);
        }




        [HttpPost]
        public ActionResult Editar(ActaDecomisoViewModel model)
        {
            AutorizarEditar();
            actaDecomisoDAL = new ActaDecomisoDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            AuditoriaViewModel auditoria_model = new AuditoriaViewModel
            {
                Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "2").idTablaGeneral,
                IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "9").idTablaGeneral,
                IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario
            };

            DateTime newDateTime = model.Fecha.Date + model.Hora.TimeOfDay;
            model.Fecha = newDateTime;
            int estado = actaDecomisoDAL.GetActaDecomiso(model.IdActaDecomiso).estado;
            try
            {
                if (ModelState.IsValid)
                {
                    if (tablaGeneralDAL.GetCodigo("Actas", "estadoActa", model.Estado.ToString()).idTablaGeneral != estado)
                    {
                        auditoriaDAL.Add(CambiarEstadoAuditoria(model.IdActaDecomiso));
                    }
                    actaDecomisoDAL.Edit(ConvertirActaDecomiso(model));
                    auditoria_model.IdElemento = actaDecomisoDAL.GetActaDecomisoFolio(model.NumeroFolio).idActaDecomiso;
                    auditoriaDAL.Add(ConvertirAuditoria(auditoria_model));
                    return Redirect("~/ActaDecomiso/Detalle/" + model.IdActaDecomiso);
                }
                model.EstadosCivil = tablaGeneralDAL.Get("Generales", "estadoCivil").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
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
        public Auditorias CambiarEstadoAuditoria(int idActaDecomiso)
        {
            AuditoriaViewModel modelo = new AuditoriaViewModel();
            tablaGeneralDAL = new TablaGeneralDAL();
            actaDecomisoDAL = new ActaDecomisoDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            return new Auditorias
            {
                idAuditoria = modelo.IdAuditoria,
                accion = modelo.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "3").idTablaGeneral,
                idCategoria = modelo.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "9").idTablaGeneral,
                idUsuario = modelo.IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario,
                fecha = DateTime.Now,
                idElemento = actaDecomisoDAL.GetActaDecomiso(idActaDecomiso).idActaDecomiso

            };
        }

        public void CreatePDF(int id)
        {
            //--------------------------Creacion de los DataSet--------------------------
            actaDecomisoDAL = new ActaDecomisoDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            personaDAL = new PersonaDAL();

            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = Path.Combine(Server.MapPath("~/PDFs"), "ReporteDecomiso.rdlc");

            //Decomisos
            ActasDecomiso decomiso = actaDecomisoDAL.GetActaDecomiso(id);
            List<ActasDecomiso> decomisos = new List<ActasDecomiso>();
            if (decomiso.observaciones == null)
            {
                decomiso.observaciones = "No aplica";
            }
            decomisos.Add(decomiso);

            //Policias
            Policias policiaActuante = policiaDAL.GetPolicia(decomiso.oficialActuante);
            List<Policias> policiasA = new List<Policias>();
            policiasA.Add(policiaActuante);

            Policias policiaAcompañante = policiaDAL.GetPolicia(decomiso.oficialAcompanante);
            List<Policias> policiasAc = new List<Policias>();
            policiasAc.Add(policiaAcompañante);

            Policias policiaSupervisor = policiaDAL.GetPolicia(decomiso.supervisorDecomiso);
            List<Policias> policiasS = new List<Policias>();
            policiasS.Add(policiaSupervisor);

            //Persona
            Personas decomisado = personaDAL.GetPersona(decomiso.idDecomisado);
            List<Personas> decomisados = new List<Personas>();
            if (decomisado.telefonoCelular == null)
            {
                decomisado.telefonoCelular = "No Aplica";
            }
            if (decomisado.direccionPersona == null)
            {
                decomisado.direccionPersona = "No Aplica";
            }
            decomisados.Add(decomisado);

            TablaGeneral estadoCivil = tablaGeneralDAL.Get(decomiso.estadoCivilDecomisado);
            List<TablaGeneral> estadosCiviles = new List<TablaGeneral>();
            estadosCiviles.Add(estadoCivil);

            //Agregado a Data Set
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DecomisoDataSet", decomisos));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("ActuanteDataSet", policiasA));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("AcompañanteDataSet", policiasAc));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("SupervisorDataSet", policiasS));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DecomisadoDataSet", decomisados));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("EstadoCivilDataSet", estadosCiviles));

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
            Response.AddHeader("content-disposition", "attachment; filename=" + "Acta de decomiso No. " + decomiso.numeroFolio + "." + extension);
            Response.BinaryWrite(bytes);
            Response.Flush();
        }
    }
}