using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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
        public DbSet<Recolectar> Recolectar { get; set; }
        public DbSet<SubTipoResiduo> SubTipoResiduos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Permitimos reflejar los enums como cadenas en la base de datos, es mas legible.
            modelBuilder.Entity<Usuario>()
                .Property(u => u.Rol)
                .HasMaxLength(20)
                .IsRequired();

            modelBuilder.Entity<Residuo>()
                .Property(r => r.TipoResiduo)
                .HasConversion<string>(
                    v => ConvertEnumToString(v),
                    v => ConvertStringToEnum(v));

            modelBuilder.Entity<SubTipoResiduo>()
                .HasOne(s => s.Residuo)
                .WithMany(r => r.SubTipoResiduo)
                .HasForeignKey(s => s.IDResiduo);

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
                .ToTable("Recolectar", t =>
                {
                    t.HasCheckConstraint("CK_Recolectar_PesoKg", "PesoKg >0");
                });

            modelBuilder.Entity<PuntosUsuario>()
                .ToTable(t =>
                {
                    t.HasCheckConstraint("CK_PuntosUsuario_FechaObtencion", "FechaObtencion <= GETDATE()");
                });

            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Localidad>().ToTable("Localidad");
            modelBuilder.Entity<Residuo>().ToTable("Residuo");
            modelBuilder.Entity<SubTipoResiduo>().ToTable("SubTipoResiduo");
            modelBuilder.Entity<EmpresaRecolectora>().ToTable("EmpresaRecolectora");
        }

        // Configuramos la depuracion de datos sensibles.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.EnableSensitiveDataLogging();
            }
            base.OnConfiguring(optionsBuilder);
        }

        // Metodo auxiliar para la conversion.
        private static string ConvertEnumToString(TipoResiduoEnum valor)
        {
            return valor switch
            {
                TipoResiduoEnum.Orgánico => "Orgánico",
                TipoResiduoEnum.InorgánicoReciclable => "Inorgánico Reciclable",
                TipoResiduoEnum.Peligroso => "Peligroso",
                _ => throw new ArgumentOutOfRangeException("Valor no soportado")
            };
        }
        private static TipoResiduoEnum ConvertStringToEnum(string valor)
        {
            return valor switch
            {
                "Orgánico" => TipoResiduoEnum.Orgánico,
                "Inorgánico Reciclable" => TipoResiduoEnum.InorgánicoReciclable,
                "Peligroso" => TipoResiduoEnum.Peligroso,
                _ => throw new InvalidOperationException("Valor no soportado")
            };
        }

        // Agregamos el metodo SaveChanges, nos ayuda a detectar y prevenir errrores de validacion antes de que lleguen a la base de datos.
        public override int SaveChanges()
        {
            try
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
            catch (Exception ex)
            {
                // Manejo de excepciones de validación
                Console.WriteLine($"Error en SaveChanges: {ex.Message}");
                throw new DbUpdateException($"Error de validación: {ex.Message}", ex);
            }
        }
    }  
}