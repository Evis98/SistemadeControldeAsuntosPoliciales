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
    
    public partial class Requisitos
    {
        public int idRequisito { get; set; }
        public string detalles { get; set; }
        public Nullable<System.DateTime> fechaVencimiento { get; set; }
        public int idPolicia { get; set; }
        public int tipoRequisito { get; set; }
        public string archivo { get; set; }
    
        public virtual TablaGeneral TablaGeneral { get; set; }
        public virtual TablaGeneral TablaGeneral1 { get; set; }
    }
}
