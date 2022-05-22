using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public interface IActaDecomisoDAL
    {
        List<ActasDecomiso> Get();
        void Add(ActasDecomiso nuevo);
        void Edit(ActasDecomiso cambiada);
        ActasDecomiso GetActaDecomiso(int idActaDecomiso);
        ActasDecomiso GetActaDecomisoFolio(string numeroFolio);
        bool FolioExiste(string numeroFolio);
        string GetFolioActaDecomiso(string numeroFolio);
        int GetCount();
        List<ActasDecomiso> GetActaDecomisoRango(DateTime inicio, DateTime final);
        void CambiaEstadoActa(int idActa, int estado);

        Personas GetPersonaPorIdActa(int id);
    }
}

