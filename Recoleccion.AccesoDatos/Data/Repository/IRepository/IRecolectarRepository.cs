using ModelsRecolectar;

namespace Recoleccion.AccesoDatos.Data.Repository.IRepository
{
    public interface IRecolectarRepository : IRepository<Recolectar>
    {
        void Update(Recolectar recoleccion);
        // Definimos un método para obtener todas las solicitudes de recolección.
        // Su implementacion se hara en la clase que implemente esta interfaz.
        IEnumerable<Recolectar> GetAllSolicitudRecoleccion();
    }
 }
