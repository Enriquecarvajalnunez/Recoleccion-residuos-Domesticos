using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using Recoleccion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recoleccion.AccesoDatos.Data.Repository
{
    internal class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        private readonly RecoleccionResiduosContext _db;

        public UsuarioRepository(RecoleccionResiduosContext db) : base(db)
        {
            _db = db;
        }
        //solo para el metodo de actualización se crea un repositorio adicional
        public void Update(Usuario usuario)
        {
            var objDesdeDb = _db.usuario.FirstOrDefault(s => s.Idusuario == usuario.Idusuario);
            //actualizamos los atributos del objeto, accde a la clase del modelo
            objDesdeDb.Idlocalidad = usuario.Idlocalidad;
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
