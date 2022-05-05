
using Drippyz.Data;
using Drippyz.Data.Services;
using Drippyz.Data.Static;
using Drippyz.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Drippyz.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ProductsController : Controller
    {

        //declare app db context 
        private readonly IProductsService _service;
        private IWebHostEnvironment _environment;
        //constructor

        public ProductsController(IProductsService service, IWebHostEnvironment environment)
        {
            _service = service;
            _environment = environment;
        }

        //default action result 
        //var data = return products in this controller and also  pass the data as a parameter to the view
        //Asynchronous method with parameters
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allProducts = await _service.GetAllAsync(n => n.Store);
            return View(allProducts);

        }

        //Search functionality 
        [AllowAnonymous]
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
        [AllowAnonymous]
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

        public async Task<IActionResult> Create(NewProductVM product, [FromForm] IFormFile imageURL)
        {
            var productDropdownsData = await _service.GetNewProductDropdownsValues();
            ViewBag.Stores = new SelectList(productDropdownsData.Stores, "Id", "Name");
            ModelState.Remove("ImageURL");
            if (ModelState.IsValid)
            {
                if (imageURL != null)
                {
                    var name = Path.Combine(_environment.WebRootPath + "/Images", Path.GetFileName(imageURL.FileName));
                    await imageURL.CopyToAsync(new FileStream(name, FileMode.Create));
                    product.ImageURL = "Images/" + imageURL.FileName;
                }

                if (imageURL == null)
                {
                    product.ImageURL = "Images/noimage.PNG";
                }
                await _service.AddNewProductAsync(product);
                return RedirectToAction(nameof(Index));
                
            }
            return View(product);
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

        public async Task<IActionResult> Edit(int id, NewProductVM product, [FromForm] IFormFile imageURL)
        {
            var productDropdownsData = await _service.GetNewProductDropdownsValues();
            ViewBag.Stores = new SelectList(productDropdownsData.Stores, "Id", "Name");
            
            if (id != product.Id) return View("NotFound");
            
            ModelState.Remove("ImageURL");
            if (ModelState.IsValid)
            {
                if (imageURL != null)
                {
                    var name = Path.Combine(_environment.WebRootPath + "/Images", Path.GetFileName(imageURL.FileName));
                    await imageURL.CopyToAsync(new FileStream(name, FileMode.Create));
                    product.ImageURL = "Images/" + imageURL.FileName;
                }

                if (imageURL == null)
                {
                    product.ImageURL = "Images/noimage.PNG";
                }
                await _service.UpdateProductAsync(product);
                return RedirectToAction(nameof(Index));

                
            }
            return View(product);
        }

        //Product Delete

        //of products exists in database call the delete confirm method

        public async Task<IActionResult> Delete(int id)
        {
            var productDetails = await _service.GetByIdAsync(id);
            if (productDetails == null) return View("NotFound");
            return View(productDetails);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {

            var productDetails = await _service.GetByIdAsync(id);
            if (productDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
