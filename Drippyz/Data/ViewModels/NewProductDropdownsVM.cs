using Drippyz.Models;

namespace Drippyz.Data.ViewModels
{
    public class NewProductDropdownsVM
    {
        public NewProductDropdownsVM()
        {
            Stores = new List<Store>();
        }
        public List<Store> Stores { get; set; }

    }
}
