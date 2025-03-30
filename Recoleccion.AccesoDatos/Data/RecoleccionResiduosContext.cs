using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApi_Recoleccion_residuos_Domesticos.Models;

namespace Recoleccion.AccesoDatos.Data;

public partial class RecoleccionResiduosContext : DbContext
{
    public RecoleccionResiduosContext()
    {
    }

    public RecoleccionResiduosContext(DbContextOptions<RecoleccionResiduosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CanjePunto> CanjePuntos { get; set; }

    public virtual DbSet<ConfiguracionPunto> ConfiguracionPuntos { get; set; }

    public virtual DbSet<EmpresaRecolectora> EmpresaRecolectoras { get; set; }

    public virtual DbSet<Localidad> Localidads { get; set; }

    public virtual DbSet<Notificacion> Notificacions { get; set; }

    public virtual DbSet<PuntosUsuario> PuntosUsuarios { get; set; }

    public virtual DbSet<Recoleccion> Recoleccions { get; set; }

    public virtual DbSet<Residuo> Residuos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {         
        optionsBuilder.UseSqlServer("Server=ENRIQUE\\SQLEXPRESS;Database=Recoleccion-Residuos;Integrated Security=True;TrustServerCertificate=True;");
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CanjePunto>(entity =>
        {
            entity.HasKey(e => e.Idcanje).HasName("PK__CanjePun__D8C7D4B88F81F7B9");

            entity.Property(e => e.Idcanje).HasColumnName("IDCanje");
            entity.Property(e => e.FechaCanje)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Idusuario).HasColumnName("IDUsuario");
            entity.Property(e => e.Tienda).HasMaxLength(100);

            entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.CanjePuntos)
                .HasForeignKey(d => d.Idusuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CanjePunt__IDUsu__6477ECF3");
        });

        modelBuilder.Entity<ConfiguracionPunto>(entity =>
        {
            entity.HasKey(e => e.Idconfiguracion).HasName("PK__Configur__34AFFB0DA34E2939");

            entity.Property(e => e.Idconfiguracion).HasColumnName("IDConfiguracion");
            entity.Property(e => e.FactorConversion).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.UltimaActualizacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<EmpresaRecolectora>(entity =>
        {
            entity.HasKey(e => e.Idempresa).HasName("PK__EmpresaR__ED09F0D531A90B1E");

            entity.ToTable("EmpresaRecolectora");

            entity.Property(e => e.Idempresa).HasColumnName("IDEmpresa");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.TipoResiduo).HasMaxLength(50);
        });

        modelBuilder.Entity<Localidad>(entity =>
        {
            entity.HasKey(e => e.Idlocalidad).HasName("PK__Localida__33CB848AC9DC6523");

            entity.ToTable("Localidad");

            entity.Property(e => e.Idlocalidad).HasColumnName("IDLocalidad");
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Notificacion>(entity =>
        {
            entity.HasKey(e => e.Idnotificacion).HasName("PK__Notifica__8687136772ED20E9");

            entity.ToTable("Notificacion");

            entity.Property(e => e.Idnotificacion).HasColumnName("IDNotificacion");
            entity.Property(e => e.FechaEnvio).HasColumnType("datetime");
            entity.Property(e => e.Idusuario).HasColumnName("IDUsuario");
            entity.Property(e => e.Mensaje).HasMaxLength(255);

            entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.Notificacions)
                .HasForeignKey(d => d.Idusuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificac__IDUsu__5FB337D6");
        });

        modelBuilder.Entity<PuntosUsuario>(entity =>
        {
            entity.HasKey(e => e.Idpuntos).HasName("PK__PuntosUs__7BE7876F31382C57");

            entity.ToTable("PuntosUsuario");

            entity.Property(e => e.Idpuntos).HasColumnName("IDPuntos");
            entity.Property(e => e.Estado).HasMaxLength(20);
            entity.Property(e => e.FechaObtencion).HasColumnType("datetime");
            entity.Property(e => e.Idusuario).HasColumnName("IDUsuario");

            entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.PuntosUsuarios)
                .HasForeignKey(d => d.Idusuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PuntosUsu__IDUsu__5CD6CB2B");
        });

        modelBuilder.Entity<Recoleccion>(entity =>
        {
            entity.HasKey(e => e.Idrecoleccion).HasName("PK__Recolecc__07AB6F139172A223");

            entity.ToTable("Recoleccion");

            entity.Property(e => e.Idrecoleccion).HasColumnName("IDRecoleccion");
            entity.Property(e => e.Estado).HasMaxLength(20);
            entity.Property(e => e.FechaRecoleccion).HasColumnType("datetime");
            entity.Property(e => e.Idempresa).HasColumnName("IDEmpresa");
            entity.Property(e => e.Idresiduo).HasColumnName("IDResiduo");
            entity.Property(e => e.Idusuario).HasColumnName("IDUsuario");
            entity.Property(e => e.PesoKg).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.IdempresaNavigation).WithMany(p => p.Recoleccions)
                .HasForeignKey(d => d.Idempresa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Recolecci__IDEmp__6A30C649");

            entity.HasOne(d => d.IdresiduoNavigation).WithMany(p => p.Recoleccions)
                .HasForeignKey(d => d.Idresiduo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Recolecci__IDRes__6B24EA82");

            entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.Recoleccions)
                .HasForeignKey(d => d.Idusuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Recolecci__IDUsu__693CA210");
        });

        modelBuilder.Entity<Residuo>(entity =>
        {
            entity.HasKey(e => e.Idresiduo).HasName("PK__Residuo__772FFCF49BC19F6D");

            entity.ToTable("Residuo");

            entity.HasIndex(e => e.TipoResiduo, "UQ__Residuo__544E9F0A7270E37E").IsUnique();

            entity.Property(e => e.Idresiduo).HasColumnName("IDResiduo");
            entity.Property(e => e.TipoResiduo).HasMaxLength(50);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Idusuario).HasName("PK__Usuario__52311169D638CFB2");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Telefono, "UQ__Usuario__4EC50480005280C4").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Usuario__A9D1053440F4D5E4").IsUnique();

            entity.Property(e => e.Idusuario).HasColumnName("IDUsuario");
            entity.Property(e => e.Apellidos).HasMaxLength(100);
            entity.Property(e => e.Direccion).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Idlocalidad).HasColumnName("IDLocalidad");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Rol).HasMaxLength(20);
            entity.Property(e => e.Telefono).HasMaxLength(15);

            entity.HasOne(d => d.IdlocalidadNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.Idlocalidad)
                .HasConstraintName("FK__Usuario__IDLocal__5812160E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
