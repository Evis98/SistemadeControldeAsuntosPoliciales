using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public class ArmaDAL : IArmaDAL
    {
        //Recibe todos las armas ingresados en la base de datos con su información
        public List<Armas> Get()
        {
            List<Armas> lista = new List<Armas>();
            using (SCAPEntities db = new SCAPEntities())
            {
                lista = db.Armas.ToList();
            }
            return lista;
        }

        //Permite agregar un arma nueva a la base de datos
        public void Add(Armas nuevo)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.Armas.Add(nuevo);
                db.SaveChanges();
            }
        }

        //Permite actualizar la información de un arma en la base de datos
        public void Edit(Armas cambiada)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.Entry(cambiada).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

        }
        //Permite recibir un arma con toda su información a través de su atributo "idArma"
        public Armas GetArma(int idArma)
        {
            try
            {
                Armas resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.Armas.Find(idArma);
                }
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Armas GetArmaNumSerie(string numeroSerie)
        {
            try
            {
                Armas resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.Armas.Where(x => x.numeroSerie == numeroSerie).FirstOrDefault();
                }
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CambiaEstadoArma(int idArma, int estado)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                string comando = "update Armas set estadoArma = " + estado + "where idArma = " + idArma;
                db.Database.ExecuteSqlCommand(comando);
                db.SaveChanges();
            }
        }

        public bool ArmaExiste(string numeroSerie)
        {
            try
            {
                int contador;
                using (SCAPEntities db = new SCAPEntities())
                {
                    contador = db.Armas.Where(x => x.numeroSerie == numeroSerie).Count();
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