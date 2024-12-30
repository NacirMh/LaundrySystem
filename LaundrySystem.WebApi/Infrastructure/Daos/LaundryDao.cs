using LaundrySystem.Domain.Models;
using LaundrySystem.WebApi.Business.Domain.Interfaces;
using LaundrySystem.WebApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LaundrySystem.WebApi.Infrastructure.Daos
{
    public class LaundryDao : ILaundryDao
    {
        private readonly AppDbContext _dbContext;
        public LaundryDao(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Laundry> GetAll()
        {
            
            return _dbContext.Laveries.ToList();
        }

        public Laundry? GetById(int id)
        {
           var laundry = _dbContext.Laveries
                .Include(x=>x.Machines)
               .FirstOrDefault(x => x.Id == id);
           return laundry;
        }
    }
}
