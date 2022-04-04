using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public class ParteDAL : IParteDAL
    {
        //Recibe todas los partes ingresados en la base de datos con su información
        public List<PartesPoliciales> Get()
        {
            List<PartesPoliciales> lista = new List<PartesPoliciales>();
            using (SCAPEntities db = new SCAPEntities())
            {
                lista = db.PartesPoliciales.ToList();
            }
            return lista;
        }

        //Permite agregar una persona nueva a la base de datos
        public void Add(PartesPoliciales parte)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.PartesPoliciales.Add(parte);
                db.SaveChanges();
            }
        }

        //Permite actualizar la información de un parte en la base de datos
        public void Edit(PartesPoliciales parte)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.Entry(parte).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        //Permite recibir un parte con toda su información a través de su atributo "idParte"
        public PartesPoliciales GetParte(int idParte)
        {
            try
            {
                PartesPoliciales resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.PartesPoliciales.Find(idParte);
                }
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public PartesPoliciales GetPartePolicial(string numeroFolio)
        {
            try
            {
                PartesPoliciales resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.PartesPoliciales.Where(x => x.numeroFolio == numeroFolio).FirstOrDefault();
                }
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool NumeroFolioExiste(string numeroFolio)
        {
            try
            {
                int contador;
                using (SCAPEntities db = new SCAPEntities())
                {
                    contador = db.PartesPoliciales.Where(x => x.numeroFolio == numeroFolio).Count();
                }
                return contador > 0 ? true : false;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public string GetNumeroFolio(string numeroFolio)
        {
            if (NumeroFolioExiste(numeroFolio))
            {
                return numeroFolio;
            }
            else
            {
                return null;
            }
        }

        public int GetCount()
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                
                return db.PartesPoliciales.Count();
            }

        }

        public List<PartesPoliciales> GetPartesRango(DateTime inicio, DateTime final)
        {
            try
            {
                List<PartesPoliciales> resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.PartesPoliciales.Where(x => x.fecha >= inicio && x.fecha <= final).ToList();
                }
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}