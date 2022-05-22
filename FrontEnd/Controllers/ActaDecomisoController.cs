﻿using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using BackEnd.DAL;
using BackEnd;
using FrontEnd.Models;
using FrontEnd.Models.ViewModels;
using System.IO;

namespace FrontEnd.Controllers
{
    public class ActaDecomisoController : Controller
    {
        ITablaGeneralDAL tablaGeneralDAL;
        IActaDecomisoDAL actaDecomisoDAL;
        IPoliciaDAL policiaDAL;
        IAuditoriaDAL auditoriaDAL;
        IUsuarioDAL usuarioDAL;
        IPersonaDAL personaDAL;
        public ActasDecomiso ConvertirActaDecomiso(ActaDecomisoViewModel modelo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            personaDAL = new PersonaDAL();
            return new ActasDecomiso
            {
                idActaDecomiso = modelo.IdActaDecomiso,
                numeroFolio = modelo.NumeroFolio,
                fecha = modelo.Fecha,
                idDecomisado = personaDAL.GetPersonaIdentificacion(modelo.IdDecomisado).idPersona,
                estadoCivilDecomisado = tablaGeneralDAL.GetCodigo("Generales", "estadoCivil", modelo.EstadoCivil.ToString()).idTablaGeneral,
                lugarProcedimiento = modelo.LugarDelProcedimiento,
                inventario = modelo.Inventario,
                observaciones = modelo.Observaciones,
                oficialAcompanante = policiaDAL.GetPoliciaCedula(modelo.OficialAcompanante).idPolicia,
                oficialActuante = policiaDAL.GetPoliciaCedula(modelo.OficialActuante).idPolicia,
                supervisorDecomiso = policiaDAL.GetPoliciaCedula(modelo.Supervisor).idPolicia,
                estado = tablaGeneralDAL.GetCodigo("Actas", "estadoActa", modelo.Estado.ToString()).idTablaGeneral
            };
        }
        public ActaDecomisoViewModel CargarActaDecomiso(ActasDecomiso actaDecomiso)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            personaDAL = new PersonaDAL();
          
                ActaDecomisoViewModel acta = new ActaDecomisoViewModel();
            {
                acta.IdActaDecomiso = actaDecomiso.idActaDecomiso;
                acta.NumeroFolio = actaDecomiso.numeroFolio;
                acta.OficialAcompanante = policiaDAL.GetPolicia(actaDecomiso.oficialAcompanante).cedula;
                acta.OficialActuante = policiaDAL.GetPolicia(actaDecomiso.oficialActuante).cedula;
                acta.Supervisor = policiaDAL.GetPolicia(actaDecomiso.supervisorDecomiso).cedula;
                acta.EstadoCivil = int.Parse(tablaGeneralDAL.Get(actaDecomiso.estadoCivilDecomisado).codigo);
                acta.VistaTipoEstadoCivil = tablaGeneralDAL.Get(actaDecomiso.estadoCivilDecomisado).descripcion;
                acta.Fecha = actaDecomiso.fecha;
                acta.Hora = actaDecomiso.fecha;
                acta.NombreDecomisado = personaDAL.GetPersona(actaDecomiso.idDecomisado).nombre;
                acta.IdDecomisado = personaDAL.GetPersona(actaDecomiso.idDecomisado).identificacion;
                //acta.TelefonoDecomisado = personaDAL.GetPersona(actaDecomiso.idDecomisado).telefonoCelular;
                //acta.DireccionDecomisado = personaDAL.GetPersona(actaDecomiso.idDecomisado).direccionPersona;
                acta.LugarDelProcedimiento = actaDecomiso.lugarProcedimiento;
                acta.Inventario = actaDecomiso.inventario;
                acta.Observaciones = actaDecomiso.observaciones;
                acta.Estado = int.Parse(tablaGeneralDAL.Get(actaDecomiso.estado).codigo);
                acta.VistaEstadoActa = tablaGeneralDAL.Get(actaDecomiso.estado).descripcion;
                acta.VistaOficialAcompanante = policiaDAL.GetPolicia(actaDecomiso.oficialAcompanante).nombre;
                acta.VistaOficialActuante = policiaDAL.GetPolicia(actaDecomiso.oficialActuante).nombre;
                acta.VistaPoliciaSupervisor = policiaDAL.GetPolicia(actaDecomiso.supervisorDecomiso).nombre;
             if (personaDAL.GetPersona(actaDecomiso.idDecomisado).telefonoCelular != null)
            {
                    acta.TelefonoDecomisado = personaDAL.GetPersona(actaDecomiso.idDecomisado).telefonoCelular;
                }
            else
            {
               acta.TelefonoDecomisado = "No Aplica";
            }
                if (personaDAL.GetPersona(actaDecomiso.idDecomisado).direccionPersona != null)
                {
                    acta.DireccionDecomisado = personaDAL.GetPersona(actaDecomiso.idDecomisado).direccionPersona;
                }
                else
                {
                    acta.DireccionDecomisado = "No Aplica";
                }

            };return acta;
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
                    if (policia.nombre.Contains(nombre))
                    {
                        policiasFiltrados.Add(policia);
                    }
                }
            }
            policiasFiltrados = policiasFiltrados.OrderBy(x => x.nombre).ToList();

            return PartialView("_ListaPoliciasParcial", ConvertirListaPoliciasFiltrados(policiasFiltrados));
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
            List<Personas> personasFiltradas = new List<Personas>();
            foreach (Personas persona in personas)
            {
                if (persona.nombre.Contains(nombre))
                {
                    if (tablaGeneralDAL.Get(persona.tipoIdentificacion).descripcion != "Cédula Jurídica")
                    {
                        personasFiltradas.Add(persona);
                    }
                }
            }
            personasFiltradas = personasFiltradas.OrderBy(x => x.nombre).ToList();
            return PartialView("_ListaPersonasParcial", ConvertirListaPersonasFiltrados(personasFiltradas));
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
        public void AutorizarEditar()
        {
            if (Session["userID"] != null)
            {
                if (Session["Rol"].ToString() == "4" || Session["Rol"].ToString() == "1")
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
        public ActionResult Index(string filtrosSeleccionado, string busqueda, string busquedaFechaInicioH, string busquedaFechaFinalH)
        {
            Autorizar();
            actaDecomisoDAL = new ActaDecomisoDAL();
            policiaDAL = new PoliciaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            List<ActaDecomisoViewModel> actasDecomiso = new List<ActaDecomisoViewModel>();
            List<ActaDecomisoViewModel> actasDecomisoFiltradas = new List<ActaDecomisoViewModel>();
            List<TablaGeneral> comboindex = tablaGeneralDAL.Get("ActasDecomiso", "index");
            List<SelectListItem> items = comboindex.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.descripcion
                };
            });
            ViewBag.items = items;
            foreach (ActasDecomiso actaDecomiso in actaDecomisoDAL.Get())
            {
                actasDecomiso.Add(CargarActaDecomiso(actaDecomiso));
            }
            if (busqueda != null)
            {

                foreach (ActaDecomisoViewModel actaDecomiso in actasDecomiso)
                {
                    if (filtrosSeleccionado == "Número de Folio")
                    {
                        if (actaDecomiso.NumeroFolio.Contains(busqueda))
                        {
                            actasDecomisoFiltradas.Add(actaDecomiso);
                        }
                    }
                    if (filtrosSeleccionado == "Nombre Policía Actuante")
                    {
                        if (policiaDAL.GetPoliciaCedula(actaDecomiso.OficialActuante).nombre.Contains(busqueda))
                        {
                            actasDecomisoFiltradas.Add(actaDecomiso);
                        }
                    }
                    if (filtrosSeleccionado == "Nombre a Quién Decomisan")
                    {
                        if (actaDecomiso.NombreDecomisado.Contains(busqueda))
                        {
                            actasDecomisoFiltradas.Add(actaDecomiso);
                        }
                    }
                }
                if (filtrosSeleccionado == "Fecha")
                {
                    DateTime fechaInicio = DateTime.Parse(busquedaFechaInicioH);
                    DateTime fechaFinal = DateTime.Parse(busquedaFechaFinalH);
                    if (fechaInicio < fechaFinal)
                    {
                        if (actaDecomisoDAL.GetActaDecomisoRango(fechaInicio, fechaFinal) != null)
                        {
                            foreach (ActasDecomiso actaDecomisoFecha in actaDecomisoDAL.GetActaDecomisoRango(fechaInicio, fechaFinal).ToList())
                            {
                                actasDecomisoFiltradas.Add(CargarActaDecomiso(actaDecomisoFecha));
                            }
                        }
                    }
                }
                actasDecomiso = actasDecomisoFiltradas;
            }
            return View(actasDecomiso.OrderBy(x => x.NumeroFolio).ToList());

        }
        public ActionResult Nuevo()
        {
            Autorizar();
            tablaGeneralDAL = new TablaGeneralDAL();
            ActaDecomisoViewModel modelo = new ActaDecomisoViewModel()
            {
                EstadosCivil = tablaGeneralDAL.Get("Generales", "estadoCivil").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                Fecha = DateTime.Today,
                Estados = tablaGeneralDAL.Get("Actas", "estadoActa").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo })
            };
            return View(modelo);
        }



        [HttpPost]
        public ActionResult Nuevo(ActaDecomisoViewModel model)
        {
            Autorizar();
            actaDecomisoDAL = new ActaDecomisoDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            model.EstadosCivil = tablaGeneralDAL.Get("Generales", "estadoCivil").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.NumeroFolio = (actaDecomisoDAL.GetCount() + 1).ToString() + "-" + DateTime.Now.Year;
            model.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "1").idTablaGeneral;
            model.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "9").idTablaGeneral;
            model.IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario;
            model.Estado = int.Parse(tablaGeneralDAL.GetCodigo("Actas", "estadoActa", "1").codigo);
            DateTime newDateTime = model.Fecha.Date + model.Hora.TimeOfDay;
            model.Fecha = newDateTime;

            try
            {
                if (ModelState.IsValid)
                {
                    actaDecomisoDAL.Add(ConvertirActaDecomiso(model));
                    int aux = actaDecomisoDAL.GetActaDecomisoFolio(model.NumeroFolio).idActaDecomiso;
                    model.IdElemento = actaDecomisoDAL.GetActaDecomisoFolio(model.NumeroFolio).idActaDecomiso;
                    auditoriaDAL.Add(ConvertirAuditoria(model));
                    return Redirect("~/ActaDecomiso/Detalle/" + aux);

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
      //      Autorizar();
            actaDecomisoDAL = new ActaDecomisoDAL();
            Session["idActaDecomiso"] = id;
            Session["numeroFolio"] = actaDecomisoDAL.GetActaDecomiso(id).numeroFolio;
            ActaDecomisoViewModel modelo = CargarActaDecomiso(actaDecomisoDAL.GetActaDecomiso(id));
            return View(modelo);


        }


        //Devuelve la página de edición de policías con sus apartados llenos
        public ActionResult Editar(int id)
        {
            AutorizarEditar();
            tablaGeneralDAL = new TablaGeneralDAL();
            actaDecomisoDAL = new ActaDecomisoDAL();
            ActaDecomisoViewModel modelo = CargarActaDecomiso(actaDecomisoDAL.GetActaDecomiso(id));
            modelo.EstadosCivil = tablaGeneralDAL.Get("Generales", "estadoCivil").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            modelo.Estados = tablaGeneralDAL.Get("Actas", "estadoActa").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            return View(modelo);
        }




        [HttpPost]
        public ActionResult Editar(ActaDecomisoViewModel model)
        {
            AutorizarEditar();
            actaDecomisoDAL = new ActaDecomisoDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            model.EstadosCivil = tablaGeneralDAL.Get("Generales", "estadoCivil").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.Estados = tablaGeneralDAL.Get("Actas", "estadoActa").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            DateTime newDateTime = model.Fecha.Date + model.Hora.TimeOfDay;
            model.Fecha = newDateTime;
            model.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "2").idTablaGeneral;
            model.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "9").idTablaGeneral;
            model.IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario;
            int estado = actaDecomisoDAL.GetActaDecomiso(model.IdActaDecomiso).estado;
            try
            {
                if (ModelState.IsValid)
                {
                    if (tablaGeneralDAL.GetCodigo("Actas", "estadoActa", model.Estado.ToString()).idTablaGeneral != estado && model.IdActaDecomiso != 0)
                    {
                        auditoriaDAL.Add(CambiarEstadoAuditoria(model.IdActaDecomiso));
                    }
                    actaDecomisoDAL.Edit(ConvertirActaDecomiso(model));
                    model.IdElemento = actaDecomisoDAL.GetActaDecomisoFolio(model.NumeroFolio).idActaDecomiso;
                    auditoriaDAL.Add(ConvertirAuditoria(model));
                    return Redirect("~/ActaDecomiso/Detalle/" + model.IdActaDecomiso);
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Auditorias ConvertirAuditoria(ActaDecomisoViewModel modelo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            actaDecomisoDAL = new ActaDecomisoDAL();
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
        public Auditorias CambiarEstadoAuditoria(int idActaDecomiso)
        {
            ActaDecomisoViewModel modelo = new ActaDecomisoViewModel();
            tablaGeneralDAL = new TablaGeneralDAL();
            actaDecomisoDAL = new ActaDecomisoDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            return new Auditorias
            {
                idAuditoria = modelo.IdAuditoria,
                accion = modelo.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "3").idTablaGeneral,
                idCategoria = modelo.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "9").idTablaGeneral,
                idUsuario = modelo.IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario,
                fecha = DateTime.Now,
                idElemento = actaDecomisoDAL.GetActaDecomiso(idActaDecomiso).idActaDecomiso

            };
        }

    }
}