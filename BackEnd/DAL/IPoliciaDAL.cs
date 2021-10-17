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
        Policias getPolicia(int id);
        string getPoliciaNombre(int id);
        int getPoliciaCedula(string cedula);
        void CambiaEstadoPolicia(int id, int estado);
    }
}
