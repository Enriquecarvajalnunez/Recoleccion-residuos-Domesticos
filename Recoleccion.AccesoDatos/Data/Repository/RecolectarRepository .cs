using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using Recoleccion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recoleccion.AccesoDatos.Data.Repository
{
    
    internal class RecolectarRepository : Repository<Recolectar>, IRecolectarRepository
    {
        private readonly RecoleccionResiduosContext _db;

        public RecolectarRepository(RecoleccionResiduosContext db) : base(db)
        {
            _db = db;            
        }

        public void Update(Recolectar recolectar)
        {
            var objDesdeDb = _db.recolectar.FirstOrDefault(s => s.Idrecoleccion == recolectar.Idrecoleccion );
            if (objDesdeDb != null)
            {
                objDesdeDb.Idusuario = recolectar.Idusuario;
                objDesdeDb.Idempresa = recolectar.Idempresa;
                objDesdeDb.Idresiduo = recolectar.Idresiduo;
                objDesdeDb.FechaRecoleccion = recolectar.FechaRecoleccion;
                objDesdeDb.PesoKg = recolectar.PesoKg;
                objDesdeDb.Estado = recolectar.Estado;

                _db.SaveChanges();
            }
            else
            {
                throw new Exception("No se encontró recolectar" +
                    "" +
                    " para actualizar.");
            }
        }
    }
}
