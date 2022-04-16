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


            ActasHallazgo actaHallazgo = new ActasHallazgo();
            {
                actaHallazgo.idActaHallazgo = modelo.IdActaHallazgo;
                actaHallazgo.numeroFolio = modelo.NumeroFolio;
                actaHallazgo.distrito = tablaGeneralDAL.GetCodigo("Generales", "distrito", modelo.Distrito.ToString()).idTablaGeneral;
                actaHallazgo.fechaHora = modelo.Fecha;
                actaHallazgo.avenida = modelo.Avenida;
                actaHallazgo.calle = modelo.Calle;
                actaHallazgo.otrasSenas = modelo.OtrasSenas;
                actaHallazgo.inventario = modelo.Inventario;
                actaHallazgo.estadoActa = tablaGeneralDAL.GetCodigo("Actas", "estadoActa", "1").idTablaGeneral;
                actaHallazgo.observaciones = modelo.Observaciones;
                actaHallazgo.encargado = policiaDAL.GetPoliciaCedula(modelo.Encargado).idPolicia;
                if (modelo.VistaPoliciaTestigo != null) {
                    actaHallazgo.testigo = policiaDAL.GetPoliciaCedula(modelo.Testigo).idPolicia;
                }
                actaHallazgo.supervisor = policiaDAL.GetPoliciaCedula(modelo.Supervisor).idPolicia;
            };
            return actaHallazgo;
        }
        public ActaHallazgoViewModel CargarActaHallazgo(ActasHallazgo actaHallazgo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            ActaHallazgoViewModel actaHallazgoCarga = new ActaHallazgoViewModel();
            {
                actaHallazgoCarga.IdActaHallazgo = actaHallazgo.idActaHallazgo;
                actaHallazgoCarga.NumeroFolio = actaHallazgo.numeroFolio;
                actaHallazgoCarga.Encargado = policiaDAL.GetPolicia(actaHallazgo.encargado).cedula;
                actaHallazgoCarga.Supervisor = policiaDAL.GetPolicia(actaHallazgo.supervisor).cedula;
                actaHallazgoCarga.Distrito = int.Parse(tablaGeneralDAL.Get(actaHallazgo.distrito).codigo);
                actaHallazgoCarga.VistaDistrito = tablaGeneralDAL.Get(actaHallazgo.distrito).descripcion;
                actaHallazgoCarga.Fecha = actaHallazgo.fechaHora.Value;
                actaHallazgoCarga.Hora = actaHallazgo.fechaHora.Value;
                actaHallazgoCarga.Avenida = actaHallazgo.avenida;
                actaHallazgoCarga.Calle = actaHallazgo.calle;
                actaHallazgoCarga.OtrasSenas = actaHallazgo.otrasSenas;
                actaHallazgoCarga.Inventario = actaHallazgo.inventario;
                actaHallazgoCarga.Observaciones = actaHallazgo.observaciones;
                actaHallazgoCarga.VistaPoliciaEncargado = policiaDAL.GetPolicia(actaHallazgo.encargado).nombre;
                actaHallazgoCarga.VistaPoliciaSupervisor = policiaDAL.GetPolicia(actaHallazgo.supervisor).nombre;
                actaHallazgoCarga.EstadoActa = actaHallazgo.estadoActa;
                actaHallazgoCarga.VistaEstadoActa = tablaGeneralDAL.Get(actaHallazgo.estadoActa).descripcion;
                if ( actaHallazgo.testigo != null)
                {
                    actaHallazgoCarga.Testigo = policiaDAL.GetPolicia((int)actaHallazgo.testigo).cedula;
                    actaHallazgoCarga.VistaPoliciaTestigo = policiaDAL.GetPolicia((int)actaHallazgo.testigo).nombre;
                }
            };
            return actaHallazgoCarga;
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

        public ActionResult Index(string filtroSeleccionados, string busqueda, string busquedaFechaInicioH, string busquedaFechaFinalH)
        {
            actaHallazgoDAL = new ActaHallazgoDAL();
            policiaDAL = new PoliciaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            List<ActaHallazgoViewModel> actasHallazgo = new List<ActaHallazgoViewModel>();
            List<ActaHallazgoViewModel> actasHallazgoFiltradas = new List<ActaHallazgoViewModel>();
            List<TablaGeneral> comboindex = tablaGeneralDAL.Get("ActasHallazgo", "index");
            List<SelectListItem> items = comboindex.ConvertAll(d =>
            {               
                return new SelectListItem()
                {
                    Text = d.descripcion                                           
                };              
            });
            ViewBag.items = items;
            foreach (ActasHallazgo actaHallazgo in actaHallazgoDAL.Get())
            {
                actasHallazgo.Add(CargarActaHallazgo(actaHallazgo));
            }
            
            if (busqueda != null && filtroSeleccionados != "" )
            {
             
                foreach (ActaHallazgoViewModel actaHallazgo in actasHallazgo)
                {                   
                    if (filtroSeleccionados == "Número de Folio")
                    {
                        if (actaHallazgo.NumeroFolio.Contains(busqueda))
                        {
                            actasHallazgoFiltradas.Add(actaHallazgo);
                        }
                    }
                    if (filtroSeleccionados == "Nombre Policía Encargado")
                    {
                        if (policiaDAL.GetPoliciaCedula(actaHallazgo.Encargado).nombre.Contains(busqueda))
                        {
                            actasHallazgoFiltradas.Add(actaHallazgo);
                        }
                    }
                }
                if (filtroSeleccionados == "Fecha")
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
            Session["idActaHallazgo"] = id;
            actaHallazgoDAL = new ActaHallazgoDAL();
            ActaHallazgoViewModel modelo = CargarActaHallazgo(actaHallazgoDAL.GetActaHallazgo(id));
           
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
                   
                    return Redirect("~/ActaHallazgo/Detalle/" + model.IdActaHallazgo);
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public ActionResult CambioEstadoActa(int id)
        {
            int estado;
            actaHallazgoDAL = new ActaHallazgoDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            try
            {
                if (tablaGeneralDAL.Get((int)id).descripcion == "Activa")
                {
                    estado = tablaGeneralDAL.Get("Actas", "estadoActa", "Inactiva").idTablaGeneral;
                }
                else
                {
                    estado = tablaGeneralDAL.Get("Actas", "estadoActa", "Activa").idTablaGeneral;
                }
                actaHallazgoDAL.CambiaEstadoActa((int)Session["idActaHallazgo"], estado);
                return Redirect("~/ActaHallazgo/Detalle/" + Session["idActaHallazgo"]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}