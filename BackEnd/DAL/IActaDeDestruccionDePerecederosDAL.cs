using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public interface IActaDeDestruccionDePerecederosDAL
    {
        List<ActasDeDestruccionDePerecederos> Get();
        void Add(ActasDeDestruccionDePerecederos nuevo);
        void Edit(ActasDeDestruccionDePerecederos cambiada);
        ActasDeDestruccionDePerecederos GetActaDeDestruccionDePerecederos(int idActaDeDestruccionDePerecederos);
        ActasDeDestruccionDePerecederos GetActaDeDestruccionDePerecederosFolio(string numeroFolio);
        bool FolioExiste(string numeroFolio);
        string GetFolioActaDeDestruccionDePerecederos(string numeroFolio);
        int GetCount();
        List<ActasDeDestruccionDePerecederos> GetActaDeDestruccionDePerecederosRango(DateTime inicio, DateTime final);
    }
}