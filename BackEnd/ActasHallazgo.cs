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
    
    public partial class ActasHallazgo
    {
        public int idActaHallazgo { get; set; }
        public string numeroFolio { get; set; }
        public int encargado { get; set; }
        public int testigo { get; set; }
        public int supervisor { get; set; }
        public int distrito { get; set; }
        public Nullable<System.DateTime> fechaHora { get; set; }
        public string avenida { get; set; }
        public string calle { get; set; }
        public string otrasSenas { get; set; }
        public string inventario { get; set; }
        public string observaciones { get; set; }
    
        public virtual Policias Policias { get; set; }
        public virtual Policias Policias1 { get; set; }
        public virtual Policias Policias2 { get; set; }
    }
}
