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

        public void Update(Localidad localidad)
        {
            var objDesdeDb = _db.Localidades.FirstOrDefault(l => l.IDLocalidad == localidad.IDLocalidad);
            if (objDesdeDb == null)
            {
                throw new KeyNotFoundException($"No se encontró la localidad con ID {localidad.IDLocalidad} en la base de datos.");
            }
            objDesdeDb.Nombre = localidad.Nombre;
            _db.SaveChanges();
        }
    }
}