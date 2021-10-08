﻿using System;
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
        // GET: Requisito
        public ActionResult Index(int id)
        {         
            requisitoDAL = new RequisitoDAL ();
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
                         IdPolicia = (int) d.idPolicia,
                         Imagen = d.imagen,
                         Detalles = d.detalles,
                     }).ToList();

            return View(lista);            
        }
       
        public ActionResult Agregar(int id)
        {
            RequisitoViewModel requisito = new RequisitoViewModel();
            {
                requisito.IdPolicia = id;
            }
            return View(requisito);
        }

        [HttpPost]
        public ActionResult Agregar(RequisitoViewModel model)
        {
            requisitoDAL = new RequisitoDAL();
            try
            {
                string RutaSitio = Server.MapPath("~/");
                string PathArchivo = Path.Combine(RutaSitio + @"Files\" + model.Detalles+ Session["idPolicia"].ToString() + ".pdf");
                if (ModelState.IsValid)
                {
                    var oRequisito = new Requisitos();
                    oRequisito.idPolicia = Convert.ToInt32(Session["idPolicia"]);
                    oRequisito.detalles = model.Detalles;
                    oRequisito.fechaVencimiento = model.Fecha_Vencimiento;
                    oRequisito.tipoRequsito = model.TipoRequisito;
                    oRequisito.imagen = @"~\Files\" + model.Detalles + Session["idPolicia"].ToString() + ".pdf";
                    model.Archivo.SaveAs(PathArchivo);
                    requisitoDAL.Add(oRequisito);
                    return Redirect("~/Requisito/Index/" + Session["idPolicia"].ToString());
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return View();
        }

        public ActionResult Detalle(int id)
        {
            PoliciaDAL policiaDAL = new PoliciaDAL();
            Policias policia = policiaDAL.getPolicia(Convert.ToInt32(Session["idPolicia"]));
            requisitoDAL = new RequisitoDAL();
            RequisitoViewModel modelo = new RequisitoViewModel();
            Session["idRequisito"] = id;
            {
                Requisitos oRequisito = requisitoDAL.getRequisito(id);
                modelo.Imagen = oRequisito.imagen;
                modelo.TipoRequisito = (int)oRequisito.tipoRequsito;
                modelo.Detalles = oRequisito.detalles;
                modelo.IdPolicia = (int)oRequisito.idPolicia;
                modelo.Nombre = policia.nombre;
            }
            return View(modelo);
        }
        public ActionResult Eliminar(int id)
        {
            //RequisitoViewModel modelo = new RequisitoViewModel();
            requisitoDAL = new RequisitoDAL();
            {
                var oRequisito = requisitoDAL.getRequisito(id);
                requisitoDAL.EliminaRequisito(oRequisito);
            }
            return Redirect("~/Requisito/Index/" + Session["idPolicia"].ToString());
        }
        public ActionResult Editar(int id)
        {
            requisitoDAL = new RequisitoDAL();
            RequisitoViewModel modelo = new RequisitoViewModel();
            {
                Requisitos oRequisito = requisitoDAL.getRequisito(id);
                modelo.IdRequisito = oRequisito.idRequisito;
                modelo.Detalles = oRequisito.detalles;
                modelo.Fecha_Vencimiento = Convert.ToDateTime(oRequisito.fechaVencimiento);
                modelo.IdPolicia = (int)oRequisito.idPolicia;
                modelo.TipoRequisito = (int)oRequisito.tipoRequsito;
                modelo.Imagen = oRequisito.imagen;
            }
            return View(modelo);
        }

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
                        oRequisito.fechaVencimiento = Convert.ToDateTime(modelo.Fecha_Vencimiento);
                        oRequisito.idPolicia = (int)modelo.IdPolicia;
                        oRequisito.tipoRequsito = (int)modelo.TipoRequisito;
                        oRequisito.imagen = modelo.Imagen;
                        requisitoDAL.Edit(oRequisito);
                    }
                    return Redirect("~/Requisito/Index/" + Session["idPolicia"].ToString());
                }

                return View(modelo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}