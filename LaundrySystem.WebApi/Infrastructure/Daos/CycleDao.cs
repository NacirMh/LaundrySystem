using LaundrySystem.Domain.Models;
using LaundrySystem.WebApi.Business.Domain.Interfaces;
using LaundrySystem.WebApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LaundrySystem.WebApi.Infrastructure.Daos
{
    public class CycleDao : ICycleDAO
    {

        private readonly AppDbContext _dbContext;
        public CycleDao(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Cycle> GetAll()
        {
            return _dbContext.Cycles.ToList();
        }
        
        public Cycle GetById(int id)
        {
            var cycle = _dbContext.Cycles
                .Include(x=>x.Machine)
                .ThenInclude(x=>x.Laundry)
              .FirstOrDefault(x => x.Id == id);
            return cycle;
        }
    }
}
