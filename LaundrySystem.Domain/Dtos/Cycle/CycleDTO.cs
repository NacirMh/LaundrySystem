using LaundrySystem.Domain.Dtos.Action;

namespace LaundrySystem.Domain.Dtos.Cycle
{
    public class CycleDTO
    {

        public int Id { get; set; }
        public decimal Cout { get; set; }

        public int Duration { get; set; }

        public int MachineId { get; set; }
        public List<ActionDTO> Actions { get; set; } = new List<ActionDTO>();

    }
}
