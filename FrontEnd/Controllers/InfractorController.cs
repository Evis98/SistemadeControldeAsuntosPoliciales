using BackEnd;
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
        IAuditoriaDAL auditoriaDAL;
        IUsuarioDAL usuarioDAL;

        //Metodos Útiles
        public void Autorizar()
        {
            if (Session["userID"] != null)
            {
                if (Session["Rol"].ToString() == "4")
                {
                    Session["Error"] = "Usuario no autorizado";
                    Response.Redirect("~/Error/ErrorUsuario.cshtml");
                }
            }
            else
            {
                Response.Redirect("~/Login/Index");
            }
        }
        public void AutorizarEditar()
        {
            if (Session["userID"] != null)
            {
                if (Session["Rol"].ToString() == "4" || Session["Rol"].ToString() == "1")
                {
                    Session["Error"] = "Usuario no autorizado";
                    Response.Redirect("~/Error/ErrorUsuario.cshtml");
                }
            }
            else
            {
                Response.Redirect("~/Login/Index");
            }
        }

        public Infractores ConvertirInfractor(InfractorViewModel modelo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            Infractores infractor = new Infractores()
            {
                idInfractor = modelo.IdInfractor,
                tipoDeIdentificacion = tablaGeneralDAL.GetCodigo("Generales", "tipoDeIdentificacion", modelo.TipoIdentificacion.ToString()).idTablaGeneral,
                numeroDeIdentificacion = modelo.Identificacion.ToUpper(),
                nacionalidad = tablaGeneralDAL.GetCodigo("Generales", "nacionalidad", modelo.Nacionalidad.ToString()).idTablaGeneral,
                nombreCompleto = modelo.Nombre.ToUpper(),
                fechaNacimiento = modelo.FechaNacimiento.Date,
                telefono = modelo.Telefono,
                direccionExacta = modelo.DireccionExacta.ToUpper(),
                sexo = tablaGeneralDAL.GetCodigo("Generales", "sexo", modelo.Sexo.ToString()).idTablaGeneral,
                correoEletronico = modelo.CorreoElectronico,
                observaciones = modelo.Observaciones,
                estatura = modelo.Estatura,
                imagen = modelo.Imagen
            };
            if (modelo.ProfesionUOficio != null)
            {
                infractor.profesionUOficio = modelo.ProfesionUOficio.ToUpper();
            }
            if (modelo.Tatuajes != null)
            {
                infractor.tatuajes = modelo.Tatuajes.ToUpper();
            }
            if (modelo.NombrePadre != null)
            {
                infractor.nombreDelPadre = modelo.NombrePadre.ToUpper();
            }
            if (modelo.NombreMadre != null)
            {
                infractor.nombreDeLaMadre = modelo.NombreMadre.ToUpper();
            }
            if (modelo.ApodoInfractor != null)
            {
                infractor.apodoInfractor = modelo.ApodoInfractor.ToUpper();
            }
            if (modelo.RasgosFisicos != null)
            {
                infractor.rasgosFisicosInfractor = modelo.RasgosFisicos.ToUpper();
            }
            return infractor;
        }

        public InfractorViewModel CargarInfractor(Infractores infractor)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            InfractorViewModel model = new InfractorViewModel()
            {
                IdInfractor = infractor.idInfractor,
                TipoIdentificacion = int.Parse(tablaGeneralDAL.Get(infractor.tipoDeIdentificacion).codigo),
                VistaTipoidentifiacion = tablaGeneralDAL.Get(infractor.tipoDeIdentificacion).descripcion,
                Identificacion = infractor.numeroDeIdentificacion,
                Nacionalidad = tablaGeneralDAL.Get(infractor.nacionalidad).codigo,
                VistaNacionalidad = tablaGeneralDAL.Get(infractor.nacionalidad).descripcion,
                Nombre = infractor.nombreCompleto,
                FechaNacimiento = infractor.fechaNacimiento,
                Telefono = infractor.telefono,
                DireccionExacta = infractor.direccionExacta,
                Sexo = int.Parse(tablaGeneralDAL.Get(infractor.sexo).codigo),
                VistaSexo = tablaGeneralDAL.Get(infractor.sexo).descripcion,
                CorreoElectronico = infractor.correoEletronico,
                Observaciones = infractor.observaciones,
                ProfesionUOficio = infractor.profesionUOficio,
                Estatura = infractor.estatura,
                Tatuajes = infractor.tatuajes,
                NombrePadre = infractor.nombreDelPadre,
                NombreMadre = infractor.nombreDeLaMadre,
                RasgosFisicos = infractor.rasgosFisicosInfractor,
                ApodoInfractor = infractor.apodoInfractor,
                Imagen = infractor.imagen
            };
            return model;
        }

        public void CrearCarpetaInfractor(InfractorViewModel model)
        {
            string folderPath = Server.MapPath(@"~\ArchivosSCAP\Infractores\" + model.Identificacion.ToString() + "-" + model.Nombre.ToString());
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                Console.WriteLine(folderPath);
            }
            string folderPath2 = Server.MapPath(@"~\ArchivosSCAP\Infractores\" + model.Identificacion.ToString() + "-" + model.Nombre.ToString() + @"\" + @"Imagen\");
            if (!Directory.Exists(folderPath2))
            {
                Directory.CreateDirectory(folderPath2);
                Console.WriteLine(folderPath2);
            }
        }
        public Auditorias ConvertirAuditoria(AuditoriaViewModel modelo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            return new Auditorias
            {
                idAuditoria = modelo.IdAuditoria,
                idCategoria = modelo.IdCategoria,
                idElemento = modelo.IdElemento,
                fecha = DateTime.Now,
                accion = modelo.Accion,
                idUsuario = modelo.IdUsuario,

            };
        }

        string ObtenerEdad(DateTime? fechaNacimiento)
        {
            var edad = DateTime.Today.Year - fechaNacimiento.Value.Year;
            if (fechaNacimiento.Value.Date > DateTime.Today.AddYears(-edad)) edad--;
            return edad.ToString();

        }

        //Metodos de las Vistas
        public ActionResult Index(string filtrosSeleccionado, string busqueda)
        {
            Autorizar();
            infractorDAL = new InfractorDAL();
            tablaGeneralDAL = new TablaGeneralDAL();

            //Carga combobox busqueda
            List<TablaGeneral> comboindex = tablaGeneralDAL.Get("Infractores", "index");
            List<SelectListItem> items = comboindex.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.descripcion
                };
            });
            ViewBag.items = items;

            //Carga lista de policias
            List<InfractorViewModel> infractores = new List<InfractorViewModel>();
            List<InfractorViewModel> infractoresFiltrados = new List<InfractorViewModel>();
            foreach (Infractores infractor in infractorDAL.Get())
            {
                infractores.Add(CargarInfractor(infractor));
            }
            if (busqueda != null)
            {
                foreach (InfractorViewModel infractor in infractores)
                {
                    if (filtrosSeleccionado == "Cédula")
                    {
                        if (infractor.Identificacion.Contains(busqueda))
                        {
                            infractoresFiltrados.Add(infractor);
                        }
                    }
                    if (filtrosSeleccionado == "Nombre")
                    {
                        if (infractor.Nombre.Contains(busqueda))
                        {
                            infractoresFiltrados.Add(infractor);
                        }
                    }
                }
                infractores = infractoresFiltrados;
            }
            return View(infractores.OrderBy(x => x.Nombre).ToList());
        }


        public ActionResult Nuevo()
        {
            Autorizar();
            tablaGeneralDAL = new TablaGeneralDAL();
            InfractorViewModel modelo = new InfractorViewModel()
            {
                Nacionalidades = tablaGeneralDAL.Get("Generales", "nacionalidad").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                TiposDeIdentificacion = tablaGeneralDAL.Get("Generales", "tipoDeIdentificacionInfractor").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                TiposDeSexo = tablaGeneralDAL.Get("Generales", "sexo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                FechaNacimiento = DateTime.Today

            };
            return View(modelo);

        }

        [HttpPost]
        public ActionResult Nuevo(InfractorViewModel modelo)
        {
            Autorizar();
            infractorDAL = new InfractorDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();

            AuditoriaViewModel auditoria_modelo = new AuditoriaViewModel
            {
                Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "1").idTablaGeneral,
                IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "3").idTablaGeneral,
                IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario
            };

            try
            {
                if (ModelState.IsValid)
                {
                    int errores = 0;
                    if (infractorDAL.InfractorExiste(modelo.Identificacion))
                    {
                        ModelState.AddModelError(nameof(modelo.Identificacion), "La cédula ingresada ya existe");
                        errores++;
                    }
                    if (errores == 0)
                    {
                        CrearCarpetaInfractor(modelo);
                        string rutaSitio = Server.MapPath("~/");
                        Infractores infractor = ConvertirInfractor(modelo);

                        if (modelo.Archivo != null)
                        {
                            string fileExt = System.IO.Path.GetExtension(modelo.Archivo.FileName);
                            if (fileExt == ".jpg")
                            {
                                string pathArchivo = Path.Combine(rutaSitio + @"ArchivosSCAP\Infractores\" + modelo.Identificacion.ToString() + "-" + modelo.Nombre.ToString() + @"\" + @"Imagen\" + modelo.Nombre.ToString() + modelo.Identificacion + ".jpg");
                                infractor.imagen = @"~\ArchivosSCAP\Infractores\" + modelo.Identificacion.ToString() + "-" + modelo.Nombre.ToString() + @"\" + @"Imagen\" + modelo.Nombre.ToString() + modelo.Identificacion + ".jpg";
                                modelo.Archivo.SaveAs(pathArchivo);
                            }
                            else if (fileExt == ".png")
                            {
                                string pathArchivo = Path.Combine(rutaSitio + @"ArchivosSCAP\Infractores\" + modelo.Identificacion.ToString() + "-" + modelo.Nombre.ToString() + @"\" + @"Imagen\" + modelo.Nombre.ToString() + modelo.Identificacion + ".png");
                                infractor.imagen = @"~\ArchivosSCAP\Infractores\" + modelo.Identificacion.ToString() + "-" + modelo.Nombre.ToString() + @"\" + @"Imagen\" + modelo.Nombre.ToString() + modelo.Identificacion + ".png";
                                modelo.Archivo.SaveAs(pathArchivo);
                            }
                        }
                        else
                        {
                            infractor.imagen = null;
                        }

                        infractorDAL.Add(infractor);
                        auditoria_modelo.IdElemento = infractorDAL.GetInfractorIdentificacion(modelo.Identificacion).idInfractor;
                        auditoriaDAL.Add(ConvertirAuditoria(auditoria_modelo));
                        int aux = infractorDAL.GetInfractorIdentificacion(modelo.Identificacion).idInfractor;
                        return Redirect("~/Infractor/Detalle/" + aux);
                    }

                }
                modelo.Nacionalidades = tablaGeneralDAL.Get("Generales", "nacionalidad").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                modelo.TiposDeIdentificacion = tablaGeneralDAL.Get("Generales", "tipoDeIdentificacionInfractor").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                modelo.TiposDeSexo = tablaGeneralDAL.Get("Generales", "sexo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                return View(modelo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public ActionResult Detalle(int id)
        {
            Autorizar();
            infractorDAL = new InfractorDAL();
            Session["idInfractor"] = id;
            Session["auditoria"] = infractorDAL.GetInfractor(id).nombreCompleto;
            Session["tabla"] = "Infractor";
            InfractorViewModel modelo = CargarInfractor(infractorDAL.GetInfractor(id));
            modelo.Edad = ObtenerEdad(modelo.FechaNacimiento);
            return View(modelo);
        }

        public ActionResult Editar(int id)
        {
            AutorizarEditar();
            infractorDAL = new InfractorDAL();
            InfractorViewModel modelo = CargarInfractor(infractorDAL.GetInfractor(id));
            modelo.Nacionalidades = tablaGeneralDAL.Get("Generales", "nacionalidad").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            modelo.TiposDeIdentificacion = tablaGeneralDAL.Get("Generales", "tipoDeIdentificacionInfractor").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            modelo.TiposDeSexo = tablaGeneralDAL.Get("Generales", "sexo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            return View(modelo);
        }

        [HttpPost]
        public ActionResult Editar(InfractorViewModel modelo)
        {
            AutorizarEditar();
            infractorDAL = new InfractorDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();

            AuditoriaViewModel auditoria_modelo = new AuditoriaViewModel
            {
                Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "2").idTablaGeneral,
                IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "3").idTablaGeneral,
                IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario
            };
            try
            {
                if (ModelState.IsValid)
                {
                    int errores = 0;
                    if (infractorDAL.InfractorExiste(modelo.Identificacion) && modelo.Identificacion != infractorDAL.GetInfractor(modelo.IdInfractor).numeroDeIdentificacion)
                    {
                        ModelState.AddModelError(nameof(modelo.Identificacion), "La cédula ingresada ya existe");
                        errores++;
                    }
                    if (errores == 0)
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
                                string pathArchivo = Path.Combine(rutaSitio + @"ArchivosSCAP\Infractores\" + modelo.Identificacion.ToString() + "-" + modelo.Nombre.ToString() + @"\" + @"Imagen\" + modelo.Nombre.ToString() + modelo.Identificacion + ".jpg");
                                infractor.imagen = @"~\ArchivosSCAP\Infractores\" + modelo.Identificacion.ToString() + "-" + modelo.Nombre.ToString() + @"\" + @"Imagen\" + modelo.Nombre.ToString() + modelo.Identificacion + ".jpg";
                                modelo.Archivo.SaveAs(pathArchivo);
                            }
                            else if (fileExt == ".png")
                            {
                                string pathArchivo = Path.Combine(rutaSitio + @"ArchivosSCAP\Infractores\" + modelo.Identificacion.ToString() + "-" + modelo.Nombre.ToString() + @"\" + @"Imagen\" + modelo.Nombre.ToString() + modelo.Identificacion + ".png");
                                infractor.imagen = @"~\ArchivosSCAP\Infractores\" + modelo.Identificacion.ToString() + "-" + modelo.Nombre.ToString() + @"\" + @"Imagen\" + modelo.Nombre.ToString() + modelo.Identificacion + ".png";
                                modelo.Archivo.SaveAs(pathArchivo);
                            }
                        }
                        else
                        {
                            infractor.imagen = modelo.Imagen;
                        }

                        infractorDAL.Edit(infractor);
                        auditoria_modelo.IdElemento = infractorDAL.GetInfractorIdentificacion(modelo.Identificacion).idInfractor;
                        auditoriaDAL.Add(ConvertirAuditoria(auditoria_modelo));
                        return Redirect("~/Infractor/Detalle/" + modelo.IdInfractor);
                    }
                }
                modelo.Nacionalidades = tablaGeneralDAL.Get("Generales", "nacionalidad").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                modelo.TiposDeIdentificacion = tablaGeneralDAL.Get("Generales", "tipoDeIdentificacionInfractor").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                modelo.TiposDeSexo = tablaGeneralDAL.Get("Generales", "sexo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                return View(modelo);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}