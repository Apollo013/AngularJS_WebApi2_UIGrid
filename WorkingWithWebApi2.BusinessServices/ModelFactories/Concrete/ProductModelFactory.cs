using System;
using WorkingWithWebApi2.BusinessServices.ModelFactories.Abstract;
using WorkingWithWebApi2.Models.DomainEntities;
using WorkingWithWebApi2.Models.ViewModels.Categories;
using WorkingWithWebApi2.Models.ViewModels.Products;
using WorkingWithWebApi2.Models.ViewModels.Shared;
using WorkingWithWebApi2.Models.ViewModels.Suppliers;

namespace WorkingWithWebApi2.BusinessServices.ModelFactories.Concrete
{
    /// <summary>
    /// Class responsible for converting Product entities to DTO's and v.v.
    /// </summary>
    public class ProductModelFactory : IProductModelFactory
    {
        /// <summary>
        /// Converts a Product edit DTO to a Product entity. Usually used prior to persisting data.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Product CreateDomainEntity(ProductEditVM model)
        {
            return new Product()
            {
                ProductID = model.ProductID,
                ProductName = model.ProductName,
                CategoryID = model.CategoryID,
                SupplierID = model.SupplierID,
                UnitsInStock = model.UnitsInStock,
                UnitPrice = model.UnitPrice,
                UnitsOnOrder = model.UnitsOnOrder,
                ReorderLevel = model.ReorderLevel,
                QuantityPerUnit = model.QuantityPerUnit,
                Discontinued = model.Discontinued,
                RowVersion = model.RowVersion ?? Guid.NewGuid().ToByteArray()
        };
        }

        /// <summary>
        /// Converts a Product entity to a Product DTO used in drop lists.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DropListVM CreateDropListViewModel(Product model)
        {
            return new DropListVM()
            {
                ID = model.ProductID,
                Name = model.ProductName
            };
        }

        /// <summary>
        /// Converts a Product entity to a Product DTO used for editing.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ProductEditVM CreateEditViewModel(Product model)
        {
            return new ProductEditVM()
            {
                ProductID = model.ProductID,
                ProductName = model.ProductName,
                CategoryID = model.CategoryID,
                SupplierID = model.SupplierID,
                UnitsInStock = model.UnitsInStock,
                UnitPrice = model.UnitPrice,
                UnitsOnOrder = model.UnitsOnOrder,
                ReorderLevel = model.ReorderLevel,
                QuantityPerUnit = model.QuantityPerUnit,
                Discontinued = model.Discontinued,
                RowVersion = model.RowVersion                
            };
        }

        /// <summary>
        /// Converts a Product entity to a Product DTO used in main lists.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ProductListVM CreateListViewModel(Product model)
        {
            return new ProductListVM()
            {
                ProductID = model.ProductID,
                ProductName = model.ProductName,
                UnitsInStock = model.UnitsInStock,
                UnitPrice = model.UnitPrice,
                UnitsOnOrder = model.UnitsOnOrder,
                ReorderLevel = model.ReorderLevel,
                QuantityPerUnit = model.QuantityPerUnit,
                Discontinued = model.Discontinued,
                Category = new CategoryRefVM() { CategoryName = model.Category.CategoryName },
                Supplier = new SupplierRefVM() { CompanyName = model.Supplier.CompanyName }
            };
        }
    }
}
