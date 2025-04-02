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
    public DbSet<Recoleccion> recoleccion { get; set; }
}
