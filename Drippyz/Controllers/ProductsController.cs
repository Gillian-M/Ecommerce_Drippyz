
using Drippyz.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Drippyz.Controllers
{
    public class ProductsController : Controller
    {

        //declare app db context 
        private readonly AppDbContext _context;
        //constructor
        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        //default action result 
        //var data = return products in this controller and also  pass the data as a parameter to the view
        //Asynchronous method with parameters
        public async Task<IActionResult> Index()
        {
            var allProducts = await _context.Products.ToListAsync();
            return View(allProducts);

        }
        /*
        //Action Get request 
        public async Task<IActionResult> Details(int id)
        {
            var productDetail = await _service.GetByIdAsync(id);
            return View(productDetail);
        }

        //Get Product/Create
        public IActionResult Create()
        {
            ViewData["Welcome"] = "Welcome to our store";
            ViewBag.Description = "This is the store description.";
            return View();
        }*/
    }
}
