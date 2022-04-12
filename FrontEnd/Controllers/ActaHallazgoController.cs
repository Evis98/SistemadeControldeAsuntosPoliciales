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
    public class ActaHallazgoController : Controller
    {
        ITablaGeneralDAL tablaGeneralDAL;
        IActaHallazgoDAL actaHallazgoDAL;
        IPoliciaDAL policiaDAL;

        public ActasHallazgo ConvertirActaHallazgo(ActaHallazgoViewModel modelo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();

            return new ActasHallazgo
            {
                idActaHallazgo = modelo.IdActaHallazgo,
                numeroFolio = modelo.NumeroFolio,
                distrito = tablaGeneralDAL.GetCodigo("Generales", "distrito", modelo.Distrito.ToString()).idTablaGeneral,
                fechaHora = modelo.Fecha,
                avenida = modelo.Avenida,
                calle = modelo.Calle,
                otrasSenas = modelo.OtrasSenas,
                inventario = modelo.Inventario,
                observaciones = modelo.Observaciones,
                encargado = policiaDAL.GetPoliciaCedula(modelo.Encargado).idPolicia,
                testigo = policiaDAL.GetPoliciaCedula(modelo.Testigo).idPolicia,
                supervisor = policiaDAL.GetPoliciaCedula(modelo.Supervisor).idPolicia,
            };
        }
        public ActaHallazgoViewModel CargarActaHallazgo(ActasHallazgo actaHallazgo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();

            return new ActaHallazgoViewModel
            {
                IdActaHallazgo = actaHallazgo.idActaHallazgo,
                NumeroFolio = actaHallazgo.numeroFolio,
                Encargado = policiaDAL.GetPolicia(actaHallazgo.encargado).cedula,
                Testigo = policiaDAL.GetPolicia(actaHallazgo.testigo).cedula,
                Supervisor = policiaDAL.GetPolicia(actaHallazgo.supervisor).cedula,
                Distrito = int.Parse(tablaGeneralDAL.Get(actaHallazgo.distrito).codigo),
                VistaDistrito = tablaGeneralDAL.Get(actaHallazgo.distrito).descripcion,
                Fecha = actaHallazgo.fechaHora.Value,
                Hora = actaHallazgo.fechaHora.Value,
                Avenida = actaHallazgo.avenida,
                Calle = actaHallazgo.calle,
                OtrasSenas = actaHallazgo.otrasSenas,
                Inventario = actaHallazgo.inventario,
                Observaciones = actaHallazgo.observaciones,
                VistaPoliciaEncargado = policiaDAL.GetPolicia(actaHallazgo.encargado).nombre,
                VistaPoliciaTestigo = policiaDAL.GetPolicia(actaHallazgo.testigo).nombre,
                VistaPoliciaSupervisor = policiaDAL.GetPolicia(actaHallazgo.supervisor).nombre
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

        public ActionResult Index(string filtroSeleccionado, string busqueda, string busquedaFechaInicioH, string busquedaFechaFinalH)
        {
            actaHallazgoDAL = new ActaHallazgoDAL();
            policiaDAL = new PoliciaDAL();
            List<ActaHallazgoViewModel> actasHallazgo = new List<ActaHallazgoViewModel>();
            List<ActaHallazgoViewModel> actasHallazgoFiltradas = new List<ActaHallazgoViewModel>();
            foreach (ActasHallazgo actaHallazgo in actaHallazgoDAL.Get())
            {
                actasHallazgo.Add(CargarActaHallazgo(actaHallazgo));
            }
            if (busqueda != null)
            {
                foreach (ActaHallazgoViewModel actaHallazgo in actasHallazgo)
                {
                    if (filtroSeleccionado == "Número de Folio")
                    {
                        if (actaHallazgo.NumeroFolio.Contains(busqueda))
                        {
                            actasHallazgoFiltradas.Add(actaHallazgo);
                        }
                    }
                    if (filtroSeleccionado == "Nombre Policía Encargado")
                    {
                        if (policiaDAL.GetPoliciaCedula(actaHallazgo.Encargado).nombre.Contains(busqueda))
                        {
                            actasHallazgoFiltradas.Add(actaHallazgo);
                        }
                    }
                }
                if (filtroSeleccionado == "Fecha")
                {
                    DateTime fechaInicio = DateTime.Parse(busquedaFechaInicioH);
                    DateTime fechaFinal = DateTime.Parse(busquedaFechaFinalH);
                    if (fechaInicio < fechaFinal)
                    {
                        if (actaHallazgoDAL.GetActaHallazgoRango(fechaInicio, fechaFinal) != null)
                        {
                            foreach (ActasHallazgo actaHallazgoFecha in actaHallazgoDAL.GetActaHallazgoRango(fechaInicio, fechaFinal).ToList())
                            {
                                actasHallazgoFiltradas.Add(CargarActaHallazgo(actaHallazgoFecha));
                            }
                        }
                    }
                }
                
                actasHallazgo = actasHallazgoFiltradas;
            }
            return View(actasHallazgo.OrderBy(x => x.NumeroFolio).ToList());
        }
        public ActionResult Nuevo()
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            ActaHallazgoViewModel modelo = new ActaHallazgoViewModel()
            {
                Distritos = tablaGeneralDAL.Get("Generales", "distrito").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                Fecha = DateTime.Today

            };
            return View(modelo);
        }

        [HttpPost]
        public ActionResult Nuevo(ActaHallazgoViewModel model)
        {
            actaHallazgoDAL = new ActaHallazgoDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            model.Distritos = tablaGeneralDAL.Get("Generales", "distrito").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.NumeroFolio = (actaHallazgoDAL.GetCount() + 1).ToString() + "-" + DateTime.Now.Year;
            DateTime newDateTime = model.Fecha.Date + model.Hora.TimeOfDay;
            model.Fecha = newDateTime;
            try
            {
                if (ModelState.IsValid)
                {
                    actaHallazgoDAL.Add(ConvertirActaHallazgo(model));
                    int aux = actaHallazgoDAL.GetActaHallazgoFolio(model.NumeroFolio).idActaHallazgo;
                    TempData["smsnuevaActaH"] = "Acta de Hallazgo creada con éxito";
                    ViewBag.smsnuevaActaH = TempData["smsnuevaActaH"];
                    return Redirect("~/ActaHallazgo/Detalle/" + aux);
               
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
            actaHallazgoDAL = new ActaHallazgoDAL();
            ActaHallazgoViewModel modelo = CargarActaHallazgo(actaHallazgoDAL.GetActaHallazgo(id));
            try
            {
                ViewBag.smsnuevaActaH = TempData["smsnuevaActaH"];            
                ViewBag.smseditaractaH = TempData["smseditaractaH"];
            }
            catch { }
            return View(modelo);
        }

        //Devuelve la página de edición de policías con sus apartados llenos
        public ActionResult Editar(int id)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            actaHallazgoDAL = new ActaHallazgoDAL();
            ActaHallazgoViewModel modelo = CargarActaHallazgo(actaHallazgoDAL.GetActaHallazgo(id));
            modelo.Distritos = tablaGeneralDAL.Get("Generales", "distrito").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            return View(modelo);
        }

        [HttpPost]
        public ActionResult Editar(ActaHallazgoViewModel model)
        {
            actaHallazgoDAL = new ActaHallazgoDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            model.Distritos = tablaGeneralDAL.Get("Generales", "distrito").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            DateTime newDateTime = model.Fecha.Date + model.Hora.TimeOfDay;
            model.Fecha = newDateTime;
            try
            {
                if (ModelState.IsValid)
                {
                    actaHallazgoDAL.Edit(ConvertirActaHallazgo(model));
                    TempData["smseditaractaH"] = "Acta de Hallazgo editada con éxito";
                    ViewBag.smseditaractaH = TempData["smseditaractaH"];
                    return Redirect("~/ActaHallazgo/Detalle/" + model.IdActaHallazgo);
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