using Drippyz.Data.Static;
using Drippyz.Models;
using Microsoft.AspNetCore.Identity;

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

                //if (!context.Stores.Any())
                //{
                //    context.Stores.AddRange(new List<Store>()
                //    {
                //        new Store()
                //        {

                //            Glyph = "https://www.dotnethow.net/images/stores/store-2",
                //            Name = "test upasted",
                //            Description = "update description oop"
                //        },
                //    });
                //    context.SaveChanges();
                //}

                //if (!context.Products.Any())
                //{
                //    context.Products.AddRange(new List<Product>()
                //    {

                //        new Product()
                //        {

                //            Name = "Vanilla",
                //            Description = "500ML Home made ice cream.",
                //            Price = 11.00,
                //            ImageURL = "https://sample/images/products/product-1",
                //            ProductCategory = ProductCategory.Bar,
                //            StoreId = 1
                //        },

                //        new Product()
                //        {

                //            Name = "Chocolte",
                //            Description = "500ML Home made Chocolate ice cream.",
                //            Price = 12.00,
                //            ImageURL = "https://sample/images/products/product-2",
                //            ProductCategory = ProductCategory.Bar,
                //            StoreId = 1

                //        },
                //     });
                //    context.SaveChanges();
                //}


            }
        }

        public static async Task SeedUserAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                // Roles section

                    if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                        await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                    if (!await roleManager.RoleExistsAsync(UserRoles.User))
                        await roleManager.CreateAsync(new IdentityRole(UserRoles.User));


                // User section
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "admind@drippyz.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Admin User",
                        UserName = "admin-user",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }


                string appUserEmail = "user@drippyz.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "Application User",
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }

            }
        }      
                    
                    
    }
}
