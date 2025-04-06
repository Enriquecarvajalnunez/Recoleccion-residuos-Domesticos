using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using Recoleccion.Models;

namespace Recoleccion.AccesoDatos.Data.Repository
{
    public class NotificacionRepository : Repository<Notificacion>, INotificacionRepository
    {
        private readonly RecoleccionResiduosContext _db;

        public NotificacionRepository(RecoleccionResiduosContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Notificacion notificacion)
        {
            var objDesdeDb = _db.Notificacion.FirstOrDefault(n => n.Idnotificacion == notificacion.Idnotificacion);
            if (objDesdeDb != null)
            {
                objDesdeDb.Mensaje = notificacion.Mensaje;
                objDesdeDb.FechaEnvio = notificacion.FechaEnvio;
                objDesdeDb.Idusuario = notificacion.Idusuario;
                objDesdeDb.Idnotificacion = notificacion.Idnotificacion;
                _db.SaveChanges();
            }
        }
    }
}