using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using Recoleccion.Models;
using WebApi_Recoleccion_residuos_Domesticos.Models;

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
            var objDesdeDb = _db.Localidad.FirstOrDefault(l => l.Idlocalidad == localidad.Idlocalidad);
            if (objDesdeDb != null)
            {
                objDesdeDb.Nombre = localidad.Nombre;
                objDesdeDb.Idlocalidad = localidad.Idlocalidad;
                _db.SaveChanges();
            }
        }
    }
}