using Recoleccion.Models;
using Recoleccion.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Recoleccion.AccesoDatos.Data.Repository.IRepository
{
    public interface IRecolectarRepository : IRepository<Recolectar>
    {
        void Update(Recolectar recolectar);
        List<RecolectarReporteDto> ObtenerReporteRecolectasDetallado();
        List<UsuariosPuntosReporteDto> ObtenerReporteUsuariosPuntos();

    }
}
