using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public class ActaDeObservacionPolicialDAL : IActaDeObservacionPolicialDAL
    {
        //Recibe todos las armas ingresados en la base de datos con su información
        public List<ActasDeObservacionPolicial> Get()
        {
            List<ActasDeObservacionPolicial> lista = new List<ActasDeObservacionPolicial>();
            using (SCAPEntities db = new SCAPEntities())
            {
                lista = db.ActasDeObservacionPolicial.ToList();
            }
            return lista;
        }

        //Permite agregar un arma nueva a la base de datos
        public void Add(ActasDeObservacionPolicial nuevo)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.ActasDeObservacionPolicial.Add(nuevo);
                db.SaveChanges();
            }
        }

        //Permite actualizar la información de un arma en la base de datos
        public void Edit(ActasDeObservacionPolicial cambiada)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.Entry(cambiada).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

        }
        //Permite recibir un arma con toda su información a través de su atributo "idArma"
        public ActasDeObservacionPolicial GetActaDeObservacionPolicial(int idActaDeObservacionPolicial)
        {
            try
            {
                ActasDeObservacionPolicial resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.ActasDeObservacionPolicial.Find(idActaDeObservacionPolicial);
                }
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActasDeObservacionPolicial GetActaDeObservacionPolicialFolio(string numeroFolio)
        {
            try
            {
                ActasDeObservacionPolicial resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.ActasDeObservacionPolicial.Where(x => x.numeroFolio == numeroFolio).FirstOrDefault();
                }
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int GetCount()
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                return db.ActasDeObservacionPolicial.Where(x => x.fechaHora.Year == DateTime.Now.Year).Count();
            }

        }
        public bool FolioExiste(string numeroFolio)
        {
            try
            {
                int contador;
                using (SCAPEntities db = new SCAPEntities())
                {
                    contador = db.ActasDeObservacionPolicial.Where(x => x.numeroFolio == numeroFolio).Count();
                }
                return contador > 0 ? true : false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string GetFolioActaDeObservacionPolicial(string serie)
        {
            if (FolioExiste(serie))
            {
                return serie;
            }
            else
            {
                return null;
            }
        }
        public List<ActasDeObservacionPolicial> GetActaDeObservacionPolicialRango(DateTime inicio, DateTime final)
        {
            try
            {
                List<ActasDeObservacionPolicial> resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.ActasDeObservacionPolicial.Where(x => x.fechaHora >= inicio && x.fechaHora <= final).ToList();
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