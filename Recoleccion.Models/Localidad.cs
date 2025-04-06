using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Recoleccion.Models;

public partial class Localidad
{
    [Key]
    public int Idlocalidad { get; set; }

    public string Nombre { get; set; } = null!;

    //public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
