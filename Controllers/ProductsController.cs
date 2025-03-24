using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VMGsite.Models;

namespace VMGsite.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            var products = GetProducts();
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
