using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VMGsite.Models;

namespace VMGsite.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index(string query)
        {
            var products = GetProducts();

            // Если введен запрос, фильтруем товары
            if (!string.IsNullOrEmpty(query))
            {
                products = products
                    .Where(p => p.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                                p.Description.Contains(query, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return View(products);
        }

        public IActionResult Details(string name)
        {
            var products = GetProducts();
            var product = products.FirstOrDefault(p => p.Name == name);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        private List<Product> GetProducts()
        {
            var json = System.IO.File.ReadAllText("Data/products.json");
            return JsonConvert.DeserializeObject<List<Product>>(json);
        }
    }
}
