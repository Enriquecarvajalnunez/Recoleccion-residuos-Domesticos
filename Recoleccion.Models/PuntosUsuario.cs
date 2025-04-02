using System;
using System.Collections.Generic;

namespace Recoleccion.Models;

public class PuntosUsuario
{
    public int Idpuntos { get; set; }

    public int Idusuario { get; set; }

    public int? Puntos { get; set; }

    public DateTime FechaObtencion { get; set; }

    public string? Estado { get; set; }

   // public virtual Usuario IdusuarioNavigation { get; set; } = null!;
}
