using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public interface ITablaGeneralDAL
    {
        int? estadoDefault();
        int? getTipoCedula(int tipoCedula);
        string getEstadoPolicia(int estado);
        int getIdEstado(string estado);
        int? getTipoRequisito(int tipoRequisito);
        string getDescripcionRequisito(int tipoRequisito);
    }
}