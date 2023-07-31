using Application.Services;
using Application.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.V1
{
    public class AuthController : BaseController
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration) => _configuration = configuration;

        [HttpPost]
        public IActionResult Auth(UserViewModel user)
        {
            if (user.Username.Equals("usuario") && user.Password.Equals("teste"))
            {
                TokenService tokenService = new TokenService(_configuration);
                return Ok(tokenService.GenerateToken(new Domain.Models.Person()));
            }

            return BadRequest(new { Message = "username or password are incorrect." });
        }
    }
}