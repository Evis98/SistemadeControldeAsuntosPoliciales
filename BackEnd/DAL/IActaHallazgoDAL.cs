using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public interface IActaHallazgoDAL
    {
        List<ActasHallazgo> Get();
        void Add(ActasHallazgo nuevo);
        void Edit(ActasHallazgo cambiada);
        ActasHallazgo GetActaHallazgo(int idActaHallazgo);
        ActasHallazgo GetActaHallazgoFolio(string numeroFolio);
        bool FolioExiste(string numeroFolio);
        string GetFolioActaHallazgo(string numeroFolio);
        int GetCount();
        List<ActasHallazgo> GetActaHallazgoRango(DateTime inicio, DateTime final);
    }
}
