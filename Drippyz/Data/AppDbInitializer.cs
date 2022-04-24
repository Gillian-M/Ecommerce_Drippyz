using Drippyz.Models;

namespace Drippyz.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                if (!context.Stores.Any())
                {
                    context.Stores.AddRange(new List<Store>()
                    {
                        new Store()
                        {

                            Glyph = "https://www.dotnethow.net/images/stores/store-2",
                            Name = "test upasted",
                            Description = "update description oop"
                        },
                    });
                    context.SaveChanges();
                }

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
                            StoreId = 3
                        },

                        new Product()
                        {

                            Name = "Chocolte",
                            Description = "500ML Home made Chocolate ice cream.",
                            Price = 12.00,
                            ImageURL = "https://sample/images/products/product-2",
                            ProductCategory = ProductCategory.Bar,
                            StoreId = 3

                        },
                     });
                    context.SaveChanges();
                }


            }
        }
    }
}
