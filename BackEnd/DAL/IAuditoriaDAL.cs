using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public interface IAuditoriaDAL
    {
        List<Auditorias> Get();
        void Add(Auditorias nuevo);
        void Edit(Auditorias cambiada);
        Auditorias GetAuditoria(int idAuditoria);

        List<Auditorias> GetAuditoriasRango(DateTime inicio, DateTime final, int id);
    }
}

