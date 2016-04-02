using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WorkingWithWebApi2.Models.DomainEntities
{
    public partial class Category
    {
        public Category()
        {
            this.Products = new List<Product>();
        }
        
        public int CategoryID { get; set; }
        [Required, StringLength(15, MinimumLength = 5)]
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }
        
        public virtual ICollection<Product> Products { get; set; }
    }
}
