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
        IPersonaDAL personaDAL;

        public Parte1ViewModel ConvertirParte(Parte1ViewModel model)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            return new Parte1ViewModel
            {
                Fecha = model.Fecha,
                Hora = model.Hora,
                Distrito = model.Distrito,
                LugarSuceso = model.LugarSuceso,
                Barrio = model.Barrio,
                Direccion = model.Direccion,
                IdentificacionInfractor = model.IdentificacionInfractor,
                AprendidoInfractor = model.AprendidoInfractor,
                HoraAprehension = model.HoraAprehension,
                Vestimenta = model.Vestimenta,
                EntendidoInfractor = model.EntendidoInfractor,

                TiposDeIdentificacion = tablaGeneralDAL.Get("Generales", "tipoDeIdentificacion").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo })

            };
        }

        public Parte1ViewModel ConvertirParteRegreso(Parte1ViewModel model)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            return new Parte1ViewModel
            {
                Fecha = model.Fecha,
                Hora = model.Hora,
                Distrito = model.Distrito,
                LugarSuceso = model.LugarSuceso,
                Barrio = model.Barrio,
                Direccion = model.Direccion,
                IdentificacionInfractor = model.IdentificacionInfractor,
                AprendidoInfractor = model.AprendidoInfractor,
                HoraAprehension = model.HoraAprehension,
                Vestimenta = model.Vestimenta,
                EntendidoInfractor = model.EntendidoInfractor,

                TiposLugaresSuceso = tablaGeneralDAL.Get("PartesPoliciales", "lugarSuceso").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                Distritos = tablaGeneralDAL.Get("Generales", "distrito").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                Aprehendidos = tablaGeneralDAL.Get("PartesPoliciales", "aprehendido").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                Entendidos = tablaGeneralDAL.Get("PartesPoliciales", "entendido").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo })
            };
        }

        public Parte1ViewModel ConvertirParte2(Parte1ViewModel model)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            return new Parte1ViewModel
            {
                Fecha = model.Fecha,
                Hora = model.Hora,
                Distrito = model.Distrito,
                LugarSuceso = model.LugarSuceso,
                Barrio = model.Barrio,
                Direccion = model.Direccion,
                IdentificacionInfractor = model.IdentificacionInfractor,
                AprendidoInfractor = model.AprendidoInfractor,
                HoraAprehension = model.HoraAprehension,
                Vestimenta = model.Vestimenta,
                EntendidoInfractor = model.EntendidoInfractor,
                //------------------------------------------------------
                IdentificacionOfendido1 = model.IdentificacionOfendido1,
                NombreOfendido1 = model.NombreOfendido1,
                // IdentificacionOfendido2 = model.IdentificacionOfendido2,
                // NombreOfendido2 = model.NombreOfendido2,
                // IdentificacionOfendido3 = model.IdentificacionOfendido3,
                // NombreOfendido3 = model.NombreOfendido3,
                // IdentificacionOfendido4 = model.IdentificacionOfendido4,
                // NombreOfendido4 = model.NombreOfendido4,
                // IdentificacionOfendido5 = model.IdentificacionOfendido5,
                // NombreOfendido5 = model.NombreOfendido5,
                TipoIdentificacionTestigo1 = model.TipoIdentificacionTestigo1,
                IdentificacionTestigo1 = model.IdentificacionTestigo1,
                NombreTestigo1 = model.NombreTestigo1,

                Alertadores = tablaGeneralDAL.Get("PartesPoliciales", "alertador").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                EntesACargos = tablaGeneralDAL.Get("PartesPoliciales", "enteAcargo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
            };
        }

        public Parte1ViewModel ConvertirParte2Regreso(Parte1ViewModel model)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            return new Parte1ViewModel
            {
                Fecha = model.Fecha,
                Hora = model.Hora,
                Distrito = model.Distrito,
                LugarSuceso = model.LugarSuceso,
                Barrio = model.Barrio,
                Direccion = model.Direccion,
                IdentificacionInfractor = model.IdentificacionInfractor,
                AprendidoInfractor = model.AprendidoInfractor,
                HoraAprehension = model.HoraAprehension,
                Vestimenta = model.Vestimenta,
                EntendidoInfractor = model.EntendidoInfractor,
                //------------------------------------------------------
                IdentificacionOfendido1 = model.IdentificacionOfendido1,
                NombreOfendido1 = model.NombreOfendido1,
                //   IdentificacionOfendido2 = model.IdentificacionOfendido2,
                //  NombreOfendido2 = model.NombreOfendido2,
                //  IdentificacionOfendido3 = model.IdentificacionOfendido3,
                //  NombreOfendido3 = model.NombreOfendido3,
                //  IdentificacionOfendido4 = model.IdentificacionOfendido4,
                //  NombreOfendido4 = model.NombreOfendido4,
                //  IdentificacionOfendido5 = model.IdentificacionOfendido5,
                //  NombreOfendido5 = model.NombreOfendido5,
                TipoIdentificacionTestigo1 = model.TipoIdentificacionTestigo1,
                IdentificacionTestigo1 = model.IdentificacionTestigo1,
                NombreTestigo1 = model.NombreTestigo1,

                TiposDeIdentificacion = tablaGeneralDAL.Get("Generales", "tipoDeIdentificacion").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo })

            };
        }

        public Parte1ViewModel ConvertirParte3(Parte1ViewModel model)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            return new Parte1ViewModel
            {
                Fecha = model.Fecha,
                Hora = model.Hora,
                Distrito = model.Distrito,
                LugarSuceso = model.LugarSuceso,
                Barrio = model.Barrio,
                Direccion = model.Direccion,
                IdentificacionInfractor = model.IdentificacionInfractor,
                AprendidoInfractor = model.AprendidoInfractor,
                HoraAprehension = model.HoraAprehension,
                Vestimenta = model.Vestimenta,
                EntendidoInfractor = model.EntendidoInfractor,
                //------------------------------------------------------
                IdentificacionOfendido1 = model.IdentificacionOfendido1,
                NombreOfendido1 = model.NombreOfendido1,
                TipoIdentificacionTestigo1 = model.TipoIdentificacionTestigo1,
                IdentificacionTestigo1 = model.IdentificacionTestigo1,
                NombreTestigo1 = model.NombreTestigo1,
                DescripcionHechos = model.DescripcionHechos,
                DiligenciasPoliciales = model.DiligenciasPoliciales,
                ManisfestacionOfendido = model.ManisfestacionOfendido,
                ManisfestacionTestigo = model.ManisfestacionTestigo,
                Alertadores = model.Alertadores,
                EnteAcargo = model.EnteAcargo,
                Movil = model.Movil
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

        public List<ListInfractorViewModel> ConvertirListaInfractoresFiltrados(List<Infractores> infractores)
        {
            return (from d in infractores
                    select new ListInfractorViewModel
                    {
                        Identificacion = d.numeroDeIdentificacion,
                        Nombre = d.nombreCompleto,
                    }).ToList();
        }
        public List<ListPersonaViewModel> ConvertirListaPersonasFiltrados(List<Personas> personas)
        {
            return (from d in personas
                    select new ListPersonaViewModel
                    {
                        IdentificacionPersona = d.identificacion,
                        NombrePersona = d.nombre,
                    }).ToList();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Nuevo1()
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            Parte1ViewModel modelo = null;
            try
            {
                if (Session["Parte"] != null)
                {
                    modelo = ConvertirParteRegreso((Parte1ViewModel)Session["Parte"]);

                }
                else
                {

                    modelo = new Parte1ViewModel()
                    {
                        TiposLugaresSuceso = tablaGeneralDAL.Get("PartesPoliciales", "lugarSuceso").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                        Distritos = tablaGeneralDAL.Get("Generales", "distrito").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                        Aprehendidos = tablaGeneralDAL.Get("PartesPoliciales", "aprehendido").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                        Entendidos = tablaGeneralDAL.Get("PartesPoliciales", "entendido").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                        Fecha = DateTime.Today
                    };

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return View(modelo);
        }

        [HttpPost]
        public ActionResult Nuevo1(Parte1ViewModel model)
        {
            Session["Parte"] = model;
            return Redirect("~/Parte/Nuevo2");
        }


        public ActionResult Nuevo2()
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            Parte1ViewModel modelo = null;
            try
            {
                modelo = new Parte1ViewModel()
                {
                    TiposDeIdentificacion = tablaGeneralDAL.Get("Generales", "tipoDeIdentificacion").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo })

                };
                //if (convertirParte2Regreso((Parte1ViewModel)Session["Parte"]) != null)
                //{
                //    modelo = convertirParte2Regreso((Parte1ViewModel)Session["Parte"]);
                //}
                //else
                //{
                //    modelo = convertirParte((Parte1ViewModel)Session["Parte"]);
                //}
                return View(modelo);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult Nuevo2(Parte1ViewModel model)
        {
            Session["Parte"] = model;
            return Redirect("~/Parte/Nuevo3");
        }
        public ActionResult Nuevo3()
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            //Parte1ViewModel modelo = convertirParte2((Parte1ViewModel)Session["Parte"]);
            Parte1ViewModel modelo = new Parte1ViewModel()
            {

                Alertadores = tablaGeneralDAL.Get("PartesPoliciales", "alertador").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                EntesACargos = tablaGeneralDAL.Get("PartesPoliciales", "enteAcargo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo })
            };


            return View(modelo);
        }

        [HttpPost]
        public ActionResult Nuevo3(Parte1ViewModel model)
        {
            Session["Parte"] = model;
            return Redirect("~/Parte/Nuevo4");
        }

        public ActionResult Nuevo4()
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            Parte1ViewModel modelo = new Parte1ViewModel()
            {

            };

            return View(modelo);
        }

        //Métodos vista parciales Infractor

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

        //Métodos vista parciales Persona

        public PartialViewResult ListaPersonasBuscar(string nombre)
        {
            List<ListPersonaViewModel> personas = new List<ListPersonaViewModel>();
            return PartialView("_ListaPersonasBuscar", personas);
        }

        public PartialViewResult ListaPersonasParcial(string nombre)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            personaDAL = new PersonaDAL();
            List<Personas> personas = personaDAL.Get();
            List<Personas> personasFiltrados = new List<Personas>();
            if (nombre == "")
            {
                personasFiltrados = personas;
            }
            else
            {
                foreach (Personas persona in personas)
                {
                    if (persona.nombre.Contains(nombre))
                    {
                        personasFiltrados.Add(persona);
                    }
                }
            }
            personasFiltrados = personasFiltrados.OrderBy(x => x.nombre).ToList();
            return PartialView("_ListaPersonasParcial", ConvertirListaPersonasFiltrados(personasFiltrados));
        }

        public List<Personas> BuscarPersonas(List<Personas> personas, string filtroCedula)
        {
            List<Personas> personasFiltrados = new List<Personas>();
            if (filtroCedula != null)
            {
                foreach (Personas persona in personas)
                {
                    if (persona.identificacion.Contains(filtroCedula))
                    {
                        personasFiltrados.Add(persona);
                    }
                }
                personas = personasFiltrados;
            }
            return personas;
        }

        //Métodos vista parciales Policia

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