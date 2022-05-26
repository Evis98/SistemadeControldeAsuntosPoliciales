using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public class RolDAL : IRolDAL
    {
        public List<Roles> Get()
        {
            List<Roles> lista = new List<Roles>();
            using (SCAPEntities db = new SCAPEntities())
            {
                lista = db.Roles.ToList();
            }
            return lista;
        }

        public Roles GetRol(int? idRol)
        {
            try
            {
                Roles resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.Roles.Find(idRol);
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