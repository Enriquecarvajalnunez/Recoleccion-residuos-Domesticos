using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsRecolectar;
using Recoleccion.AccesoDatos.Data.Repository.IRepository;

namespace Recoleccion.AccesoDatos.Data.Repository
{
    public class SubTipoResiduoRepository : Repository<SubTipoResiduo>, ISubTipoResiduoRepository
    {
        private readonly RecoleccionResiduosContext _db;
        public SubTipoResiduoRepository(RecoleccionResiduosContext db) : base(db)
        {
            _db = db;
        }
        public IEnumerable<SubTipoResiduo> ObtenerTodosLosSubTipoResiduos()
        {
            return _db.SubTipoResiduos.ToList();
        }

        public void Update(SubTipoResiduo subTipoResiduo)
        {
            throw new NotImplementedException();
        }
    }
}
