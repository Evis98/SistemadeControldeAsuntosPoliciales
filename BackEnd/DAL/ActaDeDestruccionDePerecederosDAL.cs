using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public class ActaDeDestruccionDePerecederosDAL : IActaDeDestruccionDePerecederosDAL
    {
        public List<ActasDeDestruccionDePerecederos> Get()
        {
            List<ActasDeDestruccionDePerecederos> lista = new List<ActasDeDestruccionDePerecederos>();
            using (SCAPEntities db = new SCAPEntities())
            {
                lista = db.ActasDeDestruccionDePerecederos.ToList();
            }
            return lista;
        }

        public void Add(ActasDeDestruccionDePerecederos nuevo)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.ActasDeDestruccionDePerecederos.Add(nuevo);
                db.SaveChanges();
            }
        }

        public void Edit(ActasDeDestruccionDePerecederos cambiada)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.Entry(cambiada).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

        }

        public ActasDeDestruccionDePerecederos GetActaDeDestruccionDePerecederos(int idActaDeDestruccionDePerecederos)
        {
            try
            {
                ActasDeDestruccionDePerecederos resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.ActasDeDestruccionDePerecederos.Find(idActaDeDestruccionDePerecederos);
                }
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActasDeDestruccionDePerecederos GetActaDeDestruccionDePerecederosFolio(string numeroFolio)
        {
            try
            {
                ActasDeDestruccionDePerecederos resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.ActasDeDestruccionDePerecederos.Where(x => x.numeroFolio == numeroFolio).FirstOrDefault();
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
                return db.ActasDeDestruccionDePerecederos.Where(x => x.fechaHora.Year == DateTime.Now.Year).Count();
            }

        }

        public bool FolioExiste(string numeroFolio)
        {
            try
            {
                int contador;
                using (SCAPEntities db = new SCAPEntities())
                {
                    contador = db.ActasEntrega.Where(x => x.numeroFolio == numeroFolio).Count();
                }
                return contador > 0 ? true : false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetFolioActaDeDestruccionDePerecederos(string serie)
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

        public List<ActasDeDestruccionDePerecederos> GetActaDeDestruccionDePerecederosRango(DateTime inicio, DateTime final)
        {
            try
            {
                List<ActasDeDestruccionDePerecederos> resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.ActasDeDestruccionDePerecederos.Where(x => x.fechaHora >= inicio && x.fechaHora <= final).ToList();
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
