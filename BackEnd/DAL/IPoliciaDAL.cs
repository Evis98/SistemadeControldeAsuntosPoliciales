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
        Policias GetPolicia(int idPolicia);
        Policias GetPoliciaCedula(string cedula);
        bool CedulaPoliciaExiste(string cedula);
        string GetCedulaPolicia(string cedula);
        List<Policias> GetPoliciasRango(DateTime inicio, DateTime final);
    }
}