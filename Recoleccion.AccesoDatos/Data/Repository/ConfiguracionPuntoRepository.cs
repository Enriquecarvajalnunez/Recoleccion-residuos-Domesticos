using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using Recoleccion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recoleccion.AccesoDatos.Data.Repository
{
    
    internal class ConfiguracionPuntoRepository : Repository<ConfiguracionPunto>, IConfiguracionPuntoRepository
    {
        private readonly RecoleccionResiduosContext _db;

        public ConfiguracionPuntoRepository(RecoleccionResiduosContext db) : base(db)
        {
            _db = db;            
        }

        public void Update(ConfiguracionPunto configuracionPunto)
        {
            var objDesdeDb = _db.configuracionPuntos.FirstOrDefault(s => s.Idconfiguracion == configuracionPunto.Idconfiguracion);

            objDesdeDb.FactorConversion = configuracionPunto.FactorConversion;
            objDesdeDb.UltimaActualizacion = configuracionPunto.UltimaActualizacion;
            
            _db.SaveChanges();
        }
    }
}
