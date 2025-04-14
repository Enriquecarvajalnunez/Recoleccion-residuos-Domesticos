using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsRecolectar;

public enum RolEnum
{
    Administrador,
    Usuario
}
public class Usuario

{
    [Key]
    public int IDUsuario { get; set; }

    [Required]
    [ForeignKey("Localidad")]
    public int IDLocalidad { get; set; }

    [Required]
    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [Required]
    [StringLength(100)]
    public string Apellidos { get; set; } = null!;

    [Required]
    [StringLength(15)]
    public string Telefono { get; set; } = null!;

    [Required]
    [StringLength(100)]
    public string Email { get; set; } = null!;

    [Required]
    [StringLength(100)]
    public string Direccion { get; set; } = null!;

    [Required]
    public RolEnum Rol { get; set; }

    public virtual Localidad Localidad { get; set; } = null!;

    public virtual ICollection<Notificacion> Notificacions { get; set; } = new List<Notificacion>();

    public virtual ICollection<PuntosUsuario> PuntosUsuarios { get; set; } = new List<PuntosUsuario>();

    public virtual ICollection<Recolectar> Recolectars { get; set; } = new List<Recolectar>();

    public virtual ICollection<CanjePuntos> CanjePuntos { get; set; } = new List<CanjePuntos>();
}
