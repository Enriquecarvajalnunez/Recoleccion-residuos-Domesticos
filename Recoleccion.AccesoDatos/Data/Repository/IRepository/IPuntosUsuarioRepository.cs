using ModelsRecolectar;

namespace Recoleccion.AccesoDatos.Data.Repository.IRepository
{
    public interface IPuntosUsuarioRepository : IRepository<PuntosUsuario>
    {
        void Update(PuntosUsuario puntosUsuario);
        IEnumerable<PuntosUsuario> ObtenerPorUsuario(int idUsuario);
        int ObtenerTotalAcumulado(int idUsuario);
    }
}
