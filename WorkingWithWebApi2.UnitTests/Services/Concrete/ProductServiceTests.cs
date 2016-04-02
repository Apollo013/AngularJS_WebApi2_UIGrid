using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using WorkingWithWebApi2.BusinessServices.ModelFactories.Concrete;
using WorkingWithWebApi2.Models.ViewModels.Products;

namespace WorkingWithWebApi2.BusinessServices.Services.Concrete.Tests
{
    [TestClass()]
    public class ProductServiceTests
    {
        private ProductService service;

        public ProductService Service
        {
            get
            {
                if (service == null)
                {
                    service = new ProductService(new ProductModelFactory());
                }
                return service;
            }
        }

        [TestMethod()]
        public void GetProductByIdTest()
        {
            // Arrange
            ProductEditVM product = null;
            ProductEditVM product2 = null;
            ProductEditVM product3 = null;

            // Act
            product = Service.GetById(5);
            try
            {
                product2 = Service.GetById(-1);
                product3 = Service.GetById(1000);
            }
            catch (Exception ex) when (ex is ArgumentOutOfRangeException || ex is InvalidOperationException || ex is NullReferenceException)
            {
                Assert.IsNull(product2);
                Assert.IsNull(product3);
            }

            // Assert
            Assert.IsNotNull(product);
            Assert.AreEqual("Chef Anton's Gumbo Mix", product.ProductName);
        }

        [TestMethod()]
        public void GetProductsTest()
        {
            // Arrange
            IEnumerable<ProductListVM> products = null;

            // Act
            products = Service.GetAll();

            // Assert
            Assert.IsNotNull(products);            
        }
    }
}