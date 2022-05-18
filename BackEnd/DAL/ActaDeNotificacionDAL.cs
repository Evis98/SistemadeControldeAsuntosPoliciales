using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{

    public class ActaDeNotificacionDAL : IActaDeNotificacionDAL
    {
        public List<ActasDeNotificacion> Get()
        {
            List<ActasDeNotificacion> lista = new List<ActasDeNotificacion>();
            using (SCAPEntities db = new SCAPEntities())
            {
                lista = db.ActasDeNotificacion.ToList();
            }
            return lista;
        }

        public void Add(ActasDeNotificacion nuevo)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.ActasDeNotificacion.Add(nuevo);
                db.SaveChanges();
            }
        }

        public void Edit(ActasDeNotificacion cambiada)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.Entry(cambiada).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

        }

        public ActasDeNotificacion GetActaDeNotificacion(int idActaDeNotificacion)
        {
            try
            {
                ActasDeNotificacion resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.ActasDeNotificacion.Find(idActaDeNotificacion);
                }
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActasDeNotificacion GetActaDeNotificacionFolio(string numeroFolio)
        {
            try
            {
                ActasDeNotificacion resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.ActasDeNotificacion.Where(x => x.numeroFolio == numeroFolio).FirstOrDefault();
                }
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int GetCount(DateTime year)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                return db.ActasDeNotificacion.Where(x => x.fechaHora.Year == year.Year).Count();
            }

        }

        public bool FolioExiste(string numeroFolio)
        {
            try
            {
                int contador;
                using (SCAPEntities db = new SCAPEntities())
                {
                    contador = db.ActasDeNotificacion.Where(x => x.numeroFolio == numeroFolio).Count();
                }
                return contador > 0 ? true : false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetFolioActaDeNotificacion(string serie)
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

        public List<ActasDeNotificacion> GetActaDeNotificacionRango(DateTime inicio, DateTime final)
        {
            try
            {
                List<ActasDeNotificacion> resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.ActasDeNotificacion.Where(x => x.fechaHora >= inicio && x.fechaHora <= final).ToList();
                }
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool NotificadoExiste(int idNotificado)
        {
            try
            {
                int contador;
                using (SCAPEntities db = new SCAPEntities())
                {
                    contador = db.ActasDeNotificacion.Where(x => x.personaNotificada == idNotificado).Count();
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
