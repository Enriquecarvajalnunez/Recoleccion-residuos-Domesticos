using System;
namespace Recoleccion.Models;

using System.ComponentModel.DataAnnotations;

public partial class EmpresaRecolectora
{
    [Key]
    public int Idempresa { get; set; }

    public string Nombre { get; set; } = null!;

    public string TipoResiduo { get; set; } = null!;

    //public virtual ICollection<Recoleccion> Recoleccions { get; set; } = new List<Recoleccion>();
}
