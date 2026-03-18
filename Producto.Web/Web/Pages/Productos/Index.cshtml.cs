using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Threading.Tasks;
using static Abstracciones.Modelos.ProductoBase;
using Abstracciones.Interfaces.Reglas;

namespace Web.Pages.Productos
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguracion _configuration;
        public IList<ProductoResponse> productos { get; set; } = default!;

        public IndexModel(IConfiguracion configuration)
        {
            _configuration = configuration;
        }

        public async Task OnGet()
        {
            string endpoint = _configuration.ObtenerMetodo("APIEnpoints","ObtenerProductos");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);
            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            productos = JsonSerializer.Deserialize<List<ProductoResponse>>(resultado, opciones);
        }
    }
}
