using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Recoleccion.Models;

public partial class CanjePunto
{
    [Key]
    public int Idcanje { get; set; }

    [Required(ErrorMessage = "El usuario es obligatorio")]
    public int Idusuario { get; set; }

    public int PuntosUsados { get; set; }

    public string Tienda { get; set; } = null!;

    public DateTime FechaCanje { get; set; }

    [ForeignKey("Idusuario")]
    [JsonIgnore]
    public Usuario? Usuario { get; set; } = null!;
}
