﻿using System;
using System.Collections.Generic;

namespace WebApi_Recoleccion_residuos_Domesticos.Models;

public partial class Notificacion
{
    public int Idnotificacion { get; set; }

    public int Idusuario { get; set; }

    public string Mensaje { get; set; } = null!;

    public DateTime FechaEnvio { get; set; }

   // public virtual Usuario IdusuarioNavigation { get; set; } = null!;
}
