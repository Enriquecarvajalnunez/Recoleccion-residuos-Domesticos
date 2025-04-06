using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Recoleccion.Models;

public class PuntosUsuario
{
    [Key]
    public int Idpuntos { get; set; }

    public int Idusuario { get; set; }

    public int? Puntos { get; set; }

    public DateTime FechaObtencion { get; set; }

    public string? Estado { get; set; }

    [ForeignKey("Idusuario")]
    [JsonIgnore]
    public Usuario? usuario { get; set; } = null!;

    // public virtual Usuario IdusuarioNavigation { get; set; } = null!;
}
