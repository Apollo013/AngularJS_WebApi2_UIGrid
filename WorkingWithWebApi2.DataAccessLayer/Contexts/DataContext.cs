using DataAccessLayer.DataTableConfigurations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WorkingWithWebApi2.Models.DomainEntities;

namespace WorkingWithWebApi2.DataAccessLayer.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext() : base("AzureConnection")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CategoryConfiguration());
            modelBuilder.Configurations.Add(new SupplierConfiguration());
            modelBuilder.Configurations.Add(new ProductConfiguration());
            modelBuilder.Configurations.Add(new CustomerConfiguration());
            modelBuilder.Configurations.Add(new OrderConfiguration());
            modelBuilder.Configurations.Add(new OrderDetailConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);
                
                var exceptionMessage = string.Concat(ex.Message, "A Validation Exception occured with the following: (", string.Join(", ", errorMessages), ")");
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
            catch (DbUpdateException ex)
            {
                List<string> entityNames = new List<string>();
                if (ex.Entries != null && ex.Entries.Any())
                {
                    foreach (var entry in ex.Entries)
                    {
                        if (entry.Entity != null)
                        {
                            entityNames.Add(entry.Entity.GetType().Name);
                        }
                    }
                }

                if (entityNames.Any())
                {
                    string exceptionMessage = string.Concat("An Update Exception occurred with the following: (", string.Join("," , entityNames), ")");
                    throw new DbUpdateException(exceptionMessage, ex.InnerException);
                }
                else
                {
                    throw;
                }
            }
        }

    }
}
