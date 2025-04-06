using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using Recoleccion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recoleccion.AccesoDatos.Data.Repository
{
    
    internal class NotificacionRespository : Repository<Notificacion>, INotificacionRepository
    {
        private readonly RecoleccionResiduosContext _db;

        public NotificacionRespository(RecoleccionResiduosContext db) : base(db)
        {
            _db = db;            
        }

        public void Update(Notificacion notificacion)
        {
            var objDesdeDb = _db.notificacion.FirstOrDefault(s => s.Idnotificacion == notificacion.Idnotificacion);
            if (objDesdeDb != null) // Verificamos que el registro exista
            {
                objDesdeDb.Idusuario = notificacion.Idusuario;
                objDesdeDb.Mensaje = notificacion.Mensaje;
                objDesdeDb.FechaEnvio = notificacion.FechaEnvio;
                _db.SaveChanges(); // Guardamos cambios en la BD
            }
            else
            {
                throw new Exception("No se encontró la notificación" +
                    "" +
                    " para actualizar.");
            }          
        }    
    }
}
