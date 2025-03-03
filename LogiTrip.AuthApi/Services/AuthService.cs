using LogiTrip.AuthApi.Config;
using LogiTrip.Extensions.Context;
using LogiTrip.Model.Entity;
using LogiTrip.Model.Enums;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace LogiTrip.AuthApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly JwtHelper _jwtHelper;

        public AuthService(AppDbContext context, JwtHelper jwtHelper)
        {
            _context = context;
            _jwtHelper = jwtHelper;
        }

        public async Task<(string Token, string RefreshToken, User User)> AuthenticateAsync(string email, string password)
        {
            var user = await _context.Users.Include(u => u.RefreshTokens).SingleOrDefaultAsync(u => u.Email == email);
            if (user == null || !VerifyPassword(password, user.Password))
            {
                return (null, null, null);
            }

            var token = _jwtHelper.GenerateToken(user);
            var refreshToken = GenerateRefreshToken();
            user.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();

            return (token, refreshToken.Token, user);
        }

        public async Task<User> RegisterAsync(string name, string email, string password, string role)
        {
            if (!Enum.TryParse(role, out Role userRole))
            {
                throw new ArgumentException("Invalid role");
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = name,
                Email = email,
                Password = HashPassword(password),
                Role = userRole
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<(string Token, string RefreshToken)> RefreshTokenAsync(string token)
        {
            var refreshToken = await _context.RefreshTokens.Include(rt => rt.User).SingleOrDefaultAsync(rt => rt.Token == token);

            if (refreshToken == null || !refreshToken.IsActive)
            {
                return (null, null);
            }

            var newJwtToken = _jwtHelper.GenerateToken(refreshToken.User);
            var newRefreshToken = GenerateRefreshToken();
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.User.RefreshTokens.Add(newRefreshToken);
            await _context.SaveChangesAsync();

            return (newJwtToken, newRefreshToken.Token);
        }

        private RefreshToken GenerateRefreshToken()
        {
            var randomBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.UtcNow.AddDays(7),
                    Created = DateTime.UtcNow
                };
            }
        }

        private bool VerifyPassword(string inputPassword, string storedPassword)
        {
            // Implement password verification logic (e.g., hashing)
            return inputPassword == storedPassword;
        }

        private string HashPassword(string password)
        {
            // Implement password hashing logic
            return password;
        }
    }
}