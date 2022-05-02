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
        //constructor
        public StoresController(IStoresService service)
        {
            _service= service;
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
        public async Task<IActionResult> Create([Bind("Glyph,Name,Description")] Store store)
        {
            if (!ModelState.IsValid) return View(store);
            await _service.AddAsync(store);
            return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Glyph,Name,Description")] Store store)
        {
            if (!ModelState.IsValid) return View(store);
            await _service.UpdateAsync(id, store);
            return RedirectToAction(nameof(Index));
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