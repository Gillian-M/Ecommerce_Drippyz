using System.Collections.Generic;
using System.Web.Mvc;
using Drippyz.Data;
using Drippyz.Data.Services;
using Drippyz.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace UnitTestDrippyz_1.Controllers
{
    [TestClass]
    public class TestStoresController
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "StoresControllerTest")
            .Options;

        AppDbContext context;
        StoresService storesService;
        

        [OneTimeSetUp]
        public void Setup()
        {
            context = new AppDbContext(dbContextOptions);
            context.Database.EnsureCreated();

            SeedDatabase();

            
            storesService = new StoresService(context);
            
        }
            
        [Test, Order(1)]
        public void Test_CreateStore_WithResponse()
        {
            
            Store store = new Store() { Id = 5, Name = "Test create", Description = "Test description", Glyph = "TestGlyph1.PNG" };

            var result = storesService.AddAsync(store);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(result);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(store.Name, "Test create");
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(store.Id, 5);

        }

        [Test, Order(2)]
        public void GetStoreByIdAsync_WithResponse_Test()
        {
            var result = storesService.GetByIdAsync(1);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(result);
            

        }

        [Test, Order(3)]
        public void Test_DeleteStore_WithResponse()
        {

            var result = storesService.DeleteAsync(5);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(result);
        }

        [Test, Order(4)]
        public void GetStoreByIdAsync_WithResponse_TestFail()
        {
            var result = storesService.GetByIdAsync(9999);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNull(result); 

        }

        [Test, Order(5)]
        public void GetAllAsync_WithResponse_Test()
        {
            var result = storesService.GetAllAsync();

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(result);

        }


        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }


        public void SeedDatabase()
        {
            var stores = new List<Store>
            { 
                new Store() { Id = 1, Name = "TestStore1", Description = "TestDescription1", Glyph = "TestGlyph1.PNG" },
                new Store() { Id = 2, Name = "TestStore2", Description = "TestDescription2", Glyph = "TestGlyph2.PNG" },
                new Store() { Id = 3, Name = "TestStore3", Description = "TestDescription3", Glyph = "TestGlyph3.PNG" },
                new Store() { Id = 4, Name = "TestStore4", Description = "TestDescription4", Glyph = "TestGlyph4.PNG" }
            };
            context.Stores.AddRange(stores);

            context.SaveChanges();

        }


    }
}
