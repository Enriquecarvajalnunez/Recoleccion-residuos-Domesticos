using ModelsRecolectar;
using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using Recoleccion.AccesoDatos.Data;

namespace Recoleccion.AccesoDatos.Data.Repository
{
    public class EmpresaRecolectoraRepository : Repository<EmpresaRecolectora>, IEmpresaRecolectoraRepository
    {
        private readonly RecoleccionResiduosContext _db;
        public EmpresaRecolectoraRepository(RecoleccionResiduosContext db) : base(db)
        {
            _db = db;
        }
        public void Update(EmpresaRecolectora empresaRecolectora)
        {
            var objDesdeDb = _db.EmpresaRecolectoras.FirstOrDefault(s => s.IDEmpresa == empresaRecolectora.IDEmpresa);
            if (objDesdeDb != null)
            {
                objDesdeDb.Nombre = empresaRecolectora.Nombre;
                objDesdeDb.TipoResiduo = empresaRecolectora.TipoResiduo;
                _db.SaveChanges();
            }
        }
    }
}
