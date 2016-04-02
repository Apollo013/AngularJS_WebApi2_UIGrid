using System.Collections.Generic;
using WorkingWithWebApi2.Models.ViewModels.Categories;
using WorkingWithWebApi2.Models.ViewModels.Shared;

namespace WorkingWithWebApi2.BusinessServices.Services.Abstract
{
    /// <summary>
    /// Categories business service contract
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Gets a category view model used for inserts and updates
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CategoryEditVM GetById(int id);

        /// <summary>
        /// Gets a list of category list view models
        /// </summary>
        /// <param name="orderBy"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        IEnumerable<CategoryListVM> GetAll(string orderBy = null, string search = null);

        /// <summary>
        /// Gets a list of category drop list view models
        /// </summary>
        /// <returns></returns>
        IEnumerable<DropListVM> GetDropList();

        /// <summary>
        /// Gets a paged result view model containing category list view models
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderBy"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        PagedResultViewModel<CategoryListVM> GetPageResult(int pageNo, int pageSize, string orderBy, string search);

        /// <summary>
        /// Creates and saves a new category to data storage
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        bool Create(CategoryEditVM vm);

        /// <summary>
        /// Updates and saves a category to data storage
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        bool Update(CategoryEditVM vm);

        /// <summary>
        /// Removes a category from data storage
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(int id);
    }
}
