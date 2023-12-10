using BirdStore.Models.Repositories.IRepository;
using BirdStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BirdStore.Models.Repositories;

namespace BirdStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public ShoppingCartController(IShoppingCartRepository shoppingCartRepository, UserManager<IdentityUser> userManager)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _userManager = userManager;
        }

        // Action to display the items in the shopping cart
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var cartItems = _shoppingCartRepository.GetAll().Where(cart => cart.UserId == userId);
            return View(cartItems);
        }

        // Action to add an item to the shopping cart
        public IActionResult AddToCart(int birdId)
        {
            var userId = _userManager.GetUserId(User);
            var shoppingCartItem = new ShoppingCart { UserId = userId, BirdId = birdId, OrderId = 0 };
            _shoppingCartRepository.Add(shoppingCartItem);
            _shoppingCartRepository.Save();

            return RedirectToAction("Index");
        }

        // Action to remove an item from the shopping cart
    


    }
}
