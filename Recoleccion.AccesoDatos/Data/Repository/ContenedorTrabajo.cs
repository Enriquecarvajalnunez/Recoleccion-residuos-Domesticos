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
            CanjePunto = new CanjePuntoRepository(_db);
            ConfiguracionPunto = new ConfiguracionPuntoRepository(db);
            EmpresaRecolectora = new EmpresaRecolectoraRepository(_db);
            Localidad = new LocalidadRespository(_db);
            Notificacion = new NotificacionRespository(db);
        }
        public IUsuarioRepository Usuario { get; private set; }
        public ICanjePuntoRepository CanjePunto { get; private set; }
        public IConfiguracionPuntoRepository ConfiguracionPunto { get; private set; }
        public IEmpresaRecolectoraRepository EmpresaRecolectora { get; private set; }
        public IlocalidadRespository Localidad { get; private set; }

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
