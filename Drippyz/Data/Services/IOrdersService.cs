using Drippyz.Models;

namespace Drippyz.Data.Services
{
    public interface IOrdersService
    {
        Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress);

        Task<List<Order>> GetOrdersByUserIdAndRolesAsync(string userId, string userRole);
    }
}
