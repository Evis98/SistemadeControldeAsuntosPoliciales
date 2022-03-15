using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public class TablaGeneralDAL : ITablaGeneralDAL
    {
        public TablaGeneral Get(int id)
        {

            try
            {
                TablaGeneral resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.TablaGeneral.Find(id);
                }
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<TablaGeneral> Get(string tabla, string campo)
        {
            try
            {
                List<TablaGeneral> resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.TablaGeneral.Where(x => x.tabla == tabla & x.campo == campo).ToList();
                }
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public TablaGeneral GetCodigo(string tabla, string campo, string codigo)
        {
            try
            {
                TablaGeneral resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.TablaGeneral.Where(x => x.tabla == tabla & x.campo == campo & x.codigo == codigo).FirstOrDefault();
                }
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public TablaGeneral Get(string tabla, string campo, string descripcion)
        {
            try
            {
                TablaGeneral resultado;
                using (SCAPEntities db = new SCAPEntities())
                {
                    resultado = db.TablaGeneral.Where(x => x.tabla == tabla & x.campo == campo & x.descripcion.Contains(descripcion)).FirstOrDefault();
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