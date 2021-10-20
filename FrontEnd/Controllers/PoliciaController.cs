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
    public class PoliciaController : Controller
    {
        IPoliciaDAL policiaDAL;
        ITablaGeneralDAL tablaGeneralDAL;

        public List<ListPoliciaViewModel> ConvertirListaPolicias(List<Policias> policias)
        {
            return (from d in policias
                    select new ListPoliciaViewModel
                    {
                        IdPolicia = d.idPolicia,
                        Cedula = d.cedula,
                        Nombre = d.nombre,
                        CorreoElectronico = d.correoElectronico,
                        Direccion = d.direccion,
                        TelefonoCelular = d.telefonoCelular,
                        TelefonoCasa = d.telefonoCasa,
                        ContactoEmergencia = d.contactoEmergencia,
                        TelefonoEmergencia = d.telefonoEmergencia,
                    }).ToList();
        }

        public Policias ConvertirPolicia(PoliciaViewModel modelo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            return new Policias
            {
                idPolicia = modelo.IdPolicia,
                cedula = modelo.Cedula,
                tipoCedula = tablaGeneralDAL.GetTipoCedula(modelo.TipoCedula),
                nombre = modelo.Nombre,
                fechaNacimiento = modelo.FechaNacimiento,
                correoElectronico = modelo.CorreoElectronico,
                direccion = modelo.Direccion,
                telefonoCelular = modelo.TelefonoCelular,
                telefonoCasa = modelo.TelefonoCasa,
                contactoEmergencia = modelo.ContactoEmergencia,
                telefonoEmergencia = modelo.TelefonoEmergencia,
                estado = tablaGeneralDAL.EstadoDefault()
            };
        }
        public PoliciaViewModel CargarPolicia(Policias policia)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            return new PoliciaViewModel
            {
                IdPolicia = policia.idPolicia,
                Cedula = policia.cedula,
                TipoCedula = policia.tipoCedula,
                Nombre = policia.nombre,
                FechaNacimiento = policia.fechaNacimiento,
                CorreoElectronico = policia.correoElectronico,
                Direccion = policia.direccion,
                TelefonoCelular = policia.telefonoCelular,
                TelefonoCasa = policia.telefonoCasa,
                ContactoEmergencia = policia.contactoEmergencia,
                TelefonoEmergencia = policia.telefonoEmergencia,
                Estado = policia.estado
            };
        }
        public ListPoliciaViewModel ConvertirPoliciaInverso(Policias policia)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            return new ListPoliciaViewModel
            {
                IdPolicia = policia.idPolicia,
                Cedula = policia.cedula,
                TipoCedula = tablaGeneralDAL.GetDescripcion(policia.tipoCedula),
                Nombre = policia.nombre,
                FechaNacimiento = policia.fechaNacimiento.Value.ToShortDateString(),
                Edad = ObtenerEdad(policia.fechaNacimiento),
                CorreoElectronico = policia.correoElectronico,
                Direccion = policia.direccion,
                TelefonoCelular = policia.telefonoCelular,
                TelefonoCasa = policia.telefonoCasa,
                ContactoEmergencia = policia.contactoEmergencia,
                TelefonoEmergencia = policia.telefonoEmergencia,
                Estado = (int)policia.estado,
                DescripcionEstado = tablaGeneralDAL.GetDescripcion(policia.estado)
            };
        }

        //Devuelve la página con el listado de todos los policías creados
        public ActionResult Index(string filtroSeleccionado, string busqueda)
        {
            policiaDAL = new PoliciaDAL();
            List<Policias> policias = policiaDAL.Get();
            List<Policias> policiasFiltrados = new List<Policias>();
            if (busqueda != null)
            {
                foreach (Policias policia in policias)
                {
                    if (filtroSeleccionado == "Cédula")
                    {
                        if (policia.cedula.Contains(busqueda))
                        {
                            policiasFiltrados.Add(policia);
                        }
                    }
                    if (filtroSeleccionado == "Nombre")
                    {
                        if (policia.nombre.Contains(busqueda))
                        {
                            policiasFiltrados.Add(policia);
                        }
                    }
                }
                policias = policiasFiltrados;
            }
            return View(ConvertirListaPolicias(policias));
        }

        //Devuelve la página que agrega nuevos policías
        public ActionResult Nuevo()
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            PoliciaViewModel modelo = new PoliciaViewModel()
            {
                TiposCedula = tablaGeneralDAL.GetTiposCedulaPolicia().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                FechaNacimiento = null

            };
            return View(modelo);
        }

        //Guarda la información ingresada en la página para crear policías
        [HttpPost]
        public ActionResult Nuevo(PoliciaViewModel model)
        {
          policiaDAL = new PoliciaDAL();
            try
            {
                if (ModelState.IsValid)
                {
                    policiaDAL.Add(ConvertirPolicia(model));
                    int aux = policiaDAL.GetPoliciaCedula(model.Cedula);
                    return Redirect("~/Policia/Detalle/" + aux);
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
            Session["idPolicia"] = id;
            policiaDAL = new PoliciaDAL();
            ListPoliciaViewModel modelo = ConvertirPoliciaInverso(policiaDAL.GetPolicia(id));
            return View(modelo);
        }

        //Devuelve la página de edición de policías con sus apartados llenos
        public ActionResult Editar(int id)
        {
            policiaDAL = new PoliciaDAL();
            PoliciaViewModel modelo = CargarPolicia(policiaDAL.GetPolicia(id));
            modelo.TiposCedula = tablaGeneralDAL.GetTiposCedulaPolicia().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            return View(modelo);
        }

        //Guarda la información modificada de los policías
        [HttpPost]
        public ActionResult Editar(PoliciaViewModel modelo)
        {
            policiaDAL = new PoliciaDAL();
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
                if (tablaGeneralDAL.GetDescripcion(id) == "Activo")
                {
                    estado = tablaGeneralDAL.GetIdEstado("Inactivo");
                }
                else
                {
                    estado = tablaGeneralDAL.GetIdEstado("Activo");
                }
                policiaDAL.CambiaEstadoPolicia((int)Session["idPolicia"], estado);
                return Redirect("~/Policia/Detalle/" + Session["idPolicia"]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        string ObtenerEdad(DateTime? fechaNacimiento) { 

            var edad = DateTime.Today.Year - fechaNacimiento.Value.Year;

            if (fechaNacimiento.Value.Date > DateTime.Today.AddYears(-edad)) edad--;

            return edad.ToString();

        }
    }
}
