using System;
using System.Collections.Generic;
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

       
    }

}
