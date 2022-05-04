using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public interface IActaDeObservacionPolicialDAL
    {
        List<ActasDeObservacionPolicial> Get();
        void Add(ActasDeObservacionPolicial nuevo);
        void Edit(ActasDeObservacionPolicial cambiada);
        ActasDeObservacionPolicial GetActaDeObservacionPolicial(int idActaEntregaPorOrdenDe);
        ActasDeObservacionPolicial GetActaDeObservacionPolicialFolio(string numeroFolio);
        bool FolioExiste(string numeroFolio);
        string GetFolioActaDeObservacionPolicial(string numeroFolio);
        int GetCount();
        List<ActasDeObservacionPolicial> GetActaDeObservacionPolicialRango(DateTime inicio, DateTime final);
       
    }

}
