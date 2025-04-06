using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recoleccion.AccesoDatos.Data.Repository.IRepository
{
    public interface IContenedorTrabajo : IDisposable
    {
        IUsuarioRepository Usuario { get; }
        ICanjePuntoRepository CanjePunto { get; }
        IConfiguracionPuntoRepository ConfiguracionPunto { get; }
        void Save();                
    }
}
