using ModelsRecolectar;

namespace Recoleccion.AccesoDatos.Data.Repository.IRepository
{
    public interface IResiduoRepository : IRepository<Residuo>
    {
        void Update(Residuo residuo)
        {
            // Este método no se puede implementar porque los tipos de residuos son fijos y no pueden ser modificados.
            throw new InvalidOperationException("Los tipos de residuos son fijos y no pueden ser modificados");
        }
    }
}
