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
            return (from d in armas
                    select new ListArmaViewModel
                    {
                        IdArma = d.idArma,
                        PoliciaAsignado = (int)d.policiaAsignado,
                        NumeroSerie = d.numeroSerie,
                        TipoArma = d.tipoArma,
                        Marca = d.marca,
                        Modelo = d.modelo,
                        Calibre = d.calibre,
                        Condicion = d.condicion,
                        Ubicacion = d.ubicacion,
                        Observacion = d.observacion,
                        EstadoArma = d.estadoArma,
                        NombrePolicia = policiaDAL.GetPolicia((int)d.policiaAsignado).nombre
                    }).ToList();
        }
        public Armas ConvertirArma(ArmaViewModel modelo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            return new Armas
            {
                idArma = modelo.IdArma,
                //policiaAsignado = modelo.PoliciaAsignado,
                numeroSerie = modelo.NumeroSerie,
                //tipoArma = modelo.tipoArma,
                marca = modelo.Marca,
                modelo = modelo.Modelo,
                //calibre = modelo.Calibre,
                //condicion = modelo.Condicion,
                //ubicacion = modelo.Ubicacion,
                observacion = modelo.Observacion,
                //estadoArma = modelo.EstadoArma,
            };
        }

        public ArmaViewModel ConvertirArmaInverso(Armas arma)
        {
            return new ArmaViewModel
            {
                IdArma = arma.idArma,
                PoliciaAsignado = (int)arma.policiaAsignado,
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
            return View();
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
            // Session["idPolicia"] = id;
            armaDAL = new ArmaDAL();
            ArmaViewModel modelo = ConvertirArmaInverso(armaDAL.GetArma(id));
            return View(modelo);
        }



    }
}