using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEnd;

namespace BackEnd.DAL
{
    public interface IPoliciaDAL
    {
        List<Policias> Get();
        int? estadoDefault();
        int? getTipoCedula(int tipoCedula);

    }
}
