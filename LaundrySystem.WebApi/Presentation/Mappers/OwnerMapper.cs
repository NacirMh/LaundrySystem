using LaundrySystem.Domain.Models;
using LaundrySystem.Domain.Dtos.Owner;

namespace LaundrySystem.WebApi.Presentation.Mappers
{
    public static class OwnerMapper
    {
        public static OwnerDTO ToOwnerDTO(this Owner prop)
        {
            return
              new OwnerDTO
            {
                Id = prop.Id,
                Laundries = prop.Laundries.Select(x => x.ToLaundryDTO()).ToList(),
                Name = prop.Name,
            };
        }
    }
}
