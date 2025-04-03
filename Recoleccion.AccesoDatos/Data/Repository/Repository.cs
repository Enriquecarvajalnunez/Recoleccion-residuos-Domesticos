using Microsoft.EntityFrameworkCore;
using Recoleccion.AccesoDatos.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Recoleccion.AccesoDatos.Data.Repository
{
    internal class Repository<T> : IRepository<T> where T : class
    {
        //en esta clase se trabaja con el contexto
        protected readonly DbContext Context;
        internal DbSet<T> dbSet;

        //Metodo inyeccion de dependencia, constructor de la clase
        public Repository(DbContext context)
        {
            Context = context;
            this.dbSet = context.Set<T>();
        }

        //Agrega a la BD
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        //Obtiene el registro por su ID
        public T Get(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeProperties = null)
        {
            //Crea una consulta IQueryable a partir del Dbset del contexto 
            IQueryable<T> query = dbSet;

            //se aplica filtro si se proporciona, es decir si se pasó un filtro entonces entra al if
            if (filter != null)
            {
                query = query.Where(filter);
            }

            //Se incluyen propiedades de navegacion si se proporcionan, es decir, trae los datos relacionados
            if (includeProperties != null)
            {
                // se divide la cadena de propiedades por coma y se itera sobre ellas
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            //se intenta ordenar los registros
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }

            //para el caso que no se aplica ninguno de los casos anteriores
            return query.ToList();
        }

        //Obtiene un registro de forma individual
        public T GetFirstOrDefault(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {   //Rehutilizamos parte del codiso del metodo GetAll
            //Crea una consulta IQueryable a partir del dbset del contexto 
            IQueryable<T> query = dbSet;

            //se aplica filtro si se propoerciona
            if (filter != null)
            {
                query = query.Where(filter);
            }

            //Se incluyen propiedades de navegacion si se proporcionan
            if (includeProperties != null)
            {
                // se divide la cadena de propiedades por coma y se itera sobre ellas
                //se hace separado por comas porque las tablas se pasan separadas por coma eje: categoria, articulo
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            return query.FirstOrDefault();
        }

        //Remueve registro por su ID
        public void Remove(int id)
        {
            T entityToRemove = dbSet.Find(id);
        }

        //Remueve registro por su identidad
        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}
