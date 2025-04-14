using ModelsRecolectar;

namespace Recoleccion.AccesoDatos.Data.Repository.IRepository
{
    public interface IPuntosUsuarioRepository : IRepository<PuntosUsuario>
    {
        void Update(PuntosUsuario puntosUsuario);
    }
}
