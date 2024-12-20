using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundrySystem.Domain.Models
{
    public class Actionn
    {
        [Key]
        public int Id { get; set; } 
        public int CycleId { get; set; }
        public virtual Cycle Cycle { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;  
    }
}
