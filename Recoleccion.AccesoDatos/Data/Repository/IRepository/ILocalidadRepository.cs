using ModelsRecolectar;

namespace Recoleccion.AccesoDatos.Data.Repository.IRepository
{
    public interface ILocalidadRepository : IRepository<Localidad>
    {
        void Update(Localidad localidad);
    }
}
