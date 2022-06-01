using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public interface IInfractorDAL
    {
        List<Infractores> Get();
        void Add(Infractores infractor);
        void Edit(Infractores infractor);
        Infractores GetInfractor(int idInfractor);
        Infractores GetInfractorIdentificacion(string identificacionInfractor);
        bool InfractorExiste(string identificacionInfractor);

    }
}