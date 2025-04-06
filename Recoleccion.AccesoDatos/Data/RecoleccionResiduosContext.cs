using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Recoleccion.Models;

namespace Recoleccion.AccesoDatos.Data;

public class RecoleccionResiduosContext : IdentityDbContext
{
    public RecoleccionResiduosContext(DbContextOptions<RecoleccionResiduosContext> options) 
        : base(options)
    {
    }

    public DbSet<Usuario> usuario { get; set; }
    public DbSet<CanjePunto> canjePuntos { get; set; }
    public DbSet<ConfiguracionPunto> configuracionPuntos { get; set; }
    public DbSet<EmpresaRecolectora> empresaRecolectora { get; set; }
    public DbSet<Localidad> localidad { get; set; }
    public DbSet<Notificacion> notificacion { get; set; }
    public DbSet<PuntosUsuario> puntosUsuario { get; set; }
    public DbSet<Residuo> residuo { get; set; }
    public DbSet<Recolectar> recolectar { get; set; }

}
