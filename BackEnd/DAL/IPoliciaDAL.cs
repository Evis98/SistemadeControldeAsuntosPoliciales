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
        void Add(Policias policia);
        void Edit(Policias policia);
        Policias GetPolicia(int id);
        string GetPoliciaNombre(int id);
        int GetPoliciaCedula(string cedula);
        void CambiaEstadoPolicia(int id, int estado);
    }
}
