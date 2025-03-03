using LogiTrip.Model.Entity;

namespace LogiTrip.AuthApi.Services
{
    public interface IAuthService
    {
        Task<(string Token, string RefreshToken, User User)> AuthenticateAsync(string email, string password);
        Task<User> RegisterAsync(string name, string email, string password, string role);
        Task<(string Token, string RefreshToken)> RefreshTokenAsync(string token);
    }
}
