using LaundrySystem.Domain.Models;
using LaundrySystem.WebApi.Business.Domain.Interfaces;
using LaundrySystem.WebApi.Infrastructure.Data;

namespace LaundrySystem.WebApi.Infrastructure.Daos
{
    public class OwnerDao : IOwnerDao
    {
        private readonly AppDbContext _dbContext;
        public OwnerDao(AppDbContext appDbContext)
        {
             _dbContext = appDbContext;
        }
        public Owner? Login(string name, string password)
        {
            var owner = _dbContext.Owners.FirstOrDefault(x=>x.Name == name  && x.Password == password);
            return owner;
        }
    }
}
