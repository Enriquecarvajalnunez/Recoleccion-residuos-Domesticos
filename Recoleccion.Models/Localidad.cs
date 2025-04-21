using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModelsRecolectar;

public partial class Localidad
{
    [Key]
    public int IDLocalidad { get; set; }

    [Required]
    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    public virtual ICollection<Usuario>? Usuarios { get; set; } = new List<Usuario>();
}
