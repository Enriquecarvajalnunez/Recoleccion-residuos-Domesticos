using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsRecolectar;

public partial class CanjePuntos
{
    [Key]
    public int IDCanje { get; set; }

    [ForeignKey("Usuario")]
    public int IDUsuario { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int PuntosUsados { get; set; }

    [Required]
    [StringLength(100)]
    [RegularExpression(@"\S+", ErrorMessage = "La tienda no puede estar vacia o solo contener espacios")]
    public string Tienda { get; set; } = null!;

    [Required]
    public DateTime FechaCanje { get; set; } = DateTime.Now;

    public virtual Usuario Usuario { get; set; } = null!;
}
