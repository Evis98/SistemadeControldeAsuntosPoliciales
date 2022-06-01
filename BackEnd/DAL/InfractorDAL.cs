using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public class InfractorDAL : IInfractorDAL
    {
        //Recibe todos los infractores ingresados en la base de datos con su información
        public List<Infractores> Get()
        {
            List<Infractores> lista = new List<Infractores>();
            using (SCAPEntities db = new SCAPEntities())
            {
                lista = db.Infractores.ToList();
            }
            return lista;
        }

        //Permite agregar un infractor nuevo a la base de datos
        public void Add(Infractores infractor)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.Infractores.Add(infractor);
                db.SaveChanges();
            }
        }

        //Permite actualizar la información de un infractor en la base de datos
        public void Edit(Infractores infractor)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.Entry(infractor).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        //Permite recibir un infractor con toda su información a través de su atributo "idInfractor"
        public Infractores GetInfractor(int idInfractor)
        {
            try
            {
                Infractores resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.Infractores.Find(idInfractor);
                }
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Permite recibir el numero de indentificacion del infractor a través de su atributo "idInfractor"
        public Infractores GetInfractorIdentificacion(string numeroDeIdentificacion)
        {
            try
            {
                Infractores resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.Infractores.Where(x => x.numeroDeIdentificacion == numeroDeIdentificacion).FirstOrDefault();
                }
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InfractorExiste(string numeroDeIdentificacion)
        {
            try
            {
                int contador;
                using (SCAPEntities db = new SCAPEntities())
                {
                    contador = db.Infractores.Where(x => x.numeroDeIdentificacion == numeroDeIdentificacion).Count();
                }
                return contador > 0 ? true : false;
            }
            catch (Exception)
            {
                throw;
            }

        }


    }
}