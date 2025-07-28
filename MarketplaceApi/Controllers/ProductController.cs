using Microsoft.AspNetCore.Mvc;
using MarketplaceMvc.Models; // Même namespace que votre modèle MVC

namespace MarketplaceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        // Liste en mémoire pour stocker les produits (remplacez par votre base de données)
        private static List<Product> _products = new List<Product>();
        private static int _nextId = 1;

        // GET: api/products
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            return Ok(_products);
        }

        // GET: api/products/5
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // POST: api/products
        [HttpPost]
        public ActionResult<Product> CreateProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Assigner un ID automatiquement
            product.Id = _nextId++;
            _products.Add(product);

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        // PUT: api/products/5
        // [HttpPut("{id}")]
        // public IActionResult UpdateProduct(int id, Product product)
        // {
        //     var existingProduct = _products.FirstOrDefault(p => p.Id == id);
        //     if (existingProduct == null)
        //     {
        //         return NotFound();
        //     }

        //     // Mise à jour des propriétés
        //     existingProduct.Name = product.Name;
        //     existingProduct.Price = product.Price;
        //     existingProduct.Description = product.Description;

        //     return NoContent();
        // }

        // DELETE: api/products/5
        // [HttpDelete("{id}")]
        // public IActionResult DeleteProduct(int id)
        // {
        //     var product = _products.FirstOrDefault(p => p.Id == id);
        //     if (product == null)
        //     {
        //         return NotFound();
        //     }

        //     _products.Remove(product);
        //     return NoContent();
        // }
    }
}