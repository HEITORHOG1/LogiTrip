using LLogiTrip.Model.Requests;
using LogiTrip.AuthApi.Services;
using LogiTrip.Model.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LogiTrip.AuthApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.AuthenticateAsync(request.Email, request.Password);
            if (result.Token == null)
            {
                return Unauthorized();
            }

            return Ok(new
            {
                Token = result.Token,
                RefreshToken = result.RefreshToken,
                User = new
                {
                    result.User.Id,
                    result.User.Name,
                    result.User.Email,
                    Role = result.User.Role.ToString()
                }
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var user = await _authService.RegisterAsync(request.Name, request.Email, request.Password, request.Role);
            if (user == null)
            {
                return BadRequest("User registration failed.");
            }
            return Ok(new { Message = "User registered successfully!" });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var result = await _authService.RefreshTokenAsync(request.Token);
            if (result.Token == null)
            {
                return Unauthorized();
            }

            return Ok(new
            {
                Token = result.Token,
                RefreshToken = result.RefreshToken
            });
        }
    }
}