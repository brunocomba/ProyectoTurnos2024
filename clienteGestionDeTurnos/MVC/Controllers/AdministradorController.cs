using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using Newtonsoft.Json;
using System.Text;


namespace MVC.Controllers
{
    public class AdministradorController : Controller
    {
        private readonly HttpClient _httpClient;

        public AdministradorController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7147/administradores");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("administradores/listado");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var administradores = JsonConvert.DeserializeObject<IEnumerable<Administrador>>(content);

                return View("Index", administradores);
            }

            return View (new List<Administrador>());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Administrador adm)
        {
            var jsonData = JsonConvert.SerializeObject(adm);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:7147/administradores/add", content);

            if (response.IsSuccessStatusCode)
            {
                // Manejar la respuesta exitosa
                var result = await response.Content.ReadAsStringAsync();
                return Content($"Éxito: {result}");

                //return RedirectToAction("Index");   // volver al index
            }
            else
            {
                var resultNeg = await response.Content.ReadAsStringAsync();
                return Content($"Error: {resultNeg}");

                //ModelState.AddModelError(string.Empty, "Error al crear un nuevo administrador.");
            }

        }
     
    }
}
