using LaundrySystem.Domain.Dtos.Cycle;
using LaundrySystem.Domain.ValueObjects;

namespace LaundrySystem.Domain.Dtos.Machine
{
    public class MachineDTO
    {
        public int Id { get; set; }
        public string Model { get; set; }

        public  MachineState State  { get; set; }   
        public int LaundryId { get; set; }
        public decimal TotalIncome { get; set; }

        public decimal TodayIncome { get; set; }

        public decimal MonthIncome { get; set; }
        public List<CycleDTO> Cycles { get; set; } = new List<CycleDTO>();
    }
}
