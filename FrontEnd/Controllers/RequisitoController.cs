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
                         fecha_Vencimiento = Convert.ToDateTime(d.fechaVencimiento),
                         TipoRequisito = (int)d.tipoRequsito,
                         IdPolicia = (int) d.idPolicia,
                         imagen = d.imagen,
                         detalles = d.detalles,
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
                if (ModelState.IsValid)
                {
                    var oRequisito = new Requisitos
                    {
                        idPolicia = model.IdPolicia,
                        detalles = model.detalles,
                        fechaVencimiento = model.fecha_Vencimiento,
                        tipoRequsito = model.TipoRequisito
                    };
                    string RutaSitio = Server.MapPath("~/");
                    string PathArchivo = Path.Combine(RutaSitio + "/Files/" + model.idRequisito + ".png");
                    model.imagen = PathArchivo;
                    model.Archivo.SaveAs(PathArchivo);
                    requisitoDAL.Add(oRequisito);
                    return Redirect("~/Requisito/");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return View();
        }
    }
}