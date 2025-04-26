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

        //reportes
        public List<RecolectarReporteDto> ObtenerReporteRecolectasDetallado()
        {
            var totalesPorTipo = _db.recolectar
               .Join(_db.residuo,
                     re => re.Idresiduo,
                     r => r.Idresiduo,
                     (re, r) => new { re.PesoKg, r.TipoResiduo })
               .GroupBy(x => x.TipoResiduo)
               .Select(g => new
               {
                   TipoResiduo = g.Key,
                   Total = g.Sum(x => x.PesoKg ?? 0)
               })
               .ToDictionary(x => x.TipoResiduo, x => x.Total);  // Usamos ToDictionary para acceso rápido

            var query = from re in _db.recolectar
                        join r in _db.residuo on re.Idresiduo equals r.Idresiduo
                        join u in _db.usuario on re.Idusuario equals u.Idusuario
                        join l in _db.localidad on u.Idlocalidad equals l.Idlocalidad
                        select new
                        {
                            re,
                            r,
                            u,
                            l,
                            // Asignar el total de manera segura sin usar 'out' dentro de la expresión LINQ
                            TotalPorTipo = totalesPorTipo.ContainsKey(r.TipoResiduo) ? totalesPorTipo[r.TipoResiduo] : 0
                        };

            var resultado = query.Select(x => new RecolectarReporteDto
            {
                IDRecoleccion = x.re.Idrecoleccion,
                IDUsuario = x.u.Idusuario,
                IDEmpresa = x.re.Idempresa,
                IDResiduo = x.re.Idresiduo,
                FechaRecoleccion = x.re.FechaRecoleccion,
                PesoKg = x.re.PesoKg ?? 0,
                Estado = x.re.Estado ?? "Sin estado",
                TipoResiduo = x.r.TipoResiduo,
                IDLocalidad = (int)(x.u.Idlocalidad ?? 0),
                Nombre = x.u.Nombre,
                Apellidos = x.u.Apellidos,
                Telefono = x.u.Telefono,
                Email = x.u.Email,
                Direccion = x.u.Direccion,
                Rol = x.u.Rol,
                Localidad = x.l.Nombre,
                TotalPorTipoResiduo = x.TotalPorTipo
            });
            return resultado.ToList();
        }

        public List<UsuariosPuntosReporteDto> ObtenerReporteUsuariosPuntos()
        {
            var query = from r in _db.recolectar
                        join u in _db.usuario on r.Idusuario equals u.Idusuario
                        join p in _db.puntosUsuario on u.Idusuario equals p.Idusuario
                        select new UsuariosPuntosReporteDto
                        {
                            EstadoRecoleccion = r.Estado ?? "Sin estado",
                            FechaRecoleccion = r.FechaRecoleccion,
                            PesoKg = r.PesoKg ?? 0,
                            NombreUsuario = u.Nombre,
                            PuntosGanados = p.Puntos ?? 0,
                            EstadoPuntos = p.Estado ?? "Sin estado"
                        };

            return query.ToList();
        }
    }
}
