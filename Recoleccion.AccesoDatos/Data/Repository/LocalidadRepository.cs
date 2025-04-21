using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using ModelsRecolectar;
using Recoleccion.AccesoDatos.Data;

namespace Recoleccion.AccesoDatos.Data.Repository
{
    public class LocalidadRepository : Repository<Localidad>, ILocalidadRepository
    {
        private readonly RecoleccionResiduosContext _db;

        public LocalidadRepository(RecoleccionResiduosContext db) : base(db)
        {
            _db = db;
        }
        
        public IEnumerable<Localidad> ObtenerTodasLasLocalidades()
        {
            // Este método obtiene todas las localidades de la base de datos.
            return _db.Localidades.ToList();
        }
    }
}