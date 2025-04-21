using ModelsRecolectar;

namespace Recoleccion.AccesoDatos.Data.Repository.IRepository
{
    public interface IContenedorTrabajo : IDisposable
    {
        IEmpresaRecolectoraRepository EmpresaRecolectora { get; }
        ILocalidadRepository Localidad { get; }
        IResiduoRepository Residuo { get; }
        IConfiguracionPuntosRepository ConfiguracionPuntos { get; }
        IUsuarioRepository Usuario { get; }                   
        IPuntosUsuarioRepository PuntosUsuario { get; }            
        INotificacionRepository Notificacion { get; }
        ICanjePuntosRepository CanjePuntos { get; }
        IRecolectarRepository Recolectar { get; }
        ISubTipoResiduoRepository SubTipoResiduo { get; }

        IEnumerable<Localidad> ObtenerTodasLasLocalidades();
        IEnumerable<SubTipoResiduo> ObtenerTodosLosSubTipoResiduos();
        void Save();
    }
}
