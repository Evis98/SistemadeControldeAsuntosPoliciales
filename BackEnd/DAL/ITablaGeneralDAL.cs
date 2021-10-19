using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public interface ITablaGeneralDAL
    {
        int EstadoDefault();
        int GetTipoCedula(int? tipoCedula);
        int GetIdEstado(string estado);
        int GetTipoRequisito(int? tipoRequisito);
        string GetDescripcion(int? idTablaGeneral);
        List<string> GetTiposArma();
        List<TablaGeneral> GetTiposCedulaPolicia();
        List<TablaGeneral> GetTiposRequisito();

    }
}