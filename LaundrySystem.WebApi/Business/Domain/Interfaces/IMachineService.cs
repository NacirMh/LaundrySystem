using LaundrySystem.Domain.Models;

namespace LaundrySystem.WebApi.Business.Domain.Interfaces
{
    public interface IMachineService
    {
         public Actionn StartMachine(int cycleId);
         public decimal CalculateMonthIncomes(int MachineId);
         public decimal CalculateTodayIncomes(int MachineId);
         public decimal CalculateTotalIncomes(int MachineId);
        public Machine StopMachine(int MachineId);
    }
}
