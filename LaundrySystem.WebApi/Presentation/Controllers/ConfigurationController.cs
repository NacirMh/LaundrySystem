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
        private readonly IMachineService _machineService;
        public ConfigurationController(IConfigurationService configService ,IMachineService machineService)
        {
            _configService = configService;
            _machineService = machineService;
        }

        [HttpGet("{id}")]
        public IActionResult GetConfig(string id)
        {
            var Configurations = _configService.GetConfigurations(id).ToOwnerDTO();

            foreach (var Configuration in Configurations.Laundries) {
                Configuration.Machines.ForEach(machine => {
                    machine.TodayIncome = _machineService.CalculateTodayIncomes(machine.Id);
                    machine.MonthIncome = _machineService.CalculateMonthIncomes(machine.Id);
                    machine.TotalIncome = _machineService.CalculateTotalIncomes(machine.Id);
                });
            }

            if (Configurations == null)
            {
                return NotFound();
            }
            return Ok(Configurations);
        }


    }
}

