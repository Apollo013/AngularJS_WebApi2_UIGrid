using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WorkingWithWebApi2.Models.DomainEntities
{
    public class Product
    {
        public Product()
        {
            this.OrderDetails = new List<OrderDetail>();
        }

        public int ProductID { get; set; }
        [Required,StringLength(40, MinimumLength = 1)]
        public string ProductName { get; set; }
        public int SupplierID { get; set; } = 1;
        public int CategoryID { get; set; } = 1;
        public string QuantityPerUnit { get; set; }
        [Range(0.00, 79228162514264337593543950335.00)]
        public decimal UnitPrice { get; set; } = 0.01M;
        [Range(0, short.MaxValue)]
        public short UnitsInStock { get; set; } = 0;
        [Range(0, short.MaxValue)]
        public short UnitsOnOrder { get; set; } = 0;
        [Range(0, short.MaxValue)]
        public short ReorderLevel { get; set; } = 0;
        public bool Discontinued { get; set; } = false;
        public byte[] RowVersion { get; set; }       
        public char test { get; set; }

        public virtual Supplier Supplier { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

    }
}
