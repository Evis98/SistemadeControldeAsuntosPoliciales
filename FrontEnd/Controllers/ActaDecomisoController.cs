using System;
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
        public ActasDecomiso ConvertirActaDecomiso(ActaDecomisoViewModel modelo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();

            return new ActasDecomiso
            {
                idActaDecomiso = modelo.IdActaDecomiso,
                numeroFolio = modelo.NumeroFolio,                
                fecha = modelo.Fecha,
                nombreDecomisado = modelo.NombreDecomisado,
                numeroDeIdentificacionDecomisado = modelo.CedulaDecomisado,
                estadoCivilDecomisado = tablaGeneralDAL.GetCodigo("Generales", "estadoCivil", modelo.EstadoCivil.ToString()).idTablaGeneral,
                telefonoDecomisado = modelo.TelefonoDecomisado,
                direccionDecomisado = modelo.DireccionDecomisado,
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

            return new ActaDecomisoViewModel
            {
                IdActaDecomiso = actaDecomiso.idActaDecomiso,
                NumeroFolio = actaDecomiso.numeroFolio,
                OficialAcompanante = policiaDAL.GetPolicia(actaDecomiso.oficialAcompanante).cedula,
                OficialActuante = policiaDAL.GetPolicia(actaDecomiso.oficialActuante).cedula,
                Supervisor = policiaDAL.GetPolicia(actaDecomiso.supervisorDecomiso).cedula,
                EstadoCivil = int.Parse(tablaGeneralDAL.Get(actaDecomiso.estadoCivilDecomisado).codigo),
                VistaTipoEstadoCivil = tablaGeneralDAL.Get(actaDecomiso.estadoCivilDecomisado).descripcion,
                Fecha = actaDecomiso.fecha,
                Hora = actaDecomiso.fecha,
                NombreDecomisado = actaDecomiso.nombreDecomisado,
                CedulaDecomisado = actaDecomiso.numeroDeIdentificacionDecomisado,
                TelefonoDecomisado = actaDecomiso.telefonoDecomisado,
                DireccionDecomisado = actaDecomiso.direccionDecomisado,
                LugarDelProcedimiento = actaDecomiso.lugarProcedimiento,
                Inventario = actaDecomiso.inventario,
                Observaciones = actaDecomiso.observaciones,
                Estado = int.Parse(tablaGeneralDAL.Get(actaDecomiso.estado).codigo),
                VistaEstadoActa = tablaGeneralDAL.Get(actaDecomiso.estado).descripcion,
                VistaOficialAcompanante = policiaDAL.GetPolicia(actaDecomiso.oficialAcompanante).nombre,
                VistaOficialActuante = policiaDAL.GetPolicia(actaDecomiso.oficialActuante).nombre,
                VistaPoliciaSupervisor = policiaDAL.GetPolicia(actaDecomiso.supervisorDecomiso).nombre
            };
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

        //[Authorize(Users = "juanpablo@scap.com")]
        public ActionResult Index(string filtrosSeleccionado, string busqueda, string busquedaFechaInicioH, string busquedaFechaFinalH)
        {
            if (Session["userID"] != null)
            {
        
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
            else
            {
                return Redirect("~/Shared/Error.cshtml");
            }
        }
        public ActionResult Nuevo()
        {
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
            actaDecomisoDAL = new ActaDecomisoDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            model.EstadosCivil = tablaGeneralDAL.Get("Generales", "estadoCivil").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.NumeroFolio = (actaDecomisoDAL.GetCount() + 1).ToString() + "-" + DateTime.Now.Year;
            model.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "1").idTablaGeneral;
            model.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "9").idTablaGeneral;
            model.IdUsuario = usuarioDAL.GetUsuario(1).idUsuario;
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
            actaDecomisoDAL = new ActaDecomisoDAL();
            Session["idActaDecomiso"] = id;     
            Session["numeroFolio"] = actaDecomisoDAL.GetActaDecomiso(id).numeroFolio;
            ActaDecomisoViewModel modelo = CargarActaDecomiso(actaDecomisoDAL.GetActaDecomiso(id));
            return View(modelo);
        }

        //Devuelve la página de edición de policías con sus apartados llenos
        public ActionResult Editar(int id)
        {
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
            model.IdUsuario = usuarioDAL.GetUsuario(1).idUsuario;
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
        //public ActionResult CambioEstadoActa(int id)
        //{
        //    int estado;
        //    actaDecomisoDAL = new ActaDecomisoDAL();
        //    tablaGeneralDAL = new TablaGeneralDAL();
        //    try
        //    {
        //        if (tablaGeneralDAL.Get((int)id).descripcion == "Activa")
        //        {
        //            estado = tablaGeneralDAL.Get("Actas", "estadoActa", "Inactiva").idTablaGeneral;
        //        }
        //        else
        //        {
        //            estado = tablaGeneralDAL.Get("Actas", "estadoActa", "Activa").idTablaGeneral;
        //        }
        //        actaDecomisoDAL.CambiaEstadoActa((int)Session["idActaDecomiso"], estado);
        //        return Redirect("~/ActaDecomiso/Detalle/" + Session["idActaDecomiso"]);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

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
                idUsuario = modelo.IdUsuario = usuarioDAL.GetUsuario(1).idUsuario,
                fecha = DateTime.Now,
                idElemento = actaDecomisoDAL.GetActaDecomiso(idActaDecomiso).idActaDecomiso

            };
        }

    }
}