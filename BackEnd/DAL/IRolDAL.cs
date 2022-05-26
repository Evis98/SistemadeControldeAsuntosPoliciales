using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public interface IRolDAL
    {
        List<Roles> Get();
        //void Add(Roles rol);
        //void Edit(Roles rol);
        Roles GetRol(int? idRol);
    }
}
