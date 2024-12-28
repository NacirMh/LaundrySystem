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
        private readonly ILaundryService _laundryService;
        public ConfigurationController(IConfigurationService configService ,IMachineService machineService , ILaundryService laundryService)
        {
            _configService = configService;
            _machineService = machineService;
            _laundryService = laundryService;
        }

        [HttpGet("{id}")]
        public IActionResult GetConfig(string id)
        {
            var Configurations = _configService.GetConfigurations(id).ToOwnerDTO();

            foreach (var laundry in Configurations.Laundries) {
                laundry.Machines.ForEach(machine => {
                    machine.TodayIncome = _machineService.CalculateTodayIncomes(machine.Id);
                    machine.MonthIncome = _machineService.CalculateMonthIncomes(machine.Id);
                    machine.TotalIncome = _machineService.CalculateTotalIncomes(machine.Id);
                });
                laundry.TodayIncome = _laundryService.CalculateTodayIncomes(laundry.Id);
                laundry.MonthIncome = _laundryService.CalculateMonthIncomes(laundry.Id);
                laundry.TotalIncome = _laundryService.CalculateTotalIncomes(laundry.Id);

            }

            if (Configurations == null)
            {
                return NotFound();
            }
            return Ok(Configurations);
        }


    }
}

