using LaundrySystem.Domain.Models;
using LaundrySystem.WebApi.Business.Domain.Interfaces;
using LaundrySystem.WebApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LaundrySystem.WebApi.Infrastructure.Daos
{
    public class ConfigurationDao : IConfigurationDAO
    {
        private readonly AppDbContext _DbContext;
        public ConfigurationDao(AppDbContext dbCon)
        {
            _DbContext = dbCon;
        }


        public  Owner? GetConfigurationById(string id)
        {
            var owner = _DbContext.Owners
                .AsNoTracking()
                .Include(x=>x.Laundries)
                .ThenInclude(x=>x.Machines)
                .ThenInclude(x=>x.Cycles)
                .ThenInclude(x=>x.Actions)
              .FirstOrDefault(x => x.Id == id);
            if (owner != null)
            {
               
            }
            return owner;
        }

        public List<Owner> GetAllConfigurations()
        {
            var configs = _DbContext.Owners.ToList();
            return configs;
        }


    }
}
