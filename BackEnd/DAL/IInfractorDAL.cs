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
        Infractores GetInfractor(int? idInfractor);
        string GetNombreInfractor(int idInfractor);
        int GetNumeroIdInfractor(string identificacionInfractor);

    }
}