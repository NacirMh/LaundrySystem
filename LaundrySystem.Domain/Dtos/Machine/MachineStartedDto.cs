using LaundrySystem.Domain.Dtos.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundrySystem.Domain.Dtos.Machine
{
    public class MachineStartedDto
    {
        public int MachineId { get; set; }
        public int CycleId { get; set; }
        public ActionDTO Action { get; set; }

        public decimal TotalIncome { get; set; }

        public decimal TodayIncome { get; set; }

        public decimal MonthIncome { get; set; }    
    }
}
