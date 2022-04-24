
using Drippyz.Data;
using Drippyz.Data.Services;
using Drippyz.Models;
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

        //Search functionality 
        public async Task<IActionResult> Filter(string searchString)
        {
            var allProducts = await _service.GetAllAsync(n => n.Store);
            if(!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allProducts.Where(n => n.Name.Contains(searchString) || n.Description.Contains(searchString)).ToList();    
                return View("Index", filteredResult);
            }

            return View("Index", allProducts);

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

        [HttpPost]

        public async Task<IActionResult> Create(NewProductVM product)
        {
            if (!ModelState.IsValid)
            {
                var productDropdownsData = await _service.GetNewProductDropdownsValues();
                ViewBag.Stores = new SelectList(productDropdownsData.Stores, "Id", "Name");
              
                return View(product);
            }
            await _service.AddNewProductAsync(product);
            return RedirectToAction(nameof(Index));
        }

   

        //Get Product/Edi/1
        public async Task<IActionResult> Edit(int id)
        {
            var productDetails = await _service.GetProductByIdAsync(id);
            if (productDetails == null) return View("NotFound");

            var response = new NewProductVM()
            {
                Id = productDetails.Id,
                Name = productDetails.Name,
                Description = productDetails.Description,
                Price = productDetails.Price,
                ImageURL = productDetails.ImageURL,
                ProductCategory = productDetails.ProductCategory,
                StoreId = productDetails.StoreId

            };

            //data for the dropdown
            var productDropdownsData = await _service.GetNewProductDropdownsValues();
            ViewBag.Stores = new SelectList(productDropdownsData.Stores, "Id", "Name");
            //have prefilled fields before editing/ updating a product 
            return View(response);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(int id, NewProductVM product)
        {
            if (id != product.Id) return View("NotFound");
            if (!ModelState.IsValid)
            {
                var productDropdownsData = await _service.GetNewProductDropdownsValues();
                ViewBag.Stores = new SelectList(productDropdownsData.Stores, "Id", "Name");

                return View(product);
            }
            await _service.UpdateProductAsync(product);
            return RedirectToAction(nameof(Index));
        }

    }
}
