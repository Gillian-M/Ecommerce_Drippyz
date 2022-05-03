using Drippyz.Data;
using Drippyz.Data.Services;
using Drippyz.Data.Static;
using Drippyz.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Drippyz.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class StoresController : Controller
    {
        // //inject IStore service 
        private readonly IStoresService _service;
        private IWebHostEnvironment _environment;
        //constructor
        public StoresController(IStoresService service, IWebHostEnvironment environment)
        {
            _service= service;
            _environment= environment;
        }



        //default action result 
        //var data = return store in this controller and also  pass the data as a parameter to the view
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allStores = await _service.GetAllAsync();
            return View(allStores);
        }
        
        //Get Request Create View 
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        
        public async Task<IActionResult> Create( Store store, [FromForm] IFormFile glyph)
        {
            if (ModelState.IsValid)
            {
                if (glyph != null)
                {
                    var name = Path.Combine(_environment.WebRootPath + "/Images", Path.GetFileName(glyph.FileName));
                    await glyph.CopyToAsync(new FileStream(name, FileMode.Create));
                    store.Glyph = "Images/" + glyph.FileName;
                }

                if (glyph == null)
                {
                    store.Glyph = "Images/noimage.PNG";
                }
                await _service.AddAsync(store);
                return RedirectToAction(nameof(Index));

            }
            return View(store);

            
        }
        //Get: Stores Details
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var storeDetails = await _service.GetByIdAsync(id);
            if (storeDetails == null) return View("NotFound");
            return View(storeDetails);





        }
        //Get the store details edit Id
        //Post request after the details are updated
        public async Task<IActionResult> Edit(int id)
        {
            var storeDetails = await _service.GetByIdAsync(id);
            if (storeDetails == null) return View("NotFound");
            return View(storeDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Glyph,Name,Description")] Store store, IFormFile glyph)
        {
            if (ModelState.IsValid)
            {
                if (glyph != null)
                {
                    var name = Path.Combine(_environment.WebRootPath + "/Images", Path.GetFileName(glyph.FileName));
                    await glyph.CopyToAsync(new FileStream(name, FileMode.Create));
                    store.Glyph = "Images/" + glyph.FileName;
                }

                if (glyph == null)
                {
                    store.Glyph = "Images/noimage.PNG";
                }
                await _service.UpdateAsync(id, store);
                return RedirectToAction(nameof(Index));
            }
            return View(store);

        }


        //Store Delete
     
        //of store exists in database call the delete confirm method

        public async Task<IActionResult> Delete(int id)
        {
            var storeDetails = await _service.GetByIdAsync(id);
            if (storeDetails == null) return View("NotFound");
            return View(storeDetails);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            
            var storeDetails = await _service.GetByIdAsync(id);
            if (storeDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

///,[Bind("Id,Glyph,Name,Description")]Store store