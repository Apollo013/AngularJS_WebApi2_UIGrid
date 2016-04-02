using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using WorkingWithWebApi2.Models.DomainEntities;

namespace DataAccessLayer.DataTableConfigurations
{
    public class OrderDetailConfiguration : EntityTypeConfiguration<OrderDetail>
    {
        public OrderDetailConfiguration()
        {
            // Primary Key
            HasKey(p => new { p.OrderID, p.ProductID });

            // Fields
            Property(p => p.OrderID).IsRequired();
            Property(p => p.ProductID).IsRequired();
            Property(p => p.UnitPrice).IsRequired().HasPrecision(18, 2);
            Property(p => p.Quantity).IsRequired();
            Property(p => p.Discount).IsRequired().HasPrecision(18, 2);

            // Indexes
            Property(p => p.OrderID).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IDX_OrderDetailOrderID")));
            Property(p => p.ProductID).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IDX_OrderDetailProductID")));


            // Table
            ToTable("OrderDetail", "Northwind");
        }
    }
}
