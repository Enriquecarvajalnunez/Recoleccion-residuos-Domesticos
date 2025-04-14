using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ModelsRecolectar;

namespace Recoleccion.AccesoDatos.Data
{
    public class RecoleccionResiduosContext : IdentityDbContext
    {
        public RecoleccionResiduosContext(DbContextOptions<RecoleccionResiduosContext> options)
            : base(options)
        {
        }
        public DbSet<EmpresaRecolectora> EmpresaRecolectoras { get; set; }
        public DbSet<Localidad> Localidades { get; set; }
        public DbSet<Residuo> Residuos { get; set; }
        public DbSet<ConfiguracionPuntos> ConfiguracionPuntos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<PuntosUsuario> PuntosUsuarios { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }
        public DbSet<CanjePuntos> CanjePuntos { get; set; }
        public DbSet<Recolectar> Recolecciones { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Permitimos reflejar los enums como cadenas en la base de datos, es mas legible.
            modelBuilder.Entity<Usuario>()
                .Property(u => u.Rol)
                .HasConversion(
                    rol => rol.ToString(),
                    rol => (RolEnum)Enum.Parse(typeof(RolEnum), rol)
                 );

            modelBuilder.Entity<PuntosUsuario>()
                .Property(p => p.Estado)
                .HasConversion(
                    estado => estado.ToString(),
                    estado => (EstadoPuntosEnum)Enum.Parse(typeof(EstadoPuntosEnum), estado)
                 );

            modelBuilder.Entity<Recolectar>()
                .Property(r => r.Estado)
                .HasConversion(
                    estado => estado.ToString(),
                    estado => (EstadoRecolectarEnum)Enum.Parse(typeof(EstadoRecolectarEnum), estado)
                 );

            // Garantizamos que las restricciones esten documentadas y aseguramos su aplicacion durante migraciones.
            modelBuilder.Entity<Recolectar>()
                .ToTable(t =>
                {
                    t.HasCheckConstraint("CK_Recolectar_PesoKg", "PesoKg >0");
                });

            modelBuilder.Entity<PuntosUsuario>()
                .ToTable(t =>
                {
                    t.HasCheckConstraint("CK_PuntosUsuario_FechaObtencion", "FechaObtencion <= GETDATE()");
                });

            // Aseguramos las relaciones que se ocnfiguren explicitamente, evitando errores de mapeo.
            modelBuilder.Entity<Notificacion>()
                .HasOne(n => n.Usuario)
                .WithMany(u => u.Notificacions)
                .HasForeignKey(n => n.IDUsuario);

            modelBuilder.Entity<Recolectar>()
                .HasOne(r => r.Usuario)
                .WithMany(u => u.Recolectars)
                .HasForeignKey(r => r.IDUsuario);
        }
        // Agregamos el metodo SaveChanges, nos ayuda a detectar y prevenir errrores de validacion antes de que lleguen a la base de datos.
        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                   var entity = entry.Entity;
                   var validationContext = new ValidationContext(entity);
                   Validator.ValidateObject(entity, validationContext, validateAllProperties: true);
                }
            }
            return base.SaveChanges();
        }
    }  
}
// OnModelCrating: Se usa para manejar configuraciones avanzadas (como enums, restricciones y relaciones) que mejoran la legibilidad, la integridad de los datos y el control en la base de datos.
// SaveChanges: Nos permite validar automaticamente las propiedades del modelo antes de guardar cambios, asegurando la calidad de los datos.

