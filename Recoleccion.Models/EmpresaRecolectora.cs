using System;
using System.Collections.Generic;

namespace WebApi_Recoleccion_residuos_Domesticos.Models;

public partial class EmpresaRecolectora
{
    public int Idempresa { get; set; }

    public string Nombre { get; set; } = null!;

    public string TipoResiduo { get; set; } = null!;

    public virtual ICollection<Recoleccion> Recoleccions { get; set; } = new List<Recoleccion>();
}
