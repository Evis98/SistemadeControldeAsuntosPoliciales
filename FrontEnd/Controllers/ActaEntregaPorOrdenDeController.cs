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
        public ActasEntregaPorOrdenDe ConvertirActaEntregaPorOrdenDe(ActaEntregaPorOrdenDeViewModel modelo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            actaDecomisoDAL = new ActaDecomisoDAL();
            personaDAL = new PersonaDAL();
            actaHallazgoDAL = new ActaHallazgoDAL();
            ActasEntregaPorOrdenDe acta = new ActasEntregaPorOrdenDe();

            acta.idActaEntregaPorOrdenDe = modelo.IdActaEntregaPorOrdenDe;
            acta.numeroFolio = modelo.NumeroFolio;
            acta.idFuncionarioQueEntrega = policiaDAL.GetPoliciaCedula(modelo.CedulaFuncionarioQueEntrega).idPolicia;
            acta.idTestigoEntrega = policiaDAL.GetPoliciaCedula(modelo.CedulaTestigoDeLaEntrega).idPolicia;
         
            if (actaDecomisoDAL.FolioExiste(modelo.NumeroActaLigada))
            {
                string cod = "1";
                acta.tipoInventario = tablaGeneralDAL.GetCodigo("ActasEntregaPorOrdenDe", "tipoInventario", cod).idTablaGeneral;
                acta.idActaLigada = actaDecomisoDAL.GetActaDecomisoFolio(modelo.NumeroActaLigada).idActaDecomiso;
            }
            else
            {
                string cod = "2";
                acta.tipoInventario = tablaGeneralDAL.GetCodigo("ActasEntregaPorOrdenDe", "tipoInventario", cod).idTablaGeneral;
                acta.numeroInventario = modelo.NumeroInventario;
            }
            acta.fechaHoraEntrega = modelo.Fecha;
            acta.idPorOrdenDe = personaDAL.GetPersonaIdentificacion(modelo.IdentificacionPorOrdenDe).idPersona;
            acta.numeroResolucion = modelo.NumeroResolucion;
            acta.idPersonaQueSeLeEntrega = personaDAL.GetPersonaIdentificacion(modelo.IdentificacionPorOrdenDe).idPersona;
            //acta.nombrePersonaQueSeLeEntrega = modelo.NombrePersonaQueSeLeEntrega;
            acta.estado = tablaGeneralDAL.GetCodigo("Actas", "estadoActa", modelo.Estado.ToString()).idTablaGeneral;

            return acta;
        }


        public ActaEntregaPorOrdenDeViewModel CargarActaEntregaPorOrdenDe(ActasEntregaPorOrdenDe actaEntregaPorOrdenDe)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            actaDecomisoDAL = new ActaDecomisoDAL();
            personaDAL = new PersonaDAL();
            actaHallazgoDAL = new ActaHallazgoDAL();

            ActaEntregaPorOrdenDeViewModel acta = new ActaEntregaPorOrdenDeViewModel();

            acta.IdActaEntregaPorOrdenDe = actaEntregaPorOrdenDe.idActaEntregaPorOrdenDe;
            acta.NumeroFolio = actaEntregaPorOrdenDe.numeroFolio;
            acta.CedulaTestigoDeLaEntrega = policiaDAL.GetPolicia(actaEntregaPorOrdenDe.idTestigoEntrega).cedula;
            acta.NombreTestigoDeLaEntrega = policiaDAL.GetPolicia(actaEntregaPorOrdenDe.idTestigoEntrega).nombre;
            acta.CedulaFuncionarioQueEntrega = policiaDAL.GetPolicia(actaEntregaPorOrdenDe.idFuncionarioQueEntrega).cedula;
            acta.NombreFuncionarioQueEntrega = policiaDAL.GetPolicia(actaEntregaPorOrdenDe.idFuncionarioQueEntrega).nombre;

            acta.VistaTipoInventario = tablaGeneralDAL.Get(actaEntregaPorOrdenDe.tipoInventario).descripcion;
            acta.Fecha = actaEntregaPorOrdenDe.fechaHoraEntrega.Value;
            acta.Hora = actaEntregaPorOrdenDe.fechaHoraEntrega.Value;
            acta.TipoInventario = int.Parse(tablaGeneralDAL.Get(actaEntregaPorOrdenDe.tipoInventario).codigo);
            acta.VistaTipoInventario = tablaGeneralDAL.Get(actaEntregaPorOrdenDe.tipoInventario).descripcion;
            acta.IdentificacionPorOrdenDe = personaDAL.GetPersona(actaEntregaPorOrdenDe.idPorOrdenDe).identificacion;
            acta.NombrePorOrdenDe = personaDAL.GetPersona(actaEntregaPorOrdenDe.idPorOrdenDe).nombre;
            acta.Estado = int.Parse(tablaGeneralDAL.Get(actaEntregaPorOrdenDe.estado).codigo);
            acta.VistaEstadoActa = tablaGeneralDAL.Get(actaEntregaPorOrdenDe.estado).descripcion;
            if (tablaGeneralDAL.Get(actaEntregaPorOrdenDe.tipoInventario).codigo == "1") {
                acta.NumeroActaLigada = actaDecomisoDAL.GetActaDecomiso(actaEntregaPorOrdenDe.idActaLigada.Value).numeroFolio;
                acta.DescripcionDeArticulos = actaDecomisoDAL.GetActaDecomiso(actaEntregaPorOrdenDe.idActaLigada.Value).inventario;
            }
            else
            {
                acta.NumeroInventario = actaEntregaPorOrdenDe.numeroInventario;
            }

            acta.NumeroResolucion = actaEntregaPorOrdenDe.numeroResolucion;
            acta.NumeroInventario = actaEntregaPorOrdenDe.numeroInventario;
            acta.NombrePersonaQueSeLeEntrega = personaDAL.GetPersona(actaEntregaPorOrdenDe.idPorOrdenDe).nombre;
            acta.CedulaPersonaQueSeLeEntrega = personaDAL.GetPersona(actaEntregaPorOrdenDe.idPorOrdenDe).identificacion;


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
            actaEntregaPorOrdenDeDAL = new ActaEntregaPorOrdenDeDAL();
            policiaDAL = new PoliciaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            List<ActaEntregaPorOrdenDeViewModel> actasEntregaPorOrdenDe = new List<ActaEntregaPorOrdenDeViewModel>();
            List<ActaEntregaPorOrdenDeViewModel> actasEntregaPorOrdenDeFiltradas = new List<ActaEntregaPorOrdenDeViewModel>();
            List<TablaGeneral> comboindex = tablaGeneralDAL.Get("ActasEntregaPorOrdenDe", "index");
            List<SelectListItem> items = comboindex.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.descripcion
                };
            });
            ViewBag.items = items;
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
            tablaGeneralDAL = new TablaGeneralDAL();
            ActaEntregaPorOrdenDeViewModel modelo = new ActaEntregaPorOrdenDeViewModel()
            {
                TiposDeInventario = tablaGeneralDAL.Get("ActasEntregaPorOrdenDe", "tipoInventario").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                Fecha = DateTime.Today

            };
            return View(modelo);
        }

        [HttpPost]
        public ActionResult Nuevo(ActaEntregaPorOrdenDeViewModel model)
        {
            actaEntregaPorOrdenDeDAL = new ActaEntregaPorOrdenDeDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            model.TiposDeInventario = tablaGeneralDAL.Get("ActasEntregaPorOrdenDe", "tipoInventario").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.NumeroFolio = (actaEntregaPorOrdenDeDAL.GetCount() + 1).ToString() + "-" + DateTime.Now.Year;
            DateTime newDateTime = model.Fecha.Date + model.Hora.TimeOfDay;
            model.Fecha = newDateTime;           
            model.Estado = int.Parse(tablaGeneralDAL.GetCodigo("Actas", "estadoActa", "1").codigo);
            model.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "1").idTablaGeneral;
            model.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "10").idTablaGeneral;
            model.IdUsuario = usuarioDAL.GetUsuario(1).idUsuario;
            try
            {
                if (ModelState.IsValid)
                {
                    actaEntregaPorOrdenDeDAL.Add(ConvertirActaEntregaPorOrdenDe(model));
                    int aux = actaEntregaPorOrdenDeDAL.GetActaEntregaPorOrdenDeFolio(model.NumeroFolio).idActaEntregaPorOrdenDe;
                    model.IdElemento = actaEntregaPorOrdenDeDAL.GetActaEntregaPorOrdenDeFolio(model.NumeroFolio).idActaEntregaPorOrdenDe;
                    auditoriaDAL.Add(ConvertirAuditoria(model));
                    return Redirect("~/ActaEntregaPorOrdenDe/Detalle/" + aux);

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
            actaEntregaPorOrdenDeDAL = new ActaEntregaPorOrdenDeDAL();
            Session["idActaEntregaPorOrdenDe"] = id;
            Session["numeroFolio"] = actaEntregaPorOrdenDeDAL.GetActaEntregaPorOrdenDe(id).numeroFolio;
            ActaEntregaPorOrdenDeViewModel modelo = CargarActaEntregaPorOrdenDe(actaEntregaPorOrdenDeDAL.GetActaEntregaPorOrdenDe(id));
            return View(modelo);
        }

        //Devuelve la página de edición de policías con sus apartados llenos
        public ActionResult Editar(int id)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            actaEntregaPorOrdenDeDAL = new ActaEntregaPorOrdenDeDAL();
            ActaEntregaPorOrdenDeViewModel modelo = CargarActaEntregaPorOrdenDe(actaEntregaPorOrdenDeDAL.GetActaEntregaPorOrdenDe(id));
            modelo.Estados = tablaGeneralDAL.Get("Actas", "estadoActa").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            modelo.TiposDeInventario = tablaGeneralDAL.Get("ActasEntregaPorOrdenDe", "tipoInventario").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            return View(modelo);
        }

        [HttpPost]
        public ActionResult Editar(ActaEntregaPorOrdenDeViewModel model)
        {
            actaEntregaPorOrdenDeDAL = new ActaEntregaPorOrdenDeDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            model.Estados = tablaGeneralDAL.Get("Actas", "estadoActa").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "2").idTablaGeneral;
            model.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "10").idTablaGeneral;
            model.IdUsuario = usuarioDAL.GetUsuario(1).idUsuario;
            int estado = actaEntregaPorOrdenDeDAL.GetActaEntregaPorOrdenDe(model.IdActaEntregaPorOrdenDe).estado;
            model.TiposDeInventario = tablaGeneralDAL.Get("ActasEntregaPorOrdenDe", "tipoInventario").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            DateTime newDateTime = model.Fecha.Date + model.Hora.TimeOfDay;
            model.Fecha = newDateTime;
            try
            {
                if (ModelState.IsValid)
                {
                    if (tablaGeneralDAL.GetCodigo("Actas", "estadoActa", model.Estado.ToString()).idTablaGeneral != estado && model.IdActaEntregaPorOrdenDe != 0)
                    {
                        auditoriaDAL.Add(CambiarEstadoAuditoria(model.IdActaEntregaPorOrdenDe));
                    }
                    actaEntregaPorOrdenDeDAL.Edit(ConvertirActaEntregaPorOrdenDe(model));
                    model.IdElemento = actaEntregaPorOrdenDeDAL.GetActaEntregaPorOrdenDeFolio(model.NumeroFolio).idActaEntregaPorOrdenDe;
                    auditoriaDAL.Add(ConvertirAuditoria(model));
                    return Redirect("~/ActaEntregaPorOrdenDe/Detalle/" + model.IdActaEntregaPorOrdenDe);
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Auditorias ConvertirAuditoria(ActaEntregaPorOrdenDeViewModel modelo)
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
            ActaEntregaPorOrdenDeViewModel modelo = new ActaEntregaPorOrdenDeViewModel();
            tablaGeneralDAL = new TablaGeneralDAL();
            actaEntregaPorOrdenDeDAL = new ActaEntregaPorOrdenDeDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            return new Auditorias
            {
                idAuditoria = modelo.IdAuditoria,
                accion = modelo.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "3").idTablaGeneral,
                idCategoria = modelo.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "10").idTablaGeneral,
                idUsuario = modelo.IdUsuario = usuarioDAL.GetUsuario(1).idUsuario,
                fecha = DateTime.Now,
                idElemento = actaEntregaPorOrdenDeDAL.GetActaEntregaPorOrdenDe(idActaEntregaPorOrdenDe).idActaEntregaPorOrdenDe

            };
        }

    }
}