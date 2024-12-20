using System.ComponentModel.DataAnnotations;

namespace LaundrySystem.Domain.Models
{
    public class Owner
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<Laundry> Laundries { get; set; } = new List<Laundry>();

    }
}
