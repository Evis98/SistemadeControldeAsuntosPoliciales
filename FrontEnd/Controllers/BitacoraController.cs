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
            return new BitacoraViewModel
            {
                IdBitacora = bitacora.idBitacora,            
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
                VistaEstadoActual = tablaGeneralDAL.Get(bitacora.estadoActualBitacora).descripcion
            };
        }

        public Bitacoras ConvertirBitacora(BitacoraViewModel modelo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            armaDAL = new ArmaDAL();
            return new Bitacoras
            {
                idBitacora = modelo.IdBitacora,
                numeroConsecutivo = modelo.NumeroConsecutivo,
                idArmeroProveedor = policiaDAL.GetPoliciaCedula(modelo.ArmeroProveedor).idPolicia,
                idPoliciaSolicitante = policiaDAL.GetPoliciaCedula(modelo.PoliciaSolicitante).idPolicia,
                idArma = armaDAL.GetArmaNumSerie(modelo.NumeroSerieArma).idArma,
                fechaCreacion = modelo.FechaCreacion,
                municionEntregada = modelo.MunicionEntregada,
                cargadoresEntregados = modelo.CargadoresEntregados,
                observacionesEntrega = modelo.ObservacionesEntrega,
                estadoActualBitacora = tablaGeneralDAL.GetCodigo("Bitacoras", "estadoActualBitacora", "1").idTablaGeneral,
            };
        }

     
        public ActionResult Index(string filtroSeleccionado, string busqueda, string estadoBitacora)
        {
            bitacoraDAL = new BitacoraDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            List<Bitacoras> bitacoras = bitacoraDAL.Get();
            List<Bitacoras> bitacorasFiltradas = new List<Bitacoras>();
            List<BitacoraViewModel> bitacorasOrdenados = new List<BitacoraViewModel>();
            if (busqueda != null)
            {
                foreach (Bitacoras bitacora in bitacoras)
                {
                    if (filtroSeleccionado == "Nombre Policía")
                    {
                        if (policiaDAL.GetPolicia(bitacora.idPoliciaSolicitante).nombre.Contains(busqueda))
                        {
                            bitacorasFiltradas.Add(bitacora);
                        }
                    }
                    if (filtroSeleccionado == "Estado de bitácora")
                    {
                        if (tablaGeneralDAL.Get(bitacora.estadoActualBitacora).descripcion.Contains(estadoBitacora))
                        {
                            bitacorasFiltradas.Add(bitacora);
                        }
                    }
                    /*Meter un buscar por fecha (Eva lo va a pasar)*/
                }
                bitacoras = bitacorasFiltradas;

            }
            foreach (Bitacoras bitacora in bitacoras)
            {
                bitacorasOrdenados.Add(CargarBitacora(bitacora));
            }
            bitacorasOrdenados = bitacorasOrdenados.OrderBy(x => x.EstadoActual).ToList();
            return View(bitacorasOrdenados);
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