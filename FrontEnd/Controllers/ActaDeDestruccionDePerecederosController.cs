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
    public class ActaDeDestruccionDePerecederosController : Controller
    {
        ITablaGeneralDAL tablaGeneralDAL;
        IActaDeDestruccionDePerecederosDAL actaDeDestruccionDePerecederosDAL;
        IPoliciaDAL policiaDAL;
        IActaHallazgoDAL actaHallazgoDAL;
        IPersonaDAL personaDAL;
        IActaDecomisoDAL actaDecomisoDAL;
        IAuditoriaDAL auditoriaDAL;
        IUsuarioDAL usuarioDAL;

        //metodos útiles
        public ActasDeDestruccionDePerecederos ConvertirActaDeDestruccionDePerecederos(ActaDeDestruccionDePerecederosViewModel model)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            personaDAL = new PersonaDAL();
            actaHallazgoDAL = new ActaHallazgoDAL();
            actaDecomisoDAL = new ActaDecomisoDAL();

            ActasDeDestruccionDePerecederos acta = new ActasDeDestruccionDePerecederos
            {
                idActaDeDestruccionDePerecederos = model.IdActaDeDestruccionDePerecederos,
                numeroFolio = model.NumeroFolio,
                encargado = policiaDAL.GetPoliciaCedula(model.Encargado).idPolicia,
                supervisor = policiaDAL.GetPoliciaCedula(model.Supervisor).idPolicia,
                fechaHora = model.Fecha,
                estado = tablaGeneralDAL.GetCodigo("Actas", "estadoActa", model.Estado.ToString()).idTablaGeneral,
                tipoActaD = tablaGeneralDAL.GetCodigo("Actas", "tipoActa", model.TipoActaD.ToString()).idTablaGeneral,
                tipoTestigo = tablaGeneralDAL.GetCodigo("Actas", "tipoTestigo", model.TipoTestigo.ToString()).idTablaGeneral,
                inventarioDestruido = model.InventarioDestruido
            };

            if (model.TipoTestigo == 1)
            {
                acta.testigo = policiaDAL.GetPoliciaCedula(model.IdTestigoPolicia).idPolicia;
            }
            if (model.TipoTestigo == 2)
            {
                acta.testigo = personaDAL.GetPersonaIdentificacion(model.IdTestigoPersona).idPersona;
            }
            if (model.TipoActaD == 1 && actaHallazgoDAL.FolioExiste(model.ConsecutivoActaHallazgo))
            {
                acta.idActaLigada = actaHallazgoDAL.GetActaHallazgoFolio(model.ConsecutivoActaHallazgo).idActaHallazgo;

            }
            else if (model.TipoActaD == 2 && actaDecomisoDAL.FolioExiste(model.ConsecutivoActaDecomiso))
            {
                acta.idActaLigada = actaDecomisoDAL.GetActaDecomisoFolio(model.ConsecutivoActaDecomiso).idActaDecomiso;
            }


            return acta;
        }

        public ActaDeDestruccionDePerecederosViewModel CargarActaDeDestruccionDePerecederos(ActasDeDestruccionDePerecederos actaDeDestruccionDePerecederos)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            actaHallazgoDAL = new ActaHallazgoDAL();
            actaDecomisoDAL = new ActaDecomisoDAL();
            personaDAL = new PersonaDAL();
            ActaDeDestruccionDePerecederosViewModel acta = new ActaDeDestruccionDePerecederosViewModel();
            {
                acta.IdActaDeDestruccionDePerecederos = actaDeDestruccionDePerecederos.idActaDeDestruccionDePerecederos;
                acta.NumeroFolio = actaDeDestruccionDePerecederos.numeroFolio;
                acta.Encargado = policiaDAL.GetPolicia(actaDeDestruccionDePerecederos.encargado).cedula;

                acta.Supervisor = policiaDAL.GetPolicia(actaDeDestruccionDePerecederos.supervisor).cedula;
                acta.Fecha = actaDeDestruccionDePerecederos.fechaHora;
                acta.Hora = actaDeDestruccionDePerecederos.fechaHora;
                acta.TipoActaD = int.Parse(tablaGeneralDAL.Get(actaDeDestruccionDePerecederos.tipoActaD).codigo);
                acta.VistaTipoActaD = tablaGeneralDAL.Get(actaDeDestruccionDePerecederos.tipoActaD).descripcion;
                acta.TipoTestigo = int.Parse(tablaGeneralDAL.Get(actaDeDestruccionDePerecederos.tipoTestigo).codigo);

                if (int.Parse(tablaGeneralDAL.Get(actaDeDestruccionDePerecederos.tipoTestigo).codigo) != 3 && actaDeDestruccionDePerecederos.testigo != null)
                {
                    if (int.Parse(tablaGeneralDAL.Get(actaDeDestruccionDePerecederos.tipoTestigo).codigo) == 1 && policiaDAL.GetPolicia((int)actaDeDestruccionDePerecederos.testigo) != null)
                    {
                        acta.VistaTestigo = policiaDAL.GetPolicia((int)actaDeDestruccionDePerecederos.testigo).nombre;
                        acta.IdTestigoPolicia = policiaDAL.GetPolicia((int)actaDeDestruccionDePerecederos.testigo).cedula;
                        acta.VistaIdTestigo = policiaDAL.GetPolicia((int)actaDeDestruccionDePerecederos.testigo).cedula;
                    }
                    else if (int.Parse(tablaGeneralDAL.Get(actaDeDestruccionDePerecederos.tipoTestigo).codigo) == 2 && personaDAL.GetPersona((int)actaDeDestruccionDePerecederos.testigo) != null)
                    {
                        acta.VistaTestigo = personaDAL.GetPersona((int)actaDeDestruccionDePerecederos.testigo).nombre;
                        acta.IdTestigoPersona = personaDAL.GetPersona((int)actaDeDestruccionDePerecederos.testigo).identificacion;
                        acta.VistaIdTestigo = personaDAL.GetPersona((int)actaDeDestruccionDePerecederos.testigo).identificacion;
                    }

                }
                else
                {
                    acta.VistaTestigo = "No alplica";
                    acta.IdTestigoPersona = "No alplica";
                    acta.VistaIdTestigo = "No alplica";
                }



                if (tablaGeneralDAL.Get(actaDeDestruccionDePerecederos.tipoActaD).descripcion == "Hallazgo")
                {
                    acta.IdActaHallazgo = (int)actaDeDestruccionDePerecederos.idActaLigada;
                    acta.NumeroActaLigada = actaHallazgoDAL.GetActaHallazgo((int)actaDeDestruccionDePerecederos.idActaLigada).numeroFolio;
                    acta.ConsecutivoActaHallazgo = actaHallazgoDAL.GetActaHallazgo((int)actaDeDestruccionDePerecederos.idActaLigada).numeroFolio;

                }
                else if (tablaGeneralDAL.Get(actaDeDestruccionDePerecederos.tipoActaD).descripcion == "Decomiso")
                {
                    acta.IdActaDecomiso = (int)actaDeDestruccionDePerecederos.idActaLigada;
                    acta.NumeroActaLigada = actaDecomisoDAL.GetActaDecomiso((int)actaDeDestruccionDePerecederos.idActaLigada).numeroFolio;
                    acta.ConsecutivoActaDecomiso = actaDecomisoDAL.GetActaDecomiso((int)actaDeDestruccionDePerecederos.idActaLigada).numeroFolio;
                }

                acta.InventarioDestruido = actaDeDestruccionDePerecederos.inventarioDestruido;
                acta.VistaPoliciaEncargado = policiaDAL.GetPolicia(actaDeDestruccionDePerecederos.encargado).nombre;
                acta.VistaTipoTestigo = tablaGeneralDAL.Get(actaDeDestruccionDePerecederos.tipoTestigo).descripcion;
                acta.VistaPoliciaSupervisor = policiaDAL.GetPolicia(actaDeDestruccionDePerecederos.supervisor).nombre;
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
                    if (policia.nombre.Contains(nombre.ToUpper()))
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
        public ActionResult Index(string filtroSeleccionado, string busqueda, string busquedaFechaInicioH, string busquedaFechaFinalH)
        {
            Autorizar();
            actaDeDestruccionDePerecederosDAL = new ActaDeDestruccionDePerecederosDAL();
            policiaDAL = new PoliciaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            List<ActaDeDestruccionDePerecederosViewModel> actasDeDestruccionDePerecederos = new List<ActaDeDestruccionDePerecederosViewModel>();
            List<ActaDeDestruccionDePerecederosViewModel> actasDeDestruccionDePerecederosFiltradas = new List<ActaDeDestruccionDePerecederosViewModel>();
            List<TablaGeneral> comboindex = tablaGeneralDAL.Get("ActasDeDestruccionDePerecederos", "index");
            List<SelectListItem> items = comboindex.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.descripcion
                };
            });
            ViewBag.items = items;
            foreach (ActasDeDestruccionDePerecederos actaDeDestruccionDePerecederos in actaDeDestruccionDePerecederosDAL.Get())
            {
                actasDeDestruccionDePerecederos.Add(CargarActaDeDestruccionDePerecederos(actaDeDestruccionDePerecederos));
            }
            if (busqueda != null)
            {
                foreach (ActaDeDestruccionDePerecederosViewModel actaDeDestruccionDePerecederos in actasDeDestruccionDePerecederos)
                {
                    if (filtroSeleccionado == "Número de Folio")
                    {
                        if (actaDeDestruccionDePerecederos.NumeroFolio.Contains(busqueda))
                        {
                            actasDeDestruccionDePerecederosFiltradas.Add(actaDeDestruccionDePerecederos);
                        }
                    }
                    if (filtroSeleccionado == "Nombre Policía Encargado")
                    {
                        if (policiaDAL.GetPoliciaCedula(actaDeDestruccionDePerecederos.Encargado).nombre.Contains(busqueda.ToUpper()))
                        {
                            actasDeDestruccionDePerecederosFiltradas.Add(actaDeDestruccionDePerecederos);
                        }
                    }
                }
                if (filtroSeleccionado == "Fecha")
                {
                    DateTime fechaInicio = DateTime.Parse(busquedaFechaInicioH);
                    DateTime fechaFinal = DateTime.Parse(busquedaFechaFinalH);
                    if (fechaInicio < fechaFinal)
                    {
                        if (actaDeDestruccionDePerecederosDAL.GetActaDeDestruccionDePerecederosRango(fechaInicio, fechaFinal) != null)
                        {
                            foreach (ActasDeDestruccionDePerecederos actaDeDestruccionDePerecederosFecha in actaDeDestruccionDePerecederosDAL.GetActaDeDestruccionDePerecederosRango(fechaInicio, fechaFinal).ToList())
                            {
                                actasDeDestruccionDePerecederosFiltradas.Add(CargarActaDeDestruccionDePerecederos(actaDeDestruccionDePerecederosFecha));
                            }
                        }
                    }
                }

                actasDeDestruccionDePerecederos = actasDeDestruccionDePerecederosFiltradas;
            }
            return View(actasDeDestruccionDePerecederos.OrderBy(x => x.NumeroFolio).ToList());


        }

        public ActionResult Nuevo()
        {
            Autorizar();
            tablaGeneralDAL = new TablaGeneralDAL();
            ActaDeDestruccionDePerecederosViewModel modelo = new ActaDeDestruccionDePerecederosViewModel()
            {
                TiposActaD = tablaGeneralDAL.Get("Actas", "tipoActa").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                TiposTestigo = tablaGeneralDAL.Get("Actas", "tipoTestigo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                Fecha = DateTime.Today

            };
            return View(modelo);

        }

        [HttpPost]
        public ActionResult Nuevo(ActaDeDestruccionDePerecederosViewModel model)
        {
            Autorizar();
            actaDeDestruccionDePerecederosDAL = new ActaDeDestruccionDePerecederosDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            AuditoriaViewModel auditoria_model = new AuditoriaViewModel();
            model.TiposActaD = tablaGeneralDAL.Get("Actas", "tipoActa").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.TiposTestigo = tablaGeneralDAL.Get("Actas", "tipoTestigo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.NumeroFolio = (actaDeDestruccionDePerecederosDAL.GetCount() + 1).ToString() + "-" + DateTime.Now.Year;
            DateTime newDateTime = model.Fecha.Date + model.Hora.TimeOfDay;
            model.Fecha = newDateTime;
            model.Estado = int.Parse(tablaGeneralDAL.GetCodigo("Actas", "estadoActa", "1").codigo);
            auditoria_model.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "1").idTablaGeneral;
            auditoria_model.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "11").idTablaGeneral;
            auditoria_model.IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario;
            try
            {
                if (ModelState.IsValid)
                {
                    actaDeDestruccionDePerecederosDAL.Add(ConvertirActaDeDestruccionDePerecederos(model));
                    auditoria_model.IdElemento = actaDeDestruccionDePerecederosDAL.GetActaDeDestruccionDePerecederosFolio(model.NumeroFolio).idActaDeDestruccionDePerecederos;
                    auditoriaDAL.Add(ConvertirAuditoria(auditoria_model));
                    int aux = actaDeDestruccionDePerecederosDAL.GetActaDeDestruccionDePerecederosFolio(model.NumeroFolio).idActaDeDestruccionDePerecederos;
                    return Redirect("~/ActaDeDestruccionDePerecederos/Detalle/" + aux);

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
            actaDeDestruccionDePerecederosDAL = new ActaDeDestruccionDePerecederosDAL();
            Session["idActaDeDestruccionDePerecederos"] = id;
            Session["auditoria"] = actaDeDestruccionDePerecederosDAL.GetActaDeDestruccionDePerecederos(id).numeroFolio;
            Session["tabla"] = "Acta de Destrucción de Perecederos";
            ActaDeDestruccionDePerecederosViewModel modelo = CargarActaDeDestruccionDePerecederos(actaDeDestruccionDePerecederosDAL.GetActaDeDestruccionDePerecederos(id));
            return View(modelo);


        }

        public ActionResult Editar(int id)
        {
            AutorizarEditar();
            tablaGeneralDAL = new TablaGeneralDAL();
            actaDeDestruccionDePerecederosDAL = new ActaDeDestruccionDePerecederosDAL();
            ActaDeDestruccionDePerecederosViewModel model = CargarActaDeDestruccionDePerecederos(actaDeDestruccionDePerecederosDAL.GetActaDeDestruccionDePerecederos(id));
            model.TiposActaD = tablaGeneralDAL.Get("Actas", "tipoActa").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.Estados = tablaGeneralDAL.Get("Actas", "estadoActa").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.TiposTestigo = tablaGeneralDAL.Get("Actas", "tipoTestigo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(ActaDeDestruccionDePerecederosViewModel model)
        {
            AutorizarEditar();
            actaDeDestruccionDePerecederosDAL = new ActaDeDestruccionDePerecederosDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            AuditoriaViewModel auditoria_model = new AuditoriaViewModel();
            model.TiposActaD = tablaGeneralDAL.Get("Actas", "tipoActa").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.TiposTestigo = tablaGeneralDAL.Get("Actas", "tipoTestigo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.Estados = tablaGeneralDAL.Get("Actas", "estadoActa").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            DateTime newDateTime = model.Fecha.Date + model.Hora.TimeOfDay;
            model.Fecha = newDateTime;
            auditoria_model.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "2").idTablaGeneral;
            auditoria_model.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "11").idTablaGeneral;
            auditoria_model.IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario;
            int estado = actaDeDestruccionDePerecederosDAL.GetActaDeDestruccionDePerecederos(model.IdActaDeDestruccionDePerecederos).estado;
            try
            {
                if (ModelState.IsValid)
                {
                    if (tablaGeneralDAL.GetCodigo("Actas", "estadoActa", model.Estado.ToString()).idTablaGeneral != estado && model.IdActaDeDestruccionDePerecederos != 0)
                    {
                        auditoriaDAL.Add(CambiarEstadoAuditoria(model.IdActaDeDestruccionDePerecederos));
                    }
                    actaDeDestruccionDePerecederosDAL.Edit(ConvertirActaDeDestruccionDePerecederos(model));
                    auditoria_model.IdElemento = actaDeDestruccionDePerecederosDAL.GetActaDeDestruccionDePerecederosFolio(model.NumeroFolio).idActaDeDestruccionDePerecederos;
                    auditoriaDAL.Add(ConvertirAuditoria(auditoria_model));
                    return Redirect("~/ActaDeDestruccionDePerecederos/Detalle/" + model.IdActaDeDestruccionDePerecederos);
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
        public Auditorias CambiarEstadoAuditoria(int idActaDeDestruccionDePerecederos)
        {
            AuditoriaViewModel modelo = new AuditoriaViewModel();
            tablaGeneralDAL = new TablaGeneralDAL();
            actaDeDestruccionDePerecederosDAL = new ActaDeDestruccionDePerecederosDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            return new Auditorias
            {
                idAuditoria = modelo.IdAuditoria,
                accion = modelo.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "3").idTablaGeneral,
                idCategoria = modelo.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "11").idTablaGeneral,
                idUsuario = modelo.IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario,
                fecha = DateTime.Now,
                idElemento = actaDeDestruccionDePerecederosDAL.GetActaDeDestruccionDePerecederos(idActaDeDestruccionDePerecederos).idActaDeDestruccionDePerecederos

            };
        }
        public void CreatePDF(int id)
        {
            //--------------------------Creacion de los DataSet--------------------------
            actaDeDestruccionDePerecederosDAL = new ActaDeDestruccionDePerecederosDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            personaDAL = new PersonaDAL();
            actaHallazgoDAL = new ActaHallazgoDAL();
            actaDecomisoDAL = new ActaDecomisoDAL();

            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = Path.Combine(Server.MapPath("~/PDFs"), "ReporteDestruccionDePerecederos.rdlc");

            //Destruccion
            ActasDeDestruccionDePerecederos perecedero = actaDeDestruccionDePerecederosDAL.GetActaDeDestruccionDePerecederos(id);
            List<ActasDeDestruccionDePerecederos> perecederos = new List<ActasDeDestruccionDePerecederos>();
            perecederos.Add(perecedero);

            //Policias
            Policias policiaSupervisor = policiaDAL.GetPolicia(perecedero.supervisor);
            List<Policias> policiasS = new List<Policias>();
            policiasS.Add(policiaSupervisor);

            Policias policiaEncargado = policiaDAL.GetPolicia(perecedero.encargado);
            List<Policias> policiasE = new List<Policias>();
            policiasE.Add(policiaEncargado);

            //TablaGeneral
            TablaGeneral tipoDeActa = new TablaGeneral();
            List<TablaGeneral> tiposDeActas = new List<TablaGeneral>();
            tipoDeActa.descripcion = tablaGeneralDAL.Get(perecedero.tipoActaD).descripcion;
            tiposDeActas.Add(tipoDeActa);

            TablaGeneral tipoDeTestigo = new TablaGeneral();
            List<TablaGeneral> tiposDeTestigos = new List<TablaGeneral>();
            tipoDeTestigo.descripcion = tablaGeneralDAL.Get(perecedero.tipoTestigo).descripcion;
            tiposDeTestigos.Add(tipoDeTestigo);

            //Agregado a Data Set
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DestruccionDataSet", perecederos));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("PEncargadoDataSet", policiasE));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("PSupervisorDataSet", policiasS));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("TGTipoDataSet", tiposDeActas));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("TGTestigoDataSet", tiposDeTestigos));

            //Actas de Hallazgo y Decomiso
            if (tipoDeActa.descripcion == "Hallazgo")
            {
                ActasHallazgo actaHallazgo = actaHallazgoDAL.GetActaHallazgo(perecedero.idActaLigada);
                List<ActasHallazgo> hallazgos = new List<ActasHallazgo>();
                hallazgos.Add(actaHallazgo);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("HallazgoDataSet", hallazgos));

                ActasDecomiso actaDecomiso = new ActasDecomiso();
                List<ActasDecomiso> decomisos = new List<ActasDecomiso>();
                decomisos.Add(actaDecomiso);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("DecomisoDataSet", decomisos));
            }
            else
            {
                ActasDecomiso actaDecomiso = actaDecomisoDAL.GetActaDecomiso(perecedero.idActaLigada);
                List<ActasDecomiso> decomisos = new List<ActasDecomiso>();
                decomisos.Add(actaDecomiso);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("DecomisoDataSet", decomisos));

                ActasHallazgo actaHallazgo = new ActasHallazgo();
                List<ActasHallazgo> hallazgos = new List<ActasHallazgo>();
                hallazgos.Add(actaHallazgo);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("HallazgoDataSet", hallazgos));
            }

            //Tipos de Testigo
            if (tipoDeTestigo.descripcion == "Policía")
            {
                Policias policiaTestigo = policiaDAL.GetPolicia((int)perecedero.testigo);
                List<Policias> policiasT = new List<Policias>();
                policiasT.Add(policiaTestigo);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("PTestigoDataSet", policiasT));

                Personas persona = new Personas();
                List<Personas> personas = new List<Personas>();
                personas.Add(persona);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("PerTestigoDataSet", personas));

                TablaGeneral noAplica = new TablaGeneral();
                List<TablaGeneral> noAplican = new List<TablaGeneral>();
                noAplican.Add(noAplica);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("aplicaDataSet", noAplican));
            }
            else
            {
                if (tipoDeTestigo.descripcion == "Persona")
                {
                    Personas personaTestigo = personaDAL.GetPersona((int)perecedero.testigo);
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
                    noAplica.descripcion = "No Aplica";
                    noAplican.Add(noAplica);

                    viewer.LocalReport.DataSources.Add(new ReportDataSource("aplicaDataSet", noAplican));

                    Policias policiaTestigo = new Policias();
                    List<Policias> policiasT = new List<Policias>();
                    policiasT.Add(policiaTestigo);

                    viewer.LocalReport.DataSources.Add(new ReportDataSource("PTestigoDataSet", policiasT));

                    Personas persona = new Personas();
                    List<Personas> personas = new List<Personas>();
                    personas.Add(persona);

                    viewer.LocalReport.DataSources.Add(new ReportDataSource("PerTestigoDataSet", personas));
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
            Response.AddHeader("content-disposition", "attachment; filename=" + "Acta de destrucción de perecederos No. " + perecedero.numeroFolio + "." + extension);
            Response.BinaryWrite(bytes);
            Response.Flush();
        }
    }
}