using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Recoleccion.Models;
using WebApi_Recoleccion_residuos_Domesticos.Models;

namespace Recoleccion.AccesoDatos.Data
{
    public class RecoleccionResiduosContext : IdentityDbContext
    {
        public RecoleccionResiduosContext(DbContextOptions<RecoleccionResiduosContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<ConfiguracionPunto> ConfiguracionPunto { get; set; }
        public DbSet<RecoleccionResiduosContext> Recoleccion { get; set; }
        public DbSet<PuntosUsuario> PuntosUsuario { get; set; }
        public DbSet<Residuo> Residuo { get; set; }
        public DbSet<Localidad> Localidad { get; set; }
        public DbSet<Notificacion> Notificacion { get; set; }

    }
}

