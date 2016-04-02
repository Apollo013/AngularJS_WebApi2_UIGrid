using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using WorkingWithWebApi2.Models.DomainEntities;

namespace DataAccessLayer.DataTableConfigurations
{
    public class CategoryConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoryConfiguration()
        {
            // Primary Key
            HasKey(p => p.CategoryID);

            // Fields
            Property(p => p.CategoryID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.CategoryName).IsRequired().IsVariableLength().HasMaxLength(15);

            // Indexes
            Property(p => p.CategoryName).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IDX_CategoryName") { IsUnique = true }));

            // Table
            ToTable("Category", "Northwind");
        }
    }
}
