using System;
using System.Collections.Generic;

namespace WebApi_Recoleccion_residuos_Domesticos.Models;

public partial class Recoleccion
{
    public int Idrecoleccion { get; set; }

    public int Idusuario { get; set; }

    public int Idempresa { get; set; }

    public int Idresiduo { get; set; }

    public DateTime FechaRecoleccion { get; set; }

    public decimal? PesoKg { get; set; }

    public string? Estado { get; set; }

    public virtual EmpresaRecolectora IdempresaNavigation { get; set; } = null!;

    public virtual Residuo IdresiduoNavigation { get; set; } = null!;

   // public virtual Usuario IdusuarioNavigation { get; set; } = null!;
}
