using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Pizza_Ordering_System.Repositories.Abstract;
using Online_Pizza_Ordering_System.Data;
using Online_Pizza_Ordering_System.Models;
using Online_Pizza_Ordering_System.Repositories;

namespace Online_Pizza_Ordering_System.Controllers
{
    [Authorize]
    public class StoresController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IStoresRepositories _storesRepo;
        private readonly IFileService _fileService;
        public StoresController(AppDbContext context, IStoresRepositories storesRepo ,IFileService fileService)
        {
            _context = context;
            _storesRepo = storesRepo;
            _fileService = fileService;
        }


        //GEt Stores/Index/
        public async Task<IActionResult> Index()
        {
            //var storelist = await _storesRepo.GetAllAsync();
            return View(await _storesRepo.GetAllAsync());
        }

       

        // GET: Stores/Details/
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stores = await _storesRepo.GetByIdAsync(id);

            if (stores == null)
            {
                return NotFound();
            }

            return View(stores);
        }


        // GET: Stores/Create/
        public IActionResult Create()
        {
            return View();
        }


        //POST: Stores/Create/
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StoreId,StoreName,AddressLine1,AddressLine2,ZipCode,City,State,StoreUrl,PhoneNumber,ImageFile")] Stores stores)
        {
            if (stores.ImageFile != null)
            {
                var fileReult = this._fileService.SaveImage(stores.ImageFile);
                if (fileReult.Item1 == 0)
                {
                    TempData["msg"] = "File could not saved";
                    return View("Create");
                }
                var imageName = fileReult.Item2;
                stores.StoreUrl = imageName;
                _storesRepo.Add(stores);
                await _storesRepo.SaveChangesAsync();
            }
            else
            {
                return View("Create");
            }
            return RedirectToAction("Index");
        }

        // GET: Stores/Edit/
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stores = await _storesRepo.GetByIdAsync(id);

            if (stores == null)
            {
                return NotFound();
            }
            return View(stores);
        }



        //POST: Stores/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,[Bind("StoreId,StoreName,AddressLine1,AddressLine2,ZipCode,City,State,StoreUrl,PhoneNumber,ImageFile")] Stores stores)
        {
            if(id != stores.StoreId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (stores.ImageFile != null)
                    {
                        var fileReult = this._fileService.SaveImage(stores.ImageFile);
                        if (fileReult.Item1 == 0)
                        {
                            TempData["msg"] = "File could not saved";
                            return View("Create");
                        }
                        var imageName = fileReult.Item2;
                        stores.StoreUrl = imageName;
                        _storesRepo.Update(stores);
                        await _storesRepo.SaveChangesAsync();
                    }
                }
                catch(DbUpdateConcurrencyException) 
                {
                    if (!StoresExist(stores.StoreId))
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
            return View(stores);
        }



        // GET: Stores/Delete/
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stores = await _storesRepo.GetByIdAsync(id);

            if (stores == null)
            {
                return NotFound();
            }

            return View(stores);
        }


        // POST: Stores/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var stores = await _storesRepo.GetByIdAsync(id);
            _storesRepo.Remove(stores);
            await _storesRepo.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        //GET: Store/StoresExist
        private bool StoresExist(Guid id)
        {
            return _storesRepo.Exists(id);
        }
    }
}
