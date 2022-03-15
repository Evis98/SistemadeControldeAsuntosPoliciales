using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using BackEnd.DAL;
using BackEnd;
using FrontEnd.Models;
using FrontEnd.Models.ViewModels;


namespace FrontEnd.Controllers
{
    public class ArmaController : Controller
    {
        IPoliciaDAL policiaDAL;
        IArmaDAL armaDAL;
        ITablaGeneralDAL tablaGeneralDAL;
        public List<ListArmaViewModel> ConvertirListaArmas(List<Armas> armas)
        {

            policiaDAL = new PoliciaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            List<ListArmaViewModel> listadoArmas = (from d in armas
                                                    select new ListArmaViewModel
                                                    {
                                                        IdArma = d.idArma,
                                                        PoliciaAsignado = d.policiaAsignado,
                                                        NumeroSerie = d.numeroSerie,
                                                        TipoArma = tablaGeneralDAL.Get(d.tipoArma).descripcion,
                                                        Marca = tablaGeneralDAL.Get(d.marca).descripcion,
                                                        Modelo = d.modelo,
                                                        Calibre = tablaGeneralDAL.Get(d.calibre).descripcion,
                                                        Condicion = tablaGeneralDAL.Get(d.condicion).descripcion,
                                                        Ubicacion = tablaGeneralDAL.Get(d.ubicacion).descripcion,
                                                        Observacion = d.observacion,
                                                        EstadoArma = tablaGeneralDAL.Get(d.estadoArma).descripcion,
                                                    }).ToList();

            foreach (ListArmaViewModel arma in listadoArmas)
            {
                if (arma.PoliciaAsignado != null)
                {
                    arma.NombrePolicia = policiaDAL.GetPolicia((int)arma.PoliciaAsignado).nombre;
                }
                else
                {
                    arma.NombrePolicia = "No Asignado";
                }
            }
            return listadoArmas;
        }


        public Armas ConvertirArma(ArmaViewModel modelo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            return new Armas
            {
                idArma = modelo.IdArma,
                numeroSerie = modelo.NumeroSerie,
                tipoArma = tablaGeneralDAL.GetCodigo("Armas", "tipoArma", modelo.TipoArma.ToString()).idTablaGeneral,
                marca = tablaGeneralDAL.GetCodigo("Armas", "marca", modelo.Marca.ToString()).idTablaGeneral,
                modelo = modelo.ModeloArma,
                calibre = tablaGeneralDAL.GetCodigo("Armas", "calibre", modelo.Calibre.ToString()).idTablaGeneral,
                condicion = tablaGeneralDAL.GetCodigo("Armas", "condicion", modelo.Condicion.ToString()).idTablaGeneral,
                ubicacion = tablaGeneralDAL.GetCodigo("Armas", "ubicacion", modelo.Ubicacion.ToString()).idTablaGeneral,
                observacion = modelo.Observacion,
                estadoArma = tablaGeneralDAL.GetCodigo("Armas","estadoArma","1").idTablaGeneral,
            };
        }
        public ArmaViewModel CargarArma(Armas arma)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            ArmaViewModel armaEditar = new ArmaViewModel();
            {
                armaEditar.IdArma = arma.idArma;
                armaEditar.NumeroSerie = arma.numeroSerie;
                armaEditar.TipoArma = int.Parse(tablaGeneralDAL.Get(arma.tipoArma).codigo);
                armaEditar.Marca = int.Parse(tablaGeneralDAL.Get(arma.marca).codigo);
                armaEditar.ModeloArma = arma.modelo;
                armaEditar.Calibre = int.Parse(tablaGeneralDAL.Get(arma.calibre).codigo);
                armaEditar.Condicion = int.Parse(tablaGeneralDAL.Get(arma.condicion).codigo);
                armaEditar.Ubicacion = int.Parse(tablaGeneralDAL.Get(arma.ubicacion).codigo);
                armaEditar.Observacion = arma.observacion;
                armaEditar.EstadoArma = arma.estadoArma;
            };
            if (arma.policiaAsignado != null)
            {
                armaEditar.PoliciaAsignado = policiaDAL.GetPolicia((int)arma.policiaAsignado).cedula;
            }
            return armaEditar;
        }
        public ListArmaViewModel ConvertirArmaInverso(Armas arma)
        {
            policiaDAL = new PoliciaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            return new ListArmaViewModel
            {
                IdArma = arma.idArma,
                PoliciaAsignado = arma.policiaAsignado,

                NumeroSerie = arma.numeroSerie,
                TipoArma = tablaGeneralDAL.Get(arma.tipoArma).descripcion,
                Marca = tablaGeneralDAL.Get(arma.marca).descripcion,
                Modelo = arma.modelo,
                Calibre = tablaGeneralDAL.Get(arma.calibre).descripcion,
                Condicion = tablaGeneralDAL.Get(arma.condicion).descripcion,
                Ubicacion = tablaGeneralDAL.Get(arma.ubicacion).descripcion,
                Observacion = arma.observacion,
                EstadoArma = tablaGeneralDAL.Get(arma.estadoArma).descripcion
            };
        }
        public List<ListPoliciaViewModel> ConvertirListaPoliciasFiltrados(List<Policias> policias)
        {
            return (from d in policias
                    select new ListPoliciaViewModel
                    {
                        Cedula = d.cedula,
                        Nombre = d.nombre,
                    }).ToList();
        }
        public ActionResult Index(string filtroSeleccionado, string busqueda)
        {
            armaDAL = new ArmaDAL();
            List<ListArmaViewModel> armas = ConvertirListaArmas(armaDAL.Get());
            List<ListArmaViewModel> armasFiltradas = new List<ListArmaViewModel>();
            if (busqueda != null)
            {
                foreach (ListArmaViewModel arma in armas)
                {
                    if (filtroSeleccionado == "Policía Asignado")
                    {
                        if (arma.NombrePolicia.Contains(busqueda))
                        {
                            armasFiltradas.Add(arma);
                        }
                    }
                    if (filtroSeleccionado == "Número de Serie")
                    {
                        if (arma.NumeroSerie.Contains(busqueda))
                        {
                            armasFiltradas.Add(arma);
                        }
                    }
                }
                armas = armasFiltradas;
            }
            return View(armas);
        }

        public ActionResult Nuevo()
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            ArmaViewModel modelo = new ArmaViewModel()
            {
                TiposMarcas = tablaGeneralDAL.Get("Armas", "marca").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                TiposArma = tablaGeneralDAL.Get("Armas", "tipoArma").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                TiposCalibre = tablaGeneralDAL.Get("Armas", "calibre").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                TiposCondicion = tablaGeneralDAL.Get("Armas", "condicion").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                TiposUbicacion = tablaGeneralDAL.Get("Armas", "ubicacion").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
            };
            return View(modelo);
        }

        //Guarda la información ingresada en la página para crear policías
        [HttpPost]
        public ActionResult Nuevo(ArmaViewModel modelo)
        {
            armaDAL = new ArmaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            modelo.SerieFiltrada = armaDAL.GetSerieArma(modelo.NumeroSerie);
            try
            {
                if (!armaDAL.SerieExiste(modelo.NumeroSerie))
                {
                    if (ModelState.IsValid)
                    {
                        Armas arma = ConvertirArma(modelo);
                        if (modelo.PoliciaAsignado != null)
                        {
                            arma.policiaAsignado = policiaDAL.GetPoliciaCedula(modelo.PoliciaAsignado).idPolicia;
                        }
                        else
                        {
                            arma.policiaAsignado = null;

                        }
                        armaDAL.Add(arma);

                        int aux = armaDAL.GetArmaNumSerie(modelo.NumeroSerie).idArma;
                        TempData["smsnuevaarma"] = "Arma creada con éxito";
                        ViewBag.smsnuevaarma = TempData["smsnuevaarma"];
                        return Redirect("~/Arma/Detalle/" + aux);
                    }
                }
                modelo.TiposArma = tablaGeneralDAL.Get("Armas", "tipoArma").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                modelo.TiposCalibre = tablaGeneralDAL.Get("Armas", "calibre").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                modelo.TiposCondicion = tablaGeneralDAL.Get("Armas", "condicion").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                modelo.TiposUbicacion = tablaGeneralDAL.Get("Armas", "ubicacion").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                modelo.TiposMarcas = tablaGeneralDAL.Get("Armas", "marca").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                return View(modelo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public PartialViewResult ListaPoliciasBuscar(string nombre)
        {
            List<ListPoliciaViewModel> policias = new List<ListPoliciaViewModel>();

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
        public ActionResult Detalle(int id)
        {
            Session["idArma"] = id;
            armaDAL = new ArmaDAL();
            ListArmaViewModel modelo = ConvertirArmaInverso(armaDAL.GetArma(id));
            if (modelo.PoliciaAsignado != null)
            {
                modelo.NombrePolicia = policiaDAL.GetPolicia((int)modelo.PoliciaAsignado).cedula + " " + policiaDAL.GetPolicia((int)modelo.PoliciaAsignado).nombre;
            }
            else
            {
                modelo.NombrePolicia = "No Asignado";
            }
            try
            {
                ViewBag.smseditararma = TempData["smseditararma"];
                ViewBag.smscambioestadoarma = TempData["smscambioestadoarma"];
                ViewBag.smsnuevaarma = TempData["smsnuevaarma"];
            }
            catch { }
            return View(modelo);
        }
        public ActionResult Editar(int id)
        {
            armaDAL = new ArmaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            ArmaViewModel modelo = CargarArma(armaDAL.GetArma(id));
            modelo.TiposArma = tablaGeneralDAL.Get("Armas", "tipoArma").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            modelo.TiposCalibre = tablaGeneralDAL.Get("Armas", "calibre").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            modelo.TiposCondicion = tablaGeneralDAL.Get("Armas", "condicion").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            modelo.TiposUbicacion = tablaGeneralDAL.Get("Armas", "ubicacion").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            modelo.TiposMarcas = tablaGeneralDAL.Get("Armas", "marca").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });

            return View(modelo);
        }

        [HttpPost]
        public ActionResult Editar(ArmaViewModel modelo)
        {
            armaDAL = new ArmaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            try
            {
                if (ModelState.IsValid)
                {
                    Armas arma = ConvertirArma(modelo);
                    if (modelo.PoliciaAsignado != null)
                    {
                        arma.policiaAsignado = policiaDAL.GetPoliciaCedula(modelo.PoliciaAsignado).idPolicia;
                    }
                    else
                    {
                        arma.policiaAsignado = null;

                    }
                    armaDAL.Edit(arma); 
                    TempData["smseditararma"] = "Arma editada con éxito";
                    ViewBag.smseditararma = TempData["smseditararma"];
                    return Redirect("~/Arma/Detalle/" + modelo.IdArma);
                }
               
                return View(modelo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public ActionResult CambioEstadoArma(string id)
        {
            int estado;
            armaDAL = new ArmaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            try
            {
                if (id == "Activo")
                {
                    estado = tablaGeneralDAL.Get("Armas", "estadoArma", "Inactivo").idTablaGeneral;
                }
                else
                {
                    estado = tablaGeneralDAL.Get("Armas", "estadoArma", "Activo").idTablaGeneral;
                }
                armaDAL.CambiaEstadoArma((int)Session["idArma"], estado);
                TempData["smscambioestadoarma"] = "Cambio de estado realizado con éxito";
                ViewBag.smscambioestadoarma = TempData["smscambioestadoarma"];
                return Redirect("~/Arma/Detalle/" + Session["idArma"]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Policias> BuscarPolicias(List<Policias> policias, string filtroCedula)
        {
            List<Policias> policiasFiltrados = new List<Policias>();
            if (filtroCedula != null)
            {
                foreach (Policias policia in policias)
                {
                    if (policia.cedula.Contains(filtroCedula))
                    {
                        policiasFiltrados.Add(policia);
                    }
                }
                policias = policiasFiltrados;
            }
            return policias;
        }
    }
}