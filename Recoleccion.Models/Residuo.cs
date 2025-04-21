using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace ModelsRecolectar;

public partial class Residuo
{
    [Key]
    public int IDResiduo { get; set; }

    [Required]
    [EnumDataType(typeof(TipoResiduoEnum))]
    public TipoResiduoEnum TipoResiduo { get; set; }
    public ICollection<SubTipoResiduo>? SubTipoResiduo { get; set; } = new List<SubTipoResiduo>();
}
public enum TipoResiduoEnum
{
    Orgánico,
    InorgánicoReciclable,
    Peligroso
}
