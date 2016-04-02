using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using WorkingWithWebApi2.Models.DomainEntities;

namespace DataAccessLayer.DataTableConfigurations
{
    public class CustomerConfiguration : EntityTypeConfiguration<Customer>
    {
        public CustomerConfiguration()
        {
            // Primary Key
            HasKey(p => p.CustomerID);

            // Fields
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

            // Indexes
            Property(p => p.CompanyName).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IDX_CustomerCompanyName")));
            Property(p => p.PostalCode).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IDX_CustomerPostalCode")));
            Property(p => p.City).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IDX_CustomerCity")));
            Property(p => p.Region).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IDX_CustomerRegion")));

            // Table
            ToTable("Customer", "Northwind");
        }
    }
}
