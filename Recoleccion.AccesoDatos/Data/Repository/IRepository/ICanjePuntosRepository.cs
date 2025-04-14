using ModelsRecolectar;

namespace Recoleccion.AccesoDatos.Data.Repository.IRepository
{
    public interface ICanjePuntosRepository : IRepository<CanjePuntos>
    {
        void Update(CanjePuntos canjePuntos);
    }
}
