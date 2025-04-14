using ModelsRecolectar;

namespace Recoleccion.AccesoDatos.Data.Repository.IRepository
{
    public interface IEmpresaRecolectoraRepository : IRepository<EmpresaRecolectora>
    {
        void Update(EmpresaRecolectora empresaRecolectora);
    }
}
