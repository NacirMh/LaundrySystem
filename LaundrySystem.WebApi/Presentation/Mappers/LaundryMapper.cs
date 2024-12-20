using LaundrySystem.Domain.Models;
using LaundrySystem.Domain.Dtos.Laundry;

namespace LaundrySystem.WebApi.Presentation.Mappers
{
    public static class LaundryMapper
    {
        public static LaundryDTO ToLaundryDTO(this Laundry laverie)
        {
            return new LaundryDTO
            {
                Id = laverie.Id,
                Name = laverie.Name,
                OwnerId = laverie.OwnerId,
                Machines = laverie.Machines.Select(x => x.ToMachineDTO()).ToList(),
            };

        }
    }
}
