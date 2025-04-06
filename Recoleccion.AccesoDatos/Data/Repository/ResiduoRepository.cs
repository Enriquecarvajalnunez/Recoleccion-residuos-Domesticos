using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using Recoleccion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recoleccion.AccesoDatos.Data.Repository
{
    
    internal class ResiduoRepository : Repository<Residuo>, IResiduoRepository
    {
        private readonly RecoleccionResiduosContext _db;

        public ResiduoRepository(RecoleccionResiduosContext db) : base(db)
        {
            _db = db;            
        }

        public void Update(Residuo residuo)
        {
            var objDesdeDb = _db.residuo.FirstOrDefault(s => s.Idresiduo == residuo.Idresiduo);
            if (objDesdeDb != null)
            {
                objDesdeDb.TipoResiduo = residuo.TipoResiduo;
                _db.SaveChanges();
            }
            else
            {
                throw new Exception("No se encontró el residuo" +
                    "" +
                    " para actualizar.");
            }
        }
    }
}
