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
       
       
        public List<ListPersonaViewModel> ConvertirListaPersonas(List<Personas> personas)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            return (from d in personas
                    select new ListPersonaViewModel
                    {
                        IdPersona = d.idPersona,
                        IdentificacionPersona = d.identificacion,
                        NacionalidadPersona = tablaGeneralDAL.Get(d.nacionalidad).descripcion,
                        NombrePersona = d.nombre,
                        TelefonoCelularPersona = d.telefonoCelular,
                        TelefonoHabitacionPersona = d.telefonoCelular,
                        TelefonoTrabajoPersona = d.telefonoCelular,
                        DireccionExactaPersona = d.direccionPersona,
                        CorreoElectronicoPersona = d.correoElectronicoPersona,
                        ProfesionUOficioPersona = d.profesion,
                        
                    }).ToList();
        }

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
                Identificacion = persona.identificacion,
                NacionalidadPersona = tablaGeneralDAL.Get(persona.nacionalidad).codigo,
                NombrePersona = persona.nombre,
                FechaNacimientoPersona = (DateTime)persona.fechaNacimiento,
                TelefonoCasaPersona = persona.telefonoHabitacion,
                TelefonoTrabajoPersona = persona.telefonoTrabajo,
                TelefonoCelularPersona = persona.telefonoCelular,
                DireccionExacta = persona.direccionPersona,
                SexoPersona = int.Parse(tablaGeneralDAL.Get(persona.sexo).codigo),
                CorreoElectronicoPersona = persona.correoElectronicoPersona,                
                ProfesionPersona = persona.profesion,
                LugarTrabajoPersona = persona.lugarTrabajoPersona                
            };
        }

        public ListPersonaViewModel ConvertirPersonaInverso(Personas persona)
        {
            tablaGeneralDAL = new TablaGeneralDAL();
            return new ListPersonaViewModel
            {
                IdPersona = persona.idPersona,
                TipoIdentificacionPersona = tablaGeneralDAL.Get(persona.tipoIdentificacion).descripcion,
                IdentificacionPersona = persona.identificacion,
                NacionalidadPersona = tablaGeneralDAL.Get(persona.nacionalidad).descripcion,
                NombrePersona = persona.nombre,
                FechaNacimientoPersona = persona.fechaNacimiento.ToShortDateString(),
                TelefonoHabitacionPersona = persona.telefonoHabitacion,
                TelefonoTrabajoPersona = persona.telefonoTrabajo,
                TelefonoCelularPersona = persona.telefonoCelular,
                DireccionExactaPersona = persona.direccionPersona,
                Sexo = tablaGeneralDAL.Get(persona.sexo).descripcion,
                CorreoElectronicoPersona = persona.correoElectronicoPersona,
                ProfesionUOficioPersona = persona.profesion,
                LugarTrabajoPersona = persona.lugarTrabajoPersona               
            };
        }


        public ActionResult Index(string filtrosSeleccionado, string busqueda)
        {
            personaDAL = new PersonaDAL();
            tablaGeneralDAL = new TablaGeneralDAL();
            List<Personas> personas = personaDAL.Get();
            List<Personas> personasFiltrados = new List<Personas>();
            List<TablaGeneral> comboindex = tablaGeneralDAL.Get("Personas", "index");
            List<SelectListItem> items = comboindex.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.descripcion
                };
            });
            ViewBag.items = items;            
            if (busqueda != null)
            {
                foreach (Personas persona in personas)
                {
                    if (filtrosSeleccionado == "Cédula")
                    {
                        if (persona.identificacion.Contains(busqueda))
                        {

                            personasFiltrados.Add(persona);
                        }
                    }
                    if (filtrosSeleccionado == "Nombre")
                    {
                        if (persona.nombre.Contains(busqueda))
                        {
                            personasFiltrados.Add(persona);
                        }
                    }
                }
                personas = personasFiltrados;
            }
            personas = personas.OrderBy(x => x.nombre).ToList();
            return View(ConvertirListaPersonas(personas));
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
            model.TiposDeIdentificacion = tablaGeneralDAL.Get("Generales", "tipoDeIdentificacion").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.Nacionalidades = tablaGeneralDAL.Get("Generales", "nacionalidad").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.TiposDeSexo = tablaGeneralDAL.Get("Generales", "sexo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            model.IdentificacionPersonaFiltrada = personaDAL.GetCedulaPersona(model.Identificacion);
            try
            {
                if (!personaDAL.IdentificacionExiste(model.Identificacion))
                {
                    if (ModelState.IsValid)
                    {
                        personaDAL.Add(ConvertirPersona(model));
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
            Session["idPersona"] = id;
            personaDAL = new PersonaDAL();
            ListPersonaViewModel modelo = ConvertirPersonaInverso(personaDAL.GetPersona(id));
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
            modelo.TiposDeIdentificacion = tablaGeneralDAL.Get("Generales", "tipoDeIdentificacion").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            modelo.Nacionalidades = tablaGeneralDAL.Get("Generales", "nacionalidad").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            modelo.TiposDeSexo = tablaGeneralDAL.Get("Generales", "sexo").Select(i => new SelectListItem() { Text = i.descripcion, Value = i.codigo });
            try
            {
                if (ModelState.IsValid)
                {
                    personaDAL.Edit(ConvertirPersona(modelo));

                    return Redirect("~/Persona/Detalle/" + modelo.IdPersona);

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
