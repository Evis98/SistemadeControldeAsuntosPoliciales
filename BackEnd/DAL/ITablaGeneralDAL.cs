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
        int GetIdEstadoPolicia(string estado);
        int GetTipoRequisito(int? tipoRequisito);
        string GetDescripcion(int? idTablaGeneral);
        List<TablaGeneral> GetTiposArma();
        List<TablaGeneral> GetTiposCedulaPolicia();
        List<TablaGeneral> GetTiposRequisito();
        int GetTipoIdentificacionInfractor(int? tipoId);
        List<TablaGeneral> GetTipoSexoInfractor();
        List<TablaGeneral> GetTiposIdentificacionInfractor(); 
        List<TablaGeneral> GetTiposCalibre(); 
        List<TablaGeneral> GetTiposCondicion();
        List<TablaGeneral> GetTiposUbicacion();
        int getIdTipoArma(int tipoArma);
        int getIdCalibreArma(int calibre);
        int getIdCondicionArma(int condicion);
        int getIdUbicacionArma(int ubicacion);
        int EstadoDefaultArma();
        int GetIdEstadoArmas(string estadoArma);
        void CambiaEstadoArma(int idArma, int estado);
    }
}