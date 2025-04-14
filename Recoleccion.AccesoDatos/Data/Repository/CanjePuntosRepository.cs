using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using ModelsRecolectar;
using Recoleccion.AccesoDatos.Data;

namespace Recoleccion.AccesoDatos.Data.Repository
{
    public class CanjePuntosRepository : Repository<CanjePuntos>, ICanjePuntosRepository
    {
        private readonly RecoleccionResiduosContext _db;
        public CanjePuntosRepository(RecoleccionResiduosContext db) : base(db)
        {
            _db = db;
        }
        public void Update(CanjePuntos canjePuntos)
        {
            var objDesdeDb = _db.CanjePuntos.SingleOrDefault(s => s.IDCanje == canjePuntos.IDCanje);
            if (objDesdeDb == null)
            {
                throw new KeyNotFoundException($"No se encontro un Canje de Puntos con ID {canjePuntos.IDCanje}");
            }
            objDesdeDb.IDUsuario = canjePuntos.IDUsuario;
            objDesdeDb.PuntosUsados = canjePuntos.PuntosUsados;
            objDesdeDb.Tienda = canjePuntos.Tienda;
            objDesdeDb.FechaCanje = DateTime.Now;
            _db.SaveChanges();
        }
    }
}