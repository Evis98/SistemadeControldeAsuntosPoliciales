﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SCAPEntities : DbContext
    {
        public SCAPEntities()
            : base("name=SCAPEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ActasDecomiso> ActasDecomiso { get; set; }
        public virtual DbSet<ActasEntrega> ActasEntrega { get; set; }
        public virtual DbSet<ActasEntregaPorOrdenDe> ActasEntregaPorOrdenDe { get; set; }
        public virtual DbSet<ActasHallazgo> ActasHallazgo { get; set; }
        public virtual DbSet<Armas> Armas { get; set; }
        public virtual DbSet<Bitacoras> Bitacoras { get; set; }
        public virtual DbSet<Infractores> Infractores { get; set; }
        public virtual DbSet<PartesPoliciales> PartesPoliciales { get; set; }
        public virtual DbSet<Personas> Personas { get; set; }
        public virtual DbSet<Policias> Policias { get; set; }
        public virtual DbSet<Requisitos> Requisitos { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<RolesUsuarios> RolesUsuarios { get; set; }
        public virtual DbSet<TablaGeneral> TablaGeneral { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
    }
}
