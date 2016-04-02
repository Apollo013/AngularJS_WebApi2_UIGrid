using WorkingWithWebApi2.Models.DomainEntities;
using WorkingWithWebApi2.Models.ViewModels.Shared;
using WorkingWithWebApi2.Models.ViewModels.Suppliers;

namespace WorkingWithWebApi2.BusinessServices.ModelFactories.Abstract
{
    public interface ISupplierModelFactory : IModelFactory<Supplier, SupplierEditVM, SupplierListVM, DropListVM>
    {
        /// <summary>
        /// Converts a Supplier object to a view model used in child references
        /// </summary>
        /// <param name="entity">The Supplier entity object to be converted</param>
        /// <returns>The converted view model</returns>
        SupplierRefVM CreateRefVM(Supplier entity);
    }
}
