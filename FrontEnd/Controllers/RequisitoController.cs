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
            policiaDAL = new PoliciaDAL();
            return (from d in requisitos
                    select new ListRequisitoViewModel
                    {
                        IdRequisito = d.idRequisito,
                        DetalleTipoRequisito = tablaGeneralDAL.GetDescripcion(d.tipoRequisito),
                        Imagen = d.imagen,
                        Detalles = d.detalles,
                        NombrePolicia = policiaDAL.GetPolicia(d.idPolicia).nombre
                    }).ToList();
        }

        public Requisitos ConvertirRequisito(RequisitoViewModel modelo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            return new Requisitos
            {
                idRequisito = modelo.IdRequisito,
                idPolicia = (int)Session["idPolicia"],
                detalles = modelo.Detalles,
                fechaVencimiento = modelo.FechaVencimiento,
                tipoRequisito = tablaGeneralDAL.GetTipoRequisito(modelo.TipoRequisito),
                imagen = modelo.Imagen
            };           
        }

       public RequisitoViewModel CargarRequisito(Requisitos requisito)
        {
            return new RequisitoViewModel
            {
                Imagen = requisito.imagen,
                IdRequisito = requisito.idRequisito,
                FechaVencimiento = requisito.fechaVencimiento,
                TipoRequisito = int.Parse(tablaGeneralDAL.GetCodigo(requisito.tipoRequisito)),
                Detalles = requisito.detalles,
                IdPolicia = requisito.idPolicia
            };
        }
       
        public ListRequisitoViewModel ConvertirRequisitoInverso(Requisitos requisito)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            string fechaVencimiento = null;
            if (requisito.fechaVencimiento.HasValue)
            {
                fechaVencimiento = requisito.fechaVencimiento.Value.ToShortDateString();
            }
            return new ListRequisitoViewModel
            {
                Imagen = requisito.imagen,
                IdRequisito = requisito.idRequisito,
                FechaVencimiento = fechaVencimiento,
                TipoRequisito = tablaGeneralDAL.GetDescripcion(requisito.tipoRequisito),
                Detalles = requisito.detalles,
                IdPolicia = requisito.idPolicia
            };
        }

        //Devuelve la página con el listado de todos los requisitos creados
        public ActionResult Index(string filtroSeleccionado, string busqueda, string tipoRequisito)
        {
            requisitoDAL = new RequisitoDAL();
            tablaGeneralDAL = new TablaGeneralDAL();

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
                        if (tablaGeneralDAL.GetDescripcion(requisito.tipoRequisito).Contains(tipoRequisito))
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
            tablaGeneralDAL = new TablaGeneralDAL();
            RequisitoViewModel requisito = new RequisitoViewModel
            {
                TiposRequisito = tablaGeneralDAL.GetTiposRequisito().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                IdPolicia = id
            };
            return View(requisito);
        }

        //Guarda la información ingresada en la página para crear requisitos
        [HttpPost]
        public ActionResult Nuevo(RequisitoViewModel modelo)
        {
            requisitoDAL = new RequisitoDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            try
            {
                if (ModelState.IsValid)
                {
                    string rutaSitio = Server.MapPath("~/");
                    string pathArchivo = Path.Combine(rutaSitio + @"Files\" + modelo.Detalles + Session["idPolicia"].ToString() + ".pdf");
                    Requisitos requisito = ConvertirRequisito(modelo);
                    if (modelo.FechaVencimiento != null)
                    {
                        requisito.fechaVencimiento = modelo.FechaVencimiento;
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
                modelo.TiposRequisito = tablaGeneralDAL.GetTiposRequisito().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                return View(modelo);
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
            ListRequisitoViewModel modelo = ConvertirRequisitoInverso(requisitoDAL.GetRequisito(id));
            modelo.NombrePolicia = policiaDAL.GetPolicia(modelo.IdPolicia).nombre;
            return View(modelo);
        }

        //Permite la eliminación de requisitos de la base de datos
        public ActionResult Eliminar(int id)
        {
            requisitoDAL = new RequisitoDAL();
            Requisitos requisito = requisitoDAL.GetRequisito(id);
            int? idPolicia = requisito.idPolicia;
            requisitoDAL.EliminaRequisito(requisito);
            return Redirect("~/Requisito/Listado/" + idPolicia);
        }

        //Devuelve la página de edición de requisitos con sus apartados llenos
        public ActionResult Editar(int id)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            requisitoDAL = new RequisitoDAL();
            RequisitoViewModel modelo = CargarRequisito(requisitoDAL.GetRequisito(id));
            modelo.TiposRequisito = tablaGeneralDAL.GetTiposRequisito().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
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
                    if (modelo.Archivo != null && System.IO.File.Exists(requisitoDAL.GetRequisito(modelo.IdRequisito).imagen))
                    {
                        System.IO.File.Delete(requisitoDAL.GetRequisito(modelo.IdRequisito).imagen);
                        modelo.Archivo.SaveAs(modelo.Imagen);
                    }
                    requisitoDAL.Edit(ConvertirRequisito(modelo));
                    return Redirect("~/Requisito/Listado/" + modelo.IdPolicia);
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