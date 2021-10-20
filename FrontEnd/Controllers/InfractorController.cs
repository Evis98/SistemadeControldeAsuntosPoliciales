using BackEnd;
using BackEnd.DAL;
using FrontEnd.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    public class InfractorController : Controller
    {
        IInfractorDAL infractorDAL;
        ITablaGeneralDAL tablaGeneralDAL;

        public List<ListInfractorViewModel> ConvertirListaInfractores(List<Infractores> infractores)
        {
            return (from d in infractores
                    select new ListInfractorViewModel
                    {
                        IdInfractor = d.idInfractor,
                        Identificacion = d.numeroDeIdentificacion,
                        Nacionalidad = d.nacionalidad,
                        Nombre = d.nombreCompleto,
                        Telefono = d.telefono,
                        DireccionExacta = d.direccionExacta,
                        CorreoElectronico = d.correoEletronico,
                        Observaciones = d.observaciones,
                        ProfesionUOficio = d.profesionUOficio,
                        Estatura = d.estatura,
                        Tatuajes = d.tatuajes,
                        NombrePadre = d.nombreDelPadre,
                        NombreMadre = d.nombreDeLaMadre,
                        Imagen = d.imagen,
                    }).ToList();
        }

        public Infractores ConvertirInfractor(InfractorViewModel modelo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            return new Infractores
            {
                idInfractor = modelo.IdInfractor,
                tipoDeIdentificacion = tablaGeneralDAL.GetTipoIdentificacionInfractor(modelo.TipoIdentificacion),
                numeroDeIdentificacion = modelo.Identificacion,
                nacionalidad = modelo.Nacionalidad,
                nombreCompleto = modelo.Nombre,
                fechaNacimiento = modelo.FechaNacimiento,
                telefono = modelo.Telefono,
                direccionExacta = modelo.DireccionExacta,
                sexo = modelo.Sexo,
                correoEletronico = modelo.CorreoElectronico,
                observaciones = modelo.Observaciones,
                profesionUOficio = modelo.ProfesionUOficio,
                estatura = modelo.Estatura,
                tatuajes = modelo.Tatuajes,
                nombreDelPadre = modelo.NombrePadre,
                nombreDeLaMadre = modelo.NombreMadre,
                imagen = modelo.Imagen,
            };
        }

        public InfractorViewModel CargarInfractor(Infractores infractor)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            return new InfractorViewModel
            {
                IdInfractor = infractor.idInfractor,
                TipoIdentificacion = infractor.tipoDeIdentificacion,
                Identificacion = infractor.numeroDeIdentificacion,
                Nacionalidad = infractor.nacionalidad,
                Nombre = infractor.nombreCompleto,
                FechaNacimiento = infractor.fechaNacimiento,
                Telefono = infractor.telefono,
                DireccionExacta = infractor.direccionExacta,
                Sexo = infractor.sexo,
                CorreoElectronico = infractor.correoEletronico,
                Observaciones = infractor.observaciones,
                ProfesionUOficio = infractor.profesionUOficio,
                Estatura = infractor.estatura,
                Tatuajes = infractor.tatuajes,
                NombrePadre = infractor.nombreDelPadre,
                NombreMadre = infractor.nombreDeLaMadre,
                Imagen = infractor.imagen
            };
        }

        public ListInfractorViewModel ConvertirInfractorInverso(Infractores infractor)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            return new ListInfractorViewModel
            {
                IdInfractor = infractor.idInfractor,
                TipoIdentificacion = tablaGeneralDAL.GetDescripcion(infractor.tipoDeIdentificacion),
                Identificacion = infractor.numeroDeIdentificacion,
                Nacionalidad = infractor.nacionalidad,
                Nombre = infractor.nombreCompleto,
                FechaNacimiento = infractor.fechaNacimiento.ToShortDateString(),
                Edad = ObtenerEdad(infractor.fechaNacimiento),
                Telefono = infractor.telefono,
                DireccionExacta = infractor.direccionExacta,
                Sexo = tablaGeneralDAL.GetDescripcion(infractor.sexo),
                CorreoElectronico = infractor.correoEletronico,
                Observaciones = infractor.observaciones,
                ProfesionUOficio = infractor.profesionUOficio,
                Estatura = infractor.estatura,
                Tatuajes = infractor.tatuajes,
                NombrePadre = infractor.nombreDelPadre,
                NombreMadre = infractor.nombreDeLaMadre,
                Imagen = infractor.imagen
            };
        }

        string ObtenerEdad(DateTime? fechaNacimiento)
        {

            var edad = DateTime.Today.Year - fechaNacimiento.Value.Year;

            if (fechaNacimiento.Value.Date > DateTime.Today.AddYears(-edad)) edad--;

            return edad.ToString();

        }

        public ActionResult Index(string filtroSeleccionado, string busqueda)
        {
            infractorDAL = new InfractorDAL();
            List<Infractores> infractores = infractorDAL.Get();
            List<Infractores> infractoresFiltrados = new List<Infractores>();
            if (busqueda != null)
            {
                foreach (Infractores infractor in infractores)
                {
                    if (filtroSeleccionado == "No de Identificación")
                    {
                        if (infractor.numeroDeIdentificacion.Contains(busqueda))
                        {
                            infractoresFiltrados.Add(infractor);
                        }
                    }
                    if (filtroSeleccionado == "Nombre")
                    {
                        if (infractor.nombreCompleto.Contains(busqueda))
                        {
                            infractoresFiltrados.Add(infractor);
                        }
                    }
                }
                infractores = infractoresFiltrados;
            }
            return View(ConvertirListaInfractores(infractores));
        }

        public ActionResult Nuevo()
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            InfractorViewModel modelo = new InfractorViewModel()
            {
                TiposDeIdentificacion = tablaGeneralDAL.GetTiposIdentificacionInfractor().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                FechaNacimiento = DateTime.Today,


            };
            return View(modelo);
            
        }

        //Guarda la información ingresada en la página para crear infractores
        [HttpPost]
        public ActionResult Nuevo(InfractorViewModel model)
        {
            infractorDAL = new InfractorDAL();
            try
            {
                if (ModelState.IsValid)
                {
                    infractorDAL.Add(ConvertirInfractor(model));
                    int aux = infractorDAL.GetNumeroIdInfractor(model.IdInfractor);
                    return Redirect("~/Infractor/Detalle/" + aux);
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
            infractorDAL = new InfractorDAL();
            ListInfractorViewModel modelo = ConvertirInfractorInverso(infractorDAL.GetInfractor(id));
            return View(modelo);
        }

        //Devuelve la página de edición de policías con sus apartados llenos
        public ActionResult Editar(int id)
        {
            infractorDAL = new InfractorDAL();
            InfractorViewModel modelo = CargarInfractor(infractorDAL.GetInfractor(id));
            modelo.TiposDeIdentificacion = tablaGeneralDAL.GetTiposIdentificacionInfractor().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            return View(modelo);
        }

        //Guarda la información modificada de los policías
        [HttpPost]
        public ActionResult Editar(InfractorViewModel modelo)
        {
            infractorDAL = new InfractorDAL();
            try
            {
                if (ModelState.IsValid)
                {
                    infractorDAL.Edit(ConvertirInfractor(modelo));
                    return Redirect("~/Infractor/Detalle/" + modelo.IdInfractor);
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
