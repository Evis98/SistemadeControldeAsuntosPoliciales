using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public interface IActaEntregaPorOrdenDeDAL
    {
        List<ActasEntregaPorOrdenDe> Get();
        void Add(ActasEntregaPorOrdenDe nuevo);
        void Edit(ActasEntregaPorOrdenDe cambiada);
        ActasEntregaPorOrdenDe GetActaEntregaPorOrdenDe(int idActaEntregaPorOrdenDe);
        ActasEntregaPorOrdenDe GetActaEntregaPorOrdenDeFolio(string numeroFolio);
        bool FolioExiste(string numeroFolio);
        string GetFolioActaEntregaPorOrdenDe(string numeroFolio);
        int GetCount();
        List<ActasEntregaPorOrdenDe> GetActaEntregaPorOrdenDeRango(DateTime inicio, DateTime final);
        void CambiaEstadoActa(int idActa, int estado);
    }
}
