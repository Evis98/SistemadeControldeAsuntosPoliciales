using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public interface IActaDeNotificacionDAL
    {
        List<ActasDeNotificacion> Get();
        void Add(ActasDeNotificacion nuevo);
        void Edit(ActasDeNotificacion cambiada);
        ActasDeNotificacion GetActaDeNotificacion(int idActaDeNotificacion);
        ActasDeNotificacion GetActaDeNotificacionFolio(string numeroFolio);
        bool FolioExiste(string numeroFolio);
        string GetFolioActaDeNotificacion(string numeroFolio);
        int GetCount(DateTime year);
        List<ActasDeNotificacion> GetActaDeNotificacionRango(DateTime inicio, DateTime final);

        bool NotificadoExiste(int idNotificado);

        int VecesNotificado(int id);
    }
}
