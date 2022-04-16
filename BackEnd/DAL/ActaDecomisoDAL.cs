using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public class ActaDecomisoDAL : IActaDecomisoDAL
    {
        //Recibe todos las armas ingresados en la base de datos con su información
        public List<ActasDecomiso> Get()
        {
            List<ActasDecomiso> lista = new List<ActasDecomiso>();
            using (SCAPEntities db = new SCAPEntities())
            {
                lista = db.ActasDecomiso.ToList();
            }
            return lista;
        }

        //Permite agregar un arma nueva a la base de datos
        public void Add(ActasDecomiso nuevo)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.ActasDecomiso.Add(nuevo);
                db.SaveChanges();
            }
        }

        //Permite actualizar la información de un arma en la base de datos
        public void Edit(ActasDecomiso cambiada)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.Entry(cambiada).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

        }
        //Permite recibir un arma con toda su información a través de su atributo "idArma"
        public ActasDecomiso GetActaDecomiso(int idActaDecomiso)
        {
            try
            {
                ActasDecomiso resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.ActasDecomiso.Find(idActaDecomiso);
                }
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActasDecomiso GetActaDecomisoFolio(string numeroFolio)
        {
            try
            {
                ActasDecomiso resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.ActasDecomiso.Where(x => x.numeroFolio == numeroFolio).FirstOrDefault();
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
                return db.ActasDecomiso.Where(x => x.fecha.Year == DateTime.Now.Year).Count();
            }

        }
        public bool FolioExiste(string numeroFolio)
        {
            try
            {
                int contador;
                using (SCAPEntities db = new SCAPEntities())
                {
                    contador = db.ActasDecomiso.Where(x => x.numeroFolio == numeroFolio).Count();
                }
                return contador > 0 ? true : false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string GetFolioActaDecomiso(string serie)
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
        public List<ActasDecomiso> GetActaDecomisoRango(DateTime inicio, DateTime final)
        {
            try
            {
                List<ActasDecomiso> resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.ActasDecomiso.Where(x => x.fecha >= inicio && x.fecha <= final).ToList();
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
                string comando = "update ActasDecomiso set estadoActa = " + estado + "where idActaDecomiso = " + idActa;
                db.Database.ExecuteSqlCommand(comando);
                db.SaveChanges();
            }
        }
    }
}