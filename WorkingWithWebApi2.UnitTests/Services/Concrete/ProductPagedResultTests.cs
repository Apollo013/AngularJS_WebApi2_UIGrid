using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using WorkingWithWebApi2.BusinessServices.ModelFactories.Concrete;
using WorkingWithWebApi2.Models.ViewModels.Products;
using WorkingWithWebApi2.Models.ViewModels.Shared;
using WorkingWithWebApi2.UnitTests.CheckExtensions;

namespace WorkingWithWebApi2.BusinessServices.Services.Concrete.Tests
{
    [TestClass]
    public class ProductPagedResultTests
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
        public void GetPageResult_TestPaginationParameters()
        {
            // Act
            try
            {
                PagedResultViewModel<ProductListVM> pagedResult = Service.GetPageResult(1, 0);
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.IsTrue(true);
            }

            try
            {
                PagedResultViewModel<ProductListVM> pagedResult1 = Service.GetPageResult(0, 1);
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.IsTrue(true);
            }

            PagedResultViewModel<ProductListVM> pagedResult2 = Service.GetPageResult(2, 25);
            PagedResultViewModel<ProductListVM> pagedResult3 = Service.GetPageResult(3, 5);
            PagedResultViewModel<ProductListVM> pagedResult4 = Service.GetPageResult(4, 25);     // Only 77 records so should only return 2
            PagedResultViewModel<ProductListVM> pagedResult5 = Service.GetPageResult(1, 50);
            PagedResultViewModel<ProductListVM> pagedResult6 = Service.GetPageResult(1, 100);    // Only 77 records so should only return 77
                                                                                     
            try
            {
                PagedResultViewModel<ProductListVM> pagedResult7 = Service.GetPageResult(5, 25); // SHOULD THROW NULL
                Assert.Fail();
            }
            catch (NullReferenceException ex)
            {
                Assert.IsTrue(true);
            }

            // Assert          
            Assert.IsTrue(pagedResult2.Items.Count().InRange(1, 25));
            Assert.IsTrue(pagedResult3.Items.Count().InRange(1, 25));
            Assert.IsTrue(pagedResult4.Items.Count().InRange(1, 25));
            Assert.IsTrue(pagedResult5.Items.Count().InRange(1, 50));
            Assert.IsTrue(pagedResult6.Items.Count().InRange(1, 100));

            Assert.IsTrue(pagedResult2.PageCount.InRange(1, 5));
            Assert.IsTrue(pagedResult2.TotalRecordCount.InRange(1, 100));
            Assert.AreEqual(2, pagedResult2.PageNo);
            Assert.AreEqual(25, pagedResult2.PageSize);

            // We want to see if the products are ordered by the default criteria
            printItems(pagedResult5);
        }

        [TestMethod()]
        public void GetPageResult_TestFilter_ProductName()
        {
            // Arrange
            string paramFilter = "productName: cha";    // Should Pass
            string paramFilter1 = "productName: xxx";   // Shoud Fail (throw null)
            string paramFilter2 = "product Name: xxx";  // Shoud Fail (throw null)

            // Act
            PagedResultViewModel<ProductListVM> pagedResult = Service.GetPageResult(1, 25, orderBy: null, filter: paramFilter);
            try
            {
                PagedResultViewModel<ProductListVM> pagedResult1 = Service.GetPageResult(1, 25, orderBy: null, filter: paramFilter1);
                PagedResultViewModel<ProductListVM> pagedResult2 = Service.GetPageResult(1, 25, orderBy: null, filter: paramFilter2);
                PagedResultViewModel<ProductListVM> pagedResult3 = Service.GetPageResult(2, 25, orderBy: null, filter: paramFilter); // Should Fail Becasue we've requested the 2nd page and there are only 3 records
            }
            catch (NullReferenceException)
            {
                Assert.IsTrue(true);
            }

            // Assert
            Assert.IsTrue(pagedResult.Items.Count().InRange(1, 25));

            printItems(pagedResult);
        }

        [TestMethod()]
        public void GetPageResult_TestFilter_CategoryName()
        {
            // Arrange
            string paramFilter = "category.categoryName: bev";    // Should Pass
            string paramFilter1 = "category.categoryName: xxx";   // Shoud Fail (throw null)
            string paramFilter2 = "category. categoryName: bev";  // Shoud Fail (throw null)

            // Act
            PagedResultViewModel<ProductListVM> pagedResult = Service.GetPageResult(1, 25, orderBy: null, filter: paramFilter);
            try
            {
                PagedResultViewModel<ProductListVM> pagedResult1 = Service.GetPageResult(1, 25, orderBy: null, filter: paramFilter1);
                PagedResultViewModel<ProductListVM> pagedResult2 = Service.GetPageResult(1, 25, orderBy: null, filter: paramFilter2);
                PagedResultViewModel<ProductListVM> pagedResult3 = Service.GetPageResult(2, 25, orderBy: null, filter: paramFilter); // Should Fail Becasue we've requested the 2nd page and there are only 3 records
            }
            catch (NullReferenceException)
            {
                Assert.IsTrue(true);
            }

            // Assert
            Assert.IsTrue(pagedResult.Items.Count().InRange(1, 25));

            printItems(pagedResult);
        }

        [TestMethod()]
        public void GetPageResult_TestFilter_SupplierName()
        {
            // Arrange
            string paramFilter = "supplier.companyName:bigfoot";    // Should Pass
            string paramFilter1 = "supplier.companyName: xxx";      // Shoud Fail (throw null)
            string paramFilter2 = "supplier. companyName: bigfoot"; // Shoud Fail (throw null)

            // Act
            PagedResultViewModel<ProductListVM> pagedResult = Service.GetPageResult(1, 25, orderBy: null, filter: paramFilter);
            try
            {
                PagedResultViewModel<ProductListVM> pagedResult1 = Service.GetPageResult(1, 25, orderBy: null, filter: paramFilter1);
                PagedResultViewModel<ProductListVM> pagedResult2 = Service.GetPageResult(1, 25, orderBy: null, filter: paramFilter2);
                PagedResultViewModel<ProductListVM> pagedResult3 = Service.GetPageResult(2, 25, orderBy: null, filter: paramFilter); // Should Fail Becasue we've requested the 2nd page and there are only 3 records
            }
            catch (NullReferenceException)
            {
                Assert.IsTrue(true);
            }

            // Assert
            Assert.IsTrue(pagedResult.Items.Count().InRange(1, 25));

            printItems(pagedResult);
        }

        [TestMethod()]
        public void GetPageResult_TestFilter_Discontinued()
        {
            string paramFilter = "discontinued:t";             // Should return only discontinued records
            string paramFilter1 = "discontinued: true";         // Should return only discontinued records
            string paramFilter2 = "discontinued: fa";           // Should return only active records
            string paramFilter3 = "discontinued:false";         // Should return only active records
            string paramFilter4 = "discontinued: fair play";    // Fail - Should return both
            string paramFilter5 = "discontinued: truth";        // Fail - Should return both

            PagedResultViewModel<ProductListVM> pagedResult = Service.GetPageResult(1, 10, orderBy: null, filter: paramFilter);
            PagedResultViewModel<ProductListVM> pagedResult1 = Service.GetPageResult(1, 25, orderBy: null, filter: paramFilter1);
            PagedResultViewModel<ProductListVM> pagedResult2 = Service.GetPageResult(1, 10, orderBy: null, filter: paramFilter2);
            PagedResultViewModel<ProductListVM> pagedResult3 = Service.GetPageResult(2, 25, orderBy: null, filter: paramFilter3);
            PagedResultViewModel<ProductListVM> pagedResult4 = Service.GetPageResult(1, 100, orderBy: null, filter: paramFilter4);
            PagedResultViewModel<ProductListVM> pagedResult5 = Service.GetPageResult(1, 100, orderBy: null, filter: paramFilter5);

            Assert.IsTrue(pagedResult.Items.Count().InRange(1, 10));
            Assert.IsTrue(pagedResult1.Items.Count().InRange(1, 25));
            Assert.IsTrue(pagedResult2.Items.Count().InRange(1, 10));
            Assert.IsTrue(pagedResult3.Items.Count().InRange(1, 25));

            // -------------------------------------------------------------------------------------------------------
            // To test the last to paged results, we need to iterate through the items
            // to check that we have both true & false values for the discontinued property
            // -------------------------------------------------------------------------------------------------------
            int falseCount = 0, trueCount = 0;

            foreach (ProductListVM product in pagedResult4.Items)
            {
                if (product.Discontinued == true)
                {
                    trueCount++;
                }
                else
                {
                    falseCount++;
                }
            }

            Assert.IsTrue(trueCount > 0 && falseCount > 0);

            // -------------------------------------------------------------------------------------------------------
            // Do the same for pagedresult5
            // -------------------------------------------------------------------------------------------------------
            falseCount = 0;
            trueCount = 0;

            foreach (ProductListVM product in pagedResult5.Items)
            {
                if (product.Discontinued == true)
                {
                    trueCount++;
                }
                else
                {
                    falseCount++;
                }
            }

            Assert.IsTrue(trueCount > 0 && falseCount > 0);

            // -------------------------------------------------------------------------------------------------------
            // Print Items To Check
            // -------------------------------------------------------------------------------------------------------
            printItems(pagedResult);
            printItems(pagedResult2);
        }

        [TestMethod]
        public void GetPagedResult_TestFilter_All()
        {
            //string paramFilter = "productName: cha,supplier.companyName:bigfoot,category.categoryName: bev,discontinued: false";
            string paramFilter1 = "category.categoryName: bev";
            string paramFilter2 = "category.categoryName: bev,discontinued: false";
            string paramFilter3 = "productName:C,category.categoryName: bev,discontinued: false";

            PagedResultViewModel<ProductListVM> pagedResult1 = Service.GetPageResult(1, 10, orderBy: null, filter: paramFilter1);
            PagedResultViewModel<ProductListVM> pagedResult2 = Service.GetPageResult(1, 10, orderBy: null, filter: paramFilter2);
            PagedResultViewModel<ProductListVM> pagedResult3 = Service.GetPageResult(1, 10, orderBy: null, filter: paramFilter3);

            bool passed = true;

            // -------------------------------------------------------------------------------------------------------
            // Make sure items only contain a category that contains "bev"
            // -------------------------------------------------------------------------------------------------------
            foreach (ProductListVM product in pagedResult1.Items)
            {
                if (!product.Category.CategoryName.Contains("Bev"))
                {
                    passed = false;
                }
            }
            Assert.IsTrue(passed);

            // -------------------------------------------------------------------------------------------------------
            // Make sure items only contain a category that contains "bev" and discontinued == false
            // -------------------------------------------------------------------------------------------------------
            foreach (ProductListVM product in pagedResult1.Items)
            {
                if (!product.Category.CategoryName.Contains("Bev") && product.Discontinued != false)
                {
                    passed = false;
                }
            }
            Assert.IsTrue(passed);

            // -------------------------------------------------------------------------------------------------------
            // Make sure items only contain a category that contains "bev" and discontinued == false & Product Name contains 'C'
            // -------------------------------------------------------------------------------------------------------
            foreach (ProductListVM product in pagedResult1.Items)
            {
                if (!product.ProductName.Contains("C") && !product.Category.CategoryName.Contains("Bev") && product.Discontinued != false)
                {
                    passed = false;
                }
            }
            Assert.IsTrue(passed);

            // -------------------------------------------------------------------------------------------------------
            // Print Items To Check
            // -------------------------------------------------------------------------------------------------------
            printItems(pagedResult3);
        }

        [TestMethod]
        public void GetPagedResult_TestOrderBy()
        {
            // Arrange
            string orderByCriteria = "category.categoryname, productname:desc"; // Category name shoul;d default to asc

            // Act
            PagedResultViewModel<ProductListVM> pagedResult = Service.GetPageResult(1, 100, orderBy: orderByCriteria, filter: null);

            // Assert
            Assert.IsTrue(pagedResult.Items.Count().InRange(1, 100));

            printItems(pagedResult);
        }


        [TestMethod]
        public void GetPagedResult_TestOrderBy_Filter()
        {
            // Arrange
            string orderByCriteria = "category.categoryname, productname:desc"; // Category name shoul;d default to asc
            string paramFilter = "productName:C,discontinued: false";

            // Act
            PagedResultViewModel<ProductListVM> pagedResult = Service.GetPageResult(1, 100, orderBy: orderByCriteria, filter: paramFilter);

            // Assert
            Assert.IsTrue(pagedResult.Items.Count().InRange(1, 100));

            printItems(pagedResult);
        }

        /// <summary>
        /// Private method for printing results
        /// </summary>
        /// <param name="list"></param>
        private void printItems(PagedResultViewModel<ProductListVM> list)
        {
            if (list == null)
            {
                Console.WriteLine("List is null");
                return;
            }

            foreach (ProductListVM model in list.Items)
            {
                Console.WriteLine($"{model.ProductName} : \tCategory: {model.Category.CategoryName} : \tSupplier: {model.Supplier.CompanyName} : \tDiscontinued: {model.Discontinued}");
            }
        }
    }
}
