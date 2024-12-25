using LaundrySystem.Domain.Dtos.Machine;
using LaundrySystem.Domain.Models;
using LaundrySystem.Domain.ValueObjects;
using LaundrySystem.WebApi.Business.Domain.Interfaces;
using LaundrySystem.WebApi.Presentation.Mappers;
using LaundrySystem.WebApi.Presentation.QueryObjects;
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
        public MachineController(IMachineService machineManagement , WebSocketHandler webSocketHandler)
        {
            _machineManagement = machineManagement;
            _webSocketHandler = webSocketHandler;
        }

        [HttpPut("start/{cycleId}")]
        public async  Task<IActionResult> StartMachine(int cycleId, [FromBody] MachineState state)
        {
            Actionn action = _machineManagement.StartMachine(cycleId);

            var socketId = action.Cycle.Machine.Laundry.OwnerId;

            var machineStartedDTO = new MachineStartedDto
            {
                CycleId = cycleId,
                MachineId = action.Cycle.Machine.Id,
                Action = action.ToActionDTO(),
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
