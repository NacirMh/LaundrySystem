using LaundrySystem.Domain.Dtos.Machine;

namespace LaundrySystem.Domain.Dtos.Laundry
{
    public class LaundryDTO
    {

        public int Id { get; set; }
        public string Name { get; set; }


        public string OwnerId { get; set; }

        public List<MachineDTO> Machines { get; set; } = new List<MachineDTO>();
    }
}
