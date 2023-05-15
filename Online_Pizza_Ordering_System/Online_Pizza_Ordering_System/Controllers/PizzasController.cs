using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Online_Pizza_Ordering_System.Repositories.Abstract;
using Online_Pizza_Ordering_System.Data;
using Online_Pizza_Ordering_System.Models;
using Online_Pizza_Ordering_System.Repositories;
using Online_Pizza_Ordering_System.ViewModel;

namespace Online_Pizza_Ordering_System.Controllers
{
    [Authorize]
    public class PizzasController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IPizzaRepositories _pizzaRepo;
        private readonly IStoresRepositories _storeRepo;
        private readonly IFileService _fileService;

        public PizzasController(AppDbContext context, IPizzaRepositories pizzaRepo, IStoresRepositories storeRepo, IFileService fileService)
        {
            _context = context;
            _pizzaRepo = pizzaRepo;
            _storeRepo = storeRepo;
            _fileService = fileService;
        }


        // GET: Pizzas
        public async Task<IActionResult> Index()
        {
            return View(await _pizzaRepo.GetAllIncludedAsync());
        }

        // GET: Pizzas
        [AllowAnonymous]
        public async Task<IActionResult> ListAll()
        {
            var model = new SearchPizzasViewModel()
            {
                PizzaList = await _pizzaRepo.GetAllIncludedAsync(),
                SearchText = null
            };

            return View(model);
        }

        private async Task<List<Pizzas>> GetPizzaSearchList(string userInput)
        {
            var userInputTrimmed = userInput?.ToLower()?.Trim();

            if (string.IsNullOrWhiteSpace(userInputTrimmed))
            {
                return await _context.Pizzas.Include(p => p.store)
                    .Select(p => p).OrderBy(p => p.Name)
                    .ToListAsync();
            }
            else
            {
                return await _context.Pizzas.Include(p => p.store)
                    .Where(p => p
                    .Name.ToLower().Contains(userInputTrimmed))
                    .Select(p => p).OrderBy(p => p.Name)
                    .ToListAsync();
            }
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> AjaxSearchList(string searchString)
        {
            var pizzaList = await GetPizzaSearchList(searchString);

            return PartialView(pizzaList);
        }


        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ListAll([Bind("SearchText")] SearchPizzasViewModel model)
        {
            var pizzas = await _pizzaRepo.GetAllIncludedAsync();
            if (model.SearchText == null || model.SearchText == string.Empty)
            {
                model.PizzaList = pizzas;
                return View(model);
            }

            var input = model.SearchText.Trim();
            if (input == string.Empty || input == null)
            {
                model.PizzaList = pizzas;
                return View(model);
            }
            var searchString = input.ToLower();

            if (string.IsNullOrEmpty(searchString))
            {
                model.PizzaList = pizzas;
            }
            else
            {
                var pizzaList = await _context.Pizzas.Include(x => x.store).OrderBy(x => x.Name).Where(p =>
                     p.Name.ToLower().Contains(searchString)
                  || p.Price.ToString("c").ToLower().Contains(searchString)
                  || p.store.StoreName.ToLower().Contains(searchString)).ToListAsync();

                if (pizzaList.Any())
                {
                    model.PizzaList = pizzaList;
                }
                else
                {
                    model.PizzaList = new List<Pizzas>();
                }

            }
            return View(model);
        }


        // GET: Pizzas
        [AllowAnonymous]
        public async Task<IActionResult> ListStores(string storeName)
        {
            bool storeExtist = _context.Stores.Any(c => c.StoreName == storeName);
            if (!storeExtist)
            {
                return NotFound();
            }

            var store = await _context.Stores.FirstOrDefaultAsync(c => c.StoreName == storeName);

            if (store == null)
            {
                return NotFound();
            }

            bool anyPizzas = await _context.Pizzas.AnyAsync(x => x.store == store);
            if (!anyPizzas)
            {
                return NotFound($"No Pizzas were found in the category: {storeName}");
            }

            var pizzas = _context.Pizzas.Where(x => x.store == store)
                .Include(x => x.store);

            ViewBag.CurrentStore = store.StoreName;
            return View(pizzas);
        }

        // GET: Pizzas/Details/
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzas = await _pizzaRepo.GetByIdIncludedAsync(id);

            if (pizzas == null)
            {
                return NotFound();
            }

            return View(pizzas);
        }



        // GET: Pizzas/Details/
        [AllowAnonymous]
        public async Task<IActionResult> DisplayDetails(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzas = await _pizzaRepo.GetByIdIncludedAsync(id);

            //var listOfIngredients = await _context.PizzaIngredients.Where(x => x.PizzaId == id).Select(x => x.Ingredient.Name).ToListAsync();
            //ViewBag.PizzaIngredients = listOfIngredients;

            //var listOfReviews = await _context.Reviews.Where(x => x.PizzaId == id).Select(x => x).ToListAsync();
            //ViewBag.Reviews = listOfReviews;
            //double score;
            //if (_context.Reviews.Any(x => x.PizzaId == id))
            //{
            //    var review = _context.Reviews.Where(x => x.PizzaId == id);
            //    score = review.Average(x => x.Grade);
            //    score = Math.Round(score, 2);
            ////}
            //else
            //{
            //    score = 0;
            //}
            //ViewBag.AverageReviewScore = score;

            if (pizzas == null)
            {
                return NotFound();
            }

            return View(pizzas);
        }



        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchPizzas([Bind("SearchText")] SearchPizzasViewModel model)
        {
            var pizzas = await _pizzaRepo.GetAllIncludedAsync();
            var search = model.SearchText.ToLower();

            if (string.IsNullOrEmpty(search))
            {
                model.PizzaList = pizzas;
            }
            else
            {
                var pizzaList = await _context.Pizzas.Include(x => x.store).OrderBy(x => x.Name)
                    .Where(p =>
                     p.Name.ToLower().Contains(search)
                  || p.Price.ToString("c").ToLower().Contains(search)
                  || p.store.StoreName.ToLower().Contains(search)).ToListAsync();

                if (pizzaList.Any())
                {
                    model.PizzaList = pizzaList;
                }
                else
                {
                    model.PizzaList = new List<Pizzas>();
                }

            }
            return View(model);
        }



        // GET: Pizzas/Create
        public IActionResult Create()
        {
            ViewData["StoreId"] = new SelectList(_storeRepo.GetAll(), "StoreId", "StoreName");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Description,ImageFile,StoreId,PizzaUrl")] Pizzas pizzas)
        {
            if (ModelState.IsValid)
            {
                var fileReult = this._fileService.SaveImage(pizzas.ImageFile);
                if (fileReult.Item1 == 0)
                {
                    TempData["msg"] = "File could not saved";
                    return View("Create");
                }
                var imageName = fileReult.Item2;
                pizzas.PizzaUrl = imageName;
                _pizzaRepo.Add(pizzas);
                await _pizzaRepo.SaveChangesAsync();
                return RedirectToAction("Index","Stores");
            }
            ViewData["StoreId"] = new SelectList(_storeRepo.GetAll(), "StoreId", "StoreName", pizzas.StoreId);
            return View(pizzas);
        }



        // GET: Pizzas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzas = await _pizzaRepo.GetByIdAsync(id);

            if (pizzas == null)
            {
                return NotFound();
            }
            ViewData["StoreId"] = new SelectList(_storeRepo.GetAll(), "Id", "Name", pizzas.StoreId);
            return View(pizzas);
        }


        // POST: Pizzas/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Price,Description,PizzaUrl,ImageFile,StoreId")] Pizzas pizzas)
        {
            if (id != pizzas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var fileReult = this._fileService.SaveImage(pizzas.ImageFile);
                    if (fileReult.Item1 == 0)
                    {
                        TempData["msg"] = "File could not saved";
                        return View("Create");
                    }
                    var imageName = fileReult.Item2;
                    pizzas.PizzaUrl = imageName;
                    _pizzaRepo.Update(pizzas);
                    await _pizzaRepo.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PizzasExists(pizzas.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["StoreId"] = new SelectList(_storeRepo.GetAll(), "StoreId", "StoreName", pizzas.StoreId);
            return View(pizzas);
        }



        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizzas = await _pizzaRepo.GetByIdIncludedAsync(id);

            if (pizzas == null)
            {
                return NotFound();
            }

            return View(pizzas);
        }


        // POST: Pizzas/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var pizzas = await _pizzaRepo.GetByIdAsync(id);
            _pizzaRepo.Remove(pizzas);
            await _pizzaRepo.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PizzasExists(Guid id)
        {
            return _pizzaRepo.Exists(id);
        }

    }
}
