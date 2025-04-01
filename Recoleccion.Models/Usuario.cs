using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Recoleccion.Models;

public class Usuario
{
    [Key]
    public int Idusuario { get; set; }

    public int? Idlocalidad { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string? Email { get; set; }

    public string? Direccion { get; set; }

    public string? Rol { get; set; }

    //public virtual ICollection<CanjePunto> CanjePuntos { get; set; } = new List<CanjePunto>();

    //public virtual Localidad? IdlocalidadNavigation { get; set; }

    //public virtual ICollection<Notificacion> Notificacions { get; set; } = new List<Notificacion>();

    //public virtual ICollection<PuntosUsuario> PuntosUsuarios { get; set; } = new List<PuntosUsuario>();

    //public virtual ICollection<Recoleccion> Recoleccions { get; set; } = new List<Recoleccion>();
}
