using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public interface IParteDAL
    {
        List<PartesPoliciales> Get();
        void Add(PartesPoliciales parte);
        void Edit(PartesPoliciales parte);
        PartesPoliciales GetParte(int idParte);
        PartesPoliciales GetPartePolicial(string numeroFolio);
        bool NumeroFolioExiste(string numeroFolio);
        string GetNumeroFolio(string numeroFolio);
        int GetCount();
        List<PartesPoliciales> GetPartesRango(DateTime inicio, DateTime final);

    }
}
