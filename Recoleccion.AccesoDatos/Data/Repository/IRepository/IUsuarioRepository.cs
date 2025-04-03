using Recoleccion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recoleccion.AccesoDatos.Data.Repository.IRepository
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        void Update(Usuario usuario);
    }
}
