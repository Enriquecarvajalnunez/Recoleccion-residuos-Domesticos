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

    [ForeignKey("Usuario")]
    public int IDUsuario { get; set; }
    public virtual Usuario? Usuario { get; set; } // Agregamos la propiedad de navegación para Usuario.

    [ForeignKey("EmpresaRecolectora")]
    public int? IDEmpresa { get; set; }
    public virtual EmpresaRecolectora? EmpresaRecolectora { get; set; } // Agregamos la propiedad de navegación para EmpresaRecolectora.

    [ForeignKey("Residuo")]
    public int IDResiduo { get; set; }
    public virtual Residuo? Residuo { get; set; } // Agregamos la propiedad de navegación para Residuo.

    [Required]
    public DateTime FechaSolicitudRecoleccion { get; set; } = DateTime.Now; // Valor por defecto es la fecha y hora actual.

    public DateTime? FechaRecoleccion { get; set; }

    [Range(0.01, Double.MaxValue)]
    public decimal? PesoKg { get; set; }

    [Required]
    public EstadoRecolectarEnum Estado { get; set; } = EstadoRecolectarEnum.Programada; // Valor por defecto es Programada.
}
