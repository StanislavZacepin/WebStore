using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Indentity;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Role.Administrators)]
    public class ProductsController : Controller
    {
        private readonly IProductData _ProductData;

        public ProductsController(IProductData ProductData)
        {
            _ProductData = ProductData;
        }
        public IActionResult Index()
        {
            var products = _ProductData.GetProducts();
            return View(products);
        }
        #region Edit
        public IActionResult Edit(int? id)
        {
            if (id is null)
                return View(new ProductViewModel());

            var product = _ProductData.GetProductById((int)id);
            if (product is null) return NotFound();

            var model = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
               
            };
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = Role.Administrators)]
        public IActionResult Edit(ProductViewModel model)
        {
            
            if (!ModelState.IsValid) return View(model);

            var product = new Product
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                ImageUrl = model.ImageUrl,
              
            };

            //if (product.Id == 0)
            //    _ProductData.Add(product);
            //else
                _ProductData.Update(product);

            return RedirectToAction(nameof(Index));


        }
        #endregion


        #region Delete
        public IActionResult Delete(int id)
        {
            if (id < 0) return BadRequest();

            var product = _ProductData.GetProductById(id);
            if (product is null) return NotFound();

            return View(new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                
            });
        }
        [HttpPost]
        [Authorize(Roles = Role.Administrators)]
        public IActionResult DeleteConfirmed(int id)
        {
            _ProductData.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
