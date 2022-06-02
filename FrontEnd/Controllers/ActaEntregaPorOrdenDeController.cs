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
    public class ActaEntregaPorOrdenDeController : Controller
    {
        ITablaGeneralDAL tablaGeneralDAL;
        IActaHallazgoDAL actaHallazgoDAL;
        IPoliciaDAL policiaDAL;
        IActaDecomisoDAL actaDecomisoDAL;
        IPersonaDAL personaDAL;
        IActaEntregaPorOrdenDeDAL actaEntregaPorOrdenDeDAL;
        IAuditoriaDAL auditoriaDAL;
        IUsuarioDAL usuarioDAL;

        //Metodos Útiles
        public ActasEntregaPorOrdenDe ConvertirActaEntregaPorOrdenDe(ActaEntregaPorOrdenDeViewModel modelo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            actaDecomisoDAL = new ActaDecomisoDAL();
            personaDAL = new PersonaDAL();
            actaHallazgoDAL = new ActaHallazgoDAL();
            ActasEntregaPorOrdenDe acta = new ActasEntregaPorOrdenDe
            {
                idActaEntregaPorOrdenDe = modelo.IdActaEntregaPorOrdenDe,
                numeroFolio = modelo.NumeroFolio,
                idFuncionarioQueEntrega = policiaDAL.GetPoliciaCedula(modelo.CedulaFuncionarioQueEntrega).idPolicia,
                tipoTestigo = tablaGeneralDAL.GetCodigo("Actas", "tipoTestigo", modelo.TipoTestigo.ToString()).idTablaGeneral,
                fechaHoraEntrega = modelo.Fecha,
                idPorOrdenDe = personaDAL.GetPersonaIdentificacion(modelo.IdentificacionPorOrdenDe).idPersona,
                numeroResolucion = modelo.NumeroResolucion,
                idPersonaQueSeLeEntrega = personaDAL.GetPersonaIdentificacion(modelo.IdentificacionPorOrdenDe).idPersona,
                estado = tablaGeneralDAL.GetCodigo("Actas", "estadoActa", modelo.Estado.ToString()).idTablaGeneral,
                tipoInventario = tablaGeneralDAL.GetCodigo("ActasEntregaPorOrdenDe", "tipoInventario", modelo.TipoInventario.ToString()).idTablaGeneral
            };

            if (modelo.TipoTestigo == 1)
            {
                acta.testigo = policiaDAL.GetPoliciaCedula(modelo.IdTestigoPolicia).idPolicia;
            }
            if (modelo.TipoTestigo == 2)
            {
                acta.testigo = personaDAL.GetPersonaIdentificacion(modelo.IdTestigoPersona).idPersona;
            }
            if (modelo.Estado != 0)
            {
                acta.estado = tablaGeneralDAL.GetCodigo("Actas", "estadoActa", modelo.Estado.ToString()).idTablaGeneral;
            }
            if (modelo.TipoInventario == 1)
            {
                acta.idActaLigada = actaDecomisoDAL.GetActaDecomisoFolio(modelo.NumeroActaLigada).idActaDecomiso;
            }
            if (modelo.TipoInventario == 2)
            {
                acta.numeroInventario = modelo.NumeroInventario;
            }
            return acta;
        }


        public ActaEntregaPorOrdenDeViewModel CargarActaEntregaPorOrdenDe(ActasEntregaPorOrdenDe actaEntregaPorOrdenDe)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            actaDecomisoDAL = new ActaDecomisoDAL();
            personaDAL = new PersonaDAL();
            actaHallazgoDAL = new ActaHallazgoDAL();

            ActaEntregaPorOrdenDeViewModel acta = new ActaEntregaPorOrdenDeViewModel
            {
                IdActaEntregaPorOrdenDe = actaEntregaPorOrdenDe.idActaEntregaPorOrdenDe,
                NumeroFolio = actaEntregaPorOrdenDe.numeroFolio,
                CedulaFuncionarioQueEntrega = policiaDAL.GetPolicia(actaEntregaPorOrdenDe.idFuncionarioQueEntrega).cedula,
                NombreFuncionarioQueEntrega = policiaDAL.GetPolicia(actaEntregaPorOrdenDe.idFuncionarioQueEntrega).nombre,
                VistaTipoInventario = tablaGeneralDAL.Get(actaEntregaPorOrdenDe.tipoInventario).descripcion,
                Fecha = actaEntregaPorOrdenDe.fechaHoraEntrega.Value,
                Hora = actaEntregaPorOrdenDe.fechaHoraEntrega.Value,
                TipoInventario = int.Parse(tablaGeneralDAL.Get(actaEntregaPorOrdenDe.tipoInventario).codigo),
                IdentificacionPorOrdenDe = personaDAL.GetPersona(actaEntregaPorOrdenDe.idPorOrdenDe).identificacion,
                NombrePorOrdenDe = personaDAL.GetPersona(actaEntregaPorOrdenDe.idPorOrdenDe).nombre,
                Estado = int.Parse(tablaGeneralDAL.Get(actaEntregaPorOrdenDe.estado).codigo),
                VistaEstadoActa = tablaGeneralDAL.Get(actaEntregaPorOrdenDe.estado).descripcion,
                TipoTestigo = int.Parse(tablaGeneralDAL.Get(actaEntregaPorOrdenDe.tipoTestigo).codigo),
                NumeroResolucion = actaEntregaPorOrdenDe.numeroResolucion,
                NumeroInventario = actaEntregaPorOrdenDe.numeroInventario,
                NombrePersonaQueSeLeEntrega = personaDAL.GetPersona(actaEntregaPorOrdenDe.idPorOrdenDe).nombre,
                CedulaPersonaQueSeLeEntrega = personaDAL.GetPersona(actaEntregaPorOrdenDe.idPorOrdenDe).identificacion,
                VistaTipoTestigo = tablaGeneralDAL.Get(actaEntregaPorOrdenDe.tipoTestigo).descripcion
            };

            if (actaEntregaPorOrdenDe.testigo != null)
            {
                if (int.Parse(tablaGeneralDAL.Get(actaEntregaPorOrdenDe.tipoTestigo).codigo) == 1)
                {
                    acta.VistaTestigo = policiaDAL.GetPolicia((int)actaEntregaPorOrdenDe.testigo).nombre;
                    acta.IdTestigoPolicia = policiaDAL.GetPolicia((int)actaEntregaPorOrdenDe.testigo).cedula;
                    acta.VistaIdTestigo = policiaDAL.GetPolicia((int)actaEntregaPorOrdenDe.testigo).cedula;
                }
                else if (int.Parse(tablaGeneralDAL.Get(actaEntregaPorOrdenDe.tipoTestigo).codigo) == 2)
                {
                    acta.VistaTestigo = personaDAL.GetPersona((int)actaEntregaPorOrdenDe.testigo).nombre;
                    acta.IdTestigoPersona = personaDAL.GetPersona((int)actaEntregaPorOrdenDe.testigo).identificacion;
                    acta.VistaIdTestigo = personaDAL.GetPersona((int)actaEntregaPorOrdenDe.testigo).identificacion;
                }

            }
            if (tablaGeneralDAL.Get(actaEntregaPorOrdenDe.tipoInventario).codigo == "1")
            {
                acta.IdActaLigada = (int)actaEntregaPorOrdenDe.idActaLigada;
                acta.NumeroActaLigada = actaDecomisoDAL.GetActaDecomiso(actaEntregaPorOrdenDe.idActaLigada.Value).numeroFolio;
                acta.DescripcionDeArticulos = actaDecomisoDAL.GetActaDecomiso(actaEntregaPorOrdenDe.idActaLigada.Value).inventario;
            }
            else
            {
                acta.NumeroInventario = actaEntregaPorOrdenDe.numeroInventario;
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

        public List<ActaDecomisoViewModel> ConvertirListaActasDecomisoFiltrados(List<ActasDecomiso> actasDecomiso)
        {
            return (from d in actasDecomiso
                    select new ActaDecomisoViewModel
                    {
                        NumeroFolio = d.numeroFolio,
                        Fecha = d.fecha,
                        Inventario = d.inventario,
                    }).ToList();
        }
        public PartialViewResult ListaActasDecomisoBuscar(string numero)
        {
            List<ActaDecomisoViewModel> actasDecomiso = new List<ActaDecomisoViewModel>();

            return PartialView("_ListaActasDecomisoBuscar", actasDecomiso);
        }

        public PartialViewResult ListaActasDecomisoParcial(string numero)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            actaDecomisoDAL = new ActaDecomisoDAL();
            List<ActasDecomiso> actasDecomiso = actaDecomisoDAL.Get();
            List<ActasDecomiso> actasDecomisoFiltrados = new List<ActasDecomiso>();

            if (numero == "")
            {
                actasDecomisoFiltrados = actasDecomiso;
            }
            else
            {
                foreach (ActasDecomiso actaDecomiso in actasDecomiso)
                {
                    if (actaDecomiso.numeroFolio.Contains(numero))
                    {
                        actasDecomisoFiltrados.Add(actaDecomiso);
                    }
                }
            }
            actasDecomisoFiltrados = actasDecomisoFiltrados.OrderBy(x => x.numeroFolio).ToList();

            return PartialView("_ListaActasDecomisoParcial", ConvertirListaActasDecomisoFiltrados(actasDecomisoFiltrados));
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

        //Metodos de las Vistas
        public ActionResult Index(string filtrosSeleccionado, string busqueda, string busquedaFechaInicioH, string busquedaFechaFinalH)
        {
            Autorizar();
            actaEntregaPorOrdenDeDAL = new ActaEntregaPorOrdenDeDAL();
            policiaDAL = new PoliciaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            personaDAL = new PersonaDAL();

            //Carga combobox busqueda
            List<TablaGeneral> comboindex = tablaGeneralDAL.Get("ActasEntregaPorOrdenDe", "index");
            List<SelectListItem> items = comboindex.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.descripcion
                };
            });
            ViewBag.items = items;

            //Carga lista de policias
            List<ActaEntregaPorOrdenDeViewModel> actasEntregaPorOrdenDe = new List<ActaEntregaPorOrdenDeViewModel>();
            List<ActaEntregaPorOrdenDeViewModel> actasEntregaPorOrdenDeFiltradas = new List<ActaEntregaPorOrdenDeViewModel>();
            foreach (ActasEntregaPorOrdenDe actaEntregaPorOrdenDe in actaEntregaPorOrdenDeDAL.Get())
            {
                actasEntregaPorOrdenDe.Add(CargarActaEntregaPorOrdenDe(actaEntregaPorOrdenDe));
            }
            if (busqueda != null)
            {
                foreach (ActaEntregaPorOrdenDeViewModel actaEntregaPorOrdenDe in actasEntregaPorOrdenDe)
                {
                    if (filtrosSeleccionado == "Número de Folio")
                    {
                        if (actaEntregaPorOrdenDe.NumeroFolio.Contains(busqueda))
                        {
                            actasEntregaPorOrdenDeFiltradas.Add(actaEntregaPorOrdenDe);
                        }
                    }
                    if (filtrosSeleccionado == "Nombre funcionario que entrega")
                    {
                        if (policiaDAL.GetPoliciaCedula(actaEntregaPorOrdenDe.CedulaFuncionarioQueEntrega).nombre.Contains(busqueda))
                        {
                            actasEntregaPorOrdenDeFiltradas.Add(actaEntregaPorOrdenDe);
                        }
                    }
                    if (filtrosSeleccionado == "Persona que se le entrega")
                    {
                        if (personaDAL.GetPersonaIdentificacion(actaEntregaPorOrdenDe.CedulaPersonaQueSeLeEntrega).nombre.Contains(busqueda))
                        {
                            actasEntregaPorOrdenDeFiltradas.Add(actaEntregaPorOrdenDe);
                        }
                    }
                }
                if (filtrosSeleccionado == "Fecha")
                {
                    DateTime fechaInicio = DateTime.Parse(busquedaFechaInicioH);
                    DateTime fechaFinal = DateTime.Parse(busquedaFechaFinalH);
                    if (fechaInicio < fechaFinal)
                    {
                        if (actaEntregaPorOrdenDeDAL.GetActaEntregaPorOrdenDeRango(fechaInicio, fechaFinal) != null)
                        {
                            foreach (ActasEntregaPorOrdenDe actaEntregaPorOrdenDeFecha in actaEntregaPorOrdenDeDAL.GetActaEntregaPorOrdenDeRango(fechaInicio, fechaFinal).ToList())
                            {
                                actasEntregaPorOrdenDeFiltradas.Add(CargarActaEntregaPorOrdenDe(actaEntregaPorOrdenDeFecha));
                            }
                        }
                    }
                }

                actasEntregaPorOrdenDe = actasEntregaPorOrdenDeFiltradas;
            }
            return View(actasEntregaPorOrdenDe.OrderBy(x => x.NumeroFolio).ToList());
        }
        public ActionResult Nuevo()
        {
            Autorizar();
            tablaGeneralDAL = new TablaGeneralDAL();
            ActaEntregaPorOrdenDeViewModel modelo = new ActaEntregaPorOrdenDeViewModel()
            {
                TiposDeInventario = tablaGeneralDAL.Get("ActasEntregaPorOrdenDe", "tipoInventario").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                Fecha = DateTime.Today,
                TiposTestigo = tablaGeneralDAL.Get("Actas", "tipoTestigo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo })

            };
            return View(modelo);
        }

        [HttpPost]
        public ActionResult Nuevo(ActaEntregaPorOrdenDeViewModel model)
        {
            Autorizar();
            actaEntregaPorOrdenDeDAL = new ActaEntregaPorOrdenDeDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            AuditoriaViewModel auditoria_model = new AuditoriaViewModel
            {
                Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "1").idTablaGeneral,
                IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "10").idTablaGeneral,
                IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario
            };

            model.NumeroFolio = (actaEntregaPorOrdenDeDAL.GetCount() + 1).ToString() + "-" + DateTime.Now.Year;
            DateTime newDateTime = model.Fecha.Date + model.Hora.TimeOfDay;
            model.Fecha = newDateTime;
            model.Estado = int.Parse(tablaGeneralDAL.GetCodigo("Actas", "estadoActa", "1").codigo);

            try
            {
                if (ModelState.IsValid)
                {
                    actaEntregaPorOrdenDeDAL.Add(ConvertirActaEntregaPorOrdenDe(model));
                    int aux = actaEntregaPorOrdenDeDAL.GetActaEntregaPorOrdenDeFolio(model.NumeroFolio).idActaEntregaPorOrdenDe;
                    auditoria_model.IdElemento = actaEntregaPorOrdenDeDAL.GetActaEntregaPorOrdenDeFolio(model.NumeroFolio).idActaEntregaPorOrdenDe;
                    auditoriaDAL.Add(ConvertirAuditoria(auditoria_model));
                    return Redirect("~/ActaEntregaPorOrdenDe/Detalle/" + aux);

                }
                model.TiposDeInventario = tablaGeneralDAL.Get("ActasEntregaPorOrdenDe", "tipoInventario").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
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
            actaEntregaPorOrdenDeDAL = new ActaEntregaPorOrdenDeDAL();
            Session["idActaEntregaPorOrdenDe"] = id;
            Session["auditoria"] = actaEntregaPorOrdenDeDAL.GetActaEntregaPorOrdenDe(id).numeroFolio;
            Session["tabla"] = "Acta de Entrega Por Orden De";
            ActaEntregaPorOrdenDeViewModel modelo = CargarActaEntregaPorOrdenDe(actaEntregaPorOrdenDeDAL.GetActaEntregaPorOrdenDe(id));
            return View(modelo);
        }

        //Devuelve la página de edición de policías con sus apartados llenos
        public ActionResult Editar(int id)
        {
            AutorizarEditar();
            tablaGeneralDAL = new TablaGeneralDAL();
            actaEntregaPorOrdenDeDAL = new ActaEntregaPorOrdenDeDAL();
            ActaEntregaPorOrdenDeViewModel modelo = CargarActaEntregaPorOrdenDe(actaEntregaPorOrdenDeDAL.GetActaEntregaPorOrdenDe(id));
            modelo.Estados = tablaGeneralDAL.Get("Actas", "estadoActa").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            modelo.TiposTestigo = tablaGeneralDAL.Get("Actas", "tipoTestigo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            modelo.TiposDeInventario = tablaGeneralDAL.Get("ActasEntregaPorOrdenDe", "tipoInventario").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            return View(modelo);
        }

        [HttpPost]
        public ActionResult Editar(ActaEntregaPorOrdenDeViewModel model)
        {
            AutorizarEditar();
            actaEntregaPorOrdenDeDAL = new ActaEntregaPorOrdenDeDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            AuditoriaViewModel auditoria_model = new AuditoriaViewModel
            {
                Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "2").idTablaGeneral,
                IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "10").idTablaGeneral,
                IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario
            };

            int estado = actaEntregaPorOrdenDeDAL.GetActaEntregaPorOrdenDe(model.IdActaEntregaPorOrdenDe).estado;
            DateTime newDateTime = model.Fecha.Date + model.Hora.TimeOfDay;
            model.Fecha = newDateTime;

            try
            {
                if (ModelState.IsValid)
                {
                    if (tablaGeneralDAL.GetCodigo("Actas", "estadoActa", model.Estado.ToString()).idTablaGeneral != estado)
                    {
                        auditoriaDAL.Add(CambiarEstadoAuditoria(model.IdActaEntregaPorOrdenDe));
                    }
                    actaEntregaPorOrdenDeDAL.Edit(ConvertirActaEntregaPorOrdenDe(model));
                    auditoria_model.IdElemento = actaEntregaPorOrdenDeDAL.GetActaEntregaPorOrdenDeFolio(model.NumeroFolio).idActaEntregaPorOrdenDe;
                    auditoriaDAL.Add(ConvertirAuditoria(auditoria_model));
                    return Redirect("~/ActaEntregaPorOrdenDe/Detalle/" + model.IdActaEntregaPorOrdenDe);
                }
                model.TiposDeInventario = tablaGeneralDAL.Get("ActasEntregaPorOrdenDe", "tipoInventario").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
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
            actaEntregaPorOrdenDeDAL = new ActaEntregaPorOrdenDeDAL();
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
        public Auditorias CambiarEstadoAuditoria(int idActaEntregaPorOrdenDe)
        {
            AuditoriaViewModel modelo = new AuditoriaViewModel();
            tablaGeneralDAL = new TablaGeneralDAL();
            actaEntregaPorOrdenDeDAL = new ActaEntregaPorOrdenDeDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            return new Auditorias
            {
                idAuditoria = modelo.IdAuditoria,
                accion = modelo.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "3").idTablaGeneral,
                idCategoria = modelo.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "10").idTablaGeneral,
                idUsuario = modelo.IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario,
                fecha = DateTime.Now,
                idElemento = actaEntregaPorOrdenDeDAL.GetActaEntregaPorOrdenDe(idActaEntregaPorOrdenDe).idActaEntregaPorOrdenDe

            };
        }

    }
}