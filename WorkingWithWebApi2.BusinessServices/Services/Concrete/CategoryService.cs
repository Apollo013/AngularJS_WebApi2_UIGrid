using System;
using System.Collections.Generic;
using System.Linq;
using WorkingWithWebApi2.BusinessServices.Helpers;
using WorkingWithWebApi2.BusinessServices.ModelFactories.Abstract;
using WorkingWithWebApi2.BusinessServices.Services.Abstract;
using WorkingWithWebApi2.Models.DomainEntities;
using WorkingWithWebApi2.Models.ViewModels.Categories;
using WorkingWithWebApi2.Models.ViewModels.Shared;

namespace WorkingWithWebApi2.BusinessServices.Services.Concrete
{
    public class CategoryService : ServiceBase, ICategoryService
    {
        #region Properties & Constructors

        /// <summary>
        /// Default value for query OrderBy
        /// </summary>
        private string defaultOrderByCriteria = "categoryName:asc";

        /// <summary>
        /// Converts Entity models to DTO'S & v.v.
        /// </summary>
        private ICategoryModelFactory modelFactory;

        public CategoryService(ICategoryModelFactory modelFactory)
        {
            this.modelFactory = modelFactory;
        }

        #endregion

        #region ICategoryService Implementations

        /// <summary>
        /// Creates a new category and saves it to data storage
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public bool Create(CategoryEditVM vm)
        {
            var entity = modelFactory.CreateDomainEntity(vm);
            repositoryManager.CategoryRepository.Insert(entity);
            return repositoryManager.Save() == 1;
        }

        /// <summary>
        /// Deletes a category from data storage
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            var entity = repositoryManager.CategoryRepository.GetByID(id);

            if (entity == null)
            {
                throw new NullReferenceException("Category cannot be found");
            }

            repositoryManager.CategoryRepository.Delete(entity);
            return repositoryManager.Save() == 1;
        }

        /// <summary>
        /// Gets a list of categories depending upon filter criteria.
        /// </summary>
        /// <param name="orderBy"></param>
        /// <param name="filter"></param>
        /// <returns>A List of CategoryListVM objects that match the filter criteria.</returns>
        public IEnumerable<CategoryListVM> GetAll(string orderBy, string search)
        {
            // Create initial query
            IQueryable<Category> query = repositoryManager.CategoryRepository.Query();

            // Where
            if (!string.IsNullOrEmpty(search))
            {
                query = query.ApplyFilter(search);
            }

            // OrderBy
            query = query.ApplyOrder(orderBy, defaultOrderByCriteria);

            // Grab records
            var list = query.ToList().Select(c => modelFactory.CreateListVM(c));

            // Check that we have records to return
            if (!list.Any())
            {
                throw new NullReferenceException("No Categories Found");
            }

            return list;
        }

        /// <summary>
        /// Gets a category view model used for inserts and updates
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CategoryEditVM GetById(int id)
        {
            if (id == 0)
            {
                return new CategoryEditVM();
            }
            else if (id > 0)
            {
                try
                {
                    var entity = repositoryManager.CategoryRepository.GetByID(id);

                    if (entity != null)
                    {
                        var editViewModel = modelFactory.CreateEditVM(entity);
                        return editViewModel;
                    }
                    else
                    {
                        throw new NullReferenceException("Category Not Found");
                    }
                }
                catch (InvalidOperationException)
                {
                    throw new InvalidOperationException("More than one category was found");
                }
            }
            else // (ID < 0)
            {
                throw new ArgumentOutOfRangeException("ID");
            }
        }

        /// <summary>
        /// Gets a list of category drop list view models
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DropListVM> GetDropList()
        {
            var query = repositoryManager.CategoryRepository.Query();
            query = query.ApplyOrder(defaultOrderByCriteria);
            return query.ToList().Select(e => modelFactory.CreateDropListVM(e));
        }

        /// <summary>
        /// Gets a paged result view model containing category list view models
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderBy"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public PagedResultViewModel<CategoryListVM> GetPageResult(int pageNo, int pageSize, string orderBy, string search)
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
            IQueryable<Category> query = repositoryManager.CategoryRepository.Query();

            // Where
            if (!string.IsNullOrEmpty(search))
            {
                query = query.ApplyFilter(search);
            }

            // OrderBy
            query = query.ApplyOrder(orderBy, defaultOrderByCriteria);   // Pass in a default also in case orderby is null or has invalid property names specified

            int skip = (pageNo - 1) * pageSize;             // Calculate number of records to skip 
            long totalRecordCount = query.Count();  // Get total number of records

            // Grab records
            var list = query
                .Skip(skip)
                .Take(pageSize)
                .ToList()
                .Select(c => modelFactory.CreateListVM(c));

            // Check that we have records to return
            if (!list.Any())
            {
                throw new NullReferenceException("No Categories Found");
            }

            return new PagedResultViewModel<CategoryListVM>(list, pageNo, pageSize, totalRecordCount);
        }

        /// <summary>
        /// Updates and saves a category to data storage
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public bool Update(CategoryEditVM vm)
        {
            var entity = modelFactory.CreateDomainEntity(vm);
            repositoryManager.CategoryRepository.Update(entity);
            return repositoryManager.Save() == 1;
        }

        #endregion
    }
}
