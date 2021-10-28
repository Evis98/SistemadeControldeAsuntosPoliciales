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
        public void Add(Policias policia)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.Policias.Add(policia);
                db.SaveChanges();
            }
        }

        //Permite actualizar la información de un policía en la base de datos
        public void Edit(Policias policia)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.Entry(policia).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

        }

        //Permite cambiar el atributo "estado" de un policía al recibir el dato "idPolicia" y "estado" 
        public void CambiaEstadoPolicia(int idPolicia, int estado)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                string comando = "update Policias set estado = " + estado + "where idPolicia = " + idPolicia;
                db.Database.ExecuteSqlCommand(comando);
                db.SaveChanges();
            }
        }


        //Permite recibir un policía con toda su información a través de su atributo "idPolicia"
        public Policias GetPolicia(int? idPolicia)
        {
            Policias poli = new Policias();
            using (SCAPEntities db = new SCAPEntities())
            {
                poli = db.Database.SqlQuery<Policias>("select * from Policias where idPolicia =" + idPolicia).Single<Policias>();
            }
            return poli;
        }

        //Permite recibir un policía con toda su información a través de su atributo "cédula"
        public int GetPoliciaCedula(string cedula)
        {
            int poli;
            using (SCAPEntities db = new SCAPEntities())
            {
                poli = db.Database.SqlQuery<int>("select idPolicia from Policias where cedula ='" + cedula + "'").Single<int>();
            }
            return poli;
        }

        public List<Policias> BuscaPolicias(string cedulaPolicia)
        {
            List<Policias> lista = new List<Policias>();
            using (SCAPEntities db = new SCAPEntities())
            {
                lista = db.Database.SqlQuery<Policias>("Select 'nombre','cedula' from Policias where tabla= 'Armas' and 'policiaAsignado' like" + cedulaPolicia).ToList<Policias>();
            }
            return lista;
        }
        public List<Policias> GetPolicias()
        {
            List<Policias> policias;
            using (SCAPEntities db = new SCAPEntities())
            {
                policias = db.Database.SqlQuery<Policias>("select * from Policias").ToList<Policias>();
            }
            return policias;
        }

        public bool CedulaPoliciaExiste(string cedulaPolicia)
        {
            int contador;
            using (SCAPEntities db = new SCAPEntities())
            {
                contador = db.Database.SqlQuery<int>("select count(cedula) from Policias where cedula ='" + cedulaPolicia + "'").Single<int>();
            }

            return contador > 0 ? true : false;
        }

        public string GetCedulaPolicia(string cedulaPolicia)
        {
            if (CedulaPoliciaExiste(cedulaPolicia))
            {
                return cedulaPolicia;
            }
            else
            {
                return null;
            }
        }
    }
}