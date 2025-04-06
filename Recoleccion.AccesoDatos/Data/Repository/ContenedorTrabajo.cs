using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using Recoleccion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recoleccion.AccesoDatos.Data.Repository
{
    public class ContenedorTrabajo : IContenedorTrabajo
    {
        private readonly RecoleccionResiduosContext _db;
        public ContenedorTrabajo(RecoleccionResiduosContext db)
        {
            _db = db;
            //Aqui se llaman los demas repositorios para tenerlos encapsulados
            Usuario = new UsuarioRepository(_db);
            ConfiguracionPunto = new ConfiguracionPuntoRepository(_db);
            PuntosUsuario = new PuntosUsuarioRepository(_db);
            Residuo = new ResiduoRepository(_db);
            Localidad = new LocalidadRepository(_db);
            Notificacion = new NotificacionRepository(_db);
        }
        public IUsuarioRepository Usuario { get; private set; }
        public IConfiguracionPuntoRepository ConfiguracionPunto { get; private set; }
        public IPuntosUsuarioRepository PuntosUsuario { get; private set; }
        public IResiduoRepository Residuo { get; private set; }
        public ILocalidadRepository Localidad { get; private set; }
        public INotificacionRepository Notificacion { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
