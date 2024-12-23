using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundrySystem.Domain.ValueObjects
{
    public class AuthMessage
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Token { get; set; }
    }
}
