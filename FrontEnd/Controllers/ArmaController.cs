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

        //Metodos Útiles
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
        public Armas ConvertirArma(ArmaViewModel modelo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();

            Armas arma = new Armas()
            {
                idArma = modelo.IdArma,
                numeroSerie = modelo.NumeroSerie.ToUpper(),
                tipoArma = tablaGeneralDAL.GetCodigo("Armas", "tipoArma", modelo.TipoArma.ToString()).idTablaGeneral,
                marca = tablaGeneralDAL.GetCodigo("Armas", "marca", modelo.Marca.ToString()).idTablaGeneral,
                modelo = modelo.ModeloArma.ToUpper(),
                calibre = tablaGeneralDAL.GetCodigo("Armas", "calibre", modelo.Calibre.ToString()).idTablaGeneral,
                condicion = tablaGeneralDAL.GetCodigo("Armas", "condicion", modelo.Condicion.ToString()).idTablaGeneral,
                ubicacion = tablaGeneralDAL.GetCodigo("Armas", "ubicacion", modelo.Ubicacion.ToString()).idTablaGeneral,
                
            };
            if (modelo.EstadoArma == 0)
            {
                arma.estadoArma = tablaGeneralDAL.Get("Generales", "estado", "Activo").idTablaGeneral;
            }
            else
            {
                arma.estadoArma = modelo.EstadoArma;
            }
            if (modelo.PoliciaAsignado != null)
            {
                arma.policiaAsignado = policiaDAL.GetPoliciaCedula(modelo.PoliciaAsignado).idPolicia;
            }
            else
            {
                arma.policiaAsignado = null;
            }
            if (modelo.Observacion != null) {
                arma.observacion = modelo.Observacion.ToUpper();
            }
            return arma;
        }
        public ArmaViewModel CargarArma(Armas arma)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            ArmaViewModel modelo = new ArmaViewModel()
            {
                IdArma = arma.idArma,
                NumeroSerie = arma.numeroSerie,
                TipoArma = int.Parse(tablaGeneralDAL.Get(arma.tipoArma).codigo),
                VistaTipoArma = tablaGeneralDAL.Get(arma.tipoArma).descripcion,
                Marca = int.Parse(tablaGeneralDAL.Get(arma.marca).codigo),
                VistaMarca = tablaGeneralDAL.Get(arma.marca).descripcion,
                ModeloArma = arma.modelo,
                Calibre = int.Parse(tablaGeneralDAL.Get(arma.calibre).codigo),
                VistaCalibre = tablaGeneralDAL.Get(arma.calibre).descripcion,
                Condicion = int.Parse(tablaGeneralDAL.Get(arma.condicion).codigo),
                VistaCondicion = tablaGeneralDAL.Get(arma.condicion).descripcion,
                Ubicacion = int.Parse(tablaGeneralDAL.Get(arma.ubicacion).codigo),
                VistaUbicacion = tablaGeneralDAL.Get(arma.ubicacion).descripcion,
                Observacion = arma.observacion,
                EstadoArma = arma.estadoArma,
                VistaEstadoArma = tablaGeneralDAL.Get(arma.estadoArma).descripcion

            };
            if (arma.policiaAsignado != null)
            {
                modelo.PoliciaAsignado = policiaDAL.GetPolicia((int)arma.policiaAsignado).cedula;
                modelo.NombrePolicia = policiaDAL.GetPolicia((int)arma.policiaAsignado).nombre;
            }
            else
            {
                modelo.NombrePolicia = "NO ASIGNADO";
            }
            return modelo;
        }
        public ActionResult CambioEstado()
        {
            Autorizar();
            armaDAL = new ArmaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            auditoriaDAL = new AuditoriaDAL();
            Armas arma = armaDAL.GetArma((int)Session["idArma"]);
            if (tablaGeneralDAL.Get(arma.estadoArma).descripcion == "Activo")
            {
                arma.estadoArma = tablaGeneralDAL.Get("Generales", "estado", "Inactivo").idTablaGeneral;
            }
            else
            {
                arma.estadoArma = tablaGeneralDAL.Get("Generales", "estado", "Activo").idTablaGeneral;
            }
            armaDAL.Edit(arma);
            auditoriaDAL.Add(CambiarEstadoAuditoria((int)Session["idArma"]));
            return Redirect("~/Arma/Detalle/" + Session["idArma"]);
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

        public Auditorias CambiarEstadoAuditoria(int idArma)
        {
            AuditoriaViewModel modelo = new AuditoriaViewModel();
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
                idElemento = armaDAL.GetArma(idArma).idArma


            };
        }

        //Metodos de las Vistas
        public ActionResult Index(string filtrosSeleccionado, string busqueda)
        {
            Autorizar();
            armaDAL = new ArmaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();

            //Carga combobox busqueda
            List<TablaGeneral> comboindex = tablaGeneralDAL.Get("Armas", "index");
            List<SelectListItem> items = comboindex.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.descripcion
                };
            });
            ViewBag.items = items;

            //Carga lista de armas
            List<ArmaViewModel> armas = new List<ArmaViewModel>();
            List<ArmaViewModel> armasFiltradas = new List<ArmaViewModel>();
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

        [HttpPost]
        public ActionResult Nuevo(ArmaViewModel modelo)
        {
            Autorizar();
            armaDAL = new ArmaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();

            AuditoriaViewModel auditoria_model = new AuditoriaViewModel
            {
                Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "1").idTablaGeneral,
                IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "5").idTablaGeneral,
                IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario
            };
            try
            {
                if (ModelState.IsValid)
                {
                    int errores = 0;
                    if (armaDAL.ArmaExiste(modelo.NumeroSerie))
                    {
                        ModelState.AddModelError(nameof(modelo.NumeroSerie), "El número de serie ingresado ya existe");
                        errores++;
                    }
                    if (errores == 0)
                    {
                        Armas arma = ConvertirArma(modelo);
                        armaDAL.Add(arma);
                        auditoria_model.IdElemento = armaDAL.GetArmaNumSerie(modelo.NumeroSerie).idArma;
                        auditoriaDAL.Add(ConvertirAuditoria(auditoria_model));
                        int aux = armaDAL.GetArmaNumSerie(modelo.NumeroSerie).idArma;
                        return Redirect("~/Arma/Detalle/" + aux);
                    }
                }
                modelo.TiposArma = tablaGeneralDAL.Get("Armas", "tipoArma").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                modelo.TiposCalibre = tablaGeneralDAL.Get("Armas", "calibre").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                modelo.TiposCondicion = tablaGeneralDAL.Get("Armas", "condicion").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                modelo.TiposUbicacion = tablaGeneralDAL.Get("Armas", "ubicacion").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                modelo.TiposMarcas = tablaGeneralDAL.Get("Armas", "marca").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                return View(modelo);
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
            Session["auditoria"] = armaDAL.GetArma(id).numeroSerie;
            Session["tabla"] = "Arma";
            ArmaViewModel modelo = CargarArma(armaDAL.GetArma(id));
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
            AuditoriaViewModel auditoria_modelo = new AuditoriaViewModel
            {
                Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "2").idTablaGeneral,
                IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "5").idTablaGeneral,
                IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario
            };
            try
            {
                if (ModelState.IsValid)
                {
                    int errores = 0;
                    if (armaDAL.ArmaExiste(modelo.NumeroSerie) && modelo.NumeroSerie != armaDAL.GetArma(modelo.IdArma).numeroSerie)
                    {
                        ModelState.AddModelError(nameof(modelo.NumeroSerie), "El número de serie ingresado ya existe");
                        errores++;
                    }
                    if (errores == 0)
                    {
                        Armas arma = ConvertirArma(modelo);
                        armaDAL.Edit(arma);
                        auditoria_modelo.IdElemento = armaDAL.GetArmaNumSerie(modelo.NumeroSerie).idArma;
                        auditoriaDAL.Add(ConvertirAuditoria(auditoria_modelo));
                        return Redirect("~/Arma/Detalle/" + modelo.IdArma);
                    }
                }
                modelo.TiposArma = tablaGeneralDAL.Get("Armas", "tipoArma").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                modelo.TiposCalibre = tablaGeneralDAL.Get("Armas", "calibre").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                modelo.TiposCondicion = tablaGeneralDAL.Get("Armas", "condicion").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                modelo.TiposUbicacion = tablaGeneralDAL.Get("Armas", "ubicacion").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                modelo.TiposMarcas = tablaGeneralDAL.Get("Armas", "marca").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                return View(modelo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}