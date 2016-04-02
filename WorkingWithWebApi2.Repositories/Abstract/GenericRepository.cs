using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using WorkingWithWebApi2.DataAccessLayer.Contexts;

namespace WorkingWithWebApi2.Repositories.Abstract
{
    /// <summary>
    /// Generic repository used for persisting and retrieving data from the database
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class GenericRepository<TEntity> where TEntity : class
    {
        #region Private Variables

        internal DataContext context;
        internal DbSet<TEntity> dbSet;

        #endregion

        #region Constructors

        public GenericRepository(DataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        #endregion

        #region Queries

        /// <summary>
        /// Generic get method based on the unique id for the entity.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);         
        }

        /// <summary>
        /// Generic get method that finds based on a unique name for the entity.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual TEntity Get(Expression<Func<TEntity,bool>> predicate)
        {
            return dbSet.Find(predicate);
        }

        /// <summary>
        /// Generic get method based on a spcified predicate for the entity.
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual TEntity GetSingleOrDefault(Expression<Func<TEntity, bool>> predicate, string[] includeProperties = null)
        {
            if(includeProperties == null)
            {
                return this.Query().Where(predicate).SingleOrDefault();
            }
            return this.Query(includeProperties).Where(predicate).SingleOrDefault();            
        }
        
        /// <summary>
        /// Constructs a query
        /// </summary>
        /// <param name="includeProperties">String array containing a list of associate class names to include</param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> Query(string[] includeProperties = null)
        {
            IQueryable<TEntity> query  = dbSet;

            // Include Properties
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return query.AsQueryable();
        }

        #endregion

        #region Create, Update & Delete Members

        /// <summary>
        /// generic Insert method for the entities
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        /// <summary>
        /// Generic Delete method for the entities
        /// </summary>
        /// <param name="id"></param>
        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        /// <summary>
        /// Generic Delete method for the entities
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(TEntity entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        /// <summary>
        /// Generic update method for the entities
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(TEntity entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        #endregion

    }
}
