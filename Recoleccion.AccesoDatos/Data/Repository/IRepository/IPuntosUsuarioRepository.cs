using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recoleccion.Models;

namespace Recoleccion.AccesoDatos.Data.Repository.IRepository
{
    public interface IPuntosUsuarioRepository : IRepository<PuntosUsuario>
    {
        void Update(PuntosUsuario puntosUsuario);
    }
}
