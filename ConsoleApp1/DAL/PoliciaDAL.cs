using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp1.DAL
{
    public class PoliciaDAL : IPoliciaDAL
    {
        public List<Policias> Get()
        {
            List<Policias> lista = new List<Policias>();
            using (SCAPEntities db = new SCAPEntities())
            {
                lista = db.Policias.ToList();
            }
            return lista;
        }
    }
}
