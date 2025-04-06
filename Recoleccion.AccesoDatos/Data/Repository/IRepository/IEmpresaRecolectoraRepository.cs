using Recoleccion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recoleccion.AccesoDatos.Data.Repository.IRepository
{
    public interface IEmpresaRecolectoraRepository: IRepository<EmpresaRecolectora>
    {
        void Update(EmpresaRecolectora empresaRecolectora);
    }
}
