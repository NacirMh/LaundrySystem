using LaundrySystem.Domain.Models;
using LaundrySystem.Domain.Dtos.Machine;


namespace LaundrySystem.WebApi.Presentation.Mappers
{
    public static class MachineMapper
    {
        
        public static MachineDTO ToMachineDTO(this Machine machine)
        {
            return new MachineDTO
            {
                Id = machine.Id,
                LaundryId = machine.LaundryId,
                Model = machine.Model,
                State = machine.State,
                Cycles = machine.Cycles.Select(x => x.ToCycleDTO()).ToList(),
            };
        }
    }
}
