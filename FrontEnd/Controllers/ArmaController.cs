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
            return (from d in armas
                    select new ListArmaViewModel
                    {
                        IdArma = d.idArma,
                        PoliciaAsignado = d.policiaAsignado,
                        NumeroSerie = d.numeroSerie,
                        TipoArma = tablaGeneralDAL.GetDescripcion(d.tipoArma),
                        Marca = d.marca,
                        Modelo = d.modelo,
                        Calibre = tablaGeneralDAL.GetDescripcion(d.calibre),
                        Condicion = tablaGeneralDAL.GetDescripcion(d.condicion),
                        Ubicacion = tablaGeneralDAL.GetDescripcion(d.ubicacion),
                        Observacion = d.observacion,
                        EstadoArma = tablaGeneralDAL.GetDescripcion(d.estadoArma),
                        NombrePolicia = policiaDAL.GetPolicia(d.policiaAsignado).nombre
                    }).ToList();
        }
        public Armas ConvertirArma(ArmaViewModel modelo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            return new Armas
            {
                idArma = modelo.IdArma,
                policiaAsignado = policiaDAL.GetPoliciaCedula(modelo.PoliciaAsignado),
                numeroSerie = modelo.NumeroSerie,
                tipoArma = tablaGeneralDAL.GetIdTipoArma(modelo.TipoArma),
                marca = modelo.Marca,
                modelo = modelo.ModeloArma,
                calibre = tablaGeneralDAL.GetIdCalibreArma(modelo.Calibre),
                condicion = tablaGeneralDAL.GetIdCondicionArma(modelo.Condicion),
                ubicacion = tablaGeneralDAL.GetIdUbicacionArma(modelo.Ubicacion),
                observacion = modelo.Observacion,
                estadoArma = tablaGeneralDAL.EstadoDefaultArma(),
            };
        }
        public ArmaViewModel CargarArma(Armas arma)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            return new ArmaViewModel
            {
                IdArma = arma.idArma,
                PoliciaAsignado = policiaDAL.GetPolicia(arma.policiaAsignado).cedula,
                NumeroSerie = arma.numeroSerie,
                TipoArma = int.Parse(tablaGeneralDAL.GetCodigo(arma.tipoArma)),
                Marca = arma.marca,
                ModeloArma = arma.modelo,
                Calibre = int.Parse(tablaGeneralDAL.GetCodigo(arma.calibre)),
                Condicion = int.Parse(tablaGeneralDAL.GetCodigo(arma.condicion)),
                Ubicacion = int.Parse(tablaGeneralDAL.GetCodigo(arma.ubicacion)),
                Observacion = arma.observacion,
                EstadoArma = arma.estadoArma,
            };
        }
        public ListArmaViewModel ConvertirArmaInverso(Armas arma)
        {
            policiaDAL = new PoliciaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            return new ListArmaViewModel
            {
                IdArma = arma.idArma,
                PoliciaAsignado = arma.policiaAsignado,
                NombrePolicia = policiaDAL.GetPolicia(arma.policiaAsignado).cedula + " " + policiaDAL.GetPolicia(arma.policiaAsignado).nombre,
                NumeroSerie = arma.numeroSerie,
                TipoArma = tablaGeneralDAL.GetDescripcion(arma.tipoArma),
                Marca = arma.marca,
                Modelo = arma.modelo,
                Calibre = tablaGeneralDAL.GetDescripcion(arma.calibre),
                Condicion = tablaGeneralDAL.GetDescripcion(arma.condicion),
                Ubicacion = tablaGeneralDAL.GetDescripcion(arma.ubicacion),
                Observacion = arma.observacion,
                EstadoArma = tablaGeneralDAL.GetDescripcion(arma.estadoArma)
            };
        }
        // GET: Arma
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
                TiposArma = tablaGeneralDAL.GetTiposArma().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                TiposCalibre = tablaGeneralDAL.GetTiposCalibre().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                TiposCondicion = tablaGeneralDAL.GetTiposCondicion().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                TiposUbicacion = tablaGeneralDAL.GetTiposUbicacion().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
            };

            return View(modelo);
        }

        //Guarda la información ingresada en la página para crear policías
        [HttpPost]
        public ActionResult Nuevo(ArmaViewModel model)
        {
            policiaDAL = new PoliciaDAL();
            armaDAL = new ArmaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
          
            try
            {
                if (!armaDAL.SerieExiste(model.NumeroSerie))
                {
                    if (ModelState.IsValid)
                    {
                        armaDAL.Add(ConvertirArma(model));
                        int aux = armaDAL.GetArmaNumSerie(model.NumeroSerie);
                        return Redirect("~/Arma/Detalle/" + aux);
                    }
                }
                model.TiposArma = tablaGeneralDAL.GetTiposArma().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                model.TiposCalibre = tablaGeneralDAL.GetTiposCalibre().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                model.TiposCondicion = tablaGeneralDAL.GetTiposCondicion().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                model.TiposUbicacion = tablaGeneralDAL.GetTiposUbicacion().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
               

                return View(model);
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
            return View(modelo);
        }
        public ActionResult Editar(int id)
        {
            armaDAL = new ArmaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            ArmaViewModel modelo = CargarArma(armaDAL.GetArma(id));
            modelo.TiposArma = tablaGeneralDAL.GetTiposArma().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            modelo.TiposCalibre = tablaGeneralDAL.GetTiposCalibre().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            modelo.TiposCondicion = tablaGeneralDAL.GetTiposCondicion().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            modelo.TiposUbicacion = tablaGeneralDAL.GetTiposUbicacion().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
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
                    armaDAL.Edit(ConvertirArma(modelo));
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
                    estado = tablaGeneralDAL.GetIdEstadoArmas("Inactivo");
                }
                else
                {
                    estado = tablaGeneralDAL.GetIdEstadoArmas("Activo");
                }
                tablaGeneralDAL.CambiaEstadoArma((int)Session["idArma"], estado);
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