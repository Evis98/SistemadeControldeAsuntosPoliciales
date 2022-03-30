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
    
    public partial class Policias
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Policias()
        {
            this.ActasHallazgo = new HashSet<ActasHallazgo>();
            this.ActasHallazgo1 = new HashSet<ActasHallazgo>();
            this.ActasHallazgo2 = new HashSet<ActasHallazgo>();
            this.Bitacoras = new HashSet<Bitacoras>();
            this.Bitacoras1 = new HashSet<Bitacoras>();
            this.PartesPoliciales = new HashSet<PartesPoliciales>();
            this.PartesPoliciales1 = new HashSet<PartesPoliciales>();
        }
    
        public int idPolicia { get; set; }
        public string cedula { get; set; }
        public int tipoCedula { get; set; }
        public string nombre { get; set; }
        public System.DateTime fechaNacimiento { get; set; }
        public string correoElectronico { get; set; }
        public string direccion { get; set; }
        public string telefonoCelular { get; set; }
        public string telefonoCasa { get; set; }
        public string contactoEmergencia { get; set; }
        public string telefonoEmergencia { get; set; }
        public int estado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActasHallazgo> ActasHallazgo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActasHallazgo> ActasHallazgo1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActasHallazgo> ActasHallazgo2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bitacoras> Bitacoras { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bitacoras> Bitacoras1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PartesPoliciales> PartesPoliciales { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PartesPoliciales> PartesPoliciales1 { get; set; }
        public virtual TablaGeneral TablaGeneral { get; set; }
        public virtual TablaGeneral TablaGeneral1 { get; set; }
    }
}
