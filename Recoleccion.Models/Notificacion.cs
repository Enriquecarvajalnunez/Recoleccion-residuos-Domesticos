using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Recoleccion.Models;

public class Notificacion
{
    [Key]
    public int Idnotificacion { get; set; }

    public int Idusuario { get; set; }

    public string Mensaje { get; set; } = null!;

    public DateTime FechaEnvio { get; set; }

    [ForeignKey("Idusuario")]
    [JsonIgnore]
    public Usuario? Usuario { get; set; } = null!;

    // public virtual Usuario IdusuarioNavigation { get; set; } = null!;
}
