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
    
    public partial class Policias
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Policias()
        {
            this.Requisitos = new HashSet<Requisitos>();
        }
    
        public int idPolicia { get; set; }
        public string cedula { get; set; }
        public Nullable<int> tipoCedula { get; set; }
        public string nombre { get; set; }
        public Nullable<System.DateTime> fechaNacimiento { get; set; }
        public string correoElectronico { get; set; }
        public string direccion { get; set; }
        public string telefonoCelular { get; set; }
        public string telefonoCasa { get; set; }
        public string contactoEmergencia { get; set; }
        public string telefonoEmergencia { get; set; }
        public Nullable<int> estado { get; set; }
    
        public virtual TablaGeneral TablaGeneral { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Requisitos> Requisitos { get; set; }
        public virtual TablaGeneral TablaGeneral1 { get; set; }
    }
}
