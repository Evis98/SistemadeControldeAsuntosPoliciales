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
        int GetTipoSexoInfractor(int? tipoSexo);
        List<TablaGeneral> GetTiposSexoInfractor();
        List<TablaGeneral> GetTiposIdentificacionInfractor(); 
        List<TablaGeneral> GetTiposCalibre(); 
        List<TablaGeneral> GetTiposCondicion();
        List<TablaGeneral> GetTiposUbicacion();
        int GetIdTipoArma(int? tipoArma);
        int GetIdCalibreArma(int? calibre);
        int GetIdCondicionArma(int? condicion);
        int GetIdUbicacionArma(int? ubicacion);
        int EstadoDefaultArma();
        int GetIdEstadoArmas(string estadoArma);
        void CambiaEstadoArma(int idArma, int estado);
        string GetCodigo(int? idTablaGeneral);
        int GetNacionalidadInfractor(int? nacionalidad);
        List<TablaGeneral> GetNacionalidadesInfractor();
    }
}