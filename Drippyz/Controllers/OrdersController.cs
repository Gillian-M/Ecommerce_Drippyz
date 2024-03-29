﻿using Drippyz.Data.Cart;
using Drippyz.Data.Services;
using Drippyz.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Drippyz.Controllers
{
    
    public class OrdersController : Controller
    {
        
        private readonly IProductsService _productsService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrdersService _ordersService;
        public OrdersController(IProductsService productsService, ShoppingCart shoppingCart, IOrdersService ordersService)
        {
            _productsService = productsService;
            _shoppingCart = shoppingCart;
            _ordersService = ordersService;
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);
            var orders = await _ordersService.GetOrdersByUserIdAndRolesAsync(userId, userRole);
            return View(orders);
        }

        //add Item to shopping cart 
        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(response);
        }
        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var item = await _productsService.GetProductByIdAsync(id);

            if(item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }
            return base.RedirectToAction(nameof(ShoppingCart));
        }

        //remove from shopping cart 
        public async Task<IActionResult> RemoveItemFromCart(int id)
        {
            var item = await _productsService.GetProductByIdAsync(id);

            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }
            return base.RedirectToAction(nameof(ShoppingCart));
            
        }


        //  Complete Order method
        //service injected in constructor 

        public async Task<IActionResult> CompleteOrder()
        {

            var items = _shoppingCart.GetShoppingCartItems();
            
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

            await _ordersService.StoreOrderAsync(items, userId, userEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();

            return View("CompleteOrder");

        }

        
    }
}
