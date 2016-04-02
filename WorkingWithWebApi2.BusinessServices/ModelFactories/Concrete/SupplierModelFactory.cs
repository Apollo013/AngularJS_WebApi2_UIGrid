using WorkingWithWebApi2.BusinessServices.ModelFactories.Abstract;
using WorkingWithWebApi2.Models.DomainEntities;
using WorkingWithWebApi2.Models.ViewModels.Shared;
using WorkingWithWebApi2.Models.ViewModels.Suppliers;

namespace WorkingWithWebApi2.BusinessServices.ModelFactories.Concrete
{
    public class SupplierModelFactory : ISupplierModelFactory
    {
        public Supplier CreateDomainEntity(SupplierEditVM viewModel)
        {
            return new Supplier()
            {
                SupplierID = viewModel.SupplierID,
                CompanyName = viewModel.CompanyName,
                ContactName = viewModel.ContactName,
                ContactTitle = viewModel.ContactTitle,
                Address = viewModel.Address,
                City = viewModel.City,
                Country = viewModel.Country,
                Region = viewModel.Region,
                PostalCode = viewModel.PostalCode,
                Fax = viewModel.Fax,
                Phone = viewModel.Phone,
                HomePage = viewModel.HomePage
            };
        }

        public DropListVM CreateDropListVM(Supplier entity)
        {
            return new DropListVM()
            {
                ID = entity.SupplierID,
                Name = entity.CompanyName
            };
        }

        public SupplierEditVM CreateEditVM(Supplier entity)
        {
            return new SupplierEditVM()
            {
                SupplierID = entity.SupplierID,
                CompanyName = entity.CompanyName,
                ContactName = entity.ContactName,
                ContactTitle = entity.ContactTitle,
                Address = entity.Address,
                City = entity.City,
                Country = entity.Country,
                Region = entity.Region,
                PostalCode = entity.PostalCode,
                Fax = entity.Fax,
                Phone = entity.Phone,
                HomePage = entity.HomePage
            };
        }

        public SupplierListVM CreateListVM(Supplier entity)
        {
            return new SupplierListVM()
            {
                SupplierID = entity.SupplierID,
                CompanyName = entity.CompanyName,
                ContactName = entity.ContactName
            };
        }

        public SupplierRefVM CreateRefVM(Supplier entity)
        {
            return new SupplierRefVM()
            {
                CompanyName = entity.CompanyName
            };
        }
    }
}
