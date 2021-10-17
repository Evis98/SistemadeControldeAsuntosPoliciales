using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public interface ITablaGeneralDAL
    {
        int? EstadoDefault();
        int? GetTipoCedula(int tipoCedula);
        string GetEstadoPolicia(int estado);
        int GetIdEstado(string estado);
        int? GetTipoRequisito(int tipoRequisito);
        string GetDescripcionRequisito(int tipoRequisito);
    }
}