using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public interface IPersonaDAL
    {
        List<Personas> Get();
        void Add(Personas persona);
        void Edit(Personas persona);
        Personas GetPersona(int idPersona);
        Personas GetPersonaIdentificacion(string identificacionPersona);
        bool IdentificacionExiste(string identificacionPersona);
        string GetCedulaPersona(string cedula);
    }

}