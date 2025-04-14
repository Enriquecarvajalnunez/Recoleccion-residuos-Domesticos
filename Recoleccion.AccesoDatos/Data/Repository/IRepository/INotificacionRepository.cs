using ModelsRecolectar;

namespace Recoleccion.AccesoDatos.Data.Repository.IRepository
{
    public interface INotificacionRepository : IRepository<Notificacion>
    {
        void Update(Notificacion notificacion);
    }
}
