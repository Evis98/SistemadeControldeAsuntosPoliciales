using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public interface ITablaGeneralDAL
    {
        TablaGeneral Get(int id);
        List<TablaGeneral> Get(string tabla, string campo);
        TablaGeneral GetCodigo(string tabla, string campo, string codigo);
        TablaGeneral Get(string tabla, string campo, string descripcion);
    }
}