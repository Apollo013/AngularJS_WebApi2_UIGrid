using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WorkingWithWebApi2.Models.ViewModels.Shared;

namespace WorkingWithWebApi2.Models.ViewModels.Products
{
    /// <summary>
    /// View model class used for inserts & updates
    /// </summary>
    public class ProductEditVM
    {
        public int ProductID { get; set; }
        [Required, StringLength(40, MinimumLength = 1)]
        public string ProductName { get; set; }
        [Required]
        public int SupplierID { get; set; }
        [Required]
        public int CategoryID { get; set; }
        public string QuantityPerUnit { get; set; }
        [Range(0.00, 79228162514264337593543950335.00)]
        public decimal UnitPrice { get; set; }
        [Range(0, short.MaxValue)]
        public short UnitsInStock { get; set; }
        [Range(0, short.MaxValue)]
        public short UnitsOnOrder { get; set; }
        [Range(0, short.MaxValue)]
        public short ReorderLevel { get; set; }        
        public bool Discontinued { get; set; }
        public byte[] RowVersion { get; set; }

        public IEnumerable<DropListVM> Suppliers { get; set; }
        public IEnumerable<DropListVM> Categories { get; set; }

        public ProductEditVM()
        {
            Suppliers = new List<DropListVM>();
            Categories = new List<DropListVM>();
        }
    }
}
