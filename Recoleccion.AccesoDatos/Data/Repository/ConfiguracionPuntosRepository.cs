using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using ModelsRecolectar;
using Recoleccion.AccesoDatos.Data;


namespace Recoleccion.AccesoDatos.Data.Repository
{
    public class ConfiguracionPuntosRepository : Repository<ConfiguracionPuntos>, IConfiguracionPuntosRepository
    {
        private readonly RecoleccionResiduosContext _db;
        public ConfiguracionPuntosRepository(RecoleccionResiduosContext db) : base(db)
        {
            _db = db;
        }
  
        public void Update(ConfiguracionPuntos configuracionPuntos)
        {
            var objDesdeDb = _db.ConfiguracionPuntos.FirstOrDefault(s => s.IDConfiguracion == configuracionPuntos.IDConfiguracion);

            if (objDesdeDb == null)
            {
               throw new KeyNotFoundException($"No se encontró la configuración con ID {configuracionPuntos.IDConfiguracion}");
            }
            objDesdeDb.FactorConversion = configuracionPuntos.FactorConversion;
            objDesdeDb.UltimaActualizacion = DateTime.Now;
            _db.SaveChanges(); //Guardamos los cambios en la base de datos
        }
        public new void Add(ConfiguracionPuntos configuracionPuntos)
        {
            _db.ConfiguracionPuntos.Add(configuracionPuntos);
            _db.SaveChanges(); //Guardamos los cambios en la base de datos
        }
    }
}
