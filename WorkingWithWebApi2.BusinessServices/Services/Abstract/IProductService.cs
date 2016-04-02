using System.Collections.Generic;
using WorkingWithWebApi2.Models.ViewModels.Products;
using WorkingWithWebApi2.Models.ViewModels.Shared;

namespace WorkingWithWebApi2.BusinessServices.Services.Abstract
{
    /// <summary>
    /// Products business service contract
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Gets a product view model used for inserts and updates
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ProductEditVM GetById(int id);

        /// <summary>
        /// Gets a list of product list view models
        /// </summary>
        /// <param name="orderBy"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        IEnumerable<ProductListVM> GetAll(string orderBy, string search);

        /// <summary>
        /// Gets a paged result view model containing product list view models
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderBy"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        PagedResultViewModel<ProductListVM> GetPageResult(int pageNo, int pageSize, string orderBy, string search);

        /// <summary>
        /// Creates and saves a new product to data storage
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        bool Create(ProductEditVM vm);

        /// <summary>
        /// Updates and saves a product to data storage
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        bool Update(ProductEditVM vm);

        /// <summary>
        /// Removes a product from data storage
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(int id);
    }
}
