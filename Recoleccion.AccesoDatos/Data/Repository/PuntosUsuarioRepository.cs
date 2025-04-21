using Recoleccion.AccesoDatos.Data;
using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using ModelsRecolectar;

namespace Recoleccion.AccesoDatos.Data.Repository
{
    public class PuntosUsuarioRepository : Repository<PuntosUsuario>, IPuntosUsuarioRepository
    {
        private readonly RecoleccionResiduosContext _db;
        public PuntosUsuarioRepository(RecoleccionResiduosContext db) : base(db)
        {
            _db = db;
        }

        // Agregamos metodo para obtener todos los puntos de usuario.
        public IEnumerable<PuntosUsuario> ObtenerPorUsuario(int idUsuario)
        {
            //Obtenemos los puntos de usuario por ID de usuario
            return _db.PuntosUsuarios
                .Where(p => p.IDUsuario == idUsuario).
                ToList();
        }

        // Agregamos metodo para obtener el total acumulado de puntos por ID de usuario.
        public int ObtenerTotalAcumulado (int idUsuario)
        {
            //Obtenemos el total acumulado de puntos por ID de usuario
            return _db.PuntosUsuarios
                .Where(p => p.IDUsuario == idUsuario && p.Estado == EstadoPuntosEnum.Acumulado)
                .Sum(p => p.Puntos);
        }

        //Agregamos metodo para actualizar el registro de PuntosUsuario
        public void Update(PuntosUsuario puntosUsuario)
        {
            var objDesdeDb = _db.PuntosUsuarios.FirstOrDefault(s => s.IDPuntos == puntosUsuario.IDPuntos);
            if (objDesdeDb == null)
            {
                throw new KeyNotFoundException($"No se encontró el registro de PuntosUsuario con ID {puntosUsuario.IDPuntos}");
            }
            //Actualizamos las propiedades
            objDesdeDb.Puntos = puntosUsuario.Puntos;
            objDesdeDb.FechaObtencion = puntosUsuario.FechaObtencion;
            objDesdeDb.Estado = puntosUsuario.Estado;
            objDesdeDb.IDUsuario = puntosUsuario.IDUsuario;
            //Guardamos los cambios en la base de datos
            _db.SaveChanges(); 
        }
    }
}