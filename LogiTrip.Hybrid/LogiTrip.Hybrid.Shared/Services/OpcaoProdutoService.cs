using LogiTrip.Hybrid.Shared.Endpoints;
using LogiTrip.Hybrid.Shared.Services.Interfaces;
using LogiTrip.Model.Entity;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace LogiTrip.Hybrid.Shared.Services
{
    public class OpcaoProdutoService : IOpcaoProdutoService
    {
        private readonly HttpClient _httpClient;
        private readonly IAuthService _authService;

        public OpcaoProdutoService(HttpClient httpClient, IAuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }

        public async Task<List<OpcaoProduto>> GetOpcoesByProdutoAsync(int estabelecimentoId, int produtoId)
        {
            var token = await _authService.GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = string.Format(APIAuthEndpoints.GetOpcoesByProduto, estabelecimentoId, produtoId);
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<OpcaoProduto>>();
        }

        public async Task<OpcaoProduto> GetOpcaoProdutoByIdAsync(int estabelecimentoId, int produtoId, int opcaoId)
        {
            var token = await _authService.GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = string.Format(APIAuthEndpoints.GetOpcaoProdutoById, estabelecimentoId, produtoId, opcaoId);
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<OpcaoProduto>();
            }
            return null;
        }

        public async Task<bool> CreateOpcaoProdutoAsync(int estabelecimentoId, int produtoId, OpcaoProduto opcao)
        {
            var token = await _authService.GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = string.Format(APIAuthEndpoints.CreateOpcaoProduto, estabelecimentoId, produtoId);
            var response = await _httpClient.PostAsJsonAsync(url, opcao);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateOpcaoProdutoAsync(int estabelecimentoId, int produtoId, OpcaoProduto opcao)
        {
            var token = await _authService.GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = string.Format(APIAuthEndpoints.UpdateOpcaoProduto, estabelecimentoId, produtoId, opcao.Id);
            var response = await _httpClient.PutAsJsonAsync(url, opcao);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteOpcaoProdutoAsync(int estabelecimentoId, int produtoId, int opcaoId)
        {
            var token = await _authService.GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = string.Format(APIAuthEndpoints.DeleteOpcaoProduto, estabelecimentoId, produtoId, opcaoId);
            var response = await _httpClient.DeleteAsync(url);
            return response.IsSuccessStatusCode;
        }

        // Métodos de ValorOpcaoProduto usando os endpoints definidos no APIAuthEndpoints
        public async Task<List<ValorOpcaoProduto>> GetValoresByOpcaoAsync(int estabelecimentoId, int produtoId, int opcaoId)
        {
            var token = await _authService.GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = string.Format(APIAuthEndpoints.GetValoresByOpcao, estabelecimentoId, produtoId, opcaoId);
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<ValorOpcaoProduto>>();
        }

        public async Task<bool> CreateValorOpcaoProdutoAsync(int estabelecimentoId, int produtoId, int opcaoId, ValorOpcaoProduto valor)
        {
            var token = await _authService.GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = string.Format(APIAuthEndpoints.CreateValorOpcao, estabelecimentoId, produtoId, opcaoId);
            var response = await _httpClient.PostAsJsonAsync(url, valor);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateValorOpcaoProdutoAsync(int estabelecimentoId, int produtoId, int opcaoId, ValorOpcaoProduto valor)
        {
            var token = await _authService.GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = string.Format(APIAuthEndpoints.UpdateValorOpcao, estabelecimentoId, produtoId, opcaoId, valor.Id);
            var response = await _httpClient.PutAsJsonAsync(url, valor);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteValorOpcaoProdutoAsync(int estabelecimentoId, int produtoId, int opcaoId, int valorId)
        {
            var token = await _authService.GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = string.Format(APIAuthEndpoints.DeleteValorOpcao, estabelecimentoId, produtoId, opcaoId, valorId);
            var response = await _httpClient.DeleteAsync(url);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ReplicarOpcoesParaCategoriaAsync(int categoriaId, int produtoOrigemId)
        {
            var token = await _authService.GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = string.Format(APIAuthEndpoints.ReplicarOpcoesParaCategoria, categoriaId, produtoOrigemId);
            var response = await _httpClient.PostAsync(url, null);
            return response.IsSuccessStatusCode;
        }

    }
}