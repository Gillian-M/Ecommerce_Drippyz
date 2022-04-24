using Drippyz.Data.Base;
using Drippyz.Models;
using Microsoft.AspNetCore.Mvc;

namespace Drippyz.Data.Services
{
    public class StoresService : EntityBaseRepository<Store>, IStoresService

    {

        public StoresService(AppDbContext context) : base(context)
        {
        }

        public Task UpdateAsync(Store store)
        {
            throw new NotImplementedException();
        }
    }
}
