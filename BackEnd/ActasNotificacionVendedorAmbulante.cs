//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BackEnd
{
    using System;
    using System.Collections.Generic;
    
    public partial class ActasNotificacionVendedorAmbulante
    {
        public int idNotificacionVendedorAmbulante { get; set; }
        public string numeroFolio { get; set; }
        public System.DateTime fechaHora { get; set; }
        public int idNotificado { get; set; }
        public int distrito { get; set; }
        public string caserio { get; set; }
        public int tipoTestigo { get; set; }
        public Nullable<int> testigo { get; set; }
        public string direccionNotificacion { get; set; }
        public string actividadVenta { get; set; }
        public int formaDeVenta { get; set; }
        public string placaDeVehiculo { get; set; }
        public int oficialActuante { get; set; }
        public string direccionNotificado { get; set; }
        public int estado { get; set; }
    
        public virtual TablaGeneral TablaGeneral { get; set; }
        public virtual Policias Policias { get; set; }
        public virtual TablaGeneral TablaGeneral1 { get; set; }
        public virtual TablaGeneral TablaGeneral2 { get; set; }
        public virtual Personas Personas { get; set; }
        public virtual TablaGeneral TablaGeneral3 { get; set; }
    }
}
