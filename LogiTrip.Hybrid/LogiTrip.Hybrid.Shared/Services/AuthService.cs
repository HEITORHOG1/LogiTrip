using LLogiTrip.Model.Requests;
using LogiTrip.Hybrid.Shared.Endpoints;
using LogiTrip.Hybrid.Shared.Services.Interfaces;
using LogiTrip.Model.Entity;
using LogiTrip.Model.Enums;
using LogiTrip.Model.Requests;
using LogiTrip.Model.Responses;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace LogiTrip.Hybrid.Shared.Services
{
    public class AuthService : IAuthService
    {
        public bool IsLoggedIn { get; private set; } = false;
        public User CurrentUser { get; private set; }
        private bool _initialized = false;
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private const string TokenKey = "authToken";
        private const string RefreshTokenKey = "refreshToken";
        private const string UserKey = "user";

        public AuthService(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(APIAuthEndpoints.Login, request);
                var result = await response.Content.ReadFromJsonAsync<AuthResponse>();

                if (response.IsSuccessStatusCode && result != null)
                {
                    // Verifica se temos o usuário e a lista de roles
                    if (result.User == null || result.Roles == null || !result.Roles.Any())
                    {
                        throw new Exception("Dados do usuário ou roles incompletos na resposta");
                    }

                    // Atribui a primeira role ao usuário
                    result.AssignRolesToUser();

                    // Verifica se a role foi atribuída corretamente
                    if (string.IsNullOrEmpty(result.User.Role))
                    {
                        throw new Exception("Não foi possível atribuir a role ao usuário");
                    }

                    await SaveTokensAsync(result.Token, result.RefreshToken);
                    await SaveUserAsync(result.User);
                    CurrentUser = result.User;
                    IsLoggedIn = true;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao fazer login: {ex.Message}");
            }
        }

        private async Task SaveUserAsync(User user)
        {
            await SetStorageItem(UserKey, JsonSerializer.Serialize(user), false);
        }

        public async Task<User> GetUserAsync()
        {
            var userJson = await GetStorageItem(UserKey, false);
            return userJson != null ? JsonSerializer.Deserialize<User>(userJson) : null;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(APIAuthEndpoints.Register, request);
            var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
            if (response.IsSuccessStatusCode && result != null)
            {
                await SaveTokensAsync(result.Token, result.RefreshToken);
                await SaveUserAsync(result.User);
                CurrentUser = result.User;
                IsLoggedIn = true;
            }
            return result;
        }

        public async Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(APIAuthEndpoints.RefreshToken, request);
            var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
            if (response.IsSuccessStatusCode && result != null)
            {
                await SaveTokensAsync(result.Token, result.RefreshToken);
            }
            return result;
        }

        public async Task LogoutAsync()
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", TokenKey);
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", RefreshTokenKey);
                await _jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", UserKey);
            }
            catch (InvalidOperationException)
            {
                // Ignora durante a pré-renderização
            }
            finally
            {
                IsLoggedIn = false;
                CurrentUser = null;
            }
        }

        private async Task SaveTokensAsync(string token, string refreshToken)
        {
            await SetStorageItem(TokenKey, token);
            await SetStorageItem(RefreshTokenKey, refreshToken);
        }

        public async Task<string> GetTokenAsync()
        {
            return await GetStorageItem(TokenKey);
        }

        public async Task<string> GetRefreshTokenAsync()
        {
            return await GetStorageItem(RefreshTokenKey);
        }

        public bool IsUserLoggedIn()
        {
            return IsLoggedIn;
        }

        public Role GetUserRole()
        {
            if (CurrentUser == null || string.IsNullOrEmpty(CurrentUser.Role))
                return Role.Cliente;

            return Enum.TryParse<Role>(CurrentUser.Role, out var role) ? role : Role.Cliente;
        }

        public async Task InitializeAuthStateAsync()
        {
            if (_initialized) return;

            try
            {
                var token = await GetTokenAsync();
                if (!string.IsNullOrEmpty(token))
                {
                    var user = await GetUserAsync();
                    if (user != null)
                    {
                        CurrentUser = user;
                        IsLoggedIn = true;
                        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }
                    else
                    {
                        await LogoutAsync();
                    }
                }
                _initialized = true;
            }
            catch (InvalidOperationException)
            {
                // Ignora erros de JS durante a pré-renderização
            }
            catch (Exception)
            {
                await LogoutAsync();
            }
        }

        private async Task<string> GetStorageItem(string key, bool useLocalStorage = true)
        {
            try
            {
                if (useLocalStorage)
                    return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
                return await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", key);
            }
            catch (InvalidOperationException)
            {
                return null; // Retorna null durante a pré-renderização
            }
        }

        private async Task SetStorageItem(string key, string value, bool useLocalStorage = true)
        {
            try
            {
                if (useLocalStorage)
                    await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, value);
                else
                    await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", key, value);
            }
            catch (InvalidOperationException)
            {
                // Ignora durante a pré-renderização
            }
        }

        public async Task<bool> RegisterFuncionarioAsync(RegisterFuncionarioModel model)
        {
            var requestPayload = new RegisterFuncionarioRequest
            {
                EstabelecimentoId = model.EstabelecimentoId,
                Username = model.Username,
                Email = model.Email,
                Password = model.Password,
                CPF_CNPJ = model.CPF_CNPJ,
                NomeUsuario = model.NomeUsuario,
                Endereco = model.Endereco,
                CEP = model.CEP,
                Telefone = model.Telefone,
                NivelAcesso = (int)model.NivelAcesso,
                Role = model.Role.ToString()
            };

            var token = await GetTokenAsync(); // obtém o token do local storage
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PostAsJsonAsync(APIAuthEndpoints.RegisterFuncionario, requestPayload);
            return response.IsSuccessStatusCode;
        }

        public async Task<UpdateUsuarioModel> GetUsuarioByIdAsync(string userId)
        {
            var token = await GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync(string.Format(APIAuthEndpoints.GetUsuarioById, userId));
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<UpdateUsuarioModel>();
        }

        public async Task<bool> UpdateUsuarioAsync(UpdateUsuarioModel model)
        {
            var token = await GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PutAsJsonAsync(APIAuthEndpoints.UpdateUsuario, model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RegisterAdminProrietarioAsync(RegisterRequestAdminProprietario request)
        {
            try
            {
                var token = await GetTokenAsync();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // Envia a solicitação para o endpoint
                var response = await _httpClient.PostAsJsonAsync(APIAuthEndpoints.RegisterAdminProprietario, request);

                if (response.IsSuccessStatusCode)
                {
                    // Registro bem-sucedido
                    return true;
                }

                // Trata possíveis mensagens de erro no conteúdo da resposta
                var errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Erro no registro: {errorMessage}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
                throw;
            }
        }

    }
}