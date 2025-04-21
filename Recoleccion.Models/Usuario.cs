using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ModelsRecolectar;

public class Usuario

{
    [Key]
    public int IDUsuario { get; set; }

    [ForeignKey(nameof(Localidad))]
    public int IDLocalidad { get; set; }

    [JsonIgnore]
    public Localidad? Localidad { get; set; }

    [Required]
    [StringLength(100)]
    public string? Nombre { get; set; }

    [Required]
    [StringLength(100)]
    public string? Apellidos { get; set; }

    [Required]
    [StringLength(15)]
    public string? Telefono { get; set; }

    [Required]
    [StringLength(100)]
    public string? Email { get; set; }

    [Required]
    [StringLength(100)]
    public string? Direccion { get; set; }

    [Required]
    public string? Rol { get; set; }
}
