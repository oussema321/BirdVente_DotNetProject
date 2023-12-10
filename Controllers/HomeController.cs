using BirdStore.Models;
using BirdStore.Models.Repositories.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BirdStore.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IBirdRepository _birdRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IBirdRepository birdRepository, ICategoryRepository categoryRepository, ILogger<HomeController> logger)
        {
            _birdRepository = birdRepository;
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            IEnumerable<Bird> productList = _birdRepository.GetAll(includeProperties: "Category");
            return View(productList);
        }

        public IActionResult Details(int productId)
        {
            Bird product = _birdRepository.Get(u => u.Id == productId, includeProperties: "Category");
            return View(product);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}