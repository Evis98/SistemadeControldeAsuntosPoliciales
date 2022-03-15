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
