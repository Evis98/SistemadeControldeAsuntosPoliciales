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
        public Policias GetPolicia(int idPolicia)
        {
            try
            {
                Policias resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.Policias.Find(idPolicia);
                }
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Permite recibir un policía con toda su información a través de su atributo "cédula"
        public Policias GetPoliciaCedula(string cedula)
        {
            try
            {
                Policias resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.Policias.Where(x => x.cedula == cedula).FirstOrDefault();
                }
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool CedulaPoliciaExiste(string cedula)
        {
            try
            {
                int contador;
                using (SCAPEntities db = new SCAPEntities())
                {
                    contador = db.Policias.Where(x => x.cedula == cedula).Count();
                }
                return contador > 0 ? true : false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetCedulaPolicia(string cedula)
        {
            if (CedulaPoliciaExiste(cedula))
            {
                return cedula;
            }
            else
            {
                return null;
            }
        }
    }
}