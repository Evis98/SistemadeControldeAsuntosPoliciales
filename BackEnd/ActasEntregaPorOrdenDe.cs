//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BackEnd
{
    using System;
    using System.Collections.Generic;
    
    public partial class ActasEntregaPorOrdenDe
    {
        public int idActaEntregaPorOrdenDe { get; set; }
        public string numeroFolio { get; set; }
        public int idFuncionarioQueEntrega { get; set; }
        public Nullable<int> testigo { get; set; }
        public int tipoTestigo { get; set; }
        public Nullable<int> idActaLigada { get; set; }
        public int tipoInventario { get; set; }
        public Nullable<System.DateTime> fechaHoraEntrega { get; set; }
        public int idPorOrdenDe { get; set; }
        public string numeroResolucion { get; set; }
        public string numeroInventario { get; set; }
        public Nullable<int> idPersonaQueSeLeEntrega { get; set; }
        public int estado { get; set; }
    
        public virtual TablaGeneral TablaGeneral { get; set; }
        public virtual Policias Policias { get; set; }
        public virtual Personas Personas { get; set; }
        public virtual Personas Personas1 { get; set; }
        public virtual TablaGeneral TablaGeneral1 { get; set; }
        public virtual TablaGeneral TablaGeneral2 { get; set; }
    }
}
