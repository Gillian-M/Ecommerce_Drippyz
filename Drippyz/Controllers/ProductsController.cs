
using Drippyz.Data;
using Drippyz.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Drippyz.Controllers
{
    public class ProductsController : Controller
    {

        //declare app db context 
        private readonly IProductsService _service;
        //constructor

        public ProductsController(IProductsService service)
        {
            _service = service;
        }

        //default action result 
        //var data = return products in this controller and also  pass the data as a parameter to the view
        //Asynchronous method with parameters
        public async Task<IActionResult> Index()
        {
            var allProducts = await _service.GetAllAsync(n => n.Store);
            return View(allProducts);

        }
        
        //Action Get request 
        public async Task<IActionResult> Details(int id)
        {
            var productDetail = await _service.GetProductByIdAsync(id);
            return View(productDetail);
        }
        

        //Get Product/Create
        public async Task<IActionResult> Create()
        {
            var productDropdownsData = await _service.GetNewProductDropdownsValues();

            ViewBag.Stores = new SelectList(productDropdownsData.Stores, "Id", "Name");
            return View();
        }
    }
}
