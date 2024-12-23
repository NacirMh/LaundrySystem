using System.ComponentModel.DataAnnotations;

namespace LaundrySystem.Domain.Models
{
    public class Owner
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public virtual IEnumerable<Laundry> Laundries { get; set; } = new List<Laundry>();

    }
}
