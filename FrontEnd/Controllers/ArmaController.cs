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
                estadoArma = tablaGeneralDAL.GetCodigo("Generales","estado","1").idTablaGeneral,
            };
        }
        public ArmaViewModel CargarArma(Armas arma)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            ArmaViewModel armaCarga = new ArmaViewModel();
            {
                armaCarga.IdArma = arma.idArma;
                armaCarga.NumeroSerie = arma.numeroSerie;
                armaCarga.TipoArma = int.Parse(tablaGeneralDAL.Get(arma.tipoArma).codigo);
                armaCarga.VistaTipoArma = tablaGeneralDAL.Get(arma.tipoArma).descripcion;
                armaCarga.Marca = int.Parse(tablaGeneralDAL.Get(arma.marca).codigo);
                armaCarga.VistaMarca = tablaGeneralDAL.Get(arma.marca).descripcion;
                armaCarga.ModeloArma = arma.modelo;
                armaCarga.Calibre = int.Parse(tablaGeneralDAL.Get(arma.calibre).codigo);
                armaCarga.VistaCalibre = tablaGeneralDAL.Get(arma.calibre).descripcion;
                armaCarga.Condicion = int.Parse(tablaGeneralDAL.Get(arma.condicion).codigo);
                armaCarga.VistaCondicion = tablaGeneralDAL.Get(arma.condicion).descripcion;
                armaCarga.Ubicacion = int.Parse(tablaGeneralDAL.Get(arma.ubicacion).codigo);
                armaCarga.VistaUbicacion = tablaGeneralDAL.Get(arma.ubicacion).descripcion;
                armaCarga.Observacion = arma.observacion;
                armaCarga.EstadoArma = arma.estadoArma;
                armaCarga.VistaEstadoArma = tablaGeneralDAL.Get(arma.estadoArma).descripcion;
                if (arma.policiaAsignado != null)
                {
                    armaCarga.PoliciaAsignado = policiaDAL.GetPolicia((int)arma.policiaAsignado).cedula;
                    armaCarga.NombrePolicia = policiaDAL.GetPolicia((int)arma.policiaAsignado).nombre;
                }
                else
                {
                    armaCarga.NombrePolicia = "NO ASIGNADO";
                }
            };
            return armaCarga;
        }

        public ActionResult Index(string filtrosSeleccionado, string busqueda)
        {
            armaDAL = new ArmaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            List<ArmaViewModel> armas = new List<ArmaViewModel>();
            List<ArmaViewModel> armasFiltradas = new List<ArmaViewModel>();
            List<TablaGeneral> comboindex = tablaGeneralDAL.Get("Armas", "index");
            List<SelectListItem> items = comboindex.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.descripcion
                };
            });
            ViewBag.items = items;
            foreach (Armas arma in armaDAL.Get())
            {
                armas.Add(CargarArma(arma));
            }
            if (busqueda != null && filtrosSeleccionado != "")
            {
                foreach (ArmaViewModel arma in armas)
                {
                    if (filtrosSeleccionado == "Policía Asignado")
                    {
                        if (arma.NombrePolicia.Contains(busqueda))
                        {
                            armasFiltradas.Add(arma);
                        }
                    }
                    if (filtrosSeleccionado == "Número de Serie")
                    {
                        if (arma.NumeroSerie.Contains(busqueda))
                        {
                            armasFiltradas.Add(arma);
                        }
                    }
                }
                armas = armasFiltradas;
            }
            return View(armas.OrderByDescending(x => x.PoliciaAsignado).ToList());
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
        public ActionResult Nuevo(ArmaViewModel model)
        {
            armaDAL = new ArmaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
                model.SerieFiltrada = armaDAL.GetSerieArma(model.NumeroSerie);

            try
            {
                if (!armaDAL.SerieExiste(model.NumeroSerie))
                {
                    if (ModelState.IsValid)
                    {
                        Armas arma = ConvertirArma(model);
                        armaDAL.Add(arma);
                        int aux = armaDAL.GetArmaNumSerie(model.NumeroSerie).idArma;
                    
                        return Redirect("~/Arma/Detalle/" + aux);
                    }
                }
                model.TiposArma = tablaGeneralDAL.Get("Armas", "tipoArma").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                model.TiposCalibre = tablaGeneralDAL.Get("Armas", "calibre").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                model.TiposCondicion = tablaGeneralDAL.Get("Armas", "condicion").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                model.TiposUbicacion = tablaGeneralDAL.Get("Armas", "ubicacion").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                model.TiposMarcas = tablaGeneralDAL.Get("Armas", "marca").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public ActionResult Detalle(int id)
        {
            Session["idArma"] = id;
            armaDAL = new ArmaDAL();
            ArmaViewModel modelo = CargarArma(armaDAL.GetArma(id));
            if (modelo.PoliciaAsignado != null)
            {
                modelo.NombrePolicia = policiaDAL.GetPoliciaCedula(modelo.PoliciaAsignado).cedula + " " + policiaDAL.GetPoliciaCedula (modelo.PoliciaAsignado).nombre;
            }
            else
            {
                modelo.NombrePolicia = "NO ASIGNADO";
            }
          
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
            if (modelo.PoliciaAsignado != null)
            {
                modelo.NombrePolicia = policiaDAL.GetPoliciaCedula(modelo.PoliciaAsignado).cedula + " " + policiaDAL.GetPoliciaCedula(modelo.PoliciaAsignado).nombre;
            }
            else
            {
                modelo.NombrePolicia = "NO ASIGNADO";
            }
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
                    
                    return Redirect("~/Arma/Detalle/" + modelo.IdArma);
                }
               
                return View(modelo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public ActionResult CambioEstadoArma(int id)
        {
            int estado;
            armaDAL = new ArmaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            try
            {
                if (tablaGeneralDAL.Get((int)id).descripcion == "Activo")
                {
                    estado = tablaGeneralDAL.Get("Generales", "estado", "Inactivo").idTablaGeneral;
                }
                else
                {
                    estado = tablaGeneralDAL.Get("Generales", "estado", "Activo").idTablaGeneral;
                }
                armaDAL.CambiaEstadoArma((int)Session["idArma"], estado);                
                return Redirect("~/Arma/Detalle/" + Session["idArma"]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}