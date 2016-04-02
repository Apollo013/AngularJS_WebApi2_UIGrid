using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using WorkingWithWebApi2.BusinessServices.Helpers;
using WorkingWithWebApi2.BusinessServices.ModelFactories.Abstract;
using WorkingWithWebApi2.BusinessServices.Services.Abstract;
using WorkingWithWebApi2.Models.DomainEntities;
using WorkingWithWebApi2.Models.ViewModels.Products;
using WorkingWithWebApi2.Models.ViewModels.Shared;

namespace WorkingWithWebApi2.BusinessServices.Services.Concrete
{
    /// <summary>
    /// Products business service class
    /// </summary>
    public class ProductService : ServiceBase, IProductService
    {
        #region Properties & Constructors

        /// <summary>
        /// Default value for query OrderBy
        /// </summary>
        private string defaultOrderByCriteria = "productName:asc";

        /// <summary>
        /// Default property names to include when querying
        /// </summary>
        private string[] includedProperties = new string[] { "Category", "Supplier" };

        /// <summary>
        /// Converts Entity models to DTO'S & v.v.
        /// </summary>
        private IProductModelFactory modelFactory;

        [Inject]
        public ICategoryService CategoryService { get; set; }

        [Inject]
        public ISupplierService SupplierService { get; set; }

        public ProductService(IProductModelFactory modelFactory)
        {
            this.modelFactory = modelFactory;
        }

        #endregion

        #region IProductService Implementations

        /// <summary>
        /// Creates a new product and saves it to data storage
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public bool Create(ProductEditVM vm)
        {
            var product = modelFactory.CreateDomainEntity(vm);
            repositoryManager.ProductRepository.Insert(product);
            return repositoryManager.Save() == 1;
        }

        /// <summary>
        /// Deletes a product from data storage
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            var product = repositoryManager.ProductRepository.GetByID(id);

            if(product == null)
            {
                throw new NullReferenceException("Product cannot be found");
            }

            repositoryManager.ProductRepository.Delete(product);
            return repositoryManager.Save() == 1;
        }

        /// <summary>
        /// Gets a list of products depending upon filter criteria.
        /// </summary>
        /// <param name="orderBy"></param>
        /// <param name="filter"></param>
        /// <returns>A List of ProductListVM objects that match the filter criteria.</returns>
        public IEnumerable<ProductListVM> GetAll(string orderBy = null, string search = null)
        {
            // Create initial query
            IQueryable<Product> query = repositoryManager.ProductRepository.Query(includedProperties);

            // Where
            if (!string.IsNullOrEmpty(search))
            {
                query = query.ApplyFilter(search);
            }

            // OrderBy
            query = query.ApplyOrder(orderBy, defaultOrderByCriteria);

            // Grab records
            var products = query.ToList().Select(p => modelFactory.CreateListViewModel(p));

            // Check that we have records to return
            if (!products.Any())
            {
                throw new NullReferenceException("No Products Found");
            }

            return products;
        }

        /// <summary>
        /// Gets a PagedResultViewModel object containg a list of products and other information relating to pagination.
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderBy"></param>
        /// <param name="filter"></param>
        /// <returns>A PagedResultViewModel object that match the filter & pagination criteria.</returns>
        public PagedResultViewModel<ProductListVM> GetPageResult(int pageNo = 1, int pageSize = 25, string orderBy = null, string filter = null)
        {
            // Pre-checks
            if (pageNo < 1)
            {
                throw new ArgumentOutOfRangeException("Page Number");
            }

            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException("Page Size");
            }

            // Create initial query
            IQueryable<Product> productsQuery = repositoryManager.ProductRepository.Query(includedProperties);

            // Where
            if (!string.IsNullOrEmpty(filter))
            {
                productsQuery = productsQuery.ApplyFilter(filter);
            }

            // OrderBy
            productsQuery = productsQuery.ApplyOrder(orderBy, defaultOrderByCriteria);   // Pass in a default also in case orderby is null or has invalid property names specified

            int skip = (pageNo - 1) * pageSize;             // Calculate number of records to skip 
            long totalRecordCount = productsQuery.Count();  // Get total number of records

            // Grab records
            var products = productsQuery
                .Skip(skip)
                .Take(pageSize)
                .ToList()
                .Select(p => modelFactory.CreateListViewModel(p));

            // Check that we have records to return
            if (!products.Any())
            {
                throw new NullReferenceException("No Products Found");
            }

            return new PagedResultViewModel<ProductListVM>(products, pageNo, pageSize, totalRecordCount);
        }

        /// <summary>
        /// Gets a product based on a specific unique id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductEditVM GetById(int id)
        {
            if(id == 0)
            {
                var editViewModel = modelFactory.CreateEditViewModel(new Product());
                editViewModel.Categories = CategoryService.GetDropList();
                editViewModel.Suppliers = SupplierService.GetDropList();
                return editViewModel;
            }
            else if (id > 0)
            {
                try
                {
                    Expression<Func<Product, bool>> predicate = ((p) => p.ProductID == id);
                    var product = repositoryManager.ProductRepository.GetSingleOrDefault(predicate, includedProperties);
                    
                    if (product != null)
                    {
                        var editViewModel = modelFactory.CreateEditViewModel(product);
                        editViewModel.Categories = CategoryService.GetDropList();
                        editViewModel.Suppliers = SupplierService.GetDropList();
                        return editViewModel;
                    }
                    else
                    {
                        throw new NullReferenceException("Product Not Found");
                    }
                }
                catch (InvalidOperationException)
                {
                    throw new InvalidOperationException("More than one product was found");
                }
            }
            else // (ID < 0)
            {
                throw new ArgumentOutOfRangeException("ID");
            }
        }

        /// <summary>
        /// Updates a product and saves it to data storage
        /// </summary>
        /// <param name="productModel"></param>
        /// <returns></returns>
        public bool Update(ProductEditVM vm)
        {
            var entity = modelFactory.CreateDomainEntity(vm);
            repositoryManager.ProductRepository.Update(entity);
            return repositoryManager.Save() == 1;
        }
         
        #endregion
    }
}
