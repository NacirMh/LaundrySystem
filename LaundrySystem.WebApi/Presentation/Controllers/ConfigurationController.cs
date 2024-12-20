using LaundrySystem.Domain.Dtos.Machine;
using LaundrySystem.Domain.Dtos.Owner;
using LaundrySystem.WebApi.Business.Domain.Interfaces;
using LaundrySystem.WebApi.Presentation.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LaundrySystem.WebApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfigurationService _configService;
        public ConfigurationController(IConfigurationService configService)
        {
            _configService = configService;
        }

        [HttpGet("{id}")]
        public IActionResult GetConfig(int id)
        {
            var Configurations = _configService.GetConfigurations(id).ToOwnerDTO();
            if (Configurations == null)
            {
                return NotFound();
            }
            return Ok(Configurations);
        }


    }
}

