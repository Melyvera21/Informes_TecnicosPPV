//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MdgPPV.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class InformesTecnicosDBEntities : DbContext
    {

        public InformesTecnicosDBEntities()
            : base("name=InformesTecnicosDBEntities")

        {
            this.Configuration.LazyLoadingEnabled = false;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Informes> Informes { get; set; }
        public virtual DbSet<Modulo> Modulo { get; set; }
        public virtual DbSet<Operaciones> Operaciones { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Rol_Operacion> Rol_Operacion { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}
