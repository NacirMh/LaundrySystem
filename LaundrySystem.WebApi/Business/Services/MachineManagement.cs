using LaundrySystem.Domain.Models;
using LaundrySystem.Domain.Dtos.Cycle;
using LaundrySystem.Domain.Dtos.Machine;
using LaundrySystem.WebApi.Business.Domain.Interfaces;
using LaundrySystem.Domain.ValueObjects;
using LaundrySystem.WebApi.Infrastructure.Daos;

namespace LaundrySystem.WebApi.Business.Services
{
    public class MachineManagement : IMachineService
    {
        private readonly IMachineDAO _machineDAO;
        private readonly IActionDAO _actionDAO;
        private readonly ICycleDAO _cycleDAO;
        public MachineManagement(IMachineDAO machineDAO, IActionDAO actionDAO , ICycleDAO cycleDAO)
        {
            _machineDAO = machineDAO;
            _actionDAO = actionDAO;
            _cycleDAO = cycleDAO;
        }

        public Machine StartMachine(int cycleId)
        {
            Cycle cycle = _cycleDAO.GetById(cycleId);
            Machine machine = _machineDAO.ChangeMachineState(cycle.MachineId , MachineState.Running);
            Actionn action = new Actionn
            {
                CycleId = cycleId,
            };
            _actionDAO.CreateAction(action);
            return machine;
        }


        public decimal CalculateMachineIncomes(int MachineId, DateOnly? day)
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
                if (day is not null)
                {
                    totalIncome += cycle.Actions.Where(x=> DateOnly.FromDateTime(x.Date).CompareTo(day) == 0).Count() * cycle.Cout;
                }
                else
                {
                    totalIncome += cycle.Actions.Count() * cycle.Cout;
                }
            }
            return totalIncome;
        }

        public Machine StopMachine(int MachineId)
        {
            Machine machine = _machineDAO.ChangeMachineState(MachineId,MachineState.Stopped);
            return machine;
        }
    }
}
