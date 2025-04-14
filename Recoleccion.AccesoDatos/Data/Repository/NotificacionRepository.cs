using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using ModelsRecolectar;
using Recoleccion.AccesoDatos.Data;

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
            var objDesdeDb = _db.Notificaciones.FirstOrDefault(n => n.IDNotificacion == notificacion.IDNotificacion);
            if (objDesdeDb == null)
            {
                throw new KeyNotFoundException($"No se encontro la notificacion con el ID {notificacion.IDNotificacion}. Asegurate de que el ID es correcto y que la notificacion existe en la base de datos");
            }
            objDesdeDb.Mensaje = notificacion.Mensaje;
            objDesdeDb.FechaEnvio = notificacion.FechaEnvio;
            objDesdeDb.IDUsuario = notificacion.IDUsuario;
            _db.SaveChanges();
        }
    }
}