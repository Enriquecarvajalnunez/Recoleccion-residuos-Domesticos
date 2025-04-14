using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace ModelsRecolectar;
public enum TipoResiduoEnum
{
    Orgánico,
    InorgánicoReciclable,
    Peligroso,
}
public partial class Residuo
{
    [Key]
    public int IDResiduo { get; set; }

    [Required]
    public TipoResiduoEnum TipoResiduo { get; set; }

    public virtual ICollection<Recolectar> Recolectars { get; set; } = new List<Recolectar>();
}
