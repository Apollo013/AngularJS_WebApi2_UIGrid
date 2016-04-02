using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using WorkingWithWebApi2.Models.DomainEntities;

namespace DataAccessLayer.DataTableConfigurations
{
    public class OrderConfiguration : EntityTypeConfiguration<Order>
    {
        public OrderConfiguration()
        {
            // Primary Key
            HasKey(p => p.OrderID);
            Property(p => p.OrderID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Fields
            Property(p => p.ShipName).IsRequired().IsVariableLength().HasMaxLength(40);
            Property(p => p.ShipAddress).IsVariableLength().HasMaxLength(60);
            Property(p => p.ShipCity).IsVariableLength().HasMaxLength(30);
            Property(p => p.ShipRegion).IsVariableLength().HasMaxLength(30);
            Property(p => p.ShipPostalCode).IsVariableLength().HasMaxLength(12);
            Property(p => p.ShipCountry).IsVariableLength().HasMaxLength(50);

            // Indexes
            Property(p => p.CustomerID).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IDX_OrderCustomerID")));

            // Table
            ToTable("Order", "Northwind");
        }
    }
}
