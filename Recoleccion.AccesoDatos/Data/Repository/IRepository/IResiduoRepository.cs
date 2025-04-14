using ModelsRecolectar;

namespace Recoleccion.AccesoDatos.Data.Repository.IRepository
{
    public interface IResiduoRepository : IRepository<Residuo>
    {
        void Update(Residuo residuo);
    }
}
