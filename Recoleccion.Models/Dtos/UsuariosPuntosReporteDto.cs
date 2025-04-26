using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recoleccion.Models.Dtos
{
    public class UsuariosPuntosReporteDto
    {
        public string EstadoRecoleccion { get; set; } = null!;
        public DateTime FechaRecoleccion { get; set; }
        public decimal PesoKg { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public int PuntosGanados { get; set; }
        public string EstadoPuntos { get; set; } = null!;
    }
}
