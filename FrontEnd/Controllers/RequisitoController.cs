using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BackEnd.DAL;
using BackEnd;
using FrontEnd.Models;
using FrontEnd.Models.ViewModels;

namespace FrontEnd.Controllers
{
    public class RequisitoController : Controller
    {
        RequisitoDAL requisitoDAL;
        PoliciaDAL policiaDAL;


        //*Devuelve la página con el listado de todos los requisitos creados para el policía seleccionado
        public ActionResult Listado(int id)
        {
            requisitoDAL = new RequisitoDAL();
            List<Requisitos> lista2;
            List<Requisitos> lista3 = new List<Requisitos>();
            List<ListRequisitoViewModel> lista;

            lista2 = requisitoDAL.Get();
            foreach (Requisitos requisito in lista2)
            {
                if (id == requisito.idPolicia)
                {
                    lista3.Add(requisito);
                }
            }
            lista2 = lista3;
            lista = (from d in lista2
                     select new ListRequisitoViewModel
                     {
                         IdRequisito = d.idRequisito,
                         Fecha_Vencimiento = Convert.ToDateTime(d.fechaVencimiento),
                         TipoRequisito = (int)d.tipoRequsito,
                         IdPolicia = (int)d.idPolicia,
                         Imagen = d.imagen,
                         Detalles = d.detalles,
                     }).ToList();
            return View(lista);
        }

        //Devuelve la página que agrega nuevos requisitos
        public ActionResult Nuevo(int id)
        {
            RequisitoViewModel requisito = new RequisitoViewModel();
            {
                requisito.IdPolicia = id;
            }
            return View(requisito);
        }

        //Guarda la información ingresada en la página para crear requisitos
        [HttpPost]
        public ActionResult Nuevo(RequisitoViewModel model)
        {
            requisitoDAL = new RequisitoDAL();
            try
            {
                string RutaSitio = Server.MapPath("~/");
                string PathArchivo = Path.Combine(RutaSitio + @"Files\" + model.Detalles + Session["idPolicia"].ToString() + ".pdf");
                if (ModelState.IsValid)
                {
                    var oRequisito = new Requisitos();
                    oRequisito.idPolicia = Convert.ToInt32(Session["idPolicia"]);
                    oRequisito.detalles = model.Detalles;
                    oRequisito.fechaVencimiento = model.Fecha_Vencimiento;
                    oRequisito.tipoRequsito = requisitoDAL.getTipoRequisito(model.TipoRequisito);
                    if (model.Fecha_Vencimiento != null)
                    {
                        oRequisito.fechaVencimiento = model.Fecha_Vencimiento;
                    }
                    else
                    {
                        oRequisito.fechaVencimiento = null;
                    }
                    if (model.Archivo != null)
                    {
                        oRequisito.imagen = @"~\Files\" + model.Detalles + Session["idPolicia"].ToString() + ".pdf";
                        model.Archivo.SaveAs(PathArchivo);
                    }
                    else
                    {
                        oRequisito.imagen = null ;
                    }
                    requisitoDAL.Add(oRequisito);
                    return Redirect("~/Requisito/Listado/" + Session["idPolicia"].ToString());
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return View();
        }

        //Muestra la información detallada del requisito seleccionado
        public ActionResult Detalle(int id)
        {
            policiaDAL = new PoliciaDAL();
            requisitoDAL = new RequisitoDAL();
            Requisitos oRequisito = new Requisitos();

            oRequisito = requisitoDAL.getRequisito(id);
            Policias policia = policiaDAL.getPolicia((int)oRequisito.idPolicia);

            RequisitoViewModel modelo = new RequisitoViewModel();
            {
                modelo.Imagen = oRequisito.imagen;
                modelo.IdRequisito = oRequisito.idRequisito;
                modelo.Fecha_Vencimiento = oRequisito.fechaVencimiento;
                modelo.TipoRequisito = (int)oRequisito.tipoRequsito;
                modelo.Detalles = oRequisito.detalles;
                modelo.IdPolicia = (int)oRequisito.idPolicia;
                modelo.Nombre = policia.nombre;
            }
            return View(modelo);
        }

        //Permite la eliminación de requisitos de la base de datos
        public ActionResult Eliminar(int id)
        {
            requisitoDAL = new RequisitoDAL();
            int idPolicia;

            var oRequisito = requisitoDAL.getRequisito(id);
            idPolicia = (int)oRequisito.idPolicia;
            requisitoDAL.EliminaRequisito(oRequisito);

            return Redirect("~/Requisito/Listado/" + idPolicia);
        }

        //Devuelve la página de edición de requisitos con sus apartados llenos
        public ActionResult Editar(int id)
        {
            requisitoDAL = new RequisitoDAL();
            RequisitoViewModel modelo = new RequisitoViewModel();
            {
                Requisitos oRequisito = requisitoDAL.getRequisito(id);
                modelo.IdRequisito = oRequisito.idRequisito;
                modelo.Detalles = oRequisito.detalles;
                if (oRequisito.fechaVencimiento != null)
                {
                    modelo.Fecha_Vencimiento = Convert.ToDateTime(oRequisito.fechaVencimiento);
                }
                else 
                {
                    modelo.Fecha_Vencimiento = null;
                }
                modelo.IdPolicia = (int)oRequisito.idPolicia;
                modelo.TipoRequisito = (int)oRequisito.tipoRequsito;
                modelo.Imagen = oRequisito.imagen;
            }
            return View(modelo);
        }

        //Guarda la información modificada de los requisitos
        [HttpPost]
        public ActionResult Editar(RequisitoViewModel modelo)
        {
            requisitoDAL = new RequisitoDAL();
            try
            {
                if (ModelState.IsValid)
                {
                    {
                        var oRequisito = requisitoDAL.getRequisito(modelo.IdRequisito);
                        oRequisito.idRequisito = modelo.IdRequisito;
                        oRequisito.detalles = modelo.Detalles;
                        oRequisito.fechaVencimiento = modelo.Fecha_Vencimiento;
                        oRequisito.idPolicia = (int)modelo.IdPolicia;
                        oRequisito.tipoRequsito = requisitoDAL.getTipoRequisito(modelo.TipoRequisito);
                        oRequisito.imagen = modelo.Imagen;
                        requisitoDAL.Edit(oRequisito);
                    }
                    return Redirect("~/Requisito/Listado/" + Session["idPolicia"].ToString());
                }

                return View(modelo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Devuelve la página con el listado de todos los requisitos creados
        public ActionResult Index(string searchBy, string search, string tipoRequisito)
        {
            requisitoDAL = new RequisitoDAL();
            List<Requisitos> lista2;
            List<Requisitos> lista3 = new List<Requisitos>();
            List<ListRequisitoViewModel> lista;

            lista2 = requisitoDAL.Get();
            if (search != null)
            {
                foreach (Requisitos requisito in lista2)
                {
                    if (searchBy == "Detalle de Requisito")
                    {
                        if (requisito.detalles.Contains(search))
                        {
                            lista3.Add(requisito);
                        }
                    }
                    if (searchBy == "Tipo de Requisito")
                    {
                        if (requisitoDAL.getDescripcionRequisito((int)requisito.tipoRequsito).Contains(tipoRequisito))
                        {
                            lista3.Add(requisito);
                        }
                    }
                }
                lista2 = lista3;

            }

            lista = (from d in lista2
                     select new ListRequisitoViewModel
                     {
                         IdRequisito = d.idRequisito,
                         Fecha_Vencimiento = Convert.ToDateTime(d.fechaVencimiento),
                         TipoRequisito = (int)d.tipoRequsito,
                         IdPolicia = (int)d.idPolicia,
                         Imagen = d.imagen,
                         Detalles = d.detalles,
                     }).ToList();

            return View(lista);
        }
    }
}