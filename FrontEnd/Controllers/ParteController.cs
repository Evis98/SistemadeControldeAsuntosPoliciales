using BackEnd;
using BackEnd.DAL;
using FrontEnd.Models.ViewModels;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
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
        IParteDAL parteDAL;
        IAuditoriaDAL auditoriaDAL;
        IUsuarioDAL usuarioDAL;

        public bool ActualizarParte(Parte1ViewModel model1, Parte1ViewModel model2)
        {
            if (model1 != null && model2 != null)
            {
                if (model2.Fecha != null)
                {
                    model1.Fecha = model2.Fecha.Date;
                }
                if (model2.Hora != null)
                {
                    model1.Hora = model2.Hora.Date;
                }
                if (model2.Distrito != 0)
                {
                    model1.Distrito = model2.Distrito;
                }
                if (model2.LugarSuceso != 0)
                {
                    model1.LugarSuceso = model2.LugarSuceso;
                }
                if (model2.Barrio != null)
                {
                    model1.Barrio = model2.Barrio;
                }
                if (model2.Direccion != null)
                {
                    model1.Direccion = model2.Direccion;
                }
                if (model2.IdentificacionInfractor != null)
                {
                    model1.IdentificacionInfractor = model2.IdentificacionInfractor;
                }
                if (model2.AprendidoInfractor != 0)
                {
                    model1.AprendidoInfractor = model2.AprendidoInfractor;
                }
                if (model2.HoraAprehension != null)
                {
                    model1.HoraAprehension = model2.HoraAprehension.Date;
                }
                if (model2.Vestimenta != null)
                {
                    model1.Vestimenta = model2.Vestimenta;
                }
                if (model2.EntendidoInfractor != 0)
                {
                    model1.EntendidoInfractor = model2.EntendidoInfractor;
                }
                //------------------------------------------------------
                if (model2.IdentificacionOfendido1 != null)
                {
                    model1.IdentificacionOfendido1 = model2.IdentificacionOfendido1;
                }
                if (model2.NombreOfendido1 != null)
                {
                    model1.NombreOfendido1 = model2.NombreOfendido1;
                }
                if (model2.IdentificacionOfendido2 != null)
                {
                    model1.IdentificacionOfendido2 = model2.IdentificacionOfendido2;
                }
                if (model2.IdentificacionOfendido3 != null)
                {
                    model1.IdentificacionOfendido3 = model2.IdentificacionOfendido3;
                }
                if (model2.IdentificacionOfendido4 != null)
                {
                    model1.IdentificacionOfendido4 = model2.IdentificacionOfendido4;
                }
                if (model2.IdentificacionOfendido5 != null)
                {
                    model1.IdentificacionOfendido5 = model2.IdentificacionOfendido5;
                }
                if (model2.NombreOfendido2 != null)
                {
                    model1.NombreOfendido2 = model2.NombreOfendido2;
                }
                if (model2.NombreOfendido3 != null)
                {
                    model1.NombreOfendido3 = model2.NombreOfendido3;
                }
                if (model2.NombreOfendido4 != null)
                {
                    model1.NombreOfendido4 = model2.NombreOfendido4;
                }
                if (model2.NombreOfendido5 != null)
                {
                    model1.NombreOfendido5 = model2.NombreOfendido5;
                }
                if (model2.TipoIdentificacionTestigo1 != 0)
                {
                    model1.TipoIdentificacionTestigo1 = model2.TipoIdentificacionTestigo1;
                }
                if (model2.IdentificacionTestigo1 != null)
                {
                    model1.IdentificacionTestigo1 = model2.IdentificacionTestigo1;
                }
                if (model2.NombreTestigo1 != null)
                {
                    model1.NombreTestigo1 = model2.NombreTestigo1;
                }
                if (model2.IdentificacionTestigo2 != null)
                {
                    model1.IdentificacionTestigo2 = model2.IdentificacionTestigo2;
                }
                if (model2.NombreTestigo2 != null)
                {
                    model1.NombreTestigo2 = model2.NombreTestigo2;
                }
                if (model2.DescripcionHechos != null)
                {
                    model1.DescripcionHechos = model2.DescripcionHechos;
                }
                if (model2.DiligenciasPoliciales != null)
                {
                    model1.DiligenciasPoliciales = model2.DiligenciasPoliciales;
                }
                if (model2.ManisfestacionOfendido != null)
                {
                    model1.ManisfestacionOfendido = model2.ManisfestacionOfendido;
                }
                if (model2.ManisfestacionTestigo != null)
                {
                    model1.ManisfestacionTestigo = model2.ManisfestacionTestigo;
                }
                if (model2.Alertador != 0)
                {
                    model1.Alertador = model2.Alertador;
                }
                if (model2.EnteAcargo != 0)
                {
                    model1.EnteAcargo = model2.EnteAcargo;
                }
                if (model2.Movil != null)
                {
                    model1.Movil = model2.Movil;
                }
                if (model2.IdentificacionPoliciaActuante != null)
                {
                    model1.IdentificacionPoliciaActuante = model2.IdentificacionPoliciaActuante;
                }
                if (model2.NombrePoliciaActuante != null)
                {
                    model1.NombrePoliciaActuante = model2.NombrePoliciaActuante;
                }
                if (model2.TelefonoPoliciaActuante != null)
                {
                    model1.TelefonoPoliciaActuante = model2.TelefonoPoliciaActuante;
                }
                if (model2.UnidadOrigenPoliciaActuante != 0)
                {
                    model1.UnidadOrigenPoliciaActuante = model2.UnidadOrigenPoliciaActuante;
                }
                if (model2.HoraConfeccionDocumento != null)
                {
                    model1.HoraConfeccionDocumento = model2.HoraConfeccionDocumento.Date;
                }
                if (model2.IdentificacionPoliciaAsiste != null)
                {
                    model1.IdentificacionPoliciaAsiste = model2.IdentificacionPoliciaAsiste;
                }
                if (model2.NombrePoliciaAsiste != null)
                {
                    model1.NombrePoliciaAsiste = model2.NombrePoliciaAsiste;
                }
                if (model2.TelefonoPoliciaAsiste != null)
                {
                    model1.TelefonoPoliciaAsiste = model2.TelefonoPoliciaAsiste;
                }
                if (model2.UnidadOrigenPoliciaAsiste != 0)
                {
                    model1.UnidadOrigenPoliciaAsiste = model2.UnidadOrigenPoliciaAsiste;
                }
                if (model2.NumeroMovilPolciaAsiste != null)
                {
                    model1.NumeroMovilPolciaAsiste = model2.NumeroMovilPolciaAsiste;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public ListParte1ViewModel ConvertirParteInverso(PartesPoliciales parte)
        {
            infractorDAL = new InfractorDAL();
            personaDAL = new PersonaDAL();
            policiaDAL = new PoliciaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            ListParte1ViewModel aux = new ListParte1ViewModel();
            aux.IdParte = parte.idPartepolicial;
            aux.NumeroFolio = parte.numeroFolio;
            aux.Fecha = parte.fecha.Date;
            aux.Hora = parte.horaSuceso;
            aux.Distrito = tablaGeneralDAL.Get(parte.distrito).descripcion;
            aux.LugarSuceso = tablaGeneralDAL.Get(parte.lugarSuceso).descripcion;
            aux.Barrio = parte.barrio;
            aux.Direccion = parte.direccion;
            aux.NombreInfractor = infractorDAL.GetInfractor(parte.idInfractor).nombreCompleto;
            aux.IdentificacionInfractor = infractorDAL.GetInfractor(parte.idInfractor).numeroDeIdentificacion;
            aux.EdadInfractor = parte.edadInfractor;
            aux.AprendidoInfractor = tablaGeneralDAL.Get(parte.aprehendido).descripcion;
            aux.HoraAprehension = parte.horaAprehension.Value;
            aux.Vestimenta = parte.vestimenta;
            aux.EntendidoInfractor = tablaGeneralDAL.Get((int)parte.entendido).descripcion;
            aux.IdOfendido1 = parte.ofendido1;
            aux.NombreOfendido1 = personaDAL.GetPersona((int)parte.ofendido1).nombre;
            aux.IdentificacionOfendido1 = personaDAL.GetPersona((int)parte.ofendido1).identificacion;
            aux.EdadOfendido1 = parte.edadOfendido1;
            if (parte.ofendido2 != null)
            {
                aux.IdOfendido2 = (int)parte.ofendido2;
                aux.NombreOfendido2 = personaDAL.GetPersona((int)parte.ofendido2).nombre;
                aux.IdentificacionOfendido2 = personaDAL.GetPersona((int)parte.ofendido2).identificacion;
                aux.EdadOfendido2 = parte.edadOfendido2;
            }
            if (parte.ofendido3 != null)
            {
                aux.IdOfendido3 = (int)parte.ofendido3;
                aux.NombreOfendido3 = personaDAL.GetPersona((int)parte.ofendido3).nombre;
                aux.IdentificacionOfendido3 = personaDAL.GetPersona((int)parte.ofendido3).identificacion;
                aux.EdadOfendido3 = parte.edadOfendido3;
            }
            if (parte.ofendido4 != null)
            {
                aux.IdOfendido4 = (int)parte.ofendido4;
                aux.NombreOfendido4 = personaDAL.GetPersona((int)parte.ofendido4).nombre;
                aux.IdentificacionOfendido4 = personaDAL.GetPersona((int)parte.ofendido4).identificacion;
                aux.EdadOfendido4 = parte.edadOfendido4;
            }
            if (parte.ofendido5 != null)
            {
                aux.IdOfendido5 = (int)parte.ofendido5;
                aux.NombreOfendido5 = personaDAL.GetPersona((int)parte.ofendido5).nombre;
                aux.IdentificacionOfendido5 = personaDAL.GetPersona((int)parte.ofendido5).identificacion;
                aux.EdadOfendido5 = parte.edadOfendido5;
            }
            if (parte.testigo1 != null)
            {
                aux.IdTestigo1 = (int)parte.testigo1;
                aux.IdentificacionTestigo1 = personaDAL.GetPersona((int)parte.testigo1).identificacion;
                aux.NombreTestigo1 = personaDAL.GetPersona((int)parte.testigo1).nombre;
            }
            if (parte.testigo2 != null)
            {
                aux.IdTestigo2 = (int)parte.testigo2;
                aux.IdentificacionTestigo2 = personaDAL.GetPersona((int)parte.testigo2).identificacion;
                aux.NombreTestigo2 = personaDAL.GetPersona((int)parte.testigo2).nombre;
            }
            aux.DescripcionHechos = parte.descripcionHechos;
            aux.DiligenciasPoliciales = parte.diligenciasPoliciales;
            aux.ManisfestacionOfendido = parte.manifestacionOfendido;
            aux.ManisfestacionTestigo = parte.manifestacionTestigo;
            aux.Alertador = tablaGeneralDAL.Get(parte.alertador).descripcion;
            aux.EnteAcargo = tablaGeneralDAL.Get(parte.enteAcargo).descripcion;
            aux.Movil = parte.movil;
            aux.NombrePoliciaActuante = policiaDAL.GetPolicia(parte.idPoliciaActuante).nombre;
            aux.IdentificacionPoliciaActuante = policiaDAL.GetPolicia(parte.idPoliciaActuante).cedula;
            aux.UnidadOrigenPoliciaActuante = tablaGeneralDAL.Get(parte.unidadOrigenPoliciaActuante).descripcion;
            aux.TelefonoPoliciaActuante = policiaDAL.GetPolicia(parte.idPoliciaActuante).telefonoCelular;
            aux.HoraConfeccionDocumento = parte.horaConfeccionDocumento;
            aux.NombrePoliciaAsiste = parte.nombreOficialAsistente;
            aux.IdentificacionPoliciaAsiste = parte.identificacionOficialAsistente;
            aux.UnidadOrigenPoliciaAsiste = tablaGeneralDAL.Get(parte.unidadOrigenPoliciaAsistente).descripcion;
            aux.TelefonoPoliciaAsiste = parte.telefonoOficialAsistente;

            return aux;

        }

        public List<ListParte1ViewModel> ConvertirListaPartes(List<PartesPoliciales> partes)
        {
            infractorDAL = new InfractorDAL();
            personaDAL = new PersonaDAL();
            policiaDAL = new PoliciaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            List<ListParte1ViewModel> modelo = new List<ListParte1ViewModel>();
            foreach (PartesPoliciales parte in partes)
            {
                ListParte1ViewModel aux = new ListParte1ViewModel();
                {
                    aux.IdParte = parte.idPartepolicial;
                    aux.NumeroFolio = parte.numeroFolio;
                    aux.Fecha = parte.fecha.Date;
                    aux.Hora = parte.horaSuceso;
                    aux.Distrito = tablaGeneralDAL.Get(parte.distrito).descripcion;
                    aux.LugarSuceso = tablaGeneralDAL.Get(parte.lugarSuceso).descripcion;
                    aux.Barrio = parte.barrio;
                    aux.Direccion = parte.direccion;
                    aux.NombreInfractor = infractorDAL.GetInfractor(parte.idInfractor).nombreCompleto;
                    aux.IdentificacionInfractor = infractorDAL.GetInfractor(parte.idInfractor).numeroDeIdentificacion;
                    aux.EdadInfractor = parte.edadInfractor;
                    aux.AprendidoInfractor = tablaGeneralDAL.Get(parte.aprehendido).descripcion;
                    aux.HoraAprehension = parte.horaAprehension.Value;
                    aux.Vestimenta = parte.vestimenta;
                    aux.EntendidoInfractor = tablaGeneralDAL.Get((int)parte.entendido).descripcion;
                    aux.NombreOfendido1 = personaDAL.GetPersona((int)parte.ofendido1).nombre;
                    aux.IdentificacionOfendido1 = personaDAL.GetPersona((int)parte.ofendido1).identificacion;
                    aux.EdadOfendido1 = parte.edadOfendido1;
                    if (parte.ofendido2 != null)
                    {
                        aux.NombreOfendido2 = personaDAL.GetPersona((int)parte.ofendido2).nombre;
                        aux.IdentificacionOfendido2 = personaDAL.GetPersona((int)parte.ofendido2).identificacion;
                        aux.EdadOfendido2 = parte.edadOfendido2;
                    }
                    if (parte.ofendido3 != null)
                    {
                        aux.NombreOfendido3 = personaDAL.GetPersona((int)parte.ofendido3).nombre;
                        aux.IdentificacionOfendido3 = personaDAL.GetPersona((int)parte.ofendido3).identificacion;
                        aux.EdadOfendido3 = parte.edadOfendido3;
                    }
                    if (parte.ofendido4 != null)
                    {
                        aux.NombreOfendido4 = personaDAL.GetPersona((int)parte.ofendido4).nombre;
                        aux.IdentificacionOfendido4 = personaDAL.GetPersona((int)parte.ofendido4).identificacion;
                        aux.EdadOfendido4 = parte.edadOfendido4;
                    }
                    if (parte.ofendido5 != null)
                    {
                        aux.NombreOfendido5 = personaDAL.GetPersona((int)parte.ofendido5).nombre;
                        aux.IdentificacionOfendido5 = personaDAL.GetPersona((int)parte.ofendido5).identificacion;
                        aux.EdadOfendido5 = parte.edadOfendido5;
                    }
                    if (parte.testigo1 != null)
                    {
                        aux.IdentificacionTestigo1 = personaDAL.GetPersona((int)parte.testigo1).identificacion;
                    }
                    if (parte.testigo2 != null)
                    {
                        aux.IdentificacionTestigo2 = personaDAL.GetPersona((int)parte.testigo2).identificacion;
                    }
                    aux.DescripcionHechos = parte.descripcionHechos;
                    aux.DiligenciasPoliciales = parte.diligenciasPoliciales;
                    aux.ManisfestacionOfendido = parte.manifestacionOfendido;
                    aux.ManisfestacionTestigo = parte.manifestacionTestigo;
                    aux.Alertador = tablaGeneralDAL.Get(parte.alertador).descripcion;
                    aux.EnteAcargo = tablaGeneralDAL.Get(parte.enteAcargo).descripcion; ;
                    aux.Movil = parte.movil;
                    aux.NombrePoliciaActuante = policiaDAL.GetPolicia(parte.idPoliciaActuante).nombre;
                    aux.IdentificacionPoliciaActuante = policiaDAL.GetPolicia(parte.idPoliciaActuante).cedula;
                    aux.UnidadOrigenPoliciaActuante = tablaGeneralDAL.Get(parte.unidadOrigenPoliciaActuante).descripcion;
                    aux.TelefonoPoliciaActuante = policiaDAL.GetPolicia(parte.idPoliciaActuante).telefonoCelular;
                    aux.HoraConfeccionDocumento = parte.horaConfeccionDocumento;
                    aux.NombrePoliciaAsiste = parte.nombreOficialAsistente;
                    aux.IdentificacionPoliciaAsiste = parte.identificacionOficialAsistente;
                    aux.UnidadOrigenPoliciaAsiste = tablaGeneralDAL.Get(parte.unidadOrigenPoliciaAsistente).descripcion;
                    aux.NumeroMovilPolciaAsiste = parte.movilAsistente;
                    aux.TelefonoPoliciaAsiste = parte.telefonoOficialAsistente;
                }
                modelo.Add(aux);
            }

            return modelo;

        }

        public PartesPoliciales ConvertirParte(Parte1ViewModel model)
        {
            policiaDAL = new PoliciaDAL();
            infractorDAL = new InfractorDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            personaDAL = new PersonaDAL();
            parteDAL = new ParteDAL();
            PartesPoliciales parte = new PartesPoliciales();
            //parte.idPartepolicial = model.IdPartePolcial;
            parte.fecha = DateTime.Today;
            parte.provincia = "Alajuela";
            parte.canton = "Alajuela";
            parte.horaSuceso = DateTime.Now;
            parte.distrito = tablaGeneralDAL.GetCodigo("Generales", "distrito", model.Distrito.ToString()).idTablaGeneral;
            parte.lugarSuceso = tablaGeneralDAL.GetCodigo("PartesPoliciales", "lugarSuceso", model.LugarSuceso.ToString()).idTablaGeneral;
            parte.barrio = model.Barrio;
            parte.direccion = model.Direccion;
            parte.idInfractor = infractorDAL.GetInfractorIdentificacion(model.IdentificacionInfractor).idInfractor;
            parte.edadInfractor = ObtenerEdad(infractorDAL.GetInfractorIdentificacion(model.IdentificacionInfractor).fechaNacimiento);
            parte.aprehendido = tablaGeneralDAL.GetCodigo("PartesPoliciales", "aprehendido", model.AprendidoInfractor.ToString()).idTablaGeneral;
            parte.horaAprehension = DateTime.Now; //null
            parte.vestimenta = model.Vestimenta;
            parte.entendido = tablaGeneralDAL.GetCodigo("PartesPoliciales", "entendido", model.EntendidoInfractor.ToString()).idTablaGeneral;

            parte.ofendido1 = personaDAL.GetPersonaIdentificacion(model.IdentificacionOfendido1).idPersona;
            parte.edadOfendido1 = ObtenerEdad(personaDAL.GetPersonaIdentificacion(model.IdentificacionOfendido1).fechaNacimiento);
            if (model.IdentificacionOfendido2 != null)
            {
                parte.ofendido2 = personaDAL.GetPersonaIdentificacion(model.IdentificacionOfendido2).idPersona;
                parte.edadOfendido2 = ObtenerEdad(personaDAL.GetPersonaIdentificacion(model.IdentificacionOfendido2).fechaNacimiento);
            }
            if (model.IdentificacionOfendido3 != null)
            {
                parte.ofendido3 = personaDAL.GetPersonaIdentificacion(model.IdentificacionOfendido3).idPersona;
                parte.edadOfendido3 = ObtenerEdad(personaDAL.GetPersonaIdentificacion(model.IdentificacionOfendido3).fechaNacimiento);
            }
            if (model.IdentificacionOfendido4 != null)
            {
                parte.ofendido4 = personaDAL.GetPersonaIdentificacion(model.IdentificacionOfendido4).idPersona;
                parte.edadOfendido4 = ObtenerEdad(personaDAL.GetPersonaIdentificacion(model.IdentificacionOfendido4).fechaNacimiento);
            }
            if (model.IdentificacionOfendido5 != null)
            {
                parte.ofendido5 = personaDAL.GetPersonaIdentificacion(model.IdentificacionOfendido5).idPersona;
                parte.edadOfendido5 = ObtenerEdad(personaDAL.GetPersonaIdentificacion(model.IdentificacionOfendido5).fechaNacimiento);
            }
            if (model.IdentificacionTestigo1 != null)
            {
                parte.testigo1 = personaDAL.GetPersonaIdentificacion(model.IdentificacionTestigo1).idPersona;
            }  //null
            if (model.IdentificacionTestigo2 != null)
            {
                parte.testigo2 = personaDAL.GetPersonaIdentificacion(model.IdentificacionTestigo2).idPersona;
            }
            parte.descripcionHechos = model.DescripcionHechos;
            parte.diligenciasPoliciales = model.DiligenciasPoliciales;
            parte.manifestacionOfendido = model.ManisfestacionOfendido;
            parte.manifestacionTestigo = model.ManisfestacionTestigo;
            parte.alertador = tablaGeneralDAL.GetCodigo("PartesPoliciales", "alertador", model.Alertador.ToString()).idTablaGeneral;
            parte.enteAcargo = tablaGeneralDAL.GetCodigo("PartesPoliciales", "enteAcargo", model.EnteAcargo.ToString()).idTablaGeneral;
            parte.movil = model.Movil;
            parte.idPoliciaActuante = policiaDAL.GetPoliciaCedula(model.IdentificacionPoliciaActuante).idPolicia;
            parte.nombreOficialAsistente = model.NombrePoliciaAsiste;
            parte.identificacionOficialAsistente = model.IdentificacionPoliciaAsiste;
            parte.telefonoOficialAsistente = model.TelefonoPoliciaAsiste;
            parte.unidadOrigenPoliciaActuante = tablaGeneralDAL.GetCodigo("PartesPoliciales", "enteAcargo", "1").idTablaGeneral;
            parte.horaConfeccionDocumento = DateTime.Now;
            //parte.unidadOrigenPoliciaAsistente = tablaGeneralDAL.GetCodigo("PartesPoliciales", "enteAcargo", "2").idTablaGeneral;
            parte.unidadOrigenPoliciaAsistente = tablaGeneralDAL.GetCodigo("PartesPoliciales", "enteAcargo", model.UnidadOrigenPoliciaAsiste.ToString()).idTablaGeneral;
            parte.movilAsistente = model.NumeroMovilPolciaAsiste;
            return parte;
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

        public List<InfractorViewModel> ConvertirListaInfractoresFiltrados(List<Infractores> infractores)
        {
            return (from d in infractores
                    select new InfractorViewModel
                    {
                        Identificacion = d.numeroDeIdentificacion,
                        Nombre = d.nombreCompleto,
                    }).ToList();
        }
        public List<PersonaViewModel> ConvertirListaPersonasFiltrados(List<Personas> personas)
        {
            return (from d in personas
                    select new PersonaViewModel
                    {
                        Identificacion = d.identificacion,
                        NombrePersona = d.nombre,
                    }).ToList();
        }

        public void Autorizar()
        {
            if (Session["userID"] != null)
            {
                if (Session["Rol"].ToString() == "4")
                {
                    Session["Error"] = "Usuario no autorizado";
                    Response.Redirect("~/Error/ErrorUsuario.cshtml");
                }
            }
            else
            {
                Response.Redirect("~/Login/Index");
            }
        }
       

        public ActionResult Index(string filtrosSeleccionado, string busqueda, string busquedaFechaInicioP, string busquedaFechaFinalP)
        {
            Autorizar();
            parteDAL = new ParteDAL();
            infractorDAL = new InfractorDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            List<PartesPoliciales> partes = parteDAL.Get();
            List<PartesPoliciales> partesFiltrados = new List<PartesPoliciales>();
            List<TablaGeneral> comboindex = tablaGeneralDAL.Get("PartesPoliciales", "index");
            List<SelectListItem> items = comboindex.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.descripcion
                };
            });
            ViewBag.items = items;
            if (busqueda != null)
            {
                foreach (PartesPoliciales parte in partes)
                {
                    if (filtrosSeleccionado == "Número de Folio")
                    {
                        if (parte.numeroFolio.Contains(busqueda))
                        {
                            partesFiltrados.Add(parte);
                        }
                    }
                    if (filtrosSeleccionado == "Nombre del Infractor")
                    {
                        if (infractorDAL.GetInfractor(parte.idInfractor).nombreCompleto.Contains(busqueda.ToUpper()))
                        {
                            partesFiltrados.Add(parte);
                        }
                    }
                }
                if (filtrosSeleccionado == "Fecha")
                {
                    DateTime fechaInicio = DateTime.Parse(busquedaFechaInicioP);
                    DateTime fechaFinal = DateTime.Parse(busquedaFechaFinalP);
                    if (fechaInicio < fechaFinal)
                    {
                        if (parteDAL.GetPartesRango(fechaInicio, fechaFinal) != null)
                        {
                            foreach (PartesPoliciales parteFecha in parteDAL.GetPartesRango(fechaInicio, fechaFinal).ToList())
                            {
                                partesFiltrados.Add(parteFecha);
                            }
                        }
                    }
                }

                partes = partesFiltrados;
            }
            partes = partes.OrderBy(x => x.numeroFolio).ToList();
            return View(ConvertirListaPartes(partes));
        }
           
        public ActionResult Nuevo1()
        {
            Autorizar();
            tablaGeneralDAL = new TablaGeneralDAL();
            Parte1ViewModel modelo = null;

            try
            {
                if (Session["Parte"] != null)
                {
                    ActualizarParte(modelo, (Parte1ViewModel)Session["Parte"]);
                    modelo = (Parte1ViewModel)Session["Parte"];
                    modelo.TiposLugaresSuceso = tablaGeneralDAL.Get("PartesPoliciales", "lugarSuceso").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                    modelo.Distritos = tablaGeneralDAL.Get("Generales", "distrito").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                    modelo.Aprehendidos = tablaGeneralDAL.Get("PartesPoliciales", "aprehendido").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                    modelo.Entendidos = tablaGeneralDAL.Get("PartesPoliciales", "entendido").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });

                }
                else
                {

                    modelo = new Parte1ViewModel()
                    {
                        TiposLugaresSuceso = tablaGeneralDAL.Get("PartesPoliciales", "lugarSuceso").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                        Distritos = tablaGeneralDAL.Get("Generales", "distrito").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                        Aprehendidos = tablaGeneralDAL.Get("PartesPoliciales", "aprehendido").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                        Entendidos = tablaGeneralDAL.Get("PartesPoliciales", "entendido").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                        Fecha = DateTime.Today,
                        Hora = DateTime.Now,
                        HoraAprehension = DateTime.Now,
                        HoraConfeccionDocumento = DateTime.Now
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
            Autorizar();
            if (Session["Parte"] != null)
            {
                Parte1ViewModel modelAux = (Parte1ViewModel)Session["Parte"];
                if (ActualizarParte(modelAux, model))
                {
                    Session["Parte"] = modelAux;
                }
                return Redirect("~/Parte/Nuevo2");
            }
            else
            {
                Session["Parte"] = model;
                return Redirect("~/Parte/Nuevo2");
            }

        }


        public ActionResult Nuevo2()
        {
            Autorizar();
            tablaGeneralDAL = new TablaGeneralDAL();
            Parte1ViewModel modelo = null;
            try
            {
                if ((Parte1ViewModel)Session["Parte"] != null)
                {
                    modelo = (Parte1ViewModel)Session["Parte"];
                    modelo.TiposDeIdentificacion = tablaGeneralDAL.Get("Generales", "tipoDeIdentificacion").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                }
                else
                {
                    modelo = new Parte1ViewModel();
                    modelo.TiposDeIdentificacion = tablaGeneralDAL.Get("Generales", "tipoDeIdentificacion").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                }
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
            Autorizar();
            if (Session["Parte"] != null)
            {
                Parte1ViewModel modelAux = (Parte1ViewModel)Session["Parte"];
                if (ActualizarParte(modelAux, model))
                {
                    Session["Parte"] = modelAux;
                }
                return Redirect("~/Parte/Nuevo3");
            }
            else
            {
                Session["Parte"] = model;
                return Redirect("~/Parte/Nuevo3");
            }
        }
        public ActionResult Nuevo3()
        {
            Autorizar();
            try
            {
                tablaGeneralDAL = new TablaGeneralDAL();
                Parte1ViewModel modelo = null;
                if (Session["Parte"] != null)
                {
                    modelo = (Parte1ViewModel)Session["Parte"];
                    modelo.Alertadores = tablaGeneralDAL.Get("PartesPoliciales", "alertador").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                    modelo.EntesACargos = tablaGeneralDAL.Get("PartesPoliciales", "enteAcargo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                }
                else
                {
                    modelo = new Parte1ViewModel()
                    {

                        Alertadores = tablaGeneralDAL.Get("PartesPoliciales", "alertador").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                        EntesACargos = tablaGeneralDAL.Get("PartesPoliciales", "enteAcargo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo })
                    };
                }
                return View(modelo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Nuevo3(Parte1ViewModel model)
        {
            Autorizar();
            if (Session["Parte"] != null)
            {
                Parte1ViewModel modelAux = (Parte1ViewModel)Session["Parte"];
                if (ActualizarParte(modelAux, model))
                {
                    Session["Parte"] = modelAux;
                }
                return Redirect("~/Parte/Nuevo4");
            }
            else
            {
                Session["Parte"] = model;
                return Redirect("~/Parte/Nuevo4");
            }
        }

        public ActionResult Nuevo4()
        {
            Autorizar();
            try
            {
                Parte1ViewModel modelo = null;
                tablaGeneralDAL = new TablaGeneralDAL();
                if (Session["Parte"] != null)
                {
                    modelo = (Parte1ViewModel)Session["Parte"];
                    modelo.UnidadesDeOrigen = tablaGeneralDAL.Get("PartesPoliciales", "enteAcargo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                }
                else
                {
                    modelo = new Parte1ViewModel();
                    modelo.UnidadesDeOrigen = tablaGeneralDAL.Get("PartesPoliciales", "enteAcargo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                }
                return View(modelo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }
        [HttpPost]
        public ActionResult Nuevo4(Parte1ViewModel model)
        {
            Autorizar();
            tablaGeneralDAL = new TablaGeneralDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            parteDAL = new ParteDAL();
            AuditoriaViewModel auditoria_modelo = new AuditoriaViewModel();
            try
            {

                if (Session["Parte"] != null)
                {
                    Parte1ViewModel modelAux = (Parte1ViewModel)Session["Parte"];
                    if (ActualizarParte(modelAux, model))
                    {
                        PartesPoliciales parte = ConvertirParte(modelAux);
                        parte.numeroFolio = (parteDAL.GetCount(parte.fecha.Date) + 1).ToString() + "-" + parte.fecha.Date.Year;
                        parteDAL.Add(parte);
                        auditoria_modelo.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "1").idTablaGeneral;
                        auditoria_modelo.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "15").idTablaGeneral;
                        auditoria_modelo.IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario;
                        auditoria_modelo.IdElemento = parteDAL.GetPartePolicial(parte.numeroFolio).idPartepolicial;
                        auditoriaDAL.Add(ConvertirAuditoria(auditoria_modelo));
                        int aux = parteDAL.GetPartePolicial(parte.numeroFolio).idPartepolicial;
                        return Redirect("~/Parte/Detalle1/" + aux);
                    }
                    Session["Parte"] = null;
                    return Redirect("~/Parte/Index");

                }
                else
                {
                    Session["Parte"] = null;
                    return Redirect("~/Parte/Index");
                }

            }
            catch (Exception ex)
            {
              
                throw new Exception(ex.Message);
            }
        }

        public PartialViewResult ListaInfractoresBuscar(string nombre)
        {
            List<InfractorViewModel> infractores = new List<InfractorViewModel>();
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
                    if (infractor.nombreCompleto.Contains(nombre.ToUpper()))
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

        public PartialViewResult ListaPersonasBuscar(string nombre)
        {
            List<PersonaViewModel> personas = new List<PersonaViewModel>();
            return PartialView("_ListaPersonasBuscar", personas);
        }

        public PartialViewResult ListaPersonasParcial(string nombre)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            personaDAL = new PersonaDAL();
            List<Personas> personas = personaDAL.Get();
            List<Personas> personasFiltrados = new List<Personas>();
            foreach (Personas persona in personas)
            {
                if (persona.nombre.Contains(nombre.ToUpper()))
                {
                    if (tablaGeneralDAL.Get(persona.tipoIdentificacion).descripcion != "Cédula Jurídica")
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
                    if (policia.nombre.Contains(nombre.ToUpper()))
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

        string ObtenerEdad(DateTime? fechaNacimiento)
        {

            var edad = DateTime.Today.Year - fechaNacimiento.Value.Year;

            if (fechaNacimiento.Value.Date > DateTime.Today.AddYears(-edad)) edad--;

            return edad.ToString();

        }

        public ActionResult Detalle1(int id)
        {
            Autorizar();
            parteDAL = new ParteDAL();
            Session["idParte"] = id;
            Session["auditoria"] = parteDAL.GetParte(id).numeroFolio;
            Session["tabla"] = "Parte Policial";
            ListParte1ViewModel modelo = ConvertirParteInverso(parteDAL.GetParte(id));

            return View(modelo);
        }

        public ActionResult Detalle2(int id)
        {
            Autorizar();
            Session["idParte"] = id;
            parteDAL = new ParteDAL();
            ListParte1ViewModel modelo = ConvertirParteInverso(parteDAL.GetParte(id));
            return View(modelo);
        }

        public ActionResult Detalle3(int id)
        {
            Autorizar();
            Session["idParte"] = id;
            parteDAL = new ParteDAL();
            ListParte1ViewModel modelo = ConvertirParteInverso(parteDAL.GetParte(id));
            return View(modelo);
        }

        public ActionResult Detalle4(int id)
        {
            Autorizar();
            parteDAL = new ParteDAL();
            Session["idParte"] = id;
            Session["numeroFolio"] = parteDAL.GetParte(id).numeroFolio;
            ListParte1ViewModel modelo = ConvertirParteInverso(parteDAL.GetParte(id));
            return View(modelo);
        }

        public Auditorias ConvertirAuditoria(AuditoriaViewModel modelo)
        {
            return new Auditorias
            {
                idAuditoria = modelo.IdAuditoria,
                idCategoria = modelo.IdCategoria,
                idElemento = modelo.IdElemento,
                fecha = DateTime.Now,
                accion = modelo.Accion,
                idUsuario = modelo.IdUsuario,
            };

        }
        public void CreatePDF(int id)
        {
            //--------------------------Creacion de los DataSet--------------------------
            infractorDAL = new InfractorDAL();
            parteDAL = new ParteDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            personaDAL = new PersonaDAL();

            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = Path.Combine(Server.MapPath("~/PDFs"), "ReportePartePolicial.rdlc");

            //Parte
            PartesPoliciales parte = parteDAL.GetParte(id);
            List<PartesPoliciales> partes = new List<PartesPoliciales>();
            partes.Add(parte);

            //Policia
            Policias policiaActuante = policiaDAL.GetPolicia(parte.idPoliciaActuante);
            List<Policias> policiasActuantes = new List<Policias>();
            policiasActuantes.Add(policiaActuante);

            //Personas
            Personas ofendido1 = personaDAL.GetPersona(parte.ofendido1);
            List<Personas> ofendidos1 = new List<Personas>();
            if (ofendido1.direccionPersona == null)
            {
                ofendido1.direccionPersona = "No aplica";
            }
            if (ofendido1.telefonoCelular == null)
            {
                ofendido1.telefonoCelular = "No aplica";
            }
            if (ofendido1.profesion == null)
            {
                ofendido1.profesion = "No aplica";
            }
            if (ofendido1.correoElectronicoPersona == null)
            {
                ofendido1.correoElectronicoPersona = "No aplica";
            }
            ofendido1.lugarTrabajoPersona = ObtenerEdad(ofendido1.fechaNacimiento);
            ofendidos1.Add(ofendido1);

            //Infractor
            Infractores infractor = infractorDAL.GetInfractor(parte.idInfractor);
            List<Infractores> infractores = new List<Infractores>();
            if (infractor.apodoInfractor == null)
            {
                infractor.apodoInfractor = "No aplica";
            }
            if (infractor.rasgosFisicosInfractor == null)
            {
                infractor.rasgosFisicosInfractor = "No aplica";
            }
            if (infractor.correoEletronico == null)
            {
                infractor.correoEletronico = "No aplica";
            }
            if (infractor.observaciones == null)
            {
                infractor.observaciones = "No aplica";
            }
            infractor.nombreDeLaMadre = ObtenerEdad(infractor.fechaNacimiento);
            infractores.Add(infractor);

            //TablaGeneral
            TablaGeneral distrito = new TablaGeneral();
            List<TablaGeneral> distritos = new List<TablaGeneral>();
            distrito.descripcion = tablaGeneralDAL.Get(parte.distrito).descripcion;
            distritos.Add(distrito);

            TablaGeneral lugarS = new TablaGeneral();
            List<TablaGeneral> lugaresS = new List<TablaGeneral>();
            lugarS.descripcion = tablaGeneralDAL.Get(parte.lugarSuceso).descripcion;
            lugaresS.Add(lugarS);

            TablaGeneral aprehendido = new TablaGeneral();
            List<TablaGeneral> aprehendidos = new List<TablaGeneral>();
            aprehendido.descripcion = tablaGeneralDAL.Get(parte.aprehendido).descripcion;
            aprehendidos.Add(aprehendido);

            TablaGeneral entendido = new TablaGeneral();
            List<TablaGeneral> entendidos = new List<TablaGeneral>();
            entendido.descripcion = tablaGeneralDAL.Get((int)parte.entendido).descripcion;
            entendidos.Add(entendido);

            TablaGeneral alertador = new TablaGeneral();
            List<TablaGeneral> alertadores = new List<TablaGeneral>();
            alertador.descripcion = tablaGeneralDAL.Get(parte.alertador).descripcion;
            alertadores.Add(alertador);

            TablaGeneral enteACargo = new TablaGeneral();
            List<TablaGeneral> entesACargo = new List<TablaGeneral>();
            enteACargo.descripcion = tablaGeneralDAL.Get(parte.enteAcargo).descripcion;
            entesACargo.Add(enteACargo);

            //Sexo y nacionalidad del imputado
            TablaGeneral sexo = new TablaGeneral();
            List<TablaGeneral> sexos = new List<TablaGeneral>();
            sexo.descripcion = tablaGeneralDAL.Get(infractor.sexo).descripcion;
            sexos.Add(sexo);

            TablaGeneral nacionalidad = new TablaGeneral();
            List<TablaGeneral> nacionalidades = new List<TablaGeneral>();
            nacionalidad.descripcion = tablaGeneralDAL.Get(infractor.nacionalidad).descripcion;
            nacionalidades.Add(nacionalidad);

            //Sexo y nacionalidad del ofendido 1
            TablaGeneral sexoO1 = new TablaGeneral();
            List<TablaGeneral> sexosO1 = new List<TablaGeneral>();
            sexoO1.descripcion = tablaGeneralDAL.Get((int)ofendido1.sexo).descripcion;
            sexosO1.Add(sexoO1);

            viewer.LocalReport.DataSources.Add(new ReportDataSource("SexoOfendido1DataSet", sexosO1));

            TablaGeneral nacionalidadO1 = new TablaGeneral();
            List<TablaGeneral> nacionalidadesO1 = new List<TablaGeneral>();
            nacionalidadO1.descripcion = tablaGeneralDAL.Get((int)ofendido1.nacionalidad).descripcion;
            nacionalidadesO1.Add(nacionalidadO1);

            viewer.LocalReport.DataSources.Add(new ReportDataSource("NacionalidadOfendido1DataSet", nacionalidadesO1));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("ParteDataSet", partes));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("ActuanteDataSet", policiasActuantes));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("Ofendido1DataSet", ofendidos1));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("DistritoDataSet", distritos));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("LugarSucesoDataSet", lugaresS));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("FueAprehendidoDataSet", aprehendidos));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("EntendidoDataSet", entendidos));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("AlertadorDataSet", alertadores));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("EnteACargoDataSet", entesACargo));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("InfractorDataSet", infractores));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("SexoInfractorDataSet", sexos));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("NacionalidadInfractorDataSet", nacionalidades));

            //Testigo 1
            if (parte.testigo1 == null)
            {
                Personas testigo1 = new Personas();
                List<Personas> testigos1 = new List<Personas>();
                testigos1.Add(testigo1);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("Testigo1DataSet", testigos1));

                TablaGeneral testigonulo = new TablaGeneral();
                List<TablaGeneral> testigonulos = new List<TablaGeneral>();
                testigonulo.descripcion = "No aplica";
                testigonulos.Add(testigonulo);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("TestigoNuloDataSet", testigonulos));
            }
            else
            {
                Personas testigo1 = personaDAL.GetPersona((int)parte.testigo1);
                List<Personas> testigos1 = new List<Personas>();
                if (testigo1.direccionPersona == null)
                {
                    testigo1.direccionPersona = "No aplica";
                }
                if (testigo1.telefonoCelular == null)
                {
                    testigo1.telefonoCelular = "No aplica";
                }
                if (testigo1.profesion == null)
                {
                    testigo1.profesion = "No aplica";
                }
                if (testigo1.correoElectronicoPersona == null)
                {
                    testigo1.correoElectronicoPersona = "No aplica";
                }
                if (testigo1.lugarTrabajoPersona == null)
                {
                    testigo1.lugarTrabajoPersona = "No aplica";
                }
                testigos1.Add(testigo1);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("Testigo1DataSet", testigos1));

                TablaGeneral testigonulo = new TablaGeneral();
                List<TablaGeneral> testigonulos = new List<TablaGeneral>();
                testigonulos.Add(testigonulo);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("TestigoNuloDataSet", testigonulos));
            }

            //Testigo 2
            if (parte.testigo2 == null)
            {
                Personas testigo2 = new Personas();
                List<Personas> testigos2 = new List<Personas>();
                testigos2.Add(testigo2);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("Testigo2DataSet", testigos2));

                TablaGeneral testigo2nulo = new TablaGeneral();
                List<TablaGeneral> testigo2nulos = new List<TablaGeneral>();
                testigo2nulo.descripcion = "No aplica";
                testigo2nulos.Add(testigo2nulo);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("TestigoNulo2DataSet", testigo2nulos));
            }
            else
            {
                Personas testigo2 = personaDAL.GetPersona((int)parte.testigo2);
                List<Personas> testigos2 = new List<Personas>();
                if (testigo2.direccionPersona == null)
                {
                    testigo2.direccionPersona = "No aplica";
                }
                if (testigo2.telefonoCelular == null)
                {
                    testigo2.telefonoCelular = "No aplica";
                }
                if (testigo2.profesion == null)
                {
                    testigo2.profesion = "No aplica";
                }
                if (testigo2.correoElectronicoPersona == null)
                {
                    testigo2.correoElectronicoPersona = "No aplica";
                }
                if (testigo2.lugarTrabajoPersona == null)
                {
                    testigo2.lugarTrabajoPersona = "No aplica";
                }

                testigos2.Add(testigo2);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("Testigo2DataSet", testigos2));

                TablaGeneral testigo2nulo = new TablaGeneral();
                List<TablaGeneral> testigo2nulos = new List<TablaGeneral>();
                testigo2nulos.Add(testigo2nulo);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("TestigoNulo2DataSet", testigo2nulos));
            }

            //Ofendido 2
            if (parte.ofendido2 == null)
            {
                Personas ofendido2 = new Personas();
                List<Personas> ofendidos2 = new List<Personas>();
                if (ofendido2.fechaNacimiento == DateTime.MinValue)
                {
                    ofendido2.lugarTrabajoPersona = null;
                }
                ofendidos2.Add(ofendido2);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("Ofendido2DataSet", ofendidos2));

                TablaGeneral sexoO2 = new TablaGeneral();
                List<TablaGeneral> sexosO2 = new List<TablaGeneral>();
                sexosO2.Add(sexoO2);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("SexoOfendido2DataSet", sexosO2));

                TablaGeneral nacionalidadO2 = new TablaGeneral();
                List<TablaGeneral> nacionalidadesO2 = new List<TablaGeneral>();
                nacionalidadesO2.Add(nacionalidadO2);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("NacionalidadOfendido2DataSet", nacionalidadesO2));

                TablaGeneral ofendido2nulo = new TablaGeneral();
                List<TablaGeneral> ofendido2nulos = new List<TablaGeneral>();
                ofendido2nulo.descripcion = "No aplica";
                ofendido2nulos.Add(ofendido2nulo);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("Ofendido2NuloDataSet", ofendido2nulos));

                TablaGeneral edadO2 = new TablaGeneral();
                List<TablaGeneral> edadesO2 = new List<TablaGeneral>();
                edadO2.descripcion = "No aplica";
                edadesO2.Add(edadO2);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("EdadOfendido2DataSet", edadesO2));
            }
            else
            {
                Personas ofendido2 = personaDAL.GetPersona((int)parte.ofendido2);
                List<Personas> ofendidos2 = new List<Personas>();
                if (ofendido2.direccionPersona == null)
                {
                    ofendido2.direccionPersona = "No aplica";
                }
                if (ofendido2.telefonoCelular == null)
                {
                    ofendido2.telefonoCelular = "No aplica";
                }
                if (ofendido2.profesion == null)
                {
                    ofendido2.profesion = "No aplica";
                }
                if (ofendido2.correoElectronicoPersona == null)
                {
                    ofendido2.correoElectronicoPersona = "No aplica";
                }
                ofendido2.lugarTrabajoPersona = ofendido2.fechaNacimiento?.ToString("dd-MM-yyyy");
                ofendidos2.Add(ofendido2);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("Ofendido2DataSet", ofendidos2));

                //Sexo y nacionalidad del ofendido 2
                TablaGeneral sexoO2 = new TablaGeneral();
                List<TablaGeneral> sexosO2 = new List<TablaGeneral>();
                sexoO2.descripcion = tablaGeneralDAL.Get((int)ofendido2.sexo).descripcion;
                sexosO2.Add(sexoO2);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("SexoOfendido2DataSet", sexosO2));

                TablaGeneral nacionalidadO2 = new TablaGeneral();
                List<TablaGeneral> nacionalidadesO2 = new List<TablaGeneral>();
                nacionalidadO2.descripcion = tablaGeneralDAL.Get((int)ofendido2.nacionalidad).descripcion;
                nacionalidadesO2.Add(nacionalidadO2);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("NacionalidadOfendido2DataSet", nacionalidadesO2));

                TablaGeneral ofendido2nulo = new TablaGeneral();
                List<TablaGeneral> ofendido2nulos = new List<TablaGeneral>();
                ofendido2nulos.Add(ofendido2nulo);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("Ofendido2NuloDataSet", ofendido2nulos));

                TablaGeneral edadO2 = new TablaGeneral();
                List<TablaGeneral> edadesO2 = new List<TablaGeneral>();
                edadO2.descripcion = ObtenerEdad(ofendido2.fechaNacimiento);
                edadesO2.Add(edadO2);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("EdadOfendido2DataSet", edadesO2));
            }

            //Ofendido 3
            if (parte.ofendido3 == null)
            {
                Personas ofendido3 = new Personas();
                List<Personas> ofendidos3 = new List<Personas>();
                ofendidos3.Add(ofendido3);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("Ofendido3DataSet", ofendidos3));

                TablaGeneral sexoO3 = new TablaGeneral();
                List<TablaGeneral> sexosO3 = new List<TablaGeneral>();
                sexosO3.Add(sexoO3);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("SexoOfendido3DataSet", sexosO3));

                TablaGeneral nacionalidadO3 = new TablaGeneral();
                List<TablaGeneral> nacionalidadesO3 = new List<TablaGeneral>();
                nacionalidadesO3.Add(nacionalidadO3);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("NacionalidadOfendido3DataSet", nacionalidadesO3));


                TablaGeneral ofendido3nulo = new TablaGeneral();
                List<TablaGeneral> ofendido3nulos = new List<TablaGeneral>();
                ofendido3nulo.descripcion = "No aplica";
                ofendido3nulos.Add(ofendido3nulo);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("Ofendido3NuloDataSet", ofendido3nulos));

                TablaGeneral edadO3 = new TablaGeneral();
                List<TablaGeneral> edadesO3 = new List<TablaGeneral>();
                edadO3.descripcion = "No aplica";
                edadesO3.Add(edadO3);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("EdadOfendido3DataSet", edadesO3));
            }
            else
            {
                Personas ofendido3 = personaDAL.GetPersona((int)parte.ofendido3);
                List<Personas> ofendidos3 = new List<Personas>();
                if (ofendido3.direccionPersona == null)
                {
                    ofendido3.direccionPersona = "No aplica";
                }
                if (ofendido3.telefonoCelular == null)
                {
                    ofendido3.telefonoCelular = "No aplica";
                }
                if (ofendido3.profesion == null)
                {
                    ofendido3.profesion = "No aplica";
                }
                if (ofendido3.correoElectronicoPersona == null)
                {
                    ofendido3.correoElectronicoPersona = "No aplica";
                }
                ofendido3.lugarTrabajoPersona = ofendido3.fechaNacimiento?.ToString("dd-MM-yyyy");
                ofendido3.idPersona = int.Parse(ObtenerEdad(ofendido3.fechaNacimiento));
                ofendidos3.Add(ofendido3);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("Ofendido3DataSet", ofendidos3));

                //Sexo y nacionalidad del ofendido 3
                TablaGeneral sexoO3 = new TablaGeneral();
                List<TablaGeneral> sexosO3 = new List<TablaGeneral>();
                sexoO3.descripcion = tablaGeneralDAL.Get((int)ofendido3.sexo).descripcion;
                sexosO3.Add(sexoO3);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("SexoOfendido3DataSet", sexosO3));

                TablaGeneral nacionalidadO3 = new TablaGeneral();
                List<TablaGeneral> nacionalidadesO3 = new List<TablaGeneral>();
                nacionalidadO3.descripcion = tablaGeneralDAL.Get((int)ofendido3.nacionalidad).descripcion;
                nacionalidadesO3.Add(nacionalidadO3);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("NacionalidadOfendido3DataSet", nacionalidadesO3));

                TablaGeneral ofendido3nulo = new TablaGeneral();
                List<TablaGeneral> ofendido3nulos = new List<TablaGeneral>();
                ofendido3nulos.Add(ofendido3nulo);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("Ofendido3NuloDataSet", ofendido3nulos));

                TablaGeneral edadO3 = new TablaGeneral();
                List<TablaGeneral> edadesO3 = new List<TablaGeneral>();
                edadO3.descripcion = ObtenerEdad(ofendido3.fechaNacimiento);
                edadesO3.Add(edadO3);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("EdadOfendido3DataSet", edadesO3));
            }

            //Ofendido 4
            if (parte.ofendido4 == null)
            {
                Personas ofendido4 = new Personas();
                List<Personas> ofendidos4 = new List<Personas>();
                ofendidos4.Add(ofendido4);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("Ofendido4DataSet", ofendidos4));

                TablaGeneral ofendido4nulo = new TablaGeneral();
                List<TablaGeneral> ofendido4nulos = new List<TablaGeneral>();
                ofendido4nulo.descripcion = "No aplica";
                ofendido4nulos.Add(ofendido4nulo);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("Ofendido4NuloDataSet", ofendido4nulos));

                TablaGeneral sexoO4 = new TablaGeneral();
                List<TablaGeneral> sexosO4 = new List<TablaGeneral>();
                sexosO4.Add(sexoO4);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("SexoOfendido4DataSet", sexosO4));

                TablaGeneral nacionalidadO4 = new TablaGeneral();
                List<TablaGeneral> nacionalidadesO4 = new List<TablaGeneral>();
                nacionalidadesO4.Add(nacionalidadO4);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("NacionalidadOfendido4DataSet", nacionalidadesO4));

                TablaGeneral edadO4 = new TablaGeneral();
                List<TablaGeneral> edadesO4 = new List<TablaGeneral>();
                edadO4.descripcion = "No aplica";
                edadesO4.Add(edadO4);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("EdadOfendido4DataSet", edadesO4));
            }
            else
            {
                Personas ofendido4 = personaDAL.GetPersona((int)parte.ofendido4);
                List<Personas> ofendidos4 = new List<Personas>();
                if (ofendido4.direccionPersona == null)
                {
                    ofendido4.direccionPersona = "No aplica";
                }
                if (ofendido4.telefonoCelular == null)
                {
                    ofendido4.telefonoCelular = "No aplica";
                }
                if (ofendido4.profesion == null)
                {
                    ofendido4.profesion = "No aplica";
                }
                if (ofendido4.correoElectronicoPersona == null)
                {
                    ofendido4.correoElectronicoPersona = "No aplica";
                }
                ofendido4.lugarTrabajoPersona = ofendido4.fechaNacimiento?.ToString("dd-MM-yyyy");
                ofendido4.idPersona = int.Parse(ObtenerEdad(ofendido4.fechaNacimiento));
                ofendidos4.Add(ofendido4);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("Ofendido4DataSet", ofendidos4));

                //Sexo y nacionalidad del ofendido 4
                TablaGeneral sexoO4 = new TablaGeneral();
                List<TablaGeneral> sexosO4 = new List<TablaGeneral>();
                sexoO4.descripcion = tablaGeneralDAL.Get((int)ofendido4.sexo).descripcion;
                sexosO4.Add(sexoO4);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("SexoOfendido4DataSet", sexosO4));

                TablaGeneral nacionalidadO4 = new TablaGeneral();
                List<TablaGeneral> nacionalidadesO4 = new List<TablaGeneral>();
                nacionalidadO4.descripcion = tablaGeneralDAL.Get((int)ofendido4.nacionalidad).descripcion;
                nacionalidadesO4.Add(nacionalidadO4);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("NacionalidadOfendido4DataSet", nacionalidadesO4));

                TablaGeneral ofendido4nulo = new TablaGeneral();
                List<TablaGeneral> ofendido4nulos = new List<TablaGeneral>();
                ofendido4nulos.Add(ofendido4nulo);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("Ofendido4NuloDataSet", ofendido4nulos));

                TablaGeneral edadO4 = new TablaGeneral();
                List<TablaGeneral> edadesO4 = new List<TablaGeneral>();
                edadO4.descripcion = ObtenerEdad(ofendido4.fechaNacimiento);
                edadesO4.Add(edadO4);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("EdadOfendido4DataSet", edadesO4));
            }

            //Ofendido 5
            if (parte.ofendido5 == null)
            {
                Personas ofendido5 = new Personas();
                List<Personas> ofendidos5 = new List<Personas>();
                ofendidos5.Add(ofendido5);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("Ofendido5DataSet", ofendidos5));

                TablaGeneral ofendido5nulo = new TablaGeneral();
                List<TablaGeneral> ofendido5nulos = new List<TablaGeneral>();
                ofendido5nulo.descripcion = "No aplica";
                ofendido5nulos.Add(ofendido5nulo);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("Ofendido5NuloDataSet", ofendido5nulos));

                TablaGeneral sexoO5 = new TablaGeneral();
                List<TablaGeneral> sexosO5 = new List<TablaGeneral>();
                sexosO5.Add(sexoO5);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("SexoOfendido5DataSet", sexosO5));

                TablaGeneral nacionalidadO5 = new TablaGeneral();
                List<TablaGeneral> nacionalidadesO5 = new List<TablaGeneral>();
                nacionalidadesO5.Add(nacionalidadO5);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("NacionalidadOfendido5DataSet", nacionalidadesO5));

                TablaGeneral edadO5 = new TablaGeneral();
                List<TablaGeneral> edadesO5 = new List<TablaGeneral>();
                edadO5.descripcion = "No aplica";
                edadesO5.Add(edadO5);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("EdadOfendido5DataSet", edadesO5));
            }
            else
            {
                Personas ofendido5 = personaDAL.GetPersona((int)parte.ofendido5);
                List<Personas> ofendidos5 = new List<Personas>();
                if (ofendido5.direccionPersona == null)
                {
                    ofendido5.direccionPersona = "No aplica";
                }
                if (ofendido5.telefonoCelular == null)
                {
                    ofendido5.telefonoCelular = "No aplica";
                }
                if (ofendido5.profesion == null)
                {
                    ofendido5.profesion = "No aplica";
                }
                if (ofendido5.correoElectronicoPersona == null)
                {
                    ofendido5.correoElectronicoPersona = "No aplica";
                }
                ofendido5.lugarTrabajoPersona = ofendido5.fechaNacimiento?.ToString("dd-MM-yyyy");
                ofendido5.idPersona = int.Parse(ObtenerEdad(ofendido5.fechaNacimiento));
                ofendidos5.Add(ofendido5);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("Ofendido5DataSet", ofendidos5));

                //Sexo y nacionalidad del ofendido 5
                TablaGeneral sexoO5 = new TablaGeneral();
                List<TablaGeneral> sexosO5 = new List<TablaGeneral>();
                sexoO5.descripcion = tablaGeneralDAL.Get((int)ofendido5.sexo).descripcion;
                sexosO5.Add(sexoO5);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("SexoOfendido5DataSet", sexosO5));

                TablaGeneral nacionalidadO5 = new TablaGeneral();
                List<TablaGeneral> nacionalidadesO5 = new List<TablaGeneral>();
                nacionalidadO5.descripcion = tablaGeneralDAL.Get((int)ofendido5.nacionalidad).descripcion;
                nacionalidadesO5.Add(nacionalidadO5);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("NacionalidadOfendido5DataSet", nacionalidadesO5));

                TablaGeneral ofendido5nulo = new TablaGeneral();
                List<TablaGeneral> ofendido5nulos = new List<TablaGeneral>();
                ofendido5nulos.Add(ofendido5nulo);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("Ofendido5NuloDataSet", ofendido5nulos));

                TablaGeneral edadO5 = new TablaGeneral();
                List<TablaGeneral> edadesO5 = new List<TablaGeneral>();
                edadO5.descripcion = ObtenerEdad(ofendido5.fechaNacimiento);
                edadesO5.Add(edadO5);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("EdadOfendido5DataSet", edadesO5));
            }

            if (parte.entendido == null)
            {
                TablaGeneral entendidonulo = new TablaGeneral();
                List<TablaGeneral> entendidonulos = new List<TablaGeneral>();
                entendidonulo.descripcion = "No aplica";
                entendidonulos.Add(entendidonulo);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("EntendidoNuloDataSet", entendidonulos));
            }
            else
            {
                TablaGeneral entendidonulo = new TablaGeneral();
                List<TablaGeneral> entendidonulos = new List<TablaGeneral>();
                entendidonulos.Add(entendidonulo);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("EntendidoNuloDataSet", entendidonulos));
            }

            if (aprehendido.descripcion == "No")
            {
                TablaGeneral aprehendidono = new TablaGeneral();
                List<TablaGeneral> aprehendidosno = new List<TablaGeneral>();
                aprehendidono.descripcion = "No aplica";
                aprehendidosno.Add(aprehendidono);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("AprehendidoNoDataSet", aprehendidosno));

                parte.horaAprehension = null;
            }
            else
            {
                TablaGeneral aprehendidono = new TablaGeneral();
                List<TablaGeneral> aprehendidosno = new List<TablaGeneral>();
                aprehendidosno.Add(aprehendidono);

                viewer.LocalReport.DataSources.Add(new ReportDataSource("AprehendidoNoDataSet", aprehendidosno));
            }

            //--------------------------Creacion de las variables--------------------------
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            var deviceInfo = @"<DeviceInfo>
            <EmbedFonts>None</EmbedFonts>
            <OutputFormat>PDF</OutputFormat>
            <PageWidth>8.5in</PageWidth>
            <PageHeight>11in</PageHeight>
            <MarginTop>0.25in</MarginTop>
            <MarginLeft>0.25in</MarginLeft>
            <MarginRight>0.25in</MarginRight>
            <MarginBottom>0.25in</MarginBottom>
            </DeviceInfo>";

            byte[] bytes = viewer.LocalReport.Render("PDF", deviceInfo, out mimeType, out encoding, out extension, out streamIds, out warnings);

            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename=" + "Parte policial No. " + parte.numeroFolio + "." + extension);
            Response.BinaryWrite(bytes);
            Response.Flush();
        }
    }
}