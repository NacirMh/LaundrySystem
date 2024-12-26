using LaundrySystem.Domain.Dtos.Laundry;
using Microsoft.EntityFrameworkCore;

namespace LaundrySystem.Domain.Dtos.Owner
{
    public class OwnerDTO
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public List<LaundryDTO> Laundries { get; set; } = new List<LaundryDTO>();
    }
}
