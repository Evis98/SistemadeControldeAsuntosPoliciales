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
        //Recibe todos los requisitos ingresados en la base de datos con su información
        public List<Requisitos> Get()
        {
            List<Requisitos> lista = new List<Requisitos>();
            using (SCAPEntities db = new SCAPEntities())
            {
                lista = db.Requisitos.ToList();
            }
            return lista;
        }

        //Permite agregar un requisito nuevo a la base de datos
        public void Add(Requisitos requisito)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.Requisitos.Add(requisito);
                db.SaveChanges();
            }

        }

        //Permite actualizar la información de un requisito en la base de datos
        public void Edit(Requisitos requisito)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.Entry(requisito).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

        }

        //Permite la eliminación de un requisito de la base de datos
        public void EliminaRequisito(Requisitos requisito)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.Entry(requisito).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }

        //Permite recibir un requisito con toda su información a través de su atributo "idRequisito"
        public Requisitos GetRequisito(int idRequisito)
        {
            try
            {
                Requisitos resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.Requisitos.Find(idRequisito);
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
