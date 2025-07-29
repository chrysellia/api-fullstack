using Microsoft.AspNetCore.Mvc;
using MarketplaceMvc.Models;
using System.Text;
using System.Text.Json;

namespace MarketplaceMvc.Controllers
{
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string ApiBaseUrl = "http://localhost:5273/api/products";

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: /Product/
        public async Task<IActionResult> Index()
        {
            var products = new List<Product>();

            try
            {
                using var httpClient = _httpClientFactory.CreateClient();
                var response = await httpClient.GetAsync(ApiBaseUrl);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    products = JsonSerializer.Deserialize<List<Product>>(json, options) ?? new List<Product>();
                }
                else
                {
                    ViewBag.ErrorMessage = $"Erreur API: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Erreur: {ex.Message}";
            }

            return View(products);
        }

        // GET: /Product/Create
        [HttpGet]
        public IActionResult Create()
        {
            var produit = new Product(); // ← vient bien de MarketplaceMvc.Models
            return View(produit);
        }

        // POST: /Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            try
            {
                using var httpClient = _httpClientFactory.CreateClient();

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var json = JsonSerializer.Serialize(product, options);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(ApiBaseUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Produit créé avec succès!";
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", $"Erreur lors de la création: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Erreur: {ex.Message}");
            }

            return View(product);
        }
    }
}
