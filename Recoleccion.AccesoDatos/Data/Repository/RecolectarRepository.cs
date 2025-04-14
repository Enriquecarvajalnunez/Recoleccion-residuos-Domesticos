using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using ModelsRecolectar;
using Recoleccion.AccesoDatos.Data;

namespace Recoleccion.AccesoDatos.Data.Repository
{
    public class RecolectarRepository : Repository<Recolectar>, IRecolectarRepository
    {
        private readonly RecoleccionResiduosContext _db;
        public RecolectarRepository(RecoleccionResiduosContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Recolectar recolectar)
        {
            var objDesdeDb = _db.Recolecciones.SingleOrDefault(s => s.IDRecoleccion == recolectar.IDRecoleccion);
            if (objDesdeDb == null)
            {
                throw new KeyNotFoundException($"No se encontro el ID de Recolectar {recolectar.IDRecoleccion}");
            }
            objDesdeDb.IDUsuario = recolectar.IDUsuario;
            objDesdeDb.IDEmpresa = recolectar.IDEmpresa;
            objDesdeDb.IDResiduo = recolectar.IDResiduo;
            objDesdeDb.FechaRecoleccion = recolectar.FechaRecoleccion;
            objDesdeDb.PesoKg = recolectar.PesoKg;
            objDesdeDb.Estado = recolectar.Estado;
            _db.SaveChanges();
        }
    }
}
