using LaundrySystem.Domain.Models;
using LaundrySystem.Domain.Dtos.Machine;
using LaundrySystem.WebApi.Business.Domain.Interfaces;
using LaundrySystem.WebApi.Infrastructure.Data;
using LaundrySystem.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;


namespace LaundrySystem.WebApi.Infrastructure.Daos
{
    public class MachineDao : IMachineDAO
    {
        private readonly AppDbContext _dbContext;
        public MachineDao(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

       
        public Machine? ChangeMachineState(int machineId ,MachineState state)
        {
            var machine = GetMachineById(machineId);
            if (machine == null)
            {
                return null;
            }
            machine.State = state;
            _dbContext.SaveChanges();
            return machine;
        }

        public Machine? GetMachineById(int machineId) {
            var machine = _dbContext.Machines
                .Include(x => x.Laundry)
                .Include(x=>x.Cycles)
                .ThenInclude(x=>x.Actions)
               .FirstOrDefault(x => x.Id == machineId);

            return machine;
        }
    }
}
