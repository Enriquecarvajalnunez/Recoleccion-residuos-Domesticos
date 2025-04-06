using Recoleccion.AccesoDatos.Data.Repository.IRepository;
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
        }
        public IUsuarioRepository Usuario { get; private set; }
        public ICanjePuntoRepository CanjePunto { get; private set; }
        public IConfiguracionPuntoRepository ConfiguracionPunto { get; private set; }

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
