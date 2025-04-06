using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using Recoleccion.Models;

namespace Recoleccion.AccesoDatos.Data.Repository
{
    public class PuntosUsuarioRepository : Repository<PuntosUsuario>, IPuntosUsuarioRepository
    {
        private readonly RecoleccionResiduosContext _db;
        public PuntosUsuarioRepository(RecoleccionResiduosContext db) : base(db)
        {
            _db = db;
        }
        //Agregamos metodo para actualizar el registro de PuntosUsuario
        public void Update(PuntosUsuario puntosUsuario)
        {
            var objDesdeDb = _db.PuntosUsuario.FirstOrDefault(s => s.Idpuntos == puntosUsuario.Idpuntos);
            if (objDesdeDb != null)
            {
                //Actualizamos las propiedades
                objDesdeDb.Puntos = puntosUsuario.Puntos;
                objDesdeDb.FechaObtencion = puntosUsuario.FechaObtencion;
                objDesdeDb.Estado = puntosUsuario.Estado;
                objDesdeDb.Idusuario = puntosUsuario.Idusuario;
                objDesdeDb.Idpuntos = puntosUsuario.Idpuntos;
            }
            _db.SaveChanges(); //Guardamos los cambios en la base de datos
        }
    }
}