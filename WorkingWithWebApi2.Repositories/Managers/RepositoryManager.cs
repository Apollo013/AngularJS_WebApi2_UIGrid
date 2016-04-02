using System;
using WorkingWithWebApi2.DataAccessLayer.Contexts;
using WorkingWithWebApi2.Models.DomainEntities;
using WorkingWithWebApi2.Repositories.Abstract;

namespace WorkingWithWebApi2.Repositories.Managers
{
    /// <summary>
    /// Class responsible for coordinating the work of multiple repositories.
    /// </summary>
    public class RepositoryManager : IDisposable
    {
        #region Private Variable`s

        private DataContext dataContext = new DataContext();
        private GenericRepository<Product> productRepository;
        private GenericRepository<Category> categoryRepository;
        private GenericRepository<Supplier> supplierRepository;

        #endregion

        #region Repositories

        /// <summary>
        /// Get Only Property For Product Repository
        /// </summary>
        public GenericRepository<Product> ProductRepository {
            get
            {
                if(productRepository == null)
                {
                    productRepository = new GenericRepository<Product>(dataContext);
                }
                return productRepository;
            }
        }

        /// <summary>
        /// Get Only Property For Category Repository
        /// </summary>
        public GenericRepository<Category> CategoryRepository
        {
            get
            {
                if (categoryRepository == null)
                {
                    categoryRepository = new GenericRepository<Category>(dataContext);
                }
                return categoryRepository;
            }
        }

        /// <summary>
        /// Get Only Property For Supplier Repository
        /// </summary>
        public GenericRepository<Supplier> SupplierRepository
        {
            get
            {
                if (supplierRepository == null)
                {
                    supplierRepository = new GenericRepository<Supplier>(dataContext);
                }
                return supplierRepository;
            }
        }

        #endregion

        #region Method Members

        /// <summary>
        /// Saves changes to the underlying persistance store
        /// </summary>
        /// <returns>The number of records saved</returns>
        public int Save()
        {
            return dataContext.SaveChanges();            
        }

        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    dataContext.Dispose();
                }
                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~RepositoryManager() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
