using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BackEnd.DAL
{
    public class PoliciaDAL : IPoliciaDAL
    {
        //Recibe todos los policías ingresados en la base de datos con su información
        public List<Policias> Get()
        {
            List<Policias> lista = new List<Policias>();
            using (SCAPEntities db = new SCAPEntities())
            {
                lista = db.Policias.ToList();
            }
            return lista;
        }

        //Permite agregar un policía nuevo a la base de datos
        public void Add(Policias nuevo)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.Policias.Add(nuevo);
                db.SaveChanges();
            }
        }

        //Permite actualizar la información de un policía en la base de datos
        public void Edit(Policias cambiado)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.Entry(cambiado).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

        }

        //Permite recibir un policía con toda su información a través de su atributo "idPolicia"
        public Policias GetPolicia(int id)
        {
            Policias poli = new Policias();
            using (SCAPEntities db = new SCAPEntities())
            {
                poli = db.Database.SqlQuery<Policias>("select * from Policias where idPolicia =" + id).Single<Policias>();
            }
            return poli;
        }

        public string GetPoliciaNombre(int id)
        {
            string poli;
            using (SCAPEntities db = new SCAPEntities())
            {
                poli = db.Database.SqlQuery<string>("select nombre from Policias where idPolicia =" + id).Single<string>();
            }
            return poli;
        }

        //Permite recibir un policía con toda su información a través de su atributo "cédula"
        public int GetPoliciaCedula(string cedula)
        {
            int poli;
            using (SCAPEntities db = new SCAPEntities())
            {
                poli = db.Database.SqlQuery<int>("select idPolicia from Policias where cedula ='" + cedula+"'").Single<int>();
            }
            return poli;
        }

        //Permite cambiar el atributo "estado" de un policía al recibir el dato "idPolicia" y "estado" 
        public void CambiaEstadoPolicia(int id, int estado)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                string comando = "update Policias set estado = " + estado + "where idPolicia = " + id;
                db.Database.ExecuteSqlCommand(comando);
                db.SaveChanges();
            }
        }
    }
}
