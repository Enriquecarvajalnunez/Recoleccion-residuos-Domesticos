using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsRecolectar;

public enum EstadoPuntosEnum
{
    Acumulado,
    Canjeado
}
public class PuntosUsuario
{
    [Key]
    public int IDPuntos { get; set; }

    [Required]
    [ForeignKey("Usuario")]
    public int IDUsuario { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int Puntos { get; set; }

    [Required]
    public DateTime FechaObtencion { get; set; } = DateTime.Now;

    [Required]
    public EstadoPuntosEnum Estado { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
}
