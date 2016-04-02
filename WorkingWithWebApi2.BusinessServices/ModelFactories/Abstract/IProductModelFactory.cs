using WorkingWithWebApi2.Models.DomainEntities;
using WorkingWithWebApi2.Models.ViewModels.Products;
using WorkingWithWebApi2.Models.ViewModels.Shared;

namespace WorkingWithWebApi2.BusinessServices.ModelFactories.Abstract
{
    public interface IProductModelFactory
    {
        Product CreateDomainEntity(ProductEditVM model);
        DropListVM CreateDropListViewModel(Product model);
        ProductEditVM CreateEditViewModel(Product model);
        ProductListVM CreateListViewModel(Product model);
    }
}
