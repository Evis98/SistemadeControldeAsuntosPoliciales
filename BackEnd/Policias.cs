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
    
    public partial class Policias
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Policias()
        {
            this.ActasDecomiso = new HashSet<ActasDecomiso>();
            this.ActasDecomiso1 = new HashSet<ActasDecomiso>();
            this.ActasDecomiso2 = new HashSet<ActasDecomiso>();
            this.ActasDeDestruccionDePerecederos = new HashSet<ActasDeDestruccionDePerecederos>();
            this.ActasDeDestruccionDePerecederos1 = new HashSet<ActasDeDestruccionDePerecederos>();
            this.ActasDeNotificacion = new HashSet<ActasDeNotificacion>();
            this.ActasDeObservacionPolicial = new HashSet<ActasDeObservacionPolicial>();
            this.ActasDeObservacionPolicial1 = new HashSet<ActasDeObservacionPolicial>();
            this.ActasEntrega = new HashSet<ActasEntrega>();
            this.ActasEntrega1 = new HashSet<ActasEntrega>();
            this.ActasEntregaPorOrdenDe = new HashSet<ActasEntregaPorOrdenDe>();
            this.ActasHallazgo = new HashSet<ActasHallazgo>();
            this.ActasHallazgo1 = new HashSet<ActasHallazgo>();
            this.ActasNotificacionVendedorAmbulante = new HashSet<ActasNotificacionVendedorAmbulante>();
            this.Bitacoras = new HashSet<Bitacoras>();
            this.Bitacoras1 = new HashSet<Bitacoras>();
            this.PartesPoliciales = new HashSet<PartesPoliciales>();
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
        public virtual ICollection<ActasDecomiso> ActasDecomiso { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActasDecomiso> ActasDecomiso1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActasDecomiso> ActasDecomiso2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActasDeDestruccionDePerecederos> ActasDeDestruccionDePerecederos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActasDeDestruccionDePerecederos> ActasDeDestruccionDePerecederos1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActasDeNotificacion> ActasDeNotificacion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActasDeObservacionPolicial> ActasDeObservacionPolicial { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActasDeObservacionPolicial> ActasDeObservacionPolicial1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActasEntrega> ActasEntrega { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActasEntrega> ActasEntrega1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActasEntregaPorOrdenDe> ActasEntregaPorOrdenDe { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActasHallazgo> ActasHallazgo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActasHallazgo> ActasHallazgo1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActasNotificacionVendedorAmbulante> ActasNotificacionVendedorAmbulante { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bitacoras> Bitacoras { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bitacoras> Bitacoras1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PartesPoliciales> PartesPoliciales { get; set; }
        public virtual TablaGeneral TablaGeneral { get; set; }
        public virtual TablaGeneral TablaGeneral1 { get; set; }
    }
}
