using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Recoleccion.Models;

public partial class ConfiguracionPunto
{
    [Key]
    public int Idconfiguracion { get; set; }

    public decimal FactorConversion { get; set; }

    public DateTime? UltimaActualizacion { get; set; }
}
