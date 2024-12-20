

using LaundrySystem.Domain.Models;
using LaundrySystem.Domain.ValueObjects;

namespace LaundrySystem.WebApi.Business.Domain.Interfaces
{
    public interface IMachineDAO
    {
        public Machine ChangeMachineState(int MachineId, MachineState machine);
        public Machine? GetMachineById(int machineId);
    
    }
}
