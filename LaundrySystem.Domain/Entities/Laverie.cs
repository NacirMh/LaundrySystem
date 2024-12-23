using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LaundrySystem.Domain.Models
{
    public class Laundry
    {
        [Key]
        public int Id { get; set; } 
        public string Name { get; set; }

        [ForeignKey(nameof(Owner))]
        public string OwnerId { get; set; }
        public virtual Owner Owner { get; set; }
        public virtual IEnumerable<Machine> Machines { get; set; } = new List<Machine>();

    }
}
