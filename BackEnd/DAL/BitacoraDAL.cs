using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public class BitacoraDAL : IBitacoraDAL
    {
        //Recibe todos las baticoras ingresadas en la base de datos con su información
        public List<Bitacoras> Get()
        {
            List<Bitacoras> lista = new List<Bitacoras>();
            using (SCAPEntities db = new SCAPEntities())
            {
                lista = db.Bitacoras.ToList();
            }
            return lista;
        }

        //Permite agregar una bitacora nueva a la base de datos
        public void Add(Bitacoras bitacora)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.Bitacoras.Add(bitacora);
                db.SaveChanges();
            }
        }

        //Permite actualizar la información de una bitacora en la base de datos
        public void Edit(Bitacoras bitacora)
        {
            using (SCAPEntities db = new SCAPEntities())
            {
                db.Entry(bitacora).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        //Permite recibir una bitacora con toda su información a través de su atributo "idBitacora"
        public Bitacoras GetBitacora(int idBitacora)
        {
            try
            {
                Bitacoras resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.Bitacoras.Find(idBitacora);
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