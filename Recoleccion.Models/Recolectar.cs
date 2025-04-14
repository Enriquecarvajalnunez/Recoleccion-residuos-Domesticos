using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsRecolectar;

public enum EstadoRecolectarEnum
{
    Programada,
    Completada,
    Cancelada,
}
public class Recolectar
{
    [Key]
    public int IDRecoleccion { get; set; }

    [Required]
    [ForeignKey("IDUsuario")]
    public int IDUsuario { get; set; }

    [Required]
    [ForeignKey("IDEmpresaRecolectora")]
    public int IDEmpresa { get; set; }

    [Required]
    [ForeignKey("IDResiduo")]
    public int IDResiduo { get; set; }

    [Required]
    public DateTime FechaRecoleccion { get; set; } = DateTime.Now;

    [Required]
    [Range(0.01, Double.MaxValue)]
    public decimal PesoKg { get; set; }

    [Required]
    public EstadoRecolectarEnum Estado { get; set; }
    public virtual Usuario Usuario { get; set; } = null!;
    public virtual EmpresaRecolectora EmpresaRecolectora { get; set; } = null!;
    public virtual Residuo Residuo { get; set; } = null!;
}
