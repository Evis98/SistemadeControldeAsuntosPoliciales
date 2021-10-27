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
            Armas arma = new Armas();
            using (SCAPEntities db = new SCAPEntities())
            {
                arma = db.Database.SqlQuery<Armas>("select * from Armas where idArma =" + idArma).Single<Armas>();
            }
            return arma;
        }
        public int GetArmaNumSerie(string numSerie)
        {
            int arma;
            using (SCAPEntities db = new SCAPEntities())
            {
                arma = db.Database.SqlQuery<int>("select idArma from Armas where numeroSerie ='" + numSerie + "'").Single<int>();
            }
            return arma;
        }
        public bool SerieExiste(string serie)
        {
            int contador;
            using (SCAPEntities db = new SCAPEntities())
            {
                contador = db.Database.SqlQuery<int>("select count(numeroSerie) from Armas where numeroSerie ='" + serie + "'").Single<int>();
            }

            return contador > 0 ? true : false;
        }
        public string GetSerieArma(string serie)
        {
            if (SerieExiste(serie))
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