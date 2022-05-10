using Drippyz.Controllers;
using Drippyz.Data;
using Drippyz.Data.Services;
using Drippyz.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;

namespace UnitTestDrippyz_1
{


    public class ProductsServiceTest
    {



        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "Drippyz_UnitTestDb")
            .Options;

        AppDbContext context;
        ProductsService productsService;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new AppDbContext(dbContextOptions);
            context.Database.EnsureCreated();

            SeedDatabase();

            productsService = new ProductsService(context);
        }


        [Test, Order(1)]
        public void GetProductByIdAsync_WithResponse_Test()
        {
            var result = productsService.GetProductByIdAsync(1);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(result);

        }


        [Test, Order(2)]
        public void AddNewProductAsync_WithoutResponse_Test()
        {
            var result = productsService.GetProductByIdAsync(100);

            Assert.That(result, Is.Not.Null);
        }

        [Test, Order(3)]
        public void AddNewProductAsync_WithoutException_Test()
        {
            var newProduct = new NewProductVM()
            {
                Name = "Without Ice cream"
            };

            var result = productsService.AddNewProductAsync(newProduct);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.Not.Null);
        }





        //Destory database after testing 

        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }
        private void SeedDatabase()
        {
            if (!context.Products.Any())
            {
                context.Products.AddRange(new List<Product>()
                    {

                        new Product()
                        {

                            Name = "Vanilla",
                            Description = "500ML Home made ice cream.",
                            Price = 11.00,
                            ImageURL = "https://sample/images/products/product-1",
                            ProductCategory = ProductCategory.Bar,
                            StoreId = 1
                        },

                        new Product()
                        {

                            Name = "Chocolte",
                            Description = "500ML Home made Chocolate ice cream.",
                            Price = 12.00,
                            ImageURL = "https://sample/images/products/product-2",
                            ProductCategory = ProductCategory.Bar,
                            StoreId = 1

                        },
                     });
                context.SaveChanges();
            }
        }
    }

}