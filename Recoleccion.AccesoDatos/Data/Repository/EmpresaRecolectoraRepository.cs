using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using Recoleccion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recoleccion.AccesoDatos.Data.Repository
{
    
    internal class EmpresaRecolectoraRepository : Repository<EmpresaRecolectora>, IEmpresaRecolectoraRepository
    {
        private readonly RecoleccionResiduosContext _db;

        public EmpresaRecolectoraRepository(RecoleccionResiduosContext db) : base(db)
        {
            _db = db;            
        }

        public void Update(EmpresaRecolectora empresaRecolectora)
        {
            var objDesdeDb = _db.empresaRecolectora.FirstOrDefault(s => s.Idempresa == empresaRecolectora.Idempresa);

            objDesdeDb.Idempresa = empresaRecolectora.Idempresa;
            objDesdeDb.Nombre = empresaRecolectora.Nombre;
            objDesdeDb.TipoResiduo = empresaRecolectora.TipoResiduo;                   
            
            _db.SaveChanges();
        }
    }
}
