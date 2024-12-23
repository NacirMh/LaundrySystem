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
            if (_tokenManager.Authenticate(login.Id, login.Password))
            {
                return Ok(_tokenManager.NewToken(login.Id));
            }
            return Unauthorized();
        }
    }
}
