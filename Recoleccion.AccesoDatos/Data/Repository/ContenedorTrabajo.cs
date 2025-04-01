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
        }
        public IUsuarioRepository Usuario { get; private set; }

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
