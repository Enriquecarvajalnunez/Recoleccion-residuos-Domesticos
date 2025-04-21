using ModelsRecolectar;

namespace Recoleccion.AccesoDatos.Data.Repository.IRepository
{
    public interface ILocalidadRepository : IRepository<Localidad>
    {
        void Update(Localidad localidad)
        {
            // Este método no se puede implementar porque las localidades son fijas y no pueden ser modificadas.
            throw new InvalidOperationException("Las localidades son fijas y no pueden ser modificadas");
        }
    }
}
