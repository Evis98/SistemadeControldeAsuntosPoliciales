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
            return new Personas
            {
                idPersona = modelo.IdPersona,
                tipoIdentificacion = tablaGeneralDAL.GetCodigo("Generales", "tipoDeIdentificacion", modelo.TipoIdentificacionPersona.ToString()).idTablaGeneral,
                identificacion = modelo.Identificacion,
                nacionalidad = tablaGeneralDAL.GetCodigo("Generales", "nacionalidad", modelo.NacionalidadPersona.ToString()).idTablaGeneral,
                nombre = modelo.NombrePersona,
                fechaNacimiento = modelo.FechaNacimientoPersona,
                telefonoHabitacion = modelo.TelefonoCasaPersona,
                telefonoCelular = modelo.TelefonoCelularPersona,
                telefonoTrabajo = modelo.TelefonoTrabajoPersona,
                direccionPersona = modelo.DireccionExacta,
                sexo = tablaGeneralDAL.GetCodigo("Generales", "sexo", modelo.SexoPersona.ToString()).idTablaGeneral,
                correoElectronicoPersona = modelo.CorreoElectronicoPersona,                
                profesion = modelo.ProfesionPersona,
                lugarTrabajoPersona = modelo.LugarTrabajoPersona     
            };
        }

        public PersonaViewModel CargarPersona(Personas persona)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            return new PersonaViewModel
            {
                IdPersona = persona.idPersona,
                TipoIdentificacionPersona = int.Parse(tablaGeneralDAL.Get(persona.tipoIdentificacion).codigo),
                VistaTipoIdentificacionPersona = tablaGeneralDAL.Get(persona.tipoIdentificacion).descripcion,
                Identificacion = persona.identificacion,
                NacionalidadPersona = tablaGeneralDAL.Get(persona.nacionalidad).codigo,
                VistaNacionalidadPersona = tablaGeneralDAL.Get(persona.nacionalidad).descripcion,
                NombrePersona = persona.nombre,
                FechaNacimientoPersona = (DateTime)persona.fechaNacimiento,
                TelefonoCasaPersona = persona.telefonoHabitacion,
                TelefonoTrabajoPersona = persona.telefonoTrabajo,
                TelefonoCelularPersona = persona.telefonoCelular,
                DireccionExacta = persona.direccionPersona,
                SexoPersona = int.Parse(tablaGeneralDAL.Get(persona.sexo).codigo),
                VistaSexoPersona = tablaGeneralDAL.Get(persona.sexo).descripcion,
                CorreoElectronicoPersona = persona.correoElectronicoPersona,                
                ProfesionPersona = persona.profesion,
                LugarTrabajoPersona = persona.lugarTrabajoPersona                
            };
        }




        public ActionResult Index(string filtrosSeleccionado, string busqueda)
        {
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
            model.IdUsuario = usuarioDAL.GetUsuario(1).idUsuario;
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
        {  personaDAL = new PersonaDAL();
            Session["idPersona"] = id;           
            Session["nombrePersona"] = personaDAL.GetPersona(id).nombre;
          
            PersonaViewModel modelo = CargarPersona(personaDAL.GetPersona(id));
            return View(modelo);
        }

        public ActionResult Editar(int id)
        {
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
            personaDAL = new PersonaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            usuarioDAL = new UsuarioDAL();
            auditoriaDAL = new AuditoriaDAL();
            modelo.TiposDeIdentificacion = tablaGeneralDAL.Get("Generales", "tipoDeIdentificacion").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            modelo.Nacionalidades = tablaGeneralDAL.Get("Generales", "nacionalidad").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            modelo.TiposDeSexo = tablaGeneralDAL.Get("Generales", "sexo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            modelo.Accion = tablaGeneralDAL.GetCodigo("Auditoria", "accion", "2").idTablaGeneral;
            modelo.IdCategoria = tablaGeneralDAL.GetCodigo("Auditoria", "tabla", "2").idTablaGeneral;
            modelo.IdUsuario = usuarioDAL.GetUsuario(1).idUsuario;
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
