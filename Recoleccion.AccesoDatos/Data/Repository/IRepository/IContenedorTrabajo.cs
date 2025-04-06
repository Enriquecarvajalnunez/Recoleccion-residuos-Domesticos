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
        IConfiguracionPuntoRepository ConfiguracionPunto { get; }
        IPuntosUsuarioRepository PuntosUsuario { get; }
        IResiduoRepository Residuo { get; }
        ILocalidadRepository Localidad { get; }
        INotificacionRepository Notificacion { get; }

        void Save();
    }
}
