using Auth.Models;
using Auth.Services;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenService _jwtTokenService;
        public AuthController(JwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel user)
        {
            Console.WriteLine("===> Authenticating user");
            var loginResult = _jwtTokenService.GenerateAuthToken(user);
            return loginResult is null ? Unauthorized() : Ok(loginResult);
        }
    }
}
