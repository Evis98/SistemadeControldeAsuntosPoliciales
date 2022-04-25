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
        IActaHallazgoDAL actaHallazgoDAL;
        IActaDecomisoDAL actaDecomisoDAL;

        public ActasEntrega ConvertirActaEntrega(ActaEntregaViewModel model)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();

            ActasEntrega acta = new ActasEntrega();
            {
                acta.idActaEntrega = model.IdActaEntrega;
                acta.numeroFolio = model.NumeroFolio;
                acta.encargado = policiaDAL.GetPoliciaCedula(model.Encargado).idPolicia;
                acta.testigo = policiaDAL.GetPoliciaCedula(model.Testigo).idPolicia;
                acta.supervisor = policiaDAL.GetPoliciaCedula(model.Supervisor).idPolicia;
                acta.instalaciones = tablaGeneralDAL.GetCodigo("ActaEntrega", "instalaciones", model.Instalaciones.ToString()).idTablaGeneral;
                acta.fechaHora = model.Fecha;
                acta.razonSocial = model.RazonSocial;
                acta.cedulaJuridica = model.CedulaJuridica;
                acta.tipoActa = tablaGeneralDAL.GetCodigo("Actas", "tipoActa", model.TipoActa.ToString()).idTablaGeneral;
                if (tablaGeneralDAL.Get(acta.tipoActa).descripcion == "Hallazgo")
                {
                    acta.consecutivoActa = model.ConsecutivoActaHallazgo;
                }
                else
                {
                    acta.consecutivoActa = model.ConsecutivoActaDecomiso;
                }

                acta.nombreDe = model.NombreDe;
                acta.identificacionEntregado = model.IdentificacionEntregado;
                acta.recibe = model.Recibe;
                acta.cedulaRecibe = model.CedulaJuridica;
                acta.inventarioEntregado = model.InventarioEntregado;

            };

            return acta;
        }
        public ActaEntregaViewModel CargarActaEntrega(ActasEntrega actaEntrega)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();

            ActaEntregaViewModel acta = new ActaEntregaViewModel();
            {
                acta.IdActaEntrega = actaEntrega.idActaEntrega;
                acta.NumeroFolio = actaEntrega.numeroFolio;
                acta.Encargado = policiaDAL.GetPolicia(actaEntrega.encargado).cedula;
                acta.Testigo = policiaDAL.GetPolicia(actaEntrega.testigo).cedula;
                acta.Supervisor = policiaDAL.GetPolicia(actaEntrega.supervisor).cedula;
                acta.Instalaciones = int.Parse(tablaGeneralDAL.Get(actaEntrega.instalaciones).codigo);
                acta.VistaInstalaciones = tablaGeneralDAL.Get(actaEntrega.instalaciones).descripcion;
                acta.Fecha = actaEntrega.fechaHora;
                acta.Hora = actaEntrega.fechaHora;
                acta.RazonSocial = actaEntrega.razonSocial;
                acta.CedulaJuridica = actaEntrega.cedulaJuridica;
                acta.TipoActa = int.Parse(tablaGeneralDAL.Get(actaEntrega.tipoActa).codigo);
                acta.VistaTipoActa = tablaGeneralDAL.Get(actaEntrega.tipoActa).descripcion;
                if (tablaGeneralDAL.Get(actaEntrega.tipoActa).descripcion == "Hallazgo")
                {
                    acta.ConsecutivoActaHallazgo = actaEntrega.consecutivoActa;
                }
                else
                {
                    acta.ConsecutivoActaDecomiso = actaEntrega.consecutivoActa;
                }
                acta.NombreDe = actaEntrega.nombreDe;
                acta.IdentificacionEntregado = actaEntrega.identificacionEntregado;
                acta.Recibe = actaEntrega.recibe;
                acta.CedulaRecibe = actaEntrega.cedulaRecibe;
                acta.InventarioEntregado = actaEntrega.inventarioEntregado;
                acta.VistaPoliciaEncargado = policiaDAL.GetPolicia(actaEntrega.encargado).nombre;
                acta.VistaPoliciaTestigo = policiaDAL.GetPolicia(actaEntrega.testigo).nombre;
                acta.VistaPoliciaSupervisor = policiaDAL.GetPolicia(actaEntrega.supervisor).nombre;
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

        public ActionResult Nuevo()
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            ActaEntregaViewModel modelo = new ActaEntregaViewModel()
            {
                TiposInstalaciones = tablaGeneralDAL.Get("ActaEntrega", "instalaciones").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                TiposActa = tablaGeneralDAL.Get("Actas", "tipoActa").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                Fecha = DateTime.Today

            };
            return View(modelo);
        }

        [HttpPost]
        public ActionResult Nuevo(ActaEntregaViewModel model)
        {
            actaEntregaDAL = new ActaEntregaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            model.TiposInstalaciones = tablaGeneralDAL.Get("ActaEntrega", "instalaciones").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.TiposActa = tablaGeneralDAL.Get("Actas", "tipoActa").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.NumeroFolio = (actaEntregaDAL.GetCount() + 1).ToString() + "-" + DateTime.Now.Year;

            DateTime newDateTime = model.Fecha.Date + model.Hora.TimeOfDay;
            model.Fecha = newDateTime;
            try
            {
                if (ModelState.IsValid)
                {
                    actaEntregaDAL.Add(ConvertirActaEntrega(model));
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
            ActaEntregaViewModel modelo = CargarActaEntrega(actaEntregaDAL.GetActaEntrega(id));
            return View(modelo);
        }
        public ActionResult Editar(int id)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            actaEntregaDAL = new ActaEntregaDAL();
            ActaEntregaViewModel model = CargarActaEntrega(actaEntregaDAL.GetActaEntrega(id));
            model.TiposInstalaciones = tablaGeneralDAL.Get("ActaEntrega", "instalaciones").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.TiposActa = tablaGeneralDAL.Get("Actas", "tipoActa").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(ActaEntregaViewModel model)
        {
            actaEntregaDAL = new ActaEntregaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            model.TiposInstalaciones = tablaGeneralDAL.Get("ActaEntrega", "instalaciones").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.TiposActa = tablaGeneralDAL.Get("Actas", "tipoActa").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            DateTime newDateTime = model.Fecha.Date + model.Hora.TimeOfDay;
            model.Fecha = newDateTime;
            try
            {
                if (ModelState.IsValid)
                {
                    actaEntregaDAL.Edit(ConvertirActaEntrega(model));
                    return Redirect("~/ActaEntrega/Detalle/" + model.IdActaEntrega);
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}