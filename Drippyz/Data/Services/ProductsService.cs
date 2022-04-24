using Drippyz.Data.Base;
using Drippyz.Data.ViewModels;
using Drippyz.Models;
using Microsoft.EntityFrameworkCore;

namespace Drippyz.Data.Services
{
    public class ProductsService : EntityBaseRepository<Product>, IProductsService
    {
        private readonly AppDbContext _context;
        public ProductsService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        //Create Product Method
        public async Task AddNewProductAsync(NewProductVM data)
        {
            var newProduct = new Product()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                ProductCategory = data.ProductCategory,
                StoreId = data.StoreId
            };
            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();
        }

        public async Task<NewProductDropdownsVM> GetNewProductDropdownsValues()
        {
            var response = new NewProductDropdownsVM()
            {

                Stores = await _context.Stores.OrderBy(n => n.Name).ToListAsync(),

            };
            return response;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var ProductDetails = await _context.Products
            .Include(s => s.Store)
            .FirstOrDefaultAsync(n => n.Id == id);

            return ProductDetails;
        }

        public async Task UpdateProductAsync(NewProductVM data)
        {
            var dbProduct = await _context.Products.FirstOrDefaultAsync(n => n.Id == data.Id);
            if (dbProduct != null)
            {
                dbProduct.Name = data.Name;
                dbProduct.Description = data.Description;
                dbProduct.Price = data.Price;
                dbProduct.ImageURL = data.ImageURL;
                dbProduct.ProductCategory = data.ProductCategory;
                dbProduct.StoreId = data.StoreId;
                await _context.SaveChangesAsync();
            };

        }
    }
}



