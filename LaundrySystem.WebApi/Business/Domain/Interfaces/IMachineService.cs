using LaundrySystem.Domain.Models;

namespace LaundrySystem.WebApi.Business.Domain.Interfaces
{
    public interface IMachineService
    {
         public Machine StartMachine(int cycleId);
         public decimal CalculateMachineIncomes(int MachineId, DateOnly? day);
         public Machine StopMachine(int MachineId);
    }
}
