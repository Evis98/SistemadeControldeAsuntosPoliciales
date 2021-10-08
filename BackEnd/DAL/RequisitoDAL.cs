using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public class RequisitoDAL : IRequisitoDAL
    {

        public List<Requisitos> Get()
        {
            List<Requisitos> lista = new List<Requisitos>();
            using (SCAPEntities db = new SCAPEntities())
            {
                lista = db.Requisitos.ToList();
            }
            return lista;
        }

        public void Add(Requisitos nuevo)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.Requisitos.Add(nuevo);
                db.SaveChanges();
            }

        }

        public void Edit(Requisitos cambiado)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.Entry(cambiado).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

        }

        public Requisitos getRequisito(int id)
        {
            Requisitos requisito = new Requisitos();
            using (SCAPEntities db = new SCAPEntities())
            {
                requisito = db.Database.SqlQuery<Requisitos>("select * from Requisitos where idRequisito =" + id).Single<Requisitos>();
            }
            return requisito;
        }

        public Policias getPoliciaDeRequisito(int id)
        {
            Policias pol = new Policias();
            using (SCAPEntities db = new SCAPEntities())
            {
                pol = db.Database.SqlQuery<Policias>("select * from Policias where idRequisito =" + id).Single<Policias>();
            }
            return pol;
        }

        public int? getTipoRequisito(int tipoRequisito)
        {
            int aux;
            using (SCAPEntities db = new SCAPEntities())
            {
                aux = db.Database.SqlQuery<int>("Select idTablaGeneral from TablaGeneral where tabla= 'Requisitos' and campo = 'tipoRequisito' and codigo =" + tipoRequisito).Single<int>();
            }
            return aux;
        }

        public void EliminaRequisito(Requisitos eliminable)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.Entry(eliminable).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }
    }

}
