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

        PoliciaDAL policiaDAL;

        //Devuelve la página con el listado de todos los policías creados
        public ActionResult Index(string searchBy, string search)
        {
            policiaDAL = new PoliciaDAL();
            List<Policias> lista2;
            List<Policias> lista3 = new List<Policias>();
            List<ListPoliciaViewModel> lista;

            lista2 = policiaDAL.Get();
            if (search != null)
            {
                foreach (Policias policia in lista2)
                {
                    if (searchBy == "Cedula")
                    {
                        if (policia.cedula.Contains(search))
                        {
                            lista3.Add(policia);
                        }
                    }
                    if (searchBy == "Nombre")
                    {
                        if (policia.nombre.Contains(search))
                        {
                            lista3.Add(policia);
                        }
                    }

                }
                lista2 = lista3;

            }

            lista = (from d in lista2
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

            return View(lista);
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
            int aux;
            try
            {
                if (ModelState.IsValid)
                {
                    {
                        var oPolicia = new Policias();
                        oPolicia.cedula = model.Cedula;
                        oPolicia.tipoCedula = policiaDAL.getTipoCedula(model.TipoCedula);
                        oPolicia.nombre = model.Nombre;
                        oPolicia.fechaNacimiento = Convert.ToDateTime(model.Fecha_nacimiento);
                        oPolicia.correoElectronico = model.CorreoElectronico;
                        oPolicia.direccion = model.Direccion;
                        oPolicia.telefonoCelular = model.TelefonoCelular;
                        oPolicia.telefonoCasa = model.TelefonoCasa;
                        oPolicia.contactoEmergencia = model.ContactoEmergencia;
                        oPolicia.telefonoEmergencia = model.TelefonoEmergencia;
                        oPolicia.estado = policiaDAL.estadoDefault();
                        policiaDAL.Add(oPolicia);
                        aux = policiaDAL.getPoliciaCedula(model.Cedula);
                    }
                    return Redirect("~/Policia/Detalle/" +aux);
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
            PoliciaViewModel modelo = new PoliciaViewModel();
            Session["idPolicia"] = id;
            {
                Policias oPolicia = policiaDAL.getPolicia(id);
                modelo.IdPolicia = oPolicia.idPolicia;
                modelo.Cedula = oPolicia.cedula;
                modelo.TipoCedula = (int)oPolicia.tipoCedula;
                modelo.Nombre = oPolicia.nombre;
                modelo.Fecha_nacimiento = Convert.ToDateTime(oPolicia.fechaNacimiento);
                modelo.CorreoElectronico = oPolicia.correoElectronico;
                modelo.Direccion = oPolicia.direccion;
                modelo.TelefonoCelular = oPolicia.telefonoCelular;
                modelo.TelefonoCasa = oPolicia.telefonoCasa;
                modelo.ContactoEmergencia = oPolicia.contactoEmergencia;
                modelo.TelefonoEmergencia = oPolicia.telefonoEmergencia;
                modelo.Estado = (int)oPolicia.estado;
            }
            return View(modelo);
        }

        //Devuelve la página de edición de policías con sus apartados llenos
        public ActionResult Editar(int id)
        {
            policiaDAL = new PoliciaDAL();
            PoliciaViewModel modelo = new PoliciaViewModel();
            {
                Policias oPolicia = policiaDAL.getPolicia(id);
                modelo.IdPolicia = oPolicia.idPolicia;
                modelo.Cedula = oPolicia.cedula;
                modelo.TipoCedula = (int)oPolicia.tipoCedula;
                modelo.Nombre = oPolicia.nombre;
                modelo.Fecha_nacimiento = Convert.ToDateTime(oPolicia.fechaNacimiento);
                modelo.CorreoElectronico = oPolicia.correoElectronico;
                modelo.Direccion = oPolicia.direccion;
                modelo.TelefonoCelular = oPolicia.telefonoCelular;
                modelo.TelefonoCasa = oPolicia.telefonoCasa;
                modelo.ContactoEmergencia = oPolicia.contactoEmergencia;
                modelo.TelefonoEmergencia = oPolicia.telefonoEmergencia;
                modelo.Estado = (int)oPolicia.estado;
            }
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
                    {
                        Policias oPolicia = policiaDAL.getPolicia(modelo.IdPolicia);
                        oPolicia.idPolicia = modelo.IdPolicia;
                        oPolicia.cedula = modelo.Cedula;
                        oPolicia.tipoCedula = modelo.TipoCedula;
                        oPolicia.nombre = modelo.Nombre;
                        oPolicia.fechaNacimiento = Convert.ToDateTime(modelo.Fecha_nacimiento);
                        oPolicia.correoElectronico = modelo.CorreoElectronico;
                        oPolicia.direccion = modelo.Direccion;
                        oPolicia.telefonoCelular = modelo.TelefonoCelular;
                        oPolicia.telefonoCasa = modelo.TelefonoCasa;
                        oPolicia.contactoEmergencia = modelo.ContactoEmergencia;
                        oPolicia.telefonoEmergencia = modelo.TelefonoEmergencia;
                        oPolicia.estado = modelo.Estado;
                        policiaDAL.Edit(oPolicia);
                    }
                    return Redirect("~/Policia/Detalle/" + Session["idPolicia"]);
                }

                return View(modelo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
                throw new Exception(ex.Message);
            }
        }

        //Se encarga del cambio de estado de un policía entre activo e inactivo
        public ActionResult CambioEstado(int id)
        {
            int estado = id;
            policiaDAL = new PoliciaDAL();
            try
            {
                if (ModelState.IsValid)
                {
                    {
                        string estadoPolicia = policiaDAL.getEstadoPolicia(id);
                        if (estadoPolicia == "Activo")
                        {
                            estado = policiaDAL.getIdEstado("Inactivo");
                        }
                        else
                        {
                            estado = policiaDAL.getIdEstado("Activo");
                        }
                    }
                    policiaDAL.CambiaEstadoPolicia((int)Session["idPolicia"], estado);
                    return Redirect("~/Policia/Detalle/"+Session["idPolicia"]);
                }
                return Redirect("~/Policia/Detalle/" + Session["idPolicia"]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
