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
    public class PoliciaController : Controller
    {
        IPoliciaDAL policiaDAL;
        ITablaGeneralDAL tablaGeneralDAL;

        public Policias ConvertirPolicia(PoliciaViewModel modelo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            return new Policias
            {
                idPolicia = modelo.IdPolicia,
                cedula = modelo.Cedula,
                tipoCedula = tablaGeneralDAL.GetCodigo("Policias", "tipoCedula", modelo.TipoCedula.ToString()).idTablaGeneral,
                nombre = modelo.Nombre,
                fechaNacimiento = modelo.FechaNacimiento,
                correoElectronico = modelo.CorreoElectronico,
                direccion = modelo.Direccion,
                telefonoCelular = modelo.TelefonoCelular,
                telefonoCasa = modelo.TelefonoCasa,
                contactoEmergencia = modelo.ContactoEmergencia,
                telefonoEmergencia = modelo.TelefonoEmergencia,
                estado = (int)EstadoDefault(modelo.Estado),
            };
        }
        public PoliciaViewModel CargarPolicia(Policias policia)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            return new PoliciaViewModel
            {
                IdPolicia = policia.idPolicia,
                Cedula = policia.cedula,
                TipoCedula = int.Parse(tablaGeneralDAL.Get(policia.tipoCedula).codigo),
                VistaTipoCedula = tablaGeneralDAL.Get(policia.tipoCedula).descripcion,
                Edad = ObtenerEdad(policia.fechaNacimiento),
                Nombre = policia.nombre,
                FechaNacimiento = policia.fechaNacimiento,
                CorreoElectronico = policia.correoElectronico,
                Direccion = policia.direccion,
                TelefonoCelular = policia.telefonoCelular,
                TelefonoCasa = policia.telefonoCasa,
                ContactoEmergencia = policia.contactoEmergencia,
                TelefonoEmergencia = policia.telefonoEmergencia,
                Estado = policia.estado,
                VistaEstado = tablaGeneralDAL.Get(policia.estado).descripcion,
            };
        }
        public ActionResult Index(string filtrosSeleccionado, string busqueda )
        {
            policiaDAL = new PoliciaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            List<PoliciaViewModel> policias = new List<PoliciaViewModel>();
            List<PoliciaViewModel> policiasFiltrados = new List<PoliciaViewModel>();
            List<TablaGeneral> comboindex = tablaGeneralDAL.Get("Policias", "index");
            List<SelectListItem> items =comboindex.ConvertAll(d =>
            {
                return new SelectListItem()
                {                    
                   Text = d.descripcion                    
                };
            });
            ViewBag.items = items;            
            foreach (Policias policia in policiaDAL.Get())
            {
                policias.Add(CargarPolicia(policia));
            }
            
            if (busqueda != null)
            {
                
                foreach (PoliciaViewModel policia in policias)
                {
                    if (filtrosSeleccionado == "Cédula")
                    {
                        if (policia.Cedula.Contains(busqueda))
                        {
                            policiasFiltrados.Add(policia);
                        }
                    }
                    if (filtrosSeleccionado == "Nombre")
                    {
                        if (policia.Nombre.Contains(busqueda))
                        {
                            policiasFiltrados.Add(policia);
                        }
                    }

                }
                
                policias = policiasFiltrados;
            }
            return View(policias.OrderBy(x => x.Nombre).ToList());
        }

        //Devuelve la página que agrega nuevos policías
        public ActionResult Nuevo()
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            PoliciaViewModel modelo = new PoliciaViewModel()
            {
                TiposCedula = tablaGeneralDAL.Get("Policias", "tipoCedula").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                FechaNacimiento = DateTime.Today

            };
            return View(modelo);
        }
        public void CrearCarpetaPolicia(PoliciaViewModel model)
        {
            string folderPath = Server.MapPath(@"~\ArchivosSCAP\Policias\" + model.Cedula.ToString() + " - " + model.Nombre.ToString());
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                Console.WriteLine(folderPath);
            }
        }
        //Guarda la información ingresada en la página para crear policías
        [HttpPost]
        public ActionResult Nuevo(PoliciaViewModel model)
        {
            CrearCarpetaPolicia(model);
            policiaDAL = new PoliciaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            model.TiposCedula = tablaGeneralDAL.Get("Policias", "tipoCedula").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.CedulaPoliciaFiltrada = policiaDAL.GetCedulaPolicia(model.Cedula);
            try
            {
                if (!policiaDAL.CedulaPoliciaExiste(model.Cedula))
                {
                    if (ModelState.IsValid)
                    {
                        policiaDAL.Add(ConvertirPolicia(model));
                        int aux = policiaDAL.GetPoliciaCedula(model.Cedula).idPolicia;
                        return Redirect("~/Policia/Detalle/" + aux);
                    }
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        //Muestra la información detallada de un policía
        public ActionResult Detalle(int id)
        {
            policiaDAL = new PoliciaDAL();
            Session["idPolicia"] = id;
            Session["nombrePolicia"] = policiaDAL.GetPolicia(id).nombre;
            PoliciaViewModel modelo = CargarPolicia(policiaDAL.GetPolicia(id));            
            return View(modelo);
        }

        //Devuelve la página de edición de policías con sus apartados llenos
        public ActionResult Editar(int id)
        {
            policiaDAL = new PoliciaDAL();
            PoliciaViewModel modelo = CargarPolicia(policiaDAL.GetPolicia(id));
            modelo.TiposCedula = tablaGeneralDAL.Get("Policias", "tipoCedula").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            return View(modelo);
        }

        //Guarda la información modificada de los policías
        [HttpPost]
        public ActionResult Editar(PoliciaViewModel modelo)
        {
            policiaDAL = new PoliciaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            modelo.TiposCedula = tablaGeneralDAL.Get("Policias", "tipoCedula").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            try
            {
                if (ModelState.IsValid)
                {
                    policiaDAL.Edit(ConvertirPolicia(modelo));                   
                    return Redirect("~/Policia/Detalle/" + modelo.IdPolicia);
                }
                return View(modelo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Se encarga del cambio de estado de un policía entre activo e inactivo
        public ActionResult CambioEstado(int? id)
        {
            int estado;
            policiaDAL = new PoliciaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            try
            {
                if (tablaGeneralDAL.Get((int)id).descripcion == "Activo")
                {
                    estado = tablaGeneralDAL.Get("Generales", "estado", "Inactivo").idTablaGeneral;
                }
                else
                {
                    estado = tablaGeneralDAL.Get("Generales", "estado", "Activo").idTablaGeneral;
                }
                policiaDAL.CambiaEstadoPolicia((int)Session["idPolicia"], estado);
                return Redirect("~/Policia/Detalle/" + Session["idPolicia"]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        int? EstadoDefault(int? estado)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            if (estado == null)
            {
                return tablaGeneralDAL.Get("Generales", "estado", "Activo").idTablaGeneral;
            }
            else
            {
                return estado;
            }
        }

        string ObtenerEdad(DateTime? fechaNacimiento)
        {

            var edad = DateTime.Today.Year - fechaNacimiento.Value.Year;

            if (fechaNacimiento.Value.Date > DateTime.Today.AddYears(-edad)) edad--;

            return edad.ToString();

        }
    }
}