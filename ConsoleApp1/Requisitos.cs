//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConsoleApp1
{
    using System;
    using System.Collections.Generic;
    
    public partial class Requisitos
    {
        public int idRequisito { get; set; }
        public string detalles { get; set; }
        public Nullable<System.DateTime> fechaVencimiento { get; set; }
        public Nullable<int> idPolicia { get; set; }
        public Nullable<int> tipoRequsito { get; set; }
        public string imagen { get; set; }
    
        public virtual Policias Policias { get; set; }
        public virtual TablaGeneral TablaGeneral { get; set; }
    }
}
