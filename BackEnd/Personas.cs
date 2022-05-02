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
    
    public partial class Personas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Personas()
        {
            this.PartesPoliciales = new HashSet<PartesPoliciales>();
        }
    
        public int idPersona { get; set; }
        public string nombre { get; set; }
        public int tipoIdentificacion { get; set; }
        public string identificacion { get; set; }
        public int sexo { get; set; }
        public System.DateTime fechaNacimiento { get; set; }
        public int nacionalidad { get; set; }
        public string direccionPersona { get; set; }
        public string telefonoHabitacion { get; set; }
        public string telefonoTrabajo { get; set; }
        public string telefonoCelular { get; set; }
        public string profesion { get; set; }
        public string correoElectronicoPersona { get; set; }
        public string lugarTrabajoPersona { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PartesPoliciales> PartesPoliciales { get; set; }
        public virtual TablaGeneral TablaGeneral { get; set; }
        public virtual TablaGeneral TablaGeneral1 { get; set; }
        public virtual TablaGeneral TablaGeneral2 { get; set; }
    }
}
