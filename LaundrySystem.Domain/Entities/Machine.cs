
using LaundrySystem.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LaundrySystem.Domain.Models
{
    public class Machine
    {
        [Key]
        public int Id { get; set; } 
        public string Model { get; set; }

        public MachineState State { get; set; } 

        [ForeignKey(nameof(Laundry))]
        public int LaundryId { get; set; }  
        
        public virtual Laundry Laundry { get; set; }
        public virtual IEnumerable<Cycle> Cycles { get; set; } = new List<Cycle>();

    }
}
