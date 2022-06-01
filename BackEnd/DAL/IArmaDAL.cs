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
        Armas GetArmaNumSerie(string numeroSerie);
        bool ArmaExiste(string serie);
        void CambiaEstadoArma(int idArma, int estado);

    }
}