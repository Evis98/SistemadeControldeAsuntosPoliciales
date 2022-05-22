using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public class ActaEntregaDAL : IActaEntregaDAL
    {
        //Recibe todos las armas ingresados en la base de datos con su información
        public List<ActasEntrega> Get()
        {
            List<ActasEntrega> lista = new List<ActasEntrega>();
            using (SCAPEntities db = new SCAPEntities())
            {
                lista = db.ActasEntrega.ToList();
            }
            return lista;
        }

        //Permite agregar un arma nueva a la base de datos
        public void Add(ActasEntrega nuevo)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.ActasEntrega.Add(nuevo);
                db.SaveChanges();
            }
        }

        //Permite actualizar la información de un arma en la base de datos
        public void Edit(ActasEntrega cambiada)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.Entry(cambiada).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

        }
        //Permite recibir un arma con toda su información a través de su atributo "idArma"
        public ActasEntrega GetActaEntrega(int idActaEntrega)
        {
            try
            {
                ActasEntrega resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.ActasEntrega.Find(idActaEntrega);
                }
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActasEntrega GetActaEntregaFolio(string numeroFolio)
        {
            try
            {
                ActasEntrega resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.ActasEntrega.Where(x => x.numeroFolio == numeroFolio).FirstOrDefault();
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
                return db.ActasEntrega.Where(x => x.fechaHora.Year == DateTime.Now.Year).Count();
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
        public string GetFolioActaEntrega(string serie)
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

       
        public List<ActasEntrega> GetActaEntregaRango(DateTime inicio, DateTime final)
        {
            try
            {
                List<ActasEntrega> resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.ActasEntrega.Where(x => x.fechaHora >= inicio && x.fechaHora <= final).ToList();
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