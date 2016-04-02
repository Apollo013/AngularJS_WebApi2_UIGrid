using WorkingWithWebApi2.Models.DomainEntities;
using WorkingWithWebApi2.Models.ViewModels.Categories;
using WorkingWithWebApi2.Models.ViewModels.Shared;

namespace WorkingWithWebApi2.BusinessServices.ModelFactories.Abstract
{
    public interface ICategoryModelFactory : IModelFactory<Category, CategoryEditVM, CategoryListVM, DropListVM>
    {
        /// <summary>
        /// Converts a Category object to a view model used in child references
        /// </summary>
        /// <param name="entity">The Category entity object to be converted</param>
        /// <returns>The converted view model</returns>
        CategoryRefVM CreateRefVM(Category entity);
    }
}
