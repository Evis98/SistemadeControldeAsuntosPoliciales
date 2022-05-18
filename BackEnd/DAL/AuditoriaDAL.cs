using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public class AuditoriaDAL : IAuditoriaDAL
    {
       
        public List<Auditorias> Get()
        {
            List<Auditorias> lista = new List<Auditorias>();
            using (SCAPEntities db = new SCAPEntities())
            {
                lista = db.Auditorias.ToList();
            }
            return lista;
        }

        public void Add(Auditorias nuevo)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.Auditorias.Add(nuevo);
                db.SaveChanges();
            }
        }
       
        public void Edit(Auditorias cambiada)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.Entry(cambiada).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

        }
        
        public Auditorias GetAuditoria(int idAuditoria)
        {
            try
            {
                Auditorias resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.Auditorias.Find(idAuditoria);
                }
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Auditorias> GetAuditoriasRango(DateTime inicio, DateTime final, int id)
        {
            try
            {
                List<Auditorias> resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.Auditorias.Where(x => x.fecha >= inicio && x.fecha <= final && x.idElemento == id).ToList();
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