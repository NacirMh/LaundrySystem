using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundrySystem.Domain.Models
{
    public class Cycle
    {
        [Key]
        public int Id { get; set; } 
        public decimal Cout { get; set; }

        public int Durée { get; set; }

        [ForeignKey(nameof(Machine))]
        public int MachineId { get; set; }  
        public virtual Machine Machine { get; set; }

        public virtual IEnumerable<Actionn> Actions { get; set; } = new List<Actionn>();
    }
}
