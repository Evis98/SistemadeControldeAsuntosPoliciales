using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public interface IActaNotificacionVendedorAmbulanteDAL
    {
        List<ActasNotificacionVendedorAmbulante> Get();
        void Add(ActasNotificacionVendedorAmbulante nuevo);
        void Edit(ActasNotificacionVendedorAmbulante cambiada);
        ActasNotificacionVendedorAmbulante GetActaNotificacionVendedorAmbulante(int idActaNotificacionVendedorAmbulante);
        ActasNotificacionVendedorAmbulante GetActaNotificacionVendedorAmbulanteFolio(string numeroFolio);
        bool FolioExiste(string numeroFolio);
        string GetFolioActaNotificacionVendedorAmbulante(string numeroFolio);
        int GetCount();
        List<ActasNotificacionVendedorAmbulante> GetActaNotificacionVendedorAmbulanteRango(DateTime inicio, DateTime final);
    }
}
