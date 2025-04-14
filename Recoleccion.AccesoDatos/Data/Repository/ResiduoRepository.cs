using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using ModelsRecolectar;
using Recoleccion.AccesoDatos.Data;


namespace Recoleccion.AccesoDatos.Data.Repository
{
    public class ResiduoRepository : Repository<Residuo>, IResiduoRepository
    {
        private readonly RecoleccionResiduosContext _db;
        public ResiduoRepository(RecoleccionResiduosContext db) : base(db)
        {
            _db = db;
        }
        //solo para el metodo de actualización se crea un repositorio adicional
        public void Update(Residuo residuo)
        {
            var objDesdeDb = _db.Residuos.FirstOrDefault(s => s.IDResiduo == residuo.IDResiduo);
            if (objDesdeDb == null)
            {
                throw new KeyNotFoundException($"No se encontro un Residuo con ID {residuo.IDResiduo}");
            }
            objDesdeDb.TipoResiduo = residuo.TipoResiduo;
            _db.SaveChanges();
        }
    }
}
