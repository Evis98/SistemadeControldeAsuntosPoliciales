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
    
    public partial class ActasEntrega
    {
        public int idActaEntrega { get; set; }
        public string numeroFolio { get; set; }
        public int encargado { get; set; }
        public int testigo { get; set; }
        public int supervisor { get; set; }
        public int instalaciones { get; set; }
        public System.DateTime fechaHora { get; set; }
        public string razonSocial { get; set; }
        public string cedulaJuridica { get; set; }
        public int tipoActa { get; set; }
        public string consecutivoActa { get; set; }
        public string nombreDe { get; set; }
        public string identificacionEntregado { get; set; }
        public string recibe { get; set; }
        public string cedulaRecibe { get; set; }
        public string inventarioEntregado { get; set; }
    
        public virtual Policias Policias { get; set; }
        public virtual TablaGeneral TablaGeneral { get; set; }
        public virtual Policias Policias1 { get; set; }
        public virtual Policias Policias2 { get; set; }
        public virtual TablaGeneral TablaGeneral1 { get; set; }
    }
}
