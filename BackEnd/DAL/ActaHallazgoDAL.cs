using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public class ActaHallazgoDAL : IActaHallazgoDAL
    {
        //Recibe todos las armas ingresados en la base de datos con su información
        public List<ActasHallazgo> Get()
        {
            List<ActasHallazgo> lista = new List<ActasHallazgo>();
            using (SCAPEntities db = new SCAPEntities())
            {
                lista = db.ActasHallazgo.ToList();
            }
            return lista;
        }

        //Permite agregar un arma nueva a la base de datos
        public void Add(ActasHallazgo nuevo)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.ActasHallazgo.Add(nuevo);
                db.SaveChanges();
            }
        }

        //Permite actualizar la información de un arma en la base de datos
        public void Edit(ActasHallazgo cambiada)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.Entry(cambiada).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

        }
        //Permite recibir un arma con toda su información a través de su atributo "idArma"
        public ActasHallazgo GetActaHallazgo(int idActaHallazgo)
        {
            try
            {
                ActasHallazgo resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.ActasHallazgo.Find(idActaHallazgo);
                }
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActasHallazgo GetActaHallazgoFolio(string numeroFolio)
        {
            try
            {
                ActasHallazgo resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.ActasHallazgo.Where(x => x.numeroFolio == numeroFolio).FirstOrDefault();
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
                return db.ActasHallazgo.Where(x => x.fechaHora.Value.Year == DateTime.Now.Year).Count();
            }

        }
        public bool FolioExiste(string numeroFolio)
        {
            try
            {
                int contador;
                using (SCAPEntities db = new SCAPEntities())
                {
                    contador = db.ActasHallazgo.Where(x => x.numeroFolio == numeroFolio).Count();
                }
                return contador > 0 ? true : false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string GetFolioActaHallazgo(string serie)
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
    }
}