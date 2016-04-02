using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WorkingWithWebApi2.Models.DomainEntities
{
    public class Supplier
    {
        public Supplier()
        {
            this.Products = new List<Product>();
        }

        public int SupplierID { get; set; }
        [Required, StringLength(40, MinimumLength = 1)]
        public string CompanyName { get; set; }
        [Required, StringLength(30, MinimumLength = 1)]
        public string ContactName { get; set; }
        [StringLength(30)]
        public string ContactTitle { get; set; }
        [StringLength(60)]
        public string Address { get; set; }
        [StringLength(30)]
        public string City { get; set; }
        [StringLength(30)]
        public string Region { get; set; }
        [StringLength(12)]
        public string PostalCode { get; set; }
        [StringLength(50)]
        public string Country { get; set; }
        [StringLength(30)]
        public string Phone { get; set; }
        [StringLength(30)]
        public string Fax { get; set; }
        [StringLength(255)]
        public string HomePage { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}

