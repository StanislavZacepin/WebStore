﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData _ProductData;

        public CatalogController(IProductData ProductData) => _ProductData = ProductData;
        public IActionResult Index(int? BrandId, int? SectionId)            
        {
            var Filter = new ProductFilter
            {
                BrandId = BrandId,
                SectionId = SectionId,
            };

            var products = _ProductData.GetProducts(Filter);

            var view_model = new CatologViewModel
            {
                BrandId = BrandId,
                SectionId = SectionId,
                Products = products
                   .OrderBy(p => p.Order)
                   .Select(p => new ProductViewModel
                   {
                       Id = p.Id,
                       Name = p.Name,
                       Price = p.Price,
                       ImageUrl = p.ImageUrl,
                   })
            };

            return View(view_model);
        }
    }
}
