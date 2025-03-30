using System;
using System.Collections.Generic;

namespace WebApi_Recoleccion_residuos_Domesticos.Models;

public partial class CanjePunto
{
    public int Idcanje { get; set; }

    public int Idusuario { get; set; }

    public int PuntosUsados { get; set; }

    public string Tienda { get; set; } = null!;

    public DateTime FechaCanje { get; set; }

    public virtual Usuario IdusuarioNavigation { get; set; } = null!;
}
