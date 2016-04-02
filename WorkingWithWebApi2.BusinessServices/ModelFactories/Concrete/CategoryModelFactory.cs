using WorkingWithWebApi2.BusinessServices.ModelFactories.Abstract;
using WorkingWithWebApi2.Models.DomainEntities;
using WorkingWithWebApi2.Models.ViewModels.Categories;
using WorkingWithWebApi2.Models.ViewModels.Shared;

namespace WorkingWithWebApi2.BusinessServices.ModelFactories.Concrete
{
    /// <summary>
    /// Class responsible for converting Category DTO's to database entities and v.v.
    /// </summary>
    public class CategoryModelFactory : ICategoryModelFactory
    {
        public Category CreateDomainEntity(CategoryEditVM viewModel)
        {
            return new Category()
            {
                CategoryID = viewModel.CategoryID,
                CategoryName = viewModel.CategoryName,
                Description = viewModel.Description,
                Picture = viewModel.Picture
            };
        }

        public DropListVM CreateDropListVM(Category entity)
        {
            return new DropListVM()
            {
                ID = entity.CategoryID,
                Name = entity.CategoryName
            };
        }

        public CategoryEditVM CreateEditVM(Category entity)
        {
            return new CategoryEditVM()
            {
                CategoryID = entity.CategoryID,
                CategoryName = entity.CategoryName,
                Description = entity.Description,
                Picture = entity.Picture
            };
        }

        public CategoryListVM CreateListVM(Category entity)
        {
            return new CategoryListVM()
            {
                CategoryID = entity.CategoryID,
                CategoryName = entity.CategoryName,
                Description = entity.Description,
                Picture = entity.Picture
            };
        }

        public CategoryRefVM CreateRefVM(Category entity)
        {
            return new CategoryRefVM()
            {
                CategoryName = entity.CategoryName
            };
        }
    }
}
