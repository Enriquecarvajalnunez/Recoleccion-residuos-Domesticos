using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsRecolectar;

namespace ModelsRecolectar;

public class SubTipoResiduo
{
    [Key]
    public int IDSubTipoResiduo { get; set; }
    public string? Nombre { get; set; }

    // Relación con TipoResiduo
    public int IDResiduo { get; set; }
    public Residuo? Residuo { get; set; }
}


