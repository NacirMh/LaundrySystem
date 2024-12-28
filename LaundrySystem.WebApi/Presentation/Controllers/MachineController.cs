using LaundrySystem.Domain.Dtos.Machine;
using LaundrySystem.Domain.Models;
using LaundrySystem.Domain.ValueObjects;
using LaundrySystem.WebApi.Business.Domain.Interfaces;
using LaundrySystem.WebApi.Presentation.Mappers;
using LaundrySystem.WebApi.WebSocketLib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.WebSockets;
using System.Text;

namespace LaundrySystem.WebApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineController : ControllerBase
    {
        private readonly IMachineService _machineManagement;
        private readonly WebSocketHandler _webSocketHandler;
        private readonly ILaundryService _laundryService;
        public MachineController(IMachineService machineManagement , ILaundryService laundryService, WebSocketHandler webSocketHandler)
        {
            _machineManagement = machineManagement;
            _webSocketHandler = webSocketHandler;
            _laundryService = laundryService;
        }

        [HttpPut("start/{cycleId}")]
        public async  Task<IActionResult> StartMachine(int cycleId, [FromBody] MachineState state)
        {
            Actionn action = _machineManagement.StartMachine(cycleId);

            var socketId = action.Cycle.Machine.Laundry.OwnerId;
            var laundryId = action.Cycle.Machine.LaundryId;
            var machineStartedDTO = new MachineStartedDto
            {
                Laundry = new LaundryDtoOnMachineStarted
                {
                    LaundryId = laundryId,
                    TodayIncome = _laundryService.CalculateTodayIncomes(laundryId),
                    MonthIncome = _laundryService.CalculateMonthIncomes(laundryId),
                    TotalIncome = _laundryService.CalculateTotalIncomes(laundryId)
                },
                CycleId = cycleId,
                MachineId = action.Cycle.Machine.Id,
                Action = action.ToActionDTO(),
                MonthIncome = _machineManagement.CalculateMonthIncomes(action.Cycle.Machine.Id),
                TodayIncome = _machineManagement.CalculateTodayIncomes(action.Cycle.Machine.Id),
                TotalIncome = _machineManagement.CalculateTotalIncomes(action.Cycle.Machine.Id),
            };

            var socketMessage = new WebSocketMessage
            {
                Type = "MachineStarted",
                Message = machineStartedDTO
            };

            await _webSocketHandler.SendMessageToAllSessionsAsync(socketId , socketMessage);

            return Ok(action.Cycle.Machine.ToMachineDTO());
        }

        [HttpPut("stop/{machineId}")]
        public async Task<IActionResult> StopMachine(int machineId, [FromBody] MachineState state)
        {
            Machine machine = _machineManagement.StopMachine(machineId);

            MachineStoppedDTO machineStoppedDTO = new MachineStoppedDTO
            {
                MachineId = machineId
            };

            var socketMessage = new WebSocketMessage
            {
                Type = "MachineStopped",
                Message = machineStoppedDTO
            };
            await _webSocketHandler.SendMessageToAllSessionsAsync(machine.Laundry.OwnerId , socketMessage);
            return Ok(machine.ToMachineDTO());
        }

    }
}
