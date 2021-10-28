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
                                                        TipoArma = tablaGeneralDAL.GetDescripcion(d.tipoArma),
                                                        Marca = d.marca,
                                                        Modelo = d.modelo,
                                                        Calibre = tablaGeneralDAL.GetDescripcion(d.calibre),
                                                        Condicion = tablaGeneralDAL.GetDescripcion(d.condicion),
                                                        Ubicacion = tablaGeneralDAL.GetDescripcion(d.ubicacion),
                                                        Observacion = d.observacion,
                                                        EstadoArma = tablaGeneralDAL.GetDescripcion(d.estadoArma),
                                                    }).ToList();

            foreach (ListArmaViewModel arma in listadoArmas)
            {
                if (arma.PoliciaAsignado != null)
                {
                    arma.NombrePolicia = policiaDAL.GetPolicia(arma.PoliciaAsignado).nombre;
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
            ArmaViewModel armaEditar = new ArmaViewModel();
            {
                armaEditar.IdArma = arma.idArma;
                armaEditar.NumeroSerie = arma.numeroSerie;
                armaEditar.TipoArma = int.Parse(tablaGeneralDAL.GetCodigo(arma.tipoArma));
                armaEditar.Marca = arma.marca;
                armaEditar.ModeloArma = arma.modelo;
                armaEditar.Calibre = int.Parse(tablaGeneralDAL.GetCodigo(arma.calibre));
                armaEditar.Condicion = int.Parse(tablaGeneralDAL.GetCodigo(arma.condicion));
                armaEditar.Ubicacion = int.Parse(tablaGeneralDAL.GetCodigo(arma.ubicacion));
                armaEditar.Observacion = arma.observacion;
                armaEditar.EstadoArma = arma.estadoArma;
            };
            if (arma.policiaAsignado != null)
            {
                armaEditar.PoliciaAsignado = policiaDAL.GetPolicia(arma.policiaAsignado).cedula;
            }
            //else
            //{

            //    armaEditar.PoliciaAsignado = "No Asignado";
            //}

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
                        if (modelo.PoliciaAsignado != null) { 
                        arma.policiaAsignado = policiaDAL.GetPoliciaCedula(modelo.PoliciaAsignado);
                        }
                        else {
                            arma.policiaAsignado = null;

                        }
                        armaDAL.Add(arma);
                   
                        int aux = armaDAL.GetArmaNumSerie(modelo.NumeroSerie);
                        return Redirect("~/Arma/Detalle/" + aux);
                    }

                }
                modelo.TiposArma = tablaGeneralDAL.GetTiposArma().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                modelo.TiposCalibre = tablaGeneralDAL.GetTiposCalibre().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                modelo.TiposCondicion = tablaGeneralDAL.GetTiposCondicion().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                modelo.TiposUbicacion = tablaGeneralDAL.GetTiposUbicacion().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
               

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
                modelo.NombrePolicia = policiaDAL.GetPolicia(modelo.PoliciaAsignado).cedula + " " + policiaDAL.GetPolicia(modelo.PoliciaAsignado).nombre;
            }
            else
            {
                modelo.NombrePolicia = "No Asignado";
            }
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
                    Armas arma = ConvertirArma(modelo);                
                    if (modelo.PoliciaAsignado != null)
                    {
                        arma.policiaAsignado = policiaDAL.GetPoliciaCedula(modelo.PoliciaAsignado);
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