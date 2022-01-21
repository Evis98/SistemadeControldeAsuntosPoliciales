﻿using BackEnd;
using BackEnd.DAL;
using FrontEnd.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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
            tablaGeneralDAL = new TablaGeneralDAL();
            return (from d in infractores
                    select new ListInfractorViewModel
                    {
                        IdInfractor = d.idInfractor,
                        Identificacion = d.numeroDeIdentificacion,
                        Nacionalidad = tablaGeneralDAL.GetDescripcion(d.nacionalidad),
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
                nacionalidad = tablaGeneralDAL.GetNacionalidadInfractor(int.Parse(modelo.Nacionalidad)),
                nombreCompleto = modelo.Nombre,
                fechaNacimiento = modelo.FechaNacimiento,
                telefono = modelo.Telefono,
                direccionExacta = modelo.DireccionExacta,
                sexo = tablaGeneralDAL.GetTipoSexoInfractor(modelo.Sexo),
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
                TipoIdentificacion = int.Parse(tablaGeneralDAL.GetCodigo(infractor.tipoDeIdentificacion)),
                Identificacion = infractor.numeroDeIdentificacion,
                Nacionalidad = infractor.nacionalidad.ToString(),
                Nombre = infractor.nombreCompleto,
                FechaNacimiento = infractor.fechaNacimiento,
                Telefono = infractor.telefono,
                DireccionExacta = infractor.direccionExacta,
                Sexo = int.Parse(tablaGeneralDAL.GetCodigo(infractor.sexo)),
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
                Nacionalidad = tablaGeneralDAL.GetDescripcion(infractor.nacionalidad),
                Nombre = infractor.nombreCompleto,
                FechaNacimiento = infractor.fechaNacimiento.ToShortDateString(),
                Edad = ObtenerEdad(infractor.fechaNacimiento),
                Telefono = infractor.telefono,
                DireccionExacta = infractor.direccionExacta,
                Sexo = tablaGeneralDAL.GetDescripcion(infractor.sexo),
                CorreoElectronico = infractor.correoEletronico,
                Observaciones = infractor.observaciones,
                ProfesionUOficio = infractor.profesionUOficio,
                Estatura = infractor.estatura + " m",
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
                    if (filtroSeleccionado == "Cédula")
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
            infractores = infractores.OrderBy(x => x.nombreCompleto).ToList();
            return View(ConvertirListaInfractores(infractores));
        }

        public ActionResult Nuevo()
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            InfractorViewModel modelo = new InfractorViewModel()
            {
                Nacionalidades = tablaGeneralDAL.GetNacionalidadesInfractor().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                TiposDeIdentificacion = tablaGeneralDAL.GetTiposIdentificacionInfractor().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                TiposDeSexo = tablaGeneralDAL.GetTiposSexoInfractor().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                FechaNacimiento = DateTime.Today,


            };
            return View(modelo);
            
        }

        //Guarda la información ingresada en la página para crear infractores
        [HttpPost]
        public ActionResult Nuevo(InfractorViewModel model)
        {
            infractorDAL = new InfractorDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            model.CedulaFiltrada = infractorDAL.GetCedulaInfractor(model.Identificacion);
            try
            {
                if (!infractorDAL.IdentificacionExiste(model.Identificacion))
                {
                    if (ModelState.IsValid)
                    {

                        string rutaSitio = Server.MapPath("~/");
                        Infractores infractor = ConvertirInfractor(model);


                        if (model.Archivo != null)
                        {
                            string fileExt = System.IO.Path.GetExtension(model.Archivo.FileName);
                            if (fileExt == ".jpg")
                            {
                                string pathArchivo = Path.Combine(rutaSitio + @"Files" + model.Identificacion + ".jpg");
                                infractor.imagen = @"~\Files" + model.Identificacion + ".jpg";
                                model.Archivo.SaveAs(pathArchivo);
                            }
                            else if (fileExt == ".png")
                            {
                                string pathArchivo = Path.Combine(rutaSitio + @"Files" + model.Identificacion + ".png");
                                infractor.imagen = @"~\Files" + model.Identificacion + ".png";
                                model.Archivo.SaveAs(pathArchivo);
                            }
                        }
                        else
                        {
                            infractor.imagen = null;
                        }

                        infractorDAL.Add(infractor);
                        int aux = infractorDAL.GetNumeroIdInfractor(model.Identificacion);
                        return Redirect("~/Infractor/Detalle/" + aux);
                    }
                }
                model.Nacionalidades = tablaGeneralDAL.GetNacionalidadesInfractor().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                model.TiposDeIdentificacion = tablaGeneralDAL.GetTiposIdentificacionInfractor().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                model.TiposDeSexo = tablaGeneralDAL.GetTiposSexoInfractor().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
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
            modelo.Nacionalidades = tablaGeneralDAL.GetNacionalidadesInfractor().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            modelo.TiposDeIdentificacion = tablaGeneralDAL.GetTiposIdentificacionInfractor().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            modelo.TiposDeSexo = tablaGeneralDAL.GetTiposSexoInfractor().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });        
            return View(modelo);
        }

        //Guarda la información modificada de los policías
        [HttpPost]
        public ActionResult Editar(InfractorViewModel modelo)
        {
            infractorDAL = new InfractorDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            modelo.Nacionalidades = tablaGeneralDAL.GetNacionalidadesInfractor().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            modelo.TiposDeIdentificacion = tablaGeneralDAL.GetTiposIdentificacionInfractor().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            modelo.TiposDeSexo = tablaGeneralDAL.GetTiposSexoInfractor().Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            try
            {
                if (ModelState.IsValid)
                {
                    Infractores infractor = ConvertirInfractor(modelo);

                    string rutaSitio = Server.MapPath("~/");
                    
                    if (modelo.Archivo != null)
                    {

                        if (System.IO.File.Exists(infractor.imagen))
                        {
                            System.IO.File.Delete(infractor.imagen);
                        }
                        string fileExt = System.IO.Path.GetExtension(modelo.Archivo.FileName);
                        if (fileExt == ".jpg")
                        {
                            string pathArchivo = Path.Combine(rutaSitio + @"Files" + modelo.Identificacion + ".jpg");
                            infractor.imagen = @"~\Files" + modelo.Identificacion + ".jpg";
                            modelo.Archivo.SaveAs(pathArchivo);
                        }
                        else if (fileExt == ".png")
                        {
                            string pathArchivo = Path.Combine(rutaSitio + @"Files" + modelo.Identificacion + ".png");
                            infractor.imagen = @"~\Files" + modelo.Identificacion + ".png";
                            modelo.Archivo.SaveAs(pathArchivo);
                        }                        
                    }
                    else
                    {
                        infractor.imagen = modelo.Imagen;
                    }                    
                    infractorDAL.Edit(infractor);
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
