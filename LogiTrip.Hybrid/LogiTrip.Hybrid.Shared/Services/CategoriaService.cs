using LogiTrip.Hybrid.Shared.Endpoints;
using LogiTrip.Hybrid.Shared.Services.Interfaces;
using LogiTrip.Model.Entity;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace LogiTrip.Hybrid.Shared.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly IAuthService _authService;

        public CategoriaService(HttpClient httpClient, IJSRuntime jsRuntime, IAuthService authService)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _authService = authService;
        }

        public async Task<bool> CreateCategoriaAsync(Categoria categoria)
        {
            var token = await _authService.GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                var response = await _httpClient.PostAsJsonAsync(
                    string.Format(APIAuthEndpoints.CreateCategoria, categoria.EstabelecimentoId),
                    categoria);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Status Code: {response.StatusCode}");
                    Console.WriteLine($"Error: {error}");
                }

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar categoria: {ex.Message}");
                return false;
            }
        }

        public async Task<IEnumerable<Categoria>> GetCategoriasByEstabelecimentoAsync(int estabelecimentoId)
        {
            var token = await _authService.GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                var response = await _httpClient.GetAsync(string.Format(APIAuthEndpoints.GetCategoriasByEstabelecimento, estabelecimentoId));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<IEnumerable<Categoria>>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erro ao buscar categorias do estabelecimento {estabelecimentoId}: {ex.Message}");
                throw;
            }
        }

        public async Task<Categoria> GetCategoriaByIdAsync(int estabelecimentoId, int id)
        {
            var token = await _authService.GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            try
            {
                var response = await _httpClient.GetAsync(string.Format(APIAuthEndpoints.GetCategoriaById, estabelecimentoId, id));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Categoria>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erro ao buscar categoria: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateCategoriaAsync(Categoria categoria)
        {
            var token = await _authService.GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            try
            {
                var response = await _httpClient.PutAsJsonAsync(
                    string.Format(APIAuthEndpoints.UpdateCategoria, categoria.EstabelecimentoId, categoria.Id),
                    categoria);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Status Code: {response.StatusCode}");
                    Console.WriteLine($"Error: {error}");
                }
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar categoria: {ex.Message}");
                return false;
            }
        }
    }
}