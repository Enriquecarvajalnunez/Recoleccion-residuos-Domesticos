using System;
using System.Collections.Generic;

namespace WebApi_Recoleccion_residuos_Domesticos.Models;

public partial class ConfiguracionPunto
{
    public int Idconfiguracion { get; set; }

    public decimal FactorConversion { get; set; }

    public DateTime? UltimaActualizacion { get; set; }
}
