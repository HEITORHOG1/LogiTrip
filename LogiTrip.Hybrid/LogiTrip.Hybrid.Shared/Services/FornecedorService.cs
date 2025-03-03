using LogiTrip.Hybrid.Shared.Endpoints;
using LogiTrip.Hybrid.Shared.Services.Interfaces;
using LogiTrip.Model.Entity;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace LogiTrip.Hybrid.Shared.Services
{
    public class FornecedorService : IFornecedorService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly IAuthService _authService;

        public FornecedorService(HttpClient httpClient, IJSRuntime jsRuntime, IAuthService authService)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _authService = authService;
        }

        public async Task<IEnumerable<Fornecedor>> GetFornecedoresByEstabelecimentoAsync(int estabelecimentoId)
        {
            var token = await _authService.GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                var response = await _httpClient.GetAsync(string.Format(APIAuthEndpoints.GetFornecedoresByEstabelecimento, estabelecimentoId));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<IEnumerable<Fornecedor>>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erro ao buscar fornecedores do estabelecimento {estabelecimentoId}: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> CreateFornecedorAsync(Fornecedor fornecedor)
        {
            var token = await _authService.GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                var fornecedorDto = new FornecedorCreateDto
                {
                    Nome = fornecedor.Nome,
                    CNPJ = fornecedor.Cnpj,
                    Telefone = fornecedor.Telefone,
                    Endereco = fornecedor.Endereco,
                    Email = fornecedor.Email,
                    EstabelecimentoId = fornecedor.EstabelecimentoId
                };

                // Para debug
                Console.WriteLine("URL: " + string.Format(APIAuthEndpoints.CreateFornecedor, fornecedor.EstabelecimentoId));
                Console.WriteLine("Payload: " + System.Text.Json.JsonSerializer.Serialize(fornecedorDto));

                var response = await _httpClient.PostAsJsonAsync(
                    string.Format(APIAuthEndpoints.CreateFornecedor, fornecedor.EstabelecimentoId),
                    fornecedorDto);

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
                Console.WriteLine($"Erro ao criar fornecedor: {ex.Message}");
                return false;
            }
        }

        public async Task<Fornecedor> GetFornecedorByIdAsync(int id)
        {
            var token = await _authService.GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            try
            {
                var response = await _httpClient.GetAsync(string.Format(APIAuthEndpoints.GetFornecedorById, id));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Fornecedor>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erro ao buscar fornecedor: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateFornecedorAsync(Fornecedor fornecedor)
        {
            var token = await _authService.GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            try
            {
                var fornecedorDto = new FornecedorUpdateDto
                {
                    Id = fornecedor.Id,
                    Nome = fornecedor.Nome,
                    CNPJ = fornecedor.Cnpj,
                    Telefone = fornecedor.Telefone,
                    Endereco = fornecedor.Endereco,
                    Email = fornecedor.Email,
                    EstabelecimentoId = fornecedor.EstabelecimentoId
                };

                var response = await _httpClient.PutAsJsonAsync(
                    string.Format(APIAuthEndpoints.UpdateFornecedor, fornecedor.Id),
                    fornecedorDto);

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
                Console.WriteLine($"Erro ao atualizar fornecedor: {ex.Message}");
                return false;
            }
        }
    }
}