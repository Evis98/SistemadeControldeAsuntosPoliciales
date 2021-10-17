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
                fechaNacimiento = Convert.ToDateTime(modelo.Fecha_nacimiento),
                correoElectronico = modelo.CorreoElectronico,
                direccion = modelo.Direccion,
                telefonoCelular = modelo.TelefonoCelular,
                telefonoCasa = modelo.TelefonoCasa,
                contactoEmergencia = modelo.ContactoEmergencia,
                telefonoEmergencia = modelo.TelefonoEmergencia,
                estado = tablaGeneralDAL.EstadoDefault()
            };
        }

        public PoliciaViewModel ConvertirPoliciaInverso(Policias policia)
        {
            return new PoliciaViewModel
            {
                IdPolicia = policia.idPolicia,
                Cedula = policia.cedula,
                TipoCedula = (int)policia.tipoCedula,
                Nombre = policia.nombre,
                Fecha_nacimiento = Convert.ToDateTime(policia.fechaNacimiento),
                CorreoElectronico = policia.correoElectronico,
                Direccion = policia.direccion,
                TelefonoCelular = policia.telefonoCelular,
                TelefonoCasa = policia.telefonoCasa,
                ContactoEmergencia = policia.contactoEmergencia,
                TelefonoEmergencia = policia.telefonoEmergencia,
                Estado = (int)policia.estado
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
            return View();
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
            PoliciaViewModel modelo = ConvertirPoliciaInverso(policiaDAL.GetPolicia(id));
            return View(modelo);
        }

        //Devuelve la página de edición de policías con sus apartados llenos
        public ActionResult Editar(int id)
        {
            policiaDAL = new PoliciaDAL();
            PoliciaViewModel modelo = ConvertirPoliciaInverso(policiaDAL.GetPolicia(id));
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
        public ActionResult CambioEstado(int id)
        {
            int estado;
            policiaDAL = new PoliciaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            try
            {
                if (tablaGeneralDAL.GetEstadoPolicia(id) == "Activo")
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
    }
}
