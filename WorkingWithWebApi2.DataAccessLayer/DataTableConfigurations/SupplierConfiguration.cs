using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using WorkingWithWebApi2.Models.DomainEntities;

namespace DataAccessLayer.DataTableConfigurations
{
    public class SupplierConfiguration : EntityTypeConfiguration<Supplier>
    {
        public SupplierConfiguration()
        {
            // Primary Key
            HasKey(p => p.SupplierID);

            // Fields
            Property(p => p.SupplierID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.CompanyName).IsRequired().IsVariableLength().HasMaxLength(40);
            Property(p => p.ContactName).IsRequired().IsVariableLength().HasMaxLength(30);
            Property(p => p.ContactTitle).IsVariableLength().HasMaxLength(30);
            Property(p => p.Address).IsVariableLength().HasMaxLength(60);
            Property(p => p.City).IsVariableLength().HasMaxLength(30);
            Property(p => p.Region).IsVariableLength().HasMaxLength(30);
            Property(p => p.PostalCode).IsVariableLength().HasMaxLength(12);
            Property(p => p.Country).IsVariableLength().HasMaxLength(50);            
            Property(p => p.Phone).IsVariableLength().HasMaxLength(30);
            Property(p => p.Fax).IsVariableLength().HasMaxLength(30);
            Property(p => p.HomePage).IsVariableLength().HasMaxLength(255);

            // Indexes
            Property(p => p.CompanyName).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IDX_SupplierCompanyName")));
            Property(p => p.PostalCode).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IDX_SupplierPostalCode")));
            Property(p => p.City).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IDX_SupplierCity")));
            Property(p => p.Region).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IDX_SupplierRegion")));

            // Table
            ToTable("Supplier", "Northwind");
        }
    }
}
