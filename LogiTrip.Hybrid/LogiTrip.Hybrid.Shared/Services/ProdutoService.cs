using LogiTrip.Hybrid.Shared.Endpoints;
using LogiTrip.Hybrid.Shared.Services.Interfaces;
using LogiTrip.Model.Entity;
using LogiTrip.Model.Paginacao;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.JSInterop;
using System.Globalization;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace LogiTrip.Hybrid.Shared.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly IAuthService _authService;

        public ProdutoService(HttpClient httpClient, IJSRuntime jsRuntime, IAuthService authService)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _authService = authService;
        }

        public async Task<List<Produto>> GetProdutosByEstabelecimentoAsync(int estabelecimentoId, string search = null)
        {
            var token = await _authService.GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            try
            {
                var url = $"{string.Format(APIAuthEndpoints.GetProdutosByEstabelecimento, estabelecimentoId)}";
                if (!string.IsNullOrEmpty(search))
                {
                    url += $"&search={Uri.EscapeDataString(search)}";
                }
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<Produto>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar produtos: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> CreateProdutoAsync(int estabelecimentoId, Produto produto, IBrowserFile imagem)
        {
            try
            {
                var token = await _authService.GetTokenAsync();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var multipartContent = new MultipartFormDataContent();

                // Garante que o preço será enviado com formato correto
                var precoString = produto.Preco.ToString("F2", CultureInfo.InvariantCulture).Replace(",", ".");
                multipartContent.Add(new StringContent(precoString), "Preco");

                // Adicionar os campos do produto
                multipartContent.Add(new StringContent(produto.Nome), "Nome");
                multipartContent.Add(new StringContent(produto.Descricao ?? ""), "Descricao");
                multipartContent.Add(new StringContent(precoString), "Preco"); // Usando o preço formatado
                multipartContent.Add(new StringContent(produto.Disponivel.ToString().ToLower()), "Disponivel");
                multipartContent.Add(new StringContent(produto.CategoriaId.ToString()), "CategoriaId");
                if (!string.IsNullOrEmpty(produto.CodigoDeBarras))
                {
                    multipartContent.Add(new StringContent(produto.CodigoDeBarras), "CodigoDeBarras");
                }

                // Adicionar a imagem
                var imageContent = new StreamContent(imagem.OpenReadStream(maxAllowedSize: 10485760));
                var contentType = imagem.ContentType;
                if (string.IsNullOrEmpty(contentType))
                {
                    contentType = Path.GetExtension(imagem.Name).ToLower() switch
                    {
                        ".jpg" or ".jpeg" => "image/jpeg",
                        ".png" => "image/png",
                        ".webp" => "image/webp",
                        _ => "application/octet-stream"
                    };
                }
                imageContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                multipartContent.Add(imageContent, "Imagem", imagem.Name);

                var response = await _httpClient.PostAsync(
                    string.Format(APIAuthEndpoints.CreateProduto, estabelecimentoId),
                    multipartContent);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Erro na resposta: {error}");
                }

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar produto: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteProdutoAsync(int estabelecimentoId, int id)
        {
            try
            {
                var token = await _authService.GetTokenAsync();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.DeleteAsync(
                    string.Format(APIAuthEndpoints.DeleteProduto, estabelecimentoId, id));

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao deletar produto: {ex.Message}");
                return false;
            }
        }

        public async Task<Produto> GetProdutoByIdAsync(int estabelecimentoId, int id)
        {
            try
            {
                var token = await _authService.GetTokenAsync();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.GetAsync(
                    string.Format(APIAuthEndpoints.GetProdutoById, estabelecimentoId, id));

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<Produto>();
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar produto: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> UpdateProdutoAsync(int estabelecimentoId, Produto produto, IBrowserFile imagem = null)
        {
            try
            {
                var token = await _authService.GetTokenAsync();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var multipartContent = new MultipartFormDataContent();
                // Remover qualquer formatação e enviar o valor numérico puro
                var precoDecimal = produto.Preco;
                multipartContent.Add(new StringContent(precoDecimal.ToString("0.00", CultureInfo.InvariantCulture)), "Preco");

                // Enviar o preço diretamente, sem manipulação
                multipartContent.Add(new StringContent(produto.Preco.ToString(CultureInfo.InvariantCulture)), "Preco");
                multipartContent.Add(new StringContent(produto.Nome), "Nome");
                multipartContent.Add(new StringContent(produto.Descricao ?? ""), "Descricao");
                multipartContent.Add(new StringContent(produto.Disponivel.ToString().ToLower()), "Disponivel");
                multipartContent.Add(new StringContent(produto.CategoriaId.ToString()), "CategoriaId");

                if (!string.IsNullOrEmpty(produto.CodigoDeBarras))
                {
                    multipartContent.Add(new StringContent(produto.CodigoDeBarras), "CodigoDeBarras");
                }

                if (imagem != null)
                {
                    var imageContent = new StreamContent(imagem.OpenReadStream(maxAllowedSize: 10485760));
                    imageContent.Headers.ContentType = new MediaTypeHeaderValue(imagem.ContentType);
                    multipartContent.Add(imageContent, "Imagem", imagem.Name);
                }
                else
                {
                    multipartContent.Add(new StringContent(produto.Imagem ?? ""), "Imagem");
                }

                var response = await _httpClient.PutAsync(
                    string.Format(APIAuthEndpoints.UpdateProduto, estabelecimentoId, produto.Id),
                    multipartContent);

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
                Console.WriteLine($"Erro ao atualizar produto: {ex.Message}");
                return false;
            }
        }
    }
}