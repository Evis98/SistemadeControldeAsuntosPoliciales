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
                        NumeroSerie = d.numeroSerie,
                        TipoArma = tablaGeneralDAL.GetDescripcion(d.tipoArma),
                        NombrePolicia = policiaDAL.GetPolicia(d.policiaAsignado).nombre
                    }).ToList();
        }
        public Armas ConvertirArma(ArmaViewModel modelo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            return new Armas
            {
                idArma = modelo.IdArma,
                policiaAsignado = modelo.PoliciaAsignado,
                numeroSerie = modelo.NumeroSerie,
                tipoArma = modelo.TipoArma,
                marca = modelo.Marca,
                modelo = modelo.Modelo,
                calibre = modelo.Calibre,
                condicion = modelo.Condicion,
                ubicacion = modelo.Ubicacion,
                observacion = modelo.Observacion,
                estadoArma = modelo.EstadoArma,
            };
        }
        public ArmaViewModel CargarArma(Armas arma)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            return new ArmaViewModel
            {
                IdArma = arma.idArma,
                PoliciaAsignado = arma.policiaAsignado,
                NumeroSerie = arma.numeroSerie,
                TipoArma = arma.tipoArma,
                Marca = arma.marca,
                Modelo = arma.modelo,
                Calibre = arma.calibre,
                Condicion = arma.condicion,
                Ubicacion = arma.ubicacion,
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
                PoliciaAsignado = policiaDAL.GetPolicia(arma.policiaAsignado).cedula + " " + policiaDAL.GetPolicia(arma.policiaAsignado).nombre,
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
            try
            {
                if (ModelState.IsValid)
                {
                    armaDAL.Add(ConvertirArma(model));
                    int aux = armaDAL.GetArmaNumSerie(model.NumeroSerie);
                    return Redirect("~/Arma/Detalle/" + aux);
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
            Session["idPolicia"] = id;
            armaDAL = new ArmaDAL();
            ListArmaViewModel modelo = ConvertirArmaInverso(armaDAL.GetArma(id));
            return View(modelo);
        }
        public ActionResult Editar(int id)
        {
            armaDAL = new ArmaDAL();
            ArmaViewModel modelo = CargarArma(armaDAL.GetArma(id));
            modelo.TiposArma = tablaGeneralDAL.GetTiposArma().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            modelo.TiposCalibre = tablaGeneralDAL.GetTiposCalibre().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            modelo.TiposCondicion = tablaGeneralDAL.GetTiposCondicion().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            modelo.TiposUbicacion = tablaGeneralDAL.GetTiposUbicacion().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            return View(modelo);
        }


    }
}