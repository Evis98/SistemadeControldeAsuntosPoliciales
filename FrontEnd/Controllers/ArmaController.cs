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
        IAuditoriaDAL auditoriaDAL;
        IUsuarioDAL usuarioDAL;
        public Armas ConvertirArma(ArmaViewModel modelo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            return new Armas
            {
                idArma = modelo.IdArma,
                numeroSerie = modelo.NumeroSerie,
                tipoArma = tablaGeneralDAL.GetCodigo("Armas", "tipoArma", modelo.TipoArma.ToString()).idTablaGeneral,
                marca = tablaGeneralDAL.GetCodigo("Armas", "marca", modelo.Marca.ToString()).idTablaGeneral,
                modelo = modelo.ModeloArma,
                calibre = tablaGeneralDAL.GetCodigo("Armas", "calibre", modelo.Calibre.ToString()).idTablaGeneral,
                condicion = tablaGeneralDAL.GetCodigo("Armas", "condicion", modelo.Condicion.ToString()).idTablaGeneral,
                ubicacion = tablaGeneralDAL.GetCodigo("Armas", "ubicacion", modelo.Ubicacion.ToString()).idTablaGeneral,
                observacion = modelo.Observacion,
                estadoArma = tablaGeneralDAL.GetCodigo("Generales","estado","1").idTablaGeneral,
            };
        }
        public ArmaViewModel CargarArma(Armas arma)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            ArmaViewModel armaCarga = new ArmaViewModel();
            {
                armaCarga.IdArma = arma.idArma;
                armaCarga.NumeroSerie = arma.numeroSerie;
                armaCarga.TipoArma = int.Parse(tablaGeneralDAL.Get(arma.tipoArma).codigo);
                armaCarga.VistaTipoArma = tablaGeneralDAL.Get(arma.tipoArma).descripcion;
                armaCarga.Marca = int.Parse(tablaGeneralDAL.Get(arma.marca).codigo);
                armaCarga.VistaMarca = tablaGeneralDAL.Get(arma.marca).descripcion;
                armaCarga.ModeloArma = arma.modelo;
                armaCarga.Calibre = int.Parse(tablaGeneralDAL.Get(arma.calibre).codigo);
                armaCarga.VistaCalibre = tablaGeneralDAL.Get(arma.calibre).descripcion;
                armaCarga.Condicion = int.Parse(tablaGeneralDAL.Get(arma.condicion).codigo);
                armaCarga.VistaCondicion = tablaGeneralDAL.Get(arma.condicion).descripcion;
                armaCarga.Ubicacion = int.Parse(tablaGeneralDAL.Get(arma.ubicacion).codigo);
                armaCarga.VistaUbicacion = tablaGeneralDAL.Get(arma.ubicacion).descripcion;
                armaCarga.Observacion = arma.observacion;
                armaCarga.EstadoArma = arma.estadoArma;
                armaCarga.VistaEstadoArma = tablaGeneralDAL.Get(arma.estadoArma).descripcion;
                if (arma.policiaAsignado != null)
                {
                    armaCarga.PoliciaAsignado = policiaDAL.GetPolicia((int)arma.policiaAsignado).cedula;
                    armaCarga.NombrePolicia = policiaDAL.GetPolicia((int)arma.policiaAsignado).nombre;
                }
                else
                {
                    armaCarga.NombrePolicia = "NO ASIGNADO";
                }
            };
            return armaCarga;
        }

        public void Autorizar()
        {
            if (Session["userID"] != null)
            {
                if (Session["Rol"].ToString() == "1")
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

        public ActionResult Index(string filtrosSeleccionado, string busqueda)
        {
            Autorizar();
            armaDAL = new ArmaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            List<ArmaViewModel> armas = new List<ArmaViewModel>();
            List<ArmaViewModel> armasFiltradas = new List<ArmaViewModel>();
            List<TablaGeneral> comboindex = tablaGeneralDAL.Get("Armas", "index");
            List<SelectListItem> items = comboindex.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.descripcion
                };
            });
            ViewBag.items = items;
            foreach (Armas arma in armaDAL.Get())
            {
                armas.Add(CargarArma(arma));
            }
            if (busqueda != null && filtrosSeleccionado != "")
            {
                foreach (ArmaViewModel arma in armas)
                {
                    if (filtrosSeleccionado == "Policía Asignado")
                    {
                        if (arma.NombrePolicia.Contains(busqueda))
                        {
                            armasFiltradas.Add(arma);
                        }
                    }
                    if (filtrosSeleccionado == "Número de Serie")
                    {
                        if (arma.NumeroSerie.Contains(busqueda))
                        {
                            armasFiltradas.Add(arma);
                        }
                    }
                }
                armas = armasFiltradas;
            }
            return View(armas.OrderByDescending(x => x.PoliciaAsignado).ToList());
    }
            

        public ActionResult Nuevo()
        {
            Autorizar();
            tablaGeneralDAL = new TablaGeneralDAL();
            ArmaViewModel modelo = new ArmaViewModel()
            {
                TiposMarcas = tablaGeneralDAL.Get("Armas", "marca").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                TiposArma = tablaGeneralDAL.Get("Armas", "tipoArma").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                TiposCalibre = tablaGeneralDAL.Get("Armas", "calibre").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                TiposCondicion = tablaGeneralDAL.Get("Armas", "condicion").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                TiposUbicacion = tablaGeneralDAL.Get("Armas", "ubicacion").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
            };
            return View(modelo);
        }

        //Guarda la información ingresada en la página para crear policías
        [HttpPost]
        public ActionResult Nuevo(ArmaViewModel model)
        {
            Autorizar();
            armaDAL = new ArmaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            model.SerieFiltrada = armaDAL.GetSerieArma(model.NumeroSerie);
            model.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "1").idTablaGeneral;
            model.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "5").idTablaGeneral;
            model.IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario;
            try
            {
                if (!armaDAL.SerieExiste(model.NumeroSerie))
                {
                    if (ModelState.IsValid)
                    {
                        Armas arma = ConvertirArma(model);
                        armaDAL.Add(arma);
                        model.IdElemento = armaDAL.GetArmaNumSerie(model.NumeroSerie).idArma;
                        auditoriaDAL.Add(ConvertirAuditoria(model));
                        int aux = armaDAL.GetArmaNumSerie(model.NumeroSerie).idArma;                    
                        return Redirect("~/Arma/Detalle/" + aux);
                    }
                }
                model.TiposArma = tablaGeneralDAL.Get("Armas", "tipoArma").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                model.TiposCalibre = tablaGeneralDAL.Get("Armas", "calibre").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                model.TiposCondicion = tablaGeneralDAL.Get("Armas", "condicion").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                model.TiposUbicacion = tablaGeneralDAL.Get("Armas", "ubicacion").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                model.TiposMarcas = tablaGeneralDAL.Get("Armas", "marca").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public ActionResult Detalle(int id)
        {
            Autorizar();
            armaDAL = new ArmaDAL();
            Session["idArma"] = id;           
            Session["numeroSerie"] = armaDAL.GetArma(id).numeroSerie;

            ArmaViewModel modelo = CargarArma(armaDAL.GetArma(id));
            if (modelo.PoliciaAsignado != null)
            {
                modelo.NombrePolicia = policiaDAL.GetPoliciaCedula(modelo.PoliciaAsignado).cedula + " " + policiaDAL.GetPoliciaCedula (modelo.PoliciaAsignado).nombre;
            }
            else
            {
                modelo.NombrePolicia = "NO ASIGNADO";
            }
          
            return View(modelo);
        }
        public ActionResult Editar(int id)
        {
            AutorizarEditar();
            armaDAL = new ArmaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            ArmaViewModel modelo = CargarArma(armaDAL.GetArma(id));
            modelo.TiposArma = tablaGeneralDAL.Get("Armas", "tipoArma").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            modelo.TiposCalibre = tablaGeneralDAL.Get("Armas", "calibre").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            modelo.TiposCondicion = tablaGeneralDAL.Get("Armas", "condicion").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            modelo.TiposUbicacion = tablaGeneralDAL.Get("Armas", "ubicacion").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            modelo.TiposMarcas = tablaGeneralDAL.Get("Armas", "marca").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            if (modelo.PoliciaAsignado != null)
            {
                modelo.NombrePolicia = policiaDAL.GetPoliciaCedula(modelo.PoliciaAsignado).cedula + " " + policiaDAL.GetPoliciaCedula(modelo.PoliciaAsignado).nombre;
            }
            else
            {
                modelo.NombrePolicia = "NO ASIGNADO";
            }
            return View(modelo);
        }

        [HttpPost]
        public ActionResult Editar(ArmaViewModel modelo)
        {
            AutorizarEditar();
            armaDAL = new ArmaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            modelo.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "2").idTablaGeneral;
            modelo.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "5").idTablaGeneral;
            modelo.IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario;
            try
            {
                if (ModelState.IsValid)
                {
                    Armas arma = ConvertirArma(modelo);
                    if (modelo.PoliciaAsignado != null)
                    {
                        arma.policiaAsignado = policiaDAL.GetPoliciaCedula(modelo.PoliciaAsignado).idPolicia;
                    }
                    else
                    {
                        arma.policiaAsignado = null;
                    }
                    armaDAL.Edit(arma);
                    modelo.IdElemento = armaDAL.GetArmaNumSerie(modelo.NumeroSerie).idArma;
                    auditoriaDAL.Add(ConvertirAuditoria(modelo));
                    return Redirect("~/Arma/Detalle/" + modelo.IdArma);
                }
               
                return View(modelo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult CambioEstadoArma(int estadoArma, string justificacion, int idArma)
        {
            Autorizar();
            int estadoFinal;
            armaDAL = new ArmaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
             auditoriaDAL = new AuditoriaDAL();
            try
            {
                if (tablaGeneralDAL.Get((int)estadoArma).descripcion == "Activo")
                {
                    estadoFinal = tablaGeneralDAL.Get("Generales", "estado", "Inactivo").idTablaGeneral;
                }
                else
                {
                    estadoFinal = tablaGeneralDAL.Get("Generales", "estado", "Activo").idTablaGeneral;
                }
                armaDAL.CambiaEstadoArma((int)Session["idArma"], estadoFinal);
                auditoriaDAL.Add(CambiarEstadoArma(justificacion, idArma));
                return Redirect("~/Arma/Detalle/" + Session["idArma"]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Auditorias ConvertirAuditoria(ArmaViewModel modelo)
        {

            tablaGeneralDAL = new TablaGeneralDAL();
            armaDAL = new ArmaDAL();
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

        public Auditorias CambiarEstadoArma(string justificacion, int idArma)
        {
            ArmaViewModel modelo = new ArmaViewModel();
            tablaGeneralDAL = new TablaGeneralDAL();
            armaDAL = new ArmaDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            return new Auditorias
            {
                idAuditoria = modelo.IdAuditoria,
                accion = modelo.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "3").idTablaGeneral,
                idCategoria = modelo.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "5").idTablaGeneral,
                idUsuario = modelo.IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario,
                fecha = DateTime.Now,
                justificacion = justificacion,
                idElemento = armaDAL.GetArma(idArma).idArma
                

            };
        }
    }
}