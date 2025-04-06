using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using Recoleccion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recoleccion.AccesoDatos.Data.Repository
{
    
    internal class PuntoUsuarioRepository : Repository<PuntosUsuario>, IPuntosUsuarioRepository
    {
        private readonly RecoleccionResiduosContext _db;

        public PuntoUsuarioRepository(RecoleccionResiduosContext db) : base(db)
        {
            _db = db;            
        }

        public void Update(PuntosUsuario puntosUsuario)
        {
            var objDesdeDb = _db.puntosUsuario.FirstOrDefault(s => s.Idpuntos == puntosUsuario.Idpuntos);
            if (objDesdeDb != null)
            {
                objDesdeDb.Idusuario = puntosUsuario.Idusuario;
                objDesdeDb.Puntos = puntosUsuario.Puntos;
                objDesdeDb.FechaObtencion = puntosUsuario.FechaObtencion;
                objDesdeDb.Estado = puntosUsuario.Estado;

                _db.SaveChanges();
            }
            else
            {
                throw new Exception("No se encontró puntos de usuario" +
                    "" +
                    " para actualizar.");
            }
        }
    }
}
