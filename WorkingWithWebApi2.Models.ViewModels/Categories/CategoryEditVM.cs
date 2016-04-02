using System.ComponentModel.DataAnnotations;

namespace WorkingWithWebApi2.Models.ViewModels.Categories
{
    /// <summary>
    /// View model class used for inserts & updates
    /// </summary>
    public class CategoryEditVM
    {
        public int CategoryID { get; set; }
        [Required, StringLength(15, MinimumLength = 5)]
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }
    }
}
