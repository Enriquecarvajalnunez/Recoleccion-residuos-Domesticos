using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recoleccion.Models.Dtos
{
    public class RecolectarReporteDto
    {
        public int IDRecoleccion { get; set; }
        public int IDUsuario { get; set; }
        public int IDEmpresa { get; set; }
        public int IDResiduo { get; set; }
        public DateTime FechaRecoleccion { get; set; }
        public decimal PesoKg { get; set; }
        public string? Estado { get; set; } = null!;
        public string TipoResiduo { get; set; } = null!;
        public int IDLocalidad { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public string? Direccion { get; set; } = null!;
        public string? Rol { get; set; } = null!;
        public string Localidad { get; set; } = null!;
    }
}
