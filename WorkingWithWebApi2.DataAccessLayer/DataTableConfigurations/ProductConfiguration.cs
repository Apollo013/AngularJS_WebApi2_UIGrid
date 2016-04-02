using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using WorkingWithWebApi2.Models.DomainEntities;

namespace DataAccessLayer.DataTableConfigurations
{
    public class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            // Primary Key
            HasKey(p => p.ProductID);
            Property(p => p.ProductID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Fields            
            Property(p => p.ProductName).IsRequired().IsVariableLength().HasMaxLength(40);
            Property(p => p.QuantityPerUnit).IsVariableLength().HasMaxLength(20);
            Property(p => p.Discontinued).IsRequired();

            // Indexes
            Property(p => p.ProductName).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IDX_ProductName") { IsUnique = true }));
            //
            // Table
            ToTable("Product", "Northwind");
        }
    }
}
