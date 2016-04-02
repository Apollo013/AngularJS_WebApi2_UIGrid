using System;
using WorkingWithWebApi2.Models.ViewModels.Categories;
using WorkingWithWebApi2.Models.ViewModels.Suppliers;

namespace WorkingWithWebApi2.Models.ViewModels.Products
{
    /// <summary>
    /// View model class used for lists
    /// </summary>
    public class ProductListVM
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<short> UnitsInStock { get; set; }
        public Nullable<short> UnitsOnOrder { get; set; }
        public Nullable<short> ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

        public SupplierRefVM Supplier { get; set; }
        public CategoryRefVM Category { get; set; }

    }
}
