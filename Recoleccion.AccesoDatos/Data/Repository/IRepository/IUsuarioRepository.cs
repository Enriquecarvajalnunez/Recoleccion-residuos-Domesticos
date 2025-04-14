using ModelsRecolectar;

namespace Recoleccion.AccesoDatos.Data.Repository.IRepository
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        void Update(Usuario usuario);
    }
}
