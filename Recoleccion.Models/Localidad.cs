using System;
using System.Collections.Generic;

namespace WebApi_Recoleccion_residuos_Domesticos.Models;

public partial class Localidad
{
    public int Idlocalidad { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
