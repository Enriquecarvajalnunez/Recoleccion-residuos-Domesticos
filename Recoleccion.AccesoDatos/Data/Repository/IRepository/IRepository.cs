using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Recoleccion.AccesoDatos.Data.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        //Metodo que recibe el ID de una entidad como entero
        T Get(int id);

        //obtiene la lista de registros, ordernarse con orderby,
        //includeProperties -> trae datos relacionados
        IEnumerable<T> GetAll(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string? includeProperties = null
        );

        //Obtener un registro individual
        T GetFirstOrDefault(
            Expression<Func<T, bool>>? filter = null,
            string? includeProperties = null
        );

        //Metodo para agregar la entidad a la BD
        void Add(T entity);
        //Eliminar de la BD un registro, se envia un ID
        void Remove(int id);
        //Eliminar un registro por la entidad
        void Remove(T entity);

        /*El update no existe: por el enfoque de entity framwerok ya que realiza el seguimiento de cambio automatico
        automaticamente de una clase, se espera por lo tanto que la entidad se actualice
        a través del contexto*/
    }
}
