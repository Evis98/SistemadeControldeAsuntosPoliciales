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
        public Personas ConvertirPersona(PersonaViewModel modelo)
        {
            tablaGeneralDAL = new TablaGeneralDAL();

            Personas persona = new Personas();
            {
                persona.idPersona = modelo.IdPersona;
                persona.tipoIdentificacion = tablaGeneralDAL.GetCodigo("Generales", "tipoDeIdentificacion", modelo.TipoIdentificacionPersona.ToString()).idTablaGeneral;
                persona.identificacion = modelo.Identificacion;
                if(modelo.NacionalidadPersona != null ) {
                    persona.nacionalidad = tablaGeneralDAL.GetCodigo("Generales", "nacionalidad", modelo.NacionalidadPersona.ToString()).idTablaGeneral;
                }
                persona.nombre = modelo.NombrePersona;
                if(persona.tipoIdentificacion == 77)
                {
                    persona.fechaNacimiento = null;
                }
                else
                {
                    persona.fechaNacimiento = modelo.FechaNacimientoPersona;
                }
                
                persona.telefonoHabitacion = modelo.TelefonoCasaPersona;
                persona.telefonoCelular = modelo.TelefonoCelularPersona;
                persona.telefonoTrabajo = modelo.TelefonoTrabajoPersona;
                persona.direccionPersona = modelo.DireccionExacta;
                if(modelo.SexoPersona != null)
                {
                    persona.sexo = tablaGeneralDAL.GetCodigo("Generales", "sexo", modelo.SexoPersona.ToString()).idTablaGeneral;
                }
                persona.correoElectronicoPersona = modelo.CorreoElectronicoPersona;
                persona.profesion = modelo.ProfesionPersona;
                persona.lugarTrabajoPersona = modelo.LugarTrabajoPersona;
                persona.instalaciones = modelo.Instalaciones;
            };
            return persona;
        }

        public PersonaViewModel CargarPersona(Personas persona)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            PersonaViewModel personaViewModel = new PersonaViewModel();
            {
                personaViewModel.IdPersona = persona.idPersona;
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
          
                personaViewModel.TipoIdentificacionPersona = int.Parse(tablaGeneralDAL.Get(persona.tipoIdentificacion).codigo);
                personaViewModel.VistaTipoIdentificacionPersona = tablaGeneralDAL.Get(persona.tipoIdentificacion).descripcion;
                personaViewModel.Identificacion = persona.identificacion;
              
                personaViewModel.NombrePersona = persona.nombre;
                if (persona.fechaNacimiento != null)
                {
                    personaViewModel.FechaNacimientoPersona = (DateTime)persona.fechaNacimiento;
                }
         
                personaViewModel.TelefonoCasaPersona = persona.telefonoHabitacion;
                personaViewModel.TelefonoTrabajoPersona = persona.telefonoTrabajo;
                personaViewModel.TelefonoCelularPersona = persona.telefonoCelular;
                personaViewModel.DireccionExacta = persona.direccionPersona;               
                personaViewModel.CorreoElectronicoPersona = persona.correoElectronicoPersona;
                personaViewModel.ProfesionPersona = persona.profesion;
                personaViewModel.LugarTrabajoPersona = persona.lugarTrabajoPersona;
                personaViewModel.Instalaciones = persona.instalaciones;
                
            };
            return personaViewModel;
        }


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

        public ActionResult Index(string filtrosSeleccionado, string busqueda)
        {
            Autorizar();
            personaDAL = new PersonaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            List<PersonaViewModel> personas = new List<PersonaViewModel>();
            List<PersonaViewModel> personasFiltrados = new List<PersonaViewModel>();
            List<TablaGeneral> comboindex = tablaGeneralDAL.Get("Personas", "index");
            List<SelectListItem> items = comboindex.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.descripcion
                };
            });ViewBag.items = items;   
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
        public ActionResult Nuevo(PersonaViewModel model)
        {
            Autorizar();
            personaDAL = new PersonaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            model.TiposDeIdentificacion = tablaGeneralDAL.Get("Generales", "tipoDeIdentificacion").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.Nacionalidades = tablaGeneralDAL.Get("Generales", "nacionalidad").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.TiposDeSexo = tablaGeneralDAL.Get("Generales", "sexo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.IdentificacionPersonaFiltrada = personaDAL.GetCedulaPersona(model.Identificacion);
            model.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "1").idTablaGeneral;
            model.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "2").idTablaGeneral;
            model.IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario;
            try
            {
                if (!personaDAL.IdentificacionExiste(model.Identificacion))
                {
                    if (ModelState.IsValid)
                    {
                        personaDAL.Add(ConvertirPersona(model));
                        model.IdElemento = personaDAL.GetPersonaIdentificacion(model.Identificacion).idPersona;
                        auditoriaDAL.Add(ConvertirAuditoria(model));
                        int aux = personaDAL.GetPersonaIdentificacion(model.Identificacion).idPersona;
                        return Redirect("~/Persona/Detalle/" + aux);
                    }
                }
                return View(model);
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
            Session["nombrePersona"] = personaDAL.GetPersona(id).nombre;
          
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
            modelo.TiposDeIdentificacion = tablaGeneralDAL.Get("Generales", "tipoDeIdentificacion").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            modelo.Nacionalidades = tablaGeneralDAL.Get("Generales", "nacionalidad").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            modelo.TiposDeSexo = tablaGeneralDAL.Get("Generales", "sexo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            modelo.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "2").idTablaGeneral;
            modelo.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "2").idTablaGeneral;
            modelo.IdUsuario = usuarioDAL.GetUsuario((int?)Session["userID"]).idUsuario;
            try
            {
                if (ModelState.IsValid)
                {
                    personaDAL.Edit(ConvertirPersona(modelo));
                    modelo.IdElemento = personaDAL.GetPersonaIdentificacion(modelo.Identificacion).idPersona;
                    auditoriaDAL.Add(ConvertirAuditoria(modelo));
                    return Redirect("~/Persona/Detalle/" + modelo.IdPersona);

                }
                return View(modelo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Auditorias ConvertirAuditoria(PersonaViewModel modelo)
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
    }
}
