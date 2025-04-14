using ModelsRecolectar;

namespace Recoleccion.AccesoDatos.Data.Repository.IRepository
{
    public interface IConfiguracionPuntosRepository : IRepository<ConfiguracionPuntos>
    {
        void Update(ConfiguracionPuntos configuracionPuntos);
    }
}
