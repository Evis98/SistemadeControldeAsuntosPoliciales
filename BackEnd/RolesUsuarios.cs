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
    
    public partial class RolesUsuarios
    {
        public int idRolUsuario { get; set; }
        public Nullable<int> tipoRol { get; set; }
        public Nullable<int> idUsuario { get; set; }
    
        public virtual Roles Roles { get; set; }
        public virtual Usuarios Usuarios { get; set; }
    }
}
