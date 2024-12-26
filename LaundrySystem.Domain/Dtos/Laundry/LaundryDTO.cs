using LaundrySystem.Domain.Dtos.Machine;

namespace LaundrySystem.Domain.Dtos.Laundry
{
    public class LaundryDTO
    {

        public int Id { get; set; }
        public string Name { get; set; }


        public string OwnerId { get; set; }
        public decimal TotalIncome { get; set; }

        public decimal TodayIncome { get; set; }

        public decimal MonthIncome { get; set; }
        public List<MachineDTO> Machines { get; set; } = new List<MachineDTO>();
    }
}
