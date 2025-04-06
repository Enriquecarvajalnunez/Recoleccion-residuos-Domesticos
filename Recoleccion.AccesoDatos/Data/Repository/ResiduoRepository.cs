using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using Recoleccion.Models;
using WebApi_Recoleccion_residuos_Domesticos.Models;


namespace Recoleccion.AccesoDatos.Data.Repository
{
    public class ResiduoRepository : Repository<Residuo>, IResiduoRepository
    {
        private readonly RecoleccionResiduosContext _db;
        public ResiduoRepository(RecoleccionResiduosContext db) : base(db)
        {
            _db = db;
        }
        //solo para el metodo de actualización se crea un repositorio adicional
        public void Update(Residuo residuo)
        {
            var objDesdeDb = _db.Residuo.FirstOrDefault(s => s.Idresiduo == residuo.Idresiduo);
            //actualizamos los atributos del objeto, accedemos a la clase del modelo
            objDesdeDb.TipoResiduo = residuo.TipoResiduo;
            objDesdeDb.Idresiduo = residuo.Idresiduo;
            _db.SaveChanges();
        }
    }
}
