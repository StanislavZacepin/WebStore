using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain;
using WebStore.Domain.Entities;

namespace WebStore.Interfaces.Services
{
    public interface IProductData
    {
        IEnumerable<Section> GetSections();
        Section GetSectionById(int Id);

        IEnumerable<Brand> GetBrands();
        Brand GetBrendById(int Id);

        IEnumerable<Product> GetProducts(ProductFilter Filter = null);

        Product GetProductById(int Id);

        void Update(Product product);

        bool Delete(int id);
    }
}
