using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.Components
{
    // [ViewComponent(Name = "BrandsView")]
    public class BrandsViewComponent : ViewComponent
    {
        private readonly IProductData _ProductData;

        public BrandsViewComponent(IProductData ProductData) => _ProductData = ProductData;
        //  public async Task<IViewComponentResult> InvokeAsync() => View();
        public IViewComponentResult Invoke(string BrandId) => View(GetBrands());
        private IEnumerable<BrandViewModel> GetBrands() =>
           _ProductData.GetBrands()
              .OrderBy(b => b.Order)
              .Select(b => new BrandViewModel
              {
                  Id = b.Id,
                  Name = b.Name,
              });


    }
}
