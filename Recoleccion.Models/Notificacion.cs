using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsRecolectar;

public class Notificacion
{
    [Key]
    public int IDNotificacion { get; set; }

    [Required]
    [ForeignKey("Usuario")]
    public int IDUsuario { get; set; }

    [Required]
    [StringLength(255)]
    public string Mensaje { get; set; } = null!;

    public DateTime FechaEnvio { get; set; } = DateTime.Now;

    public virtual Usuario Usuario { get; set; } = null!;
}
