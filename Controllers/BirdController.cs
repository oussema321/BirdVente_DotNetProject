using BirdStore.Models;
using BirdStore.Models.Repositories.IRepository;
using BirdStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BirdStore.Controllers
{
    [Authorize]
    public class BirdController : Controller
    {

        private readonly IBirdRepository _birdRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BirdController(IBirdRepository birdRepository, ICategoryRepository categoryRepository, IWebHostEnvironment webHostEnvironment)
        {
            _birdRepository = birdRepository;
            _categoryRepository = categoryRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: CategoryController1
        public ActionResult Index()
        {
            List<Bird> objCategoryList = _birdRepository.GetAll(includeProperties: "Category").ToList();

            return View(objCategoryList);
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            var product = _birdRepository.Get(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }


        // GET: CategoryController/Create
        public ActionResult Upsert(int? id)
        {
            // Initialize the CategoryList and Bird properties of BirdVM
            BirdVM birdVM = new BirdVM
            {
                CategoryList = _categoryRepository.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Bird = new Bird() // Initialize the Bird property
            };

            if (id != null && id != 0)
            {
                // Update
                birdVM.Bird = _birdRepository.Get(u => u.Id == id);
            }

            return View(birdVM);
        }



        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upsert(BirdVM birdVM, IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    if (file != null)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string productPath = Path.Combine(wwwRootPath, @"images\bird");

                        if (!string.IsNullOrEmpty(birdVM.Bird.ImageUrl))
                        {
                            var oldImagePath = Path.Combine(wwwRootPath, birdVM.Bird.ImageUrl.TrimStart('\\'));
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }


                        using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                        birdVM.Bird.ImageUrl = @"\images\bird\" + fileName;

                    }
                    if (birdVM.Bird.Id == 0)
                    {
                        _birdRepository.Add(birdVM.Bird);
                    }
                    else
                    {
                        _birdRepository.Update(birdVM.Bird);
                    }

                    _birdRepository.Save();
                    return RedirectToAction("Index");
                }
                else
                {
                    birdVM.CategoryList = _categoryRepository.GetAll().Select(u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString()
                    });

                    return View(birdVM);
                }
            }
            catch
            {
                return View();
            }
        }


        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Bird> objProductList = _birdRepository.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objProductList });
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productToBeDeleted = _birdRepository.Get(u => u.Id == id);
            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, productToBeDeleted.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _birdRepository.Remove(productToBeDeleted);
            _birdRepository.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion

    }
}