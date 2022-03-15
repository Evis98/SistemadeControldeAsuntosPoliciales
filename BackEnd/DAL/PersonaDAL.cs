using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public class PersonaDAL : IPersonaDAL
    {
        //Recibe todas las personas ingresadas en la base de datos con su información
        public List<Personas> Get()
        {
            List<Personas> lista = new List<Personas>();
            using (SCAPEntities db = new SCAPEntities())
            {
                lista = db.Personas.ToList();
            }
            return lista;
        }

        //Permite agregar una persona nueva a la base de datos
        public void Add(Personas persona)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.Personas.Add(persona);
                db.SaveChanges();
            }
        }

        //Permite actualizar la información de una persona en la base de datos
        public void Edit(Personas persona)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.Entry(persona).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        //Permite recibir una persona con toda su información a través de su atributo "idPersona"
        public Personas GetPersona(int idPersona)
        {
            try
            {
                Personas resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.Personas.Find(idPersona);
                }
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Permite recibir el numero de indentificacion de la persona a través de su atributo "idPersona"
        public Personas GetPersonaIdentificacion(string identificacionPersona)
        {
            try
            {
                Personas resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.Personas.Where(x => x.identificacion == identificacionPersona).FirstOrDefault();
                }
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool IdentificacionExiste(string identificacionPersona)
        {
            try
            {
                int contador;
                using (SCAPEntities db = new SCAPEntities())
                {
                    contador = db.Personas.Where(x => x.identificacion == identificacionPersona).Count();
                }
                return contador > 0 ? true : false;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public string GetCedulaPersona(string identificacion)
        {
            if (IdentificacionExiste(identificacion))
            {
                return identificacion;
            }
            else
            {
                return null;
            }
        }
    }
}
