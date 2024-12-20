using LaundrySystem.Domain.Models;
using LaundrySystem.Domain.Dtos.Cycle;

namespace LaundrySystem.WebApi.Presentation.Mappers
{
    public static class CycleMapper
    {

        public static CycleDTO ToCycleDTO(this Cycle cycle)
        {
            return new CycleDTO
            {
                Id = cycle.Id,
                Durée = cycle.Durée,
                Cout = cycle.Cout,
                MachineId = cycle.MachineId,
                Actions = cycle.Actions.Select(x => x.ToActionDTO()).ToList(),
            };
        }
        public static Cycle ToCycle(this CycleDTO cycle)
        {
            return new Cycle
            {
                Id = cycle.Id,
                Durée = cycle.Durée,
                Cout = cycle.Cout,
                MachineId = cycle.MachineId,
            };
        }
    }
}
