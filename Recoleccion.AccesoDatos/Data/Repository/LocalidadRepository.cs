using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using Recoleccion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recoleccion.AccesoDatos.Data.Repository
{
    
    internal class LocalidadRespository : Repository<Localidad>, IlocalidadRespository
    {
        private readonly RecoleccionResiduosContext _db;

        public LocalidadRespository(RecoleccionResiduosContext db) : base(db)
        {
            _db = db;            
        }

        public void Update(Localidad localidad)
        {
            var objDesdeDb = _db.localidad.FirstOrDefault(s => s.Idlocalidad == localidad.Idlocalidad);
            if (objDesdeDb != null) // Verificamos que el registro exista
            {
                objDesdeDb.Nombre = localidad.Nombre;
                _db.SaveChanges(); // Guardamos cambios en la BD
            }
            else
            {
                throw new Exception("No se encontró la localidad para actualizar.");
            }          
        }
    }
}
