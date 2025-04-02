using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi_Recoleccion_residuos_Domesticos.Models;

public partial class Residuo
{
    [Key]
    public int Idresiduo { get; set; }

    public string TipoResiduo { get; set; } = null!;

    //public virtual ICollection<Recoleccion> Recoleccions { get; set; } = new List<Recoleccion>();
}
