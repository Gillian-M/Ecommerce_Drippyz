using Drippyz.Data.Base;
using Drippyz.Data.ViewModels;
using Drippyz.Models;

namespace Drippyz.Data.Services
{
    public interface IProductsService : IEntityBaseRepository<Product>
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<NewProductDropdownsVM> GetNewProductDropdownsValues();

        Task AddNewProductAsync(NewProductVM data);

        Task UpdateProductAsync(NewProductVM data);
    }
}
