using BackEnd;
using BackEnd.DAL;
using FrontEnd.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    public class AuditoriaController : Controller
    {

        IAuditoriaDAL auditoriaDAL;
        IPoliciaDAL policiaDAL;
        ITablaGeneralDAL tablaGeneralDAL;
        IUsuarioDAL usuarioDAL;
        IPersonaDAL personaDAL;
        IInfractorDAL infractorDAL;
        IRequisitoDAL requisitoDAL;
        IArmaDAL armaDAL;
        IBitacoraDAL bitacoraDAL;
        IActaDeObservacionPolicialDAL actaDeObservacionPolicialDAL;
        IActaHallazgoDAL actaHallazgoDAL;
        IActaDecomisoDAL actaDecomisoDAL;
        IActaDeDestruccionDePerecederosDAL actaDeDestruccionDePerecederosDAL;
        IActaEntregaPorOrdenDeDAL actaEntregaPorOrdenDeDAL;
        IActaNotificacionVendedorAmbulanteDAL actaNotificacionVendedorAmbulanteDAL;
        IActaDeNotificacionDAL actaDeNotificacionDAL;
        IActaEntregaDAL actaEntregaDAL;
        IParteDAL parteDAL;
        public void Autorizar()
        {
            if (Session["userID"] != null)
            {
                if (Session["Rol"].ToString() == "4" || Session["Rol"].ToString() =="1")
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
        

        public AuditoriaViewModel CargarAuditoriaPolicia(Auditorias auditoria)
        {
            
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            auditoriaDAL = new AuditoriaDAL();
            usuarioDAL = new UsuarioDAL();
            return new PoliciaViewModel
            {
                IdAuditoria = auditoria.idAuditoria,
                IdCategoria = auditoria.idCategoria,
                VistaCategoria = tablaGeneralDAL.Get(auditoria.idCategoria).descripcion,
                IdElemento = auditoria.idElemento,
                VistaElemento = policiaDAL.GetPolicia(auditoria.idElemento).nombre,
                Fecha = auditoria.fecha,
                Accion = auditoria.accion,
                VistaAccion = tablaGeneralDAL.Get(auditoria.accion).descripcion,
                IdUsuario = auditoria.idUsuario,
                VistaUsuario = usuarioDAL.GetUsuario(auditoria.idUsuario).nombre,
                Justificacion = auditoria.justificacion
            };
        }
        public ActionResult ListadoPolicia(int id, string tiposAccion, string filtrosSeleccionado, string busquedaFechaInicioP, string busquedaFechaFinalP)
        {
            Autorizar();
            auditoriaDAL = new AuditoriaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            List<PoliciaViewModel> auditorias = new List<PoliciaViewModel>();
            List<PoliciaViewModel> auditoriasFiltrados = new List<PoliciaViewModel>();

            foreach (Auditorias auditoria in auditoriaDAL.Get())
            {
                if (id == auditoria.idElemento && tablaGeneralDAL.Get(auditoria.idCategoria).descripcion.Contains("Policia"))
                {
                    auditorias.Add((PoliciaViewModel)CargarAuditoriaPolicia(auditoria));
                }
            }

            if (filtrosSeleccionado != "0" && tiposAccion != null) {
                foreach (PoliciaViewModel auditoria in auditorias)
                {
                    if (filtrosSeleccionado == "1")
                    {
                        if (tablaGeneralDAL.Get(auditoria.Accion).descripcion.Contains(tiposAccion))
                        {
                            auditoriasFiltrados.Add(auditoria);
                        }
                    }
                }
                if (filtrosSeleccionado == "2")
                {
                    DateTime fechaInicio = DateTime.Parse(busquedaFechaInicioP);
                    DateTime fechaFinal = DateTime.Parse(busquedaFechaFinalP);
                    if (fechaInicio < fechaFinal)
                    {
                        if (auditoriaDAL.GetAuditoriasRango(fechaInicio, fechaFinal, id) != null)
                        {
                            foreach (Auditorias auditoriasFecha in auditoriaDAL.GetAuditoriasRango(fechaInicio, fechaFinal, id).ToList())
                            {
                                auditoriasFiltrados.Add((PoliciaViewModel)CargarAuditoriaPolicia(auditoriasFecha));
                            }
                        }
                    }
                }
                auditorias = auditoriasFiltrados;

            }
            return View(auditorias);
        }

        public ActionResult DetallePolicia(int id)
        {
            Autorizar();
            policiaDAL = new PoliciaDAL();
            auditoriaDAL = new AuditoriaDAL();
            PoliciaViewModel modelo = (PoliciaViewModel)CargarAuditoriaPolicia(auditoriaDAL.GetAuditoria(id));
            return View(modelo);
        }

        public AuditoriaViewModel CargarAuditoriaPersona(Auditorias auditoria)
        {
        
            tablaGeneralDAL = new TablaGeneralDAL();
            personaDAL = new PersonaDAL();
            auditoriaDAL = new AuditoriaDAL();
            usuarioDAL = new UsuarioDAL();
            return new PersonaViewModel
            {
                IdAuditoria = auditoria.idAuditoria,
                IdCategoria = auditoria.idCategoria,
                VistaCategoria = tablaGeneralDAL.Get(auditoria.idCategoria).descripcion,
                IdElemento = auditoria.idElemento,
                VistaElemento = personaDAL.GetPersona(auditoria.idElemento).nombre,
                Fecha = auditoria.fecha,
                Accion = auditoria.accion,
                VistaAccion = tablaGeneralDAL.Get(auditoria.accion).descripcion,
                IdUsuario = auditoria.idUsuario,
                VistaUsuario = usuarioDAL.GetUsuario(auditoria.idUsuario).nombre,

            };
        }
        public ActionResult ListadoPersona(int id, string tiposAccion, string filtrosSeleccionado, string busquedaFechaInicioP, string busquedaFechaFinalP)
        {
            Autorizar();
            auditoriaDAL = new AuditoriaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            List<PersonaViewModel> auditorias = new List<PersonaViewModel>();
            List<PersonaViewModel> auditoriasFiltrados = new List<PersonaViewModel>();

            foreach (Auditorias auditoria in auditoriaDAL.Get())
            {
                if (id == auditoria.idElemento && tablaGeneralDAL.Get(auditoria.idCategoria).descripcion.Contains("Persona"))
                {
                    auditorias.Add((PersonaViewModel)CargarAuditoriaPersona(auditoria));
                }
            }

            if (filtrosSeleccionado != "0" && tiposAccion != null)
            {
                foreach (PersonaViewModel auditoria in auditorias)
                {
                    if (filtrosSeleccionado == "1")
                    {
                        if (tablaGeneralDAL.Get(auditoria.Accion).descripcion.Contains(tiposAccion))
                        {
                            auditoriasFiltrados.Add(auditoria);
                        }
                    }
                }
                if (filtrosSeleccionado == "2")
                {
                    DateTime fechaInicio = DateTime.Parse(busquedaFechaInicioP);
                    DateTime fechaFinal = DateTime.Parse(busquedaFechaFinalP);
                    if (fechaInicio < fechaFinal)
                    {
                        if (auditoriaDAL.GetAuditoriasRango(fechaInicio, fechaFinal, id) != null )
                        {
                            foreach (Auditorias auditoriasFecha in auditoriaDAL.GetAuditoriasRango(fechaInicio, fechaFinal, id).ToList())
                            {
                                if (tablaGeneralDAL.Get(auditoriasFecha.idCategoria).descripcion.Contains("Persona")) { 
                                auditoriasFiltrados.Add((PersonaViewModel)CargarAuditoriaPersona(auditoriasFecha));
                                }
                            }
                        }
                    }
                }
                auditorias = auditoriasFiltrados;

            }
            return View(auditorias);
        }

        public ActionResult DetallePersona(int id)
        {
            Autorizar();
            personaDAL = new PersonaDAL();
            auditoriaDAL = new AuditoriaDAL();
            PersonaViewModel modelo = (PersonaViewModel)CargarAuditoriaPersona(auditoriaDAL.GetAuditoria(id));
            return View(modelo);
        }

        public AuditoriaViewModel CargarAuditoriaInfractor(Auditorias auditoria)
        {
           
            tablaGeneralDAL = new TablaGeneralDAL();
            infractorDAL = new InfractorDAL();
            auditoriaDAL = new AuditoriaDAL();
            usuarioDAL = new UsuarioDAL();
            return new InfractorViewModel
            {
                IdAuditoria = auditoria.idAuditoria,
                IdCategoria = auditoria.idCategoria,
                VistaCategoria = tablaGeneralDAL.Get(auditoria.idCategoria).descripcion,
                IdElemento = auditoria.idElemento,
                VistaElemento = infractorDAL.GetInfractor(auditoria.idElemento).nombreCompleto,
                Fecha = auditoria.fecha,
                Accion = auditoria.accion,
                VistaAccion = tablaGeneralDAL.Get(auditoria.accion).descripcion,
                IdUsuario = auditoria.idUsuario,
                VistaUsuario = usuarioDAL.GetUsuario(auditoria.idUsuario).nombre,

            };
        }
        public ActionResult ListadoInfractor(int id, string tiposAccion, string filtrosSeleccionado, string busquedaFechaInicioP, string busquedaFechaFinalP)
        {
            Autorizar();
            auditoriaDAL = new AuditoriaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            List<InfractorViewModel> auditorias = new List<InfractorViewModel>();
            List<InfractorViewModel> auditoriasFiltrados = new List<InfractorViewModel>();

            foreach (Auditorias auditoria in auditoriaDAL.Get())
            {
                if (id == auditoria.idElemento && tablaGeneralDAL.Get(auditoria.idCategoria).descripcion.Contains("Infractor"))
                {
                    auditorias.Add((InfractorViewModel)CargarAuditoriaInfractor(auditoria));
                }
            }

            if (filtrosSeleccionado != "0" && tiposAccion != null)
            {
                foreach (InfractorViewModel auditoria in auditorias)
                {
                    if (filtrosSeleccionado == "1")
                    {
                        if (tablaGeneralDAL.Get(auditoria.Accion).descripcion.Contains(tiposAccion))
                        {
                            auditoriasFiltrados.Add(auditoria);
                        }
                    }
                }
                if (filtrosSeleccionado == "2")
                {
                    DateTime fechaInicio = DateTime.Parse(busquedaFechaInicioP);
                    DateTime fechaFinal = DateTime.Parse(busquedaFechaFinalP);
                    if (fechaInicio < fechaFinal)
                    {
                        if (auditoriaDAL.GetAuditoriasRango(fechaInicio, fechaFinal, id) != null)
                        {
                            foreach (Auditorias auditoriasFecha in auditoriaDAL.GetAuditoriasRango(fechaInicio, fechaFinal, id).ToList())
                            {
                                if (tablaGeneralDAL.Get(auditoriasFecha.idCategoria).descripcion.Contains("Infractor")) { 
                                auditoriasFiltrados.Add((InfractorViewModel)CargarAuditoriaInfractor(auditoriasFecha));
                                }
                            }
                        }
                    }
                }
                auditorias = auditoriasFiltrados;

            }
            return View(auditorias);
        }

        public ActionResult DetalleInfractor(int id)
        {
            Autorizar();
            infractorDAL = new InfractorDAL();
            auditoriaDAL = new AuditoriaDAL();
            InfractorViewModel modelo = (InfractorViewModel)CargarAuditoriaInfractor(auditoriaDAL.GetAuditoria(id));
            return View(modelo);
        }

        public AuditoriaViewModel CargarAuditoriaRequisito(Auditorias auditoria)
        {

            tablaGeneralDAL = new TablaGeneralDAL();
            requisitoDAL = new RequisitoDAL();
            auditoriaDAL = new AuditoriaDAL();
            usuarioDAL = new UsuarioDAL();
            return new RequisitoViewModel
            {
                IdAuditoria = auditoria.idAuditoria,
                IdCategoria = auditoria.idCategoria,
                VistaCategoria = tablaGeneralDAL.Get(auditoria.idCategoria).descripcion,
                IdElemento = auditoria.idElemento,
                VistaElemento = requisitoDAL.GetRequisito(auditoria.idElemento).detalles,
                Fecha = auditoria.fecha,
                Accion = auditoria.accion,
                VistaAccion = tablaGeneralDAL.Get(auditoria.accion).descripcion,
                IdUsuario = auditoria.idUsuario,
                VistaUsuario = usuarioDAL.GetUsuario(auditoria.idUsuario).nombre,

            };
        }
        public ActionResult ListadoRequisito(int id, string tiposAccion, string filtrosSeleccionado, string busquedaFechaInicioP, string busquedaFechaFinalP)
        {
            Autorizar();
            auditoriaDAL = new AuditoriaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            List<RequisitoViewModel> auditorias = new List<RequisitoViewModel>();
            List<RequisitoViewModel> auditoriasFiltrados = new List<RequisitoViewModel>();

            foreach (Auditorias auditoria in auditoriaDAL.Get())
            {
                if (id == auditoria.idElemento && tablaGeneralDAL.Get(auditoria.idCategoria).descripcion.Contains("Requisito"))
                {
                    auditorias.Add((RequisitoViewModel)CargarAuditoriaRequisito(auditoria));
                }
            }

            if (filtrosSeleccionado != "0" && tiposAccion != null)
            {
                foreach (RequisitoViewModel auditoria in auditorias)
                {
                    if (filtrosSeleccionado == "1")
                    {
                        if (tablaGeneralDAL.Get(auditoria.Accion).descripcion.Contains(tiposAccion))
                        {
                            auditoriasFiltrados.Add(auditoria);
                        }
                    }
                }
                if (filtrosSeleccionado == "2")
                {
                    DateTime fechaInicio = DateTime.Parse(busquedaFechaInicioP);
                    DateTime fechaFinal = DateTime.Parse(busquedaFechaFinalP);
                    if (fechaInicio < fechaFinal)
                    {
                        if (auditoriaDAL.GetAuditoriasRango(fechaInicio, fechaFinal, id) != null)
                        {
                            foreach (Auditorias auditoriasFecha in auditoriaDAL.GetAuditoriasRango(fechaInicio, fechaFinal, id).ToList())
                            {
                                if (tablaGeneralDAL.Get(auditoriasFecha.idCategoria).descripcion.Contains("Requisito")) { 
                                auditoriasFiltrados.Add((RequisitoViewModel)CargarAuditoriaRequisito(auditoriasFecha));
                                }
                            }
                        }
                    }
                }
                auditorias = auditoriasFiltrados;

            }
            return View(auditorias);
        }

        public ActionResult DetalleRequisito(int id)
        {
            Autorizar();
            policiaDAL = new PoliciaDAL();
            auditoriaDAL = new AuditoriaDAL();
            RequisitoViewModel modelo = (RequisitoViewModel)CargarAuditoriaRequisito(auditoriaDAL.GetAuditoria(id));
            return View(modelo);
        }

        public AuditoriaViewModel CargarAuditoriaArma(Auditorias auditoria)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            armaDAL = new ArmaDAL();
            auditoriaDAL = new AuditoriaDAL();
            usuarioDAL = new UsuarioDAL();
            return new ArmaViewModel
            {
                IdAuditoria = auditoria.idAuditoria,
                IdCategoria = auditoria.idCategoria,
                VistaCategoria = tablaGeneralDAL.Get(auditoria.idCategoria).descripcion,
                IdElemento = auditoria.idElemento,
                VistaElemento = armaDAL.GetArma(auditoria.idElemento).numeroSerie,
                Fecha = auditoria.fecha,
                Accion = auditoria.accion,
                VistaAccion = tablaGeneralDAL.Get(auditoria.accion).descripcion,
                IdUsuario = auditoria.idUsuario,
                VistaUsuario = usuarioDAL.GetUsuario(auditoria.idUsuario).nombre,
                Justificacion = auditoria.justificacion
            };
        }
        public ActionResult ListadoArma(int id, string tiposAccion, string filtrosSeleccionado, string busquedaFechaInicioP, string busquedaFechaFinalP)
        {
            Autorizar();
            auditoriaDAL = new AuditoriaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            List<ArmaViewModel> auditorias = new List<ArmaViewModel>();
            List<ArmaViewModel> auditoriasFiltrados = new List<ArmaViewModel>();

            foreach (Auditorias auditoria in auditoriaDAL.Get())
            {
                if (id == auditoria.idElemento && tablaGeneralDAL.Get(auditoria.idCategoria).descripcion.Contains("Arma"))
                {
                    auditorias.Add((ArmaViewModel)CargarAuditoriaArma(auditoria));
                }
            }

            if (filtrosSeleccionado != "0" && tiposAccion != null)
            {
                foreach (ArmaViewModel auditoria in auditorias)
                {
                    if (filtrosSeleccionado == "1")
                    {
                        if (tablaGeneralDAL.Get(auditoria.Accion).descripcion.Contains(tiposAccion))
                        {
                            auditoriasFiltrados.Add(auditoria);
                        }
                    }
                }
                if (filtrosSeleccionado == "2")
                {
                    DateTime fechaInicio = DateTime.Parse(busquedaFechaInicioP);
                    DateTime fechaFinal = DateTime.Parse(busquedaFechaFinalP);
                    if (fechaInicio < fechaFinal)
                    {
                        if (auditoriaDAL.GetAuditoriasRango(fechaInicio, fechaFinal, id) != null)
                        {
                            foreach (Auditorias auditoriasFecha in auditoriaDAL.GetAuditoriasRango(fechaInicio, fechaFinal, id).ToList())
                            {
                                if (tablaGeneralDAL.Get(auditoriasFecha.idCategoria).descripcion.Contains("Arma"))
                                {                                
                                auditoriasFiltrados.Add((ArmaViewModel)CargarAuditoriaArma(auditoriasFecha));
                                }
                            }
                        }
                    }
                }
                auditorias = auditoriasFiltrados;

            }
            return View(auditorias);
        }

        public ActionResult DetalleArma(int id)
        {
            Autorizar();
            armaDAL = new ArmaDAL();
            auditoriaDAL = new AuditoriaDAL();
            ArmaViewModel modelo = (ArmaViewModel)CargarAuditoriaArma(auditoriaDAL.GetAuditoria(id));
            return View(modelo);
        }

        public AuditoriaViewModel CargarAuditoriaBitacora(Auditorias auditoria)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            bitacoraDAL = new BitacoraDAL();
            auditoriaDAL = new AuditoriaDAL();
            usuarioDAL = new UsuarioDAL();
            return new BitacoraViewModel
            {
                IdAuditoria = auditoria.idAuditoria,
                IdCategoria = auditoria.idCategoria,
                VistaCategoria = tablaGeneralDAL.Get(auditoria.idCategoria).descripcion,
                IdElemento = auditoria.idElemento,
                VistaElemento = bitacoraDAL.GetBitacora(auditoria.idElemento).numeroConsecutivo,
                Fecha = auditoria.fecha,
                Accion = auditoria.accion,
                VistaAccion = tablaGeneralDAL.Get(auditoria.accion).descripcion,
                IdUsuario = auditoria.idUsuario,
                VistaUsuario = usuarioDAL.GetUsuario(auditoria.idUsuario).nombre,

            };
        }
        public ActionResult ListadoBitacora(int id, string tiposAccion, string filtrosSeleccionado, string busquedaFechaInicioP, string busquedaFechaFinalP)
        {
            Autorizar();
            auditoriaDAL = new AuditoriaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            List<BitacoraViewModel> auditorias = new List<BitacoraViewModel>();
            List<BitacoraViewModel> auditoriasFiltrados = new List<BitacoraViewModel>();

            foreach (Auditorias auditoria in auditoriaDAL.Get())
            {
                if (id == auditoria.idElemento && tablaGeneralDAL.Get(auditoria.idCategoria).descripcion.Contains("Bitacora"))
                {
                    auditorias.Add((BitacoraViewModel)CargarAuditoriaBitacora(auditoria));
                }
            }

            if (filtrosSeleccionado != "0" && tiposAccion != null)
            {
                foreach (BitacoraViewModel auditoria in auditorias)
                {
                    if (filtrosSeleccionado == "1")
                    {
                        if (tablaGeneralDAL.Get(auditoria.Accion).descripcion.Contains(tiposAccion))
                        {
                            auditoriasFiltrados.Add(auditoria);
                        }
                    }
                }
                if (filtrosSeleccionado == "2")
                {
                    DateTime fechaInicio = DateTime.Parse(busquedaFechaInicioP);
                    DateTime fechaFinal = DateTime.Parse(busquedaFechaFinalP);
                    if (fechaInicio < fechaFinal)
                    {
                        if (auditoriaDAL.GetAuditoriasRango(fechaInicio, fechaFinal, id) != null)
                        {
                            foreach (Auditorias auditoriasFecha in auditoriaDAL.GetAuditoriasRango(fechaInicio, fechaFinal, id).ToList())
                            {
                                if (tablaGeneralDAL.Get(auditoriasFecha.idCategoria).descripcion.Contains("Bitacora")) {
                                auditoriasFiltrados.Add((BitacoraViewModel)CargarAuditoriaBitacora(auditoriasFecha));
                             }
                            }
                        }
                    }
                }
                auditorias = auditoriasFiltrados;

            }
            return View(auditorias);
        }

        public ActionResult DetalleBitacora(int id)
        {
            Autorizar();
            bitacoraDAL = new BitacoraDAL();
            auditoriaDAL = new AuditoriaDAL();
            BitacoraViewModel modelo = (BitacoraViewModel)CargarAuditoriaBitacora(auditoriaDAL.GetAuditoria(id));
            return View(modelo);
        }

        public AuditoriaViewModel CargarAuditoriaActaDeObservacionPolicial(Auditorias auditoria)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            actaDeObservacionPolicialDAL = new ActaDeObservacionPolicialDAL();
            auditoriaDAL = new AuditoriaDAL();
            usuarioDAL = new UsuarioDAL();
            return new ActaDeObservacionPolicialViewModel
            {
                IdAuditoria = auditoria.idAuditoria,
                IdCategoria = auditoria.idCategoria,
                VistaCategoria = tablaGeneralDAL.Get(auditoria.idCategoria).descripcion,
                IdElemento = auditoria.idElemento,
                VistaElemento = actaDeObservacionPolicialDAL.GetActaDeObservacionPolicial(auditoria.idElemento).numeroFolio,
                Fecha = auditoria.fecha,
                Accion = auditoria.accion,
                VistaAccion = tablaGeneralDAL.Get(auditoria.accion).descripcion,
                IdUsuario = auditoria.idUsuario,
                VistaUsuario = usuarioDAL.GetUsuario(auditoria.idUsuario).nombre,
                
            };
        }
        public ActionResult ListadoActaDeObservacionPolicial(int id, string tiposAccion, string filtrosSeleccionado, string busquedaFechaInicioP, string busquedaFechaFinalP)
        {
            Autorizar();
            auditoriaDAL = new AuditoriaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            List<ActaDeObservacionPolicialViewModel> auditorias = new List<ActaDeObservacionPolicialViewModel>();
            List<ActaDeObservacionPolicialViewModel> auditoriasFiltrados = new List<ActaDeObservacionPolicialViewModel>();

            foreach (Auditorias auditoria in auditoriaDAL.Get())
            {
                if (id == auditoria.idElemento && tablaGeneralDAL.Get(auditoria.idCategoria).descripcion.Contains("Acta de Observación Policial"))
                {
                    auditorias.Add((ActaDeObservacionPolicialViewModel)CargarAuditoriaActaDeObservacionPolicial(auditoria));
                }
            }

            if (filtrosSeleccionado != "0" && tiposAccion != null)
            {
                foreach (ActaDeObservacionPolicialViewModel auditoria in auditorias)
                {
                    if (filtrosSeleccionado == "1")
                    {
                        if (tablaGeneralDAL.Get(auditoria.Accion).descripcion.Contains(tiposAccion))
                        {
                            auditoriasFiltrados.Add(auditoria);
                        }
                    }
                }
                if (filtrosSeleccionado == "2")
                {
                    DateTime fechaInicio = DateTime.Parse(busquedaFechaInicioP);
                    DateTime fechaFinal = DateTime.Parse(busquedaFechaFinalP);
                    if (fechaInicio < fechaFinal)
                    {
                        if (auditoriaDAL.GetAuditoriasRango(fechaInicio, fechaFinal, id) != null)
                        {
                            foreach (Auditorias auditoriasFecha in auditoriaDAL.GetAuditoriasRango(fechaInicio, fechaFinal, id).ToList())
                            {
                                if (tablaGeneralDAL.Get(auditoriasFecha.idCategoria).descripcion.Contains("Acta de Observación Policial")) { 
                                auditoriasFiltrados.Add((ActaDeObservacionPolicialViewModel)CargarAuditoriaActaDeObservacionPolicial(auditoriasFecha));
                            }
                            }
                        }
                    }
                }
                auditorias = auditoriasFiltrados;

            }
            return View(auditorias);
        }

        public ActionResult DetalleActaDeObservacionPolicial(int id)
        {
            Autorizar();
            auditoriaDAL = new AuditoriaDAL();
            ActaDeObservacionPolicialViewModel modelo = (ActaDeObservacionPolicialViewModel)CargarAuditoriaActaDeObservacionPolicial(auditoriaDAL.GetAuditoria(id));
            return View(modelo);
        }


        public AuditoriaViewModel CargarAuditoriaActaHallazgo(Auditorias auditoria)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            actaHallazgoDAL = new ActaHallazgoDAL();
            auditoriaDAL = new AuditoriaDAL();
            usuarioDAL = new UsuarioDAL();
            return new ActaHallazgoViewModel
            {
                IdAuditoria = auditoria.idAuditoria,
                IdCategoria = auditoria.idCategoria,
                VistaCategoria = tablaGeneralDAL.Get(auditoria.idCategoria).descripcion,
                IdElemento = auditoria.idElemento,
                VistaElemento = actaHallazgoDAL.GetActaHallazgo(auditoria.idElemento).numeroFolio,
                Fecha = auditoria.fecha,
                Accion = auditoria.accion,
                VistaAccion = tablaGeneralDAL.Get(auditoria.accion).descripcion,
                IdUsuario = auditoria.idUsuario,
                VistaUsuario = usuarioDAL.GetUsuario(auditoria.idUsuario).nombre,
            };
        }
        public ActionResult ListadoActaHallazgo(int id, string tiposAccion, string filtrosSeleccionado, string busquedaFechaInicioP, string busquedaFechaFinalP)
        {
            Autorizar();
            auditoriaDAL = new AuditoriaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            List<ActaHallazgoViewModel> auditorias = new List<ActaHallazgoViewModel>();
            List<ActaHallazgoViewModel> auditoriasFiltrados = new List<ActaHallazgoViewModel>();

            foreach (Auditorias auditoria in auditoriaDAL.Get())
            {
                if (id == auditoria.idElemento && tablaGeneralDAL.Get(auditoria.idCategoria).descripcion.Contains("Acta de Hallazgo"))
                {
                    auditorias.Add((ActaHallazgoViewModel)CargarAuditoriaActaHallazgo(auditoria));
                }
            }

            if (filtrosSeleccionado != "0" && tiposAccion != null)
            {
                foreach (ActaHallazgoViewModel auditoria in auditorias)
                {
                    if (filtrosSeleccionado == "1")
                    {
                        if (tablaGeneralDAL.Get(auditoria.Accion).descripcion.Contains(tiposAccion))
                        {
                            auditoriasFiltrados.Add(auditoria);
                        }
                    }
                }
                if (filtrosSeleccionado == "2")
                {
                    DateTime fechaInicio = DateTime.Parse(busquedaFechaInicioP);
                    DateTime fechaFinal = DateTime.Parse(busquedaFechaFinalP);
                    if (fechaInicio < fechaFinal)
                    {
                        if (auditoriaDAL.GetAuditoriasRango(fechaInicio, fechaFinal, id) != null)
                        {
                            foreach (Auditorias auditoriasFecha in auditoriaDAL.GetAuditoriasRango(fechaInicio, fechaFinal, id).ToList())
                            {
                                if (tablaGeneralDAL.Get(auditoriasFecha.idCategoria).descripcion.Contains("Acta de Hallazgo")) {
                                auditoriasFiltrados.Add((ActaHallazgoViewModel)CargarAuditoriaActaHallazgo(auditoriasFecha));
                            }
                            }
                        }
                    }
                }
                auditorias = auditoriasFiltrados;

            }
            return View(auditorias);
        }

        public ActionResult DetalleActaHallazgo(int id)
        {
            Autorizar();
            auditoriaDAL = new AuditoriaDAL();
            ActaHallazgoViewModel modelo = (ActaHallazgoViewModel)CargarAuditoriaActaHallazgo(auditoriaDAL.GetAuditoria(id));
            return View(modelo);
        }

        public AuditoriaViewModel CargarAuditoriaActaDecomiso(Auditorias auditoria)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            actaDecomisoDAL = new ActaDecomisoDAL();
            auditoriaDAL = new AuditoriaDAL();
            usuarioDAL = new UsuarioDAL();
            return new ActaDecomisoViewModel
            {
                IdAuditoria = auditoria.idAuditoria,
                IdCategoria = auditoria.idCategoria,
                VistaCategoria = tablaGeneralDAL.Get(auditoria.idCategoria).descripcion,
                IdElemento = auditoria.idElemento,
                VistaElemento = actaDecomisoDAL.GetActaDecomiso(auditoria.idElemento).numeroFolio,
                Fecha = auditoria.fecha,
                Accion = auditoria.accion,
                VistaAccion = tablaGeneralDAL.Get(auditoria.accion).descripcion,
                IdUsuario = auditoria.idUsuario,
                VistaUsuario = usuarioDAL.GetUsuario(auditoria.idUsuario).nombre,
               
            };
        }
        public ActionResult ListadoActaDecomiso(int id, string tiposAccion, string filtrosSeleccionado, string busquedaFechaInicioP, string busquedaFechaFinalP)
        {
            Autorizar();
            auditoriaDAL = new AuditoriaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            List<ActaDecomisoViewModel> auditorias = new List<ActaDecomisoViewModel>();
            List<ActaDecomisoViewModel> auditoriasFiltrados = new List<ActaDecomisoViewModel>();

            foreach (Auditorias auditoria in auditoriaDAL.Get())
            {
                if (id == auditoria.idElemento && tablaGeneralDAL.Get(auditoria.idCategoria).descripcion.Contains("Acta de Decomiso"))
                {
                    auditorias.Add((ActaDecomisoViewModel)CargarAuditoriaActaDecomiso(auditoria));
                }
            }

            if (filtrosSeleccionado != "0" && tiposAccion != null)
            {
                foreach (ActaDecomisoViewModel auditoria in auditorias)
                {
                    if (filtrosSeleccionado == "1")
                    {
                        if (tablaGeneralDAL.Get(auditoria.Accion).descripcion.Contains(tiposAccion))
                        {
                            auditoriasFiltrados.Add(auditoria);
                        }
                    }
                }
                if (filtrosSeleccionado == "2")
                {
                    DateTime fechaInicio = DateTime.Parse(busquedaFechaInicioP);
                    DateTime fechaFinal = DateTime.Parse(busquedaFechaFinalP);
                    if (fechaInicio < fechaFinal)
                    {
                        if (auditoriaDAL.GetAuditoriasRango(fechaInicio, fechaFinal, id) != null)
                        {
                            foreach (Auditorias auditoriasFecha in auditoriaDAL.GetAuditoriasRango(fechaInicio, fechaFinal, id).ToList())
                            {
                                if (tablaGeneralDAL.Get(auditoriasFecha.idCategoria).descripcion.Contains("Acta de Decomiso")) { 
                                auditoriasFiltrados.Add((ActaDecomisoViewModel)CargarAuditoriaActaDecomiso(auditoriasFecha));
                            }
                            }
                        }
                    }
                }
                auditorias = auditoriasFiltrados;

            }
            return View(auditorias);
        }

        public ActionResult DetalleActaDecomiso(int id)
        {
            Autorizar();
            auditoriaDAL = new AuditoriaDAL();
            ActaDecomisoViewModel modelo = (ActaDecomisoViewModel)CargarAuditoriaActaDecomiso(auditoriaDAL.GetAuditoria(id));
            return View(modelo);
        }

        public AuditoriaViewModel CargarAuditoriaActaDeDestruccionDePerecederos(Auditorias auditoria)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            actaDeDestruccionDePerecederosDAL = new ActaDeDestruccionDePerecederosDAL();
            auditoriaDAL = new AuditoriaDAL();
            usuarioDAL = new UsuarioDAL();
            return new ActaDeDestruccionDePerecederosViewModel
            {
                IdAuditoria = auditoria.idAuditoria,
                IdCategoria = auditoria.idCategoria,
                VistaCategoria = tablaGeneralDAL.Get(auditoria.idCategoria).descripcion,
                IdElemento = auditoria.idElemento,
                VistaElemento = actaDeDestruccionDePerecederosDAL.GetActaDeDestruccionDePerecederos(auditoria.idElemento).numeroFolio,
                Fecha = auditoria.fecha,
                Accion = auditoria.accion,
                VistaAccion = tablaGeneralDAL.Get(auditoria.accion).descripcion,
                IdUsuario = auditoria.idUsuario,
                VistaUsuario = usuarioDAL.GetUsuario(auditoria.idUsuario).nombre,

            };
        }
        public ActionResult ListadoActaDeDestruccionDePerecederos(int id, string tiposAccion, string filtrosSeleccionado, string busquedaFechaInicioP, string busquedaFechaFinalP)
        {
            Autorizar();
            auditoriaDAL = new AuditoriaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            List<ActaDeDestruccionDePerecederosViewModel> auditorias = new List<ActaDeDestruccionDePerecederosViewModel>();
            List<ActaDeDestruccionDePerecederosViewModel> auditoriasFiltrados = new List<ActaDeDestruccionDePerecederosViewModel>();

            foreach (Auditorias auditoria in auditoriaDAL.Get())
            {
                if (id == auditoria.idElemento && tablaGeneralDAL.Get(auditoria.idCategoria).descripcion.Contains("Acta de Destrucción de Perecederos"))
                {
                    auditorias.Add((ActaDeDestruccionDePerecederosViewModel)CargarAuditoriaActaDeDestruccionDePerecederos(auditoria));
                }
            }

            if (filtrosSeleccionado != "0" && tiposAccion != null)
            {
                foreach (ActaDeDestruccionDePerecederosViewModel auditoria in auditorias)
                {
                    if (filtrosSeleccionado == "1")
                    {
                        if (tablaGeneralDAL.Get(auditoria.Accion).descripcion.Contains(tiposAccion))
                        {
                            auditoriasFiltrados.Add(auditoria);
                        }
                    }
                }
                if (filtrosSeleccionado == "2")
                {
                    DateTime fechaInicio = DateTime.Parse(busquedaFechaInicioP);
                    DateTime fechaFinal = DateTime.Parse(busquedaFechaFinalP);
                    if (fechaInicio < fechaFinal)
                    {
                        if (auditoriaDAL.GetAuditoriasRango(fechaInicio, fechaFinal, id) != null)
                        {
                            foreach (Auditorias auditoriasFecha in auditoriaDAL.GetAuditoriasRango(fechaInicio, fechaFinal, id).ToList())
                            {
                                if (tablaGeneralDAL.Get(auditoriasFecha.idCategoria).descripcion.Contains("Acta de Destrucción de Perecederos"))
                                {
                                    auditoriasFiltrados.Add((ActaDeDestruccionDePerecederosViewModel)CargarAuditoriaActaDeDestruccionDePerecederos(auditoriasFecha));
                                }
                            }
                        }
                    }
                }
                auditorias = auditoriasFiltrados;

            }
            return View(auditorias);
        }

        public ActionResult DetalleActaDeDestruccionDePerecederos(int id)
        {
            Autorizar();
            auditoriaDAL = new AuditoriaDAL();
            ActaDeDestruccionDePerecederosViewModel modelo = (ActaDeDestruccionDePerecederosViewModel)CargarAuditoriaActaDeDestruccionDePerecederos(auditoriaDAL.GetAuditoria(id));
            return View(modelo);
        }

        public AuditoriaViewModel CargarAuditoriaActaEntregaPorOrdenDe(Auditorias auditoria)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            actaEntregaPorOrdenDeDAL = new ActaEntregaPorOrdenDeDAL();
            auditoriaDAL = new AuditoriaDAL();
            usuarioDAL = new UsuarioDAL();
            return new ActaEntregaPorOrdenDeViewModel
            {
                IdAuditoria = auditoria.idAuditoria,
                IdCategoria = auditoria.idCategoria,
                VistaCategoria = tablaGeneralDAL.Get(auditoria.idCategoria).descripcion,
                IdElemento = auditoria.idElemento,
                VistaElemento = actaEntregaPorOrdenDeDAL.GetActaEntregaPorOrdenDe(auditoria.idElemento).numeroFolio,
                Fecha = auditoria.fecha,
                Accion = auditoria.accion,
                VistaAccion = tablaGeneralDAL.Get(auditoria.accion).descripcion,
                IdUsuario = auditoria.idUsuario,
                VistaUsuario = usuarioDAL.GetUsuario(auditoria.idUsuario).nombre,
            };
        }
        public ActionResult ListadoActaEntregaPorOrdenDe(int id, string tiposAccion, string filtrosSeleccionado, string busquedaFechaInicioP, string busquedaFechaFinalP)
        {
            Autorizar();
            auditoriaDAL = new AuditoriaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            List<ActaEntregaPorOrdenDeViewModel> auditorias = new List<ActaEntregaPorOrdenDeViewModel>();
            List<ActaEntregaPorOrdenDeViewModel> auditoriasFiltrados = new List<ActaEntregaPorOrdenDeViewModel>();

            foreach (Auditorias auditoria in auditoriaDAL.Get())
            {
                if (id == auditoria.idElemento && tablaGeneralDAL.Get(auditoria.idCategoria).descripcion.Contains("Acta de Entrega Por Orden De"))
                {
                    auditorias.Add((ActaEntregaPorOrdenDeViewModel)CargarAuditoriaActaEntregaPorOrdenDe(auditoria));
                }
            }

            if (filtrosSeleccionado != "0" && tiposAccion != null)
            {
                foreach (ActaEntregaPorOrdenDeViewModel auditoria in auditorias)
                {
                    if (filtrosSeleccionado == "1")
                    {
                        if (tablaGeneralDAL.Get(auditoria.Accion).descripcion.Contains(tiposAccion))
                        {
                            auditoriasFiltrados.Add(auditoria);
                        }
                    }
                }
                if (filtrosSeleccionado == "2")
                {
                    DateTime fechaInicio = DateTime.Parse(busquedaFechaInicioP);
                    DateTime fechaFinal = DateTime.Parse(busquedaFechaFinalP);
                    if (fechaInicio < fechaFinal)
                    {
                        if (auditoriaDAL.GetAuditoriasRango(fechaInicio, fechaFinal, id) != null)
                        {
                            foreach (Auditorias auditoriasFecha in auditoriaDAL.GetAuditoriasRango(fechaInicio, fechaFinal, id).ToList())
                            {
                                if (tablaGeneralDAL.Get(auditoriasFecha.idCategoria).descripcion.Contains("Acta de Entrega Por Orden De")) { 
                                auditoriasFiltrados.Add((ActaEntregaPorOrdenDeViewModel)CargarAuditoriaActaEntregaPorOrdenDe(auditoriasFecha));
                            }}
                        }
                    }
                }
                auditorias = auditoriasFiltrados;

            }
            return View(auditorias);
        }

        public ActionResult DetalleActaEntregaPorOrdenDe(int id)
        {
            Autorizar();
            auditoriaDAL = new AuditoriaDAL();
            ActaEntregaPorOrdenDeViewModel modelo = (ActaEntregaPorOrdenDeViewModel)CargarAuditoriaActaEntregaPorOrdenDe(auditoriaDAL.GetAuditoria(id));
            return View(modelo);
        }

        public AuditoriaViewModel CargarAuditoriaActaNotificacionVendedorAmbulante(Auditorias auditoria)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            actaNotificacionVendedorAmbulanteDAL = new ActaNotificacionVendedorAmbulanteDAL();
            auditoriaDAL = new AuditoriaDAL();
            usuarioDAL = new UsuarioDAL();
            return new ActaNotificacionVendedorAmbulanteViewModel
            {
                IdAuditoria = auditoria.idAuditoria,
                IdCategoria = auditoria.idCategoria,
                VistaCategoria = tablaGeneralDAL.Get(auditoria.idCategoria).descripcion,
                IdElemento = auditoria.idElemento,
                VistaElemento = actaNotificacionVendedorAmbulanteDAL.GetActaNotificacionVendedorAmbulante(auditoria.idElemento).numeroFolio,
                Fecha = auditoria.fecha,
                Accion = auditoria.accion,
                VistaAccion = tablaGeneralDAL.Get(auditoria.accion).descripcion,
                IdUsuario = auditoria.idUsuario,
                VistaUsuario = usuarioDAL.GetUsuario(auditoria.idUsuario).nombre,
            };
        }
        public ActionResult ListadoActaNotificacionVendedorAmbulante(int id, string tiposAccion, string filtrosSeleccionado, string busquedaFechaInicioP, string busquedaFechaFinalP)
        {
            Autorizar();
            auditoriaDAL = new AuditoriaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            List<ActaNotificacionVendedorAmbulanteViewModel> auditorias = new List<ActaNotificacionVendedorAmbulanteViewModel>();
            List<ActaNotificacionVendedorAmbulanteViewModel> auditoriasFiltrados = new List<ActaNotificacionVendedorAmbulanteViewModel>();

            foreach (Auditorias auditoria in auditoriaDAL.Get())
            {
                if (id == auditoria.idElemento && tablaGeneralDAL.Get(auditoria.idCategoria).descripcion.Contains("Acta de Notificación a Vendedor Ambulante"))
                {
                    auditorias.Add((ActaNotificacionVendedorAmbulanteViewModel)CargarAuditoriaActaNotificacionVendedorAmbulante(auditoria));
                }
            }

            if (filtrosSeleccionado != "0" && tiposAccion != null)
            {
                foreach (ActaNotificacionVendedorAmbulanteViewModel auditoria in auditorias)
                {
                    if (filtrosSeleccionado == "1")
                    {
                        if (tablaGeneralDAL.Get(auditoria.Accion).descripcion.Contains(tiposAccion))
                        {
                            auditoriasFiltrados.Add(auditoria);
                        }
                    }
                }
                if (filtrosSeleccionado == "2")
                {
                    DateTime fechaInicio = DateTime.Parse(busquedaFechaInicioP);
                    DateTime fechaFinal = DateTime.Parse(busquedaFechaFinalP);
                    if (fechaInicio < fechaFinal)
                    {
                        if (auditoriaDAL.GetAuditoriasRango(fechaInicio, fechaFinal, id) != null)
                        {
                            foreach (Auditorias auditoriasFecha in auditoriaDAL.GetAuditoriasRango(fechaInicio, fechaFinal, id).ToList())
                            {
                                if(tablaGeneralDAL.Get(auditoriasFecha.idCategoria).descripcion.Contains("Acta de Notificación a Vendedor Ambulante")) { 
                                auditoriasFiltrados.Add((ActaNotificacionVendedorAmbulanteViewModel)CargarAuditoriaActaNotificacionVendedorAmbulante(auditoriasFecha));
                                }
                            }
                        }
                    }
                }
                auditorias = auditoriasFiltrados;

            }
            return View(auditorias);
        }

        public ActionResult DetalleActaNotificacionVendedorAmbulante(int id)
        {
            Autorizar();
            auditoriaDAL = new AuditoriaDAL();
            ActaNotificacionVendedorAmbulanteViewModel modelo = (ActaNotificacionVendedorAmbulanteViewModel)CargarAuditoriaActaNotificacionVendedorAmbulante(auditoriaDAL.GetAuditoria(id));
            return View(modelo);
        }

        public AuditoriaViewModel CargarAuditoriaActaNotificacion(Auditorias auditoria)
        {
   
            tablaGeneralDAL = new TablaGeneralDAL();
            actaDeNotificacionDAL = new ActaDeNotificacionDAL();
            auditoriaDAL = new AuditoriaDAL();
            usuarioDAL = new UsuarioDAL();
            return new ActaNotificacionViewModel
            {
                IdAuditoria = auditoria.idAuditoria,
                IdCategoria = auditoria.idCategoria,
                VistaCategoria = tablaGeneralDAL.Get(auditoria.idCategoria).descripcion,
                IdElemento = auditoria.idElemento,
                VistaElemento = actaDeNotificacionDAL.GetActaDeNotificacion(auditoria.idElemento).numeroFolio,
                Fecha = auditoria.fecha,
                Accion = auditoria.accion,
                VistaAccion = tablaGeneralDAL.Get(auditoria.accion).descripcion,
                IdUsuario = auditoria.idUsuario,
                VistaUsuario = usuarioDAL.GetUsuario(auditoria.idUsuario).nombre,
            };
        }
        public ActionResult ListadoActaNotificacion(int id, string tiposAccion, string filtrosSeleccionado, string busquedaFechaInicioP, string busquedaFechaFinalP)
        {
            Autorizar();
            auditoriaDAL = new AuditoriaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            List<ActaNotificacionViewModel> auditorias = new List<ActaNotificacionViewModel>();
            List<ActaNotificacionViewModel> auditoriasFiltrados = new List<ActaNotificacionViewModel>();

            foreach (Auditorias auditoria in auditoriaDAL.Get())
            {
                if (id == auditoria.idElemento && tablaGeneralDAL.Get(auditoria.idCategoria).descripcion.Contains("Acta de Notificación"))
                {
                    auditorias.Add((ActaNotificacionViewModel)CargarAuditoriaActaNotificacion(auditoria));
                }
            }

            if (filtrosSeleccionado != "0" && tiposAccion != null)
            {
                foreach (ActaNotificacionViewModel auditoria in auditorias)
                {
                    if (filtrosSeleccionado == "1")
                    {
                        if (tablaGeneralDAL.Get(auditoria.Accion).descripcion.Contains(tiposAccion))
                        {
                            auditoriasFiltrados.Add(auditoria);
                        }
                    }
                }
                if (filtrosSeleccionado == "2")
                {
                    DateTime fechaInicio = DateTime.Parse(busquedaFechaInicioP);
                    DateTime fechaFinal = DateTime.Parse(busquedaFechaFinalP);
                    if (fechaInicio < fechaFinal)
                    {
                        if (auditoriaDAL.GetAuditoriasRango(fechaInicio, fechaFinal, id) != null)
                        {
                            foreach (Auditorias auditoriasFecha in auditoriaDAL.GetAuditoriasRango(fechaInicio, fechaFinal, id).ToList())
                            {
                                if (tablaGeneralDAL.Get(auditoriasFecha.idCategoria).descripcion.Contains("Acta de Notificación")) {
                                    auditoriasFiltrados.Add((ActaNotificacionViewModel)CargarAuditoriaActaNotificacion(auditoriasFecha));
                                }
                                }
                        }
                    }
                }
                auditorias = auditoriasFiltrados;

            }
            return View(auditorias);
        }

        public ActionResult DetalleActaNotificacion(int id)
        {
            Autorizar();
            auditoriaDAL = new AuditoriaDAL();
            ActaNotificacionViewModel modelo = (ActaNotificacionViewModel)CargarAuditoriaActaNotificacion(auditoriaDAL.GetAuditoria(id));
            return View(modelo);
        }

        public AuditoriaViewModel CargarAuditoriaActaEntrega(Auditorias auditoria)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            actaEntregaDAL = new ActaEntregaDAL();
            auditoriaDAL = new AuditoriaDAL();
            usuarioDAL = new UsuarioDAL();
            return new ActaEntregaViewModel
            {
                IdAuditoria = auditoria.idAuditoria,
                IdCategoria = auditoria.idCategoria,
                VistaCategoria = tablaGeneralDAL.Get(auditoria.idCategoria).descripcion,
                IdElemento = auditoria.idElemento,
                VistaElemento = actaEntregaDAL.GetActaEntrega(auditoria.idElemento).numeroFolio,
                Fecha = auditoria.fecha,
                Accion = auditoria.accion,
                VistaAccion = tablaGeneralDAL.Get(auditoria.accion).descripcion,
                IdUsuario = auditoria.idUsuario,
                VistaUsuario = usuarioDAL.GetUsuario(auditoria.idUsuario).nombre,
            };
        }
        public ActionResult ListadoActaEntrega(int id, string tiposAccion, string filtrosSeleccionado, string busquedaFechaInicioP, string busquedaFechaFinalP)
        {
            Autorizar();
            auditoriaDAL = new AuditoriaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            List<ActaEntregaViewModel> auditorias = new List<ActaEntregaViewModel>();
            List<ActaEntregaViewModel> auditoriasFiltrados = new List<ActaEntregaViewModel>();

            foreach (Auditorias auditoria in auditoriaDAL.Get())
            {
                if (id == auditoria.idElemento && tablaGeneralDAL.Get(auditoria.idCategoria).descripcion.Contains("Acta de Entrega"))
                {
                    auditorias.Add((ActaEntregaViewModel)CargarAuditoriaActaEntrega(auditoria));
                }
            }

            if (filtrosSeleccionado != "0" && tiposAccion != null)
            {
                foreach (ActaEntregaViewModel auditoria in auditorias)
                {
                    if (filtrosSeleccionado == "1")
                    {
                        if (tablaGeneralDAL.Get(auditoria.Accion).descripcion.Contains(tiposAccion))
                        {

                            auditoriasFiltrados.Add(auditoria);
                        }
                    }
                }
                if (filtrosSeleccionado == "2")
                {
                    DateTime fechaInicio = DateTime.Parse(busquedaFechaInicioP);
                    DateTime fechaFinal = DateTime.Parse(busquedaFechaFinalP);
                    if (fechaInicio < fechaFinal)
                    {
                        if (auditoriaDAL.GetAuditoriasRango(fechaInicio, fechaFinal, id) != null)
                        {
                            foreach (Auditorias auditoriasFecha in auditoriaDAL.GetAuditoriasRango(fechaInicio, fechaFinal, id).ToList())
                            {
                                if (tablaGeneralDAL.Get(auditoriasFecha.idCategoria).descripcion.Contains("Acta de Entrega")) { 
                                auditoriasFiltrados.Add((ActaEntregaViewModel)CargarAuditoriaActaEntrega(auditoriasFecha));
                                }
                            }
                        }
                    }
                }
                auditorias = auditoriasFiltrados;

            }
            return View(auditorias);
        }

        public ActionResult DetalleActaEntrega(int id)
        {
            Autorizar();
            auditoriaDAL = new AuditoriaDAL();
            ActaEntregaViewModel modelo = (ActaEntregaViewModel)CargarAuditoriaActaEntrega(auditoriaDAL.GetAuditoria(id));
            return View(modelo);
        }

        public AuditoriaViewModel CargarAuditoriaPartePolicial(Auditorias auditoria)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            parteDAL = new ParteDAL();
            auditoriaDAL = new AuditoriaDAL();
            usuarioDAL = new UsuarioDAL();
            return new Parte1ViewModel
            {
                IdAuditoria = auditoria.idAuditoria,
                IdCategoria = auditoria.idCategoria,
                VistaCategoria = tablaGeneralDAL.Get(auditoria.idCategoria).descripcion,
                IdElemento = auditoria.idElemento,
                VistaElemento = parteDAL.GetParte(auditoria.idElemento).numeroFolio,
                Fecha = auditoria.fecha,
                Accion = auditoria.accion,
                VistaAccion = tablaGeneralDAL.Get(auditoria.accion).descripcion,
                IdUsuario = auditoria.idUsuario,
                VistaUsuario = usuarioDAL.GetUsuario(auditoria.idUsuario).nombre,
            };
        }
        public ActionResult ListadoPartePolicial(int id, string tiposAccion, string filtrosSeleccionado, string busquedaFechaInicioP, string busquedaFechaFinalP)
        {
            Autorizar();
            auditoriaDAL = new AuditoriaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            List<Parte1ViewModel> auditorias = new List<Parte1ViewModel>();
            List<Parte1ViewModel> auditoriasFiltrados = new List<Parte1ViewModel>();

            foreach (Auditorias auditoria in auditoriaDAL.Get())
            {
                if (id == auditoria.idElemento && tablaGeneralDAL.Get(auditoria.idCategoria).descripcion.Contains("Parte Policial"))
                {
                    auditorias.Add((Parte1ViewModel)CargarAuditoriaPartePolicial(auditoria));
                }
            }

            if (filtrosSeleccionado != "0" && tiposAccion != null)
            {
                foreach (Parte1ViewModel auditoria in auditorias)
                {
                    if (filtrosSeleccionado == "1")
                    {
                        if (tablaGeneralDAL.Get(auditoria.Accion).descripcion.Contains(tiposAccion))
                        {

                            auditoriasFiltrados.Add(auditoria);
                        }
                    }
                }
                if (filtrosSeleccionado == "2")
                {
                    DateTime fechaInicio = DateTime.Parse(busquedaFechaInicioP);
                    DateTime fechaFinal = DateTime.Parse(busquedaFechaFinalP);
                    if (fechaInicio < fechaFinal)
                    {
                        if (auditoriaDAL.GetAuditoriasRango(fechaInicio, fechaFinal, id) != null)
                        {
                            foreach (Auditorias auditoriasFecha in auditoriaDAL.GetAuditoriasRango(fechaInicio, fechaFinal, id).ToList())
                            {
                                if (tablaGeneralDAL.Get(auditoriasFecha.idCategoria).descripcion.Contains("Parte Policial"))
                                {
                                    auditoriasFiltrados.Add((Parte1ViewModel)CargarAuditoriaPartePolicial(auditoriasFecha));
                                }
                            }
                        }
                    }
                }
                auditorias = auditoriasFiltrados;

            }
            return View(auditorias);
        }

        public ActionResult DetallePartePolicial(int id)
        {
            Autorizar();
            auditoriaDAL = new AuditoriaDAL();
            Parte1ViewModel modelo = (Parte1ViewModel)CargarAuditoriaPartePolicial(auditoriaDAL.GetAuditoria(id));
            return View(modelo);
        }

    }


}