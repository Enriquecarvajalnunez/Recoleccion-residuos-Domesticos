using ModelsRecolectar;

namespace Recoleccion.AccesoDatos.Data.Repository.IRepository
{
    public interface IRecolectarRepository : IRepository<Recolectar>
    {
        void Update(Recolectar recoleccion);
    }
}
