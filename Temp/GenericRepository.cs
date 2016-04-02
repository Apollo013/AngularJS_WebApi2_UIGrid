using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using WorkingWithWebApi2.Models.DomainEntities;
using WorkingWithWebApi2.DataAccessLayer;
using System.Linq.Expressions;


namespace WorkingWithWebApi2.Repositories
{
	public class GenericRepository<TEntity> where TEntity : class
	{
		#region Private member variables
		
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
		
		#region Get's & Queries
		
		/// <summary>
        /// Generic get method on the basis of id for Entities.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }
				
		public virtual IQueryable<TEntity> GetQuery(string includeProperties = null){
		
			IQueryable<TEntity> = dbSet;
			
			if(!includeProperties == null){
				foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProperty);
				}			
			}
			
			return query;
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