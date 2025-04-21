using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using ModelsRecolectar;
using Recoleccion.AccesoDatos.Data;

namespace Recoleccion.AccesoDatos.Data.Repository
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        private readonly RecoleccionResiduosContext _db;

        public UsuarioRepository(RecoleccionResiduosContext db) : base(db)
        {
            _db = db;
        }
        public new void Add(Usuario usuario)
        {
            _db.Usuarios.Add(usuario);
        }
        //solo para el metodo de actualización se crea un repositorio adicional
        public void Update(Usuario usuario)
        {
            var objDesdeDb = _db.Usuarios.FirstOrDefault(s => s.IDUsuario == usuario.IDUsuario);

            if (objDesdeDb == null)
            {
                throw new KeyNotFoundException($"No se encontro un Usuario con ID {usuario.IDUsuario}");
            }

            objDesdeDb.IDLocalidad = usuario.IDLocalidad;
            objDesdeDb.Nombre = usuario.Nombre;
            objDesdeDb.Apellidos = usuario.Apellidos;
            objDesdeDb.Telefono = usuario.Telefono;
            objDesdeDb.Email = usuario.Email;
            objDesdeDb.Direccion = usuario.Direccion;
            objDesdeDb.Rol = usuario.Rol;
            
            /*// Actualizar CanjePuntos
            objDesdeDb.CanjePuntos.Clear();

            foreach (var canje in usuario.CanjePuntos)
            {
                objDesdeDb.CanjePuntos.Add(canje);
            }
            */
            _db.SaveChanges();
        }
    }
}
