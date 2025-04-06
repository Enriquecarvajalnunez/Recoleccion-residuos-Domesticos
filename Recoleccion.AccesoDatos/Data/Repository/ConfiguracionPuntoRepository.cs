using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using Recoleccion.Models;
using WebApi_Recoleccion_residuos_Domesticos.Models;


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
            var objDesdeDb = _db.ConfiguracionPunto.FirstOrDefault(s => s.Idconfiguracion == configuracionPunto.Idconfiguracion);

            if (objDesdeDb != null)
            {
                //Actualizamos las propiedades
                objDesdeDb.FactorConversion = configuracionPunto.FactorConversion;
                objDesdeDb.UltimaActualizacion = DateTime.Now; //Actualizamos la fecha de la ultima actualizacion
                objDesdeDb.Idconfiguracion = configuracionPunto.Idconfiguracion;
            }
            _db.SaveChanges(); //Guardamos los cambios en la base de datos
        }
        public void Add(ConfiguracionPunto configuracionPunto)
        {
            _db.ConfiguracionPunto.Add(configuracionPunto);
        }
    }
}
