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
    public class ActaDeDestruccionDePerecederosController : Controller
    {
        ITablaGeneralDAL tablaGeneralDAL;
        IActaDeDestruccionDePerecederosDAL actaDeDestruccionDePerecederosDAL;
        IPoliciaDAL policiaDAL;
        IActaHallazgoDAL actaHallazgoDAL;
        IActaDecomisoDAL actaDecomisoDAL;

        public ActasDeDestruccionDePerecederos ConvertirActaDeDestruccionDePerecederos(ActaDeDestruccionDePerecederosViewModel model)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();

            ActasDeDestruccionDePerecederos acta = new ActasDeDestruccionDePerecederos();
            {
                acta.idActaDeDestruccionDePerecederos = model.IdActaDeDestruccionDePerecederos;
                acta.numeroFolio = model.NumeroFolio;
                acta.encargado = policiaDAL.GetPoliciaCedula(model.Encargado).idPolicia;
                acta.testigo = policiaDAL.GetPoliciaCedula(model.Testigo).idPolicia;
                acta.supervisor = policiaDAL.GetPoliciaCedula(model.Supervisor).idPolicia;
                acta.fechaHora = model.Fecha;
                acta.tipoActaD = tablaGeneralDAL.GetCodigo("Actas", "tipoActa"/*D*/, model.TipoActaD.ToString()).idTablaGeneral;
                if (tablaGeneralDAL.Get(acta.tipoActaD).descripcion == "Hallazgo")
                {
                    acta.consecutivoActa = model.ConsecutivoActaHallazgo;
                }
                else
                {
                    acta.consecutivoActa = model.ConsecutivoActaDecomiso;
                }
                acta.inventarioDestruido = model.InventarioDestruido;
            };
            return acta;
        }

        public ActaDeDestruccionDePerecederosViewModel CargarActaDeDestruccionDePerecederos(ActasDeDestruccionDePerecederos actaDeDestruccionDePerecederos)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();

            ActaDeDestruccionDePerecederosViewModel acta = new ActaDeDestruccionDePerecederosViewModel();
            {
                acta.IdActaDeDestruccionDePerecederos = actaDeDestruccionDePerecederos.idActaDeDestruccionDePerecederos;
                acta.NumeroFolio = actaDeDestruccionDePerecederos.numeroFolio;
                acta.Encargado = policiaDAL.GetPolicia(actaDeDestruccionDePerecederos.encargado).cedula;
                acta.Testigo = policiaDAL.GetPolicia(actaDeDestruccionDePerecederos.testigo).cedula;
                acta.Supervisor = policiaDAL.GetPolicia(actaDeDestruccionDePerecederos.supervisor).cedula;
                acta.Fecha = actaDeDestruccionDePerecederos.fechaHora;
                acta.Hora = actaDeDestruccionDePerecederos.fechaHora;
                acta.TipoActaD = int.Parse(tablaGeneralDAL.Get(actaDeDestruccionDePerecederos.tipoActaD).codigo);
                acta.VistaTipoActaD = tablaGeneralDAL.Get(actaDeDestruccionDePerecederos.tipoActaD).descripcion;
                if (tablaGeneralDAL.Get(actaDeDestruccionDePerecederos.tipoActaD).descripcion == "Hallazgo")
                {
                    acta.ConsecutivoActaHallazgo = actaDeDestruccionDePerecederos.consecutivoActa;
                }
                else
                {
                    acta.ConsecutivoActaDecomiso = actaDeDestruccionDePerecederos.consecutivoActa;
                }
                acta.InventarioDestruido = actaDeDestruccionDePerecederos.inventarioDestruido;
                acta.VistaPoliciaEncargado = policiaDAL.GetPolicia(actaDeDestruccionDePerecederos.encargado).nombre;
                acta.VistaPoliciaTestigo = policiaDAL.GetPolicia(actaDeDestruccionDePerecederos.testigo).nombre;
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

        public ActionResult Index(string filtroSeleccionado, string busqueda, string busquedaFechaInicioH, string busquedaFechaFinalH)
        {
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
                        if (policiaDAL.GetPoliciaCedula(actaDeDestruccionDePerecederos.Encargado).nombre.Contains(busqueda))
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
            tablaGeneralDAL = new TablaGeneralDAL();
            ActaDeDestruccionDePerecederosViewModel modelo = new ActaDeDestruccionDePerecederosViewModel()
            {
                TiposActaD = tablaGeneralDAL.Get("Actas", "tipoActa"/*D*/).Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                Fecha = DateTime.Today

            };
            return View(modelo);
        }

        [HttpPost]
        public ActionResult Nuevo(ActaDeDestruccionDePerecederosViewModel model)
        {
            actaDeDestruccionDePerecederosDAL = new ActaDeDestruccionDePerecederosDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            model.TiposActaD = tablaGeneralDAL.Get("Actas", "tipoActa"/*D*/).Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.NumeroFolio = (actaDeDestruccionDePerecederosDAL.GetCount() + 1).ToString() + "-" + DateTime.Now.Year;
            DateTime newDateTime = model.Fecha.Date + model.Hora.TimeOfDay;
            model.Fecha = newDateTime;
            try
            {
                if (ModelState.IsValid)
                {
                    actaDeDestruccionDePerecederosDAL.Add(ConvertirActaDeDestruccionDePerecederos(model));
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
            actaDeDestruccionDePerecederosDAL = new ActaDeDestruccionDePerecederosDAL();
            ActaDeDestruccionDePerecederosViewModel modelo = CargarActaDeDestruccionDePerecederos(actaDeDestruccionDePerecederosDAL.GetActaDeDestruccionDePerecederos(id));
            return View(modelo);
        }

        public ActionResult Editar(int id)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            actaDeDestruccionDePerecederosDAL = new ActaDeDestruccionDePerecederosDAL();
            ActaDeDestruccionDePerecederosViewModel model = CargarActaDeDestruccionDePerecederos(actaDeDestruccionDePerecederosDAL.GetActaDeDestruccionDePerecederos(id));
            model.TiposActaD = tablaGeneralDAL.Get("Actas", "tipoActa"/*D*/).Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(ActaDeDestruccionDePerecederosViewModel model)
        {
            actaDeDestruccionDePerecederosDAL = new ActaDeDestruccionDePerecederosDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            model.TiposActaD = tablaGeneralDAL.Get("Actas", "tipoActa"/*D*/).Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            DateTime newDateTime = model.Fecha.Date + model.Hora.TimeOfDay;
            model.Fecha = newDateTime;
            try
            {
                if (ModelState.IsValid)
                {
                    actaDeDestruccionDePerecederosDAL.Edit(ConvertirActaDeDestruccionDePerecederos(model));
                    return Redirect("~/ActaDeDestruccionDePerecederos/Detalle/" + model.IdActaDeDestruccionDePerecederos);
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