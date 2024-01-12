using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using WebShopApp.Core.Contracts;

using WebStoreApp.Models.Brand;

namespace WebStoreApp.Controllers
{
    public class BrandController :Controller
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            this._brandService = brandService;
        }
        public IActionResult Index()
        {
            List<BrandIndexVM> brands = _brandService.GetBrands()
               .Select(brands => new BrandIndexVM
               {
                   Id = brands.Id,
                   BrandName = brands.BrandName
               }).ToList();
            return this.View(brands);
        }

        // GET: /Brand/Create
        [Authorize(Roles ="Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Brand/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create([FromForm]BrandCreateVM item)
        {
            if (ModelState.IsValid)
            {
                var createdId = _brandService.Create(item.BrandName);
                if (createdId)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }
    }
}
