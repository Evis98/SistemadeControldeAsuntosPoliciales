using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public interface IActaEntregaDAL
    {
        List<ActasEntrega> Get();
        void Add(ActasEntrega nuevo);
        void Edit(ActasEntrega cambiada);
        ActasEntrega GetActaEntrega(int idActaEntrega);
        ActasEntrega GetActaEntregaFolio(string numeroFolio);
        bool FolioExiste(string numeroFolio);
        string GetFolioActaEntrega(string numeroFolio);
        int GetCount();
        List<ActasEntrega> GetActaEntregaRango(DateTime inicio, DateTime final);
    }
}
