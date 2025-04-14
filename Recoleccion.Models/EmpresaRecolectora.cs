using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModelsRecolectar;

public partial class EmpresaRecolectora
{
    [Key]
    public int IDEmpresa { get; set; }

    [Required]
    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [Required]
    [StringLength(50)]
    public string TipoResiduo { get; set; } = null!;

    public virtual ICollection<Recolectar> Recolectars { get; set; } = new List<Recolectar>();
}
