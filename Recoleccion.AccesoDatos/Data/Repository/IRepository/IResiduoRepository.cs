using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi_Recoleccion_residuos_Domesticos.Models;

namespace Recoleccion.AccesoDatos.Data.Repository.IRepository
{
    public interface IResiduoRepository : IRepository<Residuo>
    {
        void Update(Residuo residuo);
    }
}
