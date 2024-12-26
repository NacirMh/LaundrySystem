using LaundrySystem.Domain.Models;
using LaundrySystem.Domain.Dtos.Cycle;
using LaundrySystem.Domain.Dtos.Machine;
using LaundrySystem.WebApi.Business.Domain.Interfaces;
using LaundrySystem.Domain.ValueObjects;
using LaundrySystem.WebApi.Infrastructure.Daos;
using Microsoft.AspNetCore.Http;
using System.Net.WebSockets;
using System.Text;
using LaundrySystem.WebApi.WebSocketLib;
using System.Text.Json;
using LaundrySystem.WebApi.Presentation.Mappers;

namespace LaundrySystem.WebApi.Business.Services
{
    public class MachineManagement : IMachineService
    {
        private readonly IMachineDAO _machineDAO;
        private readonly IActionDAO _actionDAO;
        private readonly ICycleDAO _cycleDAO;

        public MachineManagement(IMachineDAO machineDAO, IActionDAO actionDAO, ICycleDAO cycleDAO)
        {
            _machineDAO = machineDAO;
            _actionDAO = actionDAO;
            _cycleDAO = cycleDAO;
        }

        public Actionn StartMachine(int cycleId)
        {
            Cycle cycle = _cycleDAO.GetById(cycleId);
            Machine machine = _machineDAO.ChangeMachineState(cycle.MachineId, MachineState.Running);
            Actionn action = new Actionn
            {
                CycleId = cycleId,
            };
            _actionDAO.CreateAction(action);
            return action;
        }

        public Machine StopMachine(int MachineId)
        {
            Machine machine = _machineDAO.ChangeMachineState(MachineId, MachineState.Stopped);
            return machine;
        }

        public decimal CalculateMonthIncomes(int MachineId)
        {
            return CalculateIncomes(MachineId, x => DateTime.Now.Month == x.Month);
        }

        public decimal CalculateTodayIncomes(int MachineId)
        {
            return CalculateIncomes(MachineId, x => DateTime.Now.Day == x.Day);
        }

        public decimal CalculateTotalIncomes(int MachineId)
        {
            return CalculateIncomes(MachineId);

        }


        private decimal CalculateIncomes(int MachineId, Func<DateTime, bool> dateCompare = null)
        {
            var machine = _machineDAO.GetMachineById(MachineId);
            if (machine == null)
            {
                return 0;
            }
            decimal totalIncome = 0;
            foreach (var cycle in machine.Cycles)
            {
                if (cycle.Actions == null)
                {
                    continue;
                }
                else
                {
                    if (dateCompare is null)
                    {
                        totalIncome += cycle.Actions.Count() * cycle.Cout;
                    }
                    else
                    {
                        totalIncome += cycle.Actions.Where(x => dateCompare(x.Date)).Count() * cycle.Cout;
                    }
                }
            }
            return totalIncome;
        }

    }
}
