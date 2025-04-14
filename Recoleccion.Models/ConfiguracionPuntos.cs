using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModelsRecolectar;

public partial class ConfiguracionPuntos
{
    [Key]
    public int IDConfiguracion { get; set; }

    private decimal _factorConversion;

    [Required]
    [Range(0.01, Double.MaxValue)]
    public decimal FactorConversion 
    { 
        get => _factorConversion;
        set => _factorConversion = Math.Round(value, 2); 
    }
    public DateTime UltimaActualizacion { get; set; } = DateTime.Now;
}
