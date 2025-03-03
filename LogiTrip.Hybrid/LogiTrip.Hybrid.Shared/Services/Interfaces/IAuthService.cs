using LLogiTrip.Model.Requests;
using LogiTrip.Model.Entity;
using LogiTrip.Model.Enums;
using LogiTrip.Model.Requests;
using LogiTrip.Model.Responses;

namespace LogiTrip.Hybrid.Shared.Services.Interfaces
{
    public interface IAuthService
    {
        bool IsLoggedIn { get; }
        User CurrentUser { get; }

        Task<User> GetUserAsync();

        Task<AuthResponse> LoginAsync(LoginRequest request);

        Task<AuthResponse> RegisterAsync(RegisterRequest request);

        Task<bool> RegisterAdminProrietarioAsync(RegisterRequestAdminProprietario request);

        Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequest request);

        Task LogoutAsync();

        Task<string> GetTokenAsync();

        Task<string> GetRefreshTokenAsync();

        bool IsUserLoggedIn();

        Role GetUserRole();

        Task InitializeAuthStateAsync();

        Task<bool> RegisterFuncionarioAsync(RegisterFuncionarioModel model);

        Task<UpdateUsuarioModel> GetUsuarioByIdAsync(string userId);

        Task<bool> UpdateUsuarioAsync(UpdateUsuarioModel model);
    }
}