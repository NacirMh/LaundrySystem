using LaundrySystem.Domain.Dtos.Laundry;

namespace LaundrySystem.Domain.Dtos.Owner
{
    public class OwnerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<LaundryDTO> Laundries { get; set; } = new List<LaundryDTO>();
    }
}
