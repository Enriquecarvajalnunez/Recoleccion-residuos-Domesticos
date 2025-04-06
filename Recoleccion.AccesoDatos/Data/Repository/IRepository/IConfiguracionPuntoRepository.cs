using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recoleccion.Models;
using WebApi_Recoleccion_residuos_Domesticos.Models;

namespace Recoleccion.AccesoDatos.Data.Repository.IRepository
{
    public interface IConfiguracionPuntoRepository : IRepository<ConfiguracionPunto>
    {
        void Update(ConfiguracionPunto configuracionPunto);
    }
}
