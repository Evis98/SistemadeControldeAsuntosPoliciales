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

        public BitacoraViewModel CargarBitacora(Bitacoras bitacora)
        {
            policiaDAL = new PoliciaDAL();
            armaDAL = new ArmaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();

            BitacoraViewModel modelo = new BitacoraViewModel();
            {
                modelo.IdBitacora = bitacora.idBitacora;
                modelo.NumeroConsecutivo = bitacora.numeroConsecutivo;
                modelo.NumeroSerieArma = armaDAL.GetArma(bitacora.idArma).numeroSerie;
                modelo.ArmeroProveedor = policiaDAL.GetPolicia(bitacora.idArmeroProveedor).cedula;
                modelo.VistaArmeroProveedor = policiaDAL.GetPolicia(bitacora.idArmeroProveedor).nombre;
                modelo.PoliciaSolicitante = policiaDAL.GetPolicia(bitacora.idPoliciaSolicitante).cedula;
                modelo.VistaPoliciaSolicitante = policiaDAL.GetPolicia(bitacora.idPoliciaSolicitante).nombre;
                modelo.FechaCreacion = bitacora.fechaCreacion;
                modelo.MunicionEntregada = bitacora.municionEntregada;
                modelo.CargadoresEntregados = bitacora.cargadoresEntregados;
                modelo.ObservacionesEntrega = bitacora.observacionesEntrega;
                modelo.EstadoActual = tablaGeneralDAL.Get(bitacora.estadoActualBitacora).idTablaGeneral;
                modelo.VistaEstadoActual = tablaGeneralDAL.Get(bitacora.estadoActualBitacora).descripcion;
                if (modelo.VistaEstadoActual == "Completada")
                {
                    modelo.ArmeroReceptor = policiaDAL.GetPolicia((int)bitacora.idArmeroReceptor).cedula;
                    modelo.VistaArmeroReceptor = policiaDAL.GetPolicia((int)bitacora.idArmeroReceptor).nombre;
                    modelo.CondicionFinal = int.Parse(tablaGeneralDAL.Get((int)bitacora.condicionFinal).codigo);
                    modelo.VistaCondicionFinal = tablaGeneralDAL.Get((int)bitacora.condicionFinal).descripcion;
                    modelo.FechaFinalizacion = DateTime.Now;
                    modelo.MunicionDevuelta = bitacora.municionDevuelta;
                    modelo.CargadoresDevueltos = bitacora.cargadoresDevueltos;
                    modelo.ObservacionesDevuelta = bitacora.observacionesDevuelta;
                }
            }
            return modelo;
        }

        public Bitacoras ConvertirBitacora(BitacoraViewModel modelo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            armaDAL = new ArmaDAL();
            Bitacoras bitacora = new Bitacoras();
            {
                bitacora.idBitacora = modelo.IdBitacora;
                bitacora.numeroConsecutivo = modelo.NumeroConsecutivo;
                bitacora.idArmeroProveedor = policiaDAL.GetPoliciaCedula(modelo.ArmeroProveedor).idPolicia;
                bitacora.idPoliciaSolicitante = policiaDAL.GetPoliciaCedula(modelo.PoliciaSolicitante).idPolicia;
                bitacora.idArma = armaDAL.GetArmaNumSerie(modelo.NumeroSerieArma).idArma;
                bitacora.fechaCreacion = modelo.FechaCreacion;
                bitacora.municionEntregada = modelo.MunicionEntregada;
                bitacora.cargadoresEntregados = modelo.CargadoresEntregados;
                bitacora.observacionesEntrega = modelo.ObservacionesEntrega;
                if (modelo.VistaEstadoActual == null)
                {
                    bitacora.estadoActualBitacora = tablaGeneralDAL.GetCodigo("Bitacoras", "estadoActualBitacora", "1").idTablaGeneral;
                }
                else
                {
                    bitacora.idArmeroReceptor = policiaDAL.GetPoliciaCedula(modelo.ArmeroReceptor).idPolicia;
                    bitacora.condicionFinal = tablaGeneralDAL.GetCodigo("Armas", "condicion", modelo.CondicionFinal.ToString()).idTablaGeneral;
                    bitacora.fechaFinalizacion = DateTime.Now;
                    bitacora.municionDevuelta = modelo.MunicionDevuelta;
                    bitacora.cargadoresDevueltos = modelo.CargadoresDevueltos;
                    bitacora.observacionesDevuelta = modelo.ObservacionesDevuelta;
                    bitacora.estadoActualBitacora = tablaGeneralDAL.GetCodigo("Bitacoras", "estadoActualBitacora", "2").idTablaGeneral;
                }

            };

            return bitacora;
        }


        public ActionResult Index(string filtroSeleccionado, string busqueda, string estadoBitacora, string busquedaFechaInicioB, string busquedaFechaFinalB)
        {
            bitacoraDAL = new BitacoraDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
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
                    if (filtroSeleccionado == "Nombre Policía")
                    {
                        if (policiaDAL.GetPoliciaCedula(bitacora.PoliciaSolicitante).nombre.Contains(busqueda))
                        {
                            bitacorasFiltradas.Add(bitacora);
                        }
                    }
                    if (filtroSeleccionado == "Estado de bitácora")
                    {
                        if (tablaGeneralDAL.Get(bitacora.EstadoActual).descripcion.Contains(estadoBitacora))
                        {
                            bitacorasFiltradas.Add(bitacora);
                        }
                    }
                }
                if (filtroSeleccionado == "Fecha de creación")
                {
                    DateTime fechaInicio = DateTime.Parse(busquedaFechaInicioB);
                    DateTime fechaFinal = DateTime.Parse(busquedaFechaFinalB);
                    //if (fechaInicio < fechaFinal)
                    //{
                        if (bitacoraDAL.GetBitacorasRango(fechaInicio, fechaFinal) != null)
                        {
                            foreach (Bitacoras bitacoraFechas in bitacoraDAL.GetBitacorasRango(fechaInicio, fechaFinal))
                            {
                                bitacorasFiltradas.Add(CargarBitacora(bitacoraFechas));
                            }
                        }
                    //}
                }
                bitacoras = bitacorasFiltradas;
            }
            return View(bitacoras.OrderBy(x => x.EstadoActual).ToList());
        }

        // Este Nuevo funciona para cargar la información para el View Nuevo
        public ActionResult Nuevo()
        {
            bitacoraDAL = new BitacoraDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            BitacoraViewModel modelo = new BitacoraViewModel();
            return View(modelo);
        }

        //Guarda la información ingresada en la página para crear bitacoras
        [HttpPost]
        public ActionResult Nuevo(BitacoraViewModel model)
        {
            bitacoraDAL = new BitacoraDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            armaDAL = new ArmaDAL();
            model.FechaCreacion = DateTime.Now;
            model.NumeroConsecutivo = (bitacoraDAL.GetCount() + 1).ToString() + "-" + DateTime.Now.Year;
            if (armaDAL.GetArmaNumSerie(model.NumeroSerieArma).policiaAsignado == null) 
            {
                model.ArmaPoliciaAsignado = true;
            }
            else
            {
                model.ArmaPoliciaAsignado = false; 
            }
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.ArmaPoliciaAsignado)
                    {
                        Armas arma = armaDAL.GetArmaNumSerie(model.NumeroSerieArma);
                        Bitacoras bitacora = ConvertirBitacora(model);
                        arma.policiaAsignado = policiaDAL.GetPoliciaCedula(model.PoliciaSolicitante).idPolicia;
                        bitacoraDAL.Add(bitacora);
                        armaDAL.Edit(arma);
                        int aux = bitacoraDAL.GetBitacoraConsecutivo(model.NumeroConsecutivo).idBitacora;
                        TempData["smsnuevabitacora"] = "Bitacora creada con éxito";
                        ViewBag.smsnuevaarma = TempData["smsnuevaarma"];
                        return Redirect("~/Bitacora/Detalle/" + aux);
                    }
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
            armaDAL = new ArmaDAL();
            bitacoraDAL = new BitacoraDAL();
            BitacoraViewModel modelo = CargarBitacora(bitacoraDAL.GetBitacora(id));
            try
            {
                ViewBag.smseditararma = TempData["smseditararma"];
                ViewBag.smscambioestadoarma = TempData["smscambioestadoarma"];
                ViewBag.smsnuevaarma = TempData["smsnuevaarma"];
            }
            catch { }
            return View(modelo);
        }

        public ActionResult Completar(int id)
        {
            bitacoraDAL = new BitacoraDAL();
            BitacoraViewModel model = CargarBitacora(bitacoraDAL.GetBitacora(id));
            model.TiposCondicion = tablaGeneralDAL.Get("Armas", "condicion").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            return View(model);
        }

        [HttpPost]
        public ActionResult Completar(BitacoraViewModel model)
        {
            bitacoraDAL = new BitacoraDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            armaDAL = new ArmaDAL();
            try
            {
                if (ModelState.IsValid)
                {
                    Armas arma = armaDAL.GetArmaNumSerie(model.NumeroSerieArma);
                    arma.policiaAsignado = null;
                    armaDAL.Edit(arma);
                    bitacoraDAL.Edit(ConvertirBitacora(model));
                    TempData["sms"] = "Policía editado con éxito";
                    ViewBag.sms = TempData["sms"];
                    return Redirect("~/Bitacora/Detalle/" + model.IdBitacora);
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult Editar(int id)
        {
            bitacoraDAL = new BitacoraDAL();
            BitacoraViewModel modelo = CargarBitacora(bitacoraDAL.GetBitacora(id));
            return View(modelo);
        }


        //Guarda la información modificada de los policías
        [HttpPost]
        public ActionResult Editar(BitacoraViewModel modelo)
        {
            bitacoraDAL = new BitacoraDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            
            try
            {
                if (ModelState.IsValid)
                {
                    bitacoraDAL.Edit(ConvertirBitacora(modelo));
                    TempData["sms"] = "Policía editado con éxito";
                    ViewBag.sms = TempData["sms"];
                    return Redirect("~/Bitacora/Detalle/" + modelo.IdBitacora);
                }
                return View(modelo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
        //--------------------------------------------------------------------------------------------------
        //Busqueda de Armas--------------------------------------------------------------------
        public List<ArmaViewModel> ConvertirListaArmasFiltradas(List<Armas> armas)
        {
            return (from d in armas
                    select new ArmaViewModel
                    {
                        NumeroSerie = d.numeroSerie,
                        VistaTipoArma = tablaGeneralDAL.Get(d.tipoArma).descripcion,
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
    }
}