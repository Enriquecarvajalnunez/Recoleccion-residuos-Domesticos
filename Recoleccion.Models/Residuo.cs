using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Recoleccion.Models;

public partial class Residuo
{
    [Key]
    public int Idresiduo { get; set; }

    public string TipoResiduo { get; set; } = null!;

    //public virtual ICollection<Recoleccion> Recoleccions { get; set; } = new List<Recoleccion>();
}
