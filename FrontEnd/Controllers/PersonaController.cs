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
    public class PersonaController : Controller
    {
        ITablaGeneralDAL tablaGeneralDAL;
        IPersonaDAL personaDAL;
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
        public Personas ConvertirPersona(PersonaViewModel modelo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();

            Personas persona = new Personas()
            {
                idPersona = modelo.IdPersona,
                tipoIdentificacion = tablaGeneralDAL.GetCodigo("Generales", "tipoDeIdentificacion", modelo.TipoIdentificacionPersona.ToString()).idTablaGeneral,
                identificacion = modelo.Identificacion,
                telefonoHabitacion = modelo.TelefonoCasaPersona,
                telefonoCelular = modelo.TelefonoCelularPersona,
                telefonoTrabajo = modelo.TelefonoTrabajoPersona,
                correoElectronicoPersona = modelo.CorreoElectronicoPersona,
                nombre = modelo.NombrePersona.ToUpper()
            };
            if (modelo.DireccionExacta != null)
            {
                persona.direccionPersona = modelo.DireccionExacta.ToUpper();
            }
            if (modelo.LugarTrabajoPersona != null)
            {
                persona.lugarTrabajoPersona = modelo.LugarTrabajoPersona.ToUpper();
            }
            if (modelo.Instalaciones != null)
            {
                persona.instalaciones = modelo.Instalaciones.ToUpper();
            }
            if (modelo.ProfesionPersona != null)
            {
                persona.profesion = modelo.ProfesionPersona.ToUpper();
            }
            if (modelo.NacionalidadPersona != null)
            {
                persona.nacionalidad = tablaGeneralDAL.GetCodigo("Generales", "nacionalidad", modelo.NacionalidadPersona.ToString()).idTablaGeneral;
            }
            if (persona.tipoIdentificacion == 77)//revisar esto
            {
                persona.fechaNacimiento = null;
            }
            else
            {
                persona.fechaNacimiento = modelo.FechaNacimientoPersona;
            }
            if (modelo.SexoPersona != null)
            {
                persona.sexo = tablaGeneralDAL.GetCodigo("Generales", "sexo", modelo.SexoPersona.ToString()).idTablaGeneral;
            }
            return persona;
        }

        public PersonaViewModel CargarPersona(Personas persona)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            PersonaViewModel personaViewModel = new PersonaViewModel()
            {
                IdPersona = persona.idPersona,
                TipoIdentificacionPersona = int.Parse(tablaGeneralDAL.Get(persona.tipoIdentificacion).codigo),
                VistaTipoIdentificacionPersona = tablaGeneralDAL.Get(persona.tipoIdentificacion).descripcion,
                Identificacion = persona.identificacion,
                NombrePersona = persona.nombre,
                TelefonoCasaPersona = persona.telefonoHabitacion,
                TelefonoTrabajoPersona = persona.telefonoTrabajo,
                TelefonoCelularPersona = persona.telefonoCelular,
                DireccionExacta = persona.direccionPersona,
                CorreoElectronicoPersona = persona.correoElectronicoPersona,
                ProfesionPersona = persona.profesion,
                LugarTrabajoPersona = persona.lugarTrabajoPersona,
                Instalaciones = persona.instalaciones

            };
            if (persona.nacionalidad != null)
            {
                personaViewModel.NacionalidadPersona = tablaGeneralDAL.Get((int)persona.nacionalidad).codigo;
                personaViewModel.VistaNacionalidadPersona = tablaGeneralDAL.Get((int)persona.nacionalidad).descripcion;
            }
            else
            {

                personaViewModel.VistaNacionalidadPersona = null;
            }
            if (persona.sexo != null)
            {
                personaViewModel.SexoPersona = tablaGeneralDAL.Get((int)persona.sexo).codigo;
                personaViewModel.VistaSexoPersona = tablaGeneralDAL.Get((int)persona.sexo).descripcion;
            }
            else
            {
                personaViewModel.VistaSexoPersona = null;
            }
            if (persona.fechaNacimiento != null)
            {
                personaViewModel.FechaNacimientoPersona = (DateTime)persona.fechaNacimiento;
            }

            return personaViewModel;
        }

        public Auditorias ConvertirAuditoria(AuditoriaViewModel modelo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            personaDAL = new PersonaDAL();
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
        //Metodos de las Vistas
        public ActionResult Index(string filtrosSeleccionado, string busqueda)
        {
            Autorizar();
            personaDAL = new PersonaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();

            //Carga combobox busqueda
            List<TablaGeneral> comboindex = tablaGeneralDAL.Get("Personas", "index");
            List<SelectListItem> items = comboindex.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.descripcion
                };
            }); ViewBag.items = items;

            //Carga lista de policias
            List<PersonaViewModel> personas = new List<PersonaViewModel>();
            List<PersonaViewModel> personasFiltrados = new List<PersonaViewModel>();
            foreach (Personas persona in personaDAL.Get())
            {
                personas.Add(CargarPersona(persona));
            }
            if (busqueda != null)
            {
                foreach (PersonaViewModel persona in personas)
                {
                    if (filtrosSeleccionado == "Cédula")
                    {
                        if (persona.Identificacion.Contains(busqueda))
                        {

                            personasFiltrados.Add(persona);
                        }
                    }
                    if (filtrosSeleccionado == "Nombre")
                    {
                        if (persona.NombrePersona.Contains(busqueda))
                        {
                            personasFiltrados.Add(persona);
                        }
                    }
                }
                personas = personasFiltrados;
            }
            return View(personas.OrderBy(x => x.NombrePersona).ToList());
        }


        public ActionResult Nuevo()
        {
            Autorizar();
            tablaGeneralDAL = new TablaGeneralDAL();
            PersonaViewModel modelo = new PersonaViewModel()
            {
                TiposDeIdentificacion = tablaGeneralDAL.Get("Generales", "tipoDeIdentificacion").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                Nacionalidades = tablaGeneralDAL.Get("Generales", "nacionalidad").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                TiposDeSexo = tablaGeneralDAL.Get("Generales", "sexo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo }),
                FechaNacimientoPersona = DateTime.Today
            };
            return View(modelo);
        }

        [HttpPost]
        public ActionResult Nuevo(PersonaViewModel modelo)
        {
            Autorizar();
            personaDAL = new PersonaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();

            AuditoriaViewModel auditoria_modelo = new AuditoriaViewModel
            {
                Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "1").idTablaGeneral,
                IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "2").idTablaGeneral,
                IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario
            };

            try
            {
                if (ModelState.IsValid)
                {
                    int errores = 0;
                    if (personaDAL.IdentificacionExiste(modelo.Identificacion))
                    {
                        ModelState.AddModelError(nameof(modelo.Identificacion), "La cédula ingresada ya existe");
                        errores++;
                    }
                    if (errores == 0)
                    {
                        personaDAL.Add(ConvertirPersona(modelo));
                        auditoria_modelo.IdElemento = personaDAL.GetPersonaIdentificacion(modelo.Identificacion).idPersona;
                        auditoriaDAL.Add(ConvertirAuditoria(auditoria_modelo));
                        int aux = personaDAL.GetPersonaIdentificacion(modelo.Identificacion).idPersona;
                        return Redirect("~/Persona/Detalle/" + aux);
                    }
                }
                modelo.TiposDeIdentificacion = tablaGeneralDAL.Get("Generales", "tipoDeIdentificacion").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                modelo.Nacionalidades = tablaGeneralDAL.Get("Generales", "nacionalidad").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
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
            personaDAL = new PersonaDAL();
            Session["idPersona"] = id;
            Session["auditoria"] = personaDAL.GetPersona(id).nombre;
            Session["tabla"] = "Persona";
            PersonaViewModel modelo = CargarPersona(personaDAL.GetPersona(id));
            return View(modelo);
        }

        public ActionResult Editar(int id)
        {
            AutorizarEditar();
            personaDAL = new PersonaDAL();
            PersonaViewModel modelo = CargarPersona(personaDAL.GetPersona(id));
            modelo.TiposDeIdentificacion = tablaGeneralDAL.Get("Generales", "tipoDeIdentificacion").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            modelo.Nacionalidades = tablaGeneralDAL.Get("Generales", "nacionalidad").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            modelo.TiposDeSexo = tablaGeneralDAL.Get("Generales", "sexo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            return View(modelo);
        }


        [HttpPost]
        public ActionResult Editar(PersonaViewModel modelo)
        {
            AutorizarEditar();
            personaDAL = new PersonaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            AuditoriaViewModel auditoria_modelo = new AuditoriaViewModel
            {
                Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "2").idTablaGeneral,
                IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "2").idTablaGeneral,
                IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario
            };
            try
            {
                if (ModelState.IsValid)
                {
                    int errores = 0;
                    if (personaDAL.IdentificacionExiste(modelo.Identificacion) && modelo.Identificacion != personaDAL.GetPersona(modelo.IdPersona).identificacion)
                    {
                        ModelState.AddModelError(nameof(modelo.Identificacion), "La cédula ingresada ya existe");
                        errores++;
                    }
                    if (errores == 0)
                    {
                        personaDAL.Edit(ConvertirPersona(modelo));
                        auditoria_modelo.IdElemento = personaDAL.GetPersonaIdentificacion(modelo.Identificacion).idPersona;
                        auditoriaDAL.Add(ConvertirAuditoria(auditoria_modelo));
                        return Redirect("~/Persona/Detalle/" + modelo.IdPersona);
                    }
                }
                modelo.TiposDeIdentificacion = tablaGeneralDAL.Get("Generales", "tipoDeIdentificacion").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
                modelo.Nacionalidades = tablaGeneralDAL.Get("Generales", "nacionalidad").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
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
