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
        IUsuarioDAL usuarioDAL;

        // Este ConvertirListaBitacoras funciona para mostrar los datos de la bitacora en el index
        public List<ListBitacoraViewModel> ConvertirListaBitacoras(List<Bitacoras> bitacoras)
        {
            policiaDAL = new PoliciaDAL();
            usuarioDAL = new UsuarioDAL();
            armaDAL = new ArmaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            return (from d in bitacoras
                    select new ListBitacoraViewModel
                    {
                        /* IdBitacora = d.idBitacora,
                        IdArma = d.idArma,
                        ArmeroProveedor = usuarioDAL.GetUsuario(d.idArmeroProveedor).nombre,
                        ArmeroReceptor = usuarioDAL.GetUsuario(d.idArmeroReceptor).nombre,*/
                        NumeroSerieArmaAsignada = armaDAL.GetArma(d.idArma).numeroSerie,
                        PoliciaSolicitante = policiaDAL.GetPolicia(d.idPoliciaSolicitante).nombre,
                        /* CondicionFinal = d.condicionFinal,*/
                        FechaCreacion = d.fechaCreacion,
                        /* FechaFinalizacion = d.fechaFinalizacion,
                         MunicionEntregada = d.municionEntregada,
                         MunicionDevuelta = d.municionDevuelta,
                         CargadoresEntregados = d.cargadoresEntregados,
                         CargadoresDevueltos = d.cargadoresDevueltos,
                         Observaciones = d.observaciones,*/
                        EstadoActualBitacora = tablaGeneralDAL.Get(d.estadoActualBitacora).descripcion,
                    }).ToList();
        }

        public Bitacoras ConvertirBitacora(BitacoraViewModel modelo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            return new Bitacoras
            {
                idBitacora = modelo.IdBitacora,
                idArmeroProveedor = policiaDAL.GetPoliciaCedula(modelo.ArmeroProveedor).idPolicia,
                idArmeroReceptor = policiaDAL.GetPoliciaCedula(modelo.ArmeroReceptor).idPolicia,
                idPoliciaSolicitante = policiaDAL.GetPoliciaCedula(modelo.PoliciaSolicitante).idPolicia,
                condicionFinal = tablaGeneralDAL.GetCodigo("Bitacora", "condicionFinal", modelo.CondicionFinal.ToString()).idTablaGeneral,
                fechaCreacion = modelo.FechaCreacion,
                fechaFinalizacion = modelo.FechaFinalizacion,
                municionEntregada = modelo.MunicionEntregada,
                municionDevuelta = modelo.MunicionDevuelta,
                cargadoresEntregados = modelo.CargadoresEntregados,
                cargadoresDevueltos = modelo.CargadoresDevueltos,
                observaciones = modelo.Observaciones,
                estadoActualBitacora = tablaGeneralDAL.GetCodigo("Bitacora", "estadoActualBitacora", modelo.EstadoActualBitacora.ToString()).idTablaGeneral,
            };
        }

        public BitacoraViewModel CargarBitacora(Bitacoras bitacora)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            policiaDAL = new PoliciaDAL();
            bitacoraDAL = new BitacoraDAL();
            armaDAL = new ArmaDAL();
            BitacoraViewModel bitacoraEditar = new BitacoraViewModel();
            {
                bitacoraEditar.IdBitacora = bitacora.idBitacora;
                bitacoraEditar.NumeroSerieArmaAsignada = armaDAL.GetArma(bitacora.idArma).numeroSerie;
                bitacoraEditar.TipoArmaAsignada = armaDAL.GetArma(bitacora.idArma).tipoArma;
                bitacoraEditar.ArmeroProveedor = policiaDAL.GetPolicia(bitacora.idArmeroProveedor).nombre;
                bitacoraEditar.PoliciaSolicitante = policiaDAL.GetPolicia(bitacora.idPoliciaSolicitante).nombre;
                bitacoraEditar.CondicionInicial = armaDAL.GetArma(bitacora.idArma).condicion;
                bitacoraEditar.FechaCreacion = bitacora.fechaCreacion;
                bitacoraEditar.MunicionEntregada = bitacora.municionEntregada;
                bitacoraEditar.CargadoresEntregados = bitacora.cargadoresEntregados;
                bitacoraEditar.Observaciones = bitacora.observaciones;
                bitacoraEditar.EstadoActualBitacora = tablaGeneralDAL.Get(bitacora.estadoActualBitacora).codigo;
                 
            };
            if (bitacora.idArmeroReceptor != null)
            {
                bitacoraEditar.ArmeroReceptor = policiaDAL.GetPolicia((int)bitacora.idArmeroReceptor).nombre;
            }
            if (bitacora.condicionFinal != null)
            {
                bitacoraEditar.CondicionFinal = (int)bitacora.condicionFinal;
            }
            if (bitacora.fechaFinalizacion != null)
            {
                bitacoraEditar.FechaFinalizacion = (DateTime)bitacora.fechaFinalizacion;
            }
            if (bitacora.municionDevuelta != null)
            {
                bitacoraEditar.MunicionDevuelta = (int)bitacora.municionDevuelta;
            }
            if (bitacora.cargadoresDevueltos != null)
            {
                bitacoraEditar.CargadoresDevueltos = (int)bitacora.cargadoresDevueltos;
            }
            return bitacoraEditar;
        }

        public ListBitacoraViewModel ConvertirBitacoraInverso(Bitacoras bitacora)
        {
            policiaDAL = new PoliciaDAL();
            armaDAL = new ArmaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            return new ListBitacoraViewModel
            {
                IdBitacora = bitacora.idBitacora,
                IdArma = bitacora.idArma,
                ArmeroProveedor = policiaDAL.GetPolicia(bitacora.idArmeroProveedor).nombre,
                ArmeroReceptor = policiaDAL.GetPolicia((int)bitacora.idArmeroReceptor).nombre,
                PoliciaSolicitante = policiaDAL.GetPolicia(bitacora.idPoliciaSolicitante).nombre,
                //CondicionInicial = armaDAL.GetArma(bitacora.idArma).condicion.ToString(),
                //CondicionFinal = bitacora.condicionFinal.ToString(),
                FechaCreacion = bitacora.fechaCreacion,
                FechaFinalizacion = (DateTime)bitacora.fechaFinalizacion,
                MunicionEntregada = bitacora.municionEntregada,
                MunicionDevuelta = (int)bitacora.municionDevuelta,
                CargadoresEntregados = bitacora.cargadoresEntregados,
                CargadoresDevueltos = (int)bitacora.cargadoresDevueltos,
                Observaciones = bitacora.observaciones,
                EstadoActualBitacora = tablaGeneralDAL.Get(bitacora.estadoActualBitacora).descripcion,
            };
        }

        public ActionResult Index(string filtroSeleccionado, string busqueda, string EstadoBitacora)
        {
            bitacoraDAL = new BitacoraDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            List<ListBitacoraViewModel> bitacoras = ConvertirListaBitacoras(bitacoraDAL.Get());
            List<ListBitacoraViewModel> bitacorasFiltradas = new List<ListBitacoraViewModel>();
            if (busqueda != null)
            {
                foreach (ListBitacoraViewModel bitacora in bitacoras)
                {
                    if (filtroSeleccionado == "Policía Solicitante")
                    {
                        if (bitacora.PoliciaSolicitante.Contains(busqueda))
                        {
                            bitacorasFiltradas.Add(bitacora);
                        }
                    }
                    if (filtroSeleccionado == "Estado de bitácora")
                    {
                        
                            //if (tablaGeneralDAL.Get(bitacora.EstadoActualBitacora).descripcion.Contains(EstadoBitacora))
                        //{
                        //    bitacorasFiltradas.Add(bitacora);
                        //}
                    }
                    /*Meter un buscar por fecha (Eva lo va a pasar)*/
                }
                bitacoras = bitacorasFiltradas;
            }
            return View(bitacoras);
        }

        // Este Nuevo funciona para cargar la información para el View Nuevo
        public ActionResult Nuevo()
        {
            bitacoraDAL = new BitacoraDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            BitacoraViewModel modelo = new BitacoraViewModel();
            {
                //modelo.TiposDeCondicion = tablaGeneralDAL.Get("Bitacoras", "condicionFinal").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            }
            return View(modelo);
        }

        //Guarda la información ingresada en la página para crear bitacoras
        [HttpPost]
        public ActionResult Nuevo(BitacoraViewModel modelo)
        {
            bitacoraDAL = new BitacoraDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            try
            {
                if (ModelState.IsValid)
                {
                    Bitacoras bitacora = ConvertirBitacora(modelo);
                    bitacoraDAL.Add(bitacora);
                    int aux = bitacoraDAL.GetBitacora(modelo.IdBitacora).idBitacora;
                    //TempData["smsnuevabitacora"] = "Bitacora creada con éxito";
                    //ViewBag.smsnuevaarma = TempData["smsnuevaarma"];
                    return Redirect("~/Bitacora/Detalle/" + aux);
                }
                return View(modelo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Busqueda de Policias--------------------------------------------------------------------
        public List<ListPoliciaViewModel> ConvertirListaPoliciasFiltrados(List<Policias> policias)
        {
            return (from d in policias
                    select new ListPoliciaViewModel
                    {
                        Cedula = d.cedula,
                        Nombre = d.nombre,
                    }).ToList();
        }

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
        //--------------------------------------------------------------------------------------------------
        //Busqueda de Armas--------------------------------------------------------------------
        public List<ListArmaViewModel> ConvertirListaArmasFiltradas(List<Armas> armas)
        {
            return (from d in armas
                    select new ListArmaViewModel
                    {
                        NumeroSerie = d.numeroSerie,
                        TipoArma = tablaGeneralDAL.Get(d.tipoArma).descripcion,
                    }).ToList();
        }

        public PartialViewResult ListaArmasBuscar(string serie)
        {
            List<ListArmaViewModel> armas = new List<ListArmaViewModel>();
            return PartialView("_ListaArmasBuscar", armas);
        }

        public PartialViewResult ListaArmasParcial(string serie)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            armaDAL = new ArmaDAL();
            List<Armas> armas = armaDAL.Get();
            List<Armas> armasFiltradas = new List<Armas>();
            if (serie == "")
            {
                armasFiltradas = armas;
            }
            else
            {
                foreach (Armas arma in armas)
                {
                    if (arma.numeroSerie.Contains(serie))
                    {
                        armasFiltradas.Add(arma);
                    }
                }
            }
            armasFiltradas = armasFiltradas.OrderBy(x => x.numeroSerie).ToList();
            return PartialView("_ListaArmasParcial", ConvertirListaArmasFiltradas(armasFiltradas));
        }
        //--------------------------------------------------------------------------------------------------
    }
}