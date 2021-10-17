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
        IRequisitoDAL requisitoDAL;
        IPoliciaDAL policiaDAL;
        ITablaGeneralDAL tablaGeneralDAL;

        public List<ListRequisitoViewModel> ConvertirListaRequisitos(List<Requisitos> requisitos)
        {
            requisitoDAL = new RequisitoDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            return (from d in requisitos
                    select new ListRequisitoViewModel
                    {
                        IdRequisito = d.idRequisito,
                        Fecha_Vencimiento = Convert.ToDateTime(d.fechaVencimiento),
                        TipoRequisito = (int)d.tipoRequsito,
                        DetalleTipoRequisito = tablaGeneralDAL.GetDescripcionRequisito((int)d.tipoRequsito),
                        IdPolicia = (int)d.idPolicia,
                        Imagen = d.imagen,
                        Detalles = d.detalles,
                        NombrePolicia = policiaDAL.GetPolicia((int)d.idPolicia).nombre
                    }).ToList();
        }

        public Requisitos ConvertirRequisito(RequisitoViewModel modelo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            return new Requisitos
            {
                idRequisito = modelo.IdRequisito,
                idPolicia = Convert.ToInt32(Session["idPolicia"]),
                detalles = modelo.Detalles,
                fechaVencimiento = modelo.Fecha_Vencimiento,
                tipoRequsito = tablaGeneralDAL.GetTipoRequisito(modelo.TipoRequisito),
                imagen = modelo.Imagen
            };           
        }

        public RequisitoViewModel ConvertirRequisitoInverso(Requisitos requisito)
        {
            return new RequisitoViewModel
            {
                Imagen = requisito.imagen,
                IdRequisito = requisito.idRequisito,
                Fecha_Vencimiento = requisito.fechaVencimiento,
                TipoRequisito = (int)requisito.tipoRequsito,
                Detalles = requisito.detalles,
                IdPolicia = (int)requisito.idPolicia
            };
        }

        //Devuelve la página con el listado de todos los requisitos creados
        public ActionResult Index(string filtroSeleccionado, string busqueda, string tipoRequisito)
        {
            requisitoDAL = new RequisitoDAL();
            policiaDAL = new PoliciaDAL();

            List<Requisitos> requisitos = requisitoDAL.Get();
            List<Requisitos> requisitosFiltrados = new List<Requisitos>();
            if (busqueda != null)
            {
                foreach (Requisitos requisito in requisitos)
                {
                    if (filtroSeleccionado == "Detalle de Requisito")
                    {
                        if (requisito.detalles.Contains(busqueda))
                        {
                            requisitosFiltrados.Add(requisito);
                        }
                    }
                    if (filtroSeleccionado == "Tipo de Requisito")
                    {
                        if (tablaGeneralDAL.GetDescripcionRequisito((int)requisito.tipoRequsito).Contains(tipoRequisito))
                        {
                            requisitosFiltrados.Add(requisito);
                        }
                    }
                }
                requisitos =requisitosFiltrados;
            }
            return View(ConvertirListaRequisitos(requisitos));
        }

        //*Devuelve la página con el listado de todos los requisitos creados para el policía seleccionado
        public ActionResult Listado(int id)
        {
            requisitoDAL = new RequisitoDAL();
            List<Requisitos> requisitos = requisitoDAL.Get();
            List<Requisitos> requisitosPolicia = new List<Requisitos>();
            foreach (Requisitos requisito in requisitos)
            {
                if (id == requisito.idPolicia)
                {
                    requisitosPolicia.Add(requisito);
                }
            }
            requisitos = requisitosPolicia;
            return View(ConvertirListaRequisitos(requisitos));
        }

        //Devuelve la página que agrega nuevos requisitos
        public ActionResult Nuevo(int id)
        {
            RequisitoViewModel requisito = new RequisitoViewModel
            {
                IdPolicia = id
            };
            return View(requisito);
        }

        //Guarda la información ingresada en la página para crear requisitos
        [HttpPost]
        public ActionResult Nuevo(RequisitoViewModel modelo)
        {
            requisitoDAL = new RequisitoDAL();
            try
            {
                if (ModelState.IsValid)
                {
                    string rutaSitio = Server.MapPath("~/");
                    string pathArchivo = Path.Combine(rutaSitio + @"Files\" + modelo.Detalles + Session["idPolicia"].ToString() + ".pdf");
                    Requisitos requisito = ConvertirRequisito(modelo);
                    if (modelo.Fecha_Vencimiento != null)
                    {
                        requisito.fechaVencimiento = modelo.Fecha_Vencimiento;
                    }
                    else
                    {
                        requisito.fechaVencimiento = null;
                    }
                    if (modelo.Archivo != null)
                    {
                        requisito.imagen = @"~\Files\" + modelo.Detalles + Session["idPolicia"].ToString() + ".pdf";
                        modelo.Archivo.SaveAs(pathArchivo);
                    }
                    else
                    {
                        requisito.imagen = null;
                    }
                    requisitoDAL.Add(requisito);
                    return Redirect("~/Requisito/Listado/" + Session["idPolicia"].ToString());
                }
                return View(modelo.IdPolicia);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }           
        }

        //Muestra la información detallada del requisito seleccionado
        public ActionResult Detalle(int id)
        {
            policiaDAL = new PoliciaDAL();
            requisitoDAL = new RequisitoDAL();
            RequisitoViewModel modelo = ConvertirRequisitoInverso(requisitoDAL.GetRequisito(id));
            modelo.Nombre = policiaDAL.GetPolicia(modelo.IdPolicia).nombre;
            return View(modelo);
        }

        //Permite la eliminación de requisitos de la base de datos
        public ActionResult Eliminar(int id)
        {
            requisitoDAL = new RequisitoDAL();
            Requisitos requisito = requisitoDAL.GetRequisito(id);
            int idPolicia = (int)requisito.idPolicia;
            requisitoDAL.EliminaRequisito(requisito);
            return Redirect("~/Requisito/Listado/" + idPolicia);
        }

        //Devuelve la página de edición de requisitos con sus apartados llenos
        public ActionResult Editar(int id)
        {
            requisitoDAL = new RequisitoDAL();        
            return View(ConvertirRequisitoInverso(requisitoDAL.GetRequisito(id)));
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
                    requisitoDAL.Edit(ConvertirRequisito(modelo));                   
                    return Redirect("~/Requisito/Listado/" + Session["idPolicia"].ToString());
                }
                return View(ConvertirRequisito(modelo));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}