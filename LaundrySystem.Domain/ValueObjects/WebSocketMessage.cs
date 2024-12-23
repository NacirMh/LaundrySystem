using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundrySystem.Domain.ValueObjects
{
    public class WebSocketMessage
    {
        public string Type { get; set; }
        public Object Message { get; set; }
    }
}
