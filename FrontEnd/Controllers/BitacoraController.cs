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
    public class BitacoraController : Controller
    {
        IPoliciaDAL policiaDAL;
        IArmaDAL armaDAL;
        ITablaGeneralDAL tablaGeneralDAL;
        IBitacoraDAL bitacoraDAL;
        IRequisitoDAL requisitoDAL;
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
                if (Session["Rol"].ToString() == "2" || Session["Rol"].ToString() == "1")
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

        //Busqueda de Policias--------------------------------------------------------------------
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
            requisitoDAL = new RequisitoDAL();
            List<Policias> policias = policiaDAL.Get();
            List<Policias> policiasFiltrados = new List<Policias>();

            if (nombre == null)
            {
                policiasFiltrados = policias;
            }
            else
            {
                foreach (Policias policia in policias)
                {

                    if (policia.nombre.Contains(nombre.ToUpper()))
                    {
                        if (requisitoDAL.GetRequisitosPortacion(policia.idPolicia, "PORTACIÓN").Count > 0)
                        {
                            policiasFiltrados.Add(policia);
                        }
                    }
                }
            }
            policiasFiltrados = policiasFiltrados.OrderBy(x => x.nombre).ToList();

            return PartialView("_ListaPoliciasParcial", ConvertirListaPoliciasFiltrados(policiasFiltrados));
        }
        //--------------------------------------------------------------------------------------------------
        //Busqueda de Armas--------------------------------------------------------------------
        public List<ArmaViewModel> ConvertirListaArmasFiltradas(List<Armas> armas)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            return (from d in armas
                    select new ArmaViewModel
                    {
                        NumeroSerie = d.numeroSerie,
                        VistaTipoArma = tablaGeneralDAL.Get(d.tipoArma).descripcion,
                        VistaEstadoArma = tablaGeneralDAL.Get((int)d.condicion).descripcion,
                    }).ToList();
        }

        public PartialViewResult ListaArmasBuscar(string serie)
        {
            List<ArmaViewModel> armas = new List<ArmaViewModel>();
            return PartialView("_ListaArmasBuscar", armas);
        }

        public PartialViewResult ListaArmasParcial(string serie)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            armaDAL = new ArmaDAL();
            List<Armas> armas = armaDAL.Get();
            List<Armas> armasFiltradas = new List<Armas>();
            if (serie == null)
            {
                armasFiltradas = armas;
            }
            else
            {
                foreach (Armas arma in armas)
                {
                    if (arma.numeroSerie.Contains(serie))
                    {
                        if (arma.policiaAsignado == null)
                        {
                            armasFiltradas.Add(arma);
                        }
                    }
                }
            }
            armasFiltradas = armasFiltradas.OrderByDescending(x => x.numeroSerie).ToList();
            return PartialView("_ListaArmasParcial", ConvertirListaArmasFiltradas(armasFiltradas));
        }
        //--------------------------------------------------------------------------------------------------
        public Auditorias EditarAuditoriaArma(int idArma)
        {
            AuditoriaViewModel modelo = new AuditoriaViewModel();
            tablaGeneralDAL = new TablaGeneralDAL();
            armaDAL = new ArmaDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            return new Auditorias
            {
                idAuditoria = modelo.IdAuditoria,
                accion = modelo.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "2").idTablaGeneral,
                idCategoria = modelo.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "5").idTablaGeneral,
                idUsuario = modelo.IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario,
                fecha = DateTime.Now,
                idElemento = armaDAL.GetArma(idArma).idArma
            };
        }

        public Auditorias ConvertirAuditoria(AuditoriaViewModel modelo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            bitacoraDAL = new BitacoraDAL();
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

        public BitacoraViewModel CargarBitacora(Bitacoras bitacora)
        {
            policiaDAL = new PoliciaDAL();
            armaDAL = new ArmaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();

            BitacoraViewModel modelo = new BitacoraViewModel()
            {
                IdBitacora = bitacora.idBitacora,
                IdArma = bitacora.idArma,
                NumeroConsecutivo = bitacora.numeroConsecutivo,
                NumeroSerieArma = armaDAL.GetArma(bitacora.idArma).numeroSerie,
                ArmeroProveedor = policiaDAL.GetPolicia(bitacora.idArmeroProveedor).cedula,
                VistaArmeroProveedor = policiaDAL.GetPolicia(bitacora.idArmeroProveedor).nombre,
                PoliciaSolicitante = policiaDAL.GetPolicia(bitacora.idPoliciaSolicitante).cedula,
                VistaPoliciaSolicitante = policiaDAL.GetPolicia(bitacora.idPoliciaSolicitante).nombre,
                FechaCreacion = bitacora.fechaCreacion,
                MunicionEntregada = bitacora.municionEntregada,
                CargadoresEntregados = bitacora.cargadoresEntregados,
                ObservacionesEntrega = bitacora.observacionesEntrega,
                EstadoActual = tablaGeneralDAL.Get(bitacora.estadoActualBitacora).idTablaGeneral,
                VistaEstadoActual = tablaGeneralDAL.Get(bitacora.estadoActualBitacora).descripcion,
                CondicionInicial = tablaGeneralDAL.Get((int)bitacora.condicionInicial).codigo,
                VistaCondicionInicial = tablaGeneralDAL.Get((int)bitacora.condicionInicial).descripcion
            };
            if (modelo.VistaEstadoActual == "Completada")
            {
                modelo.ArmeroReceptor = policiaDAL.GetPolicia((int)bitacora.idArmeroReceptor).cedula;
                modelo.VistaArmeroReceptor = policiaDAL.GetPolicia((int)bitacora.idArmeroReceptor).nombre;
                modelo.CondicionFinal = int.Parse(tablaGeneralDAL.Get((int)bitacora.condicionFinal).codigo);
                modelo.VistaCondicionFinal = tablaGeneralDAL.Get((int)bitacora.condicionFinal).descripcion;
                modelo.FechaFinalizacion = (DateTime)bitacora.fechaFinalizacion;
                modelo.MunicionDevuelta = bitacora.municionDevuelta;
                modelo.CargadoresDevueltos = bitacora.cargadoresDevueltos;
                modelo.ObservacionesDevuelta = bitacora.observacionesDevuelta;
            }
            return modelo;
        }

        public Bitacoras ConvertirBitacora(BitacoraViewModel modelo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            armaDAL = new ArmaDAL();
            Bitacoras bitacora = new Bitacoras()
            {
                idBitacora = modelo.IdBitacora,
                numeroConsecutivo = modelo.NumeroConsecutivo,
                idArmeroProveedor = policiaDAL.GetPoliciaCedula(modelo.ArmeroProveedor).idPolicia,
                idPoliciaSolicitante = policiaDAL.GetPoliciaCedula(modelo.PoliciaSolicitante).idPolicia,
                idArma = armaDAL.GetArmaNumSerie(modelo.NumeroSerieArma).idArma,
                fechaCreacion = modelo.FechaCreacion,
                municionEntregada = modelo.MunicionEntregada,
                cargadoresEntregados = modelo.CargadoresEntregados,
                //observacionesEntrega = modelo.ObservacionesEntrega.ToUpper(),
                condicionInicial = tablaGeneralDAL.Get("Armas", "condicion", modelo.VistaCondicionInicial).idTablaGeneral
            };
            if (modelo.ObservacionesEntrega != null)
            {
                bitacora.observacionesEntrega = modelo.ObservacionesEntrega.ToUpper();
            }
            if (modelo.VistaEstadoActual == "Pendiente")
            {
                bitacora.estadoActualBitacora = tablaGeneralDAL.GetCodigo("Bitacoras", "estadoActualBitacora", "1").idTablaGeneral;
            }
            else
            {
                bitacora.idArmeroReceptor = policiaDAL.GetPoliciaCedula(modelo.ArmeroReceptor).idPolicia;
                bitacora.condicionFinal = tablaGeneralDAL.GetCodigo("Armas", "condicion", modelo.CondicionFinal.ToString()).idTablaGeneral;
                bitacora.fechaFinalizacion = modelo.FechaFinalizacion;
                bitacora.municionDevuelta = modelo.MunicionDevuelta;
                bitacora.cargadoresDevueltos = modelo.CargadoresDevueltos;
                if(modelo.ObservacionesDevuelta != null) { 
                bitacora.observacionesDevuelta = modelo.ObservacionesDevuelta.ToUpper();}
                bitacora.estadoActualBitacora = tablaGeneralDAL.GetCodigo("Bitacoras", "estadoActualBitacora", "2").idTablaGeneral;
            }

            return bitacora;
        }

        //Metodos de las Vistas
        public ActionResult Index(string filtrosSeleccionado, string busqueda, string estadosBitacora, string busquedaFechaInicioB, string busquedaFechaFinalB)
        {
            Autorizar();
            bitacoraDAL = new BitacoraDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();

            //Carga combobox busqueda
            List<TablaGeneral> comboindex1 = tablaGeneralDAL.Get("Bitacoras", "index");
            List<TablaGeneral> comboindex2 = tablaGeneralDAL.Get("Bitacoras", "estadoActualBitacora");
            List<SelectListItem> items1 = comboindex1.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.descripcion
                };
            });
            ViewBag.items1 = items1;
            List<SelectListItem> items2 = comboindex2.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.descripcion
                };
            });
            ViewBag.items2 = items2;

            //Carga lista de policias
            List<BitacoraViewModel> bitacoras = new List<BitacoraViewModel>();
            List<BitacoraViewModel> bitacorasFiltradas = new List<BitacoraViewModel>();
            foreach (Bitacoras bitacora in bitacoraDAL.Get())
            {
                bitacoras.Add(CargarBitacora(bitacora));
            }
            if (busqueda != null)
            {
                foreach (BitacoraViewModel bitacora in bitacoras)
                {
                    if (filtrosSeleccionado == "Nombre Policía")
                    {
                        if (policiaDAL.GetPoliciaCedula(bitacora.PoliciaSolicitante).nombre.Contains(busqueda))
                        {
                            bitacorasFiltradas.Add(bitacora);
                        }
                    }
                    if (filtrosSeleccionado == "Estado de Bitácora")
                    {
                        if (tablaGeneralDAL.Get(bitacora.EstadoActual).descripcion.Contains(estadosBitacora))
                        {
                            bitacorasFiltradas.Add(bitacora);
                        }
                    }
                }
                if (filtrosSeleccionado == "Fecha de Creación")
                {
                    DateTime fechaInicio = DateTime.Parse(busquedaFechaInicioB);
                    DateTime fechaFinal = DateTime.Parse(busquedaFechaFinalB);
                    if (bitacoraDAL.GetBitacorasRango(fechaInicio, fechaFinal) != null)
                    {
                        foreach (Bitacoras bitacoraFechas in bitacoraDAL.GetBitacorasRango(fechaInicio, fechaFinal))
                        {
                            bitacorasFiltradas.Add(CargarBitacora(bitacoraFechas));
                        }
                    }

                }
                bitacoras = bitacorasFiltradas;
            }
            return View(bitacoras.OrderBy(x => x.EstadoActual).ToList());

        }

        public ActionResult Nuevo()
        {
            Autorizar();
            bitacoraDAL = new BitacoraDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            BitacoraViewModel modelo = new BitacoraViewModel();
            return View(modelo);
        }

        [HttpPost]
        public ActionResult Nuevo(BitacoraViewModel modelo)
        {
            Autorizar();
            bitacoraDAL = new BitacoraDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            armaDAL = new ArmaDAL();
            auditoriaDAL = new AuditoriaDAL();
            usuarioDAL = new UsuarioDAL();

            modelo.FechaCreacion = DateTime.Now;
            modelo.VistaEstadoActual = tablaGeneralDAL.GetCodigo("Bitacoras", "estadoActualBitacora", "1").descripcion;
            modelo.NumeroConsecutivo = (bitacoraDAL.GetCount() + 1).ToString() + "-" + DateTime.Now.Year;

            AuditoriaViewModel auditoria_model = new AuditoriaViewModel
            {
                Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "1").idTablaGeneral,
                IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "6").idTablaGeneral,
                IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario
            };

            try
            {
                if (ModelState.IsValid)
                {
                    int errores = 0;
                    if (armaDAL.GetArmaNumSerie(modelo.NumeroSerieArma).policiaAsignado != null)
                    {
                        ModelState.AddModelError(nameof(modelo.NumeroSerieArma), "El arma ya tiene policía asignado");
                        errores++;
                    }
                    if(errores == 0) 
                    { 
                        Armas arma = armaDAL.GetArmaNumSerie(modelo.NumeroSerieArma);
                        Bitacoras bitacora = ConvertirBitacora(modelo);
                        arma.policiaAsignado = policiaDAL.GetPoliciaCedula(modelo.PoliciaSolicitante).idPolicia;
                        bitacoraDAL.Add(bitacora);
                        auditoria_model.IdElemento = bitacoraDAL.GetBitacoraConsecutivo(modelo.NumeroConsecutivo).idBitacora;
                        auditoriaDAL.Add(ConvertirAuditoria(auditoria_model));
                        armaDAL.Edit(arma);
                        auditoriaDAL.Add(EditarAuditoriaArma(arma.idArma));
                        int aux = bitacoraDAL.GetBitacoraConsecutivo(modelo.NumeroConsecutivo).idBitacora;

                        return Redirect("~/Bitacora/Detalle/" + aux);
                    }
                }
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
            bitacoraDAL = new BitacoraDAL();
            Session["idBitacora"] = id;
            Session["auditoria"] = bitacoraDAL.GetBitacora(id).numeroConsecutivo;
            Session["tabla"] = "Bitácora";
            Session["consecutivo"] = bitacoraDAL.GetBitacora(id).numeroConsecutivo;
            BitacoraViewModel modelo = CargarBitacora(bitacoraDAL.GetBitacora(id));
            return View(modelo);
        }

        public ActionResult Completar(int id)
        {
            Autorizar();
            bitacoraDAL = new BitacoraDAL();
            BitacoraViewModel modelo = CargarBitacora(bitacoraDAL.GetBitacora(id));
            modelo.TiposCondicion = tablaGeneralDAL.Get("Armas", "condicion").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            return View(modelo);
        }

        [HttpPost]
        public ActionResult Completar(BitacoraViewModel model)
        {
            Autorizar();
            bitacoraDAL = new BitacoraDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            armaDAL = new ArmaDAL();
            auditoriaDAL = new AuditoriaDAL();
            usuarioDAL = new UsuarioDAL();

            model.FechaFinalizacion = DateTime.Now;
            model.VistaEstadoActual = tablaGeneralDAL.GetCodigo("Bitacoras", "estadoActualBitacora", "2").descripcion;

            AuditoriaViewModel auditoria_model = new AuditoriaViewModel
            {
                Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "5").idTablaGeneral,
                IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "6").idTablaGeneral,
                IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario
            };

            try
            {
                if (ModelState.IsValid)
                {
                    Armas arma = armaDAL.GetArmaNumSerie(model.NumeroSerieArma);
                    arma.policiaAsignado = null;
                    arma.condicion = tablaGeneralDAL.GetCodigo("Armas", "condicion", model.CondicionFinal.ToString()).idTablaGeneral;
                    armaDAL.Edit(arma);
                    auditoriaDAL.Add(EditarAuditoriaArma(arma.idArma));
                    bitacoraDAL.Edit(ConvertirBitacora(model));
                    auditoria_model.IdElemento = bitacoraDAL.GetBitacoraConsecutivo(model.NumeroConsecutivo).idBitacora;
                    auditoriaDAL.Add(ConvertirAuditoria(auditoria_model));

                    return Redirect("~/Bitacora/Detalle/" + model.IdBitacora);
                }
                model.TiposCondicion = tablaGeneralDAL.Get("Armas", "condicion").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult Editar(int id)
        {
            AutorizarEditar();
            bitacoraDAL = new BitacoraDAL();
            BitacoraViewModel modelo = CargarBitacora(bitacoraDAL.GetBitacora(id));
            modelo.TiposCondicion = tablaGeneralDAL.Get("Armas", "condicion").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            return View(modelo);
        }


        [HttpPost]
        public ActionResult Editar(BitacoraViewModel modelo)
        {
            AutorizarEditar();
            bitacoraDAL = new BitacoraDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            armaDAL = new ArmaDAL();
            policiaDAL = new PoliciaDAL();
            auditoriaDAL = new AuditoriaDAL();
            usuarioDAL = new UsuarioDAL();

            AuditoriaViewModel auditoria_modelo = new AuditoriaViewModel
            {
                Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "2").idTablaGeneral,
                IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "6").idTablaGeneral,
                IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario
            };

            try
            {
                if (ModelState.IsValid)
                {
                    if (modelo.NumeroSerieArma != armaDAL.GetArma(modelo.IdArma).numeroSerie)
                    {
                        Armas arma = armaDAL.GetArmaNumSerie(modelo.NumeroSerieArma);
                        Armas arma2 = armaDAL.GetArma(modelo.IdArma);
                        arma.policiaAsignado = policiaDAL.GetPoliciaCedula(modelo.PoliciaSolicitante).idPolicia;
                        arma2.policiaAsignado = null;
                        armaDAL.Edit(arma);
                        auditoriaDAL.Add(EditarAuditoriaArma(arma.idArma));
                        armaDAL.Edit(arma2);
                        auditoriaDAL.Add(EditarAuditoriaArma(arma2.idArma));
                    }
                    bitacoraDAL.Edit(ConvertirBitacora(modelo));
                    auditoria_modelo.IdElemento = bitacoraDAL.GetBitacoraConsecutivo(modelo.NumeroConsecutivo).idBitacora;
                    auditoriaDAL.Add(ConvertirAuditoria(auditoria_modelo));
                    return Redirect("~/Bitacora/Detalle/" + modelo.IdBitacora);
                }
                modelo.TiposCondicion = tablaGeneralDAL.Get("Armas", "condicion").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                return View(modelo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

       
    }
}
