using Drippyz.Data.Base;
using Drippyz.Models;

namespace Drippyz.Data.Services
{
    //define method signatures (interface = contract)
    //Define return type and method names 
    public interface IStoresService : IEntityBaseRepository<Store>
    {
        Task UpdateAsync(Store store);
    }
}
