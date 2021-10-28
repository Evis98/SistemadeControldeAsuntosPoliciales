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
        void CambiaEstadoPolicia(int idPolicia, int estado);
        Policias GetPolicia(int? idPolicia);
        int GetPoliciaCedula(string cedula);
        List<Policias> BuscaPolicias(string cedulaPolicia);
        List<Policias> GetPolicias();
        bool CedulaPoliciaExiste(string cedulaPolicia);
        string GetCedulaPolicia(string cedulaPolicia);
    }
}
