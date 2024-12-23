using LaundrySystem.Domain.Models;
using LaundrySystem.Domain.ValueObjects;
using LaundrySystem.WebApi.Business.Domain.Interfaces;
using LaundrySystem.WebApi.MiddleWares;
using LaundrySystem.WebApi.Presentation.Mappers;
using LaundrySystem.WebApi.Presentation.QueryObjects;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;

namespace LaundrySystem.WebApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineController : ControllerBase
    {
        private readonly IMachineService _machineManagement;
        public MachineController(IMachineService machineManagement , WebSocketConnectionManager webSocketManager)
        {
            _machineManagement = machineManagement;
        }

        [HttpPut("start/{cycleId}")]
        public IActionResult StartMachine(int cycleId, [FromBody] MachineState state)
        {
            Machine machine = _machineManagement.StartMachine(cycleId);
            
            return Ok(machine.ToMachineDTO());
        }

        [HttpPut("stop/{machineId}")]
        public IActionResult StopMachine(int machineId, [FromBody] MachineState state)
        {
            Machine machine = _machineManagement.StopMachine(machineId);
            return Ok(machine.ToMachineDTO());
        }

        [HttpGet("{machineId}")]
        public IActionResult MachineTotalIncome(int machineId, [FromQuery] DateQuery day)
        {
            decimal income = 0;
            if (day.Day == 0 || day.Year == 0 || day.Month == 0)
            {
                income = _machineManagement.CalculateMachineIncomes(machineId, null);
            }
            else
            {
                income = _machineManagement.CalculateMachineIncomes(machineId, day.ToDateOnlyFromDateQuery());
            }
            
            return Ok(income);
        }
    }
}
