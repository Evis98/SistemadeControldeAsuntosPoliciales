using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public class ActaEntregaPorOrdenDeDAL : IActaEntregaPorOrdenDeDAL
    {
        //Recibe todos las armas ingresados en la base de datos con su información
        public List<ActasEntregaPorOrdenDe> Get()
        {
            List<ActasEntregaPorOrdenDe> lista = new List<ActasEntregaPorOrdenDe>();
            using (SCAPEntities db = new SCAPEntities())
            {
                lista = db.ActasEntregaPorOrdenDe.ToList();
            }
            return lista;
        }

        //Permite agregar un arma nueva a la base de datos
        public void Add(ActasEntregaPorOrdenDe nuevo)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.ActasEntregaPorOrdenDe.Add(nuevo);
                db.SaveChanges();
            }
        }

        //Permite actualizar la información de un arma en la base de datos
        public void Edit(ActasEntregaPorOrdenDe cambiada)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.Entry(cambiada).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

        }
        //Permite recibir un arma con toda su información a través de su atributo "idArma"
        public ActasEntregaPorOrdenDe GetActaEntregaPorOrdenDe(int idActaEntregaPorOrdenDe)
        {
            try
            {
                ActasEntregaPorOrdenDe resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.ActasEntregaPorOrdenDe.Find(idActaEntregaPorOrdenDe);
                }
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActasEntregaPorOrdenDe GetActaEntregaPorOrdenDeFolio(string numeroFolio)
        {
            try
            {
                ActasEntregaPorOrdenDe resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.ActasEntregaPorOrdenDe.Where(x => x.numeroFolio == numeroFolio).FirstOrDefault();
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
                return db.ActasEntregaPorOrdenDe.Where(x => x.fechaHoraEntrega.Value.Year == DateTime.Now.Year).Count();
            }

        }
        public bool FolioExiste(string numeroFolio)
        {
            try
            {
                int contador;
                using (SCAPEntities db = new SCAPEntities())
                {
                    contador = db.ActasEntregaPorOrdenDe.Where(x => x.numeroFolio == numeroFolio).Count();
                }
                return contador > 0 ? true : false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string GetFolioActaEntregaPorOrdenDe(string serie)
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
        public List<ActasEntregaPorOrdenDe> GetActaEntregaPorOrdenDeRango(DateTime inicio, DateTime final)
        {
            try
            {
                List<ActasEntregaPorOrdenDe> resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.ActasEntregaPorOrdenDe.Where(x => x.fechaHoraEntrega >= inicio && x.fechaHoraEntrega <= final).ToList();
                }
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CambiaEstadoActa(int idActa, int estado)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                string comando = "update ActasEntregaPorOrdenDe set estadoActa = " + estado + "where idActaEntregaPorOrdenDe = " + idActa;
                db.Database.ExecuteSqlCommand(comando);
                db.SaveChanges();
            }
        }
    }
}