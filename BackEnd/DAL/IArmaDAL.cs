using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public interface IArmaDAL
    {
        List<Armas> Get();
        void Add(Armas nuevo);
        void Edit(Armas cambiada);
        Armas GetArma(int idArma);
        int GetArmaNumSerie(string numSerie);
        bool SerieExiste(string serie);
        string GetSerieArma(string serie);

    }
}