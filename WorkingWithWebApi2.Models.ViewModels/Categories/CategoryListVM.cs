using System.ComponentModel.DataAnnotations;

namespace WorkingWithWebApi2.Models.ViewModels.Categories
{
    /// <summary>
    /// View model class used for lists
    /// </summary>
    public class CategoryListVM
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }
    }
}
