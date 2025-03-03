using LogiTrip.Hybrid.Shared.Endpoints;
using LogiTrip.Hybrid.Shared.Services.Interfaces;
using LogiTrip.Model.Entity;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace LogiTrip.Hybrid.Shared.Services
{
    public class EstabelecimentoService : IEstabelecimentoService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly IAuthService _authService;

        public EstabelecimentoService(HttpClient httpClient, IJSRuntime jsRuntime, IAuthService authService)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _authService = authService;
        }

        public async Task<IEnumerable<Estabelecimento>> GetEstabelecimentosByProprietarioAsync(string usuarioId)
        {
            var token = await _authService.GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync($"{APIAuthEndpoints.GetEstabelecimentosByProprietario}/{usuarioId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<Estabelecimento>>();
        }

        public async Task<IEnumerable<Usuario>> GetUsuariosByEstabelecimentoAsync(int estabelecimentoId)
        {
            var token = await _authService.GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync(string.Format(APIAuthEndpoints.GetUsuariosByEstabelecimento, estabelecimentoId));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<Usuario>>();
        }

        public async Task<bool> CreateEstabelecimentoAsync(CreateEstabelecimentoModel estabelecimento, IBrowserFile? file = null)
        {
            var token = await _authService.GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var Id = GetUserIdFromToken(token);
            try
            {
                using var formContent = new MultipartFormDataContent();

                // Adicionar campos de texto ao formulário
                formContent.Add(new StringContent(estabelecimento.RazaoSocial), nameof(estabelecimento.RazaoSocial));
                formContent.Add(new StringContent(estabelecimento.NomeFantasia), nameof(estabelecimento.NomeFantasia));
                formContent.Add(new StringContent(estabelecimento.CNPJ), nameof(estabelecimento.CNPJ));
                formContent.Add(new StringContent(estabelecimento.Telefone), nameof(estabelecimento.Telefone));
                formContent.Add(new StringContent(estabelecimento.Endereco), nameof(estabelecimento.Endereco));
                formContent.Add(new StringContent(estabelecimento.Status.ToString()), nameof(estabelecimento.Status));
                formContent.Add(new StringContent(estabelecimento.CEP), nameof(estabelecimento.CEP));
                formContent.Add(new StringContent(estabelecimento.Numero), nameof(estabelecimento.Numero));
                formContent.Add(new StringContent(estabelecimento.TaxaEntregaFixa.ToString()), nameof(estabelecimento.TaxaEntregaFixa));
                formContent.Add(new StringContent(estabelecimento.Descricao ?? string.Empty), nameof(estabelecimento.Descricao));

                //// Adicionar arquivo se selecionado
                //if (file != null)
                //{
                //    var stream = file.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024);
                //    var fileContent = new StreamContent(stream);
                //    fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                //    formContent.Add(fileContent, nameof(estabelecimento.UrlImagem), file.Name);
                //}

                // Enviar requisição POST
                var response = await _httpClient.PostAsync(APIAuthEndpoints.CreateEstabelecimento,formContent);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Erro ao criar estabelecimento: {error}");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar estabelecimento: {ex.Message}");
                return false;
            }
        }

        public async Task<Estabelecimento> GetEstabelecimentoByIdAsync(int id)
        {
            var token = await _authService.GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                var response = await _httpClient.GetAsync(string.Format(APIAuthEndpoints.GetEstabelecimentoById, id));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Estabelecimento>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erro ao buscar o estabelecimento com ID {id}: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateEstabelecimentoAsync(Estabelecimento estabelecimento, IBrowserFile? file = null)
        {
            var token = await _authService.GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var userId = GetUserIdFromToken(token);
            estabelecimento.UsuarioId = userId;

            try
            {
                using var formContent = new MultipartFormDataContent();

                // Adicionar campos de texto ao formulário
                formContent.Add(new StringContent(estabelecimento.Id.ToString()), nameof(estabelecimento.Id));
                formContent.Add(new StringContent(estabelecimento.UsuarioId), nameof(estabelecimento.UsuarioId));
                formContent.Add(new StringContent(estabelecimento.RazaoSocial), nameof(estabelecimento.RazaoSocial));
                formContent.Add(new StringContent(estabelecimento.NomeFantasia), nameof(estabelecimento.NomeFantasia));
                formContent.Add(new StringContent(estabelecimento.CNPJ), nameof(estabelecimento.CNPJ));
                formContent.Add(new StringContent(estabelecimento.Telefone), nameof(estabelecimento.Telefone));
                formContent.Add(new StringContent(estabelecimento.Endereco), nameof(estabelecimento.Endereco));
                formContent.Add(new StringContent(estabelecimento.Status.ToString()), nameof(estabelecimento.Status));
                formContent.Add(new StringContent(estabelecimento.CEP), nameof(estabelecimento.CEP));
                formContent.Add(new StringContent(estabelecimento.Numero), nameof(estabelecimento.Numero));
                formContent.Add(new StringContent(estabelecimento.taxaEntregaFixa.ToString()), nameof(estabelecimento.taxaEntregaFixa));
                formContent.Add(new StringContent(estabelecimento.Descricao ?? string.Empty), nameof(estabelecimento.Descricao));

                // Adicionar arquivo se selecionado
                if (file != null)
                {
                    var stream = file.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024);
                    var fileContent = new StreamContent(stream);
                    fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                    formContent.Add(fileContent, nameof(estabelecimento.UrlImagem), file.Name);
                }

                var response = await _httpClient.PutAsync(string.Format(APIAuthEndpoints.UpdateEstabelecimento, estabelecimento.Id),
                    formContent);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Erro ao atualizar estabelecimento: {error}");
                    return false;
                }

                return response.StatusCode == System.Net.HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exceção ao atualizar estabelecimento: {ex.Message}");
                return false;
            }
        }


        public string GetUserIdFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "UserId");
            return userIdClaim?.Value;
        }
        public async Task<EnderecoDto> BuscarEnderecoPorCepAsync(string cep)
        {
            var url = $"https://viacep.com.br/ws/{cep}/json/";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<EnderecoDto>(content);
            }

            return null;
        }

        public async Task<IEnumerable<HorarioFuncionamento>> GetHorariosByEstabelecimentoIdAsync(int estabelecimentoId)
        {
            var token = await _authService.GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync(string.Format(APIAuthEndpoints.GetHorarioFuncionamentoByEstabelecimento, estabelecimentoId));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<HorarioFuncionamento>>();
        }

        public async Task<bool> CreateHorarioFuncionamentoAsync(HorarioFuncionamentoRequest horario)
        {
            try
            {
                var json = JsonSerializer.Serialize(horario, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
                Console.WriteLine($"Request JSON: {json}");

                var response = await _httpClient.PostAsJsonAsync(APIAuthEndpoints.CreateHorarioFuncionamento, horario);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateHorarioFuncionamentoAsync(HorarioFuncionamento horario)
        {
            var token = await _authService.GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PutAsJsonAsync(string.Format(APIAuthEndpoints.UpdateHorarioFuncionamento, horario.Id), horario);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteHorarioFuncionamentoAsync(int id)
        {
            var token = await _authService.GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.DeleteAsync(string.Format(APIAuthEndpoints.DeleteHorarioFuncionamento, id));
            return response.IsSuccessStatusCode;
        }

    }
}