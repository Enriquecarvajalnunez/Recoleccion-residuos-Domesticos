using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using ModelsRecolectar;

namespace Recoleccion.AccesoDatos.Data.Repository
{
    public class ContenedorTrabajo : IContenedorTrabajo
    {
        private readonly RecoleccionResiduosContext _db;
        public ContenedorTrabajo(RecoleccionResiduosContext db)
        {
            _db = db;
            //Aqui se llaman los demas repositorios para tenerlos encapsulados
            EmpresaRecolectora = new EmpresaRecolectoraRepository(_db);
            Localidad = new LocalidadRepository(_db);
            Residuo = new ResiduoRepository(_db);
            ConfiguracionPuntos = new ConfiguracionPuntosRepository(_db);
            Usuario = new UsuarioRepository(_db);
            PuntosUsuario = new PuntosUsuarioRepository(_db);
            Notificacion = new NotificacionRepository(_db);
            CanjePuntos = new CanjePuntosRepository(_db);
            Recolectar = new RecolectarRepository(_db);

        }
        public IEmpresaRecolectoraRepository EmpresaRecolectora { get; private set; }
        public ILocalidadRepository Localidad { get; private set; }
        public IResiduoRepository Residuo { get; private set; }
        public IConfiguracionPuntosRepository ConfiguracionPuntos { get; private set; }
        public IUsuarioRepository Usuario { get; private set; }
        public IPuntosUsuarioRepository PuntosUsuario { get; private set; } 
        public INotificacionRepository Notificacion { get; private set; }
        public ICanjePuntosRepository CanjePuntos { get; private set; }
        public IRecolectarRepository Recolectar { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            try
            {
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                // Manejamos excepciones
                throw new Exception("Ocurrio un error al guardar los cambios", ex);
            }
        }
    }
}
