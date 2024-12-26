using LaundrySystem.Domain.Dtos.Account;
using LaundrySystem.WebApi.Business.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LaundrySystem.WebApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IJWTTokenManager _tokenManager;

        public AuthenticationController(IJWTTokenManager tokenManager)
        {
            _tokenManager = tokenManager;
        }

        [HttpPost]
        public IActionResult authenticate([FromBody] LoginDto login)
        {
            var owner = _tokenManager.Authenticate(login.Name, login.Password);
            if (owner != null)
            {
                return Ok(_tokenManager.NewToken(owner.Id));
            }
            return Unauthorized();
        }
    }
}
