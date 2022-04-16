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

        public ActasEntrega ConvertirActaEntrega(ActaEntregaViewModel model)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();

            return new ActasEntrega
            {
                idActaEntrega = model.IdActaEntrega,
                numeroFolio = model.NumeroFolio,
                encargado = policiaDAL.GetPoliciaCedula(model.Encargado).idPolicia,
                testigo = policiaDAL.GetPoliciaCedula(model.Testigo).idPolicia,
                supervisor = policiaDAL.GetPoliciaCedula(model.Supervisor).idPolicia,
                instalaciones = tablaGeneralDAL.GetCodigo("ActaEntrega", "instalaciones", model.Instalaciones.ToString()).idTablaGeneral,
                fechaHora = model.Fecha,
                razonSocial = model.RazonSocial,
                cedulaJuridica = model.CedulaJuridica,
                tipoActa = tablaGeneralDAL.GetCodigo("Actas", "tipoActa", model.TipoActa.ToString()).idTablaGeneral,
                consecutivoActa = model.ConsecutivoActa,
                nombreDe = model.NombreDe,
                identificacionEntregado = model.IdentificacionEntregado,
                recibe = model.Recibe,
                cedulaRecibe = model.CedulaJuridica,
                inventarioEntregado = model.InventarioEntregado

            };
        }
        public ActaEntregaViewModel CargarActaEntrega(ActasEntrega actaEntrega)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();

            return new ActaEntregaViewModel
            {
                IdActaEntrega = actaEntrega.idActaEntrega,
                NumeroFolio = actaEntrega.numeroFolio,
                Encargado = policiaDAL.GetPolicia(actaEntrega.encargado).cedula,
                Testigo = policiaDAL.GetPolicia(actaEntrega.testigo).cedula,
                Supervisor = policiaDAL.GetPolicia(actaEntrega.supervisor).cedula,
                Instalaciones = int.Parse(tablaGeneralDAL.Get(actaEntrega.instalaciones).codigo),
                VistaInstalaciones = tablaGeneralDAL.Get(actaEntrega.instalaciones).descripcion,
                Fecha = actaEntrega.fechaHora,
                Hora = actaEntrega.fechaHora,
                RazonSocial = actaEntrega.razonSocial,
                CedulaJuridica = actaEntrega.cedulaJuridica,
                TipoActa = int.Parse(tablaGeneralDAL.Get(actaEntrega.tipoActa).codigo),
                VistaTipoActa = tablaGeneralDAL.Get(actaEntrega.tipoActa).descripcion,
                ConsecutivoActa = actaEntrega.consecutivoActa,
                NombreDe = actaEntrega.nombreDe,
                IdentificacionEntregado = actaEntrega.identificacionEntregado,
                Recibe = actaEntrega.recibe,
                CedulaRecibe = actaEntrega.cedulaRecibe,
                InventarioEntregado = actaEntrega.inventarioEntregado,
                VistaPoliciaEncargado = policiaDAL.GetPolicia(actaEntrega.encargado).nombre,
                VistaPoliciaTestigo = policiaDAL.GetPolicia(actaEntrega.testigo).nombre,
                VistaPoliciaSupervisor = policiaDAL.GetPolicia(actaEntrega.supervisor).nombre
            };
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
        public ActionResult Index(string filtroSeleccionado, string busqueda, string busquedaFechaInicioH, string busquedaFechaFinalH)
        {
            actaEntregaDAL = new ActaEntregaDAL();
            policiaDAL = new PoliciaDAL();
            List<ActaEntregaViewModel> actasEntrega = new List<ActaEntregaViewModel>();
            List<ActaEntregaViewModel> actasEntregaFiltradas = new List<ActaEntregaViewModel>();
            foreach (ActasEntrega actaEntrega in actaEntregaDAL.Get())
            {
                actasEntrega.Add(CargarActaEntrega(actaEntrega));
            }
            if (busqueda != null)
            {
                foreach (ActaEntregaViewModel actaEntrega in actasEntrega)
                {
                    if (filtroSeleccionado == "Número de Folio")
                    {
                        if (actaEntrega.NumeroFolio.Contains(busqueda))
                        {
                            actasEntregaFiltradas.Add(actaEntrega);
                        }
                    }
                    if (filtroSeleccionado == "Nombre Policía Encargado")
                    {
                        if (policiaDAL.GetPoliciaCedula(actaEntrega.Encargado).nombre.Contains(busqueda))
                        {
                            actasEntregaFiltradas.Add(actaEntrega);
                        }
                    }
                }
                if (filtroSeleccionado == "Fecha")
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
            Session["idActaEntrega"] = id;
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