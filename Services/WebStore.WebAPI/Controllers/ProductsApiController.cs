using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain;
using WebStore.Domain.DTO;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStore.WebAPI.Controllers
{
    [Route(WebAPIAddresses.Products)]
    [ApiController]
    public class ProductsApiController : ControllerBase
    {
        private readonly IProductData _ProductData;

        public ProductsApiController(IProductData ProductData) => _ProductData = ProductData;
        /// <summary> Получение секций где находиться продукты </summary>
        [HttpGet("sections")]
        public IActionResult GetSections()
        {
            var sections = _ProductData.GetSections();
            return Ok(sections.ToDTO());
        }

        /// <summary> Полученее секчии по ИД </summary>
        /// <param name="id"> Получние Ид секции</param>
        /// <returns></returns>
        [HttpGet("sections/{id}")]
        public IActionResult GetSection(int id)
        {
            var section = _ProductData.GetSectionById(id);
            return Ok(section.ToDTO());
        }

        /// <summary> Получение брендов </summary>
        [HttpGet("brands")]
        public IActionResult GetBrands()
        {
            var brands = _ProductData.GetBrands();
            return Ok(brands.ToDTO());
        }

        /// <summary> Получение Бренда по ИД </summary>
        /// <param name="id"> поиск по ИД</param>
        /// <returns>Ризультат нахождения бренда по ИД</returns>
        [HttpGet("brands/{id}")]
        public IActionResult GetBrand(int id)
        {
            var section = _ProductData.GetBrandById(id);
            return Ok(section.ToDTO());
        }

        /// <summary> Поиск продуктов через фильтр </summary>
        /// <param name="Filter">Фильтр Продуктов</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetProducts(ProductFilter Filter = null)
        {
            var products = _ProductData.GetProducts(Filter);
            return Ok(products.ToDTO());
        }

        /// <summary> Поиск по ИД продукт </summary>
        /// <param name="id">ИД продукта</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _ProductData.GetProductById(id);
            if (product is null)
                return NotFound();
            return Ok(product.ToDTO());
        }
    }
}
