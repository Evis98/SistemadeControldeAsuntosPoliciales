using BackEnd;
using BackEnd.DAL;
using FrontEnd.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    public class ParteController : Controller
    {
        ITablaGeneralDAL tablaGeneralDAL;
        IPoliciaDAL policiaDAL;
        IInfractorDAL infractorDAL;


        public List<ListPoliciaViewModel> ConvertirListaPoliciasFiltrados(List<Policias> policias)
        {
            return (from d in policias
                    select new ListPoliciaViewModel
                    {
                        Cedula = d.cedula,
                        Nombre = d.nombre,
                    }).ToList();
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
        public List<ListInfractorViewModel> ConvertirListaInfractoresFiltrados(List<Infractores> infractores)
        {
            return (from d in infractores
                    select new ListInfractorViewModel
                    {
                        Identificacion = d.numeroDeIdentificacion,
                        Nombre = d.nombreCompleto,
                    }).ToList();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Nuevo1()
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            Parte1ViewModel modelo = new Parte1ViewModel()
            {
                TiposLugaresSuceso = tablaGeneralDAL.Get("PartesPoliciales", "lugarSuceso").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                Distritos = tablaGeneralDAL.Get("Generales", "distrito").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                TiposDeIdentificacion = tablaGeneralDAL.Get("Generales", "tipoDeIdentificacion").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                Nacionalidades = tablaGeneralDAL.Get("Generales", "nacionalidad").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                TiposDeSexo = tablaGeneralDAL.Get("Generales", "sexo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                Alertadores = tablaGeneralDAL.Get("PartesPoliciales", "alertador").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                EntesACargos = tablaGeneralDAL.Get("PartesPoliciales", "enteAcargo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                Aprehendidos = tablaGeneralDAL.Get("PartesPoliciales", "aprehendido").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                Entendidos = tablaGeneralDAL.Get("PartesPoliciales", "entendido").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                Fecha = DateTime.Today,
                FechaNacimientoOfendido1 = DateTime.Today,
                FechaNacimientoOfendido2 = DateTime.Today,
                FechaNacimientoOfendido3 = DateTime.Today,
                FechaNacimientoOfendido4 = DateTime.Today,
                FechaNacimientoOfendido5 = DateTime.Today
                
            };
            return View(modelo);
        }
        public PartialViewResult ListaInfractoresBuscar(string nombre)
        {
            List<ListInfractorViewModel> infractores = new List<ListInfractorViewModel>();
            return PartialView("_ListaInfractoresBuscar", infractores);
        }

        public PartialViewResult ListaInfractoresParcial(string nombre)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            infractorDAL = new InfractorDAL();
            List<Infractores> infractores = infractorDAL.Get();
            List<Infractores> infractoresFiltrados = new List<Infractores>();
            if (nombre == "")
            {
                infractoresFiltrados = infractores;
            }
            else
            {
                foreach (Infractores infractor in infractores)
                {
                    if (infractor.nombreCompleto.Contains(nombre))
                    {
                        infractoresFiltrados.Add(infractor);
                    }
                }
            }
            infractoresFiltrados = infractoresFiltrados.OrderBy(x => x.nombreCompleto).ToList();
            return PartialView("_ListaInfractoresParcial", ConvertirListaInfractoresFiltrados(infractoresFiltrados));
        }
        public List<Infractores> BuscarInfractores(List<Infractores> infractores, string filtroCedula)
        {
            List<Infractores> infractoresFiltrados = new List<Infractores>();
            if (filtroCedula != null)
            {
                foreach (Infractores infractor in infractores)
                {
                    if (infractor.numeroDeIdentificacion.Contains(filtroCedula))
                    {
                        infractoresFiltrados.Add(infractor);
                    }
                }
                infractores = infractoresFiltrados;
            }
            return infractores;
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


    }
}