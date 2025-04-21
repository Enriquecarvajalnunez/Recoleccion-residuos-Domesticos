using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using ModelsRecolectar;
using Recoleccion.AccesoDatos.Data;
using System;
using System.Collections.Generic;
using System.Linq;



namespace Recoleccion.AccesoDatos.Data.Repository
{
    public class ResiduoRepository : Repository<Residuo>, IResiduoRepository
    {
        private readonly RecoleccionResiduosContext _db;
        public ResiduoRepository(RecoleccionResiduosContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<TipoResiduoEnum> ObtenerTodosLosTiposResiduos()
        {
            // Este método obtiene todos los tipos de residuo.
            return Enum.GetValues(typeof(TipoResiduoEnum)).Cast<TipoResiduoEnum>();
        }
    }
}
