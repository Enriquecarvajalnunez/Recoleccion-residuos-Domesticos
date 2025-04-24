using Microsoft.EntityFrameworkCore;
using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using Recoleccion.Models;
using Recoleccion.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recoleccion.AccesoDatos.Data.Repository
{
    
    internal class RecolectarRepository : Repository<Recolectar>, IRecolectarRepository
    {
        private readonly RecoleccionResiduosContext _db;

        public RecolectarRepository(RecoleccionResiduosContext db) : base(db)
        {
            _db = db;            
        }

        public void Update(Recolectar recolectar)
        {
            var objDesdeDb = _db.recolectar.FirstOrDefault(s => s.Idrecoleccion == recolectar.Idrecoleccion );
            if (objDesdeDb != null)
            {
                objDesdeDb.Idusuario = recolectar.Idusuario;
                objDesdeDb.Idempresa = recolectar.Idempresa;
                objDesdeDb.Idresiduo = recolectar.Idresiduo;
                objDesdeDb.FechaRecoleccion = recolectar.FechaRecoleccion;
                objDesdeDb.PesoKg = recolectar.PesoKg;
                objDesdeDb.Estado = recolectar.Estado;

                _db.SaveChanges();
            }
            else
            {
                throw new Exception("No se encontró recolectar" +
                    "" +
                    " para actualizar.");
            }
        }

        public List<RecolectarReporteDto> ObtenerReporteRecolectasDetallado()
        {
            var query = from re in _db.recolectar
                        join r in _db.residuo on re.Idresiduo equals r.Idresiduo
                        join u in _db.usuario on re.Idusuario equals u.Idusuario
                        join l in _db.localidad on u.Idlocalidad equals l.Idlocalidad
                        select new RecolectarReporteDto
                        {
                            IDRecoleccion = re.Idrecoleccion,
                            IDUsuario = u.Idusuario,
                            IDEmpresa = re.Idempresa,
                            IDResiduo = re.Idresiduo,
                            FechaRecoleccion = re.FechaRecoleccion,
                            PesoKg = re.PesoKg ?? 0,
                            Estado = re.Estado,
                            TipoResiduo = r.TipoResiduo,
                            IDLocalidad = u.Idlocalidad ?? 0,
                            Nombre = u.Nombre,
                            Apellidos = u.Apellidos,
                            Telefono = u.Telefono,
                            Email = u.Email,
                            Direccion = u.Direccion,
                            Rol = u.Rol,
                            Localidad = l.Nombre
                        };

            return query.ToList();
        }

    }
}
