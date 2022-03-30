using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEnd;

namespace BackEnd.DAL
{
    public interface IBitacoraDAL
    {
        List<Bitacoras> Get();
        void Add(Bitacoras bitacora);
        void Edit(Bitacoras bitacora);
        Bitacoras GetBitacora(int idBitacora);
        int GetCount();
        Bitacoras GetBitacoraConsecutivo(string numeroConsecutivo);
    }
}