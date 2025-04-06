using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using Recoleccion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recoleccion.AccesoDatos.Data.Repository
{
    
    internal class CanjePuntoRepository : Repository<CanjePunto>, ICanjePuntoRepository
    {
        private readonly RecoleccionResiduosContext _db;

        public CanjePuntoRepository(RecoleccionResiduosContext db) : base(db)
        {
            _db = db;            
        }

        public void Update(CanjePunto canjePunto)
        {
            var objDesdeDb = _db.canjePuntos.FirstOrDefault(s => s.Idcanje == canjePunto.Idcanje);

            objDesdeDb.Idusuario = canjePunto.Idusuario;
            objDesdeDb.PuntosUsados = canjePunto.PuntosUsados;
            objDesdeDb.Tienda = canjePunto.Tienda;
            objDesdeDb.FechaCanje = canjePunto.FechaCanje;            
            
            _db.SaveChanges();
        }
    }
}
